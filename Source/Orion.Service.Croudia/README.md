Croudia API wrapper for .NET standard 1.4.
----

```csharp
// @ Orion.Service.Croudia.linq
// Mastodon API wrapper for .NET standard 1.4.
var flag = false;
var client = new CroudiaClient("your client id", "your client secret");

// Authorization process.
if (flag)
{
    Process.Start(client.OAuth.GetAuthorizeUrl());
    (await client.OAuth.AccessTokenAsync(Console.ReadLine())).Dump();
}
else
{
    client.AccessToken = "last access token";
    client.RefreshToken = "last refresh token";

    try
    {
        (await client.Account.VerifyCredentialsAsync()).Dump();
    }
    catch
    {
        (await client.OAuth.RefreshTokenAsync()).Dump();
        (await client.Account.VerifyCredentialsAsync()).Dump();
    }
}

(await client.Statuses.PublicTimelineAsync()).Dump();
await client.Statuses.UpdateAsync("test whisper");
```
