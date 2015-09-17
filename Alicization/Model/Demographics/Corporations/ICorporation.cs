using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alicization.Model.Demographics.Corporations
{
    public interface ICorporation : IPhysicalEntity, IWealthEntity
    {
        //string corporationName { get; }
        //private double profit;
        int memberCount { get; }

        void AddMember(IEntity entity);

    }
}
