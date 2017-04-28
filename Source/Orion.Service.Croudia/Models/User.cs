using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Orion.Service.Croudia.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("id_str")]
        public string IdStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("cover_image_url_https")]
        public string CoverImageUrl { get; set; }

        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("favorites_count")]
        public int FavoritesCount { get; set; }

        [JsonProperty("follow_request_sent")]
        internal string FollowRequestSent { get; set; }

        public bool IsFollowRequestSent => bool.TryParse(FollowRequestSent, out bool b) && b;

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("following")]
        internal string Following { get; set; }

        public bool IsFollowing => bool.TryParse(Following, out bool b) && b;

        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }

        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}