using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Stahli2Robots
{
    public partial class FrmAssemblies : Form
    {
        public FrmAssemblies()
        {
            InitializeComponent();

            updateFrmAssembliesDelegate = new UpdateFrmAssembliesDelegate(UpdateFrmAssembliesDelegateFunc);

            InitStep0 = 0;
            InitStep1 = 0;
            InitStep2 = 0;
            TrayStep0 = 0;
            TrayStep1 = 0;
            TrayStep2 = 0;
            bResetDone0 = false;
            bResetDone1 = false;
            bBlinkFl0 = false;
            bBlinkFl1 = false;
            bBlinkFl2 = false;
        }

        public void RefreshForm()
        {
          
            ///------refresh buttons of Assemblies current statuse:------
            switch (AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode)
            {
                case 0:    //MANUAL
                    optManual0.BackColor = System.Drawing.Color.Chartreuse;
                    optSemiAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
                    optAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
                    //optManual0.Enabled = true;
                    //optSemiAuto0.Enabled = false;
                    break;
                case 1:    //SEMI-AUTO
                    optManual0.BackColor = System.Drawing.Color.WhiteSmoke;
                    optSemiAuto0.BackColor = System.Drawing.Color.Chartreuse;
                    optAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
                    //optManual0.Enabled = false;
                    //optSemiAuto0.Enabled = true;
                    break;
                case 2:    //AUTO
                    optManual0.BackColor = System.Drawing.Color.WhiteSmoke;
                    optSemiAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
                    optAuto0.BackColor = System.Drawing.Color.Chartreuse;
                    //optManual0.Enabled = false;
                    //optSemiAuto0.Enabled = false;
                    break;
            }
            switch (AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode)
            {
                case 0:    //MANUAL
                    optManual1.BackColor = System.Drawing.Color.Chartreuse;
                    optSemiAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
                    optAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
                    //optManual1.Enabled = true;
                    //optSemiAuto1.Enabled = false;
                    break;
                case 1:    //SEMI-AUTO
                    optManual1.BackColor = System.Drawing.Color.WhiteSmoke;
                    optSemiAuto1.BackColor = System.Drawing.Color.Chartreuse;
                    optAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
                    //optManual1.Enabled = false;
                    //optSemiAuto1.Enabled = true;
                    break;
                case 2:    //AUTO
                    optManual1.BackColor = System.Drawing.Color.WhiteSmoke;
                    optSemiAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
                    optAuto1.BackColor = System.Drawing.Color.Chartreuse;
                    //optManual1.Enabled = false;
                    //optSemiAuto1.Enabled = false;
                    break;
            }
            switch (AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode)
            {
                case 0:    //MANUAL
                    optManual2.BackColor = System.Drawing.Color.Chartreuse;
                    optSemiAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
                    optAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
                    //optManual2.Enabled = true;
                    //optSemiAuto2.Enabled = false;
                    break;
                case 1:    //SEMI-AUTO
                    optManual2.BackColor = System.Drawing.Color.WhiteSmoke;
                    optSemiAuto2.BackColor = System.Drawing.Color.Chartreuse;
                    optAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
                    //optManual2.Enabled = false;
                    //optSemiAuto2.Enabled = true;
                    break;
                case 2:    //AUTO
                    optManual2.BackColor = System.Drawing.Color.WhiteSmoke;
                    optSemiAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
                    optAuto2.BackColor = System.Drawing.Color.Chartreuse;
                    //optManual2.Enabled = false;
                    //optSemiAuto2.Enabled = false;
                    break;
            }
           //-------------------------------------------------------
            
            AppGen.Inst.MDImain.frmAssemblies.tmrMachineStat.Start();

          //  tmrErrCtrl0.Start();
          //  tmrErrCtrl1.Start();
          //  tmrErrCtrl2.Start();
        }

        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            tmrMachineStat.Stop();
            tmrCycleReset0.Stop();
            tmrCycleReset1.Stop();
            tmrGeneralInit0.Stop();
            tmrGeneralInit1.Stop();
            tmrNewTray0.Stop();
            tmrNewTray1.Stop();
            tmrNewTray2.Stop();
            tmrErrCtrl0.Stop();
            tmrErrCtrl1.Stop();
            tmrErrCtrl2.Stop();
            e.Cancel = true;
        }



        private void cmdManTray_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            sw_ManTrayTimeOut.Reset();
            sw_ManTrayTimeOut.Start();
            switch (btn.Name)
            {
                case "cmdManTray1Up":
                     StateLed0.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.TrayManUp = true;
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hTrayManUp, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.TrayManUp);
                    System.Threading.Thread.Sleep(500);
                    while (!AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.TrayManUp)
                        {
                            System.Threading.Thread.Sleep(100);
                            if (sw_ManTrayTimeOut.Elapsed.Seconds > ManTrayTimeOutVal)
                            {
                                sw_ManTrayTimeOut.Stop();
                                sw_ManTrayTimeOut.Reset();
                                StateLed0.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico"); //faild
                                break;
                            }                          
                        }
                    StateLed0.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");  //sucess
                    break;
                case "cmdManTray1Down":
                    StateLed0.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.TrayManDown = true;
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hTrayManDown, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.TrayManDown);
                    System.Threading.Thread.Sleep(500);
                    while (!AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.TrayManDown)
                        {
                            System.Threading.Thread.Sleep(100);
                            if (sw_ManTrayTimeOut.Elapsed.Seconds > ManTrayTimeOutVal)
                            {
                                sw_ManTrayTimeOut.Stop();
                                sw_ManTrayTimeOut.Reset();
                                StateLed0.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico"); //faild
                                break;
                            }                          
                        }
                    StateLed0.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");  //sucess
                    break;
                case "cmdManTray1Lock":  //not in use
                    StateLed1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    break;
                case "cmdManTray1Unlock":  //not in use
                    StateLed1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    break;
                case "cmdManTray2Up":
                    StateLed5.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.TrayManUp = true;
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hTrayManUp, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.TrayManUp);
                    System.Threading.Thread.Sleep(500);
                    while (!AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.TrayManUp)
                        {
                            System.Threading.Thread.Sleep(100);
                            if (sw_ManTrayTimeOut.Elapsed.Seconds > ManTrayTimeOutVal)
                            {
                                sw_ManTrayTimeOut.Stop();
                                sw_ManTrayTimeOut.Reset();
                                StateLed5.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico"); //faild
                                break;
                            }                          
                        }
                    StateLed5.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");  //sucess
                    break;
                case "cmdManTray2Down":
                     StateLed5.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.TrayManDown = true;
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hTrayManDown, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.TrayManDown);
                    System.Threading.Thread.Sleep(500);
                    while (!AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.TrayManDown)
                        {
                            System.Threading.Thread.Sleep(100);
                            if (sw_ManTrayTimeOut.Elapsed.Seconds > ManTrayTimeOutVal)
                            {
                                sw_ManTrayTimeOut.Stop();
                                sw_ManTrayTimeOut.Reset();
                                StateLed5.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico"); //faild
                                break;
                            }                          
                        }
                    StateLed5.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");  //sucess
                    break;
                case "cmdManTray2Lock":   //not in use
                    StateLed6.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    break;
                case "cmdManTray2Unlock":  //not in use
                    StateLed6.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    break;
            }
        }
        private void cmdSemiAutoOperation_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            switch (btn.Name)
            {
                case "cmdResetCycle0":
                    tmrCycleReset0.Start();
                    break;
                case "cmdInitConv0":
                    if (chkIncludStation0.Checked)  
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitWithStation = true;                        
                    }
                    else
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitWithStation = false;                       
                    }
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hInitWithStation, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitWithStation);
                    
                    DialogResult dialogResult = MessageBox.Show("Please check No trays on Conveyor?", "Alert", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        InitStep0 = 0;
                        StateLed3.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                        sw_TimeOut0.Start();  // Start The TimeOut StopWatch 
                        tmrGeneralInit0.Start();
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }        
                    break;
                case "cmdNewTray0":
                    TrayStep0 = 0;                   
                    sw_TrayTimeOut0.Start();  // Start The TimeOut StopWatch 
                    tmrNewTray0.Start();
                    break;               
                case "cmdResetCycle1":
                    tmrCycleReset1.Start();
                    break;
                case "cmdInitConv1":
                    if (chkIncludStation1.Checked)
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitWithStation = true;                        
                    }
                    else
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitWithStation = false;                       
                    }
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hInitWithStation, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitWithStation);

                    DialogResult dialogResult2 = MessageBox.Show("Please check No Trays on Conveyor?", "Alert", MessageBoxButtons.YesNo);
                    if (dialogResult2 == DialogResult.Yes)
                    {
                        InitStep1 = 0;
                        StateLed8.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                        sw_TimeOut1.Start();  // Start The TimeOut StopWatch 
                        tmrGeneralInit1.Start();
                    }
                    else if (dialogResult2 == DialogResult.No)
                    {
                        return;
                    }        
                    break;
                case "cmdNewTray1":
                    tmrNewTray1.Enabled = true;
                    break;               
                case "cmdResetCycle2":
                    tmrCycleReset2.Enabled = true;
                    break;
                case "cmdInitTable":

                    break;
                case "cmdTurnTable":
                    tmrNewTray2.Enabled = true;
                    break;              
            }
        }
        private void cmdResetError_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            switch (btn.Name)
            {
                case "ResetErr0":
                    if (!chkContLastMotion0.Checked)
	                {
                        AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResErr = true;
                        AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hResErr, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResErr);
	                }
                    else
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ConvResume = true;
                        AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hConvResume, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ConvResume);
                    } 
                    break;
                case "ResetErr1":
                    if (!chkContLastMotion1.Checked)
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResErr = true;
                        AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hResErr, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResErr);
                    }
                    else
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ConvResume = true;
                        AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hConvResume, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ConvResume);
                    } 
                    break;
                case "ResetErr2":
                    //tbd? not existing on 4 robots..
                    break;
            }
        }
        private void cmdIncludeStation_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox btn = sender as CheckBox;
            if (btn == null) return;
            switch (btn.Name)
            {
                case "chkIncludStation0":
                    if (chkIncludStation0.Checked)  
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitWithStation = true;                        
                    }
                    else
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitWithStation = false;                       
                    }
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hInitWithStation, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitWithStation);
                    break;
                case "chkIncludStation1":
                    if (chkIncludStation1.Checked)
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitWithStation = true;                        
                    }
                    else
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitWithStation = false;                       
                    }
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hInitWithStation, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitWithStation);
                    break;
            }
        }

        private void tmrCycleReset0_Tick(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitConv = false;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hInitConv, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitConv);
            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResCycle= true;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hResCycle, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResCycle);
            System.Threading.Thread.Sleep(500);
            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hResCycle, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResCycle);

            StateLed2.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
            cmdResetCycle0.Text = "Wait";
            bResetDone0 = AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvReady;

            if (bResetDone0)
            {
                cmdResetCycle0.Text = "Reset Cycle";
                StateLed2.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");
                tmrCycleReset0.Enabled = false;
            }
        }
        private void tmrCycleReset1_Tick(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitConv = false;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hInitConv, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitConv);
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResCycle = true;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hResCycle, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResCycle);
            System.Threading.Thread.Sleep(500);
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hResCycle, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResCycle);

            StateLed7.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
            cmdResetCycle1.Text = "Wait";
            bResetDone1 = AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvReady;

            if (bResetDone1)
            {
                cmdResetCycle1.Text = "Reset Cycle";
                StateLed7.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");
                tmrCycleReset1.Enabled = false;
            }
        }
        private void tmrGeneralInit0_Tick(object sender, EventArgs e)  //Load Coveyor Init
        {
            switch (InitStep0)
            {
                case 0:
                     AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitConv = false;
                     AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hInitConv, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitConv);
                     System.Threading.Thread.Sleep(500);
                     AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitConv = true;
                     AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hInitConv, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitConv);
                     InitStep0 = 10;
                    break;
                case 10:
                    InitStep0 = 20;
                    break;
                case 20:
                    if (!AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.InitConv)
                    {
                        InitStep0 = 0;
                        StateLed3.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");
                        sw_TimeOut0.Stop();
                        sw_TimeOut0.Reset();
                        tmrGeneralInit0.Stop();
                    }
                    else
                    {
                        if (sw_TimeOut0.Elapsed.Seconds > TimeOutVal0)  //Faild
                        {
                            InitStep0 = 0;
                            sw_TimeOut0.Stop();
                            sw_TimeOut0.Reset();
                            tmrGeneralInit0.Stop();                          
                        }
                    }
                    break;
            }
        }
        private void tmrGeneralInit1_Tick(object sender, EventArgs e)  //Unload Coveyor Init
        {
            switch (InitStep1)
            {
                case 0:
                    AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitConv = false;
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hInitConv, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitConv);
                    System.Threading.Thread.Sleep(500);
                    AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitConv = true;
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hInitConv, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitConv);
                    InitStep1 = 10;
                    break;
                case 10:
                    InitStep1 = 20;
                    break;
                case 20:
                    if (!AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.InitConv)
                    {
                        InitStep1 = 0;
                        StateLed8.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");
                        sw_TimeOut1.Stop();
                        sw_TimeOut1.Reset();
                        tmrGeneralInit1.Stop();
                    }
                    else
                    {
                        if (sw_TimeOut1.Elapsed.Seconds > TimeOutVal1)  //Faild
                        {
                            InitStep1 = 0;
                            sw_TimeOut1.Stop();
                            sw_TimeOut1.Reset();
                            tmrGeneralInit1.Stop();
                        }
                    }
                    break;
            }
        }
        private void tmrNewTray0_Tick(object sender, EventArgs e)
        {
            switch (TrayStep0)
            {               
                case 0:                   
                    StateLed4.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    cmdNewTray0.Text = "Wait";
                    AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Req_ReplaceTray = true;                    
                    AppGen.Inst.LoadTray.CurrIndex = 0;
                    TrayStep0 = 10; 
                    break;
                case 10:
                    TrayStep0 = 20; 
                    break;
                case 20:
                    if (!AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Req_ReplaceTray)
                    {
                        if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_TrayReady)
                        {
                            cmdNewTray0.Text = "Replace Tray";
                            StateLed4.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");
                            TrayStep0 = 0;
                            sw_TrayTimeOut0.Stop();
                            sw_TrayTimeOut0.Reset();
                            tmrNewTray0.Stop();
                        }
                    }
                    break;
            }
            if (sw_TrayTimeOut0.Elapsed.Seconds > TrayTimeOutVal0)  //Faild
            {
                TrayStep0 = 0;
                cmdNewTray0.Text = "Replace Tray";
                sw_TrayTimeOut0.Stop();
                sw_TrayTimeOut0.Reset();
                tmrNewTray0.Stop();
            }

        }
        private void tmrNewTray1_Tick(object sender, EventArgs e)
        {
            switch (TrayStep1)
            {
                case 0:
                    StateLed9.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\red-on_24.ico");
                    cmdNewTray1.Text = "Wait";
                    AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Req_ReplaceTray = true;
                   //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
                    //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_Req_ReplaceTray, true);
                    AppGen.Inst.UnLoadTray.CurrIndex = 0;
                    TrayStep1 = 10;
                    break;
                case 10:
                    TrayStep1 = 20;
                    break;
                case 20:
                    if (!AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Req_ReplaceTray)
                    {
                        if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_TrayReady)
                        {
                            cmdNewTray1.Text = "Replace Tray";
                            StateLed9.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\icon\\green-on_24.ico");
                            TrayStep1 = 0;
                            sw_TrayTimeOut1.Stop();
                            sw_TrayTimeOut1.Reset();
                            tmrNewTray1.Stop();
                        }
                    }
                    break;
            }
            if (sw_TrayTimeOut1.Elapsed.Seconds > TrayTimeOutVal1)  //Faild
            {
                TrayStep1 = 0;
                cmdNewTray1.Text = "Replace Tray";
                sw_TrayTimeOut1.Stop();
                sw_TrayTimeOut1.Reset();
                tmrNewTray1.Stop();
            }
        }
        private void tmrNewTray2_Tick(object sender, EventArgs e)
        {
            //tbd? : not in stahli 4 robots
        }
        private void tmrErrCtrl0_Tick(object sender, EventArgs e)
        {

        }
        private void tmrErrCtrl1_Tick(object sender, EventArgs e)
        {

        }
        private void tmrErrCtrl2_Tick(object sender, EventArgs e)
        {

        }    
        //private void tmrMachineStat_Tick(object sender, EventArgs e)
        //{
        //    if (bBlinkFl)
        //    {
        //        if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.MainMode == 2)
        //        {
        //            cmdSysMachinePlayStat.BackColor = System.Drawing.Color.Chartreuse;
        //            cmdSysMachinePauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
        //        }
        //        else
        //        {
        //            cmdSysMachinePlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
        //            cmdSysMachinePauseStat.BackColor = System.Drawing.Color.Chartreuse;
        //        }
        //        bBlinkFl = false;
        //    }
        //    else
        //    {
        //        cmdSysMachinePlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
        //        cmdSysMachinePauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
        //        bBlinkFl = true;
        //    }
        //}
        private void tmrMachineStat_Tick(object sender, EventArgs e)
        {
            if (bBlinkFl0)
            {
                if (AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB == 2)
                {
                    cmdLoadConvPlayStat.BackColor = System.Drawing.Color.Chartreuse;
                    cmdLoadConvPauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                else
                {
                    cmdLoadConvPlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
                    cmdLoadConvPauseStat.BackColor = System.Drawing.Color.Chartreuse;
                }
                bBlinkFl0 = false;
            }
            else
            {
                cmdLoadConvPlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
                cmdLoadConvPauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
                bBlinkFl0 = true;
            }

//------------------------------------
            if (bBlinkFl1)
            {
                if (AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB == 2)
                {
                    cmdUnloadConvPlayStat.BackColor = System.Drawing.Color.Chartreuse;
                    cmdUnloadConvPauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                else
                {
                    cmdUnloadConvPlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
                    cmdUnloadConvPauseStat.BackColor = System.Drawing.Color.Chartreuse;
                }
                bBlinkFl1 = false;
            }
            else
            {
                cmdUnloadConvPlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
                cmdUnloadConvPauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
                bBlinkFl1 = true;
            }
//------------------------------------
            if (bBlinkFl2)
            {
                if (AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB == 2)
                {
                    cmdTablePlayStat.BackColor = System.Drawing.Color.Chartreuse;
                    cmdTablePauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
                }
                else
                {
                    cmdTablePlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
                    cmdTablePauseStat.BackColor = System.Drawing.Color.Chartreuse;
                }
                bBlinkFl2 = false;
            }
            else
            {
                cmdTablePlayStat.BackColor = System.Drawing.Color.WhiteSmoke;
                cmdTablePauseStat.BackColor = System.Drawing.Color.WhiteSmoke;
                bBlinkFl2 = true;
            }

        }




    //members;
       private int InitStep0;
       private int InitStep1;
       private int InitStep2;
       private int TrayStep0;
       private int TrayStep1;
       private int TrayStep2;

       private bool bBlinkFl0;
       private bool bBlinkFl1;
       private bool bBlinkFl2;
       private bool bResetDone0;
       private bool bResetDone1;
       private double TimeOutVal0 = 250;
       private double TimeOutVal1 = 250;
       private double TimeOutVal2 = 250;
       private double TrayTimeOutVal0 = 60;
       private double TrayTimeOutVal1 = 60;
       private double TrayTimeOutVal2 = 60;
       private double ManTrayTimeOutVal = 5;
       System.Diagnostics.Stopwatch sw_TimeOut0 = new System.Diagnostics.Stopwatch();
       System.Diagnostics.Stopwatch sw_TimeOut1 = new System.Diagnostics.Stopwatch();
       System.Diagnostics.Stopwatch sw_TimeOut2 = new System.Diagnostics.Stopwatch();
       System.Diagnostics.Stopwatch sw_TrayTimeOut0 = new System.Diagnostics.Stopwatch();
       System.Diagnostics.Stopwatch sw_TrayTimeOut1 = new System.Diagnostics.Stopwatch();
       System.Diagnostics.Stopwatch sw_TrayTimeOut2 = new System.Diagnostics.Stopwatch();
       System.Diagnostics.Stopwatch sw_ManTrayTimeOut = new System.Diagnostics.Stopwatch();

       private void optManual0_Click(object sender, EventArgs e)
       {
           optManual0.BackColor = System.Drawing.Color.Chartreuse;
           optSemiAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
           optAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
           //optManual0.Enabled = true;
           //optSemiAuto0.Enabled = false;

           AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode = 0;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode);

       }

       private void optSemiAuto0_Click(object sender, EventArgs e)
       {
           optManual0.BackColor = System.Drawing.Color.WhiteSmoke;
           optSemiAuto0.BackColor = System.Drawing.Color.Chartreuse;
           optAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
           //optManual0.Enabled = false;
           //optSemiAuto0.Enabled = true;

           AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode = 1;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode);
       }

       private void optAuto0_Click(object sender, EventArgs e)
       {
           optManual0.BackColor = System.Drawing.Color.WhiteSmoke;
           optSemiAuto0.BackColor = System.Drawing.Color.WhiteSmoke;
           optAuto0.BackColor = System.Drawing.Color.Chartreuse;
           //optManual0.Enabled = false;
           //optSemiAuto0.Enabled = false;

           AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode = 2;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.fl_ConvMode);
       }

       private void optManual1_Click(object sender, EventArgs e)
       {
           optManual1.BackColor = System.Drawing.Color.Chartreuse;
           optSemiAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
           optAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
           //optManual1.Enabled = true;
           //optSemiAuto1.Enabled = false;

           AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode = 0;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode);
       }

       private void optSemiAuto1_Click(object sender, EventArgs e)
       {
           optManual1.BackColor = System.Drawing.Color.WhiteSmoke;
           optSemiAuto1.BackColor = System.Drawing.Color.Chartreuse;
           optAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
           //optManual1.Enabled = false;
           //optSemiAuto1.Enabled = true;

           AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode = 1;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode);
       }

       private void optAuto1_Click(object sender, EventArgs e)
       {
           optManual1.BackColor = System.Drawing.Color.WhiteSmoke;
           optSemiAuto1.BackColor = System.Drawing.Color.WhiteSmoke;
           optAuto1.BackColor = System.Drawing.Color.Chartreuse;
           //optManual1.Enabled = false;
           //optSemiAuto1.Enabled = false;

           AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode = 2;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hfl_ConvMode, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.fl_ConvMode);
       }

       private void optManual2_Click(object sender, EventArgs e)
       {
           optManual2.BackColor = System.Drawing.Color.Chartreuse;
           optSemiAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
           optAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
           //optManual2.Enabled = true;
           //optSemiAuto2.Enabled = false;

           AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode = 0;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hfl_TableMode, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode);
       }

       private void optSemiAuto2_Click(object sender, EventArgs e)
       {
           optManual2.BackColor = System.Drawing.Color.WhiteSmoke;
           optSemiAuto2.BackColor = System.Drawing.Color.Chartreuse;
           optAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
           //optManual2.Enabled = false;
           //optSemiAuto2.Enabled = true;

           AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode = 1;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hfl_TableMode, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode);
       }

       private void optAuto2_Click(object sender, EventArgs e)
       {
           optManual2.BackColor = System.Drawing.Color.WhiteSmoke;
           optSemiAuto2.BackColor = System.Drawing.Color.WhiteSmoke;
           optAuto2.BackColor = System.Drawing.Color.Chartreuse;
           //optManual2.Enabled = false;
           //optSemiAuto2.Enabled = false;

           AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode = 2;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hfl_TableMode, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.fl_TableMode);
       }

       private void cmdResetBuzzer_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmTitle.Buzzer(false);
       }

       private void cmdBuzzer_Click(object sender, EventArgs e)
       {
           if (Convert.ToInt16(txtBuzzer.Text) == 1)
           {
               AppGen.Inst.MDImain.frmTitle.Buzzer(true);
           }
           else
           {
               AppGen.Inst.MDImain.frmTitle.Buzzer(false);
           }
       }

       private void cmdTraficLight_Click(object sender, EventArgs e)
       {          
           switch (Convert.ToInt16(txtTrafic.Text))
           {
               case 0:
                   AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.None);
                   break;
               case 1:
                   AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.Green);
                   break;
               case 2:
                   AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.Yellow);
                   break;
               case 3:
                   AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.Red);
                   break;
               case 4:
                   AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.RedOff);
                   break;
           }
       }

       private void cmdToggleLight_Click(object sender, EventArgs e)
       {
           if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TopLight)
           {
               AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TopLight = false;
           }
           else
           {
               AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TopLight = true;
           }
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hTopLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TopLight);
       }

       private void cmdToggleLight1_Click(object sender, EventArgs e)
       {
           if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.BottomLight1)
           {
               AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.BottomLight1 = false;
           }
           else
           {
               AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.BottomLight1 = true;
           }
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hBottomLight1, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.BottomLight1);
       }


