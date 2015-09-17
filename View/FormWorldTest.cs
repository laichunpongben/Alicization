using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alicization.Model.Tests;

namespace Alicization.View
{
    public partial class FormWorldTest : Form
    {
        public FormWorldTest()
        {
            InitializeComponent();
        }

        private void btnNextTurn_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlayTurn();
        }

        private void btnNext10KTurns_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlay10KTurns();
        }

        private void btnNext25KTurns_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlay25KTurns();
        }

        private void btnNext50KTurns_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlay50KTurns();
        }

        private void btnNext100KTurns_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlay100KTurns();
        }

        private void btn10Worlds_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlay100KTurnsX10();
        }

        private void btnSingleTurnTest_Click(object sender, EventArgs e)
        {
            WorldTest.TestSingleTurnAllDemographicMethods();
        }

        private void btnPopulation2500_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlayUntilPopulation2500();
        }

        private void FormWorldTest_Load(object sender, EventArgs e)
        {

        }

        private void btnPopulation10K_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlayUntilPopulation10K();
        }

        private void btnPopulation500_Click(object sender, EventArgs e)
        {
            WorldTest.TestPlayUntilPopulation500();
        }
    }
}
