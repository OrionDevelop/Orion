namespace Orion.UWP.Models
{
    public class Account
    {
        /// <summary>
        ///     Unique ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Service provider
        /// </summary>
        public Provider Provider { get; set; }

        /// <summary>
        ///     Credentials
        /// </summary>
        public Credential Credential { get; set; }
    }
}