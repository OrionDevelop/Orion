using System.Collections.Generic;

namespace Orion.UWP.Models
{
    public class ParsableText
    {
        /// <summary>
        ///     Status body
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Hyperlinks
        /// </summary>
        public Dictionary<string, string> Hyperlinks { get; set; }
    }
}