using System.Threading.Tasks;

using Orion.Shared.Models;

namespace Orion.Shared.Absorb.Clients
{
    public abstract class BaseClientWrapper
    {
        protected readonly Credential Credential;
        protected readonly Provider Provider;

        protected BaseClientWrapper(Provider provider, Credential credential)
        {
            Provider = provider;
            Credential = credential;
        }

        /// <summary>
        ///     認証用 URL を取得します。
        /// </summary>
        /// <returns></returns>
        public abstract Task<string> GetAuthorizedUrlAsync();

        /// <summary>
        ///     アカウントを認証します。
        /// </summary>
        /// <param name="verifier"></param>
        /// <returns></returns>
        public abstract Task<bool> AuthorizeAsync(string verifier);

        /// <summary>
        ///     認証情報を更新します。
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> RefreshAccountAsync();

        /// <summary>
        ///     ステータスを送信します。
        /// </summary>
        /// <param name="body"></param>
        /// <param name="inReplyToStatusId"></param>
        /// <returns></returns>
        public abstract Task<bool> UpdateAsync(string body, long? inReplyToStatusId = null);
    }
}