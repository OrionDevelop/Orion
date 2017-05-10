using System.Linq;
using System.Text.RegularExpressions;

namespace Orion.UWP.Models.Emoji
{
    public static class EmojiConverter
    {
        private static readonly Regex EmojiRegex = new Regex(":[A-Za-z0-9_]+:", RegexOptions.Compiled);

        public static string Convert(string text)
        {
            if (!EmojiRegex.IsMatch(text))
                return text;

            foreach (Match match in EmojiRegex.Matches(text))
            {
                if (EmojiConstants.Emojis.All(w => w.Shortname != match.Value))
                    continue;

                var emoji = EmojiConstants.Emojis.Single(w => w.Shortname == match.Value);
                text = text.Replace(emoji.Shortname, emoji.Unicode);
            }
            return text;
        }
    }
}