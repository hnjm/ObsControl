using CPI.Plot3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public ASCOM.Utilities.Util ASCOMUtils = new ASCOM.Utilities.Util();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initVals();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {

        }


        public float DECGrad, DECRad;
        public float HAGrad, HARad;
        
        public int X0;
        public int Y0;

        private void initVals()
        {
            X0 = (int)(panel1.Width / 2 );
            Y0 = (int)(panel1.Height / 2 );

            DECGrad = (float)udDec.Value;
            HAGrad = (float)((double)udHA_hours.Value * (360.0 / 24.0) + (double)udHA_mins.Value / 60.0);

            TelescopeBack_X_Skew = (float)((double)udSkewX.Value / 180.0 * Math.PI);

            //Camera positon
            CameraPosition = new CPI.Plot3D.Point3D(Convert.ToInt32(txtCameraPos_X.Text), Convert.ToInt32(txtCameraPos_Y.Text), Convert.ToInt32(txtCameraPos_Z.Text));
            //Start postion
            StartDrawingPosition = new CPI.Plot3D.Point3D(X0, Y0, 0);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (++udHA_hours.Value > 23) udHA_hours.Value=0;

            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //DrawRotatedBlock();


            initVals();

            CalculateData();

            Draw3DTelescope(e);
        }

//////////////////////////////////////////////////////////////////////////////////////////////////////////
        public float LatitudeGrad = 56.0F;

        int PARAM_DecAxix_Len = 50;
        int PARAM_DecAxix_Thick = 10; //unused in 3d

        int PARAM_RAAxix_Len = 50;
        int PARAM_RAAxix_Thick = 4;

        int PARAM_Telescope_Thick = 30;
        int PARAM_Telescope_Len = 100;

        public CPI.Plot3D.Point3D CameraPosition;
        public CPI.Plot3D.Point3D StartDrawingPosition;
