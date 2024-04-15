namespace ProcessTimeChecker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			dataGridView1 = new DataGridView();
			menuStrip1 = new MenuStrip();
			ayarlarToolStripMenuItem = new ToolStripMenuItem();
			stayOnTopToolStripMenuItem = new ToolStripMenuItem();
			uygulamalarToolStripMenuItem = new ToolStripMenuItem();
			yeniUygulamaEkleToolStripMenuItem = new ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			// 
			// dataGridView1
			// 
			dataGridView1.BackgroundColor = SystemColors.ActiveBorder;
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Dock = DockStyle.Bottom;
			dataGridView1.Location = new Point(0, 27);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.Size = new Size(435, 739);
			dataGridView1.TabIndex = 0;
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new ToolStripItem[] { ayarlarToolStripMenuItem, uygulamalarToolStripMenuItem });
			menuStrip1.Location = new Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new Size(435, 24);
			menuStrip1.TabIndex = 1;
			menuStrip1.Text = "menuStrip1";
			// 
			// ayarlarToolStripMenuItem
			// 
			ayarlarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { stayOnTopToolStripMenuItem });
			ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
			ayarlarToolStripMenuItem.Size = new Size(109, 20);
			ayarlarToolStripMenuItem.Text = "Ayarlar / Settings";
			// 
			// stayOnTopToolStripMenuItem
			// 
			stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
			stayOnTopToolStripMenuItem.Size = new Size(217, 22);
			stayOnTopToolStripMenuItem.Text = "Tepede Kalsın / Stay on Top";
			stayOnTopToolStripMenuItem.Click += stayOnTopToolStripMenuItem_Click;
			// 
			// uygulamalarToolStripMenuItem
			// 
			uygulamalarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { yeniUygulamaEkleToolStripMenuItem });
			uygulamalarToolStripMenuItem.Name = "uygulamalarToolStripMenuItem";
			uygulamalarToolStripMenuItem.Size = new Size(124, 20);
			uygulamalarToolStripMenuItem.Text = "Uygulamalar / Apps";
			// 
			// yeniUygulamaEkleToolStripMenuItem
			// 
			yeniUygulamaEkleToolStripMenuItem.Name = "yeniUygulamaEkleToolStripMenuItem";
			yeniUygulamaEkleToolStripMenuItem.Size = new Size(267, 22);
			yeniUygulamaEkleToolStripMenuItem.Text = "Yeni Uygulama Ekle / Add New Apps";
			yeniUygulamaEkleToolStripMenuItem.Click += yeniUygulamaEkleToolStripMenuItem_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = SystemColors.GrayText;
			ClientSize = new Size(435, 766);
			Controls.Add(menuStrip1);
			Controls.Add(dataGridView1);
			ForeColor = SystemColors.ActiveCaptionText;
			FormBorderStyle = FormBorderStyle.FixedToolWindow;
			MainMenuStrip = menuStrip1;
			Name = "Form1";
			SizeGripStyle = SizeGripStyle.Hide;
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Form1";
			Load += Form1_Load;
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private DataGridView dataGridView1;
		private MenuStrip menuStrip1;
		private ToolStripMenuItem uygulamalarToolStripMenuItem;
		private ToolStripMenuItem yeniUygulamaEkleToolStripMenuItem;
		private ToolStripMenuItem ayarlarToolStripMenuItem;
		private ToolStripMenuItem stayOnTopToolStripMenuItem;
	}
}
