using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Alicization.Model.Tests
{
    class WorldTest
    {
        public static void TestPlayTurn()
        {
            World world = new World();
            world.PlayTurn();
        }

        public static void TestPlay10KTurns()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayManyTurns(10000);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }

        public static void TestPlay25KTurns()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayManyTurns(25000);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }

        public static void TestPlay50KTurns()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayManyTurns(50000);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }

        public static void TestPlay100KTurns()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayManyTurns(100000);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }

        public static void TestPlay100KTurnsX10()
        {
            for (int i = 0; i < 10; i++) {
                Debug.WriteLine("World " + i + " ...");
                World newWorld = new World();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                newWorld.PlayManyTurns(100000);
                sw.Stop();
                Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
            }
        }

        public static void TestSingleTurnAllDemographicMethods()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayManyTurns(100000);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }

        public static void TestPlayUntilPopulation500()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayUntilPopulationReach(500);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }

        public static void TestPlayUntilPopulation2500()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayUntilPopulationReach(2500);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }

        public static void TestPlayUntilPopulation10K()
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayUntilPopulationReach(10000);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.ElapsedTicks.ToString("#,##0"));
        }
    }
}
