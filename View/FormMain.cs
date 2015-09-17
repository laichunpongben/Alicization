using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alicization.Model;

namespace Alicization.View
{
    public partial class FormMain : Form
    {
        private static World world = null;

        public FormMain()
        {
            InitializeComponent();
            world = new World();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            int turnCount = Convert.ToInt32(txtTurn.Text);
            world.PlayManyTurns(turnCount);
            lblTurn.Text = "T: " + world.ReportTurn().ToString();
            lblPopulation.Text = "P: " + world.ReportPopulation().ToString();
            lblMeanScore.Text = "Mean Score: " + world.ReportMeanScore().ToString("#,##0.00");
            lblMaxScore.Text = "Max Score: " + world.ReportMaxScore().ToString("#,##0.00");
        }

        private void lblTurn_Click(object sender, EventArgs e)
        {

        }

        private void txtTurn_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnShowPlayer_Click(object sender, EventArgs e)
        {
            int playerId = Convert.ToInt32(txtPlayerId.Text);
            //try {
            //    lblPlayer.Text = world.ReportPlayerStatus(playerId);
            //} catch (Exception ex) {
            //    Debug.WriteLine(ex.Message + "," + ex.StackTrace);
            //    lblPlayer.Text = "Player not found";
            //} 
            lblPlayer.Text = world.ReportPlayerStatus(playerId);
        }
    }
}
