using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alicization.Model.Tests;

namespace Alicization.View
{
    public partial class FormRandomnessTest : Form
    {
        public FormRandomnessTest()
        {
            InitializeComponent();
        }

        private void btnBinomialPoisson_Click(object sender, EventArgs e)
        {
            RandomnessTest.TestSampleBinomialPoisson(0.00027, 50000);
        }

        private void btnGamma_Click(object sender, EventArgs e)
        {
            RandomnessTest.TestSampleGamma(1000, 10, 10000);
        }

        private void btnConstHazardRate_Click(object sender, EventArgs e)
        {
            RandomnessTest.TestSampleLifeConstantHazardRateBernoulli(0.001, 10000);
        }

        private void btnConstHazardRateExp_Click(object sender, EventArgs e)
        {
            RandomnessTest.TestSampleLifeConstantHazardRateExponential(0.001, 10000);
        }

        private void btnLinearHazardRate_Click(object sender, EventArgs e)
        {
            RandomnessTest.TestSampleLifeLinearHazardRate(2000, 10000);
        }
    }
}
