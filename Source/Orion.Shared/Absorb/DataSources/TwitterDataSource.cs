using System;
using System.Reactive.Linq;

using CoreTweet;
using CoreTweet.Streaming;

using Orion.Shared.Absorb.Objects;

using Status = Orion.Shared.Absorb.Objects.Status;

namespace Orion.Shared.Absorb.DataSources
{
    internal class TwitterDataSource : BaseDataSource
    {
        private readonly Tokens _twitterClient;

        public TwitterDataSource(Tokens twitterClient)
        {
            _twitterClient = twitterClient;
        }

        private StatusBase Convert(StreamingMessage message)
        {
            if (message is StatusMessage statusMessage)
                return new Status(statusMessage.Status);
            return null;
        }

        protected override void Connect(Source source)
        {
            if (!source.IsAdded)
                return;

            if (IsConnected(source.Name))
                return;

            Disposables.Add(source.Name, _twitterClient.Streaming.UserAsObservable()
                                                       .Do(_ => Heartbeat(source.Name))
                                                       .Select(Convert)
                                                       .Where(w => w != null)
                                                       .Subscribe(w => AddStatus(source.Name, w), w => OnError(source.Name, w)));
        }

        protected override string NormalizedSource(string source)
        {
            return "home";
        }
    }
}