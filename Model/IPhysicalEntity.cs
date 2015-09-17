using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.Geographics.Locations;
using Alicization.Model.Wealth;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Demographics.Corporations;
using Alicization.Model.Demographics.Players;

namespace Alicization.Model
{
    public interface IPhysicalEntity : IEntity
    {
        string entityName { get; }
        int dateOfBirth { get; }

        ILocation GetResidence();
        void AddResidence(ILocation location);
        void RemoveResidence();
        void ChangeResidence(ILocation location);
        int ComputeAge();

        void TransferPhysicalAsset(IEntity target, IPhysicalAsset asset, double quantity);
        void AccessPhysicalAsset(ICorporation target, IPhysicalAsset asset, double quantity);
        void AccessPhysicalAsset(ICorporation target, ILocation location, IPhysicalAsset asset, double quantity);
        void AccessVirtualAsset(ICorporation target, IVirtualAsset asset, double quantity);

        bool HasSufficientLocalInventories(IPhysicalAsset asset, double quantity);
        bool IsAccessible(ICorporation target);
    }
}
