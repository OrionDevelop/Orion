using System;
using System.Reactive.Linq;

using Orion.Service.Mastodon;
using Orion.Service.Mastodon.Models.Streaming;
using Orion.Shared.Absorb.Objects;
using Orion.Shared.Absorb.Objects.Events;

namespace Orion.Shared.Absorb.DataSources
{
    internal class MastodonDataSource : BaseDataSource
    {
        private readonly MastodonClient _mastodonClient;

        public MastodonDataSource(MastodonClient mastodonClient)
        {
            _mastodonClient = mastodonClient;
        }

        private StatusBase Convert(MessageBase message)
        {
            if (message is StatusMessage statusMessage)
                return new Status(statusMessage.Status);
            return EventBase.CreateEventFromMessage(message);
        }

        protected override void ConnectImpl(string sourceStr)
        {
            if (IsConnected(sourceStr))
                return;

            var host = ProviderRedirect.Redirect(_mastodonClient.BaseUrl);
            IObservable<MessageBase> connection = null;

            switch (sourceStr)
            {
                case "federated":
                    connection = _mastodonClient.Streaming.PublicAsObservable(host);
                    break;

                case "public":
                    connection = _mastodonClient.Streaming.LocalAsObservable(host);
                    break;

                case "home":
                    connection = _mastodonClient.Streaming.UserAsObservable(host);
                    break;
            }

            Disposables.Add(sourceStr,
                            connection.Do(_ => Heartbeat(sourceStr))
                                      .Select(Convert)
                                      .Where(w => w != null)
                                      .Subscribe(w => AddStatus(sourceStr, w), w => OnError(sourceStr, w)));
        }

        protected override string NormalizedSource(string source)
        {
            switch (source)
            {
                case "*":
                case "federated":
                    return "federated";

                case "public":
                    return "public";

                case "mentions":
                case "mention":
                case "notifications":
                case "notification":
                case "home":
                    return "home";

                default:
                    throw new ArgumentOutOfRangeException(nameof(source), source, null);
            }
        }
    }
}