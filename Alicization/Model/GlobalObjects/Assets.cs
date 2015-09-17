using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using System.Collections.Concurrent;
using Alicization.Model.Wealth.Assets;
using Alicization.Model.Wealth.Assets.Commodities;
using Alicization.Util.Numerics;

namespace Alicization.Model.GlobalObjects
{
    public class Assets : IGlobalObject
    {
        private static Assets instance = null;

        internal ConcurrentDictionary<int, IAsset> commodities { get; private set; }
        internal ConcurrentDictionary<int, IAsset> realEstates { get; private set; }
        internal ConcurrentDictionary<int, IAsset> stocks { get; private set; }
        internal ConcurrentDictionary<int, IAsset> bonds { get; private set; }

        private Assets()
        {
            ; //singleton
        }

        internal static Assets GetInstance()
        {
            return (instance == null) ? instance = new Assets() : instance; 
        }

        internal void Initialize()
        {
            commodities = new ConcurrentDictionary<int, IAsset>();
            realEstates = new ConcurrentDictionary<int, IAsset>();
            stocks = new ConcurrentDictionary<int, IAsset>();
            bonds = new ConcurrentDictionary<int, IAsset>();

            InitializeCommodities();
        }

        public int Count()
        {
            return commodities.Count;
        }

        public int MaxId()
        {
            return Numeric.GetMaxId(commodities, realEstates, stocks, bonds);
        }

        internal IDictionary<IAsset, int> ReverseDictionary()
        {
            return commodities.Select(i => new KeyValuePair<IAsset, int>(i.Value, i.Key)).ToConcurrentDictionary();
        }

        private void InitializeCommodities()
        {
            InitializeNaturalMaterials();
            InitializePlanetaryMaterials();
            InitializeOres();
            InitializeMinerals();
        }

        private void InitializeNaturalMaterials()
        {
            SampleNaturalMaterials();
        }

        private void SeedNaturalMaterials()
        {
            ICommodity fish = new NaturalMaterial(1, "Fish", 1, 0, 0, 1, 0, 0);
            ICommodity crab = new NaturalMaterial(2, "Crab", 0, 1, 0, 1, 0, 0);
            ICommodity gold = new NaturalMaterial(3, "Gold", 0, 0, 1, 1, 0, 0);
        }

        private void SampleNaturalMaterials()
        {
            foreach(var i in Enumerable.Range(0, 10)) {
                ICommodity commodity = new NaturalMaterial();
            }
        }

        private void InitializePlanetaryMaterials()
        {
            SeedPlanetaryMaterials();
        }

        private void SeedPlanetaryMaterials()
        {
            ICommodity microorganisms = new PlanetaryMaterial(2073, "Microorganisms", 0.01);
            ICommodity baseMetals = new PlanetaryMaterial(2267, "Base Metals", 0.01);
            ICommodity aqueousLiquids = new PlanetaryMaterial(2268, "Aqueous Liquids", 0.01);
            ICommodity nobleMetals = new PlanetaryMaterial(2270, "Noble Metals", 0.01);
            ICommodity heavyMetals = new PlanetaryMaterial(2272, "Heavy Metals", 0.01);
            ICommodity plankticColonies = new PlanetaryMaterial(2286, "Planktic Colonies", 0.01);
            ICommodity complexOrganisms = new PlanetaryMaterial(2287, "Complex Organisms", 0.01);
            ICommodity carbonCompounds = new PlanetaryMaterial(2288, "Carbon Compounds", 0.01);
            ICommodity autotrophs = new PlanetaryMaterial(2305, "Autotrophs", 0.01);
            ICommodity nonCSCrystals = new PlanetaryMaterial(2306, "Non-CS Crystals", 0.01);
            ICommodity felsicMagma = new PlanetaryMaterial(2307, "Felsic Magma", 0.01);
            ICommodity suspendedPlasma = new PlanetaryMaterial(2308, "Suspended Plasma", 0.01);
            ICommodity ionicSolutions = new PlanetaryMaterial(2309, "Ionic Solutions", 0.01);
            ICommodity nobleGas = new PlanetaryMaterial(2310, "Noble Gas", 0.01);
            ICommodity reactiveGas = new PlanetaryMaterial(2311, "Reactive Gas", 0.01);
        }

        private void InitializeOres()
        {
            SeedOres();
        }

        private void SeedOres()
        {
            ICommodity veldspar = new Ore(1230, "Veldspar", 0.1);
            ICommodity scordite = new Ore(1228, "Scordite", 0.15);
        }

        private void InitializeMinerals()
        {
            SeedMinerals();
        }

        private void SeedMinerals()
        {
            ICommodity tritanium = new Mineral(34, "Tritanium", 0.01);
            ICommodity pyerite = new Mineral(35, "Pyerite", 0.01);
            ICommodity mexallon = new Mineral(36, "Mexallon", 0.01);
            ICommodity isogen = new Mineral(37, "Isogen", 0.01);
            ICommodity nocxium = new Mineral(38, "Nocxium", 0.01);
            ICommodity zydrine = new Mineral(39, "Zydrine", 0.01);
            ICommodity megacyte = new Mineral(40, "Megacyte", 0.01);
        }

        internal IPhysicalAsset GetCommodity(int assetId)
        {
            return (IPhysicalAsset) commodities[assetId];
        }

        internal IPhysicalAsset GetRealEstate(int assetId)
        {
            return (IPhysicalAsset) realEstates[assetId];
        }

        internal IVirtualAsset GetStock(int assetId)
        {
            return (IVirtualAsset) stocks[assetId];
        }

        internal IVirtualAsset GetBond(int assetId)
        {
            return (IVirtualAsset) bonds[assetId];
        }

        internal void AddCommodity(ICommodity asset)
        {
            commodities.TryAdd(asset.assetId, asset);
        }

        internal void AddRealEstate(IPhysicalAsset asset)
        {
            realEstates.TryAdd(asset.assetId, asset);
        }

        internal void AddStock(IVirtualAsset asset)
        {
            stocks.TryAdd(asset.assetId, asset);
        }

        internal void AddBond(IVirtualAsset asset)
        {
            bonds.TryAdd(asset.assetId, asset);
        }

        internal IPhysicalAsset DrawCommodity()
        {
            return (IPhysicalAsset) Randomness.Draw(commodities);
        }

        internal IPhysicalAsset DrawRealEstate()
        {
            return (IPhysicalAsset) Randomness.Draw(realEstates);
        }

        internal IVirtualAsset DrawStock()
        {
            return (IVirtualAsset) Randomness.Draw(stocks);
        }

        internal IVirtualAsset DrawBond()
        {
            return (IVirtualAsset) Randomness.Draw(bonds);
        }
    }
}
