using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;



namespace Stahli2Robots
{
    public partial class FrmTitle : Form
    {
        public FrmTitle()
        {
            InitializeComponent();
            
            // delegats:
            loadOrderDataDelegate = new LoadOrderDataDelegate(LoadOrderDataDelegateFunc);
            updateFrmTitleDelegate = new UpdateFrmTitleDelegate(UpdateFrmTitleDelegateFunc);
            afterModeChangedDelegate = new AfterModeChangedDelegate(AfterModeChangedDelegateFunc);

            series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.Name = "Height Results";
            series1.Color = System.Drawing.Color.Black;
            series1.ChartType = SeriesChartType.FastPoint;
            series1.IsXValueIndexed = true;
           // series1.Points(10, 10);

            //chrtInsertMaesures.Titles.Add("Inserts Height Results:");
            chrtInsertMaesures.Series.Clear();
            chrtInsertMaesures.Width = 400;
            chrtInsertMaesures.ChartAreas[0].AxisX.IsStartedFromZero = true;
            chrtInsertMaesures.ChartAreas[0].AxisX.ScaleView.Zoom(0, 12);
            chrtInsertMaesures.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            chrtInsertMaesures.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chrtInsertMaesures.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = 2;          

            chrtInsertMaesures.ChartAreas[0].AxisY.IsStartedFromZero = false;          
            chrtInsertMaesures.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chrtInsertMaesures.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chrtInsertMaesures.ChartAreas[0].AxisY.Interval = 10;
            chrtInsertMaesures.ChartAreas[0].AxisX.Interval = 1;



            chrtInsertMaesures.Series.Add(series1);
   
            stCorrHigh2 = new System.Windows.Forms.DataVisualization.Charting.StripLine{};
            stCorrHigh1 = new System.Windows.Forms.DataVisualization.Charting.StripLine{};
            stTolHigh = new System.Windows.Forms.DataVisualization.Charting.StripLine{};
            stTarget = new System.Windows.Forms.DataVisualization.Charting.StripLine{};
            stTolLow = new System.Windows.Forms.DataVisualization.Charting.StripLine{};
            stCorrLow1 = new System.Windows.Forms.DataVisualization.Charting.StripLine{};
            stCorrLow2 = new System.Windows.Forms.DataVisualization.Charting.StripLine {};


            stCorrHigh2.Interval = 0;         
            stCorrHigh2.StripWidth = 0.001;
            stCorrHigh2.BackColor = Color.Red;

            stCorrHigh1.Interval = 0;
            stCorrHigh1.StripWidth = 0.001;
            stCorrHigh1.BackColor = Color.Blue;

            stTolHigh.Interval = 0;
            stTolHigh.StripWidth = 0.001;
            stTolHigh.BackColor = Color.Green;

            stTarget.Interval = 0;
            stTarget.StripWidth = 0.0015;
            stTarget.BackColor = Color.Gold;

            stTolLow.Interval = 0;
            stTolLow.StripWidth = 0.001;
            stTolLow.BackColor = Color.Green;

            stCorrLow1.Interval = 0;
            stCorrLow1.StripWidth = 0.001;
            stCorrLow1.BackColor = Color.Blue;

            stCorrLow2.Interval = 0;
            stCorrLow2.StripWidth = 0.001;
            stCorrLow2.BackColor = Color.Red;

        }

        private void ____cmdLoadStahliIO_Click(object sender, EventArgs e)
        {
            frmStahliIO.Show();
        }
        private void ____cmdLoadConv_Click(object sender, EventArgs e)
        {
            //frmAssemblies.Show();
            AppGen.Inst.MDImain.frmAssemblies.Show();
        }
        private void ____cmdLoadRobot_Click(object sender, EventArgs e)
        {
            //frmRobots.Show();
            AppGen.Inst.MDImain.frmRobots.Show();
        }
        private void ____cmdUnloadConv_Click(object sender, EventArgs e)
        {
            //frmAssemblies.Show();
            AppGen.Inst.MDImain.frmAssemblies.Show();
        }
        private void ____cmdUnloadRobot_Click(object sender, EventArgs e)
        {
            //frmRobots.Show();
            AppGen.Inst.MDImain.frmRobots.Show();
        }
        private void ____cmdIndexTable_Click(object sender, EventArgs e)
        {
            //frmAssemblies.Show();
            AppGen.Inst.MDImain.frmAssemblies.Show();
        }
   
        // members (משתנים מקומיים)


        // properties (משתנים שניגשים אליהם מבחוץ)
        public FrmAssemblies frmAssemblies;
        public FrmRobots frmRobots;
        public FrmStahliIO frmStahliIO;



