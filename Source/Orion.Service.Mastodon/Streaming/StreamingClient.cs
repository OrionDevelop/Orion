using System;

using Orion.Service.Mastodon.Models.Streaming;

namespace Orion.Service.Mastodon.Streaming
{
    public class StreamingClient
    {
        private readonly MastodonClient _mastodonClient;

        public StreamingClient(MastodonClient mastodonClient)
        {
            _mastodonClient = mastodonClient;
        }

        public IObservable<MessageBase> UserAsObservable(string host = null)
        {
            host = string.IsNullOrWhiteSpace(host) ? _mastodonClient.BaseUrl : $"https://{host}/";
            var observable = new StreamingObservable(_mastodonClient, $"{host}api/v1/streaming/user");
            return observable;
        }

        public IObservable<MessageBase> PublicAsObservable(string host = null)
        {
            host = string.IsNullOrWhiteSpace(host) ? _mastodonClient.BaseUrl : $"https://{host}/";
            var observable = new StreamingObservable(_mastodonClient, $"{host}api/v1/streaming/public");
            return observable;
        }

        public IObservable<MessageBase> LocalAsObservable(string host = null)
        {
            host = string.IsNullOrWhiteSpace(host) ? _mastodonClient.BaseUrl : $"https://{host}/";
            var observable = new StreamingObservable(_mastodonClient, $"{host}api/v1/streaming/public/local");
            return observable;
        }

        public IObservable<MessageBase> TagAsObservable(string tag, string host = null)
        {
            host = string.IsNullOrWhiteSpace(host) ? _mastodonClient.BaseUrl : $"https://{host}/";
            var observable = new StreamingObservable(_mastodonClient, $"{host}api/v1/streaming/hashtag?tag={_mastodonClient.UrlEncode(tag)}");
            return observable;
        }

        public IObservable<MessageBase> TagLocalAsObservable(string tag, string host = null)
        {
            host = string.IsNullOrWhiteSpace(host) ? _mastodonClient.BaseUrl : $"https://{host}/";
            var observable = new StreamingObservable(_mastodonClient, $"{host}api/v1/streaming/hashtag/local?tag={_mastodonClient.UrlEncode(tag)}");
            return observable;
        }
    }
}