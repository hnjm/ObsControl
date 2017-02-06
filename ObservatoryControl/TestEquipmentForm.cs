using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ObservatoryCenter
{
    public partial class TestEquipmentForm : Form
    {
        public MainForm MainFormLink;
        public TestEquipmentClass TestEquipmentObj;

        public Dictionary<string, TestSequenceElement> TestSequence;

        //Constructor
        public TestEquipmentForm(MainForm MF)
        {
            MainFormLink = MF;
            InitializeComponent();

            TestSequence = new Dictionary<string, TestSequenceElement>();
        }

        //Load event
        private void TestEquipment_Load(object sender, EventArgs e)
        {
            TestEquipmentObj = new TestEquipmentClass(MainFormLink.ObsControl);
            InitTestSequence();
        }

        //Prevent from unloading the form
        private void TestEquipment_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker_test.CancelAsync();

            e.Cancel = true;
            this.Hide();
        }

        //Initialize test sequence
        public void InitTestSequence()
        {
            //MaximDL sequences
            TestSequence.Add("Maxim Run", new TestSequenceElement { ChkBox = chkTestMaximDl_Run, Proc = TestEquipmentObj.TestMaximDLRun });
            TestSequence.Add("Maxim Camera", new TestSequenceElement { ChkBox = chkTestMaximDl_cameraconnect, Proc = TestEquipmentObj.TestMaximDLCamera });
            TestSequence.Add("Maxim Telescope", new TestSequenceElement { ChkBox = chkTestMaximDl_telescopeconnect, Proc = TestEquipmentObj.TestMaximDLTelescope });
            TestSequence.Add("Maxim Setcooling", new TestSequenceElement { ChkBox = chkTestMaximDl_setcooling, Proc = TestEquipmentObj.TestMaximDLCooling });
            TestSequence.Add("Maxim Shoot", new TestSequenceElement { ChkBox = chkTestMaximDl_CameraShoot, Proc = TestEquipmentObj.TestMaximDLShoot });
            TestSequence.Add("Maxim Filter", new TestSequenceElement { ChkBox = chkTestMaximDl_FilterWheel, Proc = TestEquipmentObj.TestMaximDLFilterWheel });
            
            //PHP sequences
            TestSequence.Add("PHD Run", new TestSequenceElement { ChkBox = chkTestPHD_run, Proc = TestEquipmentObj.TestPHD2Run });
            TestSequence.Add("PHD Connect", new TestSequenceElement { ChkBox = chkTestPHD_connect, Proc = TestEquipmentObj.TestPHD2Connect });
            TestSequence.Add("PHD Guiding", new TestSequenceElement { ChkBox = chkTestPHD_guiding, Proc = TestEquipmentObj.TestPHD2Guide });
  

            //CdC sequences
            TestSequence.Add("CdC Run", new TestSequenceElement { ChkBox = chkTestCdC_run, Proc = TestEquipmentObj.TestCdCRun });
            TestSequence.Add("CdC Connect", new TestSequenceElement { ChkBox = chkTestCdC_connect, Proc = TestEquipmentObj.TestCdCConnect });
            
            //FM sequences
            TestSequence.Add("FM Run", new TestSequenceElement { ChkBox = chkTestFM_run, Proc = TestEquipmentObj.TestFMRun });
            TestSequence.Add("FM FocuserMove", new TestSequenceElement { ChkBox = chkTestFM_focusermove, Proc = TestEquipmentObj.TestFMFocuserMove});
      
            //TTC sequences
            TestSequence.Add("TTC Run", new TestSequenceElement { ChkBox = chkTTC_run, Proc = TestEquipmentObj.TestTTCRun });
            TestSequence.Add("TTC SetFan", new TestSequenceElement { ChkBox = chkTTC_fan, Proc = TestEquipmentObj.TestTTCTestFan });
            TestSequence.Add("TTC SetHeater", new TestSequenceElement { ChkBox = chkTTC_heating, Proc = TestEquipmentObj.TestTTCTestHeater });

            //calc progressbar
            progressBar1.Maximum = TestSequence.Count();
        }


        /// <summary>
        /// Start test button pressed
        /// </summary>
        private void btnRunObservatoryTest_Click(object sender, EventArgs e)
        {
            //Reset checkbox state
            foreach (TestSequenceElement TestSeqEl in TestSequence.Values)
            {
                TestSeqEl.ChkBox.CheckState = CheckState.Indeterminate;
            }

            //Clear log
            txtTestFormLog.Clear();

            //Run test sequence in background
            backgroundWorker_test.RunWorkerAsync();
        }


        /// <summary>
        /// Main method to run in background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_test_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            TestResultClass TestRes; //To receive result from test function

            foreach (TestSequenceElement TestSeqEl in TestSequence.Values)
            {
                if (!backgroundWorker_test.CancellationPending)
                { 
                    //run test procedure
                    TestRes = TestSeqEl.Proc();
                    //report it result
                    backgroundWorker_test.ReportProgress(++i, new TestResultUserStateClass(){TestResult = TestRes, TestSequenceLink = TestSeqEl });
                }
                else
                {
                    //break signaled
                    Logging.AddLog("TestEquipment was interrupted by user", LogLevel.Activity);
                    break;
                }
            }
        }

        /// <summary>
        /// Report progress change
        /// </summary>
        /// <param name="e">e.ProgressPercentage - which is reported directly by backgroundWorker1.ReportProgress(i);</param>
        private void backgroundWorker_test_ProgressChanged_1(object sender, ProgressChangedEventArgs e)
        {
            // Report the result log
            foreach (string St in ((TestResultUserStateClass)e.UserState).TestResult.UserOutput)
            {
                txtTestFormLog.AppendText(St + Environment.NewLine);
            }

            // Change result text box
            ((TestResultUserStateClass)e.UserState).TestSequenceLink.ChkBox.Checked = ((TestResultUserStateClass)e.UserState).TestResult.res;
            if (((TestResultUserStateClass)e.UserState).TestResult.res)
            {
                ((TestResultUserStateClass)e.UserState).TestSequenceLink.ChkBox.CheckState = CheckState.Checked;
            }
            else
            {
                ((TestResultUserStateClass)e.UserState).TestSequenceLink.ChkBox.CheckState = CheckState.Unchecked;
            }
            // Change the value of the ProgressBar to the progress.
            progressBar1.Value = e.ProgressPercentage;

            // Set the text.
            //this.Text = e.ProgressPercentage.ToString();
        }
    }
}
