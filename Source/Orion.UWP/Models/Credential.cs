namespace Orion.UWP.Models
{
    public class Credential
    {
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