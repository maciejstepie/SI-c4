namespace DaneZPlikuOkienko
{
    partial class DaneZPliku
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
            this.btnWybierzParagon = new System.Windows.Forms.Button();
            this.tbSciezkaDoParagonu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbParagon = new System.Windows.Forms.TextBox();
            this.tbWyniki = new System.Windows.Forms.TextBox();
            this.progCzestosci = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbWarunek = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.progCzestosci)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWybierzParagon
            // 
            this.btnWybierzParagon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWybierzParagon.Location = new System.Drawing.Point(1119, 10);
            this.btnWybierzParagon.Margin = new System.Windows.Forms.Padding(2);
            this.btnWybierzParagon.Name = "btnWybierzParagon";
            this.btnWybierzParagon.Size = new System.Drawing.Size(32, 19);
            this.btnWybierzParagon.TabIndex = 0;
            this.btnWybierzParagon.Text = "...";
            this.btnWybierzParagon.UseVisualStyleBackColor = true;
            this.btnWybierzParagon.Click += new System.EventHandler(this.btnWybierzParagon_Click);
            // 
            // tbSciezkaDoParagonu
            // 
            this.tbSciezkaDoParagonu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSciezkaDoParagonu.Location = new System.Drawing.Point(134, 10);
            this.tbSciezkaDoParagonu.Margin = new System.Windows.Forms.Padding(2);
            this.tbSciezkaDoParagonu.Name = "tbSciezkaDoParagonu";
            this.tbSciezkaDoParagonu.ReadOnly = true;
            this.tbSciezkaDoParagonu.Size = new System.Drawing.Size(983, 20);
            this.tbSciezkaDoParagonu.TabIndex = 1;
            this.tbSciezkaDoParagonu.Click += new System.EventHandler(this.btnWybierzParagon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ścieżka do paragonu";
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStart.Location = new System.Drawing.Point(528, 377);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(104, 35);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Pracuj";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbParagon
            // 
            this.tbParagon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbParagon.Location = new System.Drawing.Point(9, 51);
            this.tbParagon.Margin = new System.Windows.Forms.Padding(2);
            this.tbParagon.Multiline = true;
            this.tbParagon.Name = "tbParagon";
            this.tbParagon.Size = new System.Drawing.Size(300, 302);
            this.tbParagon.TabIndex = 3;
            // 
            // tbWyniki
            // 
            this.tbWyniki.Location = new System.Drawing.Point(314, 51);
            this.tbWyniki.Multiline = true;
            this.tbWyniki.Name = "tbWyniki";
            this.tbWyniki.ReadOnly = true;
            this.tbWyniki.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbWyniki.Size = new System.Drawing.Size(834, 302);
            this.tbWyniki.TabIndex = 10;
            // 
            // progCzestosci
            // 
            this.progCzestosci.Location = new System.Drawing.Point(6, 21);
            this.progCzestosci.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.progCzestosci.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.progCzestosci.Name = "progCzestosci";
            this.progCzestosci.Size = new System.Drawing.Size(99, 22);
            this.progCzestosci.TabIndex = 12;
            this.progCzestosci.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progCzestosci);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 358);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(113, 55);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prog Czestosci";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbWarunek);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(134, 359);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(175, 54);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Warunek reguły ≥";
            // 
            // cbWarunek
            // 
            this.cbWarunek.FormattingEnabled = true;
            this.cbWarunek.Items.AddRange(new object[] {
            "1/3",
            "1/10",
            "2/10",
            "3/10",
            "4/10"});
            this.cbWarunek.Location = new System.Drawing.Point(6, 20);
            this.cbWarunek.Name = "cbWarunek";
            this.cbWarunek.Size = new System.Drawing.Size(163, 24);
            this.cbWarunek.TabIndex = 0;
            // 
            // DaneZPliku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1160, 425);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbWyniki);
            this.Controls.Add(this.tbParagon);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSciezkaDoParagonu);
            this.Controls.Add(this.btnWybierzParagon);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(529, 413);
            this.Name = "DaneZPliku";
            this.Text = "SI_4";
            ((System.ComponentModel.ISupportInitialize)(this.progCzestosci)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWybierzParagon;
        private System.Windows.Forms.TextBox tbSciezkaDoParagonu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbParagon;
        private System.Windows.Forms.TextBox tbWyniki;
        private System.Windows.Forms.NumericUpDown progCzestosci;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbWarunek;
    }
}

