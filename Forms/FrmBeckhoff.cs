using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using TwinCAT.Ads;


namespace Stahli2Robots
{
	/// <summary>
    /// Summary description for MDImain.
	/// </summary>
	public class FrmBeckhoff : System.Windows.Forms.Form
    {
		internal System.Windows.Forms.Button btnWrite;
        internal System.Windows.Forms.Button btnRead;
        internal System.Windows.Forms.GroupBox GroupBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private ArrayList notificationHandles;
        #region textbox desing
        internal TextBox textBox2;
        internal TextBox textBox1;
        internal Label label45;
        internal Label label44;
        internal Label label43;
        internal Label label42;
        internal Label label41;
        internal Label label39;
        internal Label label38;
        internal Label label36;
        internal Label label35;
        internal Label label34;
        internal Label label33;
        internal Label label30;
        internal Label label24;
        internal Label label21;
        internal Label label20;
        internal Label label19;
        internal Label label18;
        internal Label label17;
        internal Label label16;
        internal Label label15;
        internal TextBox textBox28;
        internal TextBox textBox27;
        internal TextBox textBox24;
        internal TextBox textBox23;
        internal TextBox textBox22;
        internal TextBox textBox21;
        internal TextBox textBox17;
        internal TextBox textBox15;
        internal TextBox textBox9;
        internal TextBox textBox8;
        internal TextBox textBox7;
        internal TextBox textBox6;
        internal TextBox textBox5;
        internal TextBox textBox4;
        internal TextBox textBox3;
        internal GroupBox groupBox2;
        internal TextBox textBox29;
        internal TextBox textBox30;
        internal TextBox textBox31;
        internal TextBox textBox32;
        internal TextBox textBox34;
        internal TextBox textBox36;
        internal TextBox textBox37;
        internal Label label1;
        internal Label label2;
        internal Label label3;
        internal Label label5;
        internal Label label7;
        internal Label label8;
        internal Label label10;
        internal Label label11;
        internal Label label12;
        internal Label label13;
        internal Label label47;
        internal Label label53;
        internal Label label56;
        internal Label label57;
        internal Label label58;
        internal Label label59;
        internal Label label60;
        internal Label label61;
        internal Label label62;
        internal TextBox textBox35;
        internal TextBox textBox43;
        internal TextBox textBox45;
        internal TextBox textBox49;
        internal TextBox textBox50;
        internal TextBox textBox51;
        internal TextBox textBox52;
        internal TextBox textBox55;
        internal TextBox textBox56;
        #endregion

        private TcAdsClient adsClient;
 
        
//////////PLC variable handles

        //----------robots:----------------
        public int hrobotStruct;
        public int hrobotStruct2;
        public int hPlcErrMsg0;
        public int hPlcErrMsg1;
        public int hPlcErrMsg2;
        public int hPlcErrMsg3; //qq
        
      
		public FrmBeckhoff()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            
            adsClient = new TcAdsClient();
            notificationHandles = new ArrayList();

            RobotDada = new RobotStruct(adsClient);
            RobotDada2 = new RobotStruct(adsClient);

