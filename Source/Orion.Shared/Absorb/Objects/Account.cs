using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Orion.Shared.Absorb.Clients;
using Orion.Shared.Enums;
using Orion.UWP.Models;
using Orion.UWP.Models.Clients;

namespace Orion.Shared.Absorb.Objects
{
    public class Account
    {
        /// <summary>
        ///     Unique ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Service provider
        /// </summary>
        public Provider Provider { get; set; }

        /// <summary>
        ///     Credentials
        /// </summary>
        public Credential Credential { get; set; }

        /// <summary>
        ///     Mark as default (If timeline is not associated with account, use this)
        /// </summary>
        public bool MarkAsDefault { get; set; }

        /// <summary>
        ///     Client wrapper
        /// </summary>
        [JsonIgnore]
        public BaseClientWrapper ClientWrapper { get; set; }

        public Account()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Task<bool> RestoreAsync()
        {
            ClientWrapper = CreateClientWrapper();
            return ClientWrapper.RefreshAccountAsync();
        }

        public IEnumerable<TimelineType> DefaultTimelines()
        {
            switch (Provider.Service)
            {
                case ServiceType.Twitter:
                case ServiceType.Croudia:
                case ServiceType.GnuSocial:
                    return new[] {TimelineType.HomeTimeline, TimelineType.Mentions};

                case ServiceType.Mastodon:
                    return new[] {TimelineType.PublicTimeline, TimelineType.Notifications};

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private BaseClientWrapper CreateClientWrapper()
        {
            switch (Provider.Service)
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
    }
}