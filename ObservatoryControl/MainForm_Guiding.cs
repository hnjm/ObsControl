using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using LoggingLib;
using System.Threading;

namespace ObservatoryCenter
{
    /// <summary>
    /// UPDATE GUIDING DATA
    ///  Contains methods to update some controls
    ///  usually calls from Timers 
    /// </summary>
    public partial class MainForm
    {

        //LastGuideDataUpdated
        double LastGuideReadDataTimestamp = 0.0;

        /// <summary>
        /// Update PHD state
        /// </summary>
        private void UpdatePHDstate()
        {
            if (ObsControl.objPHD2App.IsRunning())
            {
                string curstate = ObsControl.objPHD2App.currentState.ToString();
                txtPHDState.Text = curstate;

                //Short interface color indicator
                if (ObsControl.objPHD2App.currentState == PHDState.Guiding)
                {
                    chkShort_Guiding.BackColor = OnColor;
                }
                else if (ObsControl.objPHD2App.currentState == PHDState.Looping
                            || ObsControl.objPHD2App.currentState == PHDState.Dithered 
                            || ObsControl.objPHD2App.currentState == PHDState.StarSelected 
                            || ObsControl.objPHD2App.currentState == PHDState.Calibrating 
                            || ObsControl.objPHD2App.currentState == PHDState.Looping 
                            || ObsControl.objPHD2App.currentState == PHDState.Settling 
                            || ObsControl.objPHD2App.currentState == PHDState.Dithered
                            )
                {
                    /*        
                    Stopped     = 1,
                    StarSelected = 2,
                    Calibrating = 3,
                    Guiding = 4,
                    LostLock = 5,
                    Paused = 6,
                    Looping = 7,
                    Settling = 8,
                    StarLost = 9,
                    Dithered = 10
                    */
                    chkShort_Guiding.BackColor = InterColor;
                }
                else
                {
                    chkShort_Guiding.BackColor = OffColor;
                }

                //If guiding now, CHECK IF NEED TO RISE EVENTS
                if (ObsControl.objPHD2App.currentState == PHDState.Guiding)
                {
                    //If new exposure stats - reset data and start recording
                    if (ObsControl.objCCDCApp.LastExposureStartTime > ObsControl.objPHD2App.LastGuidingStatResetTime)
                    {
                        Guiding_StartRecording();
                    }


                    //Check if new image starts and Guide stats wasn't reset yet
                    if (ObsControl.objCCDCApp.LastExposureEndTime > ObsControl.objPHD2App.LastGuidingStatSaveTime)
                    {
                        Guiding_StopRecording();
                    }
                }


                //Check if any pending events
                if (ObsControl.objPHD2App.CheckProgramEvents())
                {
                    //If guiding now, calclulate and display stats
                    if (ObsControl.objPHD2App.currentState == PHDState.Guiding)
                    {
                        //Add current values to controls
                        if (ObsControl.objPHD2App.curImageGuidingStats.LastTimestamp > LastGuideReadDataTimestamp)
                        {
                            //There are new values!

                            //Get all values since last read
                            List<Tuple<double, double, double>> LastGuideValues;// = new List<Tuple<double, double, double>>();
                            LastGuideValues = ObsControl.objPHD2App.curImageGuidingStats.GetLastValuesSince(LastGuideReadDataTimestamp);

                            //display in numbers last values
                            txtGuiderErrorPHD.Text = ObsControl.objPHD2App.curImageGuidingStats.GetLastNValuesSt(10);

                            //draw 
                            foreach (Tuple<double, double, double> el in LastGuideValues)
                            {
                                string sername = "SeriesGuideError";
                                double XVal = el.Item2;
                                double YVal = el.Item3;
                                if (Math.Abs(XVal) > 1)
                                {
                                    XVal = 2 * Math.Sign(XVal); sername = "SeriesGuideErrorOutOfScale";
                                }
                                if (Math.Abs(YVal) > 1)
                                {
                                    YVal = 2 * Math.Sign(YVal); sername = "SeriesGuideErrorOutOfScale";
                                }
                                //add pount
                                chart1.Series[sername].Points.AddXY(XVal, YVal);

                                // Keep a constant number of points by removing them from the left
                                if (chart1.Series[sername].Points.Count > maxNumberOfPointsInChart)
                                {
                                    chart1.Series[sername].Points.RemoveAt(0);
                                }
                            }
                            // Adjust Y & X axis scale
                            chart1.ResetAutoValues();
                        }

                        //display RMS
                        txtRMS_X.Text = Math.Round(ObsControl.objPHD2App.curImageGuidingStats.RMS_X, 2).ToString();
                        txtRMS_Y.Text = Math.Round(ObsControl.objPHD2App.curImageGuidingStats.RMS_Y, 2).ToString();
                        txtRMS.Text = Math.Round(ObsControl.objPHD2App.curImageGuidingStats.RMS, 2).ToString();

                        //dispaly in short form
                        txtShort_curRMS.Text = Math.Round(ObsControl.objPHD2App.curImageGuidingStats.RMS_X, 2).ToString() + "/" + Math.Round(ObsControl.objPHD2App.curImageGuidingStats.RMS_Y, 2).ToString() + "/" + Math.Round(ObsControl.objPHD2App.curImageGuidingStats.RMS, 2).ToString();

                        //Dispaly other stats
                        txtGuideStat_SinceReset.Text = (DateTime.Now - ObsControl.objPHD2App.curImageGuidingStats.StartDataReceiving).TotalSeconds.ToString("N0") + " c";
                        txtGuideStat_CountPoints.Text = ObsControl.objPHD2App.curImageGuidingStats.ErrorsList.Count.ToString();
                    }
                }
            }
            else
            {
                txtPHDState.Text = "Not running";
                chkShort_Guiding.BackColor = DefaultBackColor;
            }
        }


