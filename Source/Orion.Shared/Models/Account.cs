using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Orion.Shared.Absorb.Clients;
using Orion.Shared.Enums;

namespace Orion.Shared.Models
{
    public class Account
    {
        public static int GlobalOrderIndex;

        /// <summary>
        ///     Unique ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Index
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        ///     Service provider
        /// </summary>
        public Provider Provider { get; set; }

        /// <summary>
        ///     Credential
        /// </summary>
        public Credential Credential { get; set; }

        /// <summary>
        ///     Mark as default (If timeline is not associated with any account, use marked account.)
        /// </summary>
        public bool IsMarkAsDefault { get; set; }

        /// <summary>
        ///     API client
        /// </summary>
        [JsonIgnore]
        public BaseClientWrapper ClientWrapper { get; set; }

        public Account()
        {
            Id = Guid.NewGuid().ToString();
            OrderIndex = GlobalOrderIndex++;
        }

        public Task<bool> RestoreAsync()
        {
            CreateClientWrapper();
            return ClientWrapper.RefreshAccountAsync();
        }

        public void CreateClientWrapper()
        {
            if (Credential == null)
                Credential = new Credential();

            switch (Provider.ProviderType)
            {
                case ProviderType.Twitter:
                    ClientWrapper = new TwitterClientWrapper(Provider, Credential);
                    break;

                case ProviderType.GnuSocial:
                    ClientWrapper = new GnuSocialClientWrapper(Provider, Credential);
                    break;

                case ProviderType.Mastodon:
                    ClientWrapper = new MastodonClientWrapper(Provider, Credential);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IEnumerable<TimelinePreset> DefaultTimelines()
        {
            switch (Provider.ProviderType)
            {
                case ProviderType.Twitter:
                    return new List<TimelinePreset>
                    {
                        SharedConstants.TwitterHomeTimelinePreset,
                        SharedConstants.TwitterMentionsTimelinePreset
                    };

                case ProviderType.GnuSocial:
                    return new List<TimelinePreset>
                    {
                        SharedConstants.GnuSocialFederatedTimelinePreset,
                        SharedConstants.GnuSocialMentionsTimelinePreset
                    };

                case ProviderType.Mastodon:
                    return new List<TimelinePreset>
                    {
                        SharedConstants.MastodonLocalTimelinePreset,
                        SharedConstants.MastodonNotificationsTimelinePreset
                    };

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}