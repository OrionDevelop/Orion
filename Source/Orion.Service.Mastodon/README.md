Mastodon API wrapper for .NET standard 1.4.
----

```csharp
var client = new MastodonClient("mstdn.jp");
var scopes = new [] { "read", "write", "follow" };

// Register an app.
await client.Apps.RegisterAsync("Test App", "urn:ietf:wg:oauth:2.0:oob", scopes);

// Authorization
// Note: If you set 'scopes' to 'write'/'follow', pass scopes to 3rd argument.
await client.OAuth.TokenAsync("YOUR_MAILADDRESS", "YOUR_PASSWORD", scopes);

// Toot!
var status = await client.Statuses.CreateAsync("テスト");
```
