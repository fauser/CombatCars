namespace CombatCarsTestClient
{
    partial class LogonForm
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
            this.logonControl1 = new CombatCarsTestClient.LogonControl();
            this.SuspendLayout();
            // 
            // logonControl1
            // 
            this.logonControl1.Location = new System.Drawing.Point(24, 12);
            this.logonControl1.Name = "logonControl1";
            this.logonControl1.Size = new System.Drawing.Size(310, 74);
            this.logonControl1.TabIndex = 0;
            // 
            // LogonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 96);
            this.Controls.Add(this.logonControl1);
            this.Name = "LogonForm";
            this.Text = "LogonForm";
            this.ResumeLayout(false);

        }

        #endregion

        private LogonControl logonControl1;
    }
}