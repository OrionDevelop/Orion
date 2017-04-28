using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Orion.Service.Croudia.Models
{
    public class SecretMail
    {
        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("recipient")]
        public User Recipient { get; set; }

        [JsonProperty("recipient_id")]
        public int RecipientId { get; set; }

        [JsonProperty("recipient_screen_name")]
        public string RecipientScreenName { get; set; }

        [JsonProperty("sender")]
        public User Sender { get; set; }

        [JsonProperty("sender_id")]
        public int SenderId { get; set; }

        [JsonProperty("sender_screen_name")]
        public string SenderScreenName { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("entities")]
        public Entities Entities { get; set; }
    }
}