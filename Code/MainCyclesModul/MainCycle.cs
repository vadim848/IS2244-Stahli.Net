using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace Stahli2Robots
{
    //'----------------------------------------------------------------------------------
    //'NAME:      MainCycle
    //'
    //'ABSTRACT:  a class holding all major cycle procedure
    //'
    //'DATE CREATED:  27/03/2014
    //'
    //'DATE UPDATED:
    //'
    //'* Copyright (c) 2014 by SHAFIR PRODUCTION SYSTEMS
    //'----------------------------------------------------------------------------------
    public class MainCycle
    {
        public MainCycle()  //constructor
        {
            worker = new Thread(new ThreadStart(WorkFunc));
            sw_TimeOut = new System.Diagnostics.Stopwatch();
            worker.IsBackground = true;
            MainControl = new BasicCycle();
            MainControl.lTimeOutLimit = 10;
            IndexTableControl = new BasicCycle();
            LoadCycleControl = new BasicCycle();
            UnloadCycleControl = new BasicCycle();
            MainProccesState = new ProcessStatus();
            UnloadCarrierSliceNo = 1;
            UnloadedInsCounter = 0;
            LoadedInsCounter = 0;
            CounterPerOrder = 0;
            PlaceZero = 1;
            PickSensor = 0;
            SetupDelays = 0;
            CarrierUnloadMissedCount = 0;
            TotalUnloadMissedCount = 0;
            MissPocketCarrCount = 0;
            MissPocketTotalCount = 0;
        }
        //methods:
        public void OnInitApp()
        {
            worker.Start();
        }
        public void OnClosingApp()
        {
            worker.Abort();
        }
        private void WorkFunc()    //<<<<<<<<<>>>>>>>>>>>>>>>>>main Auto-Cycle<<<<<<<<<<<<>>>>>>>>>>>>>
        {
            try
            {
                while (worker.IsAlive)
                {
                    while ((!MainControl.bPause) && (!MainControl.bError) && (MainControl.bRun))
                    {
                        if ((!LoadCycleControl.bPause) && (!LoadCycleControl.bError) && (LoadCycleControl.bRun) && (!LoadCycleControl.bDone) && (LoadCycleControl.bRobotActive))
                        {
                            LoadCycle();
                        }
                        if ((!UnloadCycleControl.bPause) && (!UnloadCycleControl.bError) && (UnloadCycleControl.bRun) && (!UnloadCycleControl.bDone) && (UnloadCycleControl.bRobotActive))
                        {
                            UnloadCycle();
                        }
                        Thread.Sleep(10);
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                AppGen.Inst.LogFile(ex.Message, LogType.SystemErr);
                System.Windows.Forms.Application.Exit();
            }
            AppGen.Inst.LogFile("Worker.IsAlive STOPED!!", LogType.GeneralErr);  //added on 25/11/14
            System.Windows.Forms.Application.Exit();                             //added on 25/11/14
        }
        public void LoadCycle()
        {
            try
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.StepLoad, LoadCycleControl.iStep.ToString());
                if ((LoadCycleControl.bPause) || (LoadCycleControl.bError) || (AppGen.Inst.RobotConnection.stLoadRobotControl.bDoorOpened))
                {
                    return;
                }
                if (LoadCycleControl.bRun)
                {
                    switch (LoadCycleControl.iStep)
                    {
                        case 0: //finding orientation and location of empty new arrived Carrier

                            //if (RescanCarrier == true)   ///japan rescan!!!!!!
                            //{
                            //    if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_CycleDone == true)
                            //    {
                            //        RescanCarrier = false;
                            //        AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.VisionAction(VisionActionType.AutoMissedScan); //scan all carrier indexes and find empty pockets
                            //        if (AppGen.Inst.MDImain.frmTitle.chkFillingMissingInserts.Checked)
                            //        {
                            //            //tbd(?)  filling missing pockets..
                            //        }
                            //        if ((AppGen.Inst.MDImain.frmTitle.chkTotalEmptyPockets.Checked) && (AppGen.Inst.MainCycle.MissPocketTotalCount >= Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtMaxTotalEmptyPockets.Text)))
                            //        {
                            //            //Stop load cycle  (Total too many Empty pockets)
                            //            AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 318;
                            //            AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                            //            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);
                            //        }
                            //        if ((AppGen.Inst.MDImain.frmTitle.chkCarrierEmptyPockets.Checked) && (AppGen.Inst.MainCycle.MissPocketCarrCount >= Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtMaxCarrEmptyPockets.Text)))
                            //        {
                            //            //Stop load cycle  (One carrier too many Empty pockets)
                            //            AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 319;
                            //            AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                            //            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);
                            //        }
                            //        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.MissedLoadCount, AppGen.Inst.MainCycle.MissPocketTotalCount.ToString());
                            //        if (!LoadCycleControl.bError)
                            //        {
                            //            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Vb_VisionDone = true; // signal to LPC VisionCarrDone=true;
                            //        }
                            //        AppGen.Inst.VisionParam.SNAP_req_fl2 = true;
                            //    }
                            //    AppGen.Inst.LoadCarrier.CurrIndex = 0;
                            //    while (!AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_CarrReady)
                            //    {
                            //        Thread.Sleep(200);   
                            //    }                                 
                            //    return;
                            //}

                            if ((AppGen.Inst.VisionParam.SNAP_req_fl2) && (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_CarrReady))
                            {
                                int fleg_no = 0;
                                do
                                {
                                    //find orientation and fullfill the new carr array after shift && rotate
                                    if (fleg_no > 1)
                                    {
                                        Thread.Sleep(1500);
                                    }
                                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.VisionAction(VisionActionType.ImageAcquisition);  //snap
                                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.VisionAction(VisionActionType.RunTool);           //run vision tools for angle and offset
                                    fleg_no += 1;
                                    if ((fleg_no > 7) || (MainProccesState != ProcessStatus.Running)) break;
                                }
                                while ((AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midleID == null) || (AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.rotation == null));  //Carrier NOT found);

                                if ((AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midleID == null) || (AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.rotation == null))  //Carrier NOT found
                                {
                                    LoadCycleControl.bError = true;
                                    LoadCycleControl.iErrNo = 104;
                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[LoadCycleControl.iErrNo]);                           
                                }
                                else                                                              //Carr WAS founded
                                {
                                    AppGen.Inst.LoadCarrier.SetRotate(AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.rotation);          //result of vision rotation, assing to Loadcarrier
                                    AppGen.Inst.LoadCarrier.SetOffset(AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointX, AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointY);
                                    if (AppGen.Inst.MDImain.frmTitle.chkPreScanLoadCarrier.Checked)
                                    {
                                        AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.VisionAction(VisionActionType.AutoPreScan); //find first empty index at carrier                              
                                        if (AppGen.Inst.LoadCarrier.CurrIndex >= AppGen.Inst.LoadCarrier.IndexList.Count)
                                        {
                                            AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 320;  //error - full tray
                                            AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);
                                            AppGen.Inst.LoadCarrier.CurrIndex = 0;
                                        }
                                    }
                                    AppGen.Inst.VisionParam.SNAP_req_fl2 = false;
                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam2ClearToSnap, "0");
                                    AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_FoundOrientation = true;
                                    CarrCycleTime.Restart();          //Start The StopWatch ...From 000
                                    LoadCycleControl.iStep = 10;
                                }
                            }
                            break;
                        case 5:  //waiting for replace tray done
                            Thread.Sleep(100);
                            if ((!AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Req_ReplaceTray) && (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_TrayReady))
                            {
                                LoadCycleControl.iStep = 10;
                            }
                            break;
                        case 10:
                            if (AppGen.Inst.VisionParam.SNAP_req_fl1)
                            {
                                if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_DataSended == false) // should be false 
                                {

                                    if (AppGen.Inst.LoadTray.CurrIndex < AppGen.Inst.LoadTray.IndexList.Count)                                //If NOT End of tray
                                    {
                                        AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.flagFullTray = false;                                   //search only in one index
                                        AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.VisionAction(VisionActionType.ImageAcquisition);
                                        AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.VisionAction(VisionActionType.RunTool);                 //run search tool on current index     

                                        if (AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.cogPMAlignTool.Results != null)
                                        {
                                            if (AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.cogPMAlignTool.Results.Count > 0)                   //found insert
                                            {
                                                LoadedInsCounter += 1;
                                                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadedInsCounter, LoadedInsCounter.ToString());
                                                CounterPerOrder += 1;
                                                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.CounterPerOrder, CounterPerOrder.ToString());
                                                AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.BuildInsertCoordinates(CAMERA_NUMBER.cLoadTrayCAM), true);                                                                                          
                                            }
                                        }
                                        AppGen.Inst.LoadTray.CurrIndex = AppGen.Inst.LoadTray.CurrIndex + 1;
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadTray, AppGen.Inst.LoadTray.CurrIndex.ToString());
                                    }
                                    else                                                                                                  //End of tray
                                    {
                                        if (AppGen.Inst.MDImain.frmTitle.chkRescanLoadTray.Checked)   //Rescan tray (only at unfounded indexes arr)
                                        {
                                            AppGen.Inst.LoadTray.CurrIndex = 0;
                                            AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.RunAgainFlag = true;
                                            LoadCycleControl.iStep = 20;                               // Rescan
                                        }
                                        else                                                           // NO-Rescan 
                                        {
                                            LastLoadInsCoordSent = true; // last on tray
                                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Req_ReplaceTray = true;                                            
                                            LoadCycleControl.iStep = 5; //waiting for replace tray done
                                            AppGen.Inst.LoadTray.CurrIndex = 0;
                                            AppGen.Inst.LoadTray.ResetUnfoundeInsertList();
                                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadTray, AppGen.Inst.LoadTray.CurrIndex.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_DataReceived)
                                    {
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Robot1CycleTime, (((double)Robot1CycleTime.Elapsed.Milliseconds) / 1000).ToString());
                                        Robot1CycleTime.Restart();          //Start The StopWatch ...From 000                                
                                        AppGen.Inst.VisionParam.SNAP_req_fl1 = false;
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam1ClearToSnap, "0");
                                        AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_FoundOrientation = false;
                                        AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_DataSended = false;                                       
                                    }
                                }
                            }
                            break;
                        case 20:     //Rescan Tray
                            while (AppGen.Inst.LoadTray.CurrIndex < AppGen.Inst.LoadTray.IndexList.Count)
                            {
                                if (AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].IsFound == false) break;
                                AppGen.Inst.LoadTray.CurrIndex += 1;
                            }
                            if (AppGen.Inst.LoadTray.CurrIndex < AppGen.Inst.LoadTray.IndexList.Count)
                            {
                                if (AppGen.Inst.VisionParam.SNAP_req_fl1)
                                {
                                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.flagFullTray = false;                                   //search only in one index
                                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.VisionAction(VisionActionType.ImageAcquisition);
                                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.VisionAction(VisionActionType.RunTool);                 //run search tool on current index                               
                                    if (AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.cogPMAlignTool.Results.Count > 0)                   //found insert
                                    {
                                        LoadedInsCounter += 1;
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadedInsCounter, LoadedInsCounter.ToString());
                                        CounterPerOrder += 1;
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.CounterPerOrder, CounterPerOrder.ToString());
                                        if (AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.BuildInsertCoordinates(CAMERA_NUMBER.cLoadTrayCAM), true))
                                        {
                                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Robot1CycleTime, Robot1CycleTime.Elapsed.Seconds.ToString());
                                            Robot1CycleTime.Restart();          //Start The StopWatch ...From 000                                
                                            AppGen.Inst.VisionParam.SNAP_req_fl1 = false;
                                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Cam1ClearToSnap, "0");
                                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_FoundOrientation = false;
                                        }
                                    }
                                    AppGen.Inst.LoadTray.CurrIndex = AppGen.Inst.LoadTray.CurrIndex + 1;
                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadTray, AppGen.Inst.LoadTray.CurrIndex.ToString());
                                }
                            }
                            else      // End of tray after Rescan (Unfounded pockets)
                            {
                                LastLoadInsCoordSent = true; // last on tray                           
                                Thread.Sleep(2000);  //delete when fix the bug (to add above lines)
                                AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Req_ReplaceTray = true;                        
                                LoadCycleControl.iStep = 5; //waiting for replace tray done 
                                AppGen.Inst.LoadTray.CurrIndex = 0;
                                AppGen.Inst.LoadTray.ResetUnfoundeInsertList();
                                AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.RunAgainFlag = false;
                                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadTray, AppGen.Inst.LoadTray.CurrIndex.ToString());
                            }
                            break;
                        case 30:
                            if (RescanCarrier == true)   ///japan rescan!!!!!!
                            {
                                if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada.I_CycleDone == true)
                                {
                                    RescanCarrier = false;
                                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.VisionAction(VisionActionType.AutoMissedScan); //scan all carrier indexes and find empty pockets
                                    if (AppGen.Inst.MDImain.frmTitle.chkFillingMissingInserts.Checked)
                                    {
                                        //tbd(?)  filling missing pockets..
                                    }
                                    if ((AppGen.Inst.MDImain.frmTitle.chkTotalEmptyPockets.Checked) && (AppGen.Inst.MainCycle.MissPocketTotalCount >= Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtMaxTotalEmptyPockets.Text)))
                                    {
                                        //Stop load cycle  (Total too many Empty pockets) 
                                        AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 319;
                                        AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);

                                        AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB = 1;  //table to pause
                                        AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB);
                                    }
                                    if ((AppGen.Inst.MDImain.frmTitle.chkCarrierEmptyPockets.Checked) && (AppGen.Inst.MainCycle.MissPocketCarrCount >= Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtMaxCarrEmptyPockets.Text)))
                                    {
                                        //Stop load cycle  (One carrier too many Empty pockets) 
                                        AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 318;
                                        AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);

                                        AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB = 1;  //table to pause
                                        AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB);
                                    }
                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.MissedLoadCount, AppGen.Inst.MainCycle.MissPocketTotalCount.ToString());
                                    AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Vb_VisionDone = true; // no matter result-->signal to LPC VisionCarrDone=true;
                                    //if (!LoadCycleControl.bError)  //remarked --> even ifstop for error,mark to PLC finish loading (operatormanually fullfill emptypocket (no automatic fullfill implement yet))
                                    //{
                                    //    AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Vb_VisionDone = true; // signal to LPC VisionCarrDone=true;
                                    //}
                                    AppGen.Inst.VisionParam.SNAP_req_fl2 = true;
                                }
                                AppGen.Inst.LoadCarrier.CurrIndex = 0;                               
                            }
                            if (!AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_CarrReady)  // PLC turned off CarrReady and will move carrier away
                            {
                                LoadCycleControl.iStep = 0;
                            }    
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        public void UnloadCycle()
        {
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.StepUnload, UnloadCycleControl.iStep.ToString());
            if ((UnloadCycleControl.bPause) || (UnloadCycleControl.bError) || (AppGen.Inst.RobotConnection.stUnloadRobotControl.bDoorOpened))
            {
                return;
            }
            if (UnloadCycleControl.bRun)
            {
                switch (UnloadCycleControl.iStep)
                {
                    case 0: //find all Inserts and submit to world Coordination Array (this project 4 slices total (not one big erea))               
                        if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_CycleDone)  //when cycle done(finish unloading carrier) need to wait untill robot and PLC will recieve the Carr-done signal
                        {
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrDone = false;                          
                            AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = false;  //search only in one slice (for next new unload carrier)
                            AppGen.Inst.VisionParam.SNAP_req_fl3 = true;
                            CarrierUnloadVisionCount = 0;
                            Thread.Sleep(200); 
                            return;
                        }
                        if (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrDone)  //fix bug: added in japan 30.01.15 (I_CycleDone, not update fast enough from robot)
                        {
                            Thread.Sleep(300);
                            return;
                        }
                        if (AppGen.Inst.RobotConnection.RU.ConnBusy == false)  //robot recieved  coordinate & and ready for next coord
                        {
                            if ((AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_Rob_snap_req_3) && (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrReady))
                            {
                                AppGen.Inst.UnLoadCarrier.CurrIndex = 0;              //init to first index of current slice result
                                if (RescanCycle == false)
                                {
                                    AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = false;  //search only in one slice
                                }
                                else
                                {
                                    AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = true;  //search All slices for Rescan with one snap
                                }
                                AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.VisionAction(VisionActionType.ImageAcquisition);
                                AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.VisionAction(VisionActionType.RunTool);
                                if (AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray == false)
                                {
                                    CarrierUnloadVisionCount = CarrierUnloadVisionCount + AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.cogPMAlignTool.Results.Count;
                                    if ((UnloadCarrierSliceNo == 4) && (CarrierUnloadVisionCount + Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtVisionMised.Text) < AppGen.Inst.LoadCarrier.IndexList.Count) && (AppGen.Inst.MDImain.frmTitle.chkVisionMised.Checked))
                                    {
                                        if (ResumeOnce)
                                        {
                                            ResumeOnce = false;
                                        }
                                        else
                                        {
                                            AppGen.Inst.MainCycle.UnloadCycleControl.bError = true;
                                            AppGen.Inst.MainCycle.UnloadCycleControl.iErrNo = 317;
                                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblUnloadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[UnloadCycleControl.iErrNo]);
                                            ResumeOnce = false;
                                            return;
                                        }                                       
                                    }
                                }
                                if (AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.cogPMAlignTool.Results.Count > 0)  //Found inserts
                                {
                                    AppGen.Inst.VisionParam.SNAP_req_fl3 = false;  //not in use?
                                    UnloadCycleControl.iStep = 10;
                                }
                                else //result empty!!!:
                                {
                                    if (UnloadCarrierSliceNo < 4)                
                                    {
                                        UnloadCarrierSliceNo += 1;   //slice index for next snap  
                                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, UnloadCarrierSliceNo.ToString());
                                        return;
                                    }
                                    else
                                    {
                                        if ((RescanCycle) || (!AppGen.Inst.MDImain.frmTitle.chkSecondUnloadRun.Checked))
                                        {//Carrier unloading DONE  //when rescan result is zero: tell robot, carr done --> robot tell PLC cycle done for replace                                       
                                            //----------max Mised Insert: --(robot missed)-----------------------(only if user checked this option. If not so skip this for avoid extra snap(save time))--------
                                            if ((AppGen.Inst.MDImain.frmTitle.chkCarrierMissed.Checked) || (AppGen.Inst.MDImain.frmTitle.chkTotalMissed.Checked))
                                            {
                                                AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = true;  //search All slices for Rescan with one snap
                                                AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.VisionAction(VisionActionType.ImageAcquisition);
                                                AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.VisionAction(VisionActionType.RunTool);
                                                CarrierUnloadMissedCount = AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.cogPMAlignTool.Results.Count;
                                                TotalUnloadMissedCount = TotalUnloadMissedCount + CarrierUnloadMissedCount;
                                                if ((CarrierUnloadMissedCount > Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtStopAfterCycleMissed.Text)) && (AppGen.Inst.MDImain.frmTitle.chkCarrierMissed.Checked))
                                                {
                                                    CarrierUnloadMissedCount = 0;
                                                    AppGen.Inst.MainCycle.UnloadCycleControl.bError = true;
                                                    AppGen.Inst.MainCycle.UnloadCycleControl.iErrNo = 315;
                                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblUnloadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[UnloadCycleControl.iErrNo]);
                                                    TotalUnloadMissedCount = 0;
                                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.MissedUnloadCount, "0");
                                                }
                                                if ((TotalUnloadMissedCount >= Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtStopAfterTotalMissed.Text)) && (AppGen.Inst.MDImain.frmTitle.chkTotalMissed.Checked))
                                                {
                                                    AppGen.Inst.MainCycle.UnloadCycleControl.bError = true;
                                                    AppGen.Inst.MainCycle.UnloadCycleControl.iErrNo = 316;
                                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblUnloadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[UnloadCycleControl.iErrNo]);
                                                    TotalUnloadMissedCount = 0;
                                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.MissedUnloadCount, "0");
                                                }
                                            }
                                            UnloadCarrierSliceNo = 1;
                                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, UnloadCarrierSliceNo.ToString());
                                            RescanCycle = false;
                                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.ProcesPercent = 0;
                                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrDone = true;                                          
                                            Thread.Sleep(500); //2500? 
                                            return;
                                        }
                                        else
                                        {
                                            RescanCycle = true; //for next cycle to snap all slices with one snap
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 5:    //waiting for replace tray done 
                        Thread.Sleep(100);
                        AppGen.Inst.UnLoadTray.CurrIndex = 0;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexUnloadTray, AppGen.Inst.UnLoadTray.CurrIndex.ToString());
                        if ((!AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Req_ReplaceTray) && (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_TrayReady))    //PLC turn off request only when done replacing tray
                        {
                            UnloadCycleControl.iStep = 10;
                        }
                        break;
                    case 10:    // N'cycle (send to robot the coordinates one by one)                      
                        if (AppGen.Inst.RobotConnection.RU.ConnBusy == false)  //robot recieved  coordinate & and ready for next coord
                        {
                            if (AppGen.Inst.UnLoadTray.CurrIndex >= AppGen.Inst.UnLoadTray.IndexList.Count)   //END OF UNLOADING TRAY (replace tray request)
                            {
                                while (!AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_InsertPlaced)
                                {
                                    Thread.Sleep(500);
                                }
                                Thread.Sleep(1500);
                                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Req_ReplaceTray = true;                               
                                UnloadCycleControl.iStep = 5; //waiting for replace tray done
                                break;
                            }
                            AppGen.Inst.VisionParam.SNAP_req_fl3 = false;
                            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, AppGen.Inst.RobotConnection.BuildInsertCoordinates(CAMERA_NUMBER.cUnloadCarrierCAM), true);
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.Robot2CycleTime, (((double)Robot2CycleTime.ElapsedMilliseconds) / 1000).ToString());
                            Robot2CycleTime.Restart();          //Start The StopWatch ...From 000                           
                            UnloadedInsCounter += 1;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.UnloadedInsCounter, UnloadedInsCounter.ToString());
                            if (AppGen.Inst.UnLoadTray.CurrIndex <= AppGen.Inst.UnLoadTray.IndexList.Count - 1)
                            {
                                AppGen.Inst.UnLoadTray.CurrIndex += 1;
                                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexUnloadTray, AppGen.Inst.UnLoadTray.CurrIndex.ToString());
                            }
                            if (AppGen.Inst.UnLoadCarrier.CurrIndex < AppGen.Inst.UnLoadCarrier.IndexList.Count - 1)  //(index of insert at Current slice)
                            {
                                AppGen.Inst.UnLoadCarrier.CurrIndex += 1;
                            }
                            else  //end of current slice or end of rescan result:   
                            {
                                UnloadCycleControl.iStep = 0;
                                if (((RescanCycle) || (!AppGen.Inst.MDImain.frmTitle.chkSecondUnloadRun.Checked)) && (UnloadCarrierSliceNo == 4))  //last insert of Rescan result (when rescan did found inserts) Or last insert in last Slice (when NO Rescan requested)
                                {
                                    while (AppGen.Inst.RobotConnection.RU.ConnBusy == true)  //signal q_CarrDone only after robot place last insert and answerd cmd15 to DataRecieve
                                    {
                                        Thread.Sleep(300);
                                    }
                                    UnloadCarrierSliceNo = 1;
                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, UnloadCarrierSliceNo.ToString());
                                    RescanCycle = false;
                                    AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.ProcesPercent = 0;
                                    AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrDone = true;                                    
                                    Thread.Sleep(500); //2500?
                                    return;
                                }
                                if ((!RescanCycle) && (AppGen.Inst.MDImain.frmTitle.chkSecondUnloadRun.Checked) && (UnloadCarrierSliceNo == 4))
                                {
                                    RescanCycle = true;    //last insert of last Slice, for snap all table(Rescan) in iStep = 0
                                }
                                if (UnloadCarrierSliceNo < 4)
                                {
                                    UnloadCarrierSliceNo += 1;   //slice index for next snap
                                    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, UnloadCarrierSliceNo.ToString());
                                }
                                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.ProcesPercent = Convert.ToInt16((AppGen.Inst.UnLoadCarrier.CurrIndex + ((UnloadCarrierSliceNo - 1 / 4) * AppGen.Inst.LoadCarrier.IndexList.Count)) / AppGen.Inst.LoadCarrier.IndexList.Count * 100);                               
                            }
                        }
                        break;
                }  //end switch
            }
        }
        //<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void ResumGeneral()
        {
            AppGen.Inst.LogFile("Operation pressed General Resume", LogType.Production);
            AppGen.Inst.MDImain.frmTitle.tmrBlinkResume0.Stop();
            AppGen.Inst.MDImain.frmTitle.tmrBlinkResume1.Stop();
            AppGen.Inst.MDImain.frmTitle.tmrBlinkResume2.Stop();
            AppGen.Inst.MDImain.frmTitle.tmrBlinkResume3.Stop();
            AppGen.Inst.MDImain.frmTitle.tmrBlinkResume4.Stop();
            AppGen.Inst.MDImain.frmTitle.tmrBlinkResume5.Stop();
            AppGen.Inst.MDImain.frmTitle.cmdResumeGeneral.BackColor = System.Drawing.Color.WhiteSmoke;
            AppGen.Inst.MDImain.frmTitle.cmdResumeLoadConv.BackColor = System.Drawing.Color.WhiteSmoke;
            AppGen.Inst.MDImain.frmTitle.cmdResumeIndexTable.BackColor = System.Drawing.Color.WhiteSmoke;
            AppGen.Inst.MDImain.frmTitle.cmdResumeUnloadConv.BackColor = System.Drawing.Color.WhiteSmoke;
            AppGen.Inst.MDImain.frmTitle.cmdResumeRobots.BackColor = System.Drawing.Color.WhiteSmoke;
            AppGen.Inst.MDImain.frmTitle.cmdResumeCameras.BackColor = System.Drawing.Color.WhiteSmoke;

            AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.Yellow);
            AppGen.Inst.MDImain.frmTitle.Buzzer(false);

            //-----resuming robots in case door opend:--------

            System.Threading.Tasks.Task.Factory.StartNew(() => { TurnOffResumBit(); });           

            //-----reseting all stations errors:--------------- fixed at 19.10.14
            //19.10.14 AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResetErrors = true;
            //19.10.14 AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hResetErrors, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResetErrors);
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResumeAll = true;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hResumeAll, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResumeAll);
            //AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
            //--------------------------------------------------

            AppGen.Inst.MainCycle.MainControl.bError = false;
            AppGen.Inst.MainCycle.MainControl.iErrNo = 0;
            AppGen.Inst.MainCycle.MainControl.sErrMsg = "";
            AppGen.Inst.MainCycle.MainControl.bMsgDisplayed = false;
            AppGen.Inst.MainCycle.MainControl.bPause = false;
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblStahliMsg, "");

            if (AppGen.Inst.MainCycle.MainControl.bStart)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Resuming Production ...");
            }

            AppGen.Inst.MainCycle.LoadCycleControl.ErrLoged = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.ErrLoged = false;
            AppGen.Inst.MainCycle.IndexTableControl.ErrLoged = false;
            AppGen.Inst.Cam1.bMsgDisplayed = false;
            AppGen.Inst.Cam2.bMsgDisplayed = false;
            AppGen.Inst.Cam3.bMsgDisplayed = false;

            ResumIndexTable();
            ResumUnloadConv();
            ResumLoadConv();
            ResumRobots();
            ResumCameras();

        }
        public void ResumLoadConv()
        {
            AppGen.Inst.LogFile("Operation pressed Resume Load Conveyor", LogType.Production);
            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResErr = true;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hResErr, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.ResErr);

            AppGen.Inst.MainCycle.LoadCycleControl.bError = false;
            AppGen.Inst.MainCycle.LoadCycleControl.ErrLoged = false;
            AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 0;
            AppGen.Inst.MainCycle.LoadCycleControl.sErrMsg = "";
            AppGen.Inst.MainCycle.LoadCycleControl.bMsgDisplayed = false;
            AppGen.Inst.MainCycle.LoadCycleControl.bPause = false;

            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, "");
            AppGen.Inst.MDImain.frmAssemblies.UpdateFrmAssemblies(FrmAssembliesData.LoadConvErrMsg, "");
        }
        public void ResumIndexTable()
        {
            AppGen.Inst.LogFile("Operation pressed Resume Index Table", LogType.Production);
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.req_ResetTableError = true;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hreq_ResetTableError, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.req_ResetTableError);

            AppGen.Inst.MainCycle.IndexTableControl.bError = false;
            AppGen.Inst.MainCycle.IndexTableControl.ErrLoged = false;
            AppGen.Inst.MainCycle.IndexTableControl.iErrNo = 0;
            AppGen.Inst.MainCycle.IndexTableControl.sErrMsg = "";
            AppGen.Inst.MainCycle.IndexTableControl.bMsgDisplayed = false;
            AppGen.Inst.MainCycle.IndexTableControl.bPause = false;

            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblIndexTableMsg, "");
            AppGen.Inst.MDImain.frmAssemblies.UpdateFrmAssemblies(FrmAssembliesData.UnloadConvErrMsg, "");
        }
        public void ResumUnloadConv()
        {
            AppGen.Inst.LogFile("Operation pressed Resume Unload Conveyor", LogType.Production);
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResErr = true;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hResErr, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.ResErr);           

            AppGen.Inst.MainCycle.UnloadCycleControl.bError = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.ErrLoged = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.iErrNo = 0;
            AppGen.Inst.MainCycle.UnloadCycleControl.sErrMsg = "";
            AppGen.Inst.MainCycle.UnloadCycleControl.bMsgDisplayed = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.bPause = false;

            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblUnloadConvMsg, "");
            AppGen.Inst.MDImain.frmAssemblies.UpdateFrmAssemblies(FrmAssembliesData.IndexTableErrMsg, "");

        }
        public void ResumRobots()
        {
            if ((AppGen.Inst.RobotConnection.stLoadRobotControl.bDoorOpened) || (AppGen.Inst.RobotConnection.stUnloadRobotControl.bDoorOpened))
            {
                return;
            }

            System.Threading.Tasks.Task.Factory.StartNew(() => { TurnOffResumBit(); });
            /*AppGen.Inst.LogFile("Operation pressed Resume Robots", LogType.Production);
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.q_ReqResumeCycle = true;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.q_ReqResumeCycle = true;
            AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
            Thread.Sleep(1000);  tbd!  stay ON now
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.q_ReqResumeCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.q_ReqResumeCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();*/

            AppGen.Inst.RobotConnection.RL.ConnBusy = false;
            AppGen.Inst.RobotConnection.RU.ConnBusy = false;

            AppGen.Inst.RobotConnection.stLoadRobotControl.ErrLoged = false;
            AppGen.Inst.RobotConnection.stUnloadRobotControl.ErrLoged = false;

            if ((AppGen.Inst.RobotConnection.stLoadRobotControl.iErrNo == 0) && (AppGen.Inst.RobotConnection.stUnloadRobotControl.iErrNo == 0))
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, "");
            }
        }
        private void TurnOffResumBit()  
        {
            AppGen.Inst.LogFile("Operation pressed Resume Robots", LogType.Production);
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqResumeCycle = true;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqResumeCycle = true;
            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_ReqResumeCycle , true);
            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_ReqResumeCycle, true);
            Thread.Sleep(2000);

            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqResumeCycle = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqResumeCycle = false;
            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_ReqResumeCycle , false);
            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.H_q_ReqResumeCycle , false);
        }
        public void ResumCameras()
        {
            AppGen.Inst.Cam1.bMsgDisplayed = false;
            AppGen.Inst.Cam2.bMsgDisplayed = false;
            AppGen.Inst.Cam3.bMsgDisplayed = false;

            AppGen.Inst.Cam1.bError = false;
            AppGen.Inst.Cam2.bError = false;
            AppGen.Inst.Cam3.bError = false;

            AppGen.Inst.LogFile("Operation pressed Resume Camera", LogType.Production);
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblCamerasMsg, "");
        }
        //<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public void RunMainCycle()    //Run All 
        {
            AppGen.Inst.LogFile("Operation pressed Start", LogType.Production);

            if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TopLight)
            {
                AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TopLight = false;
                AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hTopLight, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.TopLight);
            }

            MainProccesState = ProcessStatus.Running;
            AppGen.Inst.MDImain.AfterModeChanged();

            if (AppGen.Inst.MDImain.frmTitle.chkLoadRobotSetup.Checked)
            {
              AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqPauseCycle = false; 
                

            }
            if (AppGen.Inst.MDImain.frmTitle.chkUnloadRobotSetup.Checked)
            {
                   AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqPauseCycle = false; 
               
            }

            
            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB = 2;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB);
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB = 2;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB);
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB = 2;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB);

            UnloadCarrierSliceNo = 1;     //init for snap 1st slice
            AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = false;  //search only in one slice (for next new unload carrier)
            RescanCycle = false;
            RescanCarrier = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrDone = false;            

            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Machine at Work");
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "1");
            AppGen.Inst.MainCycle.ResumGeneral();
            AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.Green);

            AppGen.Inst.MainCycle.MainControl.bPause = false;
            AppGen.Inst.MainCycle.MainControl.bRun = true;
        }
        public void PauseMainCycle()  //Pause All
        {
            AppGen.Inst.LogFile("Operation pressed Pause", LogType.Production);
            MainProccesState = ProcessStatus.Pause;
            AppGen.Inst.MDImain.AfterModeChanged();
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "2");
            if (AppGen.Inst.MDImain.frmTitle.chkLoadRobotSetup.Checked)
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_ReqPauseCycle = true;
                
                AppGen.Inst.MainCycle.LoadCycleControl.bPause = true;
            }
            if (AppGen.Inst.MDImain.frmTitle.chkUnloadRobotSetup.Checked)
            {
                AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_ReqPauseCycle = true;
               
                AppGen.Inst.MainCycle.UnloadCycleControl.bPause = true;
            }

           
            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB = 1;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB);
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB = 1;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB);
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB = 1;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB);

            AppGen.Inst.MainCycle.MainControl.bPause = true;
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Machine Paused");

            AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.Yellow);
            
        }
        public void StopAll()         //Stop All
        {
            if (MainProccesState == ProcessStatus.Running)
            {
                PauseMainCycle();
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Machine Paused, To Stop Machine - Press Stop again");
                return;
            }
            AppGen.Inst.LogFile("Operation pressed STOP", LogType.Production);
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "0");
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_StopCycleResetAll = true;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_StopCycleResetAll = true;           
            System.Threading.Thread.Sleep(500);

            AppGen.Inst.MainCycle.MainControl.bStart = false;
            AppGen.Inst.MainCycle.MainControl.bPause = false;
            AppGen.Inst.MainCycle.MainControl.bRun = false;

            AppGen.Inst.MainCycle.LoadCycleControl.bRun = false;
            AppGen.Inst.MainCycle.UnloadCycleControl.bRun = false;

            AppGen.Inst.MDImain.frmTitle.tmrHiTimerSnap.Stop();


            AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB = 0;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.LoadConveyor_PLC.Op_VB);
            AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB = 0;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.UnloadConveyor_PLC.Op_VB);
            AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB = 0;
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.hOp_VB, AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PLC.Op_VB);

            AppGen.Inst.MDImain.frmTitle.Ramzor(RamzorColor.Yellow);

         
            MainProccesState = ProcessStatus.Stop;
            AppGen.Inst.MDImain.AfterModeChanged();
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Machine Stoped");
        }
        public void ResetAll()        //Reset All
        {
            if (MainProccesState != ProcessStatus.Stop)
            {
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Can't Reset while machine at Automatic Mode");
                return;
            }
            AppGen.Inst.AppSettings.Serialize();  //saveing all index and counters (in case of software crash)



            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResetErrors = true;          //Japan 30.6.15
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hResetErrors, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResetErrors);



            AppGen.Inst.LogFile("Operation pressed ResetAll", LogType.Production);
            AppGen.Inst.MDImain.frmTitle.setupResultGoodLed0.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultGoodLed1.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultGoodLed2.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultGoodLed3.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultGoodLed4.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultGoodLed5.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultGoodLed6.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultFailLed0.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultFailLed1.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultFailLed2.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultFailLed3.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultFailLed4.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultFailLed5.Visible = false;
            AppGen.Inst.MDImain.frmTitle.setupResultFailLed6.Visible = false;

            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "");
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblMsg, "Reset in process ...     Wait !!");
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "7");


            AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl = false;
            AppGen.Inst.RobotConnection.stUnloadRobotControl.AutoModeFl = false;

            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_FoundOrientation = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CarrDone = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_StopCycleResetAll = false;
            AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_StopCycleResetAll = false;
            
            //resuming all station errors:
            ResumGeneral();
            ResumLoadConv();
            ResumIndexTable();
            ResumUnloadConv();
            ResumRobots();

            //Resetting All state done flags:

            LoadCycleControl.bDone = true;
            LoadCycleControl.bRun = false;
            LoadCycleControl.bPause = false;
            LoadCycleControl.iStep = 0;

            ResumeOnce = false;
            UnloadCycleControl.bDone = true;
            UnloadCycleControl.bRun = false;
            UnloadCycleControl.bPause = false;
            UnloadCycleControl.iStep = 0;


            sw_TimeOut.Start();  // Start The StopWatch ...From 000
            AppGen.Inst.MDImain.frmTitle.tmrWaitResetAllDone.Start();
            //tbd: unloacking Vision manual buttons
            AppGen.Inst.RobotConnection.ResetRobotComm(ROBOT_INDEXES.ENUM_LOAD_ROBOT);
            AppGen.Inst.RobotConnection.ResetRobotComm(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT);
            //tbd: StopAfterLostIns
            //tbd: MissedCountin...Limit
            AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = true;
            AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = true;

            if (AppGen.Inst.MDImain.frmTitle.chkLoadRobotSetup.Checked)
            {
                AppGen.Inst.RobotConnection.stLoadRobotControl.bDone = false;
                AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, "99,0");
            }
            if (AppGen.Inst.MDImain.frmTitle.chkUnloadRobotSetup.Checked)
            {
                AppGen.Inst.RobotConnection.stUnloadRobotControl.bDone = false;
                AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, "99,0");
            }

            AppGen.Inst.MDImain.frmTitle.DeActiveOperationButtons();

            //-----------Enableing robot operationbuttons------:
            //tbd:  uRobotOptButtons(true)

            MainProccesState = ProcessStatus.Stop;
            AppGen.Inst.MDImain.AfterModeChanged();
            
        }
        public void CleanLine()       //Reset All
        {
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.LastBatch = true;  //qq
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hLastBatch, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.LastBatch);
            AppGen.Inst.LogFile("Operation pressed Clean-Line", LogType.Production);
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.ShowStatusImg, "3");
        }
        //<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        public System.Diagnostics.Stopwatch CarrCycleTime = new System.Diagnostics.Stopwatch();
        public System.Diagnostics.Stopwatch Robot1CycleTime = new System.Diagnostics.Stopwatch();
        public System.Diagnostics.Stopwatch Robot2CycleTime = new System.Diagnostics.Stopwatch();
        public System.Diagnostics.Stopwatch sw_TimeOut = new System.Diagnostics.Stopwatch();
        //<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>><<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        // members:
        private Thread worker;
        public BasicCycle MainControl;
        public BasicCycle LoadCycleControl;
        public BasicCycle UnloadCycleControl;
        public BasicCycle IndexTableControl;
        public int[] iaSliceStatus;
        public ProcessStatus MainProccesState;
        public bool LastInsertFl;             //flage to unload robot for last insert to unload from table(after rescane if needed)   
        public bool LastLoadInsCoordSent;  //to delete??   //this flag mentions that the last load cycle insert coordinate had been sent
        public bool RescanCycle;              // flag stat if this is first scan or second scan or current slice
        public bool RescanCarrier;              // flag stat if this
        public int UnloadCarrierSliceNo;     //carrier devided to 5 slices, every snap is for one slice (cleare and creat new UnLoadTray.IndexList) 
        public int LoadedInsCounter;
        public int UnloadedInsCounter;
        public int CounterPerOrder;
        public int InsQnt;
        public int PlaceZero;
        public int PickSensor;
        public double SetupDelays;
        public int TotalUnloadMissedCount;
        public int CarrierUnloadMissedCount;
        public int CarrierUnloadVisionCount;  //Counting Inserts vision recognize every carrier 
        public bool ResumeOnce;
        public int MissPocketCarrCount;   //loading cycle
        public int MissPocketTotalCount;  //loading cycle     
        public double SetMaxOffsetRangeHeight;//Added by noam
    }
}

