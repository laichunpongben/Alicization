using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alicization.Model.GlobalObjects;
using System.Diagnostics;
using Alicization.Model.Demographics.Players;

namespace Alicization.Model.Demographics
{
    class DemographicReport
    {
        public string content { get; private set; }
        private DemographicReportType reportType { get; set; }
        private static readonly int parameterCount = 7;
        private bool isCountPlayers { get; set; }
        private bool isCountSingles { get; set; }
        private bool isCountMarrieds { get; set; }
        private bool isCountMales { get; set; }
        private bool isCountFemales { get; set; }
        private bool isCountCorporations { get; set; }

        public enum DemographicReportType 
        {
            None = 0, 
            All = 127, 
            PlayerOnly = 64, 
            PlayerGender = 76,
            PlayerMartialStatus = 104, 
            PlayerMartialStatusGender = 124
        }

        public DemographicReport(DemographicReportType type)
        {
            int playerCount = 0;
            int singleCount = 0;
            int marriedCount = 0;
            int maleCount = 0;
            int femaleCount = 0;
            int corporationCount = 0;
            int familyCount = 0;

            string typeToBinary = string.Format("{0:0000000}", Convert.ToInt32(Convert.ToString((int) type, 2)));
            string typeToBinaryParameterLength = typeToBinary.Substring(typeToBinary.Length - parameterCount);

            bool isCountPlayers = Convert.ToBoolean(Convert.ToInt32(typeToBinaryParameterLength.Substring(0, 1)));
            bool isCountSingles = Convert.ToBoolean(Convert.ToInt32(typeToBinaryParameterLength.Substring(1, 1)));
            bool isCountMarrieds = Convert.ToBoolean(Convert.ToInt32(typeToBinaryParameterLength.Substring(2, 1)));
            bool isCountMales = Convert.ToBoolean(Convert.ToInt32(typeToBinaryParameterLength.Substring(3, 1)));
            bool isCountFemales = Convert.ToBoolean(Convert.ToInt32(typeToBinaryParameterLength.Substring(4, 1)));
            bool isCountCorporations = Convert.ToBoolean(Convert.ToInt32(typeToBinaryParameterLength.Substring(5, 1)));
            bool isCountFamilies = Convert.ToBoolean(Convert.ToInt32(typeToBinaryParameterLength.Substring(6, 1)));

            StringBuilder sb = new StringBuilder();

            sb.Append(Time.Now().ToString());

            if (isCountPlayers) {
                playerCount = World.players.Count();
                sb.Append(",");
                sb.Append(playerCount.ToString());
            }

            if (isCountSingles) {
                singleCount = World.players.CountSinglePassively();
                sb.Append(",");
                sb.Append(singleCount.ToString());
            }

            if (isCountMarrieds) {
                marriedCount = World.players.CountMarriedPassively();
                sb.Append(",");
                sb.Append(marriedCount.ToString());
            }

            if (isCountMales) {
                maleCount = World.players.CountMalePassively();
                sb.Append(",");
                sb.Append(maleCount.ToString());
            }

            if (isCountFemales) {
                femaleCount = World.players.CountFemalePassively();
                sb.Append(",");
                sb.Append(femaleCount.ToString());
            }

            if (isCountCorporations) {
                corporationCount = World.corporations.Count();
                sb.Append(",");
                sb.Append(corporationCount.ToString());
            }

            if (isCountFamilies) {
                familyCount = World.families.Count();
                sb.Append(",");
                sb.Append(familyCount.ToString());
            }

            content = sb.ToString();
        }

        public void ReportPopulationAges() // refactoring needed
        {
            IDictionary<Player, int> playerAges = World.players.GetPlayerAges();
            foreach (KeyValuePair<Player, int> entry in playerAges)
            {
                Debug.WriteLine(entry.Key.entityName.ToString() + "," + entry.Value.ToString());
            }
        }

        public void ReportDemographicHistory() // refactoring needed
        {
            if (Time.Now() == 100000)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Turn,Birth,Death,Population");
                foreach (DemographicMovement st in Demographic.demographicHistory)
                {
                    sb.Append(st.turn.ToString());
                    sb.Append(",");
                    sb.Append(st.birthCount.ToString());
                    sb.Append(",");
                    sb.Append(st.deathCount.ToString());
                    sb.Append(",");
                    sb.AppendLine(st.playerCount.ToString());
                }
                Debug.WriteLine(sb.ToString());
            }
        }
       
    }
}
