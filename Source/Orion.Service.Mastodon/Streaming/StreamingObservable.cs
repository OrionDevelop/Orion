using System;

using Orion.Service.Mastodon.Models.Streaming;

namespace Orion.Service.Mastodon.Streaming
{
    internal class StreamingObservable : IObservable<MessageBase>
    {
        private readonly string _endpoint;
        private readonly MastodonClient _mastodonClient;

        public StreamingObservable(MastodonClient mastodonClient, string endpoint)
        {
            _mastodonClient = mastodonClient;
            _endpoint = endpoint;
        }

        public IDisposable Subscribe(IObserver<MessageBase> observer)
        {
            var connection = new StreamingConnection(_mastodonClient, _endpoint, observer);
            connection.Connect();
            return connection;
        }
    }
}