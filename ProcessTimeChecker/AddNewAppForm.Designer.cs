namespace ProcessTimeChecker
{
	partial class AddNewAppForm
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
			textBox1 = new TextBox();
			listView1 = new ListView();
			button1 = new Button();
			button2 = new Button();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Location = new Point(207, 12);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(125, 59);
			textBox1.TabIndex = 0;
			// 
			// listView1
			// 
			listView1.GridLines = true;
			listView1.LabelWrap = false;
			listView1.Location = new Point(12, 77);
			listView1.Name = "listView1";
			listView1.Size = new Size(501, 196);
			listView1.TabIndex = 1;
			listView1.UseCompatibleStateImageBehavior = false;
			listView1.View = View.List;
			listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
			listView1.KeyDown += listView1_KeyDown;
			// 
			// button1
			// 
			button1.Location = new Point(335, 11);
			button1.Name = "button1";
			button1.Size = new Size(86, 60);
			button1.TabIndex = 2;
			button1.Text = "Yeni Uygulama Ekle";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(106, 12);
			button2.Name = "button2";
			button2.Size = new Size(86, 59);
			button2.TabIndex = 3;
			button2.Text = "Dosyayı Temizle";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// AddNewAppForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(525, 285);
			Controls.Add(button2);
			Controls.Add(button1);
			Controls.Add(listView1);
			Controls.Add(textBox1);
			Name = "AddNewAppForm";
			Text = "AddNewAppForm";
			Load += AddNewAppForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
		private ListView listView1;
		private Button button1;
		private Button button2;
	}
}