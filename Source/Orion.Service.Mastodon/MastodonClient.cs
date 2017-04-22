using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Orion.Service.Mastodon.Clients;

// ReSharper disable PossibleMultipleEnumeration

namespace Orion.Service.Mastodon
{
    /// <summary>
    ///     Mastodon API Wrapper
    ///     https://github.com/tootsuite/mastodon
    /// </summary>
    public class MastodonClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessTokn { get; set; }

        public AccountClient Account { get; }
        public AppsClient Apps { get; }
        public BlocksClient Blocks { get; }
        public FavouritesClient Favourites { get; }
        public FollowRequestsClient FollowRequests { get; }
        public FollowsClient Follows { get; }
        public InstanceClient Instance { get; }
        public MediaClient Media { get; }
        public MutesClient Mutes { get; }
        public NotificationsClient Notifications { get; }
        public OAuthClient OAuth { get; set; }
        public ReportsClient Reports { get; }
        public SearchClient Search { get; }
        public StatusesClient Statuses { get; }
        public TimelinesClient Timelines { get; }

        public MastodonClient(string domain)
        {
            _baseUrl = $"https://{domain}/";
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Orion.Service.Mastodon/1.0");
            Account = new AccountClient(this);
            Apps = new AppsClient(this);
            Blocks = new BlocksClient(this);
            Favourites = new FavouritesClient(this);
            FollowRequests = new FollowRequestsClient(this);
            Follows = new FollowsClient(this);
            Instance = new InstanceClient(this);
            Media = new MediaClient(this);
            Mutes = new MutesClient(this);
            Notifications = new NotificationsClient(this);
            OAuth = new OAuthClient(this);
            Reports = new ReportsClient(this);
            Search = new SearchClient(this);
            Statuses = new StatusesClient(this);
            Timelines = new TimelinesClient(this);
        }

        /// <summary>
        ///     HTTP GET
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal async Task<T> GetAsync<T>(string endpoint, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            if (!string.IsNullOrWhiteSpace(AccessTokn))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessTokn);
            if (parameters != null && parameters.Any())
                endpoint += "?" + string.Join("&", parameters.Select(w => $"{w.Key}={Uri.EscapeDataString(w.Value.ToString())}"));
            var response = await _httpClient.GetAsync(_baseUrl + endpoint);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        ///     HTTP PATCH
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal async Task<T> PatchAsync<T>(string endpoint, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            if (!string.IsNullOrWhiteSpace(AccessTokn))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessTokn);
            var content = new FormUrlEncodedContent(parameters?.Select(w => new KeyValuePair<string, string>(w.Key, w.Value.ToString())));
            var httpMethod = new HttpMethod("PATCH");
            var response = await _httpClient.SendAsync(new HttpRequestMessage(httpMethod, _baseUrl + endpoint) {Content = content});
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        ///     HTTP DELETE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        internal async Task<T> DeleteAsync<T>(string endpoint)
        {
            if (!string.IsNullOrWhiteSpace(AccessTokn))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessTokn);
            var response = await _httpClient.DeleteAsync(_baseUrl + endpoint);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        ///     HTTP POST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        internal async Task<T> PostAsync<T>(string endpoint, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            if (!string.IsNullOrWhiteSpace(AccessTokn))
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessTokn);
            HttpContent content;
            if (parameters != null && parameters.Any(w => w.Key == "file"))
            {
                content = new MultipartFormDataContent();
                foreach (var kvp in parameters)
                {
                    HttpContent formDataContent;
                    if (kvp.Key == "file")
                        formDataContent = kvp.Value as StreamContent;
                    else
                        formDataContent = new StringContent(kvp.Value.ToString());
                    ((MultipartFormDataContent) content).Add(formDataContent, kvp.Key);
                }
            }
            else
            {
                content = new FormUrlEncodedContent(parameters?.Select(w => new KeyValuePair<string, string>(w.Key, w.Value.ToString())));
            }
            var response = await _httpClient.PostAsync(_baseUrl + endpoint, content);
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}