        private void Guiding_StartRecording()
        {
            //Reset statistics
            ObsControl.objPHD2App.curImageGuidingStats.Reset();
            ObsControl.objPHD2App.LastGuidingStatResetTime = DateTime.Now;

            //Reset displayed data
            chart1.Series["SeriesGuideError"].Points.Clear();
            chart1.Series["SeriesGuideErrorOutOfScale"].Points.Clear();

            txtRMS_X.Text = "";
            txtRMS_Y.Text = "";
            txtRMS.Text = "";

            txtGuiderErrorPHD.AppendText("---new---" + Environment.NewLine);
        }

        private void Guiding_StopRecording()
        {
            //Add current guiding stats to list
            GuidingStats saveCurGuidingStats = new GuidingStats();
            saveCurGuidingStats.Copy(ObsControl.objPHD2App.curImageGuidingStats);
            ObsControl.objPHD2App.prevImageGuidingStats.Add(saveCurGuidingStats);

            //Reset current stats
            ObsControl.objPHD2App.curImageGuidingStats.Reset();
            ObsControl.objPHD2App.LastGuidingStatSaveTime = DateTime.Now;

            //display prev frame RMS
            if (ObsControl.objPHD2App.prevImageGuidingStats.Count > 0)
            {
                int j = 0;
                for (int i = ObsControl.objPHD2App.prevImageGuidingStats.Count-1; i >= 0; i--)
                {
                    j++;
                    if (j==1)
                    { 
                        txtRMS_X_prevframe3.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_X, 2).ToString();
                        txtRMS_Y_prevframe3.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_Y, 2).ToString();
                        txtRMS_prevframe3.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS, 2).ToString();

                        //dispaly in short form
                        txtShort_prevRMS.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_X, 2).ToString() + "/" + Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_Y, 2).ToString() + "/" + Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS, 2).ToString();

                    }
                    else if (j == 2)
                    {
                        txtRMS_X_prevframe2.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_X, 2).ToString();
                        txtRMS_Y_prevframe2.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_Y, 2).ToString();
                        txtRMS_prevframe2.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS, 2).ToString();
                    }
                    else if (j == 3)
                    {
                        txtRMS_X_prevframe3.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_X, 2).ToString();
                        txtRMS_Y_prevframe3.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS_Y, 2).ToString();
                        txtRMS_prevframe3.Text = Math.Round(ObsControl.objPHD2App.prevImageGuidingStats[i].RMS, 2).ToString();
                    }

                }
            }


            txtGuiderErrorPHD.AppendText("---end---" + Environment.NewLine);
        }


        // Guiding interface events 
        #region //// Guiding interface events  //////////////////////////////////////////////////
        private void btnGuiderConnect_Click(object sender, EventArgs e)
        {
            if (ObsControl.objPHD2App.IsRunning())
            {
                ObsControl.objPHD2App.CMD_ConnectEquipment(); //connect equipment
            }
        }

        private void btnGuide_Click(object sender, EventArgs e)
        {
            if (ObsControl.objPHD2App.IsRunning())
            {
                ObsControl.objPHD2App.CMD_StartGuiding(); //start  quiding
            }
        }

        private void btnClearGuidingStat_Click(object sender, EventArgs e)
        {
            txtGuiderErrorPHD.Text = "";
            ObsControl.objPHD2App.curImageGuidingStats.Reset();

            chart1.Series["SeriesGuideError"].Points.Clear();
            chart1.Series["SeriesGuideErrorOutOfScale"].Points.Clear();

            txtRMS_X.Text = "";
            txtRMS_Y.Text = "";
            txtRMS.Text = "";

        }


        private void chkShort_Guiding_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion // Guiding interface events ////////////////////////////////////////////////
        // End of Guiding interface events 









    }
}