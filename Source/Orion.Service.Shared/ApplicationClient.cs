using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Orion.Service.Shared.Exceptions;
using Orion.Service.Shared.Extensions;

using HttpParameters = System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, object>>;

// ReSharper disable UnusedParameter.Local
// ReSharper disable PossibleMultipleEnumeration

namespace Orion.Service.Shared
{
    public class ApplicationClient
    {
        private readonly AuthenticateType _authenticateType;
        private readonly HttpClient _httpClient;
        public string BaseUrl { get; }

        /// <summary>
        ///     Key names that send as binary data.
        /// </summary>
        protected List<string> BinaryParameters { get; set; } = new List<string>();

        /// <summary>
        ///     Constructor (use secure authentication as OAuth).
        /// </summary>
        /// <param name="baseUrl">Base URL without protocol (Example: `mastodon.cloud/api/v1`).</param>
        /// <param name="authenticateType">Authenticate Type (OAuth version)</param>
        protected ApplicationClient(string baseUrl, AuthenticateType authenticateType)
        {
            if (authenticateType == AuthenticateType.HttpBasic)
                throw new NotSupportedException("Please use .ctor(string, string, string).");

            BaseUrl = $"https://{baseUrl}/";
            _authenticateType = authenticateType;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Orion.Service.Shared Library/1.0");
        }

        /// <summary>
        ///     Constructor (use HTTP Basic authentication).
        /// </summary>
        /// <param name="baseUrl">Base URL without protocol (Example: `mastodon.cloud/api/v1`).</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        protected ApplicationClient(string baseUrl, string username, string password)
        {
            BaseUrl = $"https://{baseUrl}/";
            _authenticateType = AuthenticateType.HttpBasic;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Orion.Service.Shared Library/1.0");
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
        }

        /// <summary>
        ///     Send HTTP GET request.
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="endpoint">API endpoint without base url.</param>
        /// <param name="parameters">API KVP parameters.</param>
        /// <param name="requireAuth">Require authenticate.</param>
        /// <returns>API response deserialize to T.</returns>
        public async Task<T> GetAsync<T>(string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            return JsonConvert.DeserializeObject<T>(await GetRawAsync(endpoint, parameters, requireAuth).Stay());
        }

        public async Task<string> GetRawAsync(string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            PrepareForAuthenticate("GET", BaseUrl + endpoint, parameters, requireAuth);
            if (parameters != null && parameters.Any())
                endpoint += $"?{string.Join("&", parameters.Select(w => $"{w.Key}={UrlEncode(w.Value.ToString())}"))}";

            var response = await _httpClient.GetAsync(BaseUrl + endpoint).Stay();
            HandleErrors(response);
            return await response.Content.ReadAsStringAsync().Stay();
        }

        /// <summary>
        ///     Send HTTP POST request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="endpoint">API endpoint without base url.</param>
        /// <param name="parameters">API KVP parameters.</param>
        /// <param name="requireAuth">Require authenticate.</param>
        /// <returns>API response deserialize to T.</returns>
        public Task<T> PostAsync<T>(string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            return SendAsync<T>(HttpMethod.Post, endpoint, parameters, requireAuth);
        }

        public Task<string> PostRawAsync(string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            return SendRawAsync(HttpMethod.Post, endpoint, parameters, requireAuth);
        }

        /// <summary>
        ///     Send HTTP PUT request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="endpoint">API endpoint without base url.</param>
        /// <param name="parameters">API KVP parameters.</param>
        /// <param name="requireAuth">Require authenticate.</param>
        /// <returns>API response deserialize to T.</returns>
        public Task<T> PutAsync<T>(string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            return SendAsync<T>(HttpMethod.Put, endpoint, parameters, requireAuth);
        }

        /// <summary>
        ///     Send HTTP DELETE request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="endpoint">API endpoint without base url.</param>
        /// <param name="parameters">API KVP parameters.</param>
        /// <param name="requireAuth">Require authenticate.</param>
        /// <returns>API response deserialize to T.</returns>
        public async Task<T> DeleteAsync<T>(string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            PrepareForAuthenticate("DELETE", BaseUrl + endpoint, parameters, requireAuth);
            if (parameters != null && parameters.Any())
                endpoint += $"?{string.Join("&", parameters.Select(w => $"{w.Key}={UrlEncode(w.Value.ToString())}"))}";

            var response = await _httpClient.DeleteAsync(BaseUrl + endpoint).Stay();
            HandleErrors(response);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync().Stay());
        }

        /// <summary>
        ///     Send HTTP PATCH request.
        /// </summary>
        /// <typeparam name="T">Response type.</typeparam>
        /// <param name="endpoint">API endpoint without base url.</param>
        /// <param name="parameters">API KVP parameters.</param>
        /// <param name="requireAuth">Require authenticate.</param>
        /// <returns>API response deserialize to T.</returns>
        public Task<T> PatchAsync<T>(string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            return SendAsync<T>(new HttpMethod("PATCH"), endpoint, parameters, requireAuth);
        }