        private void cmdSatrtButton_Click(object sender, EventArgs e)             // Running;
        {
            AppGen.Inst.MainCycle.RunMainCycle();    
        }
        private void cmdPauseButton_Click(object sender, EventArgs e)             // Pause;
        {
            AppGen.Inst.MainCycle.PauseMainCycle();
        }
        private void cmdStoptButton_Click(object sender, EventArgs e)             // Stop;
        {          
            AppGen.Inst.MainCycle.StopAll();
        }
        private void cmdResetAllButton_Click(object sender, EventArgs e)          //Reset;
        {           
            AppGen.Inst.MainCycle.ResetAll();
        }
        private void cmdCleanLineButton_Click(object sender, EventArgs e)         //CleanLine
        {
            AppGen.Inst.MainCycle.CleanLine();
        }
        private void button16_Click(object sender, EventArgs e)  //go to setup (next)
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
        }
        private void button4_Click(object sender, EventArgs e)   //prev
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];
        }
        private void button3_Click(object sender, EventArgs e)  //next
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
        }
        private void button2_Click(object sender, EventArgs e)  //prev
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage2"];
        }
        private void button1_Click(object sender, EventArgs e)  //next
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage4"];
        }
        private void cmdPrevTab_Click(object sender, EventArgs e)  //prev
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage3"];
        }
        private void cmdBackToManualMode_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_StopCycleResetAll = true;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_StopCycleResetAll = true;
            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_StopCycleResetAll, true);
            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_StopCycleResetAll, true);

            System.Threading.Thread.Sleep(500);
            AppGen.Inst.MainCycle.StopAll();

        }
        private void cmdNextTab_Click(object sender, EventArgs e)  //Start production
        {
            tabControl1.SelectedTab = tabControl1.TabPages["tabPage1"];  //to main screen 
            AppGen.Inst.MainCycle.RunMainCycle();         
        }

        private void cmdResumeGeneral_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MainCycle.ResumGeneral();
        }
        private void cmdResumeLoadConv_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MainCycle.ResumLoadConv();          
        }
        private void cmdResumeIndexTable_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MainCycle.ResumIndexTable();          
        }
        private void cmdResumeUnloadConv_Click(object sender, EventArgs e)
        {           
            AppGen.Inst.MainCycle.ResumUnloadConv();
            AppGen.Inst.MainCycle.ResumeOnce = true;  // for one time to keep going with unloading even when counter limit of miss vision is not leagel
        }
        private void cmdResumeRobots_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MainCycle.ResumRobots();            
        }
        private void cmdResumeCameras_Click(object sender, EventArgs e)
        {  
            AppGen.Inst.MainCycle.ResumCameras();
        }
        private void cmdSetupAll_Click(object sender, EventArgs e)
        {           
            //cmdSetupAll.Enabled = false;
            //cmdStopSetup.Enabled = true;
            lblMsg.Text = "Performing General Setup, Please wait !!";
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "7");
            AppGen.Inst.MainCycle.MainControl.bRunSetup = true;

            if (chkFirstLoadOrder.Checked)
            {
                if (!chkOperatorHeightConfermation.Checked)
                {
                    lblMsg.Text = "Confirm correct Height Sensor and check box!!";
                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "5");
                    return;
                }
                chkOperatorHeightConfermation.Checked = false;
                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.S3 = true;
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hS3, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.S3);

                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ProdMode = 1;
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hProdMode, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ProdMode);
                chkFirstLoadOrder.Checked = false;           
            }
            
            AppGen.Inst.MainCycle.MainControl.bSetupDone = fbSetupAll(null, null);   //<<<<<<<<<<<<<<!!!!!>>>>>>>>>>>>>>>>>>>           
            if (AppGen.Inst.MainCycle.MainControl.bSetupDone)
            {
                AppGen.Inst.MainCycle.MainProccesState = ProcessStatus.SetupSucces;
                AppGen.Inst.MDImain.AfterModeChanged();
                lblMsg.Text = "Setup Finish Successfully !!";
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "6");
                //cmdNextTab.Enabled = true;
                //cmdBackToManualMode.Enabled = true;
                //cmdSetupAll.Enabled = false;
                AppGen.Inst.MDImain.frmTitle.ActiveOperationButtons();
            }
            else
            {
                lblMsg.Text = "Setup Faild !! !!";
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "5");
                ____cmdNextTab.Enabled = false;
                //cmdBackToManualMode.Enabled = false;
                //cmdSetupAll.Enabled = true;
            }
            //cmdStopSetup.Enabled = false;



    ///------------Led status of Setup result : ---------------
            if ((AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone) && (AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone2))
	        {
		         setupResultGoodLed0.Visible = true;
                 setupResultFailLed0.Visible = false;
	        }
            else
            {
                setupResultGoodLed0.Visible = false;
                setupResultFailLed0.Visible = true;
            }
            if ((AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone) && (AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone2))
            {
                setupResultGoodLed2.Visible = true;
                setupResultFailLed2.Visible = false;
            }
            else
            {
                setupResultGoodLed2.Visible = false;
                setupResultFailLed2.Visible = true;
            }
            if (AppGen.Inst.RobotConnection.stLoadRobotControl.bDone)
            {
                setupResultGoodLed4.Visible = true;
                setupResultFailLed4.Visible = false;
            }
            else
            {
                setupResultGoodLed4.Visible = false;
                setupResultFailLed4.Visible = true;
            }
            if (AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone)
            {
                setupResultGoodLed5.Visible = true;
                setupResultFailLed5.Visible = false;
            }
            else
            {
                setupResultGoodLed5.Visible = false;
                setupResultFailLed5.Visible = true;
            }
            if (AppGen.Inst.MainCycle.IndexTableControl.bSetupDone)
            {
                setupResultGoodLed6.Visible = true;
                setupResultFailLed6.Visible = false;
            }
            else
            {
                setupResultGoodLed6.Visible = false;
                setupResultFailLed6.Visible = true;
            }   
  //-------------------------------------------------------------------------
        }


        private bool fbSetupAll(object sender, EventArgs e)
        {
            try
            {
            bool SetupAll_flag = false;
            AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl = false;
            AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl = false;
            double TimeOutVal = 9000;
            System.Diagnostics.Stopwatch sw_TimeOut = new System.Diagnostics.Stopwatch();
            sw_TimeOut.Start();  // Start The StopWatch ...From 000	              

            //Load/Unload/Step-Table to AutoMode:
            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode = 2;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode);
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode = 2;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode);
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode = 2;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hfl_TableMode, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode);
            //-----------------------------------


            setupResultGoodLed0.Visible = false;
            setupResultGoodLed1.Visible = false;
            setupResultGoodLed2.Visible = false;
            setupResultGoodLed3.Visible = false;
            setupResultGoodLed4.Visible = false;
            setupResultGoodLed5.Visible = false;
            setupResultGoodLed6.Visible = false;
            setupResultFailLed0.Visible = false;
            setupResultFailLed1.Visible = false;
            setupResultFailLed2.Visible = false;
            setupResultFailLed3.Visible = false;
            setupResultFailLed4.Visible = false;
            setupResultFailLed5.Visible = false;
            setupResultFailLed6.Visible = false;

            AppGen.Inst.MainCycle.MainControl.bRun = false; //true;
            AppGen.Inst.MainCycle.MainControl.bPause = false;
            AppGen.Inst.MainCycle.MainControl.bError = false;

            // for set all the uncehcked station to TRUE (without checking status):
            if (chkLoadConvSetup.Checked)
	        {
                AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone = false; 
	        } 
            else
	        {
                AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone = true;
	        }
            if (chkLoadConv.Checked)
            {
                AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone2 = false;
            }
            else
            {
                AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone2 = true;
            }
            if (chkUnloadConvSetup.Checked)
	        {
                AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone = false; 
	        } 
            else
	        {
                AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone = true;
	        }
            if (chkUnloadConv.Checked)
            {
                AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone2 = false;
            }
            else
            {
                AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone2 = true;
            }
            if (chkLoadRobotSetup.Checked)
	        {
                AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = false;
	        } 
            else
	        {
                AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = true;
	        }
            if (chkUnloadRobotSetup.Checked)
	        {
                AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = false;
	        } 
            else
	        {
                AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = true;
	        }
            if (chkIndexTableSetup.Checked)
	        {
                AppGen.Inst.MainCycle.IndexTableControl.bSetupDone = false; 
	        } 
            else
	        {
                AppGen.Inst.MainCycle.IndexTableControl.bSetupDone = true;
	        }

            AppGen.Inst.MainCycle.LoadCycleControl.bDone = true;
            AppGen.Inst.MainCycle.UnloadCycleControl.bDone = true;

            //request tray replace if Conveyors checked for setup
            if (chkLoadConvSetup.Checked)
	        {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Req_ReplaceTray = true;
                //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_Req_ReplaceTray , true);
                AppGen.Inst.LoadTray.CurrIndex = 0;
                AppGen.Inst.MDImain.frmTitle.progBarLoadTray.Value = 0;
	        }         
            if (chkUnloadConvSetup.Checked)
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Req_ReplaceTray = true;
                //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_Req_ReplaceTray, true);
                AppGen.Inst.UnLoadTray.CurrIndex = 0;
                AppGen.Inst.MDImain.frmTitle.progBarUnloadTray.Value = 0;
            }


            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            //Loop waiting for trays replace done and IndexTable ready (in case of request init conv):
            while ((AppGen.Inst.MainCycle.MainControl.bRunSetup) && (!SetupAll_flag))                
            {
                System.Threading.Thread.Sleep(500);
                if (chkLoadConvSetup.Checked)
                {
                    AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone = AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_TrayReady;
                    if (AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone)
                    {
                        setupResultGoodLed0.Visible = true;
                    }
                }
                if (chkLoadConv.Checked)
                {
                    AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone2 = AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvReady;
                    if (AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone2)
                    {
                        setupResultGoodLed1.Visible = true;
                    }
                }
                if (chkUnloadConvSetup.Checked)
                {
                    AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone = AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_TrayReady;
                    if (AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone)
                    {
                        setupResultGoodLed2.Visible = true;
                    }              
                }
                if (chkUnloadConv.Checked)
                {
                    AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone2 = AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvReady;
                    if (AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone2)
                    {
                        setupResultGoodLed3.Visible = true;
                    }
                }
                if (chkIndexTableSetup.Checked)
                {
                    AppGen.Inst.MainCycle.IndexTableControl.bSetupDone = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_StepConvReady;
                    if (AppGen.Inst.MainCycle.IndexTableControl.bSetupDone)
                    {
                        setupResultGoodLed6.Visible = true;
                    }
                }

                if ((chkLoadRobotSetup.Checked) || (chkUnloadRobotSetup.Checked))        
	            {
                    AppGen.Inst.VisionParam.LastTimerBit1 = false;
                    AppGen.Inst.VisionParam.LastTimerBit2 = false;
                    AppGen.Inst.VisionParam.LastTimerBit3 = false;
                    tmrHiTimerSnap.Start();

                    if (!AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl)
                    {
                        if (chkLoadRobotSetup.Checked)         ///////////////robot1 to Auto-Mode://///////////////
                        {
                            AppGen.Inst.MainCycle.LoadCycleControl.bDone = false;
                            AppGen.Inst.MainCycle.LoadCycleControl.bPause = false;
                            AppGen.Inst.MainCycle.LoadCycleControl.bError = false;
                            AppGen.Inst.MainCycle.LoadCycleControl.bRun = false;
                            //////
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqPauseCycle = false;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_DataSended = false;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_StopCycleResetAll = false;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqResumeCycle = false;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_FoundOrientation = false;                            
                            //////
                            if (chkSetupFlag.Checked)
                            {
                                AppGen.Inst.MainCycle.SetupDelays = Convert.ToDouble(txtSetupDelay.Text);
                            }
                            else
	                        {
                                AppGen.Inst.MainCycle.SetupDelays = 0;
	                        }

                            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, "70," + AppGen.Inst.MainCycle.PlaceZero.ToString() + "," + AppGen.Inst.MainCycle.PickSensor.ToString() + "," + AppGen.Inst.MainCycle.SetupDelays.ToString());  // tbd: 0,0  is placeZero, PickSensor
                            AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = true;

                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();  // Start The StopWatch ...From 000	              
                            while (!AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (sw.ElapsedMilliseconds > 3000)
                                {
                                    sw.Stop();
                                    AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = false;
                                    break;
                                }
                            }
                            sw.Stop();       //Stop the Timer
                            if (AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl)
                            {
                                setupResultGoodLed4.Visible = true;
                            }
                            else
                            {
                                setupResultFailLed4.Visible = true;
                            }
                            AppGen.Inst.MainCycle.LoadCycleControl.bRobotActive = true;
                            AppGen.Inst.MainCycle.LoadCycleControl.bRun = true;
                            AppGen.Inst.MainCycle.LoadCycleControl.bDone = false;
                            AppGen.Inst.MainCycle.LoadCycleControl.bPause = false;
                            AppGen.Inst.MainCycle.LoadCycleControl.bError = false;
                            AppGen.Inst.MainCycle.LoadCycleControl.iStep = 0;
                            AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.RunAgainFlag = false;
                        }
                        else
                        {
                            AppGen.Inst.MainCycle.LoadCycleControl.bRobotActive = false;
                        }
                    }
                    if (!AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl)  ///////////////robot2 to Auto-Mode://///////////////
                    {
                        if (chkUnloadRobotSetup.Checked)         
                        {
                            AppGen.Inst.MainCycle.UnloadCycleControl.bDone = false;
                            AppGen.Inst.MainCycle.UnloadCycleControl.bPause = false;
                            AppGen.Inst.MainCycle.UnloadCycleControl.bError = false;
                            AppGen.Inst.MainCycle.UnloadCycleControl.bRun = false;
                            //////
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqPauseCycle = false;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_StopCycleResetAll = false;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqResumeCycle = false;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_FoundInserts = false;
                            //AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
                            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_ReqPauseCycle , false);
                            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_StopCycleResetAll , false);
                            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_ReqResumeCycle , false);
                            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_FoundInserts , false);
                            //////
                            if (chkSetupFlag.Checked)
                            {
                                AppGen.Inst.MainCycle.SetupDelays = Convert.ToDouble(txtSetupDelay.Text);
                            }
                            else
                            {
                                AppGen.Inst.MainCycle.SetupDelays = 0;
                            }

                            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, "70," + AppGen.Inst.MainCycle.SetupDelays.ToString());
                            AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = true;

                            //boolean breakFlag = false;
                            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                            sw.Start();  // Start The StopWatch ...From 000	              
                            while (!AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl)
                            {
                                System.Threading.Thread.Sleep(100);
                                if (sw.ElapsedMilliseconds > 3000)
                                {
                                    sw.Stop();
                                    AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = false;
                                    break;
                                }
                            }
                            sw.Stop();       //Stop the Timer
                            if (AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl)
                            {
                                setupResultGoodLed5.Visible = true;
                            }
                            else
                            {
                                setupResultFailLed5.Visible = true;
                            }
                            AppGen.Inst.MainCycle.UnloadCycleControl.bRobotActive = true;
                            AppGen.Inst.MainCycle.UnloadCycleControl.bRun = true;
                            AppGen.Inst.MainCycle.UnloadCycleControl.bDone = false;
                            AppGen.Inst.MainCycle.UnloadCycleControl.bPause = false;
                            AppGen.Inst.MainCycle.UnloadCycleControl.bError = false;
                            AppGen.Inst.MainCycle.UnloadCycleControl.iStep = 0;
                            AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = false;  //search only in one slice
                        }
                        else
                        {
                            AppGen.Inst.MainCycle.UnloadCycleControl.bRobotActive = false;
                        }
                    }
                }
                //////////////////////////////////////// done robot setup //////

                //AppGen.Inst.MainCycle.MainControl.bRun = false; //true;
                //AppGen.Inst.MainCycle.MainControl.bPause = false;
                //AppGen.Inst.MainCycle.MainControl.bError = false;

                //when all station setupDone, the loop is done (SetupAll_flag = true) 
                SetupAll_flag = ((AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone) && (AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone) &&
                    (AppGen.Inst.MainCycle.LoadCycleControl.bSetupDone2) && (AppGen.Inst.MainCycle.UnloadCycleControl.bSetupDone2) &&
                    (AppGen.Inst.RobotConnection.stLoadRobotControl.bDone) && (AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone) &&
                    (AppGen.Inst.MainCycle.IndexTableControl.bSetupDone));

                //TimeOut (Setup Faild)
                if (sw_TimeOut.ElapsedMilliseconds > TimeOutVal)
                {
                    sw_TimeOut.Stop();
                    AppGen.Inst.MainCycle.MainControl.bRunSetup = false;
                }
            }
            System.Windows.Forms.Cursor.Current = Cursors.Default;
            return SetupAll_flag;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                AppGen.Inst.LogFile(ex.Message, LogType.SystemErr);
                return false;
            }
        }
        private void cmdStopSetup_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MainCycle.MainControl.bRunSetup = false;
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Setup Stoped by User !");
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "5");
        }

        public void ActiveOperationButtons()
        {
            ____cmdStartButton.Enabled = true;
            ____cmdPauseButton.Enabled = true;
            ____cmdStopButton.Enabled = true;
            ____cmdCleanLineButton.Enabled = true;
        }
        public void DeActiveOperationButtons()
        {
            ____cmdStartButton.Enabled = false;
            ____cmdPauseButton.Enabled = false;
            //cmdStopButton.Enabled = false;
            ____cmdCleanLineButton.Enabled = false;
        }
        public void Ramzor(RamzorColor color)
        {
            try
            {
                switch (color)
                {
                    case RamzorColor.Red:
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.RedLight = true;
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.YellowLight = false;
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight = false;                        
                        break;
                    case RamzorColor.Yellow:
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.RedLight = false;
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.YellowLight = true;
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight = false;  
                        break;
                    case RamzorColor.Green:
                         AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.RedLight = false;
                         AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.YellowLight = false;
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight = true;  
                        break;
                }
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hRedLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.RedLight);
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hYellowLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.YellowLight);
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hGreenLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight);
            }
            catch { }
        }
        public void Buzzer(bool State)
        {
            //return;    //cancel temperory!!
            try
            {
                switch (State)
                {
                    case true:  //buzzer On
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Buzzer = true;
                        break;
                    case false:  //buzzer Off
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Buzzer = false;
                        break;                   
                }             
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hBuzzer, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Buzzer);             
            }
            catch { }
        }
        public void tmrHiTimerSnap_Tick(object sender, EventArgs e)
        {
            //no need?  System.Threading.Thread.Sleep(10);  //8.2.15 notrunning worker
            //Call ScanInput
            //InputStatus(0) -->'Load Tray       (robot controller DOUT(1))
            //InputStatus(1) -->'Load Carrier    (robot controller DOUT(2))
            //InputStatus(8) -->'Unload Carrier  (robot controller DOUT(3))         
            
            //****************************************************************************************************************************
            if ((AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_Rob_snap_req_1) && (AppGen.Inst.VisionParam.SNAP_req_fl1 == false) && (AppGen.Inst.VisionParam.LastTimerBit1 == false))
            {
                AppGen.Inst.VisionParam.SNAP_req_fl1 = true;
                //AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam1ClearToSnap, "1");
            }
            if ((AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_Rob_snap_req_1) && (AppGen.Inst.VisionParam.SNAP_req_fl1 == false))
            {
                AppGen.Inst.VisionParam.LastTimerBit1 = true;
            }
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_Rob_snap_req_1 == false)
            {
                AppGen.Inst.VisionParam.LastTimerBit1 = false;
            }
            //AppGen.Inst.VisionParam.LastTimerBit1 = AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Rob_snap_req_1;

         
            //****************************************************************************************************************************
            if ((AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_Rob_snap_req_2 == true) && (AppGen.Inst.VisionParam.LastTimerBit2 == false))
            {
                AppGen.Inst.VisionParam.SNAP_req_fl2 = true;
                //AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam2ClearToSnap, "1");
            }
            AppGen.Inst.VisionParam.LastTimerBit2 = AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_Rob_snap_req_2;
            //****************************************************************************************************************************
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_Rob_snap_req_3 == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam3ClearToSnap, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam3ClearToSnap, "0");
            }
            //if ((AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Rob_snap_req_3 == true) && (AppGen.Inst.VisionParam.LastTimerBit3 == false))
            //{
            //    AppGen.Inst.VisionParam.snap_req_fl3 = true;
            //    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam3ClearToSnap, "1");
            //    //picClearToSnap.BackColor = System.Drawing.Color.LimeGreen;
            //}
            AppGen.Inst.VisionParam.LastTimerBit3 = AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_Rob_snap_req_3;
            //****************************************************************************************************************************
        }

        bool step0 = true;
        bool step1 = true;
        bool step2 = true;
        bool step3 = true;
        bool step4 = true;
        bool step5 = true;
        int UpdateSliceInterval = 4;      
        int LoadIntervalBlink = 0;
        int UnloadIntervalBlink = 0;

        public bool plcInvalidFleg = false;
        int tmpTimeMinute = 0;
        public void tmrChkError_Tick(object sender, EventArgs e)
        {
     /////////////////debug flegs:///////////////////////////////////////////////////////////////
            if (AppGen.Inst.RobotConnection.RL.ConnBusy == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.RLconnbusy, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.RLconnbusy, "0");
            }
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_Rob_snap_req_1 == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.robotdataSnap1, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.robotdataSnap1, "0");
            }
            if (AppGen.Inst.VisionParam.SNAP_req_fl1 == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam1ClearToSnap, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam1ClearToSnap, "0");
            }
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_Rob_snap_req_2 == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.robotdataSnap2, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.robotdataSnap2, "0");
            }
            if (AppGen.Inst.VisionParam.SNAP_req_fl2 == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam2ClearToSnap, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam2ClearToSnap, "0");
            }
            if (AppGen.Inst.RobotConnection.RU.ConnBusy == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.RUconnbusy, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.RUconnbusy, "0");
            }
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_Rob_snap_req_3 == true)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.robotdata2Snap3, "1");
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.robotdata2Snap3, "0");
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////
            

            if (((DateTime.Now.Minute % 10) == 0) && (tmpTimeMinute != (DateTime.Now.Minute)))  //every 10 minutes
            {
                tmpTimeMinute = DateTime.Now.Minute;
                AppGen.Inst.LogFile("Time Frame", LogType.Production);
            }          
            if (plcInvalidFleg) return;
            if (AppGen.Inst.MDImain == null) return;

            //if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
            if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Stop)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ProductionMessage);
            }
        
            //update Palets status from PLC (every 3 Ticks):
            if (UpdateSliceInterval > 3)
            {
               
                if (AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvReady == true) ShapeLoadConvRead.Visible = false; else ShapeLoadConvRead.Visible = true;
                if (AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvReady == true) ShapeUnloadConvRead.Visible = false; else ShapeUnloadConvRead.Visible = true;
                if (AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_StepConvReady == true) ShapeIndexTableRead.Visible = false; else ShapeIndexTableRead.Visible = true;
              
                if (AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl == false) ShapeLoadRobot.Visible = false; else ShapeLoadRobot.Visible = true;
                if (AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl == false) ShapeUnloadRobot.Visible = false; else ShapeUnloadRobot.Visible = true;

                UpdateSliceStateImages();
                UpdateSliceInterval = 0;

                //tray/carrier led status  (ready/not ready): 
                if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_TrayReady)
                {
                    SapeLoadTrayReady.BackgroundImage = Properties.Resources.circle_green4;
                }
                else
                {
                    SapeLoadTrayReady.BackgroundImage = Properties.Resources.circle_red5;
                }
                if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_CarrReady)
                {
                    SapeLoadCarrierReady.BackgroundImage = Properties.Resources.circle_green4;
                }
                else
                {
                    SapeLoadCarrierReady.BackgroundImage = Properties.Resources.circle_red5;
                }
                if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_TrayReady)
                {
                    SapeUnloadTrayReady.BackgroundImage = Properties.Resources.circle_green4;
                }
                else
                {
                    SapeUnloadTrayReady.BackgroundImage = Properties.Resources.circle_red5;
                }
                if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrReady)
                {
                    SapeUnloadCarrierReady.BackgroundImage = Properties.Resources.circle_green4;
                }
                else
                {
                    SapeUnloadCarrierReady.BackgroundImage = Properties.Resources.circle_red5;
                } 
 
                //japan  30.6.15
                if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.StahliOutAutomationReady)
                {
                    sapeAutomationReady.BackgroundImage = Properties.Resources.circle_green4;
                }
                else
                {
                    sapeAutomationReady.BackgroundImage = Properties.Resources.circle_grey0;
                }
                if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.StahliInReadyForUnload)
                {
                    sapaStahliInReadyForUnload.BackgroundImage = Properties.Resources.circle_green4;
                }
                else
                {
                    sapaStahliInReadyForUnload.BackgroundImage = Properties.Resources.circle_grey0;
                }

                
                  
            }
            UpdateSliceInterval += 1;




            //robots mode (Auto/Manual):
            if (AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl)
            {
                LoadIntervalBlink += 1;
                ____cmdLoadRobot.Text = "Auto!";
                if (LoadIntervalBlink > 2)
                {
                    LoadIntervalBlink = 0;
                    ____cmdLoadRobot.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                else
                {
                    ____cmdLoadRobot.BackColor = System.Drawing.Color.DarkTurquoise;
                }              
            }
            else
            {
                ____cmdLoadRobot.BackColor = System.Drawing.Color.WhiteSmoke;
                ____cmdLoadRobot.Text = "Robot 1";
            }

            if (AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl)
            {
                UnloadIntervalBlink += 1;
                ____cmdUnloadRobot.Text = "Auto!";
                if (UnloadIntervalBlink > 2)
                {
                    UnloadIntervalBlink = 0;
                    ____cmdUnloadRobot.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                else
                {
                    ____cmdUnloadRobot.BackColor = System.Drawing.Color.DarkTurquoise;
                }
            }
            else
            {
                ____cmdUnloadRobot.BackColor = System.Drawing.Color.WhiteSmoke;
                ____cmdUnloadRobot.Text = "Robot 2";
            }

            //---------------------------------General Warning:---------------------------------------  Japan  01.07.15
            short tmpGeneralWarning = 0;
            if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Stop)
            {
                tmpGeneralWarning = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GeneralWarningCount;
            }
            if ((tmpGeneralWarning > 0) && (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight))
            {
                //turn on Yellow and Green  lights
                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.YellowLight = true;
                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight = true;
                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.RedLight = false;
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hRedLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.RedLight);
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hYellowLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.YellowLight);
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hGreenLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight);

                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                }
            }
            if ((tmpGeneralWarning == 0) && (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GreenLight) && (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.YellowLight))
            {
                Ramzor(RamzorColor.Green);
                Buzzer(false);
            }

            //---------------------------------General Errors(New 18.02.15!):---------------------------------------  //qq

            //General_PlcErrMsg
            short tmpErrGeneral = 0;
            if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Stop)
            {
                tmpErrGeneral = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.GeneralErrorCount;     
            }
            if (tmpErrGeneral > 0)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg1);                
                AppGen.Inst.MDImain.frmTitle.cmdErrForm.BackColor = System.Drawing.Color.OrangeRed;
                tmrBlinkResume0.Start();
                Ramzor(RamzorColor.Red);
                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                    PauseAll(); //Japan 01.07.15
                }
                if (!AppGen.Inst.MainCycle.MainControl.ErrLoged)
                {
                    AppGen.Inst.LogFile(AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg1, LogType.GeneralErr, LogStation.General);
                    AppGen.Inst.MainCycle.MainControl.ErrLoged = true;
                }
            }
            else
            {
                if (AppGen.Inst.MainCycle.MainControl.bError)  //Dont delete messege, in case error present from Automation side
                {
                    tmrBlinkResume0.Start();
                    Ramzor(RamzorColor.Red);
                    if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                    {
                        Buzzer(true);
                        PauseAll(); //Japan 01.07.15
                    }
                }
                else
                {
                     tmrBlinkResume1.Stop();
                    AppGen.Inst.MDImain.frmTitle.cmdResumeGeneral.BackColor = System.Drawing.Color.WhiteSmoke;
                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblStahliMsg, "");  //(general label)
                }
            }
           


            //---------------------------------Load Conveyor Errors:---------------------------------------
            short tmpErrLoad = 0;
            if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Stop)
            {
                tmpErrLoad = AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ConvErrorCount;
            }
            if (tmpErrLoad > 0)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg1);                          
                AppGen.Inst.MDImain.frmTitle.cmdErrForm.BackColor = System.Drawing.Color.OrangeRed;
                tmrBlinkResume1.Start();
                Ramzor(RamzorColor.Red);
                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                    PauseAll(); //Japan 01.07.15
                }
                if (!AppGen.Inst.MainCycle.LoadCycleControl.ErrLoged)
                {
                    AppGen.Inst.LogFile(AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg1, LogType.GeneralErr, LogStation.LoadConv);
                    AppGen.Inst.MainCycle.LoadCycleControl.ErrLoged = true;
                }
            }
            else
            {
                if (AppGen.Inst.MainCycle.LoadCycleControl.bError)  //Dont delete messege, in case error present from Automation side
                {
                    tmrBlinkResume1.Start();
                    Ramzor(RamzorColor.Red);
                    if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                    {
                        Buzzer(true);
                        PauseAll(); //Japan 01.07.15
                    }
                }
                else
                {
                    tmrBlinkResume1.Stop();
                    AppGen.Inst.MDImain.frmTitle.cmdResumeLoadConv.BackColor = System.Drawing.Color.WhiteSmoke;
                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, "");
                }
            }
            AppGen.Inst.MDImain.frmAssemblies.UpdateFrmAssemblies(FrmAssembliesData.LoadConvErrMsg, AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg1);

            //---------------------------------Unload Conveyor Errors:---------------------------------------
            short tmpErrUnload = 0;
            if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Stop)
            {
                tmpErrUnload = AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ConvErrorCount;
            }
            if (tmpErrUnload > 0)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblUnloadConvMsg, AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg1);             
                AppGen.Inst.MDImain.frmTitle.cmdErrForm.BackColor = System.Drawing.Color.OrangeRed;
                tmrBlinkResume3.Start();
                Ramzor(RamzorColor.Red);  
                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                    PauseAll(); //Japan 01.07.15
                }
                if (!AppGen.Inst.MainCycle.UnloadCycleControl.ErrLoged)
                {
                    AppGen.Inst.LogFile(AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg1, LogType.GeneralErr, LogStation.UnloadConv);
                    AppGen.Inst.MainCycle.UnloadCycleControl.ErrLoged = true;
                }
                
            }
            else
            {
                if (AppGen.Inst.MainCycle.UnloadCycleControl.bError)  // In case error present from Automation side
                {
                    tmrBlinkResume3.Start();
                    Ramzor(RamzorColor.Red);
                    if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                    {
                        Buzzer(true);
                        PauseAll(); //Japan 01.07.15
                    }
                }
                else
                {
                    tmrBlinkResume3.Stop();
                    AppGen.Inst.MDImain.frmTitle.cmdResumeUnloadConv.BackColor = System.Drawing.Color.WhiteSmoke;
                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblUnloadConvMsg, "");
                }
            }
            AppGen.Inst.MDImain.frmAssemblies.UpdateFrmAssemblies(FrmAssembliesData.UnloadConvErrMsg, AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg1);

            //---------------------------------Index Table Errors:---------------------------------------
            short tmpErrTable = 0;
            if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Stop)
            {
                tmpErrTable = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.TableConvErrorCount;
            }
            if (tmpErrTable > 0)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblIndexTableMsg, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg1);
                AppGen.Inst.MDImain.frmTitle.cmdErrForm.BackColor = System.Drawing.Color.OrangeRed;
                tmrBlinkResume2.Start();
                Ramzor(RamzorColor.Red);
                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                    PauseAll(); //Japan 01.07.15
                }
                if (!AppGen.Inst.MainCycle.IndexTableControl.ErrLoged)
                {
                    AppGen.Inst.LogFile(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg1, LogType.GeneralErr, LogStation.IndexTable);
                    AppGen.Inst.MainCycle.IndexTableControl.ErrLoged = true;
                }
            }
            else
            {
                if (!AppGen.Inst.MainCycle.IndexTableControl.bError)
                {
                    tmrBlinkResume2.Stop();
                    AppGen.Inst.MDImain.frmTitle.cmdResumeIndexTable.BackColor = System.Drawing.Color.WhiteSmoke;
                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblIndexTableMsg, "");
                }
            }
            AppGen.Inst.MDImain.frmAssemblies.UpdateFrmAssemblies(FrmAssembliesData.IndexTableErrMsg, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg1);


            //--------------------------------- Robots Errors:---------------------------------------
            //---------------------------------Loading Robot Errors:---------------------------------------
            if (AppGen.Inst.RobotConnection.stLoadRobotControl.iErrNo != 0)
            {
                if (lblRobotsMsg.Text == "") AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, "rob1:" + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.RobotConnection.stLoadRobotControl.iErrNo]);
                tmrBlinkResume4.Start();
                Ramzor(RamzorColor.Red);
                if (!AppGen.Inst.RobotConnection.stLoadRobotControl.ErrLoged)
                {
                    AppGen.Inst.LogFile(AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.RobotConnection.stLoadRobotControl.iErrNo], LogType.GeneralErr, LogStation.LoadRobot);
                    AppGen.Inst.RobotConnection.stLoadRobotControl.ErrLoged = true;
                }
                
            }
            else
            {
                //tmrBlinkResume4.Stop();              
            }
            //---------------------------------Unloading Robot Errors:---------------------------------------
            if (AppGen.Inst.RobotConnection.stUnloadRobotControl.iErrNo != 0)
            {
                if (lblRobotsMsg.Text == "") AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, "rob2:" + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.RobotConnection.stUnloadRobotControl.iErrNo]);
                tmrBlinkResume4.Start();
                Ramzor(RamzorColor.Red);
                if (!AppGen.Inst.RobotConnection.stUnloadRobotControl.ErrLoged)
                {
                    AppGen.Inst.LogFile(AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.RobotConnection.stUnloadRobotControl.iErrNo], LogType.GeneralErr, LogStation.UnloadRobot);
                    AppGen.Inst.RobotConnection.stUnloadRobotControl.ErrLoged = true;
                }
            }
            else
            {
                //tmrBlinkResume4.Stop();
            }
            //------------------------- Both robots disable Resume timer ------------
            if ((AppGen.Inst.RobotConnection.stLoadRobotControl.iErrNo == 0) && (AppGen.Inst.RobotConnection.stUnloadRobotControl.iErrNo == 0))
            {
                tmrBlinkResume4.Stop();
                AppGen.Inst.MDImain.frmTitle.cmdResumeRobots.BackColor = System.Drawing.Color.WhiteSmoke;
            }


            //---------------------------------Cameras Errors:---------------------------------------
            //---------------------------------Cam1 Errors:---------------------------------------
            if ((AppGen.Inst.Cam1.bError) && (!AppGen.Inst.Cam1.bMsgDisplayed))
            {
                AppGen.Inst.Cam1.bMsgDisplayed = true;
                tmrBlinkResume5.Start();
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblCamerasMsg, "cam1:" + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam1.iErrNo]);
                Ramzor(RamzorColor.Red);  
                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                    PauseAll(); //Japan 01.07.15
                }
                AppGen.Inst.LogFile("Camera1  " + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam1.iErrNo], LogType.GeneralErr, LogStation.Vision);
            }
            else
            {
                tmrBlinkResume5.Stop();
            }
            //---------------------------------Cam2 Errors:---------------------------------------
            if ((AppGen.Inst.Cam2.bError) && (!AppGen.Inst.Cam2.bMsgDisplayed))
            {
                AppGen.Inst.Cam2.bMsgDisplayed = true;
                tmrBlinkResume5.Start();
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblCamerasMsg, "cam2:" + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam2.iErrNo]);
                Ramzor(RamzorColor.Red); 
                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                    PauseAll(); //Japan 01.07.15
                }
                AppGen.Inst.LogFile("Camera2  " + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam2.iErrNo], LogType.GeneralErr, LogStation.Vision);
            }
            else
            {
                tmrBlinkResume5.Stop();
            }
            //---------------------------------Cam3 Errors:---------------------------------------
            if ((AppGen.Inst.Cam3.bError) && (!AppGen.Inst.Cam3.bMsgDisplayed))
            {
                AppGen.Inst.Cam3.bMsgDisplayed = true;
                tmrBlinkResume5.Start();
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblCamerasMsg, "cam3:" + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam3.iErrNo]);
                Ramzor(RamzorColor.Red); 
                if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                {
                    Buzzer(true);
                    PauseAll(); //Japan 01.07.15
                }
                AppGen.Inst.LogFile("Camera3  " + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam3.iErrNo], LogType.GeneralErr, LogStation.Vision);
            }
            else
            {
                tmrBlinkResume5.Stop();
            }

            //---------------------------------E-Stop State Update:---------------------------------------
            if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ES_State)
            {
                AppGen.Inst.MDImain._tbr_ES_Rel.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            else
            {                             
                AppGen.Inst.MDImain._tbr_ES_Rel.BackColor = System.Drawing.Color.Yellow;
            }
            //---------------------------------Open Door:---------------------------------------
            if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ZoneLocked)
            {
                AppGen.Inst.MDImain._tbr_OpenDoor.Text = "Locked";
            }
            else
            {
                AppGen.Inst.MDImain._tbr_OpenDoor.Text = "Unlock";
            }
            if ((AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Req_Lock)&&(!AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ZoneLocked))
            {
                AppGen.Inst.MDImain._tbr_OpenDoor.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                AppGen.Inst.MDImain._tbr_OpenDoor.BackColor = System.Drawing.Color.WhiteSmoke;
            }
            //--------------------------------- S3 for PLC: ---------------------------------------
            if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.S3)
            {
                AppGen.Inst.MDImain.frmTitle.____S3_Led.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico"); 
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.____S3_Led.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-off_24.ico"); 
            }

            if ((tmpErrTable == 0) && (tmpErrLoad == 0) && (tmpErrUnload == 0))
	        {
                AppGen.Inst.MDImain.frmTitle.cmdErrForm.BackColor = System.Drawing.Color.WhiteSmoke;
	        }             
        }
        public void tmrBlinkResume0_Tick(object sender, EventArgs e)
        {
            //cmdResumeGeneral
            if (step0 == true)
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeGeneral.BackColor = System.Drawing.Color.WhiteSmoke;
                step0 = false;
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeGeneral.BackColor = System.Drawing.Color.Khaki;
                step0 = true;
            }
        }
        public void tmrBlinkResume1_Tick(object sender, EventArgs e)
        {
            //cmdResumeLoadConv
            if (step1 == true)
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeLoadConv.BackColor = System.Drawing.Color.WhiteSmoke;
                step1 = false;
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeLoadConv.BackColor = System.Drawing.Color.Khaki;
                step1 = true;
            }
        }
        public void tmrBlinkResume2_Tick(object sender, EventArgs e)
        {
            //cmdResumeIndexTable
            if (step2 == true)
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeIndexTable.BackColor = System.Drawing.Color.WhiteSmoke;
                step2 = false;
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeIndexTable.BackColor = System.Drawing.Color.Khaki;
                step2 = true;
            }
        }
        public void tmrBlinkResume3_Tick(object sender, EventArgs e)
        {            
            //cmdResumeUnloadConv
            if (step3 == true)
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeUnloadConv.BackColor = System.Drawing.Color.WhiteSmoke;
                step3 = false;
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeUnloadConv.BackColor = System.Drawing.Color.Khaki;
                step3 = true;
            }
        }
        public void tmrBlinkResume4_Tick(object sender, EventArgs e)
        {
            //cmdResumeRobots
            if (step4 == true)
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeRobots.BackColor = System.Drawing.Color.WhiteSmoke;
                step4 = false;
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeRobots.BackColor = System.Drawing.Color.Khaki;
                step4 = true;
            }
        }
        public void tmrBlinkResume5_Tick(object sender, EventArgs e)
        {
            //cmdResumeCameras
            if (step5 == true)
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeCameras.BackColor = System.Drawing.Color.WhiteSmoke;
                step5 = false;
            }
            else
            {
                AppGen.Inst.MDImain.frmTitle.cmdResumeCameras.BackColor = System.Drawing.Color.Khaki;
                step5 = true;
            }
        }
        public void tmrWaitResetAllDone_Tick(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);           
            if ((AppGen.Inst.RobotConnection.stLoadRobotControl.bDone) && (AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone))
	        {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, "Reset Done");
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "6");
	        }

            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Reset in process ...   " + (AppGen.Inst.MainCycle.MainControl.lTimeOutLimit - AppGen.Inst.MainCycle.sw_TimeOut.Elapsed.Seconds) + "    Wait !!");
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "7");
            AppGen.Inst.MainCycle.MainControl.bDone = true;
            if (chkLoadRobotSetup.Checked)
            {
                AppGen.Inst.MainCycle.MainControl.bDone = AppGen.Inst.MainCycle.MainControl.bDone && AppGen.Inst.RobotConnection.stLoadRobotControl.bDone;
            }
            if (chkUnloadRobotSetup.Checked)
            {
                AppGen.Inst.MainCycle.MainControl.bDone = AppGen.Inst.MainCycle.MainControl.bDone && AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone;
            }

        
            if (AppGen.Inst.MainCycle.sw_TimeOut.Elapsed.Seconds > AppGen.Inst.MainCycle.MainControl.lTimeOutLimit)
            {                
                AppGen.Inst.MDImain.frmTitle.cmdSetupAll.Enabled = true;
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Reset All Faild");
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "5");
                AppGen.Inst.MainCycle.sw_TimeOut.Stop();
                AppGen.Inst.MainCycle.sw_TimeOut.Reset();
                tmrWaitResetAllDone.Stop();   
            }
            else if (AppGen.Inst.MainCycle.MainControl.bDone)
            {
                AppGen.Inst.MDImain.frmTitle.cmdSetupAll.Enabled = true;
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Reset All Done");
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "6");
                AppGen.Inst.MainCycle.sw_TimeOut.Stop();
                AppGen.Inst.MainCycle.sw_TimeOut.Reset();
                tmrWaitResetAllDone.Stop(); 
            }
        }

        // delegates
        private delegate void LoadOrderDataDelegate();
        private LoadOrderDataDelegate loadOrderDataDelegate;
        private void LoadOrderDataDelegateFunc()
        {
            try
            {
                txtInsertType.Text = label7.Text = AppGen.Inst.OrderParams.InsertCode;
                label8.Text = AppGen.Inst.OrderParams.InsertDescription;
                label9.Text = AppGen.Inst.OrderParams.InsertHeight.ToString();
                label10.Text = AppGen.Inst.OrderParams.InsertHoleDiameter.ToString();
                txtTrayType.Text = label11.Text = AppGen.Inst.OrderParams.ServiceLoadTrayName;
                txtCarrierType.Text = label12.Text = AppGen.Inst.OrderParams.CarrierName;
            }
            catch { }
        }
        public void LoadOrderData()
        {
            try
            {
                this.Invoke(loadOrderDataDelegate);
            }
            catch { }
        }

        private delegate void UpdateFrmTitleDelegate(FrmTitleData BoxType, string UpdatedValue);
        private UpdateFrmTitleDelegate updateFrmTitleDelegate;
        private void UpdateFrmTitleDelegateFunc(FrmTitleData BoxType, string UpdatedValue)
        {
            try
            {
                switch (BoxType)
	            {
                    case FrmTitleData.AreaIndexLoadTray:
                        txtAreaIndexLoadTray.Text = UpdatedValue;
                        AppGen.Inst.MDImain.frmTitle.progBarLoadTray.Value = Convert.ToInt16(UpdatedValue);
                        break;
                    case FrmTitleData.AreaIndexLoadCarrier:
                        txtAreaIndexLoadCarrier.Text = UpdatedValue;
                        break;
                    case FrmTitleData.AreaSliceUnloadCarrier:
                        txtAreaIndexUnloadCarrier.Text = UpdatedValue;
                        break;
                    case FrmTitleData.AreaIndexUnloadTray:
                        txtAreaIndexUnloadTray.Text = UpdatedValue;
                        AppGen.Inst.MDImain.frmTitle.progBarUnloadTray.Value = Convert.ToInt16(UpdatedValue);
                        break;
                    case FrmTitleData.CounterPerOrder:
                        txtCounterPerOrder.Text = UpdatedValue;
                        break;
                    case FrmTitleData.InsQnt:
                        txtInsQnt.Text = UpdatedValue;
                        break;
                    case FrmTitleData.lblStahliMsg:
                        lblStahliMsg.Text = UpdatedValue;
                        break;
                    case FrmTitleData.lblLoadConvMsg:
                        lblLoadConvMsg.Text = UpdatedValue;
                        break;
                    case FrmTitleData.lblIndexTableMsg:
                        lblIndexTableMsg.Text = UpdatedValue;
                        break;
                    case FrmTitleData.lblUnloadConvMsg:
                        lblUnloadConvMsg.Text = UpdatedValue;
                        break;
                    case FrmTitleData.lblRobotsMsg:
                        lblRobotsMsg.Text = UpdatedValue;
                        break;
                    case FrmTitleData.lblCamerasMsg:
                        lblCamerasMsg.Text = UpdatedValue;
                        break;
                    case FrmTitleData.StepLoad:
                        txtStepLoad.Text = UpdatedValue;
                        break;
                    case FrmTitleData.StepUnload:
                        txtStepUnload.Text = UpdatedValue;
                        break;
                    case FrmTitleData.LoadedInsCounter:
                        txtLoadedInsCounter.Text = UpdatedValue;
                        break;
                    case FrmTitleData.UnloadedInsCounter:
                        txtUnloadedInsCounter.Text = UpdatedValue;
                        break;
                    case FrmTitleData.MissedUnloadCount:
                        txtMissedUnloadCount.Text = UpdatedValue;
                        break;
                    case FrmTitleData.MissedLoadCount:
                        txtMissPocketTotalCount.Text = UpdatedValue;                       
                        break;
                    case FrmTitleData.Robot1CycleTime:
                        txtRobotCycleTime.Text = UpdatedValue;
                        break;
                    case FrmTitleData.Robot2CycleTime:
                        txtRobot2CycleTime.Text = UpdatedValue;
                        break;
                    case FrmTitleData.CarrierCycleTime:
                        txtCarrierCycleTime.Text = UpdatedValue;
                        break;
                    case FrmTitleData.lblMsg:
                        lblMsg.Text = UpdatedValue;
                        break;
                    case FrmTitleData.LoadCarrAddedX:
                           txtLoadCorrectX.Text = UpdatedValue;
                        break;
                        case FrmTitleData.LoadCarrAddedY:
                        txtLoadCorrectY.Text = UpdatedValue;
                        break;
                    case FrmTitleData.LoadAddedAngle:
                        textBox18.Text = UpdatedValue;
                        break;
                    case FrmTitleData.UnloadAddedAngle:
                        textBox19.Text = UpdatedValue;
                        break;
                    case FrmTitleData.UnloadTrayXoffset:                     
                        textBox20.Text = UpdatedValue;
                        break;
                    case FrmTitleData.UnloadTrayYoffset:
                        textBox21.Text = UpdatedValue;                       
                        break;
                    case FrmTitleData.ShowStatusImg:
                        switch (Convert.ToInt16(UpdatedValue))
	                    {
                            case 0:       //Stop
                                ImgStatusMsg1.Image = Properties.Resources.player_stop;
                                ImgStatusMsg2.Image = Properties.Resources.player_stop;
                                break;
                            case 1:       //Play
                                ImgStatusMsg1.Image = Properties.Resources.player_play;
                                ImgStatusMsg2.Image = Properties.Resources.player_play;
                                break;
                            case 2:       //Pause
                                ImgStatusMsg1.Image = Properties.Resources.player_pause;
                                ImgStatusMsg2.Image = Properties.Resources.player_pause;
                                break;
                            case 3:       //CleanLine
                                ImgStatusMsg1.Image = Properties.Resources.finish_flag;
                                ImgStatusMsg2.Image = Properties.Resources.finish_flag;
                                break;
                            case 4:      //Attention
                                ImgStatusMsg1.Image = Properties.Resources.attention;
                                ImgStatusMsg2.Image = Properties.Resources.attention;
                                break;
                            case 5:      //Fault
                                ImgStatusMsg1.Image = Properties.Resources.fail;
                                ImgStatusMsg2.Image = Properties.Resources.fail;
                                break;
                            case 6:      //OK
                                ImgStatusMsg1.Image = Properties.Resources.succed;
                                ImgStatusMsg2.Image = Properties.Resources.succed;
                                break;
                            case 7:      //Wait
                                ImgStatusMsg1.Image = Properties.Resources.hourglass;
                                ImgStatusMsg2.Image = Properties.Resources.hourglass;
                                break;

	                    }
                        break;
                    case FrmTitleData.Cam1ClearToSnap:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            picClearToSnap1.BackColor = System.Drawing.Color.LimeGreen;
                            txtSnapFleg1.Text = "True";
                        }
                        else
                        {
                            picClearToSnap1.BackColor = System.Drawing.Color.OrangeRed;
                            txtSnapFleg1.Text = "False";
                        }
                        break;
                    case FrmTitleData.Cam2ClearToSnap:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            picClearToSnap2.BackColor = System.Drawing.Color.LimeGreen;
                            txtSnapFleg2.Text = "True";                    
                        }
                        else
                        {
                            picClearToSnap2.BackColor = System.Drawing.Color.OrangeRed;
                            txtSnapFleg2.Text = "False";
                        }
                        break;
                    case FrmTitleData.Cam3ClearToSnap:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            picClearToSnap3.BackColor = System.Drawing.Color.LimeGreen;
                            txtSnapFleg3.Text = "True";
                        }
                        else
                        {
                            picClearToSnap3.BackColor = System.Drawing.Color.OrangeRed;
                            txtSnapFleg3.Text = "False";
                        }                     
                        break;
                    case FrmTitleData.LoadDoorState:
                       if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            ShapeDoor1.BackColor = System.Drawing.Color.Red;
                            ShapeDoor3.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            ShapeDoor1.BackColor = System.Drawing.Color.Gray;
                            ShapeDoor3.BackColor = System.Drawing.Color.Gray;

                        }                     
                        break;
                    case FrmTitleData.UnloadDoorState:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            ShapeDoor2.BackColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            ShapeDoor2.BackColor = System.Drawing.Color.Gray;
                        }                     
                        break;
                    case FrmTitleData.RLconnbusy:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            txtRLconbusy.Text = "True";
                        }
                        else
                        {
                            txtRLconbusy.Text = "False";
                        }
                        break;
                    case FrmTitleData.robotdataSnap1:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            txt_robotdataSnap1.Text = "True";
                        }
                        else
                        {
                            txt_robotdataSnap1.Text = "False";
                        }
                        break;
                    case FrmTitleData.robotdataSnap2:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            txt_robotdataSnap2.Text = "True";
                        }
                        else
                        {
                            txt_robotdataSnap2.Text = "False";
                        }
                        break;
                    case FrmTitleData.RUconnbusy:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            txtRUconbusy.Text = "True";
                        }
                        else
                        {
                            txtRUconbusy.Text = "False";
                        }
                        break;
                    case FrmTitleData.robotdata2Snap3:
                        if (Convert.ToInt16(UpdatedValue) == 1)
                        {
                            txt_robotdata2Snap3.Text = "True";
                        }
                        else
                        {
                            txt_robotdata2Snap3.Text = "False";
                        }
                        break;
                    case FrmTitleData.measureResult:
                        txtMeasureCarr.Text = UpdatedValue;
                        series1.Points.Add(Math.Round(Convert.ToDouble(UpdatedValue),3)); //changed in japan, need to check in Iscar (for showing thisformaton Schem Y axis)
                        chrtInsertMaesures.ChartAreas[0].AxisX.ScaleView.Zoom(series1.Points.Count - 10, series1.Points.Count + 3);
                        chrtInsertMaesures.Invalidate();
                        break;                  
	            }        
            }
            catch { }
        }

        private delegate void AfterModeChangedDelegate();
        private AfterModeChangedDelegate afterModeChangedDelegate;
        private void AfterModeChangedDelegateFunc()
        {
            try
            {
                ____cmdNextTab.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.SetupSucces);
               ____cmdPauseButton.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running);
               ____cmdStartButton.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Pause);
               ____cmdCleanLineButton.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running);
               ____cmdResetAllButton.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
               ____button16.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);  //GoTo setup button
            
               ____cmdUnloadConv.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
               ____cmdUnloadRobot.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
               ____cmdLoadConv.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
               ____cmdIndexTable.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
               ____cmdLoadStahliIO.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
               ____cmdLoadRobot.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);

               cmdResetIndexes.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
               cmdResetCounters.Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);

               txtLoadCorrectX.Enabled = (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running);
               txtLoadCorrectY.Enabled = (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running);
               textBox18.Enabled = (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running);
               textBox20.Enabled = (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running);
               textBox21.Enabled = (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running);
               textBox19.Enabled = (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running);
            

            }
            catch { }
        }
        public void AfterModeChanged()
        {
            try
            {
                this.Invoke(afterModeChangedDelegate);
            }
            catch { }
        }

        public void UpdateFrmTitle(FrmTitleData BoxType, string UpdatedValue)
        {
            try
            {
                this.Invoke(updateFrmTitleDelegate, BoxType, UpdatedValue);
            }
            catch { }
        }

        private void txtLoadCorrectX_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtLoadCorrectX.Text, out tmpCorrection))
            {
                if (tmpCorrection > 5) txtLoadCorrectX.Text = "5";
                if (tmpCorrection < -5) txtLoadCorrectX.Text = "-5";              
                AppGen.Inst.VisionParam.LoadCarrAddedX = Convert.ToDouble(txtLoadCorrectX.Text);
            }
        }
        private void txtLoadCorrectY_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtLoadCorrectY.Text, out tmpCorrection))
            {
                if (tmpCorrection > 5) txtLoadCorrectY.Text = "5";
                if (tmpCorrection < -5) txtLoadCorrectY.Text = "-5";              
                AppGen.Inst.VisionParam.LoadCarrAddedY = Convert.ToDouble(txtLoadCorrectY.Text);
            }
        }   
        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(textBox18.Text, out tmpCorrection))
            {
                AppGen.Inst.VisionParam.LoadAddedAngle = Convert.ToDouble(textBox18.Text);
            }
        }
        private void textBox20_TextChanged(object sender, EventArgs e)
        {
             double tmpCorrection;
             if (double.TryParse(textBox20.Text, out tmpCorrection))
             {
                 if (tmpCorrection > 10) textBox20.Text = "10";
                 if (tmpCorrection < -10) textBox20.Text = "-10";                 
                 AppGen.Inst.VisionParam.OriginXOffset = Convert.ToDouble(textBox20.Text);
             }
        }
        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(textBox21.Text, out tmpCorrection))
            {
                if (tmpCorrection > 10) textBox21.Text = "10";
                if (tmpCorrection < -10) textBox21.Text = "-10";
                AppGen.Inst.VisionParam.OriginYOffset = Convert.ToDouble(textBox21.Text);
            }      
        }
        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(textBox19.Text, out tmpCorrection))
            {
                AppGen.Inst.VisionParam.UnloadAddedAngle = Convert.ToDouble(textBox19.Text);
            }
        }
        private void txtAreaIndexLoadTray_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtAreaIndexLoadTray.Text, out tmpCorrection))
            {
                AppGen.Inst.LoadTray.CurrIndex = Convert.ToInt16(txtAreaIndexLoadTray.Text);
            }
        }
        private void txtAreaIndexLoadCarrier_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtAreaIndexLoadCarrier.Text, out tmpCorrection))
            {
                AppGen.Inst.LoadCarrier.CurrIndex = Convert.ToInt16(txtAreaIndexLoadCarrier.Text);
            }
        }
        private void txtAreaIndexUnloadCarrier_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtAreaIndexUnloadCarrier.Text, out tmpCorrection))
            {
                AppGen.Inst.MainCycle.UnloadCarrierSliceNo = Convert.ToInt16(txtAreaIndexUnloadCarrier.Text);
            }
        }
        private void txtAreaIndexUnloadTray_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtAreaIndexUnloadTray.Text, out tmpCorrection))
            {
                AppGen.Inst.UnLoadTray.CurrIndex = Convert.ToInt16(txtAreaIndexUnloadTray.Text);
            }
        }
        private void txtUnloadedInsCounter_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtUnloadedInsCounter.Text, out tmpCorrection))
            {
                AppGen.Inst.MainCycle.UnloadedInsCounter = Convert.ToInt16(txtUnloadedInsCounter.Text);
            }
        }
        private void txtLoadedInsCounter_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtLoadedInsCounter.Text, out tmpCorrection))
            {
                AppGen.Inst.MainCycle.LoadedInsCounter = Convert.ToInt16(txtLoadedInsCounter.Text);
            }
        }
        private void txtMissedUnload_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtMissedUnloadCount.Text, out tmpCorrection))
            {
                AppGen.Inst.MainCycle.TotalUnloadMissedCount = Convert.ToInt16(txtMissedUnloadCount.Text);
            }           
        }
        private void txtInsQnt_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtInsQnt.Text, out tmpCorrection))
            {
                AppGen.Inst.MainCycle.InsQnt = Convert.ToInt16(txtInsQnt.Text);
            }       
        }
        private void txtCounterPerOrder_TextChanged(object sender, EventArgs e)
        {
            double tmpCorrection;
            if (double.TryParse(txtCounterPerOrder.Text, out tmpCorrection))
            {
                AppGen.Inst.MainCycle.CounterPerOrder = Convert.ToInt16(txtCounterPerOrder.Text);
            }       
        }

        private void cmdErrForm_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmPlcErr.RefreshLists();
            AppGen.Inst.MDImain.frmPlcErr.Show();
            AppGen.Inst.MDImain.frmTitle.cmdErrForm.BackColor = System.Drawing.Color.WhiteSmoke;
        }
        private void cmdControlThickness_Click(object sender, EventArgs e)
        {
            if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.S3)
            {
                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.S3 = false;                
            }
            else
            {
                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.S3 = true;
            }
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hS3, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.S3);
        }
        private void cmdResetCounters_Click(object sender, EventArgs e)
        {          
            if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Reset Counters Not Available while machine at Work");
                return;
            }
            AppGen.Inst.MainCycle.CounterPerOrder = 0;
            txtCounterPerOrder.Text = "0";
            AppGen.Inst.MainCycle.LoadedInsCounter = 0;
            txtLoadedInsCounter.Text = "0";
            AppGen.Inst.MainCycle.UnloadedInsCounter = 0;
            txtUnloadedInsCounter.Text = "0";          
            txtMissedUnloadCount.Text = "0";
            AppGen.Inst.MainCycle.TotalUnloadMissedCount = 0;
            txtMissPocketTotalCount.Text = "0";
            AppGen.Inst.MainCycle.MissPocketTotalCount = 0;
        }
        private void UpdateSliceStateImages()
        {        
            SapeSliceState1.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[1]);
            SapeSliceState2.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[2]);
            SapeSliceState3.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[3]);
            SapeSliceState4.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[4]);
            SapeSliceState5.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[5]);
            SapeSliceState6.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[6]);
            SapeSliceState7.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[7]);
            SapeSliceState8.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[8]);
            SapeSliceState9.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[9]);

            SapeSliceState21.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[21]);
            SapeSliceState22.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[22]);
            SapeSliceState23.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[23]);
            SapeSliceState24.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[24]);
            SapeSliceState25.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[25]);
            SapeSliceState26.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[26]);

            SapeSliceState31.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[31]);
            SapeSliceState32.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[32]);
            SapeSliceState33.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[33]);
            SapeSliceState34.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[34]);
            SapeSliceState35.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[35]);
            SapeSliceState36.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[36]);
            SapeSliceState37.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[37]);
            SapeSliceState38.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[38]);
            SapeSliceState39.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[39]);
            SapeSliceState40.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[40]);
            SapeSliceState41.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[41]);
            SapeSliceState42.BackgroundImage = TransNumToColor(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.ia_SliceStatus[42]);
        }

        private Image TransNumToColor(short ColorNum)
        {
            switch (ColorNum)
	        {
                case 0:  //SliceStat.NoPalet:
                    return Properties.Resources.circle_grey01;
                    break;
                case 1: //SliceStat.NoCarrier:
                    return Properties.Resources.circle_orange1;
                    break;
                case 2: //SliceStat.EmptyCarrier:
                    return Properties.Resources.circle_yellow2;
                    break;
                case 3: //SliceStat.UnmachineCarrier:
                    return Properties.Resources.circle_minus_green3;
                    break;
                case 4: //SliceStat.MachineCarrier:
                    return Properties.Resources.circle_green4;
                    break;
                case 5: //SliceStat.Reserved:
                    return Properties.Resources.circle_red5;
                    break;
                case 6: //SliceStat.WrongDetect:
                    return Properties.Resources.cancel_orange6;
                    break;
                case 7: //SliceStat.ByPass:
                    return Properties.Resources.minus;
                    break;
                case 8: //SliceStat.Stone:
                    return Properties.Resources.Circle_LightBlue8;
                    break;	
	        }
            return Properties.Resources.SYSTEM_WINUPDATE;  //error

        }

        public void PauseAll()  //Japan 01.07.15
        {
            AppGen.Inst.MDImain.frmAssemblies.cmdLoadConvPauseStat_Click(null, null);
            AppGen.Inst.MDImain.frmAssemblies.cmdTablePauseStat_Click(null, null);
            AppGen.Inst.MDImain.frmAssemblies.cmdUnloadConvPauseStat_Click(null, null);
            cmdPauseButton_Click(null, null);
        }

        private void cmdLoadCarrierReady_Click(object sender, EventArgs e)
        {
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_CarrReady)
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_CarrReady = false;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_CarrReady , false);
            }
            else
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_CarrReady = true;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_CarrReady , true);
            }
            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
        }
        private void cmdLoadTrayReady_Click(object sender, EventArgs e)
        {
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_TrayReady)
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_TrayReady = false;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_TrayReady , false);
            }
            else
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_TrayReady = true;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_TrayReady , true);
            }
            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
        }
        private void cmdUnloadTrayReady_Click(object sender, EventArgs e)
        {
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_TrayReady)
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_TrayReady = false;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_TrayReady , false);
            }
            else
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_TrayReady = true;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_TrayReady , true);
            }
            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
        }
        private void cmdUnloadCarrierReady_Click(object sender, EventArgs e)
        {
            if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrReady)
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrReady = false;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_CarrReady , false);
            }
            else
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrReady = true;
                //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_CarrReady , true);
            }
            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
        }
        private void cmdResetIndexes_Click(object sender, EventArgs e)
        {
            ResetIndexes();
        }
        public void ResetIndexes()
        {
            AppGen.Inst.LoadTray.CurrIndex = 0;
            txtAreaIndexLoadTray.Text = "0";
            AppGen.Inst.LoadCarrier.CurrIndex = 0;
            txtAreaIndexLoadCarrier.Text = "0";
            AppGen.Inst.MainCycle.UnloadCarrierSliceNo = 1;
            txtAreaIndexUnloadCarrier.Text = "1";
            AppGen.Inst.UnLoadTray.CurrIndex = 0;
            txtAreaIndexUnloadTray.Text = "0";

            AppGen.Inst.MDImain.frmTitle.progBarUnloadTray.Value = 0;
            AppGen.Inst.MDImain.frmTitle.progBarLoadTray.Value = 0;
        }
        private void chkPlaceZeroAngle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlaceZeroAngle.Checked)
            {
                AppGen.Inst.MainCycle.PlaceZero = 1;
            }
            else
            {
                AppGen.Inst.MainCycle.PlaceZero = 0;
            }
        }
        private void chkNoPickedSensor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoPickedSensor.Checked)
            {
                AppGen.Inst.MainCycle.PickSensor = 0;
            }
            else
            {
                AppGen.Inst.MainCycle.PickSensor = 1;
            }
        }
        private void cmdTest_Click(object sender, EventArgs e)
        {
            //AppGen.Inst.LogFile("test1", LogType.GeneralErr,LogStation.IndexTable);
            //AppGen.Inst.LogFile("test1.1", LogType.GeneralErr, LogStation.Vision);
            //AppGen.Inst.LogFile("test2", LogType.Production);
            //AppGen.Inst.LogFile("MyMsg + SysMsg", LogType.SystemErr);
            try
            {
                string aux = "999,-608,sdbjinoaibjqi";

                int arg1 = Convert.ToInt16(aux.Split(',')[0]);
                int arg2 = Convert.ToInt16(aux.Split(',')[1]);
                string arg3 = aux.Split(',')[2];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                AppGen.Inst.LogFile(ex.Message, LogType.GeneralErr , LogStation.LoadRobot);
            } 
        }
        private void txtSetupDelay_TextChanged(object sender, EventArgs e)
        {
            //AppGen.Inst.MainCycle.SetupDelays = Convert.ToDouble(txtSetupDelay.Text);
            double tmpCorrection;
            if (double.TryParse(txtSetupDelay.Text, out tmpCorrection))
            {
                AppGen.Inst.MainCycle.SetupDelays = Convert.ToDouble(txtSetupDelay.Text);
            }
        }
        private void txtMissPocketTotalCount_TextChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MainCycle.MissPocketTotalCount = Convert.ToInt16(txtMissPocketTotalCount.Text);
        }
        private void cmdDebugLoadingCycle_Click(object sender, EventArgs e)
        {
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, "70," + AppGen.Inst.MainCycle.PlaceZero.ToString() + "," + AppGen.Inst.MainCycle.PickSensor.ToString() + "," + AppGen.Inst.MainCycle.SetupDelays.ToString());  // tbd: 0,0  is placeZero, PickSensor
            AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = true;

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();  // Start The StopWatch ...From 000	              
            while (!AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl)
            {
                System.Threading.Thread.Sleep(100);
                if (sw.ElapsedMilliseconds > 3000)
                {
                    sw.Stop();
                    AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = false;
                    break;
                }
            }
            sw.Stop();       //Stop the Timer     

            AppGen.Inst.VisionParam.LastTimerBit1 = false;
            AppGen.Inst.VisionParam.LastTimerBit2 = false;
            AppGen.Inst.VisionParam.LastTimerBit3 = false;
            tmrHiTimerSnap.Start();

           // AppGen.Inst.VisionParam.snap_req_fl1 = true;
           // AppGen.Inst.VisionParam.snap_req_fl2 = true;

            AppGen.Inst.MainCycle.LoadCycleControl.bRobotActive = true;
            AppGen.Inst.MainCycle.LoadCycleControl.bRun = true;
            AppGen.Inst.MainCycle.LoadCycleControl.bDone = false;
            AppGen.Inst.MainCycle.LoadCycleControl.bPause = false;
            AppGen.Inst.MainCycle.LoadCycleControl.bError = false;
            AppGen.Inst.MainCycle.LoadCycleControl.iStep = 0;
            AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.RunAgainFlag = false;

            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqPauseCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqResumeCycle = true;
           
            
            AppGen.Inst.MainCycle.MainControl.bPause = false;
            AppGen.Inst.MainCycle.MainControl.bRun = true;
        }
        private void cmdDebugUnloadingCycle_Click(object sender, EventArgs e)
        {
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, "70," + AppGen.Inst.MainCycle.SetupDelays.ToString());
            AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = true;

            //boolean breakFlag = false;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();  // Start The StopWatch ...From 000	              
            while (!AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl)
            {
                System.Threading.Thread.Sleep(100);
                if (sw.ElapsedMilliseconds > 3000)
                {
                    sw.Stop();
                    AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = false;
                    break;
                }
            }
            sw.Stop();//Stop the Timer  

            AppGen.Inst.VisionParam.LastTimerBit1 = false;
            AppGen.Inst.VisionParam.LastTimerBit2 = false;
            AppGen.Inst.VisionParam.LastTimerBit3 = false;
            tmrHiTimerSnap.Start();


            AppGen.Inst.MainCycle.UnloadCycleControl.bRobotActive = true;
            AppGen.Inst.MainCycle.UnloadCycleControl.bRun = true;
            AppGen.Inst.MainCycle.UnloadCycleControl.bDone = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.bPause = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.bError = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.iStep = 0;
            AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = false;  //search only in one slice
            
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqPauseCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqResumeCycle = true;           

            AppGen.Inst.MainCycle.UnloadCarrierSliceNo = 1;     //init for snap 1st slice          
            AppGen.Inst.MainCycle.RescanCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrDone = false;          

            AppGen.Inst.MainCycle.MainControl.bPause = false;
            AppGen.Inst.MainCycle.MainControl.bRun = true;
        }
        private void chkMeasureCarr1_CheckedChanged(object sender, EventArgs e)
        {          
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[1] = chkMeasureCarr1.Checked;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hcarrToMeasure[1], AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[1]);
        }
        private void chkMeasureCarr2_CheckedChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[2] = chkMeasureCarr2.Checked;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hcarrToMeasure[2], AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[2]);
        }
        private void chkMeasureCarr3_CheckedChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[3] = chkMeasureCarr3.Checked;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hcarrToMeasure[3], AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[3]);
        }
        private void chkMeasureCarr4_CheckedChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[4] = chkMeasureCarr4.Checked;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hcarrToMeasure[4], AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[4]);
        }
        private void chkMeasureCarr5_CheckedChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[5] = chkMeasureCarr5.Checked;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hcarrToMeasure[5], AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[5]);
        }
        private void chkMeasureCarr6_CheckedChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[6] = chkMeasureCarr6.Checked;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hcarrToMeasure[6], AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.carrToMeasure[6]);
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrHigh2 = float.Parse(txtNominal.Text) + float.Parse(txtCorrHigh2.Text) ;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hCorrHigh2, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrHigh2);
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrHigh1 = float.Parse(txtNominal.Text) + float.Parse(txtCorrHigh1.Text);
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hCorrHigh1, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrHigh1);
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TolHigh = float.Parse(txtNominal.Text) + float.Parse(txtTolHigh.Text);
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hTolHigh, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TolHigh);
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Target = float.Parse(txtNominal.Text);
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hTarget, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Target);
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TolLow = float.Parse(txtNominal.Text) - float.Parse(txtTolLow.Text);
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hTolLow, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TolLow);
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrLow1 = float.Parse(txtNominal.Text) - float.Parse(txtCorrLow1.Text);
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hCorrLow1, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrLow1);
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrLow2 = float.Parse(txtNominal.Text) - float.Parse(txtCorrLow2.Text);
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hCorrLow2, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrLow2);
        
            chrtInsertMaesures.ChartAreas[0].AxisY.Maximum = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrHigh2 + 0.01;
            chrtInsertMaesures.ChartAreas[0].AxisY.Minimum = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrLow2 - 0.01;          
            stCorrHigh2.IntervalOffset = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrHigh2;           
            chrtInsertMaesures.ChartAreas[0].AxisY.StripLines.Add(stCorrHigh2);
            stCorrHigh1.IntervalOffset = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrHigh1;
            chrtInsertMaesures.ChartAreas[0].AxisY.StripLines.Add(stCorrHigh1);
            stTolHigh.IntervalOffset = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TolHigh;
            chrtInsertMaesures.ChartAreas[0].AxisY.StripLines.Add(stTolHigh);
            stTarget.IntervalOffset = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Target;
            chrtInsertMaesures.ChartAreas[0].AxisY.StripLines.Add(stTarget);
            stTolLow.IntervalOffset = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TolLow;
            chrtInsertMaesures.ChartAreas[0].AxisY.StripLines.Add(stTolLow);
            stCorrLow1.IntervalOffset = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrLow1;
            chrtInsertMaesures.ChartAreas[0].AxisY.StripLines.Add(stCorrLow1);
            stCorrLow2.IntervalOffset = AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.CorrLow2;
            chrtInsertMaesures.ChartAreas[0].AxisY.StripLines.Add(stCorrLow2);

            if (series1.Points.Count == 0)  //to show stripline on empty graph
            {
                series1.Points.Add(0);
            }
            chrtInsertMaesures.Invalidate();
        }
        private void cmdClearData_Click(object sender, EventArgs e)
        {
            series1.Points.Clear();
            series1.Points.Add(0);
            chrtInsertMaesures.Invalidate();
        }
        private void cmdAddPoint_Click(object sender, EventArgs e)
        {
            series1.Points.Add(4.01);
            chrtInsertMaesures.ChartAreas[0].AxisX.ScaleView.Zoom(series1.Points.Count - 10, series1.Points.Count + 3);
            chrtInsertMaesures.Invalidate();
        }

        //members:
        public System.Windows.Forms.DataVisualization.Charting.Series series1;
        public StripLine stCorrHigh2;
        public StripLine stCorrHigh1;
        public StripLine stTolHigh;
        public StripLine stTarget;
        public StripLine stTolLow;
        public StripLine stCorrLow1;
        public StripLine stCorrLow2;

        
    }
}
