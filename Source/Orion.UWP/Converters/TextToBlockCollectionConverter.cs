using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;

using HtmlAgilityPack;

using Microsoft.Practices.Unity;

using Orion.UWP.Models;
using Orion.UWP.Services.Interfaces;

using Prism.Unity.Windows;

using ToriatamaText;

namespace Orion.UWP.Converters
{
    internal class TextToBlockCollectionConverter : IValueConverter
    {
        private static readonly string HtmlTags = string.Join("|", "a", "p", "span", "strong");
        private readonly Regex _newLineRegex = new Regex(@"<br(\s)?/>", RegexOptions.Compiled);
        private readonly Regex _tagRegex = new Regex($@"<[{HtmlTags}]( .*)?>.*?</[{HtmlTags}]>", RegexOptions.Compiled);

        private ITimelineService _timelineService;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (_timelineService == null)
                _timelineService = PrismUnityApplication.Current.Container.Resolve<ITimelineService>();
            if (value is ParsableText parsableText)
                return ParseText(parsableText);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private List<Block> ParseText(ParsableText parsableText)
        {
            var text = parsableText.Text;
            // Is HTML?
            if (_tagRegex.IsMatch(text) || _newLineRegex.IsMatch(text))
                text = FlattenHtmlText(text);

            text = text.Replace("\n", Environment.NewLine);
            text = text.Replace("<br />", Environment.NewLine);
            text = text.Replace("&lt;", "<"); // Twitter
            text = text.Replace("&gt;", ">"); // Twitter
            text = text.Replace("&apos;", "'"); // Mastodon
            text = text.Replace("&quot;", "\""); // Mastodon
            text = text.Replace("&amp;", "&"); // Twitter
            text = text.Trim();

            if (string.IsNullOrWhiteSpace(text))
                return new List<Block>();

            var extractor = new Extractor();
            var paragraph = new Paragraph();
            var currentIndex = 0;
            foreach (var entity in extractor.ExtractEntities(text))
                switch (entity.Type)
                {
                    case EntityType.Url:
                        ParseBeforeText(text, ref currentIndex, paragraph, entity);
                        var url = text.Substring(entity.StartIndex, entity.Length);
                        paragraph.Inlines.Add(parsableText.Hyperlinks.ContainsKey(url)
                            ? CreateHyperlink(parsableText.Hyperlinks[url], url)
                            : CreateHyperlink(url, url));
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
                paragraph.Inlines.Add(new Run {Text = text.Substring(currentIndex)});
            return new List<Block> {paragraph};
        }

        private void ParseBeforeText(string text, ref int currentIndex, Paragraph paragraph, EntityInfo entity)
        {
            if (currentIndex >= entity.StartIndex)
                return;

            paragraph.Inlines.Add(new Run {Text = text.Substring(currentIndex, entity.StartIndex - currentIndex)});
            currentIndex = entity.StartIndex;
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
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                var authoriry = uri.Authority;
                if (authoriry.StartsWith("www"))
                    authoriry = authoriry.Substring(4);
                var localPath = uri.PathAndQuery;
                if (localPath.Length > 14)
                    localPath = $"{localPath.Substring(0, 14)}...";
                return $"{authoriry}{localPath}";
            }
            return url;
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
                    sb.Append("<br />");
                    break;

                case "p":
                    ParseChildNodes(sb, node);
                    sb.Append("<br />");
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