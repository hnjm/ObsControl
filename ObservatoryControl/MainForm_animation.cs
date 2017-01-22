using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using CPI.Plot3D;

namespace ObservatoryCenter
{
    

    /// <summary>
    ///  ANIMATION PART OF MAIN FORM
    ///  Contains variables, constants, methods
    ///  and even button handlers
    ///  i.e. all that have connection with roof animation and control
    /// </summary>
    public partial class MainForm
    {

#region Roof settings
        public double RoofDuration=30; //sec. Actual value is read from settings
        public int RoofDurationCount = 1; //Actual value is read from settings
        internal int RoofDurationInTicks; // roof duration recalculated to tickss

        //Main animation constants
        public double waitTicksBeforeCheckTolerance = 0.20; //10% - used to calculate  waitTicksBeforeCheck = RoofDuration*(1 - waitTicksBeforeCheckTolerance)
        public double maxAnimationCountsTolerance = 0.10; //10% - used to calculate    maxAnimationCounts   = RoofDuration*(1 + axAnimationCountsTolerance)

        private Point ROOF_startPos;    //roof starting coordinate, would be auto initialized
        public int ROOF_endPos = 80;   //roof ending X coordinate
        
        private double ROOF_incPos_module = 1.0; //increament speed by modulus (pixels per tick). Autocalculated
        private double ROOF_incPos; //ROOF_incPos_module with sign ("+" open, "-" close)
        private double curXCoord=0;

        private int tickCount = 0;
        private int waitTicksBeforeCheck; //would be auto set during startAnimation procudre
        private int maxAnimationCounts; //would be auto set during startAnimation procudre

        private string prev_direction = "";

#endregion roof settings

#region ////// ROOF DRAWING /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Init Roof Opening routine
        /// </summary>
        private void btnOpenRoof_Click(object sender, EventArgs e)
        {
            //calc ticks
            RoofDurationInTicks= (int)Math.Round(RoofDuration * (1000.0 / animateRoofTimer.Interval));
            waitTicksBeforeCheck = (int)Math.Floor(RoofDuration * (1000.0 / animateRoofTimer.Interval) * (1 - waitTicksBeforeCheckTolerance));
            maxAnimationCounts = (int)Math.Ceiling(RoofDuration * (1000.0 / animateRoofTimer.Interval) * (1 + maxAnimationCountsTolerance));
            ROOF_incPos_module = ROOF_endPos / (RoofDuration * (1000.0 / animateRoofTimer.Interval));

            rectRoof.Left = ROOF_startPos.X;
            ROOF_incPos = ROOF_incPos_module;
            curXCoord = rectRoof.Left;

            prev_direction = "open";

            bool RFflag=ObsControl.RoofOpen();  //start OPENING. true if was started
            if (RFflag) startAnimation();       //start animation
        }

        /// <summary>
        /// Init close roof routine
        /// </summary>
        private void btnCloseRoof_Click(object sender, EventArgs e)
        {
            //calc ticks
            waitTicksBeforeCheck = (int)Math.Floor(RoofDuration * (1000.0 / animateRoofTimer.Interval) * (1 - waitTicksBeforeCheckTolerance));
            maxAnimationCounts = (int)Math.Ceiling(RoofDuration * (1000.0 / animateRoofTimer.Interval) * (1 + maxAnimationCountsTolerance));
            ROOF_incPos_module = ROOF_endPos / (RoofDuration * (1000.0 / animateRoofTimer.Interval));

            rectRoof.Left = ROOF_startPos.X + ROOF_endPos;
            ROOF_incPos = -ROOF_incPos_module;
            curXCoord = rectRoof.Left;

            prev_direction = "close";

            bool RFflag=ObsControl.RoofClose();
            if (RFflag) startAnimation();
        }

        /// <summary>
        /// Stop roof. As for now - stops only animation
        /// </summary>
        private void btnStopRoof_Click(object sender, EventArgs e)
        {
            //
            drawStoped();
            stopAnimation();
        }

        /// <summary>
        /// Procedure to start animation
        /// </summary>
        private void startAnimation()
        {
            btnCloseRoof.Enabled = false;
            btnOpenRoof.Enabled = false;
            btnStopRoof.Enabled = true;

            animateRoofTimer.Enabled = true;

            rectRoof.BackColor = Color.WhiteSmoke;
            rectBase.BackColor = Color.WhiteSmoke;
        }

