namespace OptymalizacjaCięcia
{
    partial class StartupWindow
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.storeButton = new System.Windows.Forms.Button();
            this.commissionButton = new System.Windows.Forms.Button();
            this.showStoreButton = new System.Windows.Forms.Button();
            this.showCommissionButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.86956F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.13044F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 208F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(492, 278);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(3, 45);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(145, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "dopuszczone stykowanie";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.storeButton);
            this.flowLayoutPanel1.Controls.Add(this.commissionButton);
            this.flowLayoutPanel1.Controls.Add(this.showStoreButton);
            this.flowLayoutPanel1.Controls.Add(this.showCommissionButton);
            this.flowLayoutPanel1.Controls.Add(this.startButton);
            this.flowLayoutPanel1.Controls.Add(this.closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 72);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(486, 203);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // storeButton
            // 
            this.storeButton.Location = new System.Drawing.Point(3, 3);
            this.storeButton.Name = "storeButton";
            this.storeButton.Size = new System.Drawing.Size(132, 23);
            this.storeButton.TabIndex = 1;
            this.storeButton.Text = "dodaj plik z magazynem";
            this.storeButton.UseVisualStyleBackColor = true;
            this.storeButton.Click += new System.EventHandler(this.storeButton_Click);
            // 
            // commissionButton
            // 
            this.commissionButton.Location = new System.Drawing.Point(3, 32);
            this.commissionButton.Name = "commissionButton";
            this.commissionButton.Size = new System.Drawing.Size(124, 23);
            this.commissionButton.TabIndex = 3;
            this.commissionButton.Text = "dodaj plik ze zleceniem";
            this.commissionButton.UseVisualStyleBackColor = true;
            this.commissionButton.Click += new System.EventHandler(this.commissionButton_Click);
            // 
            // showStoreButton
            // 
            this.showStoreButton.Location = new System.Drawing.Point(3, 61);
            this.showStoreButton.Name = "showStoreButton";
            this.showStoreButton.Size = new System.Drawing.Size(90, 23);
            this.showStoreButton.TabIndex = 4;
            this.showStoreButton.Text = "pokaż magazyn";
            this.showStoreButton.UseVisualStyleBackColor = true;
            this.showStoreButton.Click += new System.EventHandler(this.showStoreButton_Click);
            // 
            // showCommissionButton
            // 
            this.showCommissionButton.Location = new System.Drawing.Point(3, 90);
            this.showCommissionButton.Name = "showCommissionButton";
            this.showCommissionButton.Size = new System.Drawing.Size(90, 23);
            this.showCommissionButton.TabIndex = 5;
            this.showCommissionButton.Text = "pokaż zlecenie";
            this.showCommissionButton.UseVisualStyleBackColor = true;
            this.showCommissionButton.Click += new System.EventHandler(this.showCommissionButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(3, 119);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(131, 23);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "generuj listę rozkrojową";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(3, 148);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "zamknij";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(486, 36);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "Witaj w programie Optymalizacja Cięcia. Na chwilę obecną program jest w wersji te" +
    "stowej.  Wybierz odpowiednie pliki i rozpocznij obliczenia.";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Excel Files(*.xlsx)|*.xlsx";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "Excel Files(*.xlsx)|*.xlsx";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CreatePrompt = true;
            this.saveFileDialog1.Filter = "Text File(*.txt) | *.txt";
            // 
            // StartupWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 278);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StartupWindow";
            this.Text = "Optymalizacja Cięcia";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button storeButton;
        private System.Windows.Forms.Button commissionButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button showStoreButton;
        private System.Windows.Forms.Button showCommissionButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

