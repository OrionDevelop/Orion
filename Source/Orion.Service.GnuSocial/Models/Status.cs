using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models
{
    public class Status
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("truncated")]
        public bool IsTruncated { get; set; }

        [JsonProperty("created_at")]
        // [JsonConverter(typeof(IsoDateTimeConverter))]
        public string CreatedAt { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public int? InReplyToStatusId { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public int? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("geo")]
        public string Geo { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("statusnet_html")]
        public string StatusNetHtml { get; set; }

        [JsonProperty("statusnet_conversation_id")]
        public int StatusNetConversationId { get; set; }

        [JsonProperty("statusnet_in_groups")]
        public string StatusNetInGroups { get; set; }

        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }

        [JsonProperty("in_reply_to_profileurl")]
        public string InReplyToProfileUrl { get; set; }

        [JsonProperty("in_reply_to_ostatus_uri")]
        public Uri InRepltToOStatusUri { get; set; }

        [JsonProperty("attentions")]
        public List<Attention> Attentions { get; set; }

        [JsonProperty("fav_num")]
        public int FavNum { get; set; }

        [JsonProperty("repeat_num")]
        public int RepeatNum { get; set; }

        [JsonProperty("is_post_verb")]
        public bool IsPostVerb { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("favorited")]
        public bool IsFavorited { get; set; }

        [JsonProperty("repeated")]
        public bool IsRepeated { get; set; }
    }
}