using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

using Orion.Service.Mastodon.Models.Streaming;

namespace Orion.Service.Mastodon.Streaming
{
    internal class StreamingConnection : IDisposable
    {
        private readonly string _endpoint;
        private readonly MastodonClient _mastodonClient;
        private IObserver<MessageBase> _observer;
        private CancellationTokenSource _tokenSource;

        public StreamingConnection(MastodonClient mastodonClient, string endpoint, IObserver<MessageBase> observer)
        {
            _mastodonClient = mastodonClient;
            _endpoint = endpoint;
            _observer = observer;
            _tokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _tokenSource.Dispose();
            _tokenSource = null;
            _observer.OnCompleted();
            _observer = null;
        }

        public void Connect()
        {
            Task.Run(async () =>
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "Orion.Service.Shared Library/1.0");
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _mastodonClient.AccessToken);
                        var stream = await httpClient.GetStreamAsync(_endpoint);
                        using (var sr = new StreamReader(stream))
                        {
                            while (!sr.EndOfStream)
                            {
                                var e = sr.ReadLine();
                                if (e == ":thump" || string.IsNullOrWhiteSpace(e))
                                    continue;

                                var p = sr.ReadLine();
                                _observer.OnNext(MessageBase.CreateMessage(e, p));
                            }
                        }
                    }
                    _observer.OnCompleted();
                }
                catch (Exception e)
                {
                    _observer.OnError(e);
                }
            });
        }
    }
}