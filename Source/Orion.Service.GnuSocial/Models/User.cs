using System;

using Newtonsoft.Json;

namespace Orion.Service.GnuSocial.Models
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrlHttps { get; set; }

        [JsonProperty("profile_image_url_profile_size")]
        public string ProfileImageUrlProfileSize { get; set; }

        [JsonProperty("profile_image_url_original")]
        public string ProfileImageUrlOriginal { get; set; }

        [JsonProperty("groups_count")]
        public int GroupsCount { get; set; }

        [JsonProperty("linkcolor")]
        public string LinkColor { get; set; }

        [JsonProperty("backgroundcolor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("protected")]
        public bool IsProtected { get; set; }

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("friends_count")]
        public int FriendsCount { get; set; }

        [JsonProperty("created_at")]
        // [JsonConverter(typeof(IsoDateTimeConverter))]
        public string CreatedAt { get; set; }

        [JsonProperty("utf_offset")]
        public string UtfOffset { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }

        [JsonProperty("following")]
        public bool IsFollowing { get; set; }

        [JsonProperty("statusnet_blocking")]
        public bool IsStatusnetBlocking { get; set; }

        [JsonProperty("notifications")]
        public bool IsNotifications { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("statusnet_profile_url")]
        public string StatusnetProfileUrl { get; set; }

        [JsonProperty("cover_photo")]
        public string CoverPhoto { get; set; }

        [JsonProperty("background_image")]
        public string BackgroundImage { get; set; }

        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor { get; set; }

        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor { get; set; }

        [JsonProperty("profile_banner_url")]
        public string ProfileBannerUrl { get; set; }

        [JsonProperty("follows_you")]
        public bool IsFollowsYou { get; set; }

        [JsonProperty("blocks_you")]
        public bool IsBlocksYou { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }

        [JsonProperty("is_silenced")]
        public bool IsSilenced { get; set; }

        [JsonProperty("rights")]
        public Rights Rights { get; set; }

        [JsonProperty("is_sandboxed")]
        public bool IsSandboxed { get; set; }

        [JsonProperty("ostatus_uri")]
        public Uri OStatusUri { get; set; }

        [JsonProperty("favourites_count")]
        public int FavouritesCount { get; set; }
    }
}