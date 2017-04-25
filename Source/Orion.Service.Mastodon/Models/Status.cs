using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Orion.Service.Mastodon.Enum;

namespace Orion.Service.Mastodon.Models
{
    public class Status
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("in_reply_to_id")]
        public int? InReplyToId { get; set; }

        [JsonProperty("in_reply_to_account_id")]
        public int? InReplyToAccountId { get; set; }

        [JsonProperty("reblog")]
        public Status Reblog { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("reblogs_count")]
        public int ReblogsCount { get; set; }

        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }

        [JsonProperty("reblogged")]
        public bool? IsReblogged { get; set; }

        [JsonProperty("favourited")]
        public bool? IsFavourited { get; set; }

        [JsonProperty("sensitive")]
        public bool? IsSensitive { get; set; } = false;

        [JsonProperty("spoiler_text")]
        public string SpoilerText { get; set; }

        [JsonProperty("visibility")]
        public VisibilityType Visibility { get; set; }

        [JsonProperty("media_attachments")]
        public List<Attachment> MediaAttachments { get; set; }

        [JsonProperty("mentions")]
        public List<Mention> Mentions { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("application")]
        public Application Application { get; set; }
    }
}