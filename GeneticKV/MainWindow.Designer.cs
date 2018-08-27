namespace GeneticKV
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbCross2 = new System.Windows.Forms.RadioButton();
            this.rbCross1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbMut2 = new System.Windows.Forms.RadioButton();
            this.rbMut1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbSel2 = new System.Windows.Forms.RadioButton();
            this.rbSel1 = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbMut3 = new System.Windows.Forms.RadioButton();
            this.rbCross3 = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.rbCross3);
            this.groupBox2.Controls.Add(this.rbCross2);
            this.groupBox2.Controls.Add(this.rbCross1);
            this.groupBox2.Location = new System.Drawing.Point(12, 34);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Size = new System.Drawing.Size(115, 132);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Crossovers:";
            // 
            // rbCross2
            // 
            this.rbCross2.AutoSize = true;
            this.rbCross2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbCross2.Location = new System.Drawing.Point(6, 68);
            this.rbCross2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbCross2.Name = "rbCross2";
            this.rbCross2.Size = new System.Drawing.Size(49, 24);
            this.rbCross2.TabIndex = 1;
            this.rbCross2.TabStop = true;
            this.rbCross2.Text = "CX";
            this.rbCross2.UseVisualStyleBackColor = true;
            // 
            // rbCross1
            // 
            this.rbCross1.AutoSize = true;
            this.rbCross1.Checked = true;
            this.rbCross1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbCross1.Location = new System.Drawing.Point(6, 34);
            this.rbCross1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbCross1.Name = "rbCross1";
            this.rbCross1.Size = new System.Drawing.Size(50, 24);
            this.rbCross1.TabIndex = 0;
            this.rbCross1.TabStop = true;
            this.rbCross1.Text = "OX";
            this.rbCross1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbMut3);
            this.groupBox3.Controls.Add(this.rbMut2);
            this.groupBox3.Controls.Add(this.rbMut1);
            this.groupBox3.Location = new System.Drawing.Point(146, 34);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Size = new System.Drawing.Size(115, 132);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mutations:";
            // 
            // rbMut2
            // 
            this.rbMut2.AutoSize = true;
            this.rbMut2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbMut2.Location = new System.Drawing.Point(4, 66);
            this.rbMut2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbMut2.Name = "rbMut2";
            this.rbMut2.Size = new System.Drawing.Size(91, 24);
            this.rbMut2.TabIndex = 1;
            this.rbMut2.Text = "Inversion";
            this.rbMut2.UseVisualStyleBackColor = true;
            // 
            // rbMut1
            // 
            this.rbMut1.AutoSize = true;
            this.rbMut1.Checked = true;
            this.rbMut1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbMut1.Location = new System.Drawing.Point(4, 32);
            this.rbMut1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbMut1.Name = "rbMut1";
            this.rbMut1.Size = new System.Drawing.Size(90, 24);
            this.rbMut1.TabIndex = 0;
            this.rbMut1.TabStop = true;
            this.rbMut1.Text = "Saltation";
            this.rbMut1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbSel2);
            this.groupBox4.Controls.Add(this.rbSel1);
            this.groupBox4.Location = new System.Drawing.Point(277, 34);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox4.Size = new System.Drawing.Size(131, 132);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Selections:";
            // 
            // rbSel2
            // 
            this.rbSel2.AutoSize = true;
            this.rbSel2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbSel2.Location = new System.Drawing.Point(4, 66);
            this.rbSel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbSel2.Name = "rbSel2";
            this.rbSel2.Size = new System.Drawing.Size(129, 24);
            this.rbSel2.TabIndex = 1;
            this.rbSel2.TabStop = true;
            this.rbSel2.Text = "B-Tournament";
            this.rbSel2.UseVisualStyleBackColor = true;
            // 
            // rbSel1
            // 
            this.rbSel1.AutoSize = true;
            this.rbSel1.Checked = true;
            this.rbSel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbSel1.Location = new System.Drawing.Point(4, 32);
            this.rbSel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbSel1.Name = "rbSel1";
            this.rbSel1.Size = new System.Drawing.Size(88, 24);
            this.rbSel1.TabIndex = 0;
            this.rbSel1.TabStop = true;
            this.rbSel1.Text = "Roulette";
            this.rbSel1.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnStart.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnStart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnStart.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnStart.Location = new System.Drawing.Point(330, 427);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(144, 39);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "StartAlgorithm";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.AllowDrop = true;
            this.tbFileName.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.tbFileName.CausesValidation = false;
            this.tbFileName.ForeColor = System.Drawing.SystemColors.Window;
            this.tbFileName.ImeMode = System.Windows.Forms.ImeMode.Close;
            this.tbFileName.Location = new System.Drawing.Point(12, 433);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(177, 26);
            this.tbFileName.TabIndex = 8;
            this.tbFileName.Text = "Enter matrix file name.";
            this.tbFileName.Click += new System.EventHandler(this.tbFileName_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Settings:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.textBox1.Location = new System.Drawing.Point(89, 401);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 10;
            // 
            // rbMut3
            // 
            this.rbMut3.AutoSize = true;
            this.rbMut3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbMut3.Location = new System.Drawing.Point(4, 100);
            this.rbMut3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbMut3.Name = "rbMut3";
            this.rbMut3.Size = new System.Drawing.Size(63, 24);
            this.rbMut3.TabIndex = 2;
            this.rbMut3.Text = "Point";
            this.rbMut3.UseVisualStyleBackColor = true;
            // 
            // rbCross3
            // 
            this.rbCross3.AutoSize = true;
            this.rbCross3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbCross3.Location = new System.Drawing.Point(6, 100);
            this.rbCross3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.rbCross3.Name = "rbCross3";
            this.rbCross3.Size = new System.Drawing.Size(61, 24);
            this.rbCross3.TabIndex = 2;
            this.rbCross3.TabStop = true;
            this.rbCross3.Text = "PMX";
            this.rbCross3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "Form1";
            this.Text = "Evolutionary-genetic algorithm -- the traveling salesman problem\n.";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCross2;
        private System.Windows.Forms.RadioButton rbCross1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbMut2;
        private System.Windows.Forms.RadioButton rbMut1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rbSel2;
        private System.Windows.Forms.RadioButton rbSel1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton rbCross3;
        private System.Windows.Forms.RadioButton rbMut3;
    }
}

