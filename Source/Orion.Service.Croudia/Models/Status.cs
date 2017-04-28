using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Orion.Service.Croudia.Models
{
    public class Status
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("favorited")]
        public bool IsFavorited { get; set; }

        [JsonProperty("favorited_count")]
        public int FavoritedCount { get; set; }

        [JsonProperty("spread")]
        public bool Spread { get; set; }

        [JsonProperty("spread_count")]
        public int SpreadCount { get; set; }

        [JsonProperty("spread_status")]
        public Status SpreadStatus { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }

        [JsonProperty("in_reply_to_status_id")]
        public int? InReplyToStatusId { get; set; }

        [JsonProperty("in_reply_to_status_id_str")]
        public string InReplyToStatusIdStr { get; set; }

        [JsonProperty("in_reply_to_user_id")]
        public int? InReplyToUserId { get; set; }

        [JsonProperty("in_reply_to_user_id_str")]
        public string InReplyToUserIdStr { get; set; }

        [JsonProperty("in_reply_to_screen_name")]
        public string InReplyToScreenName { get; set; }

        [JsonProperty("quote_status")]
        public Status QuoteStatus { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}