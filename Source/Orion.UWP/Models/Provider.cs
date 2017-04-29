using System;
using System.Text.RegularExpressions;

using Orion.UWP.Models.Clients;
using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models
{
    public class Provider
    {
        public string Name { get; set; }

        public ServiceType Service { get; set; }

        public string Host { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public bool RequireHost { get; set; } = true;

        public bool RequireApiKeys { get; set; } = true;

        /// <summary>
        ///     `verifier` を取得し、認証用コードとして使用します。
        /// </summary>
        public Regex ParseRegex { get; set; }

        public BaseClientWrapper CreateClientWrapper()
        {
            switch (Service)
            {
                case ServiceType.Twitter:
                    return new TwitterClientWrapper(this);

                case ServiceType.Croudia:
                    return new CroudiaClientWrapper(this);

                case ServiceType.GnuSocial:
                    return new GnuSocialClientWrapper(this);

                case ServiceType.Mastodon:
                    return new MastodonClientWrapper(this);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool ValidateConfiguration(string host, string clientId, string clientSecret)
        {
            if (RequireHost && string.IsNullOrWhiteSpace(host))
                return false;

            if (RequireApiKeys && (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret)))
                return false;

            return true;
        }

        public void Configure(string host, string clientId, string clientSecret)
        {
            if (RequireHost)
                Host = host;

            if (!RequireApiKeys)
                return;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}