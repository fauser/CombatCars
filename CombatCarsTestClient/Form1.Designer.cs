namespace CombatCarsTestClient
{
    partial class Form1
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
            this.lblActiveToken = new System.Windows.Forms.Label();
            this.lbListOfCars = new System.Windows.Forms.ListBox();
            this.buGetListofCars = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblActiveToken
            // 
            this.lblActiveToken.AutoSize = true;
            this.lblActiveToken.Location = new System.Drawing.Point(12, 35);
            this.lblActiveToken.Name = "lblActiveToken";
            this.lblActiveToken.Size = new System.Drawing.Size(0, 13);
            this.lblActiveToken.TabIndex = 3;
            // 
            // lbListOfCars
            // 
            this.lbListOfCars.FormattingEnabled = true;
            this.lbListOfCars.Location = new System.Drawing.Point(12, 12);
            this.lbListOfCars.Name = "lbListOfCars";
            this.lbListOfCars.Size = new System.Drawing.Size(206, 303);
            this.lbListOfCars.TabIndex = 4;
            this.lbListOfCars.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbListOfCars_MouseClick);
            // 
            // buGetListofCars
            // 
            this.buGetListofCars.Location = new System.Drawing.Point(224, 13);
            this.buGetListofCars.Name = "buGetListofCars";
            this.buGetListofCars.Size = new System.Drawing.Size(75, 23);
            this.buGetListofCars.TabIndex = 5;
            this.buGetListofCars.Text = "Get Cars";
            this.buGetListofCars.UseVisualStyleBackColor = true;
            this.buGetListofCars.Click += new System.EventHandler(this.buGetListofCars_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 604);
            this.Controls.Add(this.buGetListofCars);
            this.Controls.Add(this.lbListOfCars);
            this.Controls.Add(this.lblActiveToken);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblActiveToken;
        private System.Windows.Forms.ListBox lbListOfCars;
        private System.Windows.Forms.Button buGetListofCars;
    }
}

