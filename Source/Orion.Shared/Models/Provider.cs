using System.Text.RegularExpressions;

using Newtonsoft.Json;

using Orion.Shared.Enums;

namespace Orion.Shared.Models
{
    public class Provider
    {
        /// <summary>
        ///     Provider name (e.g. Twitter, Mastodon)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     What is this provider?
        /// </summary>
        public ProviderType ProviderType { get; set; }

        /// <summary>
        ///     Host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///     Client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        ///     Consumer Key
        /// </summary>
        [JsonIgnore]
        public string ConsumerKey
        {
            get => ClientId;
            set => ClientId = value;
        }

        /// <summary>
        ///     Client Secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        ///     Consumer Secret
        /// </summary>
        [JsonIgnore]
        public string ConsumerSecret
        {
            get => ClientSecret;
            set => ClientSecret = value;
        }

        [JsonIgnore]
        public bool IsRequireHost { get; set; } = true;

        [JsonIgnore]
        public bool IsRequireApiKeys { get; set; } = true;

        [JsonIgnore]
        public Regex UrlParseRegex { get; set; }

        public bool Validate(string host, string clientId, string clientSecret)
        {
            if (IsRequireHost && string.IsNullOrWhiteSpace(host))
                return false;

            return !IsRequireApiKeys || !string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret);
        }

        public void Configure(string host, string clientId, string clientSecret)
        {
            if (IsRequireHost)
                Host = host;

            if (!IsRequireApiKeys)
                return;

            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}