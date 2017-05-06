using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using CoreTweet;
using CoreTweet.Streaming;

using Orion.Service.FkStreaming;
using Orion.UWP.Models.Absorb;
using Orion.UWP.Models.Enum;

using Status = Orion.UWP.Models.Absorb.Status;
using User = Orion.UWP.Models.Absorb.User;

namespace Orion.UWP.Models.Clients
{
    internal class TwitterClientWrapper : BaseClientWrapper
    {
        private OAuth.OAuthSession _session;
        private Tokens _twitterClient;

        public TwitterClientWrapper(Provider provider) : base(provider) { }

        public TwitterClientWrapper(Account account) : base(account)
        {
            _twitterClient = Tokens.Create(Provider.ClientId, Provider.ClientSecret, account.Credential.AccessToken, account.Credential.AccessTokenSecret);
        }

        public override async Task<string> GetAuthorizeUrlAsync()
        {
            _session = await OAuth.AuthorizeAsync(Provider.ClientId, Provider.ClientSecret, Constants.OAuthCallback);
            return _session.AuthorizeUri.ToString();
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                _twitterClient = await _session.GetTokensAsync(verifier);
                Account.Credential.AccessToken = _twitterClient.AccessToken;
                Account.Credential.AccessTokenSecret = _twitterClient.AccessTokenSecret;
                User = new User(await _twitterClient.Account.VerifyCredentialsAsync());
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
                User = new User(await _twitterClient.Account.VerifyCredentialsAsync());
                Account.Credential.Username = User.ScreenName;
                return true;
            }
            catch
            {
                // Revoke access permission or invalid credentials.
                return false;
            }
        }

        public override IObservable<StatusBase> GetTimelineAsObservable(TimelineType type)
        {
            switch (type)
            {
                case TimelineType.HomeTimeline:
                    return Merge(async () => (await _twitterClient.Statuses.HomeTimelineAsync()).Select(w => new Status(w)),
                                 () => _twitterClient.Streaming.UserAsObservable().OfType<StatusMessage>().Select(w => new Status(w.Status)));

                case TimelineType.Mentions:
                    return FkStreamClient.AsObservable(async (Status w) =>
                                                           (await _twitterClient.Statuses.MentionsTimelineAsync(since_id: w?.Id)).Select(v => new Status(v)),
                                                       TimeSpan.FromSeconds(15));

                case TimelineType.DirectMessages:
                    throw new NotImplementedException();

                case TimelineType.Notifications:
                case TimelineType.PublicTimeline:
                case TimelineType.FederatedTimeline:
                    throw new NotSupportedException();

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}