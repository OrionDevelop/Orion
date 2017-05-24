using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;

using HtmlAgilityPack;

using ToriatamaText;

namespace Orion.UWP.Converters
{
    internal class TextToBlockCollectionConverter : IValueConverter
    {
        private static readonly string HtmlTags = string.Join("|", "a", "p", "span", "strong");
        private readonly Regex _tagRegex = new Regex($@"<[{HtmlTags}]( .*)?>.*?</[{HtmlTags}]>", RegexOptions.Compiled);
        private readonly Regex _tcoRegex = new Regex("<tco disp=\"(?<display_url>.*?)\">(?<original_url>.*?)</tco>", RegexOptions.Compiled);

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string strValue)
                return ParseText(strValue);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private List<Block> ParseText(string value)
        {
            var text = value;
            // Is HTML?
            if (_tagRegex.IsMatch(value) || value.Contains("br"))
                text = FlattenHtmlText(value);
            text = text.Replace("&lt;", "<");
            text = text.Replace("&gt;", ">");
            text = text.Replace("&amp;", "&");
            text = text.Replace("\n", Environment.NewLine);

            var extractor = new Extractor();
            var paragraph = new Paragraph();
            var currentIndex = 0;
            foreach (var entity in extractor.ExtractEntities(text))
                switch (entity.Type)
                {
                    case EntityType.Url:
                        ParseBeforeText(text, ref currentIndex, paragraph, entity);
                        var url = text.Substring(entity.StartIndex, entity.Length);
                        paragraph.Inlines.Add(CreateHyperlink(url, url));
                        currentIndex = entity.StartIndex + entity.Length;
                        break;

                    case EntityType.Hashtag:
                        ParseBeforeText(text, ref currentIndex, paragraph, entity);
                        var hashtag = text.Substring(entity.StartIndex, entity.Length);
                        paragraph.Inlines.Add(CreateHyperlink(hashtag, null));
                        currentIndex = entity.StartIndex + entity.Length;
                        break;

                    case EntityType.Mention:
                        ParseBeforeText(text, ref currentIndex, paragraph, entity);
                        var mention = text.Substring(entity.StartIndex, entity.Length);
                        paragraph.Inlines.Add(CreateHyperlink(mention, null));
                        currentIndex = entity.StartIndex + entity.Length;
                        break;

                    case EntityType.Cashtag:
                        ParseBeforeText(text, ref currentIndex, paragraph, entity);
                        var cashtag = text.Substring(entity.StartIndex, entity.Length);
                        paragraph.Inlines.Add(CreateHyperlink(cashtag, null));
                        currentIndex = entity.StartIndex + entity.Length;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            if (currentIndex < text.Length)
                ParseTcoUrl(text.Substring(currentIndex), paragraph);
            return new List<Block> {paragraph};
        }

        private void ParseBeforeText(string text, ref int currentIndex, Paragraph paragraph, EntityInfo entity)
        {
            if (currentIndex >= entity.StartIndex)
                return;

            var target = text.Substring(currentIndex, entity.StartIndex - currentIndex);
            ParseTcoUrl(target, paragraph);
            currentIndex = entity.StartIndex;
        }

        private void ParseTcoUrl(string text, Paragraph paragraph)
        {
            if (_tcoRegex.IsMatch(text))
            {
                var regIndex = 0;
                // Create hyperlink with t.co
                foreach (Match match in _tcoRegex.Matches(text))
                {
                    if (regIndex < match.Index)
                        paragraph.Inlines.Add(new Run {Text = text.Substring(regIndex, match.Index - regIndex)});
                    paragraph.Inlines.Add(CreateHyperlink(match.Groups["display_url"].Value.Replace("^", "."),
                                                          $"https://t.co{match.Groups["original_url"].Value}", false));
                    regIndex = match.Index + match.Length;
                }
                if (regIndex < text.Length)
                    paragraph.Inlines.Add(new Run {Text = text.Substring(regIndex)});
            }
            else
            {
                paragraph.Inlines.Add(new Run {Text = text});
            }
        }

        private Hyperlink CreateHyperlink(string text, string url, bool format = true)
        {
            var hyperlink = new Hyperlink();
            if (!string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
                hyperlink.NavigateUri = uri;
            hyperlink.Inlines.Add(format ? new Run {Text = FormatUrl(text)} : new Run {Text = text});
            return hyperlink;
        }

        private string FormatUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uri)
                ? $"{uri.Authority}{(uri.LocalPath.Length > 14 ? $"{uri.LocalPath.Substring(0, 14)}..." : uri.LocalPath)}"
                : url;
        }

        #region HTML parse

        private string FlattenHtmlText(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var sb = new StringBuilder();
            ParseChildNodes(sb, document.DocumentNode);
            return sb.ToString().TrimEnd();
        }

        private void ParseChildNodes(StringBuilder sb, HtmlNode node)
        {
            var isParsed = false;
            foreach (var childNode in node.ChildNodes)
            {
                ParseTagsPerNode(sb, childNode);
                isParsed = true;
            }

            if (!isParsed)
                sb.Append(node.InnerText);
        }

        private void ParseTagsPerNode(StringBuilder sb, HtmlNode node)
        {
            switch (node.Name)
            {
                case "a":
                case "span":
                case "strong":
                    ParseChildNodes(sb, node);
                    break;

                case "br":
                    sb.Append(Environment.NewLine);
                    break;

                case "p":
                    ParseChildNodes(sb, node);
                    sb.Append(Environment.NewLine);
                    break;

                case "#text":
                    sb.Append(node.InnerText);
                    break;

                default:
                    ParseChildNodes(sb, node);
                    sb.Append(Environment.NewLine);
                    break;
            }
        }

        #endregion
    }
}