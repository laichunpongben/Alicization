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
    public partial class FormNumericTest : Form
    {
        public FormNumericTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NumericTest.TestFactorial();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NumericTest.TestTaylorExp();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NumericTest.TestTaylorLogOnePluxX();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NumericTest.TestTaylorOnePlusXPowerAlpha();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NumericTest.TestBinomialCoefficientInt();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            NumericTest.TestProduct();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            NumericTest.TestBinomialCoefficientDouble();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            NumericTest.TestPower();
        }
    }
}
