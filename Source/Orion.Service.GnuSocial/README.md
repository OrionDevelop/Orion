GNU social API wrapper for .NET standard 1.4.
----

```csharp
// Please register application to host service (Example: https://freezepeach.xyz).
var client = new GnuSocialClient("freezepeach.xyz", "YOUR_CONSUMER_KEY", "YOUR_CONSUMER_SECRET");

// Supports HTTP Basic authentication.
// var client = new GnuSocialClient("freezepeach.xyz", "YOUR_USERNAME", "YOUR_PASSWORD");

// Authenticate process.
await client.OAuth.RequestTokenAsync("oob");
client.OAuth.Authorize(); // Open browser and access to authorize url.
await client.OAuth.AccessTokenAsync(verifier); // `verifier` is PIN code.

// Notice!
var status = await client.Statuses.UpdateAsync("テスト");
```