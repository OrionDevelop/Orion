using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

using Orion.Shared.Absorb.Clients;
using Orion.Shared.Enums;
using Orion.UWP.Models.Clients;

namespace Orion.Shared
{
    public class Provider
    {
        public string Name { get; set; }

        public ServiceType Service { get; set; }

        public string Host { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        [JsonIgnore]
        public bool RequireHost { get; set; } = true;

        [JsonIgnore]
        public bool RequireApiKeys { get; set; } = true;

        /// <summary>
        ///     `verifier` を取得し、認証用コードとして使用します。
        /// </summary>
        [JsonIgnore]
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

        public IEnumerable<TimelineType> GetSupportTimelineTypes()
        {
            switch (Service)
            {
                case ServiceType.Twitter:
                case ServiceType.Croudia:
                    return new List<TimelineType>
                    {
                        TimelineType.HomeTimeline,
                        TimelineType.Mentions,
                        TimelineType.DirectMessages
                    };

                case ServiceType.GnuSocial:
                    return new List<TimelineType>
                    {
                        TimelineType.HomeTimeline,
                        TimelineType.Mentions,
                        TimelineType.DirectMessages,
                        TimelineType.PublicTimeline,
                        TimelineType.FederatedTimeline
                    };

                case ServiceType.Mastodon:
                    return new List<TimelineType>
                    {
                        TimelineType.HomeTimeline,
                        TimelineType.PublicTimeline,
                        TimelineType.FederatedTimeline,
                        TimelineType.Notifications
                    };

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool ValidateConfiguration(string host, string clientId, string clientSecret)
        {
            if (RequireHost && string.IsNullOrWhiteSpace(host))
                return false;

            return !RequireApiKeys || !string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret);
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