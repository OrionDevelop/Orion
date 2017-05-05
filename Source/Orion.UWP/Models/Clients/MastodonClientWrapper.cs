using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Orion.Service.FkStreaming;
using Orion.Service.Mastodon;
using Orion.Service.Mastodon.Enum;
using Orion.Service.Mastodon.Models.Streaming;
using Orion.UWP.Models.Absorb;
using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models.Clients
{
    internal class MastodonClientWrapper : BaseClientWrapper
    {
        private readonly MastodonClient _mastodonClient;

        public MastodonClientWrapper(Provider provider) : base(provider)
        {
            _mastodonClient = new MastodonClient(provider.Host);
        }

        public MastodonClientWrapper(Account account) : base(account)
        {
            _mastodonClient = new MastodonClient(Provider.Host)
            {
                ClientId = account.Provider.ClientId,
                ClientSecret = account.Provider.ClientSecret,
                AccessToken = account.Credential.AccessToken
            };
        }

        public override async Task<string> GetAuthorizeUrlAsync()
        {
            const Scope scopes = Scope.Read | Scope.Write | Scope.Follow;
            await _mastodonClient.Apps.RegisterAsync("Orion", "urn:ietf:wg:oauth:2.0:oob", scopes, "https://ori.kokoiroworks.com");
            return _mastodonClient.OAuth.GetAuthorizeUrl(scopes);
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _mastodonClient.OAuth.TokenAsync(verifier);
                Account.Provider.ClientId = _mastodonClient.ClientId;
                Account.Credential.AccessToken = _mastodonClient.AccessToken;
                Account.Credential.AccessTokenSecret = _mastodonClient.AccessTokenSecret;

                User = new User(await _mastodonClient.Account.VerifyCredentialsAsync());
                Account.Credential.Username = User.ScreenName;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override async Task<bool> RefreshAccountAsync()
        {
            try
            {
                User = new User(await _mastodonClient.Account.VerifyCredentialsAsync());
                return true;
            }
            catch
            {
                // Revoke access permission or invalid credentials.
                return false;
            }
        }

        public override IObservable<Status> GetTimelineAsObservable(TimelineType type)
        {
            // F**king mstdn.jp
            var host = ProviderRedirect.Redirect(_mastodonClient.BaseUrl);
            switch (type)
            {
                case TimelineType.HomeTimeline:
                    return Merge(async () => (await _mastodonClient.Timelines.HomeAsync()).Select(w => new Status(w)),
                                 () => _mastodonClient.Streaming.UserAsObservable(host).OfType<StatusMessage>().Select(w => new Status(w.Status)));

                case TimelineType.Mentions:
                case TimelineType.DirectMessages:
                    throw new NotSupportedException();

                case TimelineType.Notifications:
                    break;

                case TimelineType.PublicTimeline:
                    return FkStreamClient.AsObservable(async (Status w) =>
                                                           (await _mastodonClient.Timelines.PublicAsync(true, sinceId: (int?) w?.Id)).Select(v => new Status(v)));

                case TimelineType.FederatedTimeline:
                    return Merge(async () => (await _mastodonClient.Timelines.PublicAsync()).Select(w => new Status(w)),
                                 () => _mastodonClient.Streaming.PublicAsObservable(host).OfType<StatusMessage>().Select(w => new Status(w.Status)));

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            throw new NotSupportedException();
        }
    }
}