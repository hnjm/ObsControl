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

//using IP9212_switch;

using ASCOM;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using ASCOM.RollOffRoof_IP9212;

namespace ObservatoryCenter
{
    public partial class MainForm : Form
    {
        //internal static IP9212_switch_class IP9212;
        internal static ASCOM.DriverAccess.Dome RollOffDriverId;
        
        public ObservatoryControls ObsControl;

        string DomeDriverId = "ASCOM.IP9212_rolloffroof2.Dome";

        private Point ROOF_startPos;
        private int ROOF_endPos = 80;
        private int ROOF_incPos_module = 5;
        private int ROOF_incPos;

        private int tickCount=0;
        private int waitTicksBeforeCheck = 30;
        private int maxAnimationCounts = 50;

        private string prev_direction="";

        /// <summary>
        /// Property holds current shutter status
        /// </summary>
        internal ShutterState CurrentSutterStatus;


        public MainForm()
        {
            InitializeComponent();
            IP9212 = new IP9212_switch_class();
            ObsControl = new ObservatoryControls(IP9212);

            ObsControl.connectSwitch();

            //IP9212_rolloffroof = new ASCOM.DriverAccess.Dome(DomeDriverId);
            //""

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //init graphic elements
            ROOF_startPos = rectRoof.Location;

            //read data
            if (ObsControl.startCheckSwitch())
            {
                //chkTelescopeCamera.Enabled = true;
                //chkFocuser.Enabled = true;
                //chkHeater.Enabled = true;
                //chkRoofPower.Enabled = true;

                IP9212_rolloffroof.Connected = true;
                
                CurrentSutterStatus= IP9212_rolloffroof.ShutterStatus;
                if (CurrentSutterStatus == ShutterState.shutterClosed)
                {
                    drawClosed();
                }
                else if (CurrentSutterStatus == ShutterState.shutterOpen)
                {
                    drawOpened();
                }
                rectRoof.BackColor = Color.LightSeaGreen;
                rectBase.BackColor = Color.Turquoise;

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
                    if (!(IP9212_rolloffroof.ShutterStatus == ShutterState.shutterOpen))
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
            ShutterState curShutState = IP9212_rolloffroof.ShutterStatus;
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

            ledIndicator3.Status = ASCOM.Controls.TrafficLight.Yellow;
            ledIndicator3.Refresh();
            ledIndicator3.Update();
            ledIndicator3.Enabled=true;

        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SwitchDriverId = ASCOM.DriverAccess.Switch.Choose(Properties.Settings.Default.SwitchDriverId);
        }





    }
}
