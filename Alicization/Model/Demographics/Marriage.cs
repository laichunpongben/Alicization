using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;
using Alicization.Model.Demographics.Players;
using Alicization.Model.Markets;

namespace Alicization.Model.Demographics
{
    internal class Marriage
    {
        private static Marriage instance = null;

        private static double constMarriageRate { get; set; }

        private Marriage()
        {
            ; //singleton
        }

        internal static Marriage GetInstance()
        {
            return (instance == null) ? instance = new Marriage() : instance;
        }

        internal void Initialize()
        {
            constMarriageRate = 0.0025;
        }

        internal void Invoke()
        {
            int locationCount = World.locations.Count();
            if (locationCount > 1) {
                DrawPlayersMarriageByLocation();
            } else {
                DrawPlayersMarriage();
            }
        }

        private void DrawPlayersMarriageByLocation()
        {
            foreach (var item in World.locations.dict) {
                IEnumerable<Player> singleMales = World.players.SelectSingleMales(World.residences.SelectLocalPlayers(item.Value));
                IEnumerable<Player> singleFemales = World.players.SelectSingleFemales(World.residences.SelectLocalPlayers(item.Value));

                int singleMalesCount = singleMales.Count();
                int singleFemalesCount = singleFemales.Count();

                int maxAvailableFamiliesCount = Math.Min(singleMalesCount, singleFemalesCount);
                int marriageCount = SampleMarriage(constMarriageRate, maxAvailableFamiliesCount);
                if (marriageCount > 0) {
                    List<Player> selectedMales = Randomness.Draw(marriageCount, singleMales, singleMalesCount).ToList();
                    List<Player> selectedFemales = Randomness.Draw(marriageCount, singleFemales, singleFemalesCount).ToList();
                    //Randomness.Shuffle(selectedMales);
                    //Randomness.Shuffle(selectedFemales);
                    //Family.MatchManyCouples(selectedMales, selectedFemales);

                    foreach (var male in selectedMales) {
                        male.BidSocialRelationship(SocialRelationshipType.Marriage, 1);
                    }
                    foreach (var female in selectedFemales)
                    {
                        female.BidSocialRelationship(SocialRelationshipType.Marriage, 1);
                    }

                    World.socialMarket.ExecuteMarriageClearing();
                }
            }
        }

        private void DrawPlayersMarriage() //Only used when one location
        {
            IEnumerable<Player> singleMales = World.players.SelectSingleMales();
            IEnumerable<Player> singleFemales = World.players.SelectSingleFemales();

            int singleMalesCount = singleMales.Count();
            int singleFemalesCount = singleFemales.Count();

            int maxAvailableFamiliesCount = Math.Min(singleMalesCount, singleFemalesCount);
            int marriageCount = SampleMarriage(constMarriageRate, maxAvailableFamiliesCount);
            if (marriageCount > 0) {
                List<Player> selectedMales = Randomness.Draw(marriageCount, singleMales, singleMalesCount).ToList();
                List<Player> selectedFemales = Randomness.Draw(marriageCount, singleFemales, singleFemalesCount).ToList();
                //Randomness.Shuffle(selectedMales);
                //Randomness.Shuffle(selectedFemales);
                //Family.MatchManyCouples(selectedMales, selectedFemales);

                foreach (var male in selectedMales) {
                    male.BidSocialRelationship(SocialRelationshipType.Marriage, 1);
                }
                foreach (var female in selectedFemales)
                {
                    female.BidSocialRelationship(SocialRelationshipType.Marriage, 1);
                }

                World.socialMarket.ExecuteMarriageClearing();
            }
        }

        private int SampleMarriage()
        {
            Bernoulli bernoulli = new Bernoulli(constMarriageRate);
            return bernoulli.Sample();
        }

        private int SampleMarriage(double marriageRate)
        {
            Bernoulli bernoulli = new Bernoulli(marriageRate);
            return bernoulli.Sample();
        }

        private int SampleMarriage(double marriageRate, int groupSize)
        {
            return Randomness.SampleBinomialPoisson(marriageRate, groupSize);
        }

    }
}
