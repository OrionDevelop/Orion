namespace Orion.Service.Shared
{
    public class ApiClient<T>
    {
        protected T AppClient { get; }

        protected ApiClient(T client)
        {
            AppClient = client;
        }
    }
}