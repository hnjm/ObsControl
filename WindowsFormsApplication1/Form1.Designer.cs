namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.udHA_hours = new System.Windows.Forms.NumericUpDown();
            this.udDec = new System.Windows.Forms.NumericUpDown();
            this.X = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.udHA_mins = new System.Windows.Forms.NumericUpDown();
            this.udSkewX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCameraPos_Z = new System.Windows.Forms.TextBox();
            this.txtCameraPos_Y = new System.Windows.Forms.TextBox();
            this.txtCameraPos_X = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.udHA_hours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHA_mins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSkewX)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(35, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 200);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // udHA_hours
            // 
            this.udHA_hours.Location = new System.Drawing.Point(401, 38);
            this.udHA_hours.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.udHA_hours.Name = "udHA_hours";
            this.udHA_hours.Size = new System.Drawing.Size(40, 20);
            this.udHA_hours.TabIndex = 1;
            // 
            // udDec
            // 
            this.udDec.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udDec.Location = new System.Drawing.Point(447, 64);
            this.udDec.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.udDec.Name = "udDec";
            this.udDec.Size = new System.Drawing.Size(40, 20);
            this.udDec.TabIndex = 1;
            this.udDec.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // X
            // 
            this.X.AutoSize = true;
            this.X.Location = new System.Drawing.Point(368, 40);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(22, 13);
            this.X.TabIndex = 2;
            this.X.Text = "HA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dec";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(407, 114);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(75, 23);
            this.btnDraw.TabIndex = 3;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(325, 282);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(33, 20);
            this.txtX1.TabIndex = 4;
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(364, 282);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(33, 20);
            this.txtY1.TabIndex = 4;
            // 
            // txtX2
            // 
            this.txtX2.Location = new System.Drawing.Point(426, 282);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(33, 20);
            this.txtX2.TabIndex = 4;
            // 
            // txtY2
            // 
            this.txtY2.Location = new System.Drawing.Point(465, 282);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(33, 20);
            this.txtY2.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // udHA_mins
            // 
            this.udHA_mins.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udHA_mins.Location = new System.Drawing.Point(447, 38);
            this.udHA_mins.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.udHA_mins.Name = "udHA_mins";
            this.udHA_mins.Size = new System.Drawing.Size(40, 20);
            this.udHA_mins.TabIndex = 1;
            // 
            // udSkewX
            // 
            this.udSkewX.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udSkewX.Location = new System.Drawing.Point(442, 153);
            this.udSkewX.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.udSkewX.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.udSkewX.Name = "udSkewX";
            this.udSkewX.Size = new System.Drawing.Size(40, 20);
            this.udSkewX.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Camera";
            // 
            // txtCameraPos_Z
            // 
            this.txtCameraPos_Z.Location = new System.Drawing.Point(233, 239);
            this.txtCameraPos_Z.Name = "txtCameraPos_Z";
            this.txtCameraPos_Z.Size = new System.Drawing.Size(69, 20);
            this.txtCameraPos_Z.TabIndex = 10;
            this.txtCameraPos_Z.Text = "-1000";
            // 
            // txtCameraPos_Y
            // 
            this.txtCameraPos_Y.Location = new System.Drawing.Point(158, 239);
            this.txtCameraPos_Y.Name = "txtCameraPos_Y";
            this.txtCameraPos_Y.Size = new System.Drawing.Size(69, 20);
            this.txtCameraPos_Y.TabIndex = 9;
            this.txtCameraPos_Y.Text = "100";
            // 
            // txtCameraPos_X
            // 
            this.txtCameraPos_X.Location = new System.Drawing.Point(83, 239);
            this.txtCameraPos_X.Name = "txtCameraPos_X";
            this.txtCameraPos_X.Size = new System.Drawing.Size(69, 20);
            this.txtCameraPos_X.TabIndex = 8;
            this.txtCameraPos_X.Text = "100";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 305);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCameraPos_Z);
            this.Controls.Add(this.txtCameraPos_Y);
            this.Controls.Add(this.txtCameraPos_X);
            this.Controls.Add(this.txtY2);
            this.Controls.Add(this.txtY1);
            this.Controls.Add(this.txtX2);
            this.Controls.Add(this.txtX1);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.X);
            this.Controls.Add(this.udDec);
            this.Controls.Add(this.udSkewX);
            this.Controls.Add(this.udHA_mins);
            this.Controls.Add(this.udHA_hours);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.udHA_hours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udDec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udHA_mins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udSkewX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown udHA_hours;
        private System.Windows.Forms.NumericUpDown udDec;
        private System.Windows.Forms.Label X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.TextBox txtY2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown udHA_mins;
        private System.Windows.Forms.NumericUpDown udSkewX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCameraPos_Z;
        private System.Windows.Forms.TextBox txtCameraPos_Y;
        private System.Windows.Forms.TextBox txtCameraPos_X;
    }
}

