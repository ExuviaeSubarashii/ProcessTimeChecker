﻿namespace ProcessTimeChecker
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
		 ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
		 SuspendLayout();
		 // 
		 // dataGridView1
		 // 
		 dataGridView1.BackgroundColor = SystemColors.ActiveBorder;
		 dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		 dataGridView1.Location = new Point(0, 0);
		 dataGridView1.Name = "dataGridView1";
		 dataGridView1.Size = new Size(455, 771);
		 dataGridView1.TabIndex = 0;
		 // 
		 // Form1
		 // 
		 AutoScaleDimensions = new SizeF(7F, 15F);
		 AutoScaleMode = AutoScaleMode.Font;
		 BackColor = SystemColors.GrayText;
		 ClientSize = new Size(448, 766);
		 Controls.Add(dataGridView1);
		 ForeColor = SystemColors.ActiveCaptionText;
		 FormBorderStyle = FormBorderStyle.FixedToolWindow;
		 Name = "Form1";
		 SizeGripStyle = SizeGripStyle.Hide;
		 StartPosition = FormStartPosition.CenterParent;
		 Text = "Form1";
		 Load += Form1_Load;
		 ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
		 ResumeLayout(false);
	  }

	  #endregion

	  private DataGridView dataGridView1;
   }
}