        /// <summary>
        /// Procedure to stop animation
        /// </summary>
        private void stopAnimation()
        {
            animateRoofTimer.Enabled = false;

            rectRoof.BackColor = Color.LightSeaGreen;
            rectBase.BackColor = Color.Turquoise;

            tickCount = 0;
        }

        /// <summary>
        /// Roof opening/closing main tick
        /// </summary>
        private void animateRoof_Tick(object sender, EventArgs e)
        {
            tickCount++;
            curXCoord += ROOF_incPos;
            rectRoof.Location = new Point((int)Math.Round(curXCoord), rectRoof.Top);
                
            //Was enough time passed to start cheking actual roof status?
            if (tickCount > waitTicksBeforeCheck)
            {
                //Roof was opened/closed?
                if (((ROOF_incPos > 0) && ObsControl.objDome.ShutterStatus != ShutterState.shutterOpen) ||
                    ((ROOF_incPos < 0) && ObsControl.objDome.ShutterStatus != ShutterState.shutterClosed))

                {
                //Not yet

                    //check - if this is too long?
                    if (tickCount < RoofDurationInTicks)
                    {
                        //normal duration wasn't reached, do nothing - continue animation
                    }
                    else if (tickCount < maxAnimationCounts)
                    {
                        //restart animation
                        if (ROOF_incPos > 0){
                            btnOpenRoof_Click(null, null);
                        }else{
                            btnCloseRoof_Click(null, null);
                        }
                    }
                    else
                    {
                        //signal error
                        drawStoped();
                        if (ROOF_incPos > 0){
                            AlarmRoofMoving("open");
                        }else{
                            AlarmRoofMoving("clos");
                        }
                    }
                }
                else
                {
                //finished - closed/opened
                    //calc and save statistic
                    TimeSpan SinceStartPassed = DateTime.Now.Subtract(ObsControl.RoofRoutine_StartTime);
                    ObsControl.curRoofRoutineDuration_Seconds = (int)Math.Round(SinceStartPassed.TotalSeconds, 0);
                    RoofDuration = (RoofDuration * RoofDurationCount + ObsControl.curRoofRoutineDuration_Seconds) / (RoofDurationCount+1); //calc new roof duration
                    Properties.Settings.Default.RoofDuration = Convert.ToDecimal(RoofDuration);
                    Properties.Settings.Default.RoofDurationMeasurementsCount = RoofDurationCount+1;
                    Properties.Settings.Default.Save();

                    if (ROOF_incPos > 0)
                    {
                        drawOpened();
                        Logging.AddLog("Roof was opened in " + ObsControl.curRoofRoutineDuration_Seconds + " sec");
                    }
                    else
                    {
                        drawClosed();
                        Logging.AddLog("Roof was closed in " + ObsControl.curRoofRoutineDuration_Seconds + " sec");
                    }
                    stopAnimation();
                }
                
            }
            /*else
            {
                //close animation
                if (rectRoof.Left < ROOF_startPos.X + ROOF_incPos_module * 2)
                {
                    rectRoof.Left = ROOF_startPos.X;
                    stopAnimation();
                } 
            } */
        }

        private void drawOpened()
        {
            rectRoof.Left = ROOF_startPos.X + ROOF_endPos;
            btnCloseRoof.Enabled = true;
            btnOpenRoof.Enabled = false;
            btnStopRoof.Enabled = false;
        }
        private void drawClosed()
        {
            rectRoof.Left = ROOF_startPos.X;
            btnCloseRoof.Enabled = false;
            btnOpenRoof.Enabled = true;
            btnStopRoof.Enabled = false;
        }
        private void drawStoped()
        {
            ShutterState curShutState = ObsControl.objDome.ShutterStatus;
            if ((curShutState == ShutterState.shutterOpening) || (curShutState == ShutterState.shutterClosing))
            {
                rectRoof.Left = ROOF_startPos.X + Convert.ToInt16(Math.Round((double)ROOF_endPos / 2));
                if (prev_direction == "open")
                {
                    btnCloseRoof.Enabled = true;
                    btnOpenRoof.Enabled = false;
                    btnStopRoof.Enabled = false;
                }
                else if (prev_direction == "close")
                {
                    btnCloseRoof.Enabled = false;
                    btnOpenRoof.Enabled = true;
                    btnStopRoof.Enabled = false;
                }
            }
            else if (curShutState == ShutterState.shutterOpen)
            {
                drawOpened();
            }
            else if (curShutState == ShutterState.shutterClosed)
            {
                drawClosed();
            }
        }



