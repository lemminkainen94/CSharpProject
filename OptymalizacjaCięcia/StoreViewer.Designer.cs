namespace OptymalizacjaCięcia
{
    partial class StoreViewer
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
            this.storeBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // storeBox
            // 
            this.storeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storeBox.FormattingEnabled = true;
            this.storeBox.Location = new System.Drawing.Point(0, 0);
            this.storeBox.Name = "storeBox";
            this.storeBox.Size = new System.Drawing.Size(404, 261);
            this.storeBox.TabIndex = 0;
            this.storeBox.SelectedIndexChanged += new System.EventHandler(this.storeBox_SelectedIndexChanged);
            // 
            // StoreViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 261);
            this.Controls.Add(this.storeBox);
            this.Name = "StoreViewer";
            this.Text = "StoreViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox storeBox;
    }
}