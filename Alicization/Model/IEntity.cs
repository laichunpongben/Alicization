using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.Wealth;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Demographics.Corporations;

namespace Alicization.Model
{
    public interface IEntity
    {
        int entityId { get; }
        EntityType entityType { get; } // Nature, Market, Player, ArtificialIntelligence, PublicDomain, Family, Corporation, Government

        void TransferPhysicalAsset(IEntity target, ILocation location, IPhysicalAsset asset, double quantity);
        void TransferVirtualAsset(IEntity target, IVirtualAsset asset, double quantity);
        
        bool HasSufficientLocalInventories(ILocation location, IPhysicalAsset asset, double quantity);
        bool HasSufficientVirtualAsset(IVirtualAsset asset, double quantity);
        
    }
}
