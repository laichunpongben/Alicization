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
using WorldGraph.Model;

namespace WorldGraph.View
{
    public partial class Form1 : Form
    {
        private static World world = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            world.PlayTurn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            world.PlayManyTurns(100);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            world.PlayManyTurns(10000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++) {
                Debug.WriteLine("World " + i + " ...");
                World newWorld = new World();
                Stopwatch sw = new Stopwatch();
                sw.Start();
                newWorld.PlayManyTurns(100000);
                sw.Stop();
                Debug.WriteLine("Time = " + sw.Elapsed.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i=0; i < 100000; i++) {
                World newWorld = new World();
                newWorld.PlayTurn();
                int playerCount = newWorld.CountPlayers();
                Debug.WriteLine(playerCount.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            World newWorld = new World();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            newWorld.PlayManyTurns(100000);
            sw.Stop();
            Debug.WriteLine("Time = " + sw.Elapsed.ToString());
        }
    }
}
