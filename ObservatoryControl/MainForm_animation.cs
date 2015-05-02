using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;


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

#region /// ROOF DRAWING /////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        private void startAnimation()
        {
            btnCloseRoof.Enabled = false;
            btnOpenRoof.Enabled = false;
            btnStopRoof.Enabled = true;

            animateRoofTimer.Enabled = true;

            rectRoof.BackColor = Color.WhiteSmoke;
            rectBase.BackColor = Color.WhiteSmoke;
        }

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

        private void annunciatorPanel1_Click(object sender, EventArgs e)
        {
            annunciator1.Cadence = ASCOM.Controls.CadencePattern.BlinkSlow;
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
            }
        }
#endregion draw roof

#region //////// DRAW TELESCOPE ////////////////////////////////////////////////////////////////////////////////

        Int16 angelAz = 0;
        Int16 angelAlt = 0;
        public Int16 NorthAzimuthCorrection = 10;
        Rectangle TelescopeVertical, TelescopeVertical2;

        Int16 TelW, Tel2W = 0;
        Int16 TelH, Tel2H = 0;

        Point TelescopeVertical_startPos = new Point(50, 50);

        private void timer1_Tick(object sender, EventArgs e)
        {
        }


        private void panelTelescopeV_Paint(object sender, PaintEventArgs e)
        {
            DrawTelescopeV((Panel)sender);
        }

        private void panelTelescopeH_Paint(object sender, PaintEventArgs e)
        {
            DrawTelescopeH((Panel)sender);
        }

        private void DrawTelescopeV(Panel pannelV)
        {
            Graphics graphicsObj = pannelV.CreateGraphics();
            Pen myPen = new Pen(Color.Red, 2);
            
            TelW=(Int16)Math.Round(pannelV.Width*0.25);
            Tel2W = (Int16)Math.Round(TelW * 0.6);
            TelH=(Int16)Math.Round(pannelV.Height/2 * 0.9);
            Tel2H = (Int16)Math.Round(TelH * 0.15);
            
            TelescopeVertical_startPos.X=(pannelV.Width-TelW)/2;
            TelescopeVertical_startPos.Y = (pannelV.Height - TelH) / 2;

            TelescopeVertical = new Rectangle(TelescopeVertical_startPos.X, TelescopeVertical_startPos.Y, TelW, TelH - Tel2H);
            TelescopeVertical2 = new Rectangle((pannelV.Width - Tel2W) / 2, TelescopeVertical_startPos.Y + TelH - Tel2H, Tel2W, Tel2H);

            //the central point of the rotation
            graphicsObj.TranslateTransform(TelescopeVertical.X + 15, TelescopeVertical.Y + 50);
            //rotation procedure
            graphicsObj.RotateTransform(angelAz);

            graphicsObj.TranslateTransform(-(TelescopeVertical.X + 15), -(TelescopeVertical.Y + 50));

            graphicsObj.DrawRectangle(myPen, TelescopeVertical);
            graphicsObj.DrawRectangle(myPen, TelescopeVertical2);
        }

        private void DrawTelescopeH(Panel pannelV)
        {
            Graphics graphicsObj = pannelV.CreateGraphics();
            Pen myPen = new Pen(Color.Red, 2);

            TelW = (Int16)Math.Round(pannelV.Width * 0.25);
            Tel2W = (Int16)Math.Round(TelW * 0.6);
            TelH = (Int16)Math.Round(pannelV.Height / 2 * 0.9);
            Tel2H = (Int16)Math.Round(TelH * 0.15);

            TelescopeVertical_startPos.X = (pannelV.Width - TelW) / 2;
            TelescopeVertical_startPos.Y = (pannelV.Height - TelH) / 2;

            TelescopeVertical = new Rectangle(TelescopeVertical_startPos.X, TelescopeVertical_startPos.Y, TelW, TelH - Tel2H);
            TelescopeVertical2 = new Rectangle((pannelV.Width - Tel2W) / 2, TelescopeVertical_startPos.Y + TelH - Tel2H, Tel2W, Tel2H);

            //the central point of the rotation
            graphicsObj.TranslateTransform(TelescopeVertical.X + 15, TelescopeVertical.Y + 50);
            //rotation procedure
            graphicsObj.RotateTransform(angelAlt);

            graphicsObj.TranslateTransform(-(TelescopeVertical.X + 15), -(TelescopeVertical.Y + 50));

            graphicsObj.DrawRectangle(myPen, TelescopeVertical);
            graphicsObj.DrawRectangle(myPen, TelescopeVertical2);
        }




#endregion draw telescope

    }
}
