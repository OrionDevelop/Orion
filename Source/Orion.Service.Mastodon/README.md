Mastodon API wrapper for .NET standard 1.4.
----

```csharp
// @ Orion.Service.Mastodon.linq
// Mastodon API wrapper for .NET standard 1.4.
var flag = false;
var client = new MastodonClient("mstdn.jp");

// Authorization process.
if (flag)
{
    var scopes = Scope.Read | Scope.Write | Scope.Follow;
    (await client.Apps.RegisterAsync("Orion", "urn:ietf:wg:oauth:2.0:oob", scopes)).Dump();
    Process.Start(client.OAuth.GetAuthorizeUrl(scopes));
    (await client.OAuth.TokenAsync(Console.ReadLine())).Dump();
}
else
{
    client.ClientId = "your client id";
    client.ClientSecret = "your client secret";
    client.AccessToken = "your access token";
}


(await client.Account.VerifyCredentialsAsync()).Dump();
(await client.Timelines.PublicAsync()).Dump();
await client.Statuses.CreateAsync("test toot.");

```

### Streaming Support

```csharp
client.Streaming.UserAsObervable().OfType<StatusMessage>().Subscribe(w => {
    Console.WriteLine("---------------------------------------");
    Console.WriteLine($"{w.Status.Account.Username} @{w.Status.Account.Acct}");
    Console.WriteLine(w.Status.Content);
    Console.WriteLine();
});

// When streaming API is hosted on other domain. (e.g. mstdn.jp)
client.Streaming.UserAsObervable("streaming.mstdn.jp").OfType<StatusMessage>().Subscribe(w => {
    Console.WriteLine("---------------------------------------");
    Console.WriteLine($"{w.Status.Account.Username} @{w.Status.Account.Acct}");
    Console.WriteLine(w.Status.Content);
    Console.WriteLine();
});
```