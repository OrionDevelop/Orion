using System;

namespace Orion.Service.Mastodon.Enum
{
    [Flags]
    public enum Scope
    {
        Read = 0x01,

        Write = 0x02,

        Follow = 0x04
    }

    public static class ScopeEx
    {
        public static string[] ToStrings(this Scope scopes)
        {
            return scopes.ToString().ToLower().Replace(" ", "").Split(',');
        }
    }
}