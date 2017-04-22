namespace Orion.Service.Mastodon.Clients
{
    public class ApiClient
    {
        protected MastodonClient MastodonClient { get; }

        protected ApiClient(MastodonClient mastodonClent)
        {
            MastodonClient = mastodonClent;
        }
    }
}