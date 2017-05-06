using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Orion.UWP.Models.Absorb;
using Orion.UWP.Models.Enum;

namespace Orion.UWP.Models.Clients
{
    public abstract class BaseClientWrapper
    {
        protected Provider Provider { get; }
        public Account Account { get; }

        /// <summary>
        ///     Authenticated user.
        /// </summary>
        public User User { get; protected set; }

        protected BaseClientWrapper(Provider provider)
        {
            Provider = provider;
            Account = new Account {Provider = provider, Credential = new Credential(), ClientWrapper = this};
        }

        protected BaseClientWrapper(Account account)
        {
            Provider = account.Provider;
            Account = account;
        }

        /// <summary>
        ///     認証用 URL を取得します。
        /// </summary>
        /// <returns></returns>
        public abstract Task<string> GetAuthorizeUrlAsync();

        /// <summary>
        ///     アカウントを認証します。
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> AuthorizeAsync(string verifier);

        /// <summary>
        ///     アカウント情報を更新します。
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> RefreshAccountAsync();

        /// <summary>
        ///     `TimelineType` に応じたタイムラインをストリーミングとして取得します。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract IObservable<StatusBase> GetTimelineAsObservable(TimelineType type);

        protected IObservable<StatusBase> Merge(Func<Task<IEnumerable<StatusBase>>> firstAction, Func<IObservable<StatusBase>> streamAction)
        {
            return Task.Run(async () => streamAction.Invoke().StartWith((await firstAction.Invoke()).Reverse())).Result;
        }
    }
}