using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Extensions;
using Alicization.Model.Demographics.Players;
using Alicization.Util.Numerics;

namespace Alicization.Model.GlobalObjects
{
    public class Players : IGlobalObject
    {
        private static Players instance = null;

        internal ConcurrentDictionary<int, Player> active { get; private set; }
        internal ConcurrentDictionary<int, Player> dead { get; private set; }

        internal int singleCount { get; private set; }
        internal int marriedCount { get; private set; }
        internal int maleCount { get; private set; }
        internal int femaleCount { get; private set; }
        internal int deadCount { get; private set; }

        private Players()
        {
            ; //singleton
        }

        internal static Players GetInstance()
        {
            return (instance == null) ? instance = new Players() : instance; 
        }

        internal void Initialize()
        {
            active = new ConcurrentDictionary<int, Player>();
            dead = new ConcurrentDictionary<int, Player>();
            singleCount = 0;
            marriedCount = 0;
            maleCount = 0;
            femaleCount = 0;
            deadCount = 0;
        }

        public int Count()
        {
            return active.Count;
        }

        internal int MaxId()
        {
            return (active.Count > 0) ? active.Keys.Max() : 0;
        }

        internal int CountMale()
        {
            return active.Count(i => i.Value.gender == Player.Gender.Male);
        }

        internal int CountFemale()
        {
            return active.Count(i => i.Value.gender == Player.Gender.Female);
        }

        internal int CountSingle()
        {
            return active.Count(i => i.Value.martialStatus == Player.MartialStatus.Single);
        }

        internal int CountMarried()
        {
            return active.Count(i => i.Value.martialStatus == Player.MartialStatus.Married);
        }

        internal int CountDead()
        {
            return active.Count(i => i.Value.isDead);
        }

        internal int CountMalePassively()
        {
            return maleCount;
        }

        internal int CountFemalePassively()
        {
            return femaleCount;
        }

        internal int CountSinglePassively()
        {
            return singleCount;
        }

        internal int CountMarriedPassively()
        {
            return marriedCount;
        }

        internal void IncrementMaleCount()
        {
            maleCount++;
        }

        internal void IncrementFemaleCount()
        {
            femaleCount++;
        }

        internal void IncrementSingleCount()
        {
            singleCount++;
        }

        internal void IncrementMarriedCount()
        {
            marriedCount++;
        }

        internal void DecrementMaleCount()
        {
            maleCount--;
        }

        internal void DecrementFemaleCount()
        {
            femaleCount--;
        }

        internal void DecrementSingleCount()
        {
            singleCount--;
        }

        internal void DecrementMarriedCount()
        {
            marriedCount--;
        }

        internal IEnumerable<Player> SelectSingleMales()
        {
            return active.Where(i => i.Value.martialStatus == Player.MartialStatus.Single && i.Value.gender == Player.Gender.Male).Select(i => i.Value);
        }

        internal IEnumerable<Player> SelectSingleMales(IEnumerable<Player> localPlayers)
        {
            return localPlayers.Where(player => player.martialStatus == Player.MartialStatus.Single && player.gender == Player.Gender.Male);
        }

        internal IEnumerable<Player> SelectSingleFemales()
        {
            return active.Where(i => i.Value.martialStatus == Player.MartialStatus.Single && i.Value.gender == Player.Gender.Female).Select(i => i.Value);
        }

        internal IEnumerable<Player> SelectSingleFemales(IEnumerable<Player> localPlayers)
        {
            return localPlayers.Where(player => player.martialStatus == Player.MartialStatus.Single && player.gender == Player.Gender.Female);
        }

        internal void Add(Player player)
        {
            active.TryAdd(player.entityId, player);
        }

        internal void AddDead(Player player)
        {
            dead.TryAdd(player.entityId, player);
        }

        internal void Remove(Player player)
        {
            active.Remove(player.entityId);
        }

        internal void RemoveDead() //obsolete
        {
            foreach (var item in active.Where(i => i.Value.isDead)) {
                active.Remove(item.Key);
            }
        }

        internal Player Draw()
        {
            return Randomness.Draw(active);
        }

        internal IEnumerable<int> GetAges()
        {
            return active.Select(i => i.Value.ComputeAge());
        }

        internal IDictionary<Player, int> GetPlayerAges()
        {
            return active.Values.Zip(GetAges(), (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
        }

        public Player GetPlayer(int entityId)
        {
            Player p;
            return (active.TryGetValue(entityId, out p)) ? p :
                (dead.TryGetValue(entityId, out p)) ? p : null;
        }

        internal IDictionary<Player, int> ReverseActivePlayersDictionary()
        {
            return active.Select(i => new KeyValuePair<Player, int>(i.Value, i.Key)).ToConcurrentDictionary();
        }

        internal IDictionary<Player, int> ReverseDeadPlayersDictionary()
        {
            return dead.Select(i => new KeyValuePair<Player, int>(i.Value, i.Key)).ToConcurrentDictionary();
        }

        public List<Player> ToList()
        {
            return active.Values.ToList();
        }

    }
}
