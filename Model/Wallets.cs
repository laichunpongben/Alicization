using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Markets;
using Alicization.Model.Wealth;

namespace Alicization.Model
{
    public class Wallets //obsolete
    {
        //private static Wallets instance = null;

        //internal ConcurrentDictionary<IEntity, Wallet> dict { get; private set; }

        //private Wallets()
        //{
        //    ; //singleton
        //}

        //internal static Wallets GetInstance()
        //{
        //    return (instance == null) ? instance = new Wallets() : instance; 
        //}

        //internal void Initialize()
        //{
        //    dict = new ConcurrentDictionary<IEntity, Wallet>();
        //}

        //public int Count()
        //{
        //    return dict.Count;
        //}

        //internal void Add(IEntity entity, Wallet wallet)
        //{
        //    dict.TryAdd(entity, wallet);
        //}

        //internal void RemoveEntity(IEntity entity)
        //{
        //    dict.Remove(entity);
        //}

        //internal Wallet GetWallet(IEntity entity)
        //{
        //    return dict[entity];
        //}

        //internal void UpdateWallet(IEntity entity, ITransaction transaction)
        //{
        //    dict[entity].Add(transaction, entity);
        //}

        //internal void UpdateEntityPairWallets(ITransaction transaction)
        //{
        //    UpdateWallet(transaction.entityDouble.entityFrom, transaction);
        //    UpdateWallet(transaction.entityDouble.entityTo, transaction);
        //}

        //internal bool IsSufficientCash(IEntity entity, double amount)
        //{
        //    return (amount <= dict[entity].currentBalance);
        //}
    }
}
