using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;

namespace ObservatoryCenter
{
    public partial class MainForm : Form
    {
        
        public ObservatoryControls ObsControl;

        private Point ROOF_startPos;
        private int ROOF_endPos = 80;
        private int ROOF_incPos_module = 5;
        private int ROOF_incPos;

        private int tickCount=0;
        private int waitTicksBeforeCheck = 30;
        private int maxAnimationCounts = 50;

        private string prev_direction="";


        public MainForm()
        {
            InitializeComponent();
            ObsControl = new ObservatoryControls();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Connect Main Devices
            ObsControl.connectSwitch();
            ObsControl.connectDome();

            UpdateASCOMStatus();

            //init graphic elements
            ROOF_startPos = rectRoof.Location;

            //read data
            if (ObsControl.startCheckSwitch())
            {
                //chkTelescopeCamera.Enabled = true;
                //chkFocuser.Enabled = true;
                //chkHeater.Enabled = true;
                //chkRoofPower.Enabled = true;

                ObsControl.objDome.Connected = true;

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
        }


        //Status markers
        private void UpdateASCOMStatus()
        {
            if (ObsControl.objSwitch != null && ObsControl.objSwitch.Connected)
            {
                toolStripStatus_Switch.ForeColor = Color.Black;
            }
            else
            {
                toolStripStatus_Switch.ForeColor = Color.Gray;
            }

            //DOME
            if (ObsControl.objDome != null && ObsControl.objDome.Connected)
            {
                toolStripStatus_Dome.ForeColor = Color.Black;
            }
            else
            {
                toolStripStatus_Dome.ForeColor = Color.Gray;
            }

            //TELESCOPE
            if (ObsControl.objTelescope != null && ObsControl.objTelescope.Connected)
            {
                toolStripStatus_Telescope.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Telescope.ForeColor = Color.Gray;
            }

            //FOCUSER
            if (ObsControl.objFocuser != null && ObsControl.objFocuser.Connected)
            {
                toolStripStatus_Focuser.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Focuser.ForeColor = Color.Gray;
            }

            //CAMERA
            if (ObsControl.objCamera != null && ObsControl.objCamera.Connected)
            {
                toolStripStatus_Camera.ForeColor = Color.Blue;
            }
            else
            {
                toolStripStatus_Camera.ForeColor = Color.Gray;
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
                if (rectRoof.Left < ROOF_startPos.X + ROOF_incPos_module*2)
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
            
                rectRoof.Left = ROOF_startPos.X + Convert.ToInt16(Math.Round((double)ROOF_endPos/2));
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
            rectBase.BackColor= Color.Turquoise;

            tickCount = 0;
        }

        private void AlarmRoofMoving(string MoveType)
        {
            animateRoof.Enabled = false;
            System.Windows.Forms.MessageBox.Show("Roof oppening is too long!");

        }

        private void btnStartAll_Click(object sender, EventArgs e)
        {
            //ObsControl.startMaximDL();
            ObsControl.startCCDAP();
            //ObsControl.startPlanetarium();
        }

        private void annunciatorPanel1_Click(object sender, EventArgs e)
        {
            annunciator1.Cadence = ASCOM.Controls.CadencePattern.BlinkSlow;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            ledTelescopeNCameraPower.Status = ASCOM.Controls.TrafficLight.Green;
            ledTelescopeNCameraPower.Cadence = ASCOM.Controls.CadencePattern.SteadyOn;

        }

        private void ovalShape1_Click(object sender, EventArgs e)
        {
        }

        private void btnTelescopePower_Click(object sender, EventArgs e)
        {
            bool SwitchState=(ledTelescopeNCameraPower.Status==ASCOM.Controls.TrafficLight.Green);

            SwitchState=!SwitchState;

            ObsControl.MainPower(SwitchState);

            if (SwitchState)
                ledTelescopeNCameraPower.Status = ASCOM.Controls.TrafficLight.Green;
            else
                ledTelescopeNCameraPower.Status = ASCOM.Controls.TrafficLight.Red;

            ledTelescopeNCameraPower.Status = ASCOM.Controls.TrafficLight.Yellow;
            ledTelescopeNCameraPower.CadenceUpdate(true);

            ledSwitchIndicator.Status = ASCOM.Controls.TrafficLight.Yellow;
            ledSwitchIndicator.Refresh();
            ledSwitchIndicator.Update();
            ledSwitchIndicator.Enabled=true;

        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SwitchDriverId = ASCOM.DriverAccess.Switch.Choose(Properties.Settings.Default.SwitchDriverId);
        }





    }
}
