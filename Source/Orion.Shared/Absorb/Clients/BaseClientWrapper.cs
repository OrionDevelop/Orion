using System;
using System.Threading.Tasks;

using Orion.Shared.Absorb.DataSources;
using Orion.Shared.Absorb.Objects;
using Orion.Shared.Models;

namespace Orion.Shared.Absorb.Clients
{
    public abstract class BaseClientWrapper
    {
        protected readonly Credential Credential;
        protected readonly Provider Provider;
        protected BaseDataSource DataSource { get; set; }

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
        public abstract Task UpdateAsync(string body, long? inReplyToStatusId = null);

        /// <summary>
        ///     ステータスを削除します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task DestroyAsync(long id);

        /// <summary>
        ///     ステータスをお気に入り登録します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task FavoriteAsync(long id);

        /// <summary>
        ///     ステータスのお気に入り登録を解除します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task UnfavoriteAsync(long id);

        /// <summary>
        ///     ステータスをリブログします。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task ReblogAsync(long id);

        /// <summary>
        ///     ステータスのリブログを解除します。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Task UnreblogAsync(long id);

        /// <summary>
        ///     ストリーミング接続を開始します。
        /// </summary>
        /// <returns></returns>
        public IObservable<StatusBase> CreateOrGetConnection(string sourceStr)
        {
            return DataSource.Connect(sourceStr);
        }

        /// <summary>
        ///     接続を切断します。
        /// </summary>
        /// <param name="sourceStr"></param>
        public void Disconnect(string sourceStr)
        {
            DataSource.Disconnect(sourceStr);
        }
    }
}