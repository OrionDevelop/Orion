namespace Orion.Shared.Absorb.Objects
{
    public class Credential
    {
        /// <summary>
        ///     Username
        /// </summary>
        public string Username { get; set; }

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