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
			label1 = new Label();
			label2 = new Label();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Location = new Point(6, 148);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(125, 59);
			textBox1.TabIndex = 0;
			// 
			// listView1
			// 
			listView1.GridLines = true;
			listView1.LabelWrap = false;
			listView1.Location = new Point(213, 12);
			listView1.Name = "listView1";
			listView1.Size = new Size(300, 261);
			listView1.TabIndex = 1;
			listView1.UseCompatibleStateImageBehavior = false;
			listView1.View = View.List;
			listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
			listView1.KeyDown += listView1_KeyDown;
			// 
			// button1
			// 
			button1.Location = new Point(7, 213);
			button1.Name = "button1";
			button1.Size = new Size(124, 60);
			button1.TabIndex = 2;
			button1.Text = "Yeni Uygulama Ekle / Add New App";
			button1.UseVisualStyleBackColor = true;
			button1.Click += button1_Click;
			// 
			// button2
			// 
			button2.Location = new Point(6, 83);
			button2.Name = "button2";
			button2.Size = new Size(125, 59);
			button2.TabIndex = 3;
			button2.Text = "Dosyayı Temizle / Clear File";
			button2.UseVisualStyleBackColor = true;
			button2.Click += button2_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(7, 19);
			label1.Name = "label1";
			label1.Size = new Size(199, 15);
			label1.TabIndex = 4;
			label1.Text = "Uygulamaya tıkla ve DEL tuşuna bas.";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(6, 40);
			label2.Name = "label2";
			label2.Size = new Size(198, 15);
			label2.TabIndex = 5;
			label2.Text = "Click on the app and hit DEL button.";
			// 
			// AddNewAppForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(525, 285);
			Controls.Add(label2);
			Controls.Add(label1);
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
		private Label label1;
		private Label label2;
	}
}