using System.Collections.Generic;
using System.Threading.Tasks;

using Orion.Service.GnuSocial.Models;
using Orion.Service.Shared;
using Orion.Service.Shared.Extensions;
using Orion.Service.Shared.Helpers;

namespace Orion.Service.GnuSocial.Clients
{
    public class OAuthClient : ApiClient<GnuSocialClient>
    {
        internal OAuthClient(GnuSocialClient gnuSocialClient) : base(gnuSocialClient) { }

        public async Task<RequestToken> RequestTokenAsync(string callback)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("oauth_callback", callback)
            };

            var response = await AppClient.PostRawAsync("oauth/request_token", parameters).Stay();
            var queryDict = UrlHelper.ParseUrlQuery(response);
            var requestToken = new RequestToken
            {
                OAuthToken = queryDict["oauth_token"],
                OAuthTokenSecret = queryDict["oauth_token_secret"],
                OAuthCallbackConfirmed = bool.Parse(queryDict["oauth_callback_confirmed"])
            };
            AppClient.AccessToken = requestToken.OAuthToken;
            AppClient.AccessTokenSecret = requestToken.OAuthTokenSecret;

            return requestToken;
        }

        public string GetAuthorizeUrl(RequestToken token = null)
        {
            return token != null
                ? $"{AppClient.BaseUrl}oauth/authorize?oauth_token={token.OAuthToken}&oauth_token_secret={token.OAuthTokenSecret}"
                : $"{AppClient.BaseUrl}oauth/authorize?oauth_token={AppClient.AccessToken}&oauth_token_secret={AppClient.AccessTokenSecret}";
        }

        public async Task<AccessToken> AccessTokenAsync(string verifier)
        {
            var parameters = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("oauth_verifier", verifier)
            };

            var response = await AppClient.PostRawAsync("oauth/access_token", parameters).Stay();
            var queryDict = UrlHelper.ParseUrlQuery(response);
            var accessToken = new AccessToken
            {
                OAuthToken = queryDict["oauth_token"],
                OAuthTokenSecret = queryDict["oauth_token_secret"]
            };
            AppClient.AccessToken = accessToken.OAuthToken;
            AppClient.AccessTokenSecret = accessToken.OAuthTokenSecret;

            return accessToken;
        }
    }
}