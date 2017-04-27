using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models
{
    internal class Account
    {
        /// <summary>
        ///     Unique ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Service provider
        /// </summary>
        public Service Service { get; set; }

        /// <summary>
        ///     Hosted domain
        /// </summary>
        public string Domain { get; set; }
    }
}