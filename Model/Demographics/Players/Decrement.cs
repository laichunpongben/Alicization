using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alicization.Model.Demographics.Players
{
    static class Decrement
    {
        static Decrement()
        {
        }

        public static void CheckHygiene(this Player player)
        {
            player.CheckStarvation();
            player.CheckSickness();
            player.CheckUnhappiness();
        }

        public static void CheckStarvation(this Player player)
        {
            if (player.hygiene.satiationLevel < player.hygiene.satiationCritical && (! player.isDead)) player.EndLife(DecrementType.Starvation);
        }

        public static void CheckSickness(this Player player)
        {
            if (player.hygiene.healthLevel < player.hygiene.healthCritical && (! player.isDead)) player.EndLife(DecrementType.Sickness);
        }

        public static void CheckUnhappiness(this Player player)
        {
            if (player.hygiene.happinessLevel < player.hygiene.happinessCritical && (! player.isDead)) player.EndLife(DecrementType.Unhappiness);
        }
    }
}
