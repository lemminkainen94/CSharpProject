namespace OptymalizacjaCięcia
{
    partial class CommissionViewer
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
            this.commissionBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // commissionBox
            // 
            this.commissionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commissionBox.FormattingEnabled = true;
            this.commissionBox.Location = new System.Drawing.Point(0, 0);
            this.commissionBox.Name = "commissionBox";
            this.commissionBox.Size = new System.Drawing.Size(404, 261);
            this.commissionBox.TabIndex = 0;
            this.commissionBox.SelectedIndexChanged += new System.EventHandler(this.commission_SelectedIndexChanged);
            // 
            // CommissionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 261);
            this.Controls.Add(this.commissionBox);
            this.Name = "CommissionViewer";
            this.Text = "CommissionViewer";
            this.Load += new System.EventHandler(this.CommissionViewer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox commissionBox;
    }
}