            try
            {
               if (!AppGen.Inst.MDImain.chkPlcON.Checked) return;   
                adsClient.Connect(801);
                adsClient.AdsNotificationEx += new AdsNotificationExEventHandler(adsClient_AdsNotificationEx);

                /////////////create handles for the PLC variables;
                //----------robots:----------            
                RobotDada.H_q_ReqPauseCycle = adsClient.CreateVariableHandle(".db_Rob[1].q_ReqPauseCycle");
                RobotDada.H_q_StopCycleResetAll = adsClient.CreateVariableHandle(".db_Rob[1].q_StopCycleResetAll");
                RobotDada.H_q_ReqResumeCycle = adsClient.CreateVariableHandle(".db_Rob[1].q_ReqResumeCycle");
                RobotDada.H_q_CarrReady = adsClient.CreateVariableHandle(".db_Rob[1].q_CarrReady");
                RobotDada.H_q_TrayReady = adsClient.CreateVariableHandle(".db_Rob[1].q_TrayReady");
                RobotDada.H_q_FoundOrientation = adsClient.CreateVariableHandle(".db_Rob[1].q_FoundOrientation");
                RobotDada.H_q_FoundInserts = adsClient.CreateVariableHandle(".db_Rob[1].q_FoundInserts");
                RobotDada.H_q_DataSended = adsClient.CreateVariableHandle(".db_Rob[1].q_DataSended");
                RobotDada.H_q_CarrDone = adsClient.CreateVariableHandle(".db_Rob[1].q_CarrDone");
                RobotDada.H_i_Rob_snap_req_1 = adsClient.CreateVariableHandle(".db_Rob[1].i_Rob_snap_req_1");
                RobotDada.H_i_Rob_snap_req_2 = adsClient.CreateVariableHandle(".db_Rob[1].i_Rob_snap_req_2");
                RobotDada.H_i_Rob_snap_req_3 = adsClient.CreateVariableHandle(".db_Rob[1].i_Rob_snap_req_3");
                RobotDada.H_i_CycleDone = adsClient.CreateVariableHandle(".db_Rob[1].i_CycleDone");
                RobotDada.H_i_RobotSafePos = adsClient.CreateVariableHandle(".db_Rob[1].i_RobotSafePos");
                RobotDada.H_i_InsertPlaced = adsClient.CreateVariableHandle(".db_Rob[1].i_InsertPlaced");
                RobotDada.H_i_LifeBit = adsClient.CreateVariableHandle(".db_Rob[1].i_LifeBit");
                RobotDada.H_i_DataReceived = adsClient.CreateVariableHandle(".db_Rob[1].i_DataReceived");
                RobotDada.H_vb_VisionEmpty = adsClient.CreateVariableHandle(".db_Rob[1].vb_VisionEmpty");
                RobotDada.H_loadingFinish = adsClient.CreateVariableHandle(".db_Rob[1].LoadingFinish");
                RobotDada.H_lastCarrier = adsClient.CreateVariableHandle(".db_Rob[1].LastCarrier");
                RobotDada.H_req_ReplaceTray = adsClient.CreateVariableHandle(".db_Rob[1].Req_ReplaceTray");
                RobotDada.H_vb_VisionDone = adsClient.CreateVariableHandle(".db_Rob[1].Vb_VisionDone");
                RobotDada.H_procesTime = adsClient.CreateVariableHandle(".db_Rob[1].ProcesTime");
                RobotDada.H_procesPercent = adsClient.CreateVariableHandle(".db_Rob[1].ProcesPercent");

                RobotDada2.H_q_ReqPauseCycle = adsClient.CreateVariableHandle(".db_Rob[2].q_ReqPauseCycle");
                RobotDada2.H_q_StopCycleResetAll = adsClient.CreateVariableHandle(".db_Rob[2].q_StopCycleResetAll");
                RobotDada2.H_q_ReqResumeCycle = adsClient.CreateVariableHandle(".db_Rob[2].q_ReqResumeCycle");
                RobotDada2.H_q_CarrReady = adsClient.CreateVariableHandle(".db_Rob[2].q_CarrReady");
                RobotDada2.H_q_TrayReady = adsClient.CreateVariableHandle(".db_Rob[2].q_TrayReady");
                RobotDada2.H_q_FoundOrientation = adsClient.CreateVariableHandle(".db_Rob[2].q_FoundOrientation");
                RobotDada2.H_q_FoundInserts = adsClient.CreateVariableHandle(".db_Rob[2].q_FoundInserts");
                RobotDada2.H_q_DataSended = adsClient.CreateVariableHandle(".db_Rob[2].q_DataSended");
                RobotDada2.H_q_CaliberReady = adsClient.CreateVariableHandle(".db_Rob[2].q_CaliberReady");              
                RobotDada2.H_q_CarrDone = adsClient.CreateVariableHandle(".db_Rob[2].q_CarrDone");
                RobotDada2.H_i_Rob_snap_req_1 = adsClient.CreateVariableHandle(".db_Rob[2].i_Rob_snap_req_1");
                RobotDada2.H_i_Rob_snap_req_2 = adsClient.CreateVariableHandle(".db_Rob[2].i_Rob_snap_req_2");
                RobotDada2.H_i_Rob_snap_req_3 = adsClient.CreateVariableHandle(".db_Rob[2].i_Rob_snap_req_3");
                RobotDada2.H_i_CycleDone = adsClient.CreateVariableHandle(".db_Rob[2].i_CycleDone");
                RobotDada2.H_i_RobotSafePos = adsClient.CreateVariableHandle(".db_Rob[2].i_RobotSafePos");
                RobotDada2.H_i_InsertPlaced = adsClient.CreateVariableHandle(".db_Rob[2].i_InsertPlaced");
                RobotDada2.H_i_LifeBit = adsClient.CreateVariableHandle(".db_Rob[2].i_LifeBit");
                RobotDada2.H_i_MeasurePlaced = adsClient.CreateVariableHandle(".db_Rob[2].i_MeasurePlaced");              
                RobotDada2.H_i_DataReceived = adsClient.CreateVariableHandle(".db_Rob[2].i_DataReceived");
                RobotDada2.H_vb_VisionEmpty = adsClient.CreateVariableHandle(".db_Rob[2].vb_VisionEmpty");
                RobotDada2.H_loadingFinish = adsClient.CreateVariableHandle(".db_Rob[2].LoadingFinish");
                RobotDada2.H_lastCarrier = adsClient.CreateVariableHandle(".db_Rob[2].LastCarrier");
                RobotDada2.H_req_ReplaceTray = adsClient.CreateVariableHandle(".db_Rob[2].Req_ReplaceTray");
                RobotDada2.H_req_Measure = adsClient.CreateVariableHandle(".db_Rob[2].Req_Measure");
                RobotDada2.H_measureDone = adsClient.CreateVariableHandle(".db_Rob[2].MeasureDone");               
                RobotDada2.H_procesTime = adsClient.CreateVariableHandle(".db_Rob[2].ProcesTime");
                RobotDada2.H_procesPercent = adsClient.CreateVariableHandle(".db_Rob[2].ProcesPercent");

                hPlcErrMsg0 = adsClient.CreateVariableHandle(".act_Error_Load_HMI");
                hPlcErrMsg1 = adsClient.CreateVariableHandle(".act_Error_Unload_HMI");
                hPlcErrMsg2 = adsClient.CreateVariableHandle(".act_Error_Table_HMI");
                hPlcErrMsg3 = adsClient.CreateVariableHandle(".act_Error_General_HMI");  //qq

                //----------GeneralControl:----------
                GeneralControl_PLC.hTopLight = adsClient.CreateVariableHandle(".TopLight");
                GeneralControl_PLC.hBottomLight1 = adsClient.CreateVariableHandle(".BottomLight1");
                GeneralControl_PLC.hGreenLight = adsClient.CreateVariableHandle(".trafic_Green");
                GeneralControl_PLC.hYellowLight = adsClient.CreateVariableHandle(".trafic_Yelow");
                GeneralControl_PLC.hRedLight = adsClient.CreateVariableHandle(".trafic_Red");
                GeneralControl_PLC.hBuzzer = adsClient.CreateVariableHandle(".trafic_Buzzer");
                GeneralControl_PLC.hS3 = adsClient.CreateVariableHandle(".Req_s3");   
                GeneralControl_PLC.hProductionMessage = adsClient.CreateVariableHandle("prg_Production.Message");
                GeneralControl_PLC.hLastMeasure = adsClient.CreateVariableHandle(".s_LastMeasure");
                GeneralControl_PLC.hProdMode = adsClient.CreateVariableHandle(".m_Prod");
                GeneralControl_PLC.hReq_EmergencyRelease = adsClient.CreateVariableHandle("prg_Safety.Req_Release_ES");
                GeneralControl_PLC.hReq_Lock = adsClient.CreateVariableHandle("prg_Safety.Req_Lock");
                GeneralControl_PLC.hES_State = adsClient.CreateVariableHandle(".TSafeIn.ES_State");
                GeneralControl_PLC.hResumeAll = adsClient.CreateVariableHandle(".ResumeAll");
                GeneralControl_PLC.hResetErrors = adsClient.CreateVariableHandle("prg_Messages.ResetErrors");
                GeneralControl_PLC.hZoneLocked = adsClient.CreateVariableHandle(".bIntZone[1]");
                GeneralControl_PLC.hCorrHigh2 = adsClient.CreateVariableHandle(".ar_MeasTolerance.fCorrHigh2");
                GeneralControl_PLC.hCorrHigh1 = adsClient.CreateVariableHandle(".ar_MeasTolerance.fCorrHigh1");
                GeneralControl_PLC.hTolHigh = adsClient.CreateVariableHandle(".ar_MeasTolerance.fTolHigh");
                GeneralControl_PLC.hTarget = adsClient.CreateVariableHandle(".ar_MeasTolerance.fTarget");
                GeneralControl_PLC.hTolLow = adsClient.CreateVariableHandle(".ar_MeasTolerance.fTolLow");
                GeneralControl_PLC.hCorrLow1 = adsClient.CreateVariableHandle(".ar_MeasTolerance.fCorrLow1");
                GeneralControl_PLC.hCorrLow2 = adsClient.CreateVariableHandle(".ar_MeasTolerance.fCorrLow2");
                GeneralControl_PLC.hGeneralErrorCount = adsClient.CreateVariableHandle(".iActWarning");  // Japan 01.07.15
                GeneralControl_PLC.hLastBatch = adsClient.CreateVariableHandle(".b_LastBatch"); //qq
                GeneralControl_PLC.hStahliOutAutomationReady = adsClient.CreateVariableHandle(".db_Stahli.out_AutomationReady"); //  Japan 30.6.15
                GeneralControl_PLC.hStahliInReadyForUnload = adsClient.CreateVariableHandle(".db_Stahli.in_ReadyForUnload"); //  Japan 30.6.15
                GeneralControl_PLC.hGeneralWarningCount = adsClient.CreateVariableHandle(".iActMessages"); //  Japan 30.6.15
        
                //--------Load conveyor:-----------
                LoadConveyor_PLC.hOp_VB = adsClient.CreateVariableHandle(".op_Load_VB");
                LoadConveyor_PLC.hfl_ConvMode = adsClient.CreateVariableHandle(".m_Load_VB");
                LoadConveyor_PLC.hfl_ConvReady = adsClient.CreateVariableHandle(".st_LoadReady");                
                LoadConveyor_PLC.hInitWithStation = adsClient.CreateVariableHandle(".fb_ConvL.Init_IncludeStations");
                LoadConveyor_PLC.hInitConv = adsClient.CreateVariableHandle(".db_ConvL.Req_InitConv");
                LoadConveyor_PLC.hConvResume = adsClient.CreateVariableHandle(".fb_ConvL.ResumeSFC");
                LoadConveyor_PLC.hResErr = adsClient.CreateVariableHandle(".fb_ConvL.ResetError");
                LoadConveyor_PLC.hResCycle = adsClient.CreateVariableHandle(".fb_ConvL.SFCInit");
                LoadConveyor_PLC.hTrayManUp = adsClient.CreateVariableHandle(".db_WorkST[1].req_LoadTray");
                LoadConveyor_PLC.hTrayManDown = adsClient.CreateVariableHandle(".db_WorkST[1].req_UnloadTray");
                LoadConveyor_PLC.hConvErrorCount = adsClient.CreateVariableHandle(".act_ErrorNum_Load");
                //--------Unload conveyor:---------
                UnloadConveyor_PLC.hOp_VB = adsClient.CreateVariableHandle(".op_Unload_VB");
                UnloadConveyor_PLC.hfl_ConvMode = adsClient.CreateVariableHandle(".m_Unload_VB");
                UnloadConveyor_PLC.hfl_ConvReady = adsClient.CreateVariableHandle(".st_UnloadReady");
                UnloadConveyor_PLC.hInitWithStation = adsClient.CreateVariableHandle(".fb_ConvU.Init_IncludeStations");
                UnloadConveyor_PLC.hInitConv = adsClient.CreateVariableHandle(".db_ConvU.Req_InitConv");
                UnloadConveyor_PLC.hConvResume = adsClient.CreateVariableHandle(".fb_ConvU.ResumeSFC");
                UnloadConveyor_PLC.hResErr = adsClient.CreateVariableHandle(".fb_ConvU.ResetError");
                UnloadConveyor_PLC.hResCycle = adsClient.CreateVariableHandle(".fb_ConvU.SFCInit");
                UnloadConveyor_PLC.hTrayManUp = adsClient.CreateVariableHandle(".db_WorkST[2].req_LoadTray");
                UnloadConveyor_PLC.hTrayManDown = adsClient.CreateVariableHandle(".db_WorkST[2].req_UnloadTray");
                UnloadConveyor_PLC.hConvErrorCount = adsClient.CreateVariableHandle(".act_ErrorNum_Unload");
                //--------Index Table:-----------
                IndexTable_PLC.hOp_VB = adsClient.CreateVariableHandle(".op_Table_VB");
                IndexTable_PLC.hfl_TableMode = adsClient.CreateVariableHandle(".m_Table_VB");
                IndexTable_PLC.hfl_StepConvReady = adsClient.CreateVariableHandle(".st_StepConvReady");
                IndexTable_PLC.hfl_TableError = adsClient.CreateVariableHandle("prg_Stepper.ErrorSFC");
                IndexTable_PLC.hreq_ResetTableError = adsClient.CreateVariableHandle("prg_Stepper.ResumeSFC");
                IndexTable_PLC.hTableConvErrorCount = adsClient.CreateVariableHandle(".act_ErrorNum_Table");
                for (int ii = 1; ii <= 9; ii++)  //Table (9 is gripper)
			    {
			         IndexTable_PLC.hia_SliceStatus[ii] = adsClient.CreateVariableHandle(".Carr[" + ii + "]");
			    }
                for (int ii = 21; ii <= 26; ii++)  //Stahli
                {
                   // IndexTable_PLC.hia_SliceStatus[ii] = adsClient.CreateVariableHandle(".Carr[" + ii + "]");
                    IndexTable_PLC.hia_SliceStatus[ii] = adsClient.CreateVariableHandle(".CarrStahli_StateHMI[" + ii + "]");  //Japan  30.6.15
                }
                for (int ii = 31; ii <= 42; ii++)  //Stacker
                {
                    IndexTable_PLC.hia_SliceStatus[ii] = adsClient.CreateVariableHandle(".Carr[" + ii + "]");
                }
                IndexTable_PLC.hcarrToMeasure[1] = adsClient.CreateVariableHandle("prg_Measuring.ar_CarrToMeasure[1]");
                IndexTable_PLC.hcarrToMeasure[2] = adsClient.CreateVariableHandle("prg_Measuring.ar_CarrToMeasure[2]");
                IndexTable_PLC.hcarrToMeasure[3] = adsClient.CreateVariableHandle("prg_Measuring.ar_CarrToMeasure[3]");
                IndexTable_PLC.hcarrToMeasure[4] = adsClient.CreateVariableHandle("prg_Measuring.ar_CarrToMeasure[4]");
                IndexTable_PLC.hcarrToMeasure[5] = adsClient.CreateVariableHandle("prg_Measuring.ar_CarrToMeasure[5]");
                IndexTable_PLC.hcarrToMeasure[6] = adsClient.CreateVariableHandle("prg_Measuring.ar_CarrToMeasure[6]");
                
           
                notificationHandles.Clear();


                ///////////register notification  
                //----------robots:----------------
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_ReqPauseCycle", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_ReqPauseCycle", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_StopCycleResetAll", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_StopCycleResetAll", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_ReqResumeCycle", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_ReqResumeCycle", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_CarrReady", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_CarrReady", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_TrayReady", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_TrayReady", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_FoundOrientation", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_FoundOrientation", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_FoundInserts", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_FoundInserts", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_DataSended", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_DataSended", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].q_CarrDone", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].q_CarrDone", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_Rob_snap_req_1", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_Rob_snap_req_1", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_Rob_snap_req_2", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_Rob_snap_req_2", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_Rob_snap_req_3", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_Rob_snap_req_3", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_CycleDone", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_CycleDone", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_RobotSafePos", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_RobotSafePos", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_InsertPlaced", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_InsertPlaced", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_LifeBit", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_LifeBit", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].i_DataReceived", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].i_DataReceived", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].vb_VisionEmpty", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].vb_VisionEmpty", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].LoadingFinish", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].LoadingFinish", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].LastCarrier", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].LastCarrier", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].Req_ReplaceTray", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].Req_ReplaceTray", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].Vb_VisionDone", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].Vb_VisionDone", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].ProcesTime", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].ProcesTime", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[1].ProcesPercent", AdsTransMode.OnChange, 100, 0, ".db_Rob[1].ProcesPercent", typeof(short)));

                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_ReqPauseCycle", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_ReqPauseCycle", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_StopCycleResetAll", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_StopCycleResetAll", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_ReqResumeCycle", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_ReqResumeCycle", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_CarrReady", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_CarrReady", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_TrayReady", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_TrayReady", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_FoundOrientation", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_FoundOrientation", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_FoundInserts", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_FoundInserts", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_DataSended", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_DataSended", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_CaliberReady", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_CaliberReady", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].q_CarrDone", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].q_CarrDone", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_Rob_snap_req_1", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_Rob_snap_req_1", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_Rob_snap_req_2", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_Rob_snap_req_2", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_Rob_snap_req_3", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_Rob_snap_req_3", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_CycleDone", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_CycleDone", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_RobotSafePos", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_RobotSafePos", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_InsertPlaced", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_InsertPlaced", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_LifeBit", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_LifeBit", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_MeasurePlaced", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_MeasurePlaced", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].i_DataReceived", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].i_DataReceived", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].vb_VisionEmpty", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].vb_VisionEmpty", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].LoadingFinish", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].LoadingFinish", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].LastCarrier", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].LastCarrier", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].Req_ReplaceTray", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].Req_ReplaceTray", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].Req_Measure", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].Req_Measure", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].MeasureDone", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].MeasureDone", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].ProcesTime", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].ProcesTime", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Rob[2].ProcesPercent", AdsTransMode.OnChange, 100, 0, ".db_Rob[2].ProcesPercent", typeof(short)));

                //----------PLC ERROR MSG:----------
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".act_Error_Load_HMI", AdsTransMode.OnChange, 100, 0, ".act_Error_Load_HMI", typeof(PlcErrMsg)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".act_Error_Unload_HMI", AdsTransMode.OnChange, 100, 0, ".act_Error_Unload_HMI", typeof(PlcErrMsg)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".act_Error_Table_HMI", AdsTransMode.OnChange, 100, 0, ".act_Error_Table_HMI", typeof(PlcErrMsg)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".act_Error_General_HMI", AdsTransMode.OnChange, 100, 0, ".act_Error_General_HMI", typeof(PlcErrMsg))); //qq
                                
                //----------GeneralControl:----------
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".TopLight", AdsTransMode.OnChange, 100, 0, ".TopLight", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".BottomLight1", AdsTransMode.OnChange, 100, 0, ".BottomLight1", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".trafic_Green", AdsTransMode.OnChange, 100, 0, ".trafic_Green", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".trafic_Yelow", AdsTransMode.OnChange, 100, 0, ".trafic_Yelow", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".trafic_Red", AdsTransMode.OnChange, 100, 0, ".trafic_Red", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".trafic_Buzzer", AdsTransMode.OnChange, 100, 0, ".trafic_Buzzer", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".Req_s3", AdsTransMode.OnChange, 100, 0, ".Req_s3", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Production.Message", AdsTransMode.OnChange, 100, 0, "prg_Production.Message", typeof(string), new int[] { 40 }));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".s_LastMeasure", AdsTransMode.OnChange, 100, 0, ".s_LastMeasure", typeof(string), new int[] { 40 }));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".m_Prod", AdsTransMode.OnChange, 100, 0, ".m_Prod", typeof(short)));               
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Safety.Req_Release_ES", AdsTransMode.OnChange, 100, 0, "prg_Safety.Req_Release_ES", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".TSafeIn.ES_State", AdsTransMode.OnChange, 100, 0, ".TSafeIn.ES_State", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ResumeAll", AdsTransMode.OnChange, 100, 0, ".ResumeAll", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Messages.ResetErrors", AdsTransMode.OnChange, 100, 0, "prg_Messages.ResetErrors", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".iActWarning", AdsTransMode.OnChange, 100, 0, ".iActWarning", typeof(short)));  // Japan 01.07.15
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".b_LastBatch", AdsTransMode.OnChange, 100, 0, ".b_LastBatch", typeof(bool)));  //qq
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".b_LastBatch", AdsTransMode.OnChange, 100, 0, ".b_LastBatch", typeof(bool)));   //Japan 30.6.15
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Stahli.out_AutomationReady", AdsTransMode.OnChange, 100, 0, ".db_Stahli.out_AutomationReady", typeof(bool)));   //Japan 30.6.15
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_Stahli.in_ReadyForUnload", AdsTransMode.OnChange, 100, 0, ".db_Stahli.in_ReadyForUnload", typeof(bool)));   //Japan 30.6.15
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".iActMessages", AdsTransMode.OnChange, 100, 0, ".iActMessages", typeof(short)));   //Japan 01.07.15
                


                
                //--------Load conveyor:-----------
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".op_Load_VB", AdsTransMode.OnChange, 100, 0, ".op_Load_VB", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".m_Load_VB", AdsTransMode.OnChange, 100, 0, ".m_Load_VB", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".st_LoadReady", AdsTransMode.OnChange, 100, 0, ".st_LoadReady", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvL.ErrorToPrint", AdsTransMode.OnChange, 100, 0, ".fb_ConvL.ErrorToPrint", typeof(string), new int[] { 40 }));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvL.Init_IncludeStations", AdsTransMode.OnChange, 100, 0, ".fb_ConvL.Init_IncludeStations", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_ConvL.Req_InitConv", AdsTransMode.OnChange, 100, 0, ".db_ConvL.Req_InitConv", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvL.ResumeSFC", AdsTransMode.OnChange, 100, 0, ".fb_ConvL.ResumeSFC", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvL.ResetError", AdsTransMode.OnChange, 100, 0, ".fb_ConvL.ResetError", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvL.SFCInit", AdsTransMode.OnChange, 100, 0, ".fb_ConvL.SFCInit", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_WorkST[1].req_LoadTray", AdsTransMode.OnChange, 100, 0, ".db_WorkST[1].req_LoadTray", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_WorkST[1].req_UnloadTray", AdsTransMode.OnChange, 100, 0, ".db_WorkST[1].req_UnloadTray", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".act_ErrorNum_Load", AdsTransMode.OnChange, 100, 0, ".act_ErrorNum_Load", typeof(short)));
                //--------Unload conveyor:---------
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".op_Unload_VB", AdsTransMode.OnChange, 100, 0, ".op_Unload_VB", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".m_Unload_VB", AdsTransMode.OnChange, 100, 0, ".m_Unload_VB", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".st_UnloadReady", AdsTransMode.OnChange, 100, 0, ".st_UnloadReady", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvU.ErrorToPrint", AdsTransMode.OnChange, 100, 0, ".fb_ConvU.ErrorToPrint", typeof(string), new int[] { 40 }));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvU.Init_IncludeStations", AdsTransMode.OnChange, 100, 0, ".fb_ConvU.Init_IncludeStations", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_ConvU.Req_InitConv", AdsTransMode.OnChange, 100, 0, ".db_ConvU.Req_InitConv", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvU.ResumeSFC", AdsTransMode.OnChange, 100, 0, ".fb_ConvU.ResumeSFC", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvU.ResetError", AdsTransMode.OnChange, 100, 0, ".fb_ConvU.ResetError", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".fb_ConvU.SFCInit", AdsTransMode.OnChange, 100, 0, ".fb_ConvU.SFCInit", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_WorkST[2].req_LoadTray", AdsTransMode.OnChange, 100, 0, ".db_WorkST[2].req_LoadTray", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".db_WorkST[2].req_UnloadTray", AdsTransMode.OnChange, 100, 0, ".db_WorkST[2].req_UnloadTray", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".act_ErrorNum_Unload", AdsTransMode.OnChange, 100, 0, ".act_ErrorNum_Unload", typeof(short)));
                //--------Index Table:-----------
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".op_Table_VB", AdsTransMode.OnChange, 100, 0, ".op_Table_VB", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".m_Table_VB", AdsTransMode.OnChange, 100, 0, ".m_Table_VB", typeof(short)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".st_StepConvReady", AdsTransMode.OnChange, 100, 0, ".st_StepConvReady", typeof(bool)));          
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Stepper.ErrorToPrint", AdsTransMode.OnChange, 100, 0, "prg_Stepper.ErrorToPrint", typeof(string), new int[] { 40 }));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Stepper.ErrorSFC", AdsTransMode.OnChange, 100, 0, "prg_Stepper.ErrorSFC", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Stepper.ResumeSFC", AdsTransMode.OnChange, 100, 0, "prg_Stepper.ResumeSFC", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".act_ErrorNum_Table", AdsTransMode.OnChange, 100, 0, ".act_ErrorNum_Table", typeof(short)));                
                for (int ii = 1; ii <= 9; ii++)  //Table (9 is gripper)
                {
                    notificationHandles.Add(adsClient.AddDeviceNotificationEx(".Carr[" + ii + "]", AdsTransMode.OnChange, 100, 0, ".Carr[" + ii + "]", typeof(short)));
                }
                for (int ii = 21; ii <= 26; ii++)  //Stahli
                {
                   // notificationHandles.Add(adsClient.AddDeviceNotificationEx(".Carr[" + ii + "]", AdsTransMode.OnChange, 100, 0, ".Carr[" + ii + "]", typeof(short)));
                    notificationHandles.Add(adsClient.AddDeviceNotificationEx(".CarrStahli_StateHMI[" + ii + "]", AdsTransMode.OnChange, 100, 0, ".CarrStahli_StateHMI[" + ii + "]", typeof(short)));
                }
                for (int ii = 31; ii <= 42; ii++)  //Stacker
                {
                    notificationHandles.Add(adsClient.AddDeviceNotificationEx(".Carr[" + ii + "]", AdsTransMode.OnChange, 100, 0, ".Carr[" + ii + "]", typeof(short)));
                }
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Measuring.ar_CarrToMeasure[1]", AdsTransMode.OnChange, 100, 0, "prg_Measuring.ar_CarrToMeasure[1]", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Measuring.ar_CarrToMeasure[2]", AdsTransMode.OnChange, 100, 0, "prg_Measuring.ar_CarrToMeasure[2]", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Measuring.ar_CarrToMeasure[3]", AdsTransMode.OnChange, 100, 0, "prg_Measuring.ar_CarrToMeasure[3]", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Measuring.ar_CarrToMeasure[4]", AdsTransMode.OnChange, 100, 0, "prg_Measuring.ar_CarrToMeasure[4]", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Measuring.ar_CarrToMeasure[5]", AdsTransMode.OnChange, 100, 0, "prg_Measuring.ar_CarrToMeasure[5]", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx("prg_Measuring.ar_CarrToMeasure[6]", AdsTransMode.OnChange, 100, 0, "prg_Measuring.ar_CarrToMeasure[6]", typeof(bool)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".bIntZone[1]", AdsTransMode.OnChange, 100, 0, ".bIntZone[1]", typeof(bool)));

                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ar_MeasTolerance.fCorrHigh2", AdsTransMode.OnChange, 100, 0, ".ar_MeasTolerance.fCorrHigh2", typeof(float)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ar_MeasTolerance.fCorrHigh1", AdsTransMode.OnChange, 100, 0, ".ar_MeasTolerance.fCorrHigh1", typeof(float)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ar_MeasTolerance.fTolHigh", AdsTransMode.OnChange, 100, 0, ".ar_MeasTolerance.fTolHigh", typeof(float)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ar_MeasTolerance.fTarget", AdsTransMode.OnChange, 100, 0, ".ar_MeasTolerance.fTarget", typeof(float)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ar_MeasTolerance.fTolLow", AdsTransMode.OnChange, 100, 0, ".ar_MeasTolerance.fTolLow", typeof(float)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ar_MeasTolerance.fCorrLow1", AdsTransMode.OnChange, 100, 0, ".ar_MeasTolerance.fCorrLow1", typeof(float)));
                notificationHandles.Add(adsClient.AddDeviceNotificationEx(".ar_MeasTolerance.fCorrLow2", AdsTransMode.OnChange, 100, 0, ".ar_MeasTolerance.fCorrLow2", typeof(float)));
                
                //--------------------------------------------  
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "PLC No Comunication", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    AppGen.Inst.MDImain.frmTitle.plcInvalidFleg = false;
                }
                //MessageBox.Show(ex.Message);
                AppGen.Inst.LogFile(ex.Message, LogType.SystemErr);
            }
		}

        private void adsClient_AdsNotificationEx(object sender, AdsNotificationExEventArgs e)
        {
            if (AppGen.Inst.MDImain == null) return;
            if (!AppGen.Inst.MDImain.chkPlcON.Checked) return;      
            string name = e.UserData.ToString();
            Type type = e.Value.GetType();

   
            if (type == typeof(PlcErrMsg))
            {
                if (name == ".act_Error_Load_HMI") LoadConv_PlcErrMsg = (PlcErrMsg)e.Value;
                if (name == ".act_Error_Unload_HMI") UnloadConv_PlcErrMsg = (PlcErrMsg)e.Value;
                if (name == ".act_Error_Table_HMI") IndexTable_PlcErrMsg = (PlcErrMsg)e.Value;
                if (name == ".act_Error_General_HMI") General_PlcErrMsg = (PlcErrMsg)e.Value; //qq

                if (AppGen.Inst.MDImain.frmPlcErr.Visible)      //Japan 30.6.15
                    AppGen.Inst.MDImain.frmPlcErr.RefreshLists();
            }
            else
            {
                //----------robots:----------
                if (name == ".db_Rob[1].q_ReqPauseCycle") RobotDada.Q_ReqPauseCycle = (bool)e.Value;
                if (name == ".db_Rob[1].q_StopCycleResetAll") RobotDada.Q_StopCycleResetAll = (bool)e.Value;
                if (name == ".db_Rob[1].q_ReqResumeCycle") RobotDada.Q_ReqResumeCycle = (bool)e.Value;
                if (name == ".db_Rob[1].q_CarrReady") RobotDada.Q_CarrReady = (bool)e.Value;
                if (name == ".db_Rob[1].q_TrayReady") RobotDada.Q_TrayReady = (bool)e.Value;
                if (name == ".db_Rob[1].q_FoundInserts") RobotDada.Q_FoundInserts = (bool)e.Value;
                if (name == ".db_Rob[1].q_DataSended") RobotDada.Q_DataSended = (bool)e.Value;
                if (name == ".db_Rob[1].q_CarrDone") RobotDada.Q_CarrDone = (bool)e.Value;
                if (name == ".db_Rob[1].i_Rob_snap_req_1") RobotDada.I_Rob_snap_req_1 = (bool)e.Value;
                if (name == ".db_Rob[1].i_Rob_snap_req_2") RobotDada.I_Rob_snap_req_2 = (bool)e.Value;
                if (name == ".db_Rob[1].i_Rob_snap_req_3") RobotDada.I_Rob_snap_req_3 = (bool)e.Value;
                if (name == ".db_Rob[1].i_CycleDone") RobotDada.I_CycleDone = (bool)e.Value;
                if (name == ".db_Rob[1].i_RobotSafePos") RobotDada.I_RobotSafePos = (bool)e.Value;
                if (name == ".db_Rob[1].i_InsertPlaced") RobotDada.I_InsertPlaced = (bool)e.Value;
                if (name == ".db_Rob[1].i_LifeBit") RobotDada.I_LifeBit = (bool)e.Value;
                if (name == ".db_Rob[1].i_DataReceived") RobotDada.I_DataReceived = (bool)e.Value;
                if (name == ".db_Rob[1].vb_VisionEmpty") RobotDada.Vb_VisionEmpty = (bool)e.Value;
                if (name == ".db_Rob[1].LoadingFinish") RobotDada.LoadingFinish = (bool)e.Value;
                if (name == ".db_Rob[1].LastCarrier") RobotDada.LastCarrier = (bool)e.Value;
                if (name == ".db_Rob[1].Req_ReplaceTray") RobotDada.Req_ReplaceTray = (bool)e.Value;
                if (name == ".db_Rob[1].Vb_VisionDone") RobotDada.Vb_VisionDone = (bool)e.Value;
                if (name == ".db_Rob[1].ProcesTime") RobotDada.ProcesTime = (short)e.Value;
                if (name == ".db_Rob[1].ProcesPercent") RobotDada.ProcesPercent = (short)e.Value;

                if (name == ".db_Rob[2].q_ReqPauseCycle") RobotDada2.Q_ReqPauseCycle = (bool)e.Value;
                if (name == ".db_Rob[2].q_StopCycleResetAll") RobotDada2.Q_StopCycleResetAll = (bool)e.Value;
                if (name == ".db_Rob[2].q_ReqResumeCycle") RobotDada2.Q_ReqResumeCycle = (bool)e.Value;
                if (name == ".db_Rob[2].q_CarrReady") RobotDada2.Q_CarrReady = (bool)e.Value;
                if (name == ".db_Rob[2].q_TrayReady") RobotDada2.Q_TrayReady = (bool)e.Value;
                if (name == ".db_Rob[2].q_FoundInserts") RobotDada2.Q_FoundInserts = (bool)e.Value;
                if (name == ".db_Rob[2].q_DataSended") RobotDada2.Q_DataSended = (bool)e.Value;
                if (name == ".db_Rob[2].q_CaliberReady") RobotDada2.Q_CaliberReady = (bool)e.Value;
                if (name == ".db_Rob[2].q_CarrDone") RobotDada2.Q_CarrDone = (bool)e.Value;
                if (name == ".db_Rob[2].i_Rob_snap_req_1") RobotDada2.I_Rob_snap_req_1 = (bool)e.Value;
                if (name == ".db_Rob[2].i_Rob_snap_req_2") RobotDada2.I_Rob_snap_req_2 = (bool)e.Value;
                if (name == ".db_Rob[2].i_Rob_snap_req_3") RobotDada2.I_Rob_snap_req_3 = (bool)e.Value;
                if (name == ".db_Rob[2].i_CycleDone") RobotDada2.I_CycleDone = (bool)e.Value;
                if (name == ".db_Rob[2].i_RobotSafePos") RobotDada2.I_RobotSafePos = (bool)e.Value;
                if (name == ".db_Rob[2].i_InsertPlaced") RobotDada2.I_InsertPlaced = (bool)e.Value;
                if (name == ".db_Rob[2].i_LifeBit") RobotDada2.I_LifeBit = (bool)e.Value;
                if (name == ".db_Rob[2].i_MeasurePlaced") RobotDada2.I_MeasurePlaced = (bool)e.Value;
                if (name == ".db_Rob[2].i_DataReceived") RobotDada2.I_DataReceived = (bool)e.Value;
                if (name == ".db_Rob[2].vb_VisionEmpty") RobotDada2.Vb_VisionEmpty = (bool)e.Value;
                if (name == ".db_Rob[2].LoadingFinish") RobotDada2.LoadingFinish = (bool)e.Value;
                if (name == ".db_Rob[2].LastCarrier") RobotDada2.LastCarrier = (bool)e.Value;
                if (name == ".db_Rob[2].Req_ReplaceTray") RobotDada2.Req_ReplaceTray = (bool)e.Value;
                if (name == ".db_Rob[2].Req_Measure") RobotDada2.Req_Measure = (bool)e.Value;
                if (name == ".db_Rob[2].MeasureDone") RobotDada2.MeasureDone = (bool)e.Value;
                if (name == ".db_Rob[2].ProcesTime") RobotDada2.ProcesTime = (short)e.Value;
                if (name == ".db_Rob[2].ProcesPercent") RobotDada2.ProcesPercent = (short)e.Value;

                if (IsRobotDataVar(name))
                {
                    FillStructControls();
                }

                //----------GeneralControl:----------
                if (name == ".TopLight") GeneralControl_PLC.TopLight = (bool)e.Value;
                if (name == ".BottomLight1") GeneralControl_PLC.BottomLight1 = (bool)e.Value;
                if (name == ".trafic_Green") GeneralControl_PLC.GreenLight = (bool)e.Value;
                if (name == ".trafic_Yelow") GeneralControl_PLC.YellowLight = (bool)e.Value;
                if (name == ".trafic_Red") GeneralControl_PLC.RedLight = (bool)e.Value;
                if (name == ".trafic_Buzzer") GeneralControl_PLC.Buzzer = (bool)e.Value;
                if (name == ".Req_s3") GeneralControl_PLC.S3 = (bool)e.Value;
                if (name == "prg_Production.Message") GeneralControl_PLC.ProductionMessage = (string)e.Value;
                if (name == ".s_LastMeasure") GeneralControl_PLC.LastMeasure = (string)e.Value;
                if (name == ".m_Prod") GeneralControl_PLC.ProdMode = (short)e.Value;
                if (name == "prg_Safety.Req_Release_ES") GeneralControl_PLC.Req_EmergencyRelease = (bool)e.Value;
                if (name == "prg_Safety.Req_Lock") GeneralControl_PLC.Req_Lock = (bool)e.Value;                
                if (name == ".TSafeIn.ES_State") GeneralControl_PLC.ES_State = (bool)e.Value;
                if (name == ".ResumeAll") GeneralControl_PLC.ResumeAll = (bool)e.Value;
                if (name == "prg_Messages.ResetErrors") GeneralControl_PLC.ResetErrors = (bool)e.Value;
                if (name == ".bIntZone[1]") GeneralControl_PLC.ZoneLocked = (bool)e.Value;
                if (name == ".ar_MeasTolerance.fCorrHigh2") GeneralControl_PLC.CorrHigh2 = (float)e.Value;
                if (name == ".ar_MeasTolerance.fCorrHigh1") GeneralControl_PLC.CorrHigh1 = (float)e.Value;
                if (name == ".ar_MeasTolerance.fTolHigh") GeneralControl_PLC.TolHigh = (float)e.Value;
                if (name == ".ar_MeasTolerance.fTarget") GeneralControl_PLC.Target = (float)e.Value;
                if (name == ".ar_MeasTolerance.fTolLow") GeneralControl_PLC.TolLow = (float)e.Value;
                if (name == ".ar_MeasTolerance.fCorrLow1") GeneralControl_PLC.CorrLow1 = (float)e.Value;
                if (name == ".ar_MeasTolerance.fCorrLow2") GeneralControl_PLC.CorrLow2 = (float)e.Value;
                if (name == ".iActWarning") GeneralControl_PLC.GeneralErrorCount = (short)e.Value;  //qq
                if (name == ".iActMessages") GeneralControl_PLC.GeneralWarningCount = (short)e.Value;  // Japan 01.07.15          
                if (name == ".b_LastBatch") GeneralControl_PLC.LastBatch = (bool)e.Value; //qq
                if (name == ".db_Stahli.out_AutomationReady") GeneralControl_PLC.StahliOutAutomationReady = (bool)e.Value; //Japan 30.6.15
                if (name == ".db_Stahli.in_ReadyForUnload") GeneralControl_PLC.StahliInReadyForUnload = (bool)e.Value; //Japan 30.6.15
                //--------Load conveyor:-----------
                if (name == ".st_LoadReady") LoadConveyor_PLC.fl_ConvReady = (bool)e.Value;
                if (name == ".op_Load_VB") LoadConveyor_PLC.Op_VB = (short)e.Value;
                if (name == ".m_Load_VB") LoadConveyor_PLC.fl_ConvMode = (short)e.Value;               
                if (name == ".fb_ConvL.Init_IncludeStations") LoadConveyor_PLC.InitWithStation = (bool)e.Value;
                if (name == ".db_ConvL.Req_InitConv") LoadConveyor_PLC.InitConv = (bool)e.Value;
                if (name == ".fb_ConvL.ResumeSFC") LoadConveyor_PLC.ConvResume = (bool)e.Value;
                if (name == ".fb_ConvL.ResetError") LoadConveyor_PLC.ResErr = (bool)e.Value;
                if (name == ".fb_ConvL.SFCInit") LoadConveyor_PLC.ResCycle = (bool)e.Value;
                if (name == ".db_WorkST[1].req_LoadTray") LoadConveyor_PLC.TrayManUp = (bool)e.Value;
                if (name == ".db_WorkST[1].req_UnloadTray") LoadConveyor_PLC.TrayManDown = (bool)e.Value;
                if (name == ".act_ErrorNum_Load") LoadConveyor_PLC.ConvErrorCount = (short)e.Value;
                //--------Unload conveyor:---------
                if (name == ".st_UnloadReady") UnloadConveyor_PLC.fl_ConvReady = (bool)e.Value;
                if (name == ".op_Unload_VB") UnloadConveyor_PLC.Op_VB = (short)e.Value;
                if (name == ".m_Unload_VB") UnloadConveyor_PLC.fl_ConvMode = (short)e.Value;
                if (name == ".fb_ConvU.Init_IncludeStations") UnloadConveyor_PLC.InitWithStation = (bool)e.Value;
                if (name == ".db_ConvU.Req_InitConv") UnloadConveyor_PLC.InitConv = (bool)e.Value;
                if (name == ".fb_ConvU.ResumeSFC") UnloadConveyor_PLC.ConvResume = (bool)e.Value;
                if (name == ".fb_ConvU.ResetError") UnloadConveyor_PLC.ResErr = (bool)e.Value;
                if (name == ".fb_ConvU.SFCInit") UnloadConveyor_PLC.ResCycle = (bool)e.Value;
                if (name == ".db_WorkST[2].req_LoadTray") UnloadConveyor_PLC.TrayManUp = (bool)e.Value;
                if (name == ".db_WorkST[2].req_UnloadTray") UnloadConveyor_PLC.TrayManDown = (bool)e.Value;
                if (name == ".act_ErrorNum_Unload") UnloadConveyor_PLC.ConvErrorCount = (short)e.Value;
                //--------Index Table:-----------
                if (name == ".st_StepConvReady") IndexTable_PLC.fl_StepConvReady = (bool)e.Value;
                if (name == ".m_Table_VB") IndexTable_PLC.fl_TableMode = (short)e.Value;
                if (name == ".op_Table_VB") IndexTable_PLC.Op_VB = (short)e.Value;
                if (name == "prg_Stepper.ErrorSFC") IndexTable_PLC.fl_TableError = (bool)e.Value;
                if (name == "prg_Stepper.ResumeSFC") IndexTable_PLC.req_ResetTableError = (bool)e.Value;
                if (name == ".act_ErrorNum_Table") IndexTable_PLC.TableConvErrorCount = (short)e.Value;
                for (int ii = 1; ii <= 9; ii++)  //Table (9 is gripper)
                {
                    if (name == ".Carr[" + ii + "]") IndexTable_PLC.ia_SliceStatus[ii] = (short)e.Value;
                }
                for (int ii = 21; ii <= 26; ii++)  //Stahli
                {
                   // if (name == ".Carr[" + ii + "]") IndexTable_PLC.ia_SliceStatus[ii] = (short)e.Value;
                    if (name == ".CarrStahli_StateHMI[" + ii + "]") IndexTable_PLC.ia_SliceStatus[ii] = (short)e.Value;
                }
                for (int ii = 31; ii <= 42; ii++)  //Stacker
                {
                    if (name == ".Carr[" + ii + "]") IndexTable_PLC.ia_SliceStatus[ii] = (short)e.Value;
                }  
                if (name == "prg_Measuring.ar_CarrToMeasure[1]") IndexTable_PLC.carrToMeasure[1] = (bool)e.Value;
                if (name == "prg_Measuring.ar_CarrToMeasure[2]") IndexTable_PLC.carrToMeasure[2] = (bool)e.Value;
                if (name == "prg_Measuring.ar_CarrToMeasure[3]") IndexTable_PLC.carrToMeasure[3] = (bool)e.Value;
                if (name == "prg_Measuring.ar_CarrToMeasure[4]") IndexTable_PLC.carrToMeasure[4] = (bool)e.Value;
                if (name == "prg_Measuring.ar_CarrToMeasure[5]") IndexTable_PLC.carrToMeasure[5] = (bool)e.Value;
                if (name == "prg_Measuring.ar_CarrToMeasure[6]") IndexTable_PLC.carrToMeasure[6] = (bool)e.Value;              
                //--------------------------------------------
            }
        }
        private bool IsRobotDataVar(string varName)
        {
            if (varName == ".db_Rob[1].q_ReqPauseCycle") return true;
            if (varName == ".db_Rob[1].q_StopCycleResetAll") return true;
            if (varName == ".db_Rob[1].q_ReqResumeCycle") return true;
            if (varName == ".db_Rob[1].q_CarrReady") return true;
            if (varName == ".db_Rob[1].q_TrayReady") return true;
            if (varName == ".db_Rob[1].q_FoundInserts") return true;
            if (varName == ".db_Rob[1].q_DataSended") return true;
            if (varName == ".db_Rob[1].q_CarrDone") return true;
            if (varName == ".db_Rob[1].i_Rob_snap_req_1") return true;
            if (varName == ".db_Rob[1].i_Rob_snap_req_2") return true;
            if (varName == ".db_Rob[1].i_Rob_snap_req_3") return true;
            if (varName == ".db_Rob[1].i_CycleDone") return true;
            if (varName == ".db_Rob[1].i_RobotSafePos") return true;
            if (varName == ".db_Rob[1].i_InsertPlaced") return true;
            if (varName == ".db_Rob[1].i_LifeBit") return true;
            if (varName == ".db_Rob[1].i_DataReceived") return true;
            if (varName == ".db_Rob[1].vb_VisionEmpty") return true;
            if (varName == ".db_Rob[1].LoadingFinish") return true;
            if (varName == ".db_Rob[1].LastCarrier") return true;
            if (varName == ".db_Rob[1].Req_ReplaceTray") return true;
            if (varName == ".db_Rob[1].Vb_VisionDone") return true;
            if (varName == ".db_Rob[1].ProcesTime") return true;
            if (varName == ".db_Rob[1].ProcesPercent") return true;

            if (varName == ".db_Rob[2].q_ReqPauseCycle") return true;
            if (varName == ".db_Rob[2].q_StopCycleResetAll") return true;
            if (varName == ".db_Rob[2].q_ReqResumeCycle") return true;
            if (varName == ".db_Rob[2].q_CarrReady") return true;
            if (varName == ".db_Rob[2].q_TrayReady") return true;
            if (varName == ".db_Rob[2].q_FoundInserts") return true;
            if (varName == ".db_Rob[2].q_DataSended") return true;
            if (varName == ".db_Rob[2].q_CaliberReady") return true;        
            if (varName == ".db_Rob[2].q_CarrDone") return true;
            if (varName == ".db_Rob[2].i_Rob_snap_req_1") return true;
            if (varName == ".db_Rob[2].i_Rob_snap_req_2") return true;
            if (varName == ".db_Rob[2].i_Rob_snap_req_3") return true;
            if (varName == ".db_Rob[2].i_CycleDone") return true;
            if (varName == ".db_Rob[2].i_RobotSafePos") return true;
            if (varName == ".db_Rob[2].i_InsertPlaced") return true;
            if (varName == ".db_Rob[2].i_LifeBit") return true;
            if (varName == ".db_Rob[2].i_MeasurePlaced") return true;           
            if (varName == ".db_Rob[2].i_DataReceived") return true;
            if (varName == ".db_Rob[2].vb_VisionEmpty") return true;
            if (varName == ".db_Rob[2].LoadingFinish") return true;
            if (varName == ".db_Rob[2].LastCarrier") return true;
            if (varName == ".db_Rob[2].Req_ReplaceTray") return true;
            if (varName == ".db_Rob[2].Req_Measure") return true;
            if (varName == ".db_Rob[2].MeasureDone") return true;
            if (varName == ".db_Rob[2].ProcesTime") return true;
            if (varName == ".db_Rob[2].ProcesPercent") return true;

            return false;
        }

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.textBox45 = new System.Windows.Forms.TextBox();
            this.textBox49 = new System.Windows.Forms.TextBox();
            this.textBox50 = new System.Windows.Forms.TextBox();
            this.textBox51 = new System.Windows.Forms.TextBox();
            this.textBox52 = new System.Windows.Forms.TextBox();
            this.textBox55 = new System.Windows.Forms.TextBox();
            this.textBox56 = new System.Windows.Forms.TextBox();
            this.GroupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(259, 447);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(112, 23);
            this.btnWrite.TabIndex = 11;
            this.btnWrite.Text = "Write";
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(141, 447);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(112, 23);
            this.btnRead.TabIndex = 10;
            this.btnRead.Text = "Read";
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.label45);
            this.GroupBox1.Controls.Add(this.label44);
            this.GroupBox1.Controls.Add(this.label43);
            this.GroupBox1.Controls.Add(this.label42);
            this.GroupBox1.Controls.Add(this.label41);
            this.GroupBox1.Controls.Add(this.label39);
            this.GroupBox1.Controls.Add(this.label38);
            this.GroupBox1.Controls.Add(this.label36);
            this.GroupBox1.Controls.Add(this.label35);
            this.GroupBox1.Controls.Add(this.label34);
            this.GroupBox1.Controls.Add(this.label33);
            this.GroupBox1.Controls.Add(this.label30);
            this.GroupBox1.Controls.Add(this.label24);
            this.GroupBox1.Controls.Add(this.label21);
            this.GroupBox1.Controls.Add(this.label20);
            this.GroupBox1.Controls.Add(this.label19);
            this.GroupBox1.Controls.Add(this.label18);
            this.GroupBox1.Controls.Add(this.label17);
            this.GroupBox1.Controls.Add(this.label16);
            this.GroupBox1.Controls.Add(this.label15);
            this.GroupBox1.Controls.Add(this.textBox28);
            this.GroupBox1.Controls.Add(this.textBox27);
            this.GroupBox1.Controls.Add(this.textBox24);
            this.GroupBox1.Controls.Add(this.textBox23);
            this.GroupBox1.Controls.Add(this.textBox22);
            this.GroupBox1.Controls.Add(this.textBox21);
            this.GroupBox1.Controls.Add(this.textBox17);
            this.GroupBox1.Controls.Add(this.textBox15);
            this.GroupBox1.Controls.Add(this.textBox9);
            this.GroupBox1.Controls.Add(this.textBox8);
            this.GroupBox1.Controls.Add(this.textBox7);
            this.GroupBox1.Controls.Add(this.textBox6);
            this.GroupBox1.Controls.Add(this.textBox5);
            this.GroupBox1.Controls.Add(this.textBox4);
            this.GroupBox1.Controls.Add(this.textBox3);
            this.GroupBox1.Controls.Add(this.textBox2);
            this.GroupBox1.Controls.Add(this.textBox1);
            this.GroupBox1.Location = new System.Drawing.Point(8, 8);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(244, 427);
            this.GroupBox1.TabIndex = 7;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Robot1 Strubt";
            // 
            // label45
            // 
            this.label45.Location = new System.Drawing.Point(14, 168);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(107, 16);
            this.label45.TabIndex = 70;
            this.label45.Text = "q_DoorClose";
            // 
            // label44
            // 
            this.label44.Location = new System.Drawing.Point(14, 122);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(107, 16);
            this.label44.TabIndex = 69;
            this.label44.Text = "q_TrayReady";
            // 
            // label43
            // 
            this.label43.Location = new System.Drawing.Point(14, 99);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(107, 16);
            this.label43.TabIndex = 68;
            this.label43.Text = "q_CarrReady";
            // 
            // label42
            // 
            this.label42.Location = new System.Drawing.Point(14, 214);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(107, 16);
            this.label42.TabIndex = 67;
            this.label42.Text = "Rob_snap_req_2";
            // 
            // label41
            // 
            this.label41.Location = new System.Drawing.Point(14, 191);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(107, 16);
            this.label41.TabIndex = 66;
            this.label41.Text = "Rob_snap_req_1";
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(14, 398);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(107, 16);
            this.label39.TabIndex = 64;
            this.label39.Text = "ProcesPercent";
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(14, 375);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(107, 16);
            this.label38.TabIndex = 63;
            this.label38.Text = "ProcesTime";
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(14, 352);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(107, 16);
            this.label36.TabIndex = 61;
            this.label36.Text = "Req_ReplaceTray";
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(14, 306);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(107, 16);
            this.label35.TabIndex = 60;
            this.label35.Text = "LoadingFinish";
            // 
            // label34
            // 
            this.label34.Location = new System.Drawing.Point(14, 283);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(107, 16);
            this.label34.TabIndex = 59;
            this.label34.Text = "vb_VisionEmpty";
            // 
            // label33
            // 
            this.label33.Location = new System.Drawing.Point(14, 329);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(107, 16);
            this.label33.TabIndex = 58;
            this.label33.Text = "LastCarrier";
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(14, 260);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(107, 16);
            this.label30.TabIndex = 55;
            this.label30.Text = "i_RobotSafePos";
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(14, 237);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(107, 16);
            this.label24.TabIndex = 49;
            this.label24.Text = "i_CycleDone";
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(14, 76);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(107, 16);
            this.label21.TabIndex = 46;
            this.label21.Text = "q_ReqResumeCycle";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(14, 53);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(110, 16);
            this.label20.TabIndex = 45;
            this.label20.Text = "q_StopCycleResetAll";
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(14, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(107, 16);
            this.label19.TabIndex = 44;
            this.label19.Text = "q_ReqPauseCycle";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(14, 145);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(107, 16);
            this.label18.TabIndex = 43;
            this.label18.Text = "q_FoundOrientation";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(203, -23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(40, 10);
            this.label17.TabIndex = 42;
            this.label17.Text = "bool1:";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(203, -49);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 10);
            this.label16.TabIndex = 41;
            this.label16.Text = "bool1:";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(203, -81);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 40;
            this.label15.Text = "bool1:";
            // 
            // textBox28
            // 
            this.textBox28.Location = new System.Drawing.Point(127, 395);
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new System.Drawing.Size(104, 20);
            this.textBox28.TabIndex = 39;
            // 
            // textBox27
            // 
            this.textBox27.Location = new System.Drawing.Point(127, 372);
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new System.Drawing.Size(104, 20);
            this.textBox27.TabIndex = 38;
            // 
            // textBox24
            // 
            this.textBox24.Location = new System.Drawing.Point(127, 349);
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new System.Drawing.Size(104, 20);
            this.textBox24.TabIndex = 35;
            // 
            // textBox23
            // 
            this.textBox23.Location = new System.Drawing.Point(127, 280);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(104, 20);
            this.textBox23.TabIndex = 34;
            // 
            // textBox22
            // 
            this.textBox22.Location = new System.Drawing.Point(127, 326);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(104, 20);
            this.textBox22.TabIndex = 33;
            // 
            // textBox21
            // 
            this.textBox21.Location = new System.Drawing.Point(127, 303);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(104, 20);
            this.textBox21.TabIndex = 32;
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(127, 257);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(104, 20);
            this.textBox17.TabIndex = 28;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(127, 234);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(104, 20);
            this.textBox15.TabIndex = 26;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(127, 73);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(104, 20);
            this.textBox9.TabIndex = 20;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(127, 50);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(104, 20);
            this.textBox8.TabIndex = 19;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(127, 27);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(104, 20);
            this.textBox7.TabIndex = 18;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(127, 142);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(104, 20);
            this.textBox6.TabIndex = 17;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(127, 211);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(104, 20);
            this.textBox5.TabIndex = 16;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(127, 188);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(104, 20);
            this.textBox4.TabIndex = 15;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(127, 119);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(104, 20);
            this.textBox3.TabIndex = 14;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(127, 96);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(104, 20);
            this.textBox2.TabIndex = 13;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(127, 165);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(104, 20);
            this.textBox1.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox29);
            this.groupBox2.Controls.Add(this.textBox30);
            this.groupBox2.Controls.Add(this.textBox31);
            this.groupBox2.Controls.Add(this.textBox32);
            this.groupBox2.Controls.Add(this.textBox34);
            this.groupBox2.Controls.Add(this.textBox36);
            this.groupBox2.Controls.Add(this.textBox37);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label47);
            this.groupBox2.Controls.Add(this.label53);
            this.groupBox2.Controls.Add(this.label56);
            this.groupBox2.Controls.Add(this.label57);
            this.groupBox2.Controls.Add(this.label58);
            this.groupBox2.Controls.Add(this.label59);
            this.groupBox2.Controls.Add(this.label60);
            this.groupBox2.Controls.Add(this.label61);
            this.groupBox2.Controls.Add(this.label62);
            this.groupBox2.Controls.Add(this.textBox35);
            this.groupBox2.Controls.Add(this.textBox43);
            this.groupBox2.Controls.Add(this.textBox45);
            this.groupBox2.Controls.Add(this.textBox49);
            this.groupBox2.Controls.Add(this.textBox50);
            this.groupBox2.Controls.Add(this.textBox51);
            this.groupBox2.Controls.Add(this.textBox52);
            this.groupBox2.Controls.Add(this.textBox55);
            this.groupBox2.Controls.Add(this.textBox56);
            this.groupBox2.Location = new System.Drawing.Point(268, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 423);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Robot2 Strubt";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // textBox29
            // 
            this.textBox29.Location = new System.Drawing.Point(120, 166);
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new System.Drawing.Size(104, 20);
            this.textBox29.TabIndex = 83;
            // 
            // textBox30
            // 
            this.textBox30.Location = new System.Drawing.Point(120, 94);
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new System.Drawing.Size(104, 20);
            this.textBox30.TabIndex = 82;
            // 
            // textBox31
            // 
            this.textBox31.Location = new System.Drawing.Point(120, 118);
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new System.Drawing.Size(104, 20);
            this.textBox31.TabIndex = 81;
            // 
            // textBox32
            // 
            this.textBox32.Location = new System.Drawing.Point(120, 190);
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new System.Drawing.Size(104, 20);
            this.textBox32.TabIndex = 80;
            // 
            // textBox34
            // 
            this.textBox34.Location = new System.Drawing.Point(120, 142);
            this.textBox34.Name = "textBox34";
            this.textBox34.Size = new System.Drawing.Size(104, 20);
            this.textBox34.TabIndex = 78;
            // 
            // textBox36
            // 
            this.textBox36.Location = new System.Drawing.Point(120, 46);
            this.textBox36.Name = "textBox36";
            this.textBox36.Size = new System.Drawing.Size(104, 20);
            this.textBox36.TabIndex = 77;
            // 
            // textBox37
            // 
            this.textBox37.Location = new System.Drawing.Point(120, 70);
            this.textBox37.Name = "textBox37";
            this.textBox37.Size = new System.Drawing.Size(104, 20);
            this.textBox37.TabIndex = 76;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 171);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 70;
            this.label1.Text = "q_DoorClose";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(7, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 69;
            this.label2.Text = "q_TrayReady";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 16);
            this.label3.TabIndex = 68;
            this.label3.Text = "q_CarrReady";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 16);
            this.label5.TabIndex = 66;
            this.label5.Text = "Rob_snap_req_3";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(7, 387);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 16);
            this.label7.TabIndex = 64;
            this.label7.Text = "ProcesPercent";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(7, 363);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 16);
            this.label8.TabIndex = 63;
            this.label8.Text = "ProcesTime";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(7, 339);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 16);
            this.label10.TabIndex = 61;
            this.label10.Text = "Req_ReplaceTray";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(7, 291);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(107, 16);
            this.label11.TabIndex = 60;
            this.label11.Text = "LoadingFinish";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(7, 267);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(107, 16);
            this.label12.TabIndex = 59;
            this.label12.Text = "vb_VisionEmpty";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(7, 315);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 16);
            this.label13.TabIndex = 58;
            this.label13.Text = "LastCarrier";
            // 
            // label47
            // 
            this.label47.Location = new System.Drawing.Point(7, 243);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(107, 16);
            this.label47.TabIndex = 55;
            this.label47.Text = "i_RobotSafePos";
            // 
            // label53
            // 
            this.label53.Location = new System.Drawing.Point(7, 219);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(107, 16);
            this.label53.TabIndex = 49;
            this.label53.Text = "i_CycleDone";
            // 
            // label56
            // 
            this.label56.Location = new System.Drawing.Point(7, 75);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(107, 16);
            this.label56.TabIndex = 46;
            this.label56.Text = "q_ReqResumeCycle";
            // 
            // label57
            // 
            this.label57.Location = new System.Drawing.Point(7, 51);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(110, 16);
            this.label57.TabIndex = 45;
            this.label57.Text = "q_StopCycleResetAll";
            // 
            // label58
            // 
            this.label58.Location = new System.Drawing.Point(7, 27);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(107, 16);
            this.label58.TabIndex = 44;
            this.label58.Text = "q_ReqPauseCycle";
            // 
            // label59
            // 
            this.label59.Location = new System.Drawing.Point(7, 147);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(107, 16);
            this.label59.TabIndex = 43;
            this.label59.Text = "q_FoundInserts";
            // 
            // label60
            // 
            this.label60.Location = new System.Drawing.Point(203, -23);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(40, 10);
            this.label60.TabIndex = 42;
            this.label60.Text = "bool1:";
            // 
            // label61
            // 
            this.label61.Location = new System.Drawing.Point(203, -49);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(40, 10);
            this.label61.TabIndex = 41;
            this.label61.Text = "bool1:";
            // 
            // label62
            // 
            this.label62.Location = new System.Drawing.Point(203, -81);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(40, 16);
            this.label62.TabIndex = 40;
            this.label62.Text = "bool1:";
            // 
            // textBox35
            // 
            this.textBox35.Location = new System.Drawing.Point(120, 22);
            this.textBox35.Name = "textBox35";
            this.textBox35.Size = new System.Drawing.Size(104, 20);
            this.textBox35.TabIndex = 33;
            // 
            // textBox43
            // 
            this.textBox43.Location = new System.Drawing.Point(120, 214);
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(104, 20);
            this.textBox43.TabIndex = 25;
            // 
            // textBox45
            // 
            this.textBox45.Location = new System.Drawing.Point(120, 238);
            this.textBox45.Name = "textBox45";
            this.textBox45.Size = new System.Drawing.Size(104, 20);
            this.textBox45.TabIndex = 23;
            // 
            // textBox49
            // 
            this.textBox49.Location = new System.Drawing.Point(120, 286);
            this.textBox49.Name = "textBox49";
            this.textBox49.Size = new System.Drawing.Size(104, 20);
            this.textBox49.TabIndex = 19;
            // 
            // textBox50
            // 
            this.textBox50.Location = new System.Drawing.Point(120, 310);
            this.textBox50.Name = "textBox50";
            this.textBox50.Size = new System.Drawing.Size(104, 20);
            this.textBox50.TabIndex = 18;
            // 
            // textBox51
            // 
            this.textBox51.Location = new System.Drawing.Point(120, 262);
            this.textBox51.Name = "textBox51";
            this.textBox51.Size = new System.Drawing.Size(104, 20);
            this.textBox51.TabIndex = 17;
            // 
            // textBox52
            // 
            this.textBox52.Location = new System.Drawing.Point(120, 334);
            this.textBox52.Name = "textBox52";
            this.textBox52.Size = new System.Drawing.Size(104, 20);
            this.textBox52.TabIndex = 16;
            // 
            // textBox55
            // 
            this.textBox55.Location = new System.Drawing.Point(120, 358);
            this.textBox55.Name = "textBox55";
            this.textBox55.Size = new System.Drawing.Size(104, 20);
            this.textBox55.TabIndex = 13;
            // 
            // textBox56
            // 
            this.textBox56.Location = new System.Drawing.Point(120, 382);
            this.textBox56.Name = "textBox56";
            this.textBox56.Size = new System.Drawing.Size(104, 20);
            this.textBox56.TabIndex = 12;
            // 
            // FrmBeckhoff
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(512, 481);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.GroupBox1);
            this.Name = "FrmBeckhoff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ADS - RobotStruct";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MDImain_Closing);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserCloseForm);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
        
		[STAThread]
        private void MDImain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (adsClient != null) adsClient.Dispose();
		}

        public void btnRead_Click(object sender, System.EventArgs e)  //
		{
            if (!AppGen.Inst.MDImain.chkPlcON.Checked) return;      
            try
            {
                FillStructControls();

                LoadConv_PlcErrMsg = (PlcErrMsg)adsClient.ReadAny(hPlcErrMsg0, typeof(PlcErrMsg));
                UnloadConv_PlcErrMsg = (PlcErrMsg)adsClient.ReadAny(hPlcErrMsg1, typeof(PlcErrMsg));
                IndexTable_PlcErrMsg = (PlcErrMsg)adsClient.ReadAny(hPlcErrMsg2, typeof(PlcErrMsg));
                General_PlcErrMsg = (PlcErrMsg)adsClient.ReadAny(hPlcErrMsg3, typeof(PlcErrMsg)); //qq

                GeneralControl_PLC.TopLight = (bool)adsClient.ReadAny(GeneralControl_PLC.hTopLight, typeof(bool));
                GeneralControl_PLC.BottomLight1 = (bool)adsClient.ReadAny(GeneralControl_PLC.hBottomLight1, typeof(bool));
                GeneralControl_PLC.GreenLight = (bool)adsClient.ReadAny(GeneralControl_PLC.hGreenLight, typeof(bool));
                GeneralControl_PLC.YellowLight = (bool)adsClient.ReadAny(GeneralControl_PLC.hYellowLight, typeof(bool));
                GeneralControl_PLC.RedLight = (bool)adsClient.ReadAny(GeneralControl_PLC.hRedLight, typeof(bool));
                GeneralControl_PLC.Buzzer = (bool)adsClient.ReadAny(GeneralControl_PLC.hBuzzer, typeof(bool));
                GeneralControl_PLC.S3 = (bool)adsClient.ReadAny(GeneralControl_PLC.hS3, typeof(bool));
                GeneralControl_PLC.ProductionMessage = (string)adsClient.ReadAny(GeneralControl_PLC.hProductionMessage, typeof(string), new int[] { 40 });
                GeneralControl_PLC.LastMeasure = (string)adsClient.ReadAny(GeneralControl_PLC.hLastMeasure, typeof(string), new int[] { 40 });
                GeneralControl_PLC.ProdMode = (short)adsClient.ReadAny(GeneralControl_PLC.hProdMode, typeof(short));
                GeneralControl_PLC.Req_EmergencyRelease = (bool)adsClient.ReadAny(GeneralControl_PLC.hReq_EmergencyRelease, typeof(bool));
                GeneralControl_PLC.Req_Lock = (bool)adsClient.ReadAny(GeneralControl_PLC.hReq_Lock, typeof(bool));               
                GeneralControl_PLC.ES_State = (bool)adsClient.ReadAny(GeneralControl_PLC.hES_State, typeof(bool));
                GeneralControl_PLC.ResumeAll = (bool)adsClient.ReadAny(GeneralControl_PLC.hResumeAll, typeof(bool));
                GeneralControl_PLC.ResetErrors = (bool)adsClient.ReadAny(GeneralControl_PLC.hResetErrors, typeof(bool));
                GeneralControl_PLC.ZoneLocked = (bool)adsClient.ReadAny(GeneralControl_PLC.hZoneLocked, typeof(bool));
                GeneralControl_PLC.CorrHigh2 = (float)adsClient.ReadAny(GeneralControl_PLC.hCorrHigh2, typeof(float));
                GeneralControl_PLC.CorrHigh1 = (float)adsClient.ReadAny(GeneralControl_PLC.hCorrHigh1, typeof(float));
                GeneralControl_PLC.TolHigh = (float)adsClient.ReadAny(GeneralControl_PLC.hTolHigh, typeof(float));
                GeneralControl_PLC.Target = (float)adsClient.ReadAny(GeneralControl_PLC.hTarget, typeof(float));
                GeneralControl_PLC.TolLow = (float)adsClient.ReadAny(GeneralControl_PLC.hTolLow, typeof(float));
                GeneralControl_PLC.CorrLow1 = (float)adsClient.ReadAny(GeneralControl_PLC.hCorrLow1, typeof(float));
                GeneralControl_PLC.CorrLow2 = (float)adsClient.ReadAny(GeneralControl_PLC.hCorrLow2, typeof(float));
                GeneralControl_PLC.GeneralErrorCount = (short)adsClient.ReadAny(GeneralControl_PLC.hGeneralErrorCount, typeof(short)); //qq
                GeneralControl_PLC.GeneralWarningCount = (short)adsClient.ReadAny(GeneralControl_PLC.hGeneralWarningCount, typeof(short)); //  JApan 01.07.15
                GeneralControl_PLC.LastBatch = (bool)adsClient.ReadAny(GeneralControl_PLC.hLastBatch, typeof(bool));
               

                //--------Load conveyor:-----------
                LoadConveyor_PLC.Op_VB = (short)adsClient.ReadAny(LoadConveyor_PLC.hOp_VB, typeof(short));
                LoadConveyor_PLC.fl_ConvReady = (bool)adsClient.ReadAny(LoadConveyor_PLC.hfl_ConvReady, typeof(bool));
                LoadConveyor_PLC.fl_ConvMode = (short)adsClient.ReadAny(LoadConveyor_PLC.hfl_ConvMode, typeof(short));
                LoadConveyor_PLC.InitWithStation = (bool)adsClient.ReadAny(LoadConveyor_PLC.hInitWithStation, typeof(bool));
                LoadConveyor_PLC.InitConv = (bool)adsClient.ReadAny(LoadConveyor_PLC.hInitConv, typeof(bool));
                LoadConveyor_PLC.ConvResume = (bool)adsClient.ReadAny(LoadConveyor_PLC.hConvResume, typeof(bool));
                LoadConveyor_PLC.ResErr = (bool)adsClient.ReadAny(LoadConveyor_PLC.hResErr, typeof(bool));
                LoadConveyor_PLC.ResCycle = (bool)adsClient.ReadAny(LoadConveyor_PLC.hResCycle, typeof(bool));
                LoadConveyor_PLC.TrayManUp = (bool)adsClient.ReadAny(LoadConveyor_PLC.hTrayManUp, typeof(bool));
                LoadConveyor_PLC.TrayManDown = (bool)adsClient.ReadAny(LoadConveyor_PLC.hTrayManDown, typeof(bool));
                LoadConveyor_PLC.ConvErrorCount = (short)adsClient.ReadAny(LoadConveyor_PLC.hConvErrorCount, typeof(short));
                //--------Unload conveyor:-----------
                UnloadConveyor_PLC.Op_VB = (short)adsClient.ReadAny(UnloadConveyor_PLC.hOp_VB, typeof(short));
                UnloadConveyor_PLC.fl_ConvReady = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hfl_ConvReady, typeof(bool));
                UnloadConveyor_PLC.fl_ConvMode = (short)adsClient.ReadAny(UnloadConveyor_PLC.hfl_ConvMode, typeof(short));
                UnloadConveyor_PLC.InitWithStation = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hInitWithStation, typeof(bool));
                UnloadConveyor_PLC.InitConv = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hInitConv, typeof(bool));
                UnloadConveyor_PLC.ConvResume = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hConvResume, typeof(bool));
                UnloadConveyor_PLC.ResErr = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hResErr, typeof(bool));
                UnloadConveyor_PLC.ResCycle = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hResCycle, typeof(bool));
                UnloadConveyor_PLC.TrayManUp = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hTrayManUp, typeof(bool));
                UnloadConveyor_PLC.TrayManDown = (bool)adsClient.ReadAny(UnloadConveyor_PLC.hTrayManDown, typeof(bool));
                UnloadConveyor_PLC.ConvErrorCount = (short)adsClient.ReadAny(UnloadConveyor_PLC.hConvErrorCount, typeof(short));
                //---------Index Table:------------
                IndexTable_PLC.Op_VB = (short)adsClient.ReadAny(IndexTable_PLC.hOp_VB, typeof(short));
                IndexTable_PLC.fl_StepConvReady = (bool)adsClient.ReadAny(IndexTable_PLC.hfl_StepConvReady, typeof(bool));
                IndexTable_PLC.fl_TableMode = (short)adsClient.ReadAny(IndexTable_PLC.hfl_TableMode, typeof(short));
                IndexTable_PLC.fl_TableError = (bool)adsClient.ReadAny(IndexTable_PLC.hfl_TableError, typeof(bool));
                IndexTable_PLC.req_ResetTableError = (bool)adsClient.ReadAny(IndexTable_PLC.hreq_ResetTableError, typeof(bool));
                IndexTable_PLC.TableConvErrorCount = (short)adsClient.ReadAny(IndexTable_PLC.hTableConvErrorCount, typeof(short));              
                for (int ii = 1; ii <= 9; ii++)  //Table (9 is gripper)
                {
                    IndexTable_PLC.ia_SliceStatus[ii] = (short)adsClient.ReadAny(IndexTable_PLC.hia_SliceStatus[ii], typeof(short));
                }
                for (int ii = 21; ii <= 26; ii++)  //Stahli
                {
                    IndexTable_PLC.ia_SliceStatus[ii] = (short)adsClient.ReadAny(IndexTable_PLC.hia_SliceStatus[ii], typeof(short));
                }
                for (int ii = 31; ii <= 42; ii++)  //Stacker
                {
                    IndexTable_PLC.ia_SliceStatus[ii] = (short)adsClient.ReadAny(IndexTable_PLC.hia_SliceStatus[ii], typeof(short));
                }
                IndexTable_PLC.carrToMeasure[1] = (bool)adsClient.ReadAny(IndexTable_PLC.hcarrToMeasure[1], typeof(bool));
                IndexTable_PLC.carrToMeasure[2] = (bool)adsClient.ReadAny(IndexTable_PLC.hcarrToMeasure[2], typeof(bool));
                IndexTable_PLC.carrToMeasure[3] = (bool)adsClient.ReadAny(IndexTable_PLC.hcarrToMeasure[3], typeof(bool));
                IndexTable_PLC.carrToMeasure[4] = (bool)adsClient.ReadAny(IndexTable_PLC.hcarrToMeasure[4], typeof(bool));
                IndexTable_PLC.carrToMeasure[5] = (bool)adsClient.ReadAny(IndexTable_PLC.hcarrToMeasure[5], typeof(bool));
                IndexTable_PLC.carrToMeasure[6] = (bool)adsClient.ReadAny(IndexTable_PLC.hcarrToMeasure[6], typeof(bool));
            }
            catch (Exception ex)
            {
                AppGen.Inst.MDImain.frmTitle.plcInvalidFleg = true;
                if (MessageBox.Show(ex.Message, "PLC No Comunication", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    AppGen.Inst.MDImain.frmTitle.plcInvalidFleg = false;
                }  
                //MessageBox.Show(ex.Message);
            }
		}
		private void btnWrite_Click(object sender, System.EventArgs e)
		{
            if (!AppGen.Inst.MDImain.chkPlcON.Checked) return;      
			try
			{
                //write by handle
                ////the second parameter is the object to be written to the PLC variable
                //----------robots:----------------
                UpdateStructFromControls();

                adsClient.WriteAny(hPlcErrMsg0, LoadConv_PlcErrMsg);
                adsClient.WriteAny(hPlcErrMsg1, UnloadConv_PlcErrMsg);
                adsClient.WriteAny(hPlcErrMsg2, IndexTable_PlcErrMsg);
                adsClient.WriteAny(hPlcErrMsg3, General_PlcErrMsg); //qq
     
                //----------general Control:----------------
                adsClient.WriteAny(GeneralControl_PLC.hTopLight, GeneralControl_PLC.TopLight);
                adsClient.WriteAny(GeneralControl_PLC.hBottomLight1, GeneralControl_PLC.BottomLight1);
                adsClient.WriteAny(GeneralControl_PLC.hGreenLight, GeneralControl_PLC.GreenLight);
                adsClient.WriteAny(GeneralControl_PLC.hYellowLight, GeneralControl_PLC.YellowLight);
                adsClient.WriteAny(GeneralControl_PLC.hRedLight, GeneralControl_PLC.RedLight);
                adsClient.WriteAny(GeneralControl_PLC.hBuzzer, GeneralControl_PLC.Buzzer);
                adsClient.WriteAny(GeneralControl_PLC.hS3, GeneralControl_PLC.S3);
                adsClient.WriteAny(GeneralControl_PLC.hProdMode, GeneralControl_PLC.ProdMode);
                adsClient.WriteAny(GeneralControl_PLC.hReq_EmergencyRelease, GeneralControl_PLC.Req_EmergencyRelease);
                adsClient.WriteAny(GeneralControl_PLC.hReq_Lock, GeneralControl_PLC.Req_Lock);               
                adsClient.WriteAny(GeneralControl_PLC.hES_State, GeneralControl_PLC.ES_State);
                adsClient.WriteAny(GeneralControl_PLC.hResumeAll, GeneralControl_PLC.ResumeAll);
                adsClient.WriteAny(GeneralControl_PLC.hResetErrors, GeneralControl_PLC.ResetErrors);
                adsClient.WriteAny(GeneralControl_PLC.hZoneLocked, GeneralControl_PLC.ZoneLocked);
                adsClient.WriteAny(GeneralControl_PLC.hCorrHigh2, GeneralControl_PLC.CorrHigh2);
                adsClient.WriteAny(GeneralControl_PLC.hCorrHigh1, GeneralControl_PLC.CorrHigh1);
                adsClient.WriteAny(GeneralControl_PLC.hTolHigh, GeneralControl_PLC.TolHigh);
                adsClient.WriteAny(GeneralControl_PLC.hTarget, GeneralControl_PLC.Target);
                adsClient.WriteAny(GeneralControl_PLC.hTolLow, GeneralControl_PLC.TolLow);
                adsClient.WriteAny(GeneralControl_PLC.hCorrLow1, GeneralControl_PLC.CorrLow1);
                adsClient.WriteAny(GeneralControl_PLC.hCorrLow2, GeneralControl_PLC.CorrLow2);
                adsClient.WriteAny(GeneralControl_PLC.hGeneralErrorCount, GeneralControl_PLC.GeneralErrorCount);  //qq
                adsClient.WriteAny(GeneralControl_PLC.hLastBatch, GeneralControl_PLC.LastBatch);  //qq
                //--------Load conveyor:-----------
                adsClient.WriteAny(LoadConveyor_PLC.hOp_VB, LoadConveyor_PLC.Op_VB);
                adsClient.WriteAny(LoadConveyor_PLC.hfl_ConvReady, LoadConveyor_PLC.fl_ConvReady);
                adsClient.WriteAny(LoadConveyor_PLC.hfl_ConvMode, LoadConveyor_PLC.fl_ConvMode);
                adsClient.WriteAny(LoadConveyor_PLC.hInitWithStation, LoadConveyor_PLC.InitWithStation);
                adsClient.WriteAny(LoadConveyor_PLC.hInitConv, LoadConveyor_PLC.InitConv);
                adsClient.WriteAny(LoadConveyor_PLC.hConvResume, LoadConveyor_PLC.ConvResume);
                adsClient.WriteAny(LoadConveyor_PLC.hResErr, LoadConveyor_PLC.ResErr);
                adsClient.WriteAny(LoadConveyor_PLC.hResCycle, LoadConveyor_PLC.ResCycle);
                adsClient.WriteAny(LoadConveyor_PLC.hTrayManUp, LoadConveyor_PLC.TrayManUp);
                adsClient.WriteAny(LoadConveyor_PLC.hTrayManDown, LoadConveyor_PLC.TrayManDown);
                adsClient.WriteAny(LoadConveyor_PLC.hConvErrorCount, LoadConveyor_PLC.ConvErrorCount);
                //--------Unload conveyor:-----------
                adsClient.WriteAny(UnloadConveyor_PLC.hOp_VB, UnloadConveyor_PLC.Op_VB);
                adsClient.WriteAny(UnloadConveyor_PLC.hfl_ConvReady, UnloadConveyor_PLC.fl_ConvReady);
                adsClient.WriteAny(UnloadConveyor_PLC.hfl_ConvMode, UnloadConveyor_PLC.fl_ConvMode);
                adsClient.WriteAny(UnloadConveyor_PLC.hInitWithStation, UnloadConveyor_PLC.InitWithStation);
                adsClient.WriteAny(UnloadConveyor_PLC.hInitConv, UnloadConveyor_PLC.InitConv);
                adsClient.WriteAny(UnloadConveyor_PLC.hConvResume, UnloadConveyor_PLC.ConvResume);
                adsClient.WriteAny(UnloadConveyor_PLC.hResErr, UnloadConveyor_PLC.ResErr);
                adsClient.WriteAny(UnloadConveyor_PLC.hResCycle, UnloadConveyor_PLC.ResCycle);
                adsClient.WriteAny(UnloadConveyor_PLC.hTrayManUp, UnloadConveyor_PLC.TrayManUp);
                adsClient.WriteAny(UnloadConveyor_PLC.hTrayManDown, UnloadConveyor_PLC.TrayManDown);
                adsClient.WriteAny(UnloadConveyor_PLC.hConvErrorCount, UnloadConveyor_PLC.ConvErrorCount);
                //---------Index Table:------------
                adsClient.WriteAny(IndexTable_PLC.hOp_VB, IndexTable_PLC.Op_VB);
                adsClient.WriteAny(IndexTable_PLC.hfl_StepConvReady, IndexTable_PLC.fl_StepConvReady);
                adsClient.WriteAny(IndexTable_PLC.hfl_TableMode, IndexTable_PLC.fl_TableMode);
                adsClient.WriteAny(IndexTable_PLC.hfl_TableError, IndexTable_PLC.fl_TableError);
                adsClient.WriteAny(IndexTable_PLC.hreq_ResetTableError, IndexTable_PLC.req_ResetTableError);
                adsClient.WriteAny(IndexTable_PLC.hTableConvErrorCount, IndexTable_PLC.TableConvErrorCount);
                adsClient.WriteAny(IndexTable_PLC.hcarrToMeasure[1], IndexTable_PLC.carrToMeasure[1]);
                adsClient.WriteAny(IndexTable_PLC.hcarrToMeasure[2], IndexTable_PLC.carrToMeasure[2]);
                adsClient.WriteAny(IndexTable_PLC.hcarrToMeasure[3], IndexTable_PLC.carrToMeasure[3]);
                adsClient.WriteAny(IndexTable_PLC.hcarrToMeasure[4], IndexTable_PLC.carrToMeasure[4]);
                adsClient.WriteAny(IndexTable_PLC.hcarrToMeasure[5], IndexTable_PLC.carrToMeasure[5]);
                adsClient.WriteAny(IndexTable_PLC.hcarrToMeasure[6], IndexTable_PLC.carrToMeasure[6]);
			}
			catch(Exception ex)
			{
                AppGen.Inst.MDImain.frmTitle.plcInvalidFleg = true;
                if (MessageBox.Show(ex.Message, "PLC No Comunication", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    AppGen.Inst.MDImain.frmTitle.plcInvalidFleg = false;
                }
				//MessageBox.Show(ex.Message);
			}
		}    

        private void FillStructControls()
		{
            textBox7.Text = RobotDada.Q_ReqPauseCycle.ToString();
            textBox8.Text = RobotDada.Q_StopCycleResetAll.ToString();
            textBox9.Text = RobotDada.Q_ReqResumeCycle.ToString();
            textBox2.Text = RobotDada.Q_CarrReady.ToString();
            textBox3.Text = RobotDada.Q_TrayReady.ToString();
            textBox6.Text = RobotDada.Q_FoundOrientation.ToString();
            textBox1.Text = RobotDada.Q_DataSended.ToString();
            textBox4.Text = RobotDada.I_Rob_snap_req_1.ToString();
            textBox5.Text = RobotDada.I_Rob_snap_req_2.ToString();
            textBox15.Text = RobotDada.I_CycleDone.ToString();
            textBox17.Text = RobotDada.I_RobotSafePos.ToString();
            textBox23.Text = RobotDada.Vb_VisionEmpty.ToString();
            textBox21.Text = RobotDada.LoadingFinish.ToString();
            textBox22.Text = RobotDada.LastCarrier.ToString();
            textBox24.Text = RobotDada.Req_ReplaceTray.ToString();
            textBox27.Text = RobotDada.ProcesTime.ToString();
            textBox28.Text = RobotDada.ProcesPercent.ToString();              

            textBox35.Text = RobotDada2.Q_ReqPauseCycle.ToString();
            textBox36.Text = RobotDada2.Q_StopCycleResetAll.ToString();
            textBox37.Text = RobotDada2.Q_ReqResumeCycle.ToString();
            textBox30.Text = RobotDada2.Q_CarrReady.ToString();
            textBox31.Text = RobotDada2.Q_TrayReady.ToString();
            textBox34.Text = RobotDada2.Q_FoundInserts.ToString();
            textBox29.Text = RobotDada2.Q_DataSended.ToString();

            textBox32.Text = RobotDada2.I_Rob_snap_req_3.ToString();
            textBox43.Text = RobotDada2.I_CycleDone.ToString();
            textBox45.Text = RobotDada2.I_RobotSafePos.ToString();
            textBox51.Text = RobotDada2.Vb_VisionEmpty.ToString();
            textBox49.Text = RobotDada2.LoadingFinish.ToString();
            textBox50.Text = RobotDada2.LastCarrier.ToString();
            textBox52.Text = RobotDada2.Req_ReplaceTray.ToString();
            textBox55.Text = RobotDada2.ProcesTime.ToString();
            textBox56.Text = RobotDada2.ProcesPercent.ToString();             
		}
        private void UpdateStructFromControls()  
        {
            RobotDada.Q_DataSended = Boolean.Parse(textBox1.Text);
            RobotDada.Q_CarrReady = Boolean.Parse(textBox2.Text);
            RobotDada.Q_TrayReady = Boolean.Parse(textBox3.Text);
            RobotDada.I_Rob_snap_req_1 = Boolean.Parse(textBox4.Text);
            RobotDada.I_Rob_snap_req_2 = Boolean.Parse(textBox5.Text);
            RobotDada.Q_FoundOrientation = Boolean.Parse(textBox6.Text);
            RobotDada.Q_ReqPauseCycle = Boolean.Parse(textBox7.Text);
            RobotDada.Q_StopCycleResetAll = Boolean.Parse(textBox8.Text);
            RobotDada.Q_ReqResumeCycle = Boolean.Parse(textBox9.Text);             
            RobotDada.I_CycleDone = Boolean.Parse(textBox15.Text);
            RobotDada.I_RobotSafePos = Boolean.Parse(textBox17.Text);              
            RobotDada.LoadingFinish = Boolean.Parse(textBox21.Text);
            RobotDada.LastCarrier = Boolean.Parse(textBox22.Text);
            RobotDada.Vb_VisionEmpty = Boolean.Parse(textBox23.Text);
            RobotDada.Req_ReplaceTray = Boolean.Parse(textBox24.Text);               
            RobotDada.ProcesTime = short.Parse(textBox27.Text);
            RobotDada.ProcesPercent = short.Parse(textBox28.Text);
                
            RobotDada2.Q_DataSended = Boolean.Parse(textBox29.Text);
            RobotDada2.Q_CarrReady = Boolean.Parse(textBox30.Text);
            RobotDada2.Q_TrayReady = Boolean.Parse(textBox31.Text);
            RobotDada2.I_Rob_snap_req_3 = Boolean.Parse(textBox32.Text);
            RobotDada2.Q_FoundInserts = Boolean.Parse(textBox34.Text);
            RobotDada2.Q_ReqPauseCycle = Boolean.Parse(textBox35.Text);
            RobotDada2.Q_StopCycleResetAll = Boolean.Parse(textBox36.Text);
            RobotDada2.Q_ReqResumeCycle = Boolean.Parse(textBox37.Text);               
            RobotDada2.I_CycleDone = Boolean.Parse(textBox43.Text);               
            RobotDada2.I_RobotSafePos = Boolean.Parse(textBox45.Text);               
            RobotDada2.LoadingFinish = Boolean.Parse(textBox49.Text);
            RobotDada2.LastCarrier = Boolean.Parse(textBox50.Text);
            RobotDada2.Vb_VisionEmpty = Boolean.Parse(textBox51.Text);
            RobotDada2.Req_ReplaceTray = Boolean.Parse(textBox52.Text);               
            RobotDada2.ProcesTime = short.Parse(textBox55.Text);
            RobotDada2.ProcesPercent = short.Parse(textBox56.Text);
        }

       
        public object ReadData(int handle, Type type)
        {
            try
            {
                return (bool)adsClient.ReadAny(handle, type);
            }
            catch { }

            return false;
        }
        public void UpdatePlcData(int handle, object data)
        {
            if (!AppGen.Inst.MDImain.chkPlcON.Checked) return;      
            try
            {
                //write by handle
                adsClient.WriteAny(handle, data);
            }
            catch (Exception ex)
            {
                AppGen.Inst.MDImain.frmTitle.plcInvalidFleg = true;
                if (MessageBox.Show(ex.Message, "PLC No Comunication", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    AppGen.Inst.MDImain.frmTitle.plcInvalidFleg = false;
                }
                //MessageBox.Show(ex.Message);
            }
        }

        public  RobotStruct RobotDada;
        public RobotStruct RobotDada2;

        private GeneralControl_PLC generalControl_PLC = new GeneralControl_PLC();
        public GeneralControl_PLC GeneralControl_PLC
        {
            get
            {
                lock ((object)generalControl_PLC)
                {
                    return generalControl_PLC;
                }
            }
            set
            {
                lock ((object)generalControl_PLC)
                {
                    generalControl_PLC = value;
                }
            }
        }
        private IndexTable_PLC indexTable_PLC = new IndexTable_PLC();
        public IndexTable_PLC IndexTable_PLC
        {
            get
            {
                lock ((object)indexTable_PLC)
                {
                    return indexTable_PLC;
                }
            }
            set
            {
                lock ((object)indexTable_PLC)
                {
                    indexTable_PLC = value;
                }
            }
        }
        private Conveyor_PLC loadConveyor_PLC = new Conveyor_PLC();
        public Conveyor_PLC LoadConveyor_PLC
        {
            get
            {
                lock ((object)loadConveyor_PLC)
                {
                    return loadConveyor_PLC;
                }
            }
            set
            {
                lock ((object)loadConveyor_PLC)
                {
                    loadConveyor_PLC = value;
                }
            }
        }
        private Conveyor_PLC unloadConveyor_PLC = new Conveyor_PLC();
        public Conveyor_PLC UnloadConveyor_PLC
        {
            get
            {
                lock ((object)unloadConveyor_PLC)
                {
                    return unloadConveyor_PLC;
                }
            }
            set
            {
                lock ((object)unloadConveyor_PLC)
                {
                    unloadConveyor_PLC = value;
                }
            }
        }



        private PlcErrMsg general_PlcErrMsg = new PlcErrMsg();  //qq
        public PlcErrMsg General_PlcErrMsg
        {
            get
            {
                lock ((object)general_PlcErrMsg)
                {
                    return general_PlcErrMsg;
                }
            }
            set
            {
                lock ((object)general_PlcErrMsg)
                {
                    general_PlcErrMsg = value;
                }
            }
        }
        private PlcErrMsg loadConv_PlcErrMsg = new PlcErrMsg();
        public PlcErrMsg LoadConv_PlcErrMsg
        {
            get
            {
                lock ((object)loadConv_PlcErrMsg)
                {
                    return loadConv_PlcErrMsg;
                }
            }
            set
            {
                lock ((object)loadConv_PlcErrMsg)
                {
                    loadConv_PlcErrMsg = value;
                }
            }
        }
        private PlcErrMsg unloadConv_PlcErrMsg = new PlcErrMsg();
        public PlcErrMsg UnloadConv_PlcErrMsg
        {
            get
            {
                lock ((object)unloadConv_PlcErrMsg)
                {
                    return unloadConv_PlcErrMsg;
                }
            }
            set
            {
                lock ((object)unloadConv_PlcErrMsg)
                {
                    unloadConv_PlcErrMsg = value;
                }
            }
        }
        private PlcErrMsg indexTable_PlcErrMsg = new PlcErrMsg();
        public PlcErrMsg IndexTable_PlcErrMsg
        {
            get
            {
                lock ((object)indexTable_PlcErrMsg)
                {
                    return indexTable_PlcErrMsg;
                }
            }
            set
            {
                lock ((object)indexTable_PlcErrMsg)
                {
                    indexTable_PlcErrMsg = value;
                }
            }
        }

        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {
            //cant delete...  why?
        }
	}

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class RobotStruct
    {
        public RobotStruct(TcAdsClient _adsClient)
        {
            adsClient = _adsClient;
        }
        public void Write(int handle, object val)
        {
            try
            {
                adsClient.WriteAny(handle, val);
            }
            catch (Exception ex)
            {
            }
        }

        private TcAdsClient adsClient;

   //TO ROBOT
        public int H_q_ReqPauseCycle;
        private bool q_ReqPauseCycle;
        public bool Q_ReqPauseCycle
        {
            get
            {
                lock ((object)q_ReqPauseCycle)
                {
                    return q_ReqPauseCycle;
                }
            }
            set
            {
                lock ((object)q_ReqPauseCycle)
                {
                    if (q_ReqPauseCycle != value)
                    {
                        q_ReqPauseCycle = value;
                        Write(H_q_ReqPauseCycle, value);
                    }
                }
            }
        }
        
        public int H_q_StopCycleResetAll;
        private bool q_StopCycleResetAll;
        public bool Q_StopCycleResetAll
        {
            get
            {
                lock ((object)q_StopCycleResetAll)
                {
                    return q_StopCycleResetAll;
                }
            }
            set
            {
                lock ((object)q_StopCycleResetAll)
                {
                    if (q_StopCycleResetAll != value)
                    {
                        q_StopCycleResetAll = value;
                        Write(H_q_StopCycleResetAll, value);
                    }                  
                }
            }
        }

        public int H_q_ReqResumeCycle;
        private bool q_ReqResumeCycle;
        public bool Q_ReqResumeCycle
        {
            get
            {
                lock ((object)q_ReqResumeCycle)
                {
                    return q_ReqResumeCycle;
                }
            }
            set
            {
                lock ((object)q_ReqResumeCycle)
                {
                    if (q_ReqResumeCycle != value)
                    {
                        q_ReqResumeCycle = value;
                        Write(H_q_ReqResumeCycle, value);
                    }                    
                }
            }
        }

        public int H_q_CarrReady;
        private bool q_CarrReady;
        public bool Q_CarrReady
        {
            get
            {
                lock ((object)q_CarrReady)
                {
                    return q_CarrReady;
                }
            }
            set
            {
                lock ((object)q_CarrReady)
                {
                    if (q_CarrReady != value)
                    {
                        q_CarrReady = value;
                        Write(H_q_CarrReady, value);
                    }                   
                }
            }
        }

        public int H_q_TrayReady;
        private bool q_TrayReady;
        public bool Q_TrayReady
        {
            get
            {
                lock ((object)q_TrayReady)
                {
                    return q_TrayReady;
                }
            }
            set
            {
                lock ((object)q_TrayReady)
                {
                    if (q_TrayReady != value)
                    {
                        q_TrayReady = value;
                        Write(H_q_TrayReady, value);
                    }                    
                }
            }
        }

        public int H_q_FoundOrientation;
        private bool q_FoundOrientation;
        public bool Q_FoundOrientation
        {
            get
            {
                lock ((object)q_FoundOrientation)
                {
                    return q_FoundOrientation;
                }
            }
            set
            {
                lock ((object)q_FoundOrientation)
                {
                    if (q_FoundOrientation != value)
                    {
                        q_FoundOrientation = value;
                        Write(H_q_FoundOrientation, value);
                    }                   
                }
            }
        }

        public int H_q_FoundInserts;
        private bool q_FoundInserts;
        public bool Q_FoundInserts
        {
            get
            {
                lock ((object)q_FoundInserts)
                {
                    return q_FoundInserts;
                }
            }
            set
            {
                lock ((object)q_FoundInserts)
                {
                    if (q_FoundInserts != value)
                    {
                        q_FoundInserts = value;
                        Write(H_q_FoundInserts, value);
                    }                   
                }
            }
        }

        public int H_q_DataSended;
        private bool q_DataSended;
        public bool Q_DataSended
        {
            get
            {
                lock ((object)q_DataSended)
                {
                    return q_DataSended;
                }
            }
            set
            {
                lock ((object)q_DataSended)
                {
                    if (q_DataSended != value)
                    {
                        q_DataSended = value;
                        Write(H_q_DataSended, value);
                    }                  
                }
            }
        }

        public int H_q_CaliberReady;
        private bool q_CaliberReady;
        public bool Q_CaliberReady
        {
            get
            {
                lock ((object)q_CaliberReady)
                {
                    return q_CaliberReady;
                }
            }
            set
            {
                lock ((object)q_CaliberReady)
                {
                    if (q_CaliberReady != value)
                    {
                        q_CaliberReady = value;
                        Write(H_q_CaliberReady, value);
                    }
                }
            }
        }

        public int H_q_CarrDone;
        private bool q_CarrDone;
        public bool Q_CarrDone
        {
            get
            {
                lock ((object)q_CarrDone)
                {
                    return q_CarrDone;
                }
            }
            set
            {
                lock ((object)q_CarrDone)
                {
                    if (q_CarrDone != value)
                    {
                        q_CarrDone = value;
                        Write(H_q_CarrDone, value);
                    }                  
                }
            }
        }

  //FROM ROBOT
        public int H_i_Rob_snap_req_1;
        private bool i_Rob_snap_req_1;
        public bool I_Rob_snap_req_1
        {
            get
            {
                lock ((object)i_Rob_snap_req_1)
                {
                    return i_Rob_snap_req_1;
                }
            }
            set
            {
                lock ((object)i_Rob_snap_req_1)
                {
                    if (i_Rob_snap_req_1 != value)
                    {
                        i_Rob_snap_req_1 = value;
                        Write(H_i_Rob_snap_req_1, value);
                    }                    
                }
            }
        }

        public int H_i_Rob_snap_req_2;
        private bool i_Rob_snap_req_2;
        public bool I_Rob_snap_req_2
        {
            get
            {
                lock ((object)i_Rob_snap_req_2)
                {
                    return i_Rob_snap_req_2;
                }
            }
            set
            {
                lock ((object)i_Rob_snap_req_2)
                {
                    if (i_Rob_snap_req_2 != value)
                    {
                        i_Rob_snap_req_2 = value;
                        Write(H_i_Rob_snap_req_2, value);
                    }                    
                }
            }
        }

        public int H_i_Rob_snap_req_3;
        private bool i_Rob_snap_req_3;
        public bool I_Rob_snap_req_3
        {
            get
            {
                lock ((object)i_Rob_snap_req_3)
                {
                    return i_Rob_snap_req_3;
                }
            }
            set
            {
                lock ((object)i_Rob_snap_req_3)
                {
                    if (i_Rob_snap_req_3 != value)
                    {
                        i_Rob_snap_req_3 = value;
                        Write(H_i_Rob_snap_req_3, value);
                    }                   
                }
            }
        }

        public int H_i_CycleDone;
        private bool i_CycleDone;
        public bool I_CycleDone
        {
            get
            {
                lock ((object)i_CycleDone)
                {
                    return i_CycleDone;
                }
            }
            set
            {
                lock ((object)i_CycleDone)
                {
                    if (i_CycleDone != value)
                    {
                        i_CycleDone = value;
                        Write(H_i_CycleDone, value);
                    }                    
                }
            }
        }

        public int H_i_RobotSafePos;
        private bool i_RobotSafePos;
        public bool I_RobotSafePos
        {
            get
            {
                lock ((object)i_RobotSafePos)
                {
                    return i_RobotSafePos;
                }
            }
            set
            {
                lock ((object)i_RobotSafePos)
                {
                    if (i_RobotSafePos != value)
                    {
                        i_RobotSafePos = value;
                        Write(H_i_RobotSafePos, value);
                    }                    
                }
            }
        }

        public int H_i_InsertPlaced;
        private bool i_InsertPlaced;
        public bool I_InsertPlaced
        {
            get
            {
                lock ((object)i_InsertPlaced)
                {
                    return i_InsertPlaced;
                }
            }
            set
            {
                lock ((object)i_InsertPlaced)
                {
                    if (i_InsertPlaced != value)
                    {
                        i_InsertPlaced = value;
                        Write(H_i_InsertPlaced, value);
                    }                  
                }
            }
        }

        public int H_i_LifeBit;
        private bool i_LifeBit;
        public bool I_LifeBit
        {
            get
            {
                lock ((object)i_LifeBit)
                {
                    return i_LifeBit;
                }
            }
            set
            {
                lock ((object)i_LifeBit)
                {
                    if (i_LifeBit != value)
                    {
                        i_LifeBit = value;
                        Write(H_i_LifeBit, value);
                    }                  
                }
            }
        }

        public int H_i_MeasurePlaced;
        private bool i_MeasurePlaced;
        public bool I_MeasurePlaced
        {
            get
            {
                lock ((object)i_MeasurePlaced)
                {
                    return i_MeasurePlaced;
                }
            }
            set
            {
                lock ((object)i_MeasurePlaced)
                {
                    if (i_MeasurePlaced != value)
                    {
                        i_MeasurePlaced = value;
                        Write(H_i_MeasurePlaced, value);
                    }
                }
            }
        }


        public int H_i_DataReceived;
        private bool i_DataReceived;  //signal that robot recieved coords  
        public bool I_DataReceived
        {
            get
            {
                lock ((object)i_DataReceived)
                {
                    return i_DataReceived;
                }
            }
            set
            {
                lock ((object)i_DataReceived)
                {
                    if (i_DataReceived != value)
                    {
                        i_DataReceived = value;
                        Write(H_i_DataReceived, value);
                    }                   
                }
            }
        }

        public int H_vb_VisionEmpty;
        private bool vb_VisionEmpty;
        public bool Vb_VisionEmpty
        {
            get
            {
                lock ((object)vb_VisionEmpty)
                {
                    return vb_VisionEmpty;
                }
            }
            set
            {
                lock ((object)vb_VisionEmpty)
                {
                    if (vb_VisionEmpty != value)
                    {
                        vb_VisionEmpty = value;
                        Write(H_vb_VisionEmpty, value);
                    }                   
                }
            }
        }

        public int H_loadingFinish;
        private bool loadingFinish;
        public bool LoadingFinish
        {
            get
            {
                lock ((object)loadingFinish)
                {
                    return loadingFinish;
                }
            }
            set
            {
                lock ((object)loadingFinish)
                {
                    if (loadingFinish != value)
                    {
                        loadingFinish = value;
                        Write(H_loadingFinish, value);
                    }                  
                }
            }
        }

        public int H_lastCarrier;
        private bool lastCarrier;
        public bool LastCarrier
        {
            get
            {
                lock ((object)lastCarrier)
                {
                    return lastCarrier;
                }
            }
            set
            {
                lock ((object)lastCarrier)
                {
                    if (lastCarrier != value)
                    {
                        lastCarrier = value;
                        Write(H_lastCarrier, value);
                    }                 
                }
            }
        }

        public int H_req_ReplaceTray;
        private bool req_ReplaceTray;
        public bool Req_ReplaceTray
        {
            get
            {
                lock ((object)req_ReplaceTray)
                {
                    return req_ReplaceTray;
                }
            }
            set
            {
                lock ((object)req_ReplaceTray)
                {
                    if (req_ReplaceTray != value)
                    {
                        req_ReplaceTray = value;
                        Write(H_req_ReplaceTray, value);
                    }                   
                }
            }
        }

        //Vb_VisionDone  japan
        public int H_vb_VisionDone;
        private bool vb_VisionDone;
        public bool Vb_VisionDone
        {
            get
            {
                lock ((object)vb_VisionDone)
                {
                    return vb_VisionDone;
                }
            }
            set
            {
                lock ((object)vb_VisionDone)
                {
                    if (vb_VisionDone != value)
                    {
                        vb_VisionDone = value;
                        Write(H_vb_VisionDone, value);
                    }
                }
            }
        }

        public int H_req_Measure;
        private bool req_Measure;
        public bool Req_Measure
        {
            get
            {
                lock ((object)req_Measure)
                {
                    return req_Measure;
                }
            }
            set
            {
                lock ((object)req_Measure)
                {
                    if (req_Measure != value)
                    {
                        req_Measure = value;
                        Write(H_req_Measure, value);
                    }
                }
            }
        }


        public int H_measureDone;
        private bool measureDone;
        public bool MeasureDone
        {
            get
            {
                lock ((object)measureDone)
                {
                    return measureDone;
                }
            }
            set
            {
                lock ((object)measureDone)
                {
                    if (measureDone != value)
                    {
                        measureDone = value;
                        Write(H_measureDone, value);
                    }
                }
            }
        }

        public int H_procesTime;
        private short procesTime;
        public short ProcesTime
        {
            get
            {
                lock ((object)procesTime)
                {
                    return procesTime;
                }
            }
            set
            {
                lock ((object)procesTime)
                {
                    if (procesTime != value)
                    {
                        procesTime = value;
                        Write(H_procesTime, value);
                    }                   
                }
            }
        }

        public int H_procesPercent;
        private short procesPercent;
        public short ProcesPercent
        {
            get
            {
                lock ((object)procesPercent)
                {
                    return procesPercent;
                }
            }
            set
            {
                lock ((object)procesPercent)
                {
                    if (procesPercent != value)
                    {
                        procesPercent = value;
                        Write(H_procesPercent, value);
                    }                   
                }
            }
        }

    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PlcErrMsg
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg1 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg2 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg3 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg4 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg5 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg6 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg7 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg8 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg9 = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
        public string Msg10 = "";
    }

    public class GeneralControl_PLC
    {
        public bool TopLight;
        public bool BottomLight1;
        public bool GreenLight;
        public bool YellowLight;
        public bool RedLight;
        public bool Buzzer;
        public bool S3;
        public string ProductionMessage;
        public short ProdMode;                 // 0 - waiting to start, 1 - begining, 2 - production, 3 - finishing, 4 - dressing                                                                        
        public bool Req_EmergencyRelease;
        public bool Req_Lock;      
        public bool ES_State;
        public bool ResumeAll;
        public bool ResetErrors;
        public bool ZoneLocked;
        public string LastMeasure;
        public float CorrHigh2;
        public float CorrHigh1;
        public float TolHigh;
        public float Target;
        public float TolLow;
        public float CorrLow1;
        public float CorrLow2;
        public short GeneralErrorCount;
        public bool LastBatch;
        public bool StahliOutAutomationReady;   //Japan 30.6.15
        public bool StahliInReadyForUnload;     //Japan 30.6.15
        public short GeneralWarningCount;   //Japan 01.07.15

        public int hTopLight;
        public int hBottomLight1;
        public int hGreenLight;
        public int hYellowLight;
        public int hRedLight;
        public int hBuzzer;
        public int hS3;
        public int hProductionMessage;
        public int hProdMode;
        public int hReq_EmergencyRelease;
        public int hReq_Lock;       
        public int hES_State;
        public int hResumeAll;
        public int hResetErrors;
        public int hZoneLocked;
        public int hLastMeasure;
        public int hCorrHigh2;
        public int hCorrHigh1;
        public int hTolHigh;
        public int hTarget;
        public int hTolLow;
        public int hCorrLow1;
        public int hCorrLow2;
        public int hGeneralErrorCount;
        public int hLastBatch;

        public int hStahliOutAutomationReady;   //Japan 30.6.15
        public int hStahliInReadyForUnload; //Japan 30.6.15
        public int hGeneralWarningCount;    //Japan 01.07.15

    }

    
    public class IndexTable_PLC
    {
        public short Op_VB;
        public bool fl_StepConvReady;
        public short fl_TableMode;
        public bool fl_TableError;
        public bool req_ResetTableError;
        public short TableConvErrorCount;
        public short[] ia_SliceStatus = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public bool[] carrToMeasure = { false, false, false, false, false, false, false };
                                   //vadim:    ar_MeasTolerance.fTarget

        public int hOp_VB;
        public int hfl_StepConvReady;
        public int hfl_TableMode;
        public int hfl_TableError;
        public int hreq_ResetTableError;
        public int hTableConvErrorCount;
        public int[] hia_SliceStatus = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] hcarrToMeasure = { 0, 0, 0, 0, 0, 0,0 };
    
    }
    public class Conveyor_PLC
    {
        public short Op_VB;
        public bool fl_ConvReady;     
        public short fl_ConvMode;
        public bool InitWithStation;
        public bool InitConv;
        public bool ConvResume;
        public bool ResErr;
        public bool ResCycle;
        public bool TrayManUp;
        public bool TrayManDown;
        public short ConvErrorCount;

        public int hOp_VB;
        public int hfl_ConvReady;
        public int hfl_ConvMode;
        public int hInitWithStation;
        public int hInitConv;
        public int hConvResume;
        public int hResErr;
        public int hResCycle;
        public int hTrayManUp;
        public int hTrayManDown;
        public int hConvErrorCount;
    }


}
