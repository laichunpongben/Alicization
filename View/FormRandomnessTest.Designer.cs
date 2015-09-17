namespace Alicization.View
{
    partial class FormRandomnessTest
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
            this.btnBinomialPoisson = new System.Windows.Forms.Button();
            this.btnGamma = new System.Windows.Forms.Button();
            this.btnConstHazardRate = new System.Windows.Forms.Button();
            this.btnConstHazardRateExp = new System.Windows.Forms.Button();
            this.btnLinearHazardRate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBinomialPoisson
            // 
            this.btnBinomialPoisson.Location = new System.Drawing.Point(13, 13);
            this.btnBinomialPoisson.Name = "btnBinomialPoisson";
            this.btnBinomialPoisson.Size = new System.Drawing.Size(126, 23);
            this.btnBinomialPoisson.TabIndex = 0;
            this.btnBinomialPoisson.Text = "Binomial Poisson";
            this.btnBinomialPoisson.UseVisualStyleBackColor = true;
            this.btnBinomialPoisson.Click += new System.EventHandler(this.btnBinomialPoisson_Click);
            // 
            // btnGamma
            // 
            this.btnGamma.Location = new System.Drawing.Point(13, 42);
            this.btnGamma.Name = "btnGamma";
            this.btnGamma.Size = new System.Drawing.Size(126, 23);
            this.btnGamma.TabIndex = 1;
            this.btnGamma.Text = "Gamma";
            this.btnGamma.UseVisualStyleBackColor = true;
            this.btnGamma.Click += new System.EventHandler(this.btnGamma_Click);
            // 
            // btnConstHazardRate
            // 
            this.btnConstHazardRate.Location = new System.Drawing.Point(13, 71);
            this.btnConstHazardRate.Name = "btnConstHazardRate";
            this.btnConstHazardRate.Size = new System.Drawing.Size(126, 23);
            this.btnConstHazardRate.TabIndex = 2;
            this.btnConstHazardRate.Text = "Const hazard rate";
            this.btnConstHazardRate.UseVisualStyleBackColor = true;
            this.btnConstHazardRate.Click += new System.EventHandler(this.btnConstHazardRate_Click);
            // 
            // btnConstHazardRateExp
            // 
            this.btnConstHazardRateExp.Location = new System.Drawing.Point(12, 100);
            this.btnConstHazardRateExp.Name = "btnConstHazardRateExp";
            this.btnConstHazardRateExp.Size = new System.Drawing.Size(126, 23);
            this.btnConstHazardRateExp.TabIndex = 3;
            this.btnConstHazardRateExp.Text = "Const hazard rate exp";
            this.btnConstHazardRateExp.UseVisualStyleBackColor = true;
            this.btnConstHazardRateExp.Click += new System.EventHandler(this.btnConstHazardRateExp_Click);
            // 
            // btnLinearHazardRate
            // 
            this.btnLinearHazardRate.Location = new System.Drawing.Point(12, 129);
            this.btnLinearHazardRate.Name = "btnLinearHazardRate";
            this.btnLinearHazardRate.Size = new System.Drawing.Size(126, 23);
            this.btnLinearHazardRate.TabIndex = 4;
            this.btnLinearHazardRate.Text = "linear hazard rate";
            this.btnLinearHazardRate.UseVisualStyleBackColor = true;
            this.btnLinearHazardRate.Click += new System.EventHandler(this.btnLinearHazardRate_Click);
            // 
            // FormRandomnessTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnLinearHazardRate);
            this.Controls.Add(this.btnConstHazardRateExp);
            this.Controls.Add(this.btnConstHazardRate);
            this.Controls.Add(this.btnGamma);
            this.Controls.Add(this.btnBinomialPoisson);
            this.Name = "FormRandomnessTest";
            this.Text = "FormRandomnessTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBinomialPoisson;
        private System.Windows.Forms.Button btnGamma;
        private System.Windows.Forms.Button btnConstHazardRate;
        private System.Windows.Forms.Button btnConstHazardRateExp;
        private System.Windows.Forms.Button btnLinearHazardRate;
    }
}