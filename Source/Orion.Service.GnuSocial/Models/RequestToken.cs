namespace Orion.Service.GnuSocial.Models
{
    public class RequestToken
    {
        public string OAuthToken { get; set; }

        public string OAuthTokenSecret { get; set; }

        public bool OAuthCallbackConfirmed { get; set; }
    }
}