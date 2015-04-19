using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


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

        private Point ROOF_startPos;
        private int ROOF_endPos = 80;
        private int ROOF_incPos_module = 5;
        private int ROOF_incPos;

        private int tickCount = 0;
        private int waitTicksBeforeCheck = 30;
        private int maxAnimationCounts = 50;

        private string prev_direction = "";
        
        
        private void UpdateRoofPicture()
        {
            //Draw roof status
            if (ObsControl.objDome.Connected)
            {
                if (ObsControl.CurrentSutterStatus == ShutterState.shutterClosed)
                {
                    drawClosed();
                }
                else if (ObsControl.CurrentSutterStatus == ShutterState.shutterOpen)
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


        private void btnOpenRoof_Click(object sender, EventArgs e)
        {
            rectRoof.Left = ROOF_startPos.X;
            ROOF_incPos = ROOF_incPos_module;

            prev_direction = "open";
            startAnimation();
        }

        private void btnCloseRoof_Click(object sender, EventArgs e)
        {
            rectRoof.Left = ROOF_startPos.X + ROOF_endPos;
            ROOF_incPos = -ROOF_incPos_module;

            prev_direction = "close";
            startAnimation();
        }

        private void btnStopRoof_Click(object sender, EventArgs e)
        {
            drawStoped();
            stopAnimation();
        }

        private void animateRoof_Tick(object sender, EventArgs e)
        {
            tickCount++;
            rectRoof.Location = new Point(rectRoof.Left + ROOF_incPos, rectRoof.Top);
            if (ROOF_incPos > 0)
            {
                //open animation
                if (tickCount > waitTicksBeforeCheck)
                {
                    //Roof was opened?
                    if (!(ObsControl.CurrentSutterStatus == ShutterState.shutterOpen))
                    {
                        //check - if this is too long?
                        if (tickCount < maxAnimationCounts)
                        {
                            //restart animation
                            btnOpenRoof_Click(null, null);
                        }
                        else
                        {
                            //signal error
                            drawStoped();
                            AlarmRoofMoving("open");
                        }
                    }
                    else
                    {
                        drawOpened();
                        stopAnimation();
                    }
                }
            }
            else
            {
                //close animation
                if (rectRoof.Left < ROOF_startPos.X + ROOF_incPos_module * 2)
                {
                    rectRoof.Left = ROOF_startPos.X;
                    stopAnimation();
                }
            }
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
            ShutterState curShutState = ObsControl.CurrentSutterStatus;
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


        private void startAnimation()
        {
            btnCloseRoof.Enabled = false;
            btnOpenRoof.Enabled = false;
            btnStopRoof.Enabled = true;

            animateRoof.Enabled = true;

            rectRoof.BackColor = Color.WhiteSmoke;
            rectBase.BackColor = Color.WhiteSmoke;
        }

        private void stopAnimation()
        {
            animateRoof.Enabled = false;

            rectRoof.BackColor = Color.LightSeaGreen;
            rectBase.BackColor = Color.Turquoise;

            tickCount = 0;
        }

        private void AlarmRoofMoving(string MoveType)
        {
            animateRoof.Enabled = false;
            System.Windows.Forms.MessageBox.Show("Roof oppening is too long!");

        }

        private void annunciatorPanel1_Click(object sender, EventArgs e)
        {
            annunciator1.Cadence = ASCOM.Controls.CadencePattern.BlinkSlow;
        }
    }
}
