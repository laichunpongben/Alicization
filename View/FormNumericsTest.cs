using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WorldGraph.Model;
using MathNet.Numerics;

namespace WorldGraph.View
{
    public partial class FormNumericsTest : Form
    {
        public FormNumericsTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NumericsTest.TestFactorial();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NumericsTest.TestTaylorExp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NumericsTest.TestTaylorLogOnePluxX();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NumericsTest.TestTaylorOnePlusXPowerAlpha();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NumericsTest.TestBinomialCoefficientInt();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NumericsTest.TestProduct();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NumericsTest.TestBinomialCoefficientDouble();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NumericsTest.TestPower();
        }
    }
}
