using System;
using System.Threading.Tasks;

using Orion.Service.GnuSocial;
using Orion.UWP.Models.Absorb;
using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models.Clients
{
    internal class GnuSocialClientWrapper : BaseClientWrapper
    {
        private readonly GnuSocialClient _gnuSocialClient;

        public GnuSocialClientWrapper(Provider provider) : base(provider)
        {
            _gnuSocialClient = new GnuSocialClient(provider.Host, provider.ClientId, provider.ClientSecret);
        }

        public GnuSocialClientWrapper(Account account) : base(account)
        {
            _gnuSocialClient = new GnuSocialClient(Provider.Host, Provider.ClientId, Provider.ClientSecret)
            {
                AccessToken = account.Credential.AccessToken,
                AccessTokenSecret = account.Credential.AccessTokenSecret
            };
        }

        public override async Task<string> GetAuthorizeUrlAsync()
        {
            await _gnuSocialClient.OAuth.RequestTokenAsync(Constants.OAuthCallback);
            return _gnuSocialClient.OAuth.GetAuthorizeUrl();
        }

        public override async Task<bool> AuthorizeAsync(string verifier)
        {
            try
            {
                await _gnuSocialClient.OAuth.AccessTokenAsync(verifier);
                Account.Credential.AccessToken = _gnuSocialClient.AccessToken;
                Account.Credential.AccessTokenSecret = _gnuSocialClient.AccessTokenSecret;

                User = new User(await _gnuSocialClient.Account.VerifyCredentialsAsync());
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
                User = new User(await _gnuSocialClient.Account.VerifyCredentialsAsync());
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
                    break;

                case TimelineType.Mentions:
                    break;

                case TimelineType.DirectMessages:
                    break;

                case TimelineType.Notifications:
                    break;

                case TimelineType.PublicTimeline:
                    break;

                case TimelineType.FederatedTimeline:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            throw new NotImplementedException();
        }
    }
}