//delegate:
       private delegate void UpdateFrmAssembliesDelegate(FrmAssembliesData BoxType, string UpdatedValue);
       private UpdateFrmAssembliesDelegate updateFrmAssembliesDelegate;
       private void UpdateFrmAssembliesDelegateFunc(FrmAssembliesData BoxType, string UpdatedValue)
       {
           try
           {
               switch (BoxType)
               {
                   case FrmAssembliesData.LoadConvErrMsg:
                       lblErrMsg0.Text = UpdatedValue;
                       break;
                   case FrmAssembliesData.UnloadConvErrMsg:
                       lblErrMsg1.Text = UpdatedValue;
                       break;
                   case FrmAssembliesData.IndexTableErrMsg:
                       lblErrMsg2.Text = UpdatedValue;
                       break;                  
               }
           }
           catch { }
       }
       public void UpdateFrmAssemblies(FrmAssembliesData BoxType, string UpdatedValue)
       {
           try
           {
               this.Invoke(updateFrmAssembliesDelegate, BoxType, UpdatedValue);
           }
           catch { }
       }

       private void cmdSysMachinePlayStat_Click(object sender, EventArgs e)
       {
          // AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.MainMode = 2;
         //  AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hOp_Main, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.MainMode);
       }

       private void cmdSysMachinePauseStat_Click(object sender, EventArgs e)
       {
           //AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.MainMode = 1;
           //AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hOp_Main, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.MainMode);
       }

       private void cmdLoadConvPlayStat_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB = 2; 
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB);
       }

       private void cmdLoadConvPauseStat_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB = 1; 
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB);
       }

       private void cmdUnloadConvPlayStat_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB = 2;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB);
       }

       private void cmdUnloadConvPauseStat_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB = 1;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB);
       }

       private void cmdTablePlayStat_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB = 2;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB);
       }

       private void cmdTablePauseStat_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB = 1;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB);
       }

       private void cmdResumeAll_Click(object sender, EventArgs e)
       {
           AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResumeAll = true;
           AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hResumeAll, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResumeAll);
       }

    }
}