        private void AlarmRoofMoving(string MoveType)
        {
            animateRoofTimer.Enabled = false;
            System.Windows.Forms.MessageBox.Show("Roof " + (MoveType) + "ing is too long!");
        }


        /// <summary>
        /// Draw roof position whenever needed
        /// as for now called only once - form_load
        /// </summary>
        private void UpdateRoofPicture()
        {
            //Draw roof status
            if (ObsControl.objDome.Connected)
            {
                if (ObsControl.objDome.ShutterStatus == ShutterState.shutterClosed)
                {
                    drawClosed();
                }
                else if (ObsControl.objDome.ShutterStatus == ShutterState.shutterOpen)
                {
                    drawOpened();
                }
                rectRoof.BackColor = Color.LightSeaGreen;
                rectBase.BackColor = Color.Turquoise;
            }
            else
            {
                drawClosed();
                rectRoof.BackColor = Color.WhiteSmoke;
                rectBase.BackColor = Color.WhiteSmoke;
                btnOpenRoof.Enabled = false;
            }
        }
        #endregion draw roof

        #region ////// DRAW TELESCOPE /////////////////////||||||||||||||||||||///////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Широта
        public float LatitudeGrad = 90.0F - 56.0F; //CHANGE TO OBS in FUTURE

        //DEC ось
        int PARAM_DecAxix_Len = 50;

        //RA ось
        int PARAM_RAAxix_Len = 50;
        int PARAM_RAAxix_Thick = 4;

        //размер телескопа
        int PARAM_Telescope_Thick = 30;
        int PARAM_Telescope_Len = 100;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool PoinitingSideEtoW = true; //true - direct (east side), false - rear (west side)
        bool PoinitingPhysicalSideE = true; //true - direct (east side), false - rear (west side)

        public CPI.Plot3D.Point3D CameraPosition;
        public CPI.Plot3D.Point3D StartDrawingPosition;

        public float DEC_grad;
        public float DEC_mech_grad;
        public float HA, HA_grad, RA;
        public float HA_mech, HA_mech_grad;

        public int X0;
        public int Y0;

