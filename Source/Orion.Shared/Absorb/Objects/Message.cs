namespace Orion.Shared.Absorb.Objects
{
    /// <summary>
    ///     Direct messagess
    /// </summary>
    public class Message : StatusBase
    {
        /// <summary>
        ///     Body
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     Sender
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        ///     Recipient
        /// </summary>
        public User Recipient { get; set; }
    }
}