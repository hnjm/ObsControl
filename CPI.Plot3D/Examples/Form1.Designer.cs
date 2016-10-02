namespace Examples
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartPos_Z = new System.Windows.Forms.TextBox();
            this.txtCameraPos_Z = new System.Windows.Forms.TextBox();
            this.txtStartPos_Y = new System.Windows.Forms.TextBox();
            this.txtCameraPos_Y = new System.Windows.Forms.TextBox();
            this.txtStartPos_X = new System.Windows.Forms.TextBox();
            this.txtCameraPos_X = new System.Windows.Forms.TextBox();
            this.btnRotateAroundCenterRight = new System.Windows.Forms.Button();
            this.btnMultiAxisRotate = new System.Windows.Forms.Button();
            this.btnDrawCube = new System.Windows.Forms.Button();
            this.btnRotateCubeAroundCenterDown = new System.Windows.Forms.Button();
            this.btnRotateVertical = new System.Windows.Forms.Button();
            this.btnRotateHoriz = new System.Windows.Forms.Button();
            this.btnRotateCubeAroundEdge = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRotateSquareAroundCenter = new System.Windows.Forms.Button();
            this.btnDrawSquare = new System.Windows.Forms.Button();
            this.btnRotateSquareAroundEdge = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnSphereMultiAxisRotate = new System.Windows.Forms.Button();
            this.btnRotateSphereAroundCenterRight = new System.Windows.Forms.Button();
            this.btnRotateSphereAroundCenterDown = new System.Windows.Forms.Button();
            this.btnDrawSphere = new System.Windows.Forms.Button();
            this.btnDrawCircle = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDominoKnockDownZ = new System.Windows.Forms.Button();
            this.btnDominoSetUpZ = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDominoKnockDownX = new System.Windows.Forms.Button();
            this.btnDominoSetUpX = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 305);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(987, 123);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtStartPos_Z);
            this.tabPage2.Controls.Add(this.txtCameraPos_Z);
            this.tabPage2.Controls.Add(this.txtStartPos_Y);
            this.tabPage2.Controls.Add(this.txtCameraPos_Y);
            this.tabPage2.Controls.Add(this.txtStartPos_X);
            this.tabPage2.Controls.Add(this.txtCameraPos_X);
            this.tabPage2.Controls.Add(this.btnRotateAroundCenterRight);
            this.tabPage2.Controls.Add(this.btnMultiAxisRotate);
            this.tabPage2.Controls.Add(this.btnDrawCube);
            this.tabPage2.Controls.Add(this.btnRotateCubeAroundCenterDown);
            this.tabPage2.Controls.Add(this.btnRotateVertical);
            this.tabPage2.Controls.Add(this.btnRotateHoriz);
            this.tabPage2.Controls.Add(this.btnRotateCubeAroundEdge);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(979, 97);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cube";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Center";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Camera";
            // 
            // txtStartPos_Z
            // 
            this.txtStartPos_Z.Location = new System.Drawing.Point(539, 51);
            this.txtStartPos_Z.Name = "txtStartPos_Z";
            this.txtStartPos_Z.Size = new System.Drawing.Size(69, 20);
            this.txtStartPos_Z.TabIndex = 5;
            this.txtStartPos_Z.Text = "0";
            // 
            // txtCameraPos_Z
            // 
            this.txtCameraPos_Z.Location = new System.Drawing.Point(539, 21);
            this.txtCameraPos_Z.Name = "txtCameraPos_Z";
            this.txtCameraPos_Z.Size = new System.Drawing.Size(69, 20);
            this.txtCameraPos_Z.TabIndex = 2;
            this.txtCameraPos_Z.Text = "-1000";
            // 
            // txtStartPos_Y
            // 
            this.txtStartPos_Y.Location = new System.Drawing.Point(464, 51);
            this.txtStartPos_Y.Name = "txtStartPos_Y";
            this.txtStartPos_Y.Size = new System.Drawing.Size(69, 20);
            this.txtStartPos_Y.TabIndex = 4;
            this.txtStartPos_Y.Text = "150";
            // 
            // txtCameraPos_Y
            // 
            this.txtCameraPos_Y.Location = new System.Drawing.Point(464, 21);
            this.txtCameraPos_Y.Name = "txtCameraPos_Y";
            this.txtCameraPos_Y.Size = new System.Drawing.Size(69, 20);
            this.txtCameraPos_Y.TabIndex = 1;
            this.txtCameraPos_Y.Text = "100";
            // 
            // txtStartPos_X
            // 
            this.txtStartPos_X.Location = new System.Drawing.Point(389, 51);
            this.txtStartPos_X.Name = "txtStartPos_X";
            this.txtStartPos_X.Size = new System.Drawing.Size(69, 20);
            this.txtStartPos_X.TabIndex = 3;
            this.txtStartPos_X.Text = "150";
            // 
            // txtCameraPos_X
            // 
            this.txtCameraPos_X.Location = new System.Drawing.Point(389, 21);
            this.txtCameraPos_X.Name = "txtCameraPos_X";
            this.txtCameraPos_X.Size = new System.Drawing.Size(69, 20);
            this.txtCameraPos_X.TabIndex = 0;
            this.txtCameraPos_X.Text = "100";
            // 
            // btnRotateAroundCenterRight
            // 
            this.btnRotateAroundCenterRight.Location = new System.Drawing.Point(166, 35);
            this.btnRotateAroundCenterRight.Name = "btnRotateAroundCenterRight";
            this.btnRotateAroundCenterRight.Size = new System.Drawing.Size(152, 23);
            this.btnRotateAroundCenterRight.TabIndex = 5;
            this.btnRotateAroundCenterRight.Text = "Rotate Around Center Right";
            this.btnRotateAroundCenterRight.UseVisualStyleBackColor = true;
            this.btnRotateAroundCenterRight.Click += new System.EventHandler(this.btnRotateAroundCenterRight_Click);
            // 
            // btnMultiAxisRotate
            // 
            this.btnMultiAxisRotate.Location = new System.Drawing.Point(166, 64);
            this.btnMultiAxisRotate.Name = "btnMultiAxisRotate";
            this.btnMultiAxisRotate.Size = new System.Drawing.Size(152, 23);
            this.btnMultiAxisRotate.TabIndex = 4;
            this.btnMultiAxisRotate.Text = "Multi-Axis Rotate";
            this.btnMultiAxisRotate.UseVisualStyleBackColor = true;
            this.btnMultiAxisRotate.Click += new System.EventHandler(this.btnMultiAxisRotate_Click);
            // 
            // btnDrawCube
            // 
            this.btnDrawCube.Location = new System.Drawing.Point(8, 6);
            this.btnDrawCube.Name = "btnDrawCube";
            this.btnDrawCube.Size = new System.Drawing.Size(75, 23);
            this.btnDrawCube.TabIndex = 0;
            this.btnDrawCube.Text = "Draw";
            this.btnDrawCube.UseVisualStyleBackColor = true;
            this.btnDrawCube.Click += new System.EventHandler(this.btnDrawCube_Click);
            // 
            // btnRotateCubeAroundCenterDown
            // 
            this.btnRotateCubeAroundCenterDown.Location = new System.Drawing.Point(8, 64);
            this.btnRotateCubeAroundCenterDown.Name = "btnRotateCubeAroundCenterDown";
            this.btnRotateCubeAroundCenterDown.Size = new System.Drawing.Size(152, 23);
            this.btnRotateCubeAroundCenterDown.TabIndex = 3;
            this.btnRotateCubeAroundCenterDown.Text = "Rotate Around Center Down";
            this.btnRotateCubeAroundCenterDown.UseVisualStyleBackColor = true;
            this.btnRotateCubeAroundCenterDown.Click += new System.EventHandler(this.btnRotateCubeAroundCenter_Click);
            // 
            // btnRotateVertical
            // 
            this.btnRotateVertical.Location = new System.Drawing.Point(211, 6);
            this.btnRotateVertical.Name = "btnRotateVertical";
            this.btnRotateVertical.Size = new System.Drawing.Size(107, 23);
            this.btnRotateVertical.TabIndex = 2;
            this.btnRotateVertical.Text = "Rotate Vertical";
            this.btnRotateVertical.UseVisualStyleBackColor = true;
            this.btnRotateVertical.Click += new System.EventHandler(this.btnRotateVertical_Click);
            // 
            // btnRotateHoriz
            // 
            this.btnRotateHoriz.Location = new System.Drawing.Point(105, 6);
            this.btnRotateHoriz.Name = "btnRotateHoriz";
            this.btnRotateHoriz.Size = new System.Drawing.Size(100, 23);
            this.btnRotateHoriz.TabIndex = 2;
            this.btnRotateHoriz.Text = "Rotate Horizontal";
            this.btnRotateHoriz.UseVisualStyleBackColor = true;
            this.btnRotateHoriz.Click += new System.EventHandler(this.btnRotateHorizontal_Click);
            // 
            // btnRotateCubeAroundEdge
            // 
            this.btnRotateCubeAroundEdge.Location = new System.Drawing.Point(8, 35);
            this.btnRotateCubeAroundEdge.Name = "btnRotateCubeAroundEdge";
            this.btnRotateCubeAroundEdge.Size = new System.Drawing.Size(152, 23);
            this.btnRotateCubeAroundEdge.TabIndex = 2;
            this.btnRotateCubeAroundEdge.Text = "Rotate Around Edge";
            this.btnRotateCubeAroundEdge.UseVisualStyleBackColor = true;
            this.btnRotateCubeAroundEdge.Click += new System.EventHandler(this.btnRotateCubeAroundEdge_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRotateSquareAroundCenter);
            this.tabPage1.Controls.Add(this.btnDrawSquare);
            this.tabPage1.Controls.Add(this.btnRotateSquareAroundEdge);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(979, 97);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Square";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnRotateSquareAroundCenter
            // 
            this.btnRotateSquareAroundCenter.Location = new System.Drawing.Point(222, 6);
            this.btnRotateSquareAroundCenter.Name = "btnRotateSquareAroundCenter";
            this.btnRotateSquareAroundCenter.Size = new System.Drawing.Size(129, 23);
            this.btnRotateSquareAroundCenter.TabIndex = 3;
            this.btnRotateSquareAroundCenter.Text = "Rotate Around Center";
            this.btnRotateSquareAroundCenter.UseVisualStyleBackColor = true;
            this.btnRotateSquareAroundCenter.Click += new System.EventHandler(this.btnRotateSquareAroundCenter_Click);
            // 
            // btnDrawSquare
            // 
            this.btnDrawSquare.Location = new System.Drawing.Point(6, 6);
            this.btnDrawSquare.Name = "btnDrawSquare";
            this.btnDrawSquare.Size = new System.Drawing.Size(75, 23);
            this.btnDrawSquare.TabIndex = 0;
            this.btnDrawSquare.Text = "Draw";
            this.btnDrawSquare.UseVisualStyleBackColor = true;
            this.btnDrawSquare.Click += new System.EventHandler(this.btnDrawSquare_Click);
            // 
            // btnRotateSquareAroundEdge
            // 
            this.btnRotateSquareAroundEdge.Location = new System.Drawing.Point(87, 6);
            this.btnRotateSquareAroundEdge.Name = "btnRotateSquareAroundEdge";
            this.btnRotateSquareAroundEdge.Size = new System.Drawing.Size(129, 23);
            this.btnRotateSquareAroundEdge.TabIndex = 2;
            this.btnRotateSquareAroundEdge.Text = "Rotate Around Edge";
            this.btnRotateSquareAroundEdge.UseVisualStyleBackColor = true;
            this.btnRotateSquareAroundEdge.Click += new System.EventHandler(this.btnRotateSquareAroundEdge_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnSphereMultiAxisRotate);
            this.tabPage3.Controls.Add(this.btnRotateSphereAroundCenterRight);
            this.tabPage3.Controls.Add(this.btnRotateSphereAroundCenterDown);
            this.tabPage3.Controls.Add(this.btnDrawSphere);
            this.tabPage3.Controls.Add(this.btnDrawCircle);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(979, 97);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Circle / Sphere";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnSphereMultiAxisRotate
            // 
            this.btnSphereMultiAxisRotate.Location = new System.Drawing.Point(110, 64);
            this.btnSphereMultiAxisRotate.Name = "btnSphereMultiAxisRotate";
            this.btnSphereMultiAxisRotate.Size = new System.Drawing.Size(203, 23);
            this.btnSphereMultiAxisRotate.TabIndex = 4;
            this.btnSphereMultiAxisRotate.Text = "Sphere Multi-Axis Rotate";
            this.btnSphereMultiAxisRotate.UseVisualStyleBackColor = true;
            this.btnSphereMultiAxisRotate.Click += new System.EventHandler(this.btnSphereMultiAxisRotate_Click);
            // 
            // btnRotateSphereAroundCenterRight
            // 
            this.btnRotateSphereAroundCenterRight.Location = new System.Drawing.Point(110, 35);
            this.btnRotateSphereAroundCenterRight.Name = "btnRotateSphereAroundCenterRight";
            this.btnRotateSphereAroundCenterRight.Size = new System.Drawing.Size(203, 23);
            this.btnRotateSphereAroundCenterRight.TabIndex = 3;
            this.btnRotateSphereAroundCenterRight.Text = "Rotate Sphere Around Center Right";
            this.btnRotateSphereAroundCenterRight.UseVisualStyleBackColor = true;
            this.btnRotateSphereAroundCenterRight.Click += new System.EventHandler(this.btnRotateSphereAroundCenterRight_Click);
            // 
            // btnRotateSphereAroundCenterDown
            // 
            this.btnRotateSphereAroundCenterDown.Location = new System.Drawing.Point(110, 6);
            this.btnRotateSphereAroundCenterDown.Name = "btnRotateSphereAroundCenterDown";
            this.btnRotateSphereAroundCenterDown.Size = new System.Drawing.Size(203, 23);
            this.btnRotateSphereAroundCenterDown.TabIndex = 2;
            this.btnRotateSphereAroundCenterDown.Text = "Rotate Sphere Around Center Down";
            this.btnRotateSphereAroundCenterDown.UseVisualStyleBackColor = true;
            this.btnRotateSphereAroundCenterDown.Click += new System.EventHandler(this.btnRotateSphereAroundCenterDown_Click);
            // 
            // btnDrawSphere
            // 
            this.btnDrawSphere.Location = new System.Drawing.Point(6, 35);
            this.btnDrawSphere.Name = "btnDrawSphere";
            this.btnDrawSphere.Size = new System.Drawing.Size(98, 23);
            this.btnDrawSphere.TabIndex = 1;
            this.btnDrawSphere.Text = "Draw Sphere";
            this.btnDrawSphere.UseVisualStyleBackColor = true;
            this.btnDrawSphere.Click += new System.EventHandler(this.btnDrawSphere_Click);
            // 
            // btnDrawCircle
            // 
            this.btnDrawCircle.Location = new System.Drawing.Point(6, 6);
            this.btnDrawCircle.Name = "btnDrawCircle";
            this.btnDrawCircle.Size = new System.Drawing.Size(98, 23);
            this.btnDrawCircle.TabIndex = 0;
            this.btnDrawCircle.Text = "Draw Circle";
            this.btnDrawCircle.UseVisualStyleBackColor = true;
            this.btnDrawCircle.Click += new System.EventHandler(this.btnDrawCircle_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(979, 97);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Dominoes";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDominoKnockDownZ);
            this.groupBox2.Controls.Add(this.btnDominoSetUpZ);
            this.groupBox2.Location = new System.Drawing.Point(128, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(116, 85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Z Axis";
            // 
            // btnDominoKnockDownZ
            // 
            this.btnDominoKnockDownZ.Location = new System.Drawing.Point(6, 48);
            this.btnDominoKnockDownZ.Name = "btnDominoKnockDownZ";
            this.btnDominoKnockDownZ.Size = new System.Drawing.Size(97, 23);
            this.btnDominoKnockDownZ.TabIndex = 4;
            this.btnDominoKnockDownZ.Text = "Knock \'em Down";
            this.btnDominoKnockDownZ.UseVisualStyleBackColor = true;
            this.btnDominoKnockDownZ.Click += new System.EventHandler(this.btnDominoKnockDownZ_Click);
            // 
            // btnDominoSetUpZ
            // 
            this.btnDominoSetUpZ.Location = new System.Drawing.Point(6, 19);
            this.btnDominoSetUpZ.Name = "btnDominoSetUpZ";
            this.btnDominoSetUpZ.Size = new System.Drawing.Size(97, 23);
            this.btnDominoSetUpZ.TabIndex = 3;
            this.btnDominoSetUpZ.Text = "Set \'em Up";
            this.btnDominoSetUpZ.UseVisualStyleBackColor = true;
            this.btnDominoSetUpZ.Click += new System.EventHandler(this.btnDominoSetUpZ_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDominoKnockDownX);
            this.groupBox1.Controls.Add(this.btnDominoSetUpX);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(116, 85);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "X Axis";
            // 
            // btnDominoKnockDownX
            // 
            this.btnDominoKnockDownX.Location = new System.Drawing.Point(6, 48);
            this.btnDominoKnockDownX.Name = "btnDominoKnockDownX";
            this.btnDominoKnockDownX.Size = new System.Drawing.Size(97, 23);
            this.btnDominoKnockDownX.TabIndex = 4;
            this.btnDominoKnockDownX.Text = "Knock \'em Down";
            this.btnDominoKnockDownX.UseVisualStyleBackColor = true;
            this.btnDominoKnockDownX.Click += new System.EventHandler(this.btnDominoKnockDownX_Click);
            // 
            // btnDominoSetUpX
            // 
            this.btnDominoSetUpX.Location = new System.Drawing.Point(6, 19);
            this.btnDominoSetUpX.Name = "btnDominoSetUpX";
            this.btnDominoSetUpX.Size = new System.Drawing.Size(97, 23);
            this.btnDominoSetUpX.TabIndex = 3;
            this.btnDominoSetUpX.Text = "Set \'em Up";
            this.btnDominoSetUpX.UseVisualStyleBackColor = true;
            this.btnDominoSetUpX.Click += new System.EventHandler(this.btnDominoSetUpX_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 428);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnRotateSquareAroundCenter;
        private System.Windows.Forms.Button btnDrawSquare;
        private System.Windows.Forms.Button btnRotateSquareAroundEdge;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnRotateAroundCenterRight;
        private System.Windows.Forms.Button btnMultiAxisRotate;
        private System.Windows.Forms.Button btnDrawCube;
        private System.Windows.Forms.Button btnRotateCubeAroundCenterDown;
        private System.Windows.Forms.Button btnRotateCubeAroundEdge;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnSphereMultiAxisRotate;
        private System.Windows.Forms.Button btnRotateSphereAroundCenterRight;
        private System.Windows.Forms.Button btnRotateSphereAroundCenterDown;
        private System.Windows.Forms.Button btnDrawSphere;
        private System.Windows.Forms.Button btnDrawCircle;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDominoKnockDownZ;
        private System.Windows.Forms.Button btnDominoSetUpZ;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDominoKnockDownX;
        private System.Windows.Forms.Button btnDominoSetUpX;
        private System.Windows.Forms.TextBox txtCameraPos_Z;
        private System.Windows.Forms.TextBox txtCameraPos_Y;
        private System.Windows.Forms.TextBox txtCameraPos_X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStartPos_Z;
        private System.Windows.Forms.TextBox txtStartPos_Y;
        private System.Windows.Forms.TextBox txtStartPos_X;
        private System.Windows.Forms.Button btnRotateHoriz;
        private System.Windows.Forms.Button btnRotateVertical;
    }
}

