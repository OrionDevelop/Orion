using System;
using System.Collections.Generic;

using Windows.Data.Html;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;

using HtmlAgilityPack;

namespace Orion.UWP.Converters
{
    internal class HtmlStringToBlockCollectionConverter : DependencyObject, IValueConverter
    {
        private List<Block> GenerateBlockContents(string html)
        {
            var blocks = new List<Block>();
            try
            {
                var document = new HtmlDocument();
                document.LoadHtml(html);
                blocks.Add(GenerateParagraph(document.DocumentNode));
            }
            catch
            {
                // ignored
            }
            return blocks;
        }

        private Paragraph GenerateParagraph(HtmlNode node)
        {
            var paragraph = new Paragraph();
            AddChildren(paragraph, node);
            return paragraph;
        }

        private void AddChildren(Paragraph paragraph, HtmlNode node)
        {
            var added = false;
            foreach (var childNode in node.ChildNodes)
            {
                var inline = GenerateBlocksForNode(childNode);
                if (inline == null)
                    continue;
                if (inline is Hyperlink)
                    paragraph.Inlines.Add(new Run { Text = " " });
                paragraph.Inlines.Add(inline);
                added = true;
            }
            if (!added)
                paragraph.Inlines.Add(new Run {Text = GeneratePlainText(node)});
        }

        private void AddChildren(Span span, HtmlNode node)
        {
            var added = false;
            foreach (var childNode in node.ChildNodes)
            {
                var inline = GenerateBlocksForNode(childNode);
                if (inline == null)
                    continue;
                span.Inlines.Add(inline);
                if(inline is Hyperlink)
                    span.Inlines.Add(new Run{Text = " "});
                added = true;
            }
            if (!added)
                span.Inlines.Add(new Run {Text = GeneratePlainText(node)});
        }

        private Inline GenerateBlocksForNode(HtmlNode node)
        {
            switch (node.Name)
            {
                case "a":
                    return GenerateHyperlink(node);

                case "br":
                    return new LineBreak();

                case "p":
                    return GenerateInnerParagraph(node);

                case "span":
                    return GenerateSpan(node);

                case "strong":
                    return GenerateStrong(node);

                case "#text":
                    if (!string.IsNullOrWhiteSpace(node.InnerText))
                        return new Run {Text = GeneratePlainText(node)};
                    break;

                default:
                    return GenerateSpawnNewLine(node);
            }
            return null;
        }

        private string GeneratePlainText(HtmlNode node)
        {
            return HtmlUtilities.ConvertToText(node.InnerText);
        }

        private Inline GenerateHyperlink(HtmlNode node)
        {
            var hyperlink = new Hyperlink {NavigateUri = new Uri(node.Attributes["href"].Value)};
            hyperlink.Inlines.Add(new Run {Text = GeneratePlainText(node)});
            return hyperlink;
        }

        private Inline GenerateInnerParagraph(HtmlNode node)
        {
            var span = new Span();
            AddChildren(span, node);
            return span;
        }

        private Inline GenerateSpan(HtmlNode node)
        {
            var span = new Span();
            AddChildren(span, node);
            return span;
        }

        private Inline GenerateStrong(HtmlNode node)
        {
            var bold = new Bold();
            AddChildren(bold, node);
            return bold;
        }

        private Inline GenerateSpawnNewLine(HtmlNode node)
        {
            var span = new Span();
            AddChildren(span, node);
            if (span.Inlines.Count > 0)
                span.Inlines.Add(new LineBreak());
            return span;
        }

        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is string))
                return null;
            return GenerateBlockContents($"<p>{(string) value}</p>");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}