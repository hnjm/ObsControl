using System;
//using System.Collections.Generic;
using System.ComponentModel;
//using System.Data;
using System.Drawing;
//using System.Text;
using System.Windows.Forms;
using CPI.Plot3D;


namespace Examples
{
    public partial class Form1 : Form
    {
        private double dominoRotationAngle = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnDrawSquare_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g))
            {
                g.Clear(this.BackColor);

                p.Location = new CPI.Plot3D.Point3D(150, 150, 0);
                DrawSquare(p, 50);
            }
        }

        public void DrawSquare(Plotter3D p, float sideLength)
        {

            for (int i = 0; i < 4; i++)
            {
                p.Forward(sideLength);  // Draw a line sideLength long
                p.TurnRight(90);        // Turn right 90 degrees
            }
        }



        private void btnRotateSquareAroundEdge_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = StartLocation;
                        p.TurnRight(angle);

                        DrawSquare(p, 50);
                    }
                }
            }
        }

        private void btnRotateSquareAroundCenter_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = StartLocation;

                        // Move to the center of the square
                        p.PenUp();
                        p.Forward(25);
                        p.TurnRight(90);
                        p.Forward(25);

                        p.TurnRight(angle);

                        // Retrace your steps to move back to the starting point
                        p.TurnRight(180);
                        p.Forward(25);
                        p.TurnLeft(90);
                        p.Forward(25);
                        p.TurnLeft(180);
                        p.PenDown();

                        DrawSquare(p, 50);
                    }
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        public void DrawRect(Plotter3D p, float sideWidth, float sideHeight)
        {
            p.Forward(sideWidth);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees

            p.Forward(sideHeight);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees

            p.Forward(sideWidth);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees

            p.Forward(sideHeight);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees
        }


        public void DrawCube(Plotter3D p, float XWidth, float YHeight, float ZThick)
        {
            /*            for (int i = 0; i < 4; i++)
                        {
                            //DrawSquare(p, sideLength);
                            DrawRect(p, sideLength+10, sideLength-10);
                            p.Forward(sideLength);
                            p.TurnDown(90);
                        }
            */
            DrawRect(p, XWidth, YHeight);

            p.Forward(XWidth);

            p.TurnDown(90);
            DrawRect(p, ZThick, YHeight);

            p.Forward(ZThick);
            p.TurnDown(90);
            DrawRect(p, XWidth, YHeight);

            p.Forward(XWidth);
            p.TurnDown(90);
            DrawRect(p, ZThick, YHeight);
        }
        //////////////////////////////////////////////////////////////////////


        public CPI.Plot3D.Point3D CameraLocatioin;
        public CPI.Plot3D.Point3D StartLocation;

        private void btnDrawCube_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //Camera positon
                CameraLocatioin = new CPI.Plot3D.Point3D(Convert.ToInt32(txtCameraPos_X.Text), Convert.ToInt32(txtCameraPos_Y.Text), Convert.ToInt32(txtCameraPos_Z.Text));
                //Start postion
                StartLocation = new CPI.Plot3D.Point3D(Convert.ToInt32(txtStartPos_X.Text), Convert.ToInt32(txtStartPos_Y.Text), Convert.ToInt32(txtStartPos_Z.Text));


                using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                {
                    System.Threading.Thread.Sleep(50);
                    g.Clear(this.BackColor);

                    //Camera positon
                    p.Location = StartLocation;


                    g.TranslateTransform(StartLocation.X, StartLocation.Y);
                    //1.2. Повернуть на широту
                    g.RotateTransform(45.0F);
                    //1.3. Вернуть начало координат в начало
                    g.TranslateTransform(-StartLocation.X, -StartLocation.Y);

                    DrawCube(p, 50, 100, 50);
                }
            }
        }

        private void btnRotateLine_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 30; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                    {
                        //txtTest.Text = angle.ToString();

                        System.Threading.Thread.Sleep(100);

                        g.Clear(this.BackColor);

                        p.Location = StartLocation;

                        p.TurnDown(angle);

                        p.PenDown();

                        Point3D P_start = p.Location;
                        p.Forward(100);
                        Point3D P_end = p.Location;

                        txtTest.Text = P_start.ToString() + P_end.ToString();
                    }
                }
            }
        }


        private void btnRotateHorizontal_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = StartLocation;

                        // Move to the center of the square
                        p.PenUp();
                        p.Forward(25);
                        p.TurnRight(90);
                        p.Forward(50);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnUp(180);

                        p.TurnDown(angle);

                        // Retrace your steps to move back to the starting point
                        p.Forward(25);
                        p.TurnRight(180);
                        p.TurnUp(90);
                        p.Forward(50);
                        p.TurnLeft(90);
                        p.Forward(25);
                        p.TurnLeft(180);
                        p.PenDown();

                        //p.TurnDown(angle);

                        DrawCube(p, 50, 100, 50);
                    }
                }
            }
        }


        private void btnRotateVertical_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        g.TranslateTransform(StartLocation.X, StartLocation.Y);
                        //1.2. Повернуть на широту
                        g.RotateTransform(45.0F);
                        //1.3. Вернуть начало координат в начало
                        g.TranslateTransform(-StartLocation.X, -StartLocation.Y);

                        //Обозначим цетр
                        g.FillEllipse(new SolidBrush(Color.Red), StartLocation.X - 2, StartLocation.Y - 2, 4, 4);


                        p.Location = StartLocation;

                        // Move to the center of the square
                        p.PenUp();
                        p.Forward(25);
                        p.TurnRight(90);
                        p.Forward(50);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnUp(180);

                        p.TurnRight(angle);

                        // Retrace your steps to move back to the starting point
                        p.Forward(25);
                        p.TurnRight(180);
                        p.TurnUp(90);
                        p.Forward(50);
                        p.TurnLeft(90);
                        p.Forward(25);
                        p.TurnLeft(180);
                        p.PenDown();

                        //p.TurnDown(angle);

                        DrawCube(p, 50, 100, 50);

                        g.TranslateTransform(StartLocation.X, StartLocation.Y);
                        //1.2. Повернуть на широту
                        g.RotateTransform(-45.0F);
                        //1.3. Вернуть начало координат в начало
                        g.TranslateTransform(-StartLocation.X, -StartLocation.Y);

                    }
                }
            }
        }


        private void btnRotateCubeAroundEdge_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = StartLocation;

                        p.TurnRight(angle);

                        DrawCube(p, 50, 100, 50);
                    }
                }
            }

        }



        private void btnRotateCubeAroundCenter_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = StartLocation;

                        // Move to the center of the square
                        p.PenUp();
                        p.Forward(25);
                        p.TurnRight(90);
                        p.Forward(25);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnUp(90);

                        p.TurnDown(angle);

                        // Retrace your steps to move back to the starting point
                        p.TurnDown(90);
                        p.TurnRight(180);
                        p.Forward(25);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnLeft(90);
                        p.Forward(25);
                        p.TurnLeft(180);
                        p.PenDown();

                        DrawCube(p, 50, 50, 50);
                    }
                }
            }
        }

        private void btnMultiAxisRotate_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 6)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = StartLocation;

                        // Move to the center of the square
                        p.PenUp();
                        p.Forward(25);
                        p.TurnRight(90);
                        p.Forward(25);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnUp(90);

                        p.TurnRight(angle);
                        p.TurnDown(angle);

                        // Retrace your steps to move back to the starting point
                        p.TurnDown(90);
                        p.TurnRight(180);
                        p.Forward(25);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnLeft(90);
                        p.Forward(25);
                        p.TurnLeft(180);
                        p.PenDown();

                        DrawCube(p, 50, 100, 50);
                    }
                }
            }
        }

        private void btnRotateAroundCenterRight_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 360; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, CameraLocatioin))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = StartLocation;

                        // Move to the center of the square
                        p.PenUp();
                        p.Forward(25);
                        p.TurnRight(90);
                        p.Forward(25);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnUp(90);

                        p.TurnRight(angle);

                        // Retrace your steps to move back to the starting point
                        p.TurnDown(90);
                        p.TurnRight(180);
                        p.Forward(25);
                        p.TurnDown(90);
                        p.Forward(25);
                        p.TurnLeft(90);
                        p.Forward(25);
                        p.TurnLeft(180);
                        p.PenDown();

                        DrawCube(p, 50, 50, 50);
                    }
                }
            }
        }

        private void btnDrawCircle_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, new Point3D(200, 200, -600)))
                {
                    System.Threading.Thread.Sleep(50);
                    g.Clear(this.BackColor);

                    p.Location = new CPI.Plot3D.Point3D(150, 150, 0);

                    DrawCircle(p, 100);
                }
            }
        }

        private void DrawCircle(Plotter3D p, float diameter)
        {
            float radius = diameter / 2;

            // Increasing this number will create a better approximation,
            // but will require more work to draw
            int sides = 64;

            float innerAngle = 360F / sides;

            float sideLength = (float)(radius * Math.Sin(Orientation3D.DegreesToRadians(innerAngle) / 2) * 2);

            // Save the initial position and orientation of the cursor
            Point3D initialLocation = p.Location;
            Orientation3D initialOrientation = p.Orientation.Clone();

            // Move to the starting point of the circle
            p.PenUp();
            p.Forward(radius - (sideLength / 2));
            p.PenDown();

            // Draw the circle
            for (int i = 0; i < sides; i++)
            {
                p.Forward(sideLength);
                p.TurnRight(innerAngle);
            }

            // Restore the position and orientation to what they were before
            // we drew the circle
            p.Location = initialLocation;
            p.Orientation = initialOrientation;
        }

        public void DrawSphere(Plotter3D p, float diameter)
        {
            Point3D initialLocation = p.Location;
            Orientation3D initialOrientation = p.Orientation.Clone();

            for (int i = 0; i < 180; i += 20)
            {
                p.PenUp();
                p.Forward(diameter / 2);

                // Rotate appropriately
                p.TurnDown(i);

                // Go back to the starting point
                p.TurnDown(180);
                p.Forward(diameter / 2);
                p.TurnDown(180);
                p.PenDown();

                DrawCircle(p, 100);

                p.Orientation = initialOrientation.Clone();
                p.Location = initialLocation;
            }
        }

        private void btnDrawSphere_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, new Point3D(200, 200, -600)))
                {
                    System.Threading.Thread.Sleep(50);
                    g.Clear(this.BackColor);

                    p.Location = new CPI.Plot3D.Point3D(150, 150, 0);

                    DrawSphere(p, 100);
                }
            }
        }

        private void btnRotateSphereAroundCenterDown_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 180; angle += 2)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, new Point3D(200, 200, -600)))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = new CPI.Plot3D.Point3D(150, 150, 0);

                        // Move to the center of the sphere
                        p.PenUp();
                        p.Forward(50);
                        p.TurnRight(90);
                        p.Forward(50);

                        p.TurnDown(angle);

                        // Retrace your steps to move back to the starting point
                        p.TurnLeft(180);
                        p.Forward(50);
                        p.TurnLeft(90);
                        p.Forward(50);
                        p.TurnRight(180);
                        p.PenDown();

                        DrawSphere(p, 100);
                    }
                }
            }
        }

        private void btnRotateSphereAroundCenterRight_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 180; angle += 2)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, new Point3D(200, 200, -600)))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = new CPI.Plot3D.Point3D(150, 150, 0);

                        // Move to the center of the sphere
                        p.PenUp();
                        p.Forward(50);
                        p.TurnRight(90);
                        p.Forward(50);

                        p.TurnRight(angle);

                        // Retrace your steps to move back to the starting point
                        p.TurnLeft(180);
                        p.Forward(50);
                        p.TurnLeft(90);
                        p.Forward(50);
                        p.TurnRight(180);
                        p.PenDown();

                        DrawSphere(p, 100);
                    }
                }
            }
        }

        private void btnSphereMultiAxisRotate_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                for (int angle = 0; angle <= 180; angle += 3)
                {
                    using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, new Point3D(200, 200, -600)))
                    {
                        System.Threading.Thread.Sleep(50);
                        g.Clear(this.BackColor);

                        p.Location = new CPI.Plot3D.Point3D(150, 150, 0);

                        // Move to the center of the sphere
                        p.PenUp();
                        p.Forward(50);
                        p.TurnRight(90);
                        p.Forward(50);

                        p.TurnDown(angle * 3);
                        p.TurnRight(angle * 2);


                        // Retrace your steps to move back to the starting point
                        p.TurnLeft(180);
                        p.Forward(50);
                        p.TurnLeft(90);
                        p.Forward(50);
                        p.TurnRight(180);
                        p.PenDown();

                        DrawSphere(p, 100);
                    }
                }
            }
        }

        private void btnDominoSetUpX_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(this.BackColor);
                using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, new Point3D(350, 100, -600)))
                {
                    p.Location = new Point3D(100, 200, 0);

                    Dominoes d = new Dominoes(10, 60, 120, 16, 60);

                    d.Render(p);
                }
            }
        }

        private void btnDominoKnockDownX_Click(object sender, EventArgs e)
        {
            Dominoes dominoes = new Dominoes(10, 60, 120, 16, 60);

            dominoRotationAngle = 0;

            dominoes.PositionChanged += new Dominoes.PositionChangedEventHandler(dominoes_PositionChanged);

            dominoes.FallOver();
        }

        void dominoes_PositionChanged(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                System.Threading.Thread.Sleep(50);
                g.Clear(SystemColors.Control);

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                using (Plotter3D p = new Plotter3D(g, new Point3D(350, 100, -600)))
                {
                    p.Location = new Point3D(100, 200, 0);

                    p.TurnDown(dominoRotationAngle);

                    ((Dominoes)sender).Render(p);
                }
            }
        }

        private void btnDominoSetUpZ_Click(object sender, EventArgs e)
        {
            using (Graphics g = this.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.Clear(this.BackColor);
                using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(g, new Point3D(350, 100, -600)))
                {
                    p.Location = new Point3D(100, 200, 0);

                    p.TurnDown(90);

                    Dominoes d = new Dominoes(10, 60, 120, 16, 60);

                    d.Render(p);
                }
            }
        }

        private void btnDominoKnockDownZ_Click(object sender, EventArgs e)
        {
            Dominoes dominoes = new Dominoes(10, 60, 120, 16, 60);

            this.dominoRotationAngle = 90;

            dominoes.PositionChanged += new Dominoes.PositionChangedEventHandler(dominoes_PositionChanged);

            dominoes.FallOver();
        }

    }
}