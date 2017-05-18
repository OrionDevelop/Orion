using Orion.Service.Mastodon;

namespace Orion.Shared.Absorb.DataSources
{
    internal class MastodonDataSource : BaseDataSource
    {
        private readonly MastodonClient _mastodonClient;

        public MastodonDataSource(MastodonClient mastodonClient)
        {
            _mastodonClient = mastodonClient;
        }

        protected override void UpdateConnection() { }
    }
}