//////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Draw3DTelescope(PaintEventArgs e)
        {
            
            using (Graphics graphicsObj = e.Graphics)
            //using (Graphics graphicsObj = panel1.CreateGraphics())
            {
                graphicsObj.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //Create graph objects
                SolidBrush Brush1 = new SolidBrush(Color.LightGray); //закраска
                SolidBrush RedBrush = new SolidBrush(Color.Red); //закраска

                Pen grayPen = new Pen(Color.LightGray, 1); //цвет линии
                Pen blackPen = new Pen(Color.Black, 3); //цвет линии

                //1. Сместить и повернуть холст
                //1.1. Сместить на центр, чтобы вращение было по точке соприкосновения с телескопом
                graphicsObj.TranslateTransform(X0, Y0);
                //1.2. Повернуть на широту
                graphicsObj.RotateTransform(LatitudeGrad);
                //1.3. Вернуть начало координат в начало
                graphicsObj.TranslateTransform(-X0, -Y0);

                //Обозначим цетр
                graphicsObj.FillEllipse(RedBrush, X0 - 2, Y0 - 2, 4, 4);

                //1. Ось RA
                Rectangle recRAAxis_el1 = new Rectangle((int)(X0 - PARAM_RAAxix_Thick / 2), (int)(Y0), (int)(PARAM_RAAxix_Thick), (int)(PARAM_RAAxix_Len));
                graphicsObj.DrawRectangle(Pens.Black, recRAAxis_el1);


                using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(graphicsObj, new Pen(Color.Black,1) , CameraPosition))
                {
                    //System.Threading.Thread.Sleep(50);
                    //g.Clear(this.BackColor);

                    //Camera positon
                    p.Location = StartDrawingPosition;

                    //2. Ось DEC
                    p.PenUp();
                    p.TurnLeft(90);
                    p.TurnDown(90);

                    p.TurnRight(HAGrad); //Rotate Hour Angle

                    p.Forward(PARAM_DecAxix_Len / 2);
                    p.TurnRight(180);
                    p.PenDown();
                    p.Forward(PARAM_DecAxix_Len); //Dec axis

                    //3. Телескоп
                    p.PenUp();
                    p.TurnDown(90);
                    p.TurnRight(90);

                    p.TurnRight(DECGrad); //Rotate DEC

                    // Move to telescope start
                    p.Forward(PARAM_Telescope_Len / 2);
                    p.TurnUp(90);
                    p.Forward(PARAM_Telescope_Thick);
                    p.TurnRight(90);
                    p.Forward(PARAM_Telescope_Thick / 2);
                    p.TurnRight(90);
                    p.TurnUp(90);
                    p.PenDown();

                    Draw3DCube(p, PARAM_Telescope_Len, PARAM_Telescope_Thick, PARAM_Telescope_Thick);

                    //4. Телексоп front
                    p.TurnDown(90);
                    Draw3DRect(p, PARAM_Telescope_Thick, PARAM_Telescope_Thick, new Pen(Color.Blue));

                    //5. Телексоп строна к монтировке
                    p.PenUp();
                    p.Forward(PARAM_Telescope_Thick);
                    p.TurnUp(90);
                    p.PenDown();
                    Draw3DRect(p, PARAM_Telescope_Len, PARAM_Telescope_Thick, new Pen(Color.Silver));
                }
            }
        }



        private void DrawRotatedBlock()
        {
            //Create graph objects
            Graphics graphicsObj = panel1.CreateGraphics();
            graphicsObj.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            SolidBrush Brush1 = new SolidBrush(Color.LightGray); //закраска
            SolidBrush RedBrush = new SolidBrush(Color.Red); //закраска

            Pen grayPen = new Pen(Color.LightGray, 1); //цвет линии
            Pen blackPen = new Pen(Color.Black, 3); //цвет линии

            //1. Сместить и повернуть холст
            //1.1. Сместить на центр, чтобы вращение было по точке соприкосновения с телескопом
            graphicsObj.TranslateTransform(X0, Y0);
            //1.2. Повернуть на широту
            graphicsObj.RotateTransform(LatitudeGrad);
            //1.3. Вернуть начало координат в начало
            graphicsObj.TranslateTransform(-X0, -Y0);

            //Обозначим цетр
            graphicsObj.FillEllipse(RedBrush, X0 - 2, Y0 - 2, 4, 4);

            //2. Ось DEC
            Rectangle recDecAxis_el1 = new Rectangle(DecAxix_X1, DecAxix_Y1, DecAxix_Len, PARAM_DecAxix_Thick);
            graphicsObj.DrawRectangle(Pens.Black, recDecAxis_el1);


            //3. Ось RA
            Rectangle recRAAxis_el1 = new Rectangle((int)(X0 - PARAM_RAAxix_Thick / 2), (int)(Y0), (int)(PARAM_RAAxix_Thick ), (int)(PARAM_RAAxix_Len ));
            graphicsObj.DrawRectangle(Pens.Black, recRAAxis_el1);

            //3. телескоп
            //3.1. Труба
            Rectangle recTelescope_tuberect = new Rectangle(Telescope_X1, Telescope_Y1, PARAM_Telescope_Thick, Telescope_Len);
            //graphicsObj.FillRectangle(Brush1, recTelescope_el1);
            graphicsObj.DrawRectangle(Pens.Black, recTelescope_tuberect);

            //3.2. Перед
            Rectangle recTelescope_Front = new Rectangle(TelescopeFront_X1, TelescopeFront_Y1, PARAM_Telescope_Thick, TelescopeFront_Height);
            //graphicsObj.FillRectangle(Brush1, recTelescope_el1);
            graphicsObj.DrawRectangle(Pens.Black, recTelescope_Front);

            //3.3. Зад
            //SkewTransform(45.0F, 0.0F, X0, Y0);

            Rectangle recTelescope_Back = new Rectangle(TelescopeBack_X1, TelescopeBack_Y1, PARAM_Telescope_Thick, TelescopeBack_Height);
            //graphicsObj.FillRectangle(Brush1, recTelescope_el1);
            //graphicsObj.DrawRectangle(grayPen, recTelescope_Back);

            DrawSkewedRectagle(graphicsObj, grayPen, TelescopeBack_X1, TelescopeBack_Y1, PARAM_Telescope_Thick, TelescopeBack_Height, TelescopeBack_X_Skew, 0);


        }


        public void Draw(Graphics G, int width)
        {
            int height = 50; // height of the cube (y-axis)
            int skew = 20;
            Point Org = new Point(100, 100);
            Pen pencil = new Pen(Color.Blue, 1f);
            Rectangle R = new Rectangle(Org.X, Org.Y, width, height);
            G.DrawRectangle(pencil, R);
            G.DrawLine(pencil, Org.X, Org.Y, Org.X + skew, Org.Y - skew);
            G.DrawLine(pencil, Org.X + skew, Org.Y - skew, Org.X + width + skew, Org.Y - skew);
            // continue with DrawLine here to finish it
        }
    

        public void DrawSkewedRectagle(Graphics G, Pen curPen ,int X1, int Y1, int Width, int Height, float skewX, float skewY)
        {
            int X1n = (int)Math.Round(X1 + Height/2 *Math.Tan(skewX));
            int Y1n = Y1;
            int X2n = X1n + Width;
            int Y2n = Y1;
            int X4n = (int)Math.Round(X1 - Height/2 * Math.Tan(skewX));
            int Y4n = Y1 + Height;
            int X3n = X4n + Width;
            int Y3n = Y1 + Height;

            G.DrawLine(curPen, X1n, Y1n, X2n, Y2n);
            G.DrawLine(curPen, X3n, Y3n, X2n, Y2n);
            G.DrawLine(curPen, X3n, Y3n, X4n, Y4n);
            G.DrawLine(curPen, X1n, Y1n, X4n, Y4n);
        }

        //not used in 3D
        int Telescope_Len;
        int Telescope_X1, Telescope_Y1;
        int TelescopeFront_Height, TelescopeFront_X1, TelescopeFront_Y1;
        int TelescopeBack_Height, TelescopeBack_X1, TelescopeBack_Y1;
        float TelescopeBack_X_Skew;

        int DecAxix_Len;
        int DecAxix_X1, DecAxix_Y1;

        private void CalculateData()
        {
            //Перевести углы в радианы
            HARad = (float)(HAGrad / 180.0 * Math.PI);

            DECRad = (float) (DECGrad / 180.0 * Math.PI);

            //1. Расчитать проекцию оси DEC 
            //1.1. Длина
            DecAxix_Len = (int)(PARAM_DecAxix_Len * Math.Cos(HARad)); 

            //1.2. Точка начала
            DecAxix_X1 = (int)(X0 - DecAxix_Len / 2);
            DecAxix_Y1 = (int)(Y0 - PARAM_DecAxix_Thick / 2);


            //2. Расчитать телескоп
            //2.1. Длина и положение прямоугольника
            Telescope_Len = (int)(PARAM_Telescope_Len * Math.Sin (DECRad));
            //Стартовая точка прямоугольника
            Telescope_X1 = (int)(X0 - DecAxix_Len/2 - PARAM_Telescope_Thick/2);
            Telescope_Y1 = (int)(Y0 - Telescope_Len / 2); ;

            //2.2. Параметры входного отверстия трубы ("перед")
            TelescopeFront_Height = (int)(PARAM_Telescope_Thick * Math.Cos(DECRad));
            //Стартовая точка прямоугольника
            TelescopeFront_X1 = Telescope_X1;
            TelescopeFront_Y1 = (int)(Telescope_Y1 - TelescopeFront_Height / 2); ;

            //2.3. Параметры задней части трубы ("зад")
            TelescopeBack_Height = TelescopeFront_Height;
            TelescopeBack_X1 = TelescopeFront_X1;
            TelescopeBack_Y1 = TelescopeFront_Y1 + Telescope_Len;

            //TelescopeBack_X_Skew = 
        }

