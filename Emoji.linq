<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var httpClient = new HttpClient();
var response = await httpClient.GetAsync("https://raw.githubusercontent.com/Ranks/emojione/master/emoji_strategy.json");
var emojiStrategy = await response.Content.ReadAsStringAsync();

var emojis = JObject.Parse(emojiStrategy);

var emojiMapping = new StringBuilder();
foreach (var emoji in emojis)
{
    var unicode = emoji.Value["unicode_output"].ToString().Split('-').Select(w => $"\\u{w.PadLeft(8, '0')}");
    emojiMapping.AppendLine($"new Emoji {{ Shortname = \"{ emoji.Value["shortname"] }\", Unicode = \"{ string.Join("", unicode) }\" }},");
}

// EmojiConstants.cs
var clazz = $@"
public static class EmojiConstants
{{
    public static List<Emoji> Emojis {{ get; }} = new List<Emoji> {{
      {emojiMapping.ToString()}
    }};
}}
";
clazz.Dump();
