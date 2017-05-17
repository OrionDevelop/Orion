using Newtonsoft.Json;

using Orion.Shared.Absorb.Objects;

namespace Orion.Shared.Models
{
    public class Credential
    {
        /// <summary>
        ///     User ID per services.
        /// </summary>
        public long UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        /// <summary>
        ///     Access token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        ///     Access token secret
        /// </summary>
        public string AccessTokenSecret { get; set; }

        /// <summary>
        ///     Refresh token
        /// </summary>
        public string RefreshToken { get; set; }
    }
}