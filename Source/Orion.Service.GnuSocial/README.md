GNU social API wrapper for .NET standard 1.4.
----

```csharp
// @ Orion.Service.GnuSocial.linq
// GNU social API wrapper for .NET standard 1.4.
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
    client.AccessToken = "your access token";
    client.AccessTokenSecret = "your access token secret";
}

(await client.Account.VerifyCredentialsAsync()).Dump();
(await client.Statuses.PublicNetworkTimelineAsync()).Dump();
await client.Statuses.UpdateAsync("test notice.");
```