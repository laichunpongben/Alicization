using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alicization.Model.Wealth.Assets.Commodities;
using System.Collections.Concurrent;

namespace Alicization.Model.Demographics.Players
{
    internal class PlayerActivity
    {
        internal int activityId { get; private set; }
        internal Action<Player> activity { get; private set; }
        internal double timeCost { get; private set; }

        internal PlayerActivity(int id, Action<Player> func, double timeInTurn)
        {
            activityId = id;
            activity = func;
            timeCost = timeInTurn;
        }

        
        

        
    }
}