//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
// 3D
//////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////
        public void Draw3DRect(Plotter3D p, float sideWidth, float sideHeight)
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

        public void Draw3DRect(Plotter3D p, float sideWidth, float sideHeight, Pen PenColor)
        {
            p.Forward(sideWidth, PenColor);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees

            p.Forward(sideHeight, PenColor);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees

            p.Forward(sideWidth, PenColor);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees

            p.Forward(sideHeight, PenColor);  // Draw a line sideLength long
            p.TurnRight(90);        // Turn right 90 degrees
        }


        public void Draw3DCube(Plotter3D p, float XWidth, float YHeight, float ZThick)
        {
            /*            for (int i = 0; i < 4; i++)
                        {
                            //DrawSquare(p, sideLength);
                            DrawRect(p, sideLength+10, sideLength-10);
                            p.Forward(sideLength);
                            p.TurnDown(90);
                        }
            */
            Draw3DRect(p, XWidth, YHeight);

            p.Forward(XWidth);

            p.TurnDown(90);
            Draw3DRect(p, ZThick, YHeight);

            p.Forward(ZThick);
            p.TurnDown(90);
            Draw3DRect(p, XWidth, YHeight);

            p.Forward(XWidth);
            p.TurnDown(90);
            Draw3DRect(p, ZThick, YHeight);

            //and go to initial position
            p.PenUp();
            p.Forward(ZThick);  // Draw a line sideLength long
            p.TurnDown(90);
            p.PenDown();

        }
        //////////////////////////////////////////////////////////////////////


    }
}