        private async Task<string> SendRawAsync(HttpMethod method, string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            HttpContent content;
            // Send binary
            if (parameters != null && parameters.Any(w => BinaryParameters.Contains(w.Key)))
            {
                PrepareForAuthenticate(method.Method, BaseUrl + endpoint, new List<KeyValuePair<string, object>>(), requireAuth);
                content = new MultipartFormDataContent();
                foreach (var parameter in parameters)
                {
                    HttpContent formDataContent;
                    if (BinaryParameters.Contains(parameter.Key))
                        if (parameter.Value is StreamContent value)
                        {
                            formDataContent = value;
                        }
                        else
                        {
                            formDataContent = new StreamContent(new FileStream(parameter.Value.ToString(), FileMode.Open));
                            formDataContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                            formDataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                FileName = $"\"{Path.GetFileName(parameter.Value.ToString())}\""
                            };
                        }
                    else
                        formDataContent = new StringContent(UrlEncode(parameter.Value.ToString()));
                    ((MultipartFormDataContent) content).Add(formDataContent, parameter.Key);
                }
            }
            else
            {
                PrepareForAuthenticate(method.Method, BaseUrl + endpoint, parameters, requireAuth);
                var kvp = parameters?.Select(w => new KeyValuePair<string, string>(w.Key, w.Value.ToString()));
                content = new FormUrlEncodedContent(kvp);
            }

            var response = await _httpClient.SendAsync(new HttpRequestMessage(method, BaseUrl + endpoint) {Content = content}).Stay();
            HandleErrors(response);
            return await response.Content.ReadAsStringAsync().Stay();
        }

        private async Task<T> SendAsync<T>(HttpMethod method, string endpoint, HttpParameters parameters = null, bool? requireAuth = null)
        {
            return JsonConvert.DeserializeObject<T>(await SendRawAsync(method, endpoint, parameters, requireAuth).Stay());
        }

        private void PrepareForAuthenticate(string method, string url, HttpParameters parameters, bool? requireAuth)
        {
            if (_authenticateType == AuthenticateType.OAuth2)
                PrepareForOAuth2(requireAuth);
            else if (_authenticateType == AuthenticateType.OAuth10A)
                PrepareForOAuth1A(method, url, parameters, requireAuth);
        }

        private void PrepareForOAuth2(bool? requireAuth)
        {
            if (requireAuth.HasValue && requireAuth.Value && string.IsNullOrWhiteSpace(AccessToken))
                throw new AuthenticateRequiredException();

            if (!string.IsNullOrWhiteSpace(AccessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
        }

        private void PrepareForOAuth1A(string method, string url, HttpParameters parameters, bool? requireAuth)
        {
            if (requireAuth.HasValue && requireAuth.Value && string.IsNullOrWhiteSpace(AccessToken) && string.IsNullOrWhiteSpace(AccessTokenSecret))
                throw new AuthenticateRequiredException();

            var dict = new SortedDictionary<string, string>
            {
                ["oauth_consumer_key"] = ConsumerKey,
                ["oauth_nonce"] = GenerateNonce(),
                ["oauth_signature_method"] = "HMAC-SHA1",
                ["oauth_timestamp"] = GenerateTimestamp(),
                ["oauth_version"] = "1.0"
            };
            if (!string.IsNullOrWhiteSpace(AccessToken))
                dict["oauth_token"] = AccessToken;
            if (parameters != null)
                foreach (var parameter in parameters)
                    dict[parameter.Key] = parameter.Value.ToString();
            var key = $"{UrlEncode(ConsumerSecret)}&{UrlEncode(string.IsNullOrWhiteSpace(AccessTokenSecret) ? "" : AccessTokenSecret)}";
            var message = $"{method}&{UrlEncode(url)}&{UrlEncode(string.Join("&", dict.Select(w => $"{w.Key}={UrlEncode(w.Value)}")))}";

            var hmacsha1 = new HMACSHA1 {Key = Encoding.ASCII.GetBytes(key)};
            var signature = Convert.ToBase64String(hmacsha1.ComputeHash(Encoding.ASCII.GetBytes(message)));
            var header = string.Join(",", dict.Where(w => w.Key.StartsWith("oauth_")).Select(w => $"{w.Key}={UrlEncode(w.Value)}"));

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", $"{header},oauth_signature={UrlEncode(signature)}");
        }

        private void HandleErrors(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            response.EnsureSuccessStatusCode();
        }

        #region OAuth 1.0 implementation

        public string UrlEncode(string str)
        {
            const string reservedLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
            var sb = new StringBuilder();
            foreach (var b in Encoding.UTF8.GetBytes(str))
                if (reservedLetters.Contains((char) b))
                    sb.Append((char) b);
                else
                    sb.Append(string.Format("%{0:X2}", b));
            return sb.ToString();
        }

        private string GenerateTimestamp()
        {
            return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds).ToString();
        }

        private string GenerateNonce()
        {
            const string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var sb = new StringBuilder();
            var random = new Random();
            for (var i = 0; i < 32; i++)
                sb.Append(letters[random.Next(letters.Length)]);
            return sb.ToString();
        }

        #endregion

        #region User Keys

        /// <summary>
        ///     Access Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Access Token Secret
        /// </summary>
        public string AccessTokenSecret { get; set; }

        /// <summary>
        ///     Refresh Token
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public string RefreshToken { get; set; }

        #endregion

        #region Application Keys

        /// <summary>
        ///     Client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        ///     Client Secret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        ///     Consumer Key (alias `ClientId`)
        /// </summary>
        public string ConsumerKey
        {
            get => ClientId;
            set => ClientId = value;
        }

        /// <summary>
        ///     Consumer secret (alias `ClientSecret`)
        /// </summary>
        public string ConsumerSecret
        {
            get => ClientSecret;
            set => ClientSecret = value;
        }

        #endregion
    }
}