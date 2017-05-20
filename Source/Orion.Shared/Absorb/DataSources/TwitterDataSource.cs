using System;

using Orion.Shared.Absorb.Clients;

namespace Orion.Shared.Absorb.DataSources
{
    internal class TwitterDataSource : BaseDataSource
    {
        private readonly TwitterClientWrapper _twitterClient;

        public TwitterDataSource(TwitterClientWrapper twitterClient)
        {
            _twitterClient = twitterClient;
        }

        protected override void Connect(Source source)
        {
            throw new NotImplementedException();
        }

        protected override string NormalizedSource(string source)
        {
            throw new NotImplementedException();
        }
    }
}