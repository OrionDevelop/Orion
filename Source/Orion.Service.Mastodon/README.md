Mastodon API wrapper for .NET standard 1.4.
----

```csharp
var client = new MastodonClient("mstdn.jp");
var scopes = Scope.Read | Scope.Write | Scope.Follow;

// Register an app.
await client.Apps.RegisterAsync("Test App", "urn:ietf:wg:oauth:2.0:oob", scopes);

// Authorization process.
// Note: If you set 'scopes' to 'write'/'follow', pass scopes to 3rd argument.
client.OAuth.Authorize(scopes);
await client.OAuth.TokenAsync(verifier); // `verifier` is PIN code.

// Toot!
var status = await client.Statuses.CreateAsync("テスト");
```
