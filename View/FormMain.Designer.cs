namespace Alicization.View
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlay = new System.Windows.Forms.Button();
            this.txtTurn = new System.Windows.Forms.TextBox();
            this.btnShowPlayer = new System.Windows.Forms.Button();
            this.txtPlayerId = new System.Windows.Forms.TextBox();
            this.lblTurn = new System.Windows.Forms.Label();
            this.lblPopulation = new System.Windows.Forms.Label();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblMeanScore = new System.Windows.Forms.Label();
            this.lblMaxScore = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(169, 9);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(56, 18);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Next";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // txtTurn
            // 
            this.txtTurn.Location = new System.Drawing.Point(230, 10);
            this.txtTurn.Margin = new System.Windows.Forms.Padding(2);
            this.txtTurn.Name = "txtTurn";
            this.txtTurn.Size = new System.Drawing.Size(41, 19);
            this.txtTurn.TabIndex = 1;
            this.txtTurn.Text = "1";
            this.txtTurn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTurn.TextChanged += new System.EventHandler(this.txtTurn_TextChanged);
            // 
            // btnShowPlayer
            // 
            this.btnShowPlayer.Location = new System.Drawing.Point(169, 33);
            this.btnShowPlayer.Margin = new System.Windows.Forms.Padding(2);
            this.btnShowPlayer.Name = "btnShowPlayer";
            this.btnShowPlayer.Size = new System.Drawing.Size(56, 18);
            this.btnShowPlayer.TabIndex = 2;
            this.btnShowPlayer.Text = "Show P";
            this.btnShowPlayer.UseVisualStyleBackColor = true;
            this.btnShowPlayer.Click += new System.EventHandler(this.btnShowPlayer_Click);
            // 
            // txtPlayerId
            // 
            this.txtPlayerId.Location = new System.Drawing.Point(230, 34);
            this.txtPlayerId.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlayerId.Name = "txtPlayerId";
            this.txtPlayerId.Size = new System.Drawing.Size(41, 19);
            this.txtPlayerId.TabIndex = 3;
            this.txtPlayerId.Text = "1";
            this.txtPlayerId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTurn
            // 
            this.lblTurn.AutoSize = true;
            this.lblTurn.Location = new System.Drawing.Point(9, 12);
            this.lblTurn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTurn.Name = "lblTurn";
            this.lblTurn.Size = new System.Drawing.Size(24, 12);
            this.lblTurn.TabIndex = 4;
            this.lblTurn.Text = "T: 0";
            this.lblTurn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTurn.Click += new System.EventHandler(this.lblTurn_Click);
            // 
            // lblPopulation
            // 
            this.lblPopulation.AutoSize = true;
            this.lblPopulation.Location = new System.Drawing.Point(9, 36);
            this.lblPopulation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPopulation.Name = "lblPopulation";
            this.lblPopulation.Size = new System.Drawing.Size(24, 12);
            this.lblPopulation.TabIndex = 5;
            this.lblPopulation.Text = "P: 0";
            this.lblPopulation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Location = new System.Drawing.Point(167, 63);
            this.lblPlayer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(24, 12);
            this.lblPlayer.TabIndex = 6;
            this.lblPlayer.Text = "P: 0";
            this.lblPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMeanScore
            // 
            this.lblMeanScore.AutoSize = true;
            this.lblMeanScore.Location = new System.Drawing.Point(9, 63);
            this.lblMeanScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMeanScore.Name = "lblMeanScore";
            this.lblMeanScore.Size = new System.Drawing.Size(77, 12);
            this.lblMeanScore.TabIndex = 7;
            this.lblMeanScore.Text = "Mean Score: 0";
            this.lblMeanScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMaxScore
            // 
            this.lblMaxScore.AutoSize = true;
            this.lblMaxScore.Location = new System.Drawing.Point(9, 87);
            this.lblMaxScore.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxScore.Name = "lblMaxScore";
            this.lblMaxScore.Size = new System.Drawing.Size(71, 12);
            this.lblMaxScore.TabIndex = 8;
            this.lblMaxScore.Text = "Max Score: 0";
            this.lblMaxScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 204);
            this.Controls.Add(this.lblMaxScore);
            this.Controls.Add(this.lblMeanScore);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.lblPopulation);
            this.Controls.Add(this.lblTurn);
            this.Controls.Add(this.txtPlayerId);
            this.Controls.Add(this.btnShowPlayer);
            this.Controls.Add(this.txtTurn);
            this.Controls.Add(this.btnPlay);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.Text = "Alice";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.TextBox txtTurn;
        private System.Windows.Forms.Button btnShowPlayer;
        private System.Windows.Forms.TextBox txtPlayerId;
        private System.Windows.Forms.Label lblTurn;
        private System.Windows.Forms.Label lblPopulation;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblMeanScore;
        private System.Windows.Forms.Label lblMaxScore;
    }
}