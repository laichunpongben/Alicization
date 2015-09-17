using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Util.Numerics;

namespace Alicization.Model.GlobalObjects
{
    public class Scores : IGlobalObject
    {
        private static Scores instance = null;

        List<double> runningScores { get; set; }
        List<double> finishedScores { get; set; }

        private Scores()
        {
            //singleton
        }

        internal static Scores GetInstance()
        {
            return (instance == null) ? instance = new Scores() : instance;
        }

        internal void Initialize()
        {
            runningScores = new List<double>();
            finishedScores = new List<double>();
        }

        internal void AddRunningScore(double x)
        {
            runningScores.Add(x);
        }

        internal void AddFinishedScore(double x)
        {
            finishedScores.Add(x);
        }

        internal void ClearRunning()
        {
            runningScores.Clear();
        }

        internal double ComputeAverage()
        {
            return Numeric.Average(runningScores, finishedScores);
        }

        internal double ComputeMax()
        {
            return Numeric.Max(runningScores, finishedScores);
        }
    }
}
