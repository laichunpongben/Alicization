using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;
using Alicization.Model.Demographics.Corporations;
using Alicization.Model.Demographics.Players;

namespace Alicization.Model.Markets
{
    public class SocialMarket : IMarket
    {
        private static SocialMarket instance = null;

        internal List<SocialMarketOrder> marketOrders { get; private set; }
        internal List<SocialMarketOrder> marketOrdersMarriageMale { get; private set; }
        internal List<SocialMarketOrder> marketOrdersMarriageFemale { get; private set; }
        internal List<SocialMarketTransaction> marketTransactions { get; private set; }

        private SocialMarket()
        {
            ; //singleton
        }

        internal static SocialMarket GetInstance()
        {
            return (instance == null) ? instance = new SocialMarket() : instance;
        }

        internal void Initialize()
        {
            marketOrders = new List<SocialMarketOrder>();
            marketOrdersMarriageMale = new List<SocialMarketOrder>();
            marketOrdersMarriageFemale = new List<SocialMarketOrder>();
            marketTransactions = new List<SocialMarketTransaction>();
        }

        internal void AddOrder(SocialMarketOrder socialMarketOrder)
        {
            marketOrders.Add(socialMarketOrder);
        }

        internal void AddOrderMarriageMale(SocialMarketOrder socialMarketOrder)
        {
            marketOrdersMarriageMale.Add(socialMarketOrder);
        }

        internal void AddOrderMarriageFemale(SocialMarketOrder socialMarketOrder)
        {
            marketOrdersMarriageFemale.Add(socialMarketOrder);
        }

        internal void AddTransaction(SocialMarketTransaction socialMarketTransaction)
        {
            marketTransactions.Add(socialMarketTransaction);
        }

        internal void ExecuteClearingExceptMarriage()
        {
        }

        internal void ExecuteMarriageClearing()
        {
            ShuffleMarriageOrders();
            MatchMarriageOrders();
            ClearUnmatchedMarriageOrders();
        }

        private void ShuffleMarriageOrders()
        {
            Randomness.Shuffle(marketOrdersMarriageMale);
            Randomness.Shuffle(marketOrdersMarriageFemale);
        }

        private void MatchMarriageOrders()
        {
            int maxCoupleCount = Math.Min(marketOrdersMarriageMale.Count, marketOrdersMarriageFemale.Count);
            for (int i = 0; i < maxCoupleCount; i++) {
                Family.MatchCouple((Player) marketOrdersMarriageMale[i].entityBy, (Player) marketOrdersMarriageFemale[i].entityBy);
            }
        }

        private void ClearUnmatchedMarriageOrders()
        {
            marketOrdersMarriageMale.Clear();
            marketOrdersMarriageFemale.Clear();
        }

    }
}