        //Telescope Objective(Enter) Visibility
        private Point3D P1, P2;
        private bool TelescopeObjectiveVisible;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void calculateTelescopePositionsVars()
        {
            //Init position
            X0 = (int)(panelTele3D.Width / 2 );
            Y0 = (int)(panelTele3D.Height / 2 * 1.2);

            //update fields
            DEC_grad = (float)ObsControl.objTelescope.Declination;
            RA = (float)ObsControl.objTelescope.RightAscension;

            HA = (float)(ObsControl.objTelescope.SiderealTime - RA);
            if (HA < 0) HA = HA + 24.0F;
            if (HA > 24) HA = HA - 24.0F;

             
            //Determine physical side of peir and calculate Mechanical HA
            if (HA>=0 && HA<6)
            {
                if (ObsControl.objTelescope.SideOfPier == PierSide.pierEast)
                {
                    PoinitingSideEtoW = true;
                    PoinitingPhysicalSideE = true;
                    HA_mech = HA;
                    DEC_mech_grad = DEC_grad;
                }
                else if (ObsControl.objTelescope.SideOfPier == PierSide.pierWest)
                {
                    PoinitingSideEtoW = false;
                    PoinitingPhysicalSideE = false;
                    HA_mech = HA + 12.0f;
                    DEC_mech_grad = 180.0F - DEC_grad;
                }
            }
            else if (HA >= 6 && HA < 12)
            {
                if (ObsControl.objTelescope.SideOfPier == PierSide.pierEast)
                {
                    PoinitingSideEtoW = true;
                    PoinitingPhysicalSideE = false;
                    HA_mech = HA;
                    DEC_mech_grad = DEC_grad;
                }
                else if (ObsControl.objTelescope.SideOfPier == PierSide.pierWest)
                {
                    PoinitingSideEtoW = false;
                    PoinitingPhysicalSideE = true;
                    HA_mech = HA + 12.0f;
                    DEC_mech_grad = 180.0F - DEC_grad;
                }
            }
            else if (HA >= 12 && HA < 18)
            {
                if (ObsControl.objTelescope.SideOfPier == PierSide.pierEast)
                {
                    PoinitingSideEtoW = true;
                    PoinitingPhysicalSideE = false;
                    HA_mech = HA;
                    DEC_mech_grad = DEC_grad;
                }
                else if (ObsControl.objTelescope.SideOfPier == PierSide.pierWest)
                {
                    PoinitingSideEtoW = false;
                    PoinitingPhysicalSideE = true;
                    HA_mech = HA - 12.0f;
                    DEC_mech_grad = 180.0F - DEC_grad;
                }
            }
            else  if (HA >= 18 && HA < 24)
            {
                if (ObsControl.objTelescope.SideOfPier == PierSide.pierEast)
                {
                    PoinitingSideEtoW = true;
                    PoinitingPhysicalSideE = true;
                    HA_mech = HA;
                    DEC_mech_grad = DEC_grad;
                }
                else if (ObsControl.objTelescope.SideOfPier == PierSide.pierWest)
                {
                    PoinitingSideEtoW = false;
                    PoinitingPhysicalSideE = false;
                    HA_mech = HA - 12.0f;
                    DEC_mech_grad = 180.0F - DEC_grad;
                }
            }


            HA_grad = (float) (Math.Truncate(HA) * 15 + HA-Math.Truncate(HA)); //conver to degrees
            HA_mech_grad = (float)(Math.Truncate(HA_mech) * 15 + HA_mech - Math.Truncate(HA_mech)); //conver to degrees

            //DEC_mech_grad = DEC_grad;//for debug only

            txtTelescopeAz.Text = ObsControl.ASCOMUtils.DegreesToDMS(ObsControl.objTelescope.Azimuth);
            txtTelescopeAlt.Text = ObsControl.ASCOMUtils.DegreesToDMS(ObsControl.objTelescope.Altitude);

            txtTelescopeRA.Text = ObsControl.ASCOMUtils.HoursToHMS(RA);
            txtTelescopeDec.Text = ObsControl.ASCOMUtils.DegreesToDMS(DEC_grad);

            txtHA.Text = ObsControl.ASCOMUtils.HoursToHMS(HA);
            txtHAmech.Text = ObsControl.ASCOMUtils.HoursToHMS(HA_mech);

            txtDEC_mech.Text = ObsControl.ASCOMUtils.DegreesToDMS(DEC_mech_grad);


            if (PoinitingSideEtoW)
            {
                txtPierSide.Text = "East, looking West";
            }
            else 
            {
                txtPierSide.Text = "West, looking East";
            }

            if (PoinitingPhysicalSideE)
            {
                txtPierPhysSide.Text = "East, looking West";
            }
            else
            {
                txtPierPhysSide.Text = "West, looking East";
            }

            //Camera positon
            CameraPosition = new CPI.Plot3D.Point3D(X0, Y0, -1000);
            //Start postion
            StartDrawingPosition = new CPI.Plot3D.Point3D(X0, Y0, 0);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Draw3DTelescope(PaintEventArgs e)
        {

            using (Graphics graphicsObj = e.Graphics)
            //using (Graphics graphicsObj = panel1.CreateGraphics())
            {
                graphicsObj.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                Pen grayPen = new Pen(Color.LightGray, 1); //цвет линии  (NOT USED)
                Pen blackPen = new Pen(Color.Black, 3); //цвет линии  (NOT USED)
                Pen TracePen = new Pen(Color.Red, 2); //цвет линии  (NOT USED)
                Pen TracePen2 = new Pen(Color.PaleVioletRed, 2); //цвет линии  (NOT USED)


                //Настройки цветов
                Pen TelescopeColor = new Pen(Color.LightGray, 1); //вход телескопа виден
                Pen TelescopeObjectiveVisibleColor = Pens.Blue; //вход телескопа виден
                Pen TelescopeObjectiveInvisibleColor = Pens.LightBlue; //вход телескопа невиден

                Pen AxisColor_PhysicalSideE = new Pen(Color.LightGray, 1); //цвет оси RA/Dec когда телескоп с восточной стороны (телескоп ПЕРЕД монтировкой)
                Pen AxisColor_PhysicalSideW = new Pen(Color.Black, 1); //цвет оси RA/Dec когда телескоп с западной стороны (телескоп ЗА монтировкой)
                SolidBrush AxisPoint_PhysicalSideE = new SolidBrush(Color.LightGray); //закраска
                SolidBrush AxisPoint_PhysicalSideW = new SolidBrush(Color.Black); //закраска

                //1. Сместить и повернуть холст
                //1.1. Сместить на центр, чтобы вращение было по точке соприкосновения с телескопом
                graphicsObj.TranslateTransform(X0, Y0);
                //1.2. Повернуть на широту
                graphicsObj.RotateTransform(LatitudeGrad);
                //1.3. Вернуть начало координат в начало
                graphicsObj.TranslateTransform(-X0, -Y0);

                //Обозначим цетр
                graphicsObj.FillEllipse((PoinitingPhysicalSideE ? AxisPoint_PhysicalSideE : AxisPoint_PhysicalSideW), X0 - 2, Y0 - 2, 4, 4);

                //1. Ось RA
                Rectangle recRAAxis = new Rectangle((int)(X0 - PARAM_RAAxix_Thick / 2), (int)(Y0), (int)(PARAM_RAAxix_Thick), (int)(PARAM_RAAxix_Len));
                graphicsObj.DrawRectangle((PoinitingPhysicalSideE ? AxisColor_PhysicalSideE : AxisColor_PhysicalSideW), recRAAxis);


                using (CPI.Plot3D.Plotter3D p = new CPI.Plot3D.Plotter3D(graphicsObj, TelescopeColor, CameraPosition))
                {
                    //System.Threading.Thread.Sleep(50);
                    //g.Clear(this.BackColor);

                    //Camera positon
                    p.Location = StartDrawingPosition;

                    //2. Ось DEC
                    p.PenUp();
                    p.TurnLeft(90);
                    p.TurnDown(90);

                    p.TurnRight(HA_mech_grad); //Rotate Hour Angle

                    p.Forward(PARAM_DecAxix_Len / 2);
                    p.TurnRight(180);
                    p.PenDown();
                    p.Forward(PARAM_DecAxix_Len, (PoinitingPhysicalSideE ? AxisColor_PhysicalSideE : AxisColor_PhysicalSideW)); //Dec axis

                    //3. Телескоп
                    p.PenUp();
                    p.TurnDown(90);
                    p.TurnRight(90);

                    p.TurnRight(DEC_mech_grad); //Rotate DEC

                    // Move to telescope start
                    p.Forward(PARAM_Telescope_Len / 2);
                    p.TurnUp(90);
                    p.Forward(PARAM_Telescope_Thick);
                    p.TurnRight(90);
                    p.Forward(PARAM_Telescope_Thick / 2);
                    p.TurnRight(90);
                    p.TurnUp(90);
                    p.PenDown();


                    // Нарисовать телескоп
                    // Нулевое направление:
                    // - точка пересеения небесного меридиана/экватора на юге
                    // - расположение трубы восточная сторона на запад
                    Draw3DCube(p, PARAM_Telescope_Len, PARAM_Telescope_Thick, PARAM_Telescope_Thick);

                    /*
                    //dir test
                    p.Forward(20, TracePen);
                    p.TurnDown(90);
                    p.Forward(20, TracePen);
                    */

                    //4. Телексоп front
                    //4.1 Point P1
                    //initial pos: верхняя (меньшая по Y, помним, что Y увеличивается вниз) передняя (ближняя, по Z) точка фермы (when in zero pos, см. выше)
                    //initial dir: вдоль фермы вниз
                    //orientation: на зрителя
                     P1 = p.Location;
                        txtP1_X.Text = P1.X.ToString();
                        txtP1_Y.Text = P1.Y.ToString();
                        txtP1_Z.Text = P1.Z.ToString();
                        //Draw3DRect(p, 3, 3, TracePen); //draw point for debug

                    //4.2 Point P2
                    //initial pos: верхняя (меньшая по Y, помним, что Y увеличивается вниз) задняя (дальняя, по Z) точка фермы (when in zero pos, см. выше)
                    //после перехода к точке dir: вдоль фермы вниз
                    //после перехода к точке orientation: на зрителя
                    //go to it
                    p.PenUp();
                    p.TurnDown(90);
                    p.Forward(PARAM_Telescope_Thick);

                    P2 = p.Location;
                        txtP2_X.Text = P2.X.ToString();
                        txtP2_Y.Text = P2.Y.ToString();
                        txtP2_Z.Text = P2.Z.ToString();
                        //p.PenDown();
                        //Draw3DRect(p, 3, 3, TracePen2);//draw point for debug
                        //p.PenUp();

                    //Return from P2 to P1
                    p.TurnUp(180);
                    p.Forward(PARAM_Telescope_Thick);
                    p.TurnDown(90);
                    p.PenDown();
                    p.TurnDown(90);

                    // Определим - виден ли вход трубы?
                    if ( (P1.Z< P2.Z) && (P1.X > P2.X)  || (P1.Z > P2.Z) && (P1.X > P2.X))
                    {
                        if (PoinitingSideEtoW)
                        {
                            TelescopeObjectiveVisible = true;
                        }
                        else
                        {
                            TelescopeObjectiveVisible = false;
                        }
                    }
                    else
                    {
                        if (PoinitingSideEtoW)
                        {
                            TelescopeObjectiveVisible = false;
                        }
                        else
                        {
                            TelescopeObjectiveVisible = true;
                        }
                    }


                    Draw3DRect(p, PARAM_Telescope_Thick, PARAM_Telescope_Thick, (TelescopeObjectiveVisible ? TelescopeObjectiveVisibleColor : TelescopeObjectiveInvisibleColor));


                    /*
                    p.TurnDown(90);
                    if (p.Location.Z < -(PARAM_Telescope_Thick+ PARAM_DecAxix_Len / 2))
                    {
                        Draw3DRect(p, PARAM_Telescope_Thick, PARAM_Telescope_Thick, Pens.Blue);
                        //Draw3DRect(p, PARAM_Telescope_Thick, PARAM_Telescope_Thick, (PoinitingPhysicalSideE ? Pens.Blue : Pens.LightBlue));
                    }
                    else
                    {
                        Draw3DRect(p, PARAM_Telescope_Thick, PARAM_Telescope_Thick, Pens.LightBlue);
                    }
                    */
                    //p.Forward(20, Pens.Red);



                    ////p.TurnDown(90);
                    //if (PoinitingSideWtoE)
                    //{
                    //    p.PenUp();
                    //    p.Forward(2);
                    //    p.TurnRight(90);
                    //    p.Forward(2);
                    //    p.TurnLeft(90);
                    //    p.PenDown();

                    //    Draw3DRect(p, PARAM_Telescope_Thick - 4, PARAM_Telescope_Thick - 4, new Pen(Color.Blue));
                    //}

                    ////5. Телексоп back
                    //p.TurnLeft(90);
                    //p.Forward(2, Pens.Red);
                    //p.TurnLeft(90);
                    //p.Forward(2, Pens.Red);
                    //p.TurnDown(90);
                    //p.Forward(PARAM_Telescope_Len, Pens.Green);
                    //p.TurnDown(90);
                    //p.Forward(PARAM_Telescope_Len, Pens.Orange);
                    ////p.Forward(100, Pens.Red);
                    //Draw3DRect(p, PARAM_Telescope_Thick, PARAM_Telescope_Thick, (PoinitingSideWtoE ? Pens.Red : Pens.Red));

                    ////5. Телексоп строна к монтировке
                    //p.PenUp();
                    //p.Forward(PARAM_Telescope_Thick);
                    //p.TurnUp(90);
                    //p.PenDown();
                    //Draw3DRect(p, PARAM_Telescope_Len, PARAM_Telescope_Thick, new Pen(Color.Silver));
                }
            }
        }

        //////////////////////////////////////////////////////////////////////
        // 3D Elements
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

        #endregion draw telescope

    }
}
