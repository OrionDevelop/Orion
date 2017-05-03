Orion Fake Streaming
----

Fake Streaming support for non-streaming APIs.


```csharp
// @ Orion.Service.FkStreaming.linq
// Use GNU social federated timeline API.

var flag = false;
var client = new GnuSocialClient("freezepeach.xyz", "your consumer key", "your consumer secret");

// Authorization process.
if (flag)
{
    await client.OAuth.RequestTokenAsync("oob");
    Process.Start(client.OAuth.GetAuthorizeUrl());
    (await client.OAuth.AccessTokenAsync(Console.ReadLine())).Dump();
}
else
{
    client.AccessToken = "access token";
    client.AccessTokenSecret = "access token secret";
}

(await client.Account.VerifyCredentialsAsync()).Dump();

FkStreamClient.TimeSpan = TimeSpan.FromSeconds(10);
var disposable = FkStreamClient.AsObservable((Status w) => client.Statuses.PublicTimelineAsync(sinceId: w?.Id))
                               .Where(w => !w.IsEvent) // GNU social API contains some events. Ignored them.
                               .Subscribe(w =>
                               {
                                   Console.WriteLine("------------------------------------------------------");
                                   Console.WriteLine($"{w.User.Name} @{w.User.ScreenName}");
                                   Console.WriteLine(w.Text);
                                   Console.WriteLine();
                                   Console.WriteLine($"{w.CreatedAt:g} - via {w.Source}");
                                   Console.WriteLine();
                               });

Task.Delay(TimeSpan.FromMinutes(5)).Wait();
disposable.Dispose();
```