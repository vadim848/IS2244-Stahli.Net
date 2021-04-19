using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Drawing;

namespace Stahli2Robots
{
    /// <summary>
    /// Name:        RobotConnection
    ///
    /// Author:      Asaf Fleishman
    /// 
    /// Abstract:    This class handles all robot subroutines and function 
    /// 
    /// </summary>
    public class RobotConnection
    {
        //constructor (for initial the class 'RobotConnection')
        public RobotConnection()
        {
            commLoadRobot = new SerialPort();
            commUnloadRobot = new SerialPort();
            RL = new RobotCon();
            RU = new RobotCon();
            stLoadRobotControl = new RobotControl();
            stUnloadRobotControl = new RobotControl();
            // When data is recieved through the port's, call this method
            commLoadRobot.DataReceived += new SerialDataReceivedEventHandler(commLoadRobot_DataReceived);
            commUnloadRobot.DataReceived += new SerialDataReceivedEventHandler(commUnloadRobot_DataReceived);
            
            sendToMeasureFl = false;
            sendToPickFl = false;
            target = 0;
        }

        //Consts
        public const double MAX_TABLE_HEIGHT = 350;
        public const double MIN_TABLE_HEIGHT = 260;

        // methods
        public bool OpenRobotPorts()
        {
            // If the port is open, close it.
            if (commLoadRobot.IsOpen) commLoadRobot.Close();
            if (commUnloadRobot.IsOpen) commUnloadRobot.Close();
            // Set the port's settings
            commLoadRobot.PortName = "COM1";
            commLoadRobot.BaudRate = int.Parse("38400");
            commLoadRobot.DataBits = int.Parse("8");
            commLoadRobot.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            commLoadRobot.Parity = (Parity)Enum.Parse(typeof(Parity), "None");

            commUnloadRobot.PortName = "COM2";
            commUnloadRobot.BaudRate = int.Parse("38400");
            commUnloadRobot.DataBits = int.Parse("8");
            commUnloadRobot.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            commUnloadRobot.Parity = (Parity)Enum.Parse(typeof(Parity), "None");

            // Open the port's and verify ports opend
            commLoadRobot.Open();
            if (!commLoadRobot.IsOpen)
            {
                AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_LOAD_ROBOT, Color.Purple, "Close");
                return false;
            }
            commUnloadRobot.Open();
            if (!commUnloadRobot.IsOpen)
            {
                AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, Color.Purple, "Close");
                return false;
            }
            return true;
        }

        public string BuildInsertCoordinates(CAMERA_NUMBER CameraNo)
        {
            string sInsertLoc = "";
            double sngInsAngle = 0;
            double sngPlaceInsAngle = 0;
            switch (CameraNo)
            {
                case CAMERA_NUMBER.cLoadTrayCAM:
                case CAMERA_NUMBER.cLoadCarrierCAM:
                    switch ((INSERT_SYMETRY)AppGen.Inst.OrderParams.InsertSymetry)
                    {
                        case INSERT_SYMETRY.SQUARE:
                            if (AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes > 90)
                            {
                                AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes = AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes % 90;
                            }
                            break;
                        case INSERT_SYMETRY.TRIANGLE:
                            if (AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes > 120)
                            {
                                AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes = AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes % 120;
                            }
                            break;
                        case INSERT_SYMETRY.RECTENGLE:
                        case INSERT_SYMETRY.DAIMOND:
                            if (AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes > 180)
                            {
                                AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes = AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes % 180;
                            }
                            break;
                    }
                    if (AppGen.Inst.LoadTray.CurrIndex > AppGen.Inst.LoadTray.IndexList.Count) //camera index over the maximum index allowed
                    {
                        AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 503;
                        AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);
                        return "";
                    }

                    sInsertLoc = "15," +
                        AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].X_VisRes.ToString("f2") + "," +
                        AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Y_VisRes.ToString("f2") + "," +
                        AppGen.Inst.LoadTray.IndexList[AppGen.Inst.LoadTray.CurrIndex].Angle_VisRes.ToString("f2") + ",";     //Coord of pick from load tray

                    sInsertLoc += (AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.X + AppGen.Inst.VisionParam.LoadCarrAddedX).ToString("f2") + "," + //Coord of place on load carrier
                        (AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.Y + AppGen.Inst.VisionParam.LoadCarrAddedY).ToString("f2") + "," +
                        ((double)(AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.Angle) + AppGen.Inst.VisionParam.LoadAddedAngle).ToString("f2") + ",";
                    if (AppGen.Inst.LoadCarrier.CurrIndex < AppGen.Inst.LoadCarrier.IndexList.Count - 1)
                    {
                        sInsertLoc += "0";
                    }
                    else
                    {
                        sInsertLoc += "1";   //(not in use in this project)done loading carrier (robot use this flag to signal PLC "carrier done" by ADS (or IO))
                    }

                    if (AppGen.Inst.LoadCarrier.CurrIndex < AppGen.Inst.LoadCarrier.IndexList.Count - 1)
                    {
                        AppGen.Inst.LoadCarrier.CurrIndex += 1;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
                        AppGen.Inst.MDImain.frmBeckhoff.RobotDada.ProcesPercent = Convert.ToInt16(AppGen.Inst.LoadCarrier.CurrIndex / AppGen.Inst.LoadCarrier.IndexList.Count * 100);                       
                    }
                    else //done loading Carrier:
                    {   //(problem!  robot still not get last string(onlyatthe end of this function) and maybe hide the carrier for rescan!! )
                        //if ((AppGen.Inst.MDImain.frmTitle.chkFillingMissingInserts.Checked) || (AppGen.Inst.MDImain.frmTitle.chkTotalEmptyPockets.Checked) || (AppGen.Inst.MDImain.frmTitle.chkCarrierEmptyPockets.Checked))
                        //{
                        //    AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.VisionAction(VisionActionType.AutoMissedScan); //scan all carrier indexes and find empty pockets
                        //    if (AppGen.Inst.MDImain.frmTitle.chkFillingMissingInserts.Checked)
                        //    {
                        //        //tbd (?)  filling missing pockets..
                        //    }
                        //    if ((AppGen.Inst.MDImain.frmTitle.chkTotalEmptyPockets.Checked) && (AppGen.Inst.MainCycle.MissPocketTotalCount >= Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtMaxTotalEmptyPockets.Text)))
                        //    {
                        //        //tbd - stop load cycle  (Total too many Empty pockets)
                        //        AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 555;  
                        //        AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                        //        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);
                        //        //return "";  // remarkat japan (cant send empty string).  last string forlastindex must be sent
                        //    }
                        //    if ((AppGen.Inst.MDImain.frmTitle.chkCarrierEmptyPockets.Checked) && (AppGen.Inst.MainCycle.MissPocketCarrCount >= Convert.ToInt16(AppGen.Inst.MDImain.frmTitle.txtMaxCarrEmptyPockets.Text)))
                        //    {
                        //        //tbd - stop load cycle  (One carrier too many Empty pockets)
                        //        AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 556;  
                        //        AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                        //        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblLoadConvMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.MainCycle.LoadCycleControl.iErrNo]);
                        //        //return "";  // remarkat japan (cant send empty string).  last string forlastindex must besent
                        //    }
                        //    AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.MissedLoadCount, AppGen.Inst.MainCycle.MissPocketTotalCount.ToString());
                        //}
                        if ((AppGen.Inst.MDImain.frmTitle.chkFillingMissingInserts.Checked) || (AppGen.Inst.MDImain.frmTitle.chkTotalEmptyPockets.Checked) || (AppGen.Inst.MDImain.frmTitle.chkCarrierEmptyPockets.Checked))
                        {
                            AppGen.Inst.MainCycle.RescanCarrier = true;
                            AppGen.Inst.MainCycle.LoadCycleControl.iStep = 30;
                            //AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Vb_VisionDone = false; //only PLC turn off    --new signal to LPC VisionCarrDone=false (will turn on at step 0 after rescan)
                        }
                        else
                        {
                            AppGen.Inst.MainCycle.RescanCarrier = false;
                            AppGen.Inst.MainCycle.LoadCycleControl.iStep = 0;
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Vb_VisionDone = true;   //new signal to LPC VisionCarrDone=true 
                        }
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.CarrierCycleTime, AppGen.Inst.MainCycle.CarrCycleTime.Elapsed.Seconds.ToString());
                        AppGen.Inst.LoadCarrier.CurrIndex = 0;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
                        //AppGen.Inst.MainCycle.LoadCycleControl.iStep = 0;  //now depend if Rescan(30) or not(0)
                        AppGen.Inst.MDImain.frmBeckhoff.RobotDada.ProcesPercent = 0;                     
                    }
                    break;
                case CAMERA_NUMBER.cUnloadCarrierCAM:
                   int target;
                   target = 0;      // any other case. sent to tray normaly
                   if ((AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Req_Measure) && (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CaliberReady) && (!AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_MeasurePlaced) && (!sendToMeasureFl))
                   {
                       target = 1;   //to measure
                   }
                   else if ((AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.MeasureDone) && (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.Q_CaliberReady) && (AppGen.Inst.MDImain.frmBeckhoff.RobotDada2.I_MeasurePlaced) && (!sendToPickFl))
	               {
		                target = 2;   //from measure                        
	               }
                    sngInsAngle = AppGen.Inst.UnLoadCarrier.IndexList[AppGen.Inst.UnLoadCarrier.CurrIndex].Angle_VisRes * (-1);  //(-1) for recieveing oposite angle from vision
                    sngPlaceInsAngle = AppGen.Inst.UnLoadTray.IndexList[AppGen.Inst.UnLoadTray.CurrIndex].Angle_VisRes;
                    sngPlaceInsAngle += AppGen.Inst.VisionParam.UnloadAddedAngle;

                    switch ((INSERT_SYMETRY)AppGen.Inst.OrderParams.InsertSymetry)
                    {
                        case INSERT_SYMETRY.SQUARE:
                            if (sngInsAngle > 90)
                            {
                                sngInsAngle = sngInsAngle % 90;
                            }
                            break;
                        case INSERT_SYMETRY.TRIANGLE:
                            if (sngInsAngle > 120)
                            {
                                sngInsAngle = sngInsAngle % 120;
                            }
                            if (target == 0)
                            {
                                if (AppGen.Inst.MDImain.frmTitle.chkTriangles90.Checked)
                                { // checking if index is at odd or even row at the service tray
                                    if (((AppGen.Inst.UnLoadTray.CurrIndex) / (AppGen.Inst.UnLoadTray.InsertCountAtServiceRow)) % 2 == 1)
                                    { // service tray is at an even row
                                        sngInsAngle = sngInsAngle + 30;
                                    }
                                    else
                                    { // service tray is at an odd row
                                        sngInsAngle = sngInsAngle - 30;
                                    }
                                }
                                else  // checking if index is at odd or even row at the service tray
                                {
                                    if ((AppGen.Inst.UnLoadTray.IndexList[AppGen.Inst.UnLoadTray.CurrIndex].CollumnNum) % 2 == 0)
                                    {   // service tray is at an even row
                                        sngInsAngle = sngInsAngle + 30;
                                    }
                                    else
                                    {   // service tray is at an odd row
                                        sngInsAngle = sngInsAngle - 30;
                                    }
                                }
                            }                           
                            break;
                        case INSERT_SYMETRY.RECTENGLE:
                        case INSERT_SYMETRY.DAIMOND:
                            if (sngInsAngle > 180)
                            {
                                sngInsAngle = sngInsAngle % 180;
                            }
                            break;
                    }
                    sInsertLoc = "15," +
                       AppGen.Inst.UnLoadCarrier.IndexList[AppGen.Inst.UnLoadCarrier.CurrIndex].X_VisRes.ToString("f2") + "," +
                       AppGen.Inst.UnLoadCarrier.IndexList[AppGen.Inst.UnLoadCarrier.CurrIndex].Y_VisRes.ToString("f2") + "," +
                       sngInsAngle.ToString("f2") + ",";

                    sInsertLoc += (AppGen.Inst.UnLoadTray.IndexList[AppGen.Inst.UnLoadTray.CurrIndex].X_VisRes + AppGen.Inst.VisionParam.OriginXOffset).ToString("f2") + "," +
                                 (AppGen.Inst.UnLoadTray.IndexList[AppGen.Inst.UnLoadTray.CurrIndex].Y_VisRes + AppGen.Inst.VisionParam.OriginYOffset).ToString("f2") + "," +
                                 sngPlaceInsAngle.ToString("f2") + ",";                  
                   switch (target)
	               {
                        case 0:         // to Tray (normal cycle) 
                            sInsertLoc += "0";
                            sendToMeasureFl = false;
                            sendToPickFl = false; 
                            break;
                        case 1:         // From Carrier to Measure point 
                            sendToMeasureFl = true;
                            sInsertLoc += "1";  
                            AppGen.Inst.UnLoadTray.CurrIndex -= 1;                          
                            break;
                        case 2:        // From Measure point to Tray
                            sendToPickFl = true; 
                            sInsertLoc += "2";
                            AppGen.Inst.UnLoadCarrier.CurrIndex -= 1;
                           //read, show and Log measurement result:
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.measureResult, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.LastMeasure);
                            AppGen.Inst.LogFile("Insert Measure = " + AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.LastMeasure, LogType.Production);
                            AppGen.Inst.LogFile(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.LastMeasure, LogType.InsertMeasure);
                            break;
	                }
                    break;
            }
            return sInsertLoc;         
        }

        public bool SendData(ROBOT_INDEXES robot, string LocMsg, bool autoLoadFl = false)
        {
            try
            {
                switch (robot)
                {
                    case ROBOT_INDEXES.ENUM_LOAD_ROBOT:

                        if (AppGen.Inst.RobotConnection.stLoadRobotControl.bDoorOpened)
                        {
                            //tbd: ErrDescrip(411, frmTitle.lblRobotsMsg)
                            return false;
                        }
                        if (!String.Equals(LocMsg.Substring(0, LocMsg.Length), "CMD")) LocMsg = ("cmd" + LocMsg); //if first 3 char are NOT "CMD" then add CMD
                        if (autoLoadFl == true)  //AUTO MODE; at old project use to be:  Send_AutoLoadCmd1
                        {
                            AppGen.Inst.MDImain.frmBeckhoff.RobotDada.Q_DataSended = true;
                            //e AppGen.Inst.MDImain.frmBeckhoff.UpdateRobotData();
                            //AppGen.Inst.MDImain.frmBeckhoff.WriteData(AppGen.Inst.MDImain.frmBeckhoff.RobotDada.H_q_DataSended, true);
                            commLoadRobot.Write(LocMsg + "\n\r");
                            RL.sLastSentCmd = LocMsg.Substring(0, 5);
                            AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_LOAD_ROBOT, Color.Red, "Waiting..");
                            AppGen.Inst.MDImain.frmRobots.ShowCommString(ROBOT_INDEXES.ENUM_LOAD_ROBOT, GetSend_str.ENUM_SEND, LocMsg);
                            return true;
                        }
                        if (RL.ConnBusy)
                        {
                            Stopwatch sw = new Stopwatch();  // RL.TimeOutCounter = 3000
                            sw.Start();
                            while (true)
                            {
                                System.Threading.Thread.Sleep(50);
                                System.Windows.Forms.Application.DoEvents();
                                if (!RL.ConnBusy)
                                {
                                    //GlobalErr "Robot Sending Suspended for : " & (timeGetTime() - StartTime) & " ms" : Exit Do
                                    break;  //exiting while loop?
                                }
                                if (sw.ElapsedMilliseconds > RL.TimeOutCounter)  //(timeout accured)
                                {
                                    //if RL.sLastSentCmd = "cmd15" And Left(LocMsg, 5) = "cmd15" Then GlobalErr "No response to previous cmd15 comand. Send command wasn't executed.":  Exit Function
                                    //GlobalErr "Unload Robot connection busy, send command was not executed"
                                    //Exit Function
                                    break;
                                    //throw new TimeoutException();
                                }
                            }
                        }
                        // Send the user's text straight out the port
                        commLoadRobot.Write(LocMsg + "\n\r");     //vb6: vbCrLf
                        RL.ConnBusy = true;
                        if (LocMsg.Substring(0, 5) == "cmd70")
                        {
                            RL.ConnBusy = false;
                        }
                        RL.sLastSentCmd = LocMsg.Substring(0, 5);  //Left$(LocMsg, 5) -->    (cmdXX)
                        AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_LOAD_ROBOT, Color.Red, "Waiting..");
                        AppGen.Inst.MDImain.frmRobots.ShowCommString(ROBOT_INDEXES.ENUM_LOAD_ROBOT, GetSend_str.ENUM_SEND, LocMsg);
                        return true;
                    case ROBOT_INDEXES.ENUM_UNLOAD_ROBOT:
                        if (AppGen.Inst.RobotConnection.stUnloadRobotControl.bDoorOpened)
                        {
                            //tbd: ErrDescrip(411, frmTitle.lblRobotsMsg)
                            return false;
                        }
                        if (!String.Equals(LocMsg.Substring(0, LocMsg.Length), "CMD")) LocMsg = ("cmd" + LocMsg); //if first 3 char are NOT "CMD" then add CMD

                        if (RU.ConnBusy)
                        {
                            Stopwatch sw2 = new Stopwatch();  // RU.TimeOutCounter = 3000
                            sw2.Start();
                            while (true)
                            {
                                System.Threading.Thread.Sleep(50);
                                System.Windows.Forms.Application.DoEvents();
                                if (!RU.ConnBusy)
                                {
                                    //GlobalErr "Robot Sending Suspended for : " & (timeGetTime() - StartTime) & " ms" : Exit Do
                                    break;  //exiting while loop?
                                }
                                if (sw2.ElapsedMilliseconds > RU.TimeOutCounter)  //(timeout accured)
                                {
                                    //if RL.sLastSentCmd = "cmd15" And Left(LocMsg, 5) = "cmd15" Then GlobalErr "No response to previous cmd15 comand. Send command wasn't executed.":  Exit Function
                                    //GlobalErr "Unload Robot connection busy, send command was not executed"
                                    //Exit Function
                                    throw new TimeoutException();   //tbd.  handle this time out
                                }
                            }
                        }
                        // Send the user's text straight out the port
                        commUnloadRobot.Write(LocMsg + "\n\r");     //vb6:  vbCrLf
                        RU.ConnBusy = true;
                        RU.sLastSentCmd = LocMsg.Substring(0, 5);  //Left$(LocMsg, 5) -->    (cmdXX)
                        AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, Color.Red, "Waiting..");
                        AppGen.Inst.MDImain.frmRobots.ShowCommString(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, GetSend_str.ENUM_SEND, LocMsg);
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                AppGen.Inst.LogFile(ex.Message, LogType.SystemErr);
                return false;
            }
        }
        // This method will be called when there is data waiting in the port's buffer     
        public void commLoadRobot_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int tmpRecievedRobotErrNum = 0;
                string AdeptRecievedRobotErrMsg = "";
                // If the com port has been closed, do nothing
                if (!commLoadRobot.IsOpen) return;
                // Read all the data waiting in the buffer
                //string aux = commLoadRobot.ReadExisting();
                string aux = commLoadRobot.ReadLine();
                //vb6 //if Right$(aux, 1) = vbLf Then
                //string temp = aux.Substring(aux.Length - 1);
                //temp = aux.Substring(aux.Length - 2);

                if (aux.Substring(aux.Length - 1) == "\r")
                {
                    if (aux.Substring(0, 5) == "cmd10")
                    {
                        //todo: add global flagsSemiAutoSnapFlag = true;
                    }
                    if (aux.Substring(0, 5) == "cmd16")
                    {
                        bLoadResRecieved = true;
                    }
                    else
                    {
                        if (aux.Substring(0, 5) == "cmd15")  //pick & place cycle
                        {
                            AppGen.Inst.RobotConnection.stLoadRobotControl.AutoModeFl = false;
                            if (aux.Length > 8)
                            {
                                // in case an insert was not picked
                                if (aux.Substring(9, 1) == "0")
                                {
                                    //baInsLoadedOK(CInt(Mid(aux, 11, Len(aux)))) = false;
                                    //bLoadRobotPickFailed = true;
                                    //bLastLoadInsCoordSent = false;
                                    //iMissedLoadCount = iMissedLoadCount + 1;
                                    //if (iMissedLoadCount >= 3)
                                    //{
                                    //   stLoadCycleControl.iErrNo = 115;
                                    //   stLoadCycleControl.bError = true;
                                    //   iMissedLoadCount = 0;
                                    //}
                                }
                                else
                                {
                                    //iMissedLoadCount = 0;
                                }
                            }
                        }
                    }
                    if (RL.sLastSentCmd == aux.Substring(0, 5))
                    {
                        RL.ConnBusy = false;
                        if (aux.Substring(6, 1) == "1")
                        {
                            if (aux.Substring(0, 5) == "cmd16")
                            {
                                // if (bLastLoadInsCoordSent)
                                //{
                                //    bLastLoadInsResRecieved = true;
                                //}
                                //else 
                                //{
                                //    bLastLoadInsResRecieved = false;
                                //}
                            }
                            else if (aux.Substring(0, 5) == "cmd23")
                            {
                                //updating the input image (at frmRobots)if the relevent cmd was issued
                                //frmRobots.uUpdateInputLED (Mid(aux, 10, 2))
                                string InputState = (aux.Substring(9, (aux.Length - 9)));
                                AppGen.Inst.MDImain.frmRobots.UpdateFrmRobot(FrmRobotData.InputState1, InputState);
                            }
                            else if (aux.Substring(0, 5) == "cmd34")
                            {
                                // '   checking the robot gripper code
                                //Call uUpdateGrType(CInt(Mid(aux, 9, 2)))
                            }
                            else if (aux.Substring(0, 5) == "cmd40")
                            {
                                //'   retrieving the service tray origin
                                //Call uGetRobotOrigin(ENUM_LOAD_ROBOT1, Mid(aux, 10, Len(aux) - 9))
                            }
                            else if (aux.Substring(0, 5) == "cmd41")
                            {                               
                                string[] strArg = aux.Substring(7, (aux.Length - 7)).Split(',');
                                GetRobotPoints(strArg);
                            }
                            else if (aux.Substring(0, 5) == "cmd50")  //aftre read
                            {
                                sngCurrentHeight = Convert.ToSingle(aux.Substring(9, (aux.Length - 9)));
                                AppGen.Inst.MDImain.frmRobots.UpdateFrmRobot(FrmRobotData.CurrHight1, Convert.ToString(sngCurrentHeight));
                            }
                            else if (aux.Substring(0, 5) == "cmd51")  //after write
                            {
                                sngCurrentHeight = Convert.ToSingle(aux.Substring(9, (aux.Length - 9)));
                                AppGen.Inst.MDImain.frmRobots.UpdateFrmRobot(FrmRobotData.CurrHight1, Convert.ToString(sngCurrentHeight));
                            }
                            else if (aux.Substring(0, 5) == "cmd70")
                            {
                                stLoadRobotControl.AutoModeFl = true;
                            }
                            else if (aux.Substring(0, 5) == "cmd98")
                            {

                            }
                            else if (aux.Substring(0, 5) == "cmd99")
                            {
                                stLoadRobotControl.bDone = true;
                                stLoadRobotControl.iErrNo = 0;
                            }
                            RL.ConnSuccess = true;
                        }
                        else
                        {
                            RL.ConnSuccess = false;

                            if (aux.Substring(0, 5) == "cmd99")
                            {
                                System.Windows.Forms.MessageBox.Show("Loading Robot Must be Righty!");
                            }

                            //tbd:??  vb6?
                            //RL.ErrNo = CInt(Mid(aux, 9, 2))
                            //With frmTitle
                            //    GlobalErr ErrDescrip(RL1.ErrNo)
                            //End With
                        }
                    }
                    else if (aux.Substring(0, 3) == "999")  //robot responded with a sepcific error
                    {
                        string tmpSysErrFl = aux.Split(',')[1];
                        if (tmpSysErrFl.Substring(1, 1) == "-")
                        {
                            //Adept system error                    //-500 to -1002 is from robot system error (adept error.trap , like robot collision etc)
                            AdeptRecievedRobotErrMsg = aux.Split(',')[1];
                            tmpRecievedRobotErrNum = Convert.ToInt16(AdeptRecievedRobotErrMsg.Split('{')[0]);
                        }
                        else
                        {
                            //Shafir Error                          //200 to 220 is from robot program error (like door open etc)
                            AdeptRecievedRobotErrMsg = "";
                            tmpRecievedRobotErrNum = Convert.ToInt16(aux.Split(',')[1]);
                        }
                        if (tmpRecievedRobotErrNum == 200)  //door open, in load robot erea
                        {
                            stLoadRobotControl.iErrNo = 401;
                            stLoadRobotControl.bDoorOpened = true;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo]);
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadDoorState, "1");
                        }
                        else if (tmpRecievedRobotErrNum == 201)  //door closed, in load robot erea
                        {
                            stLoadRobotControl.bDoorOpened = false;
                            stLoadRobotControl.iErrNo = 0;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadDoorState, "0");
                        }
                        else if (tmpRecievedRobotErrNum == 203 || tmpRecievedRobotErrNum == 204)
                        {
                            stLoadRobotControl.iErrNo = 402;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo]);
                        }
                        else if (tmpRecievedRobotErrNum == 205)  //Target out of Tray/Carrier range
                        {
                            stLoadRobotControl.iErrNo = 403;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo]);
                        }
                        else if (tmpRecievedRobotErrNum == -405)  //Adept: "  illigle signal
                        {
                            stLoadRobotControl.iErrNo = 404;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, (AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg));
                        }
                        else if (tmpRecievedRobotErrNum == -618)  //Adept
                        {
                            stLoadRobotControl.iErrNo = 405;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                        }
                        else if (tmpRecievedRobotErrNum == -901)  //Adept
                        {
                            stLoadRobotControl.iErrNo = 406;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                        }
                        else if (tmpRecievedRobotErrNum == -1002)  //Adept
                        {
                            stLoadRobotControl.iErrNo = 407;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                        }
                        else if (tmpRecievedRobotErrNum == -1033)  //Adept
                        {
                            stLoadRobotControl.iErrNo = 408;
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                        }                      
                        else
                        {
                            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, "Load robot Error" + tmpRecievedRobotErrNum.ToString());
                        }
                        if ((tmpRecievedRobotErrNum != 200) && (tmpRecievedRobotErrNum != 201))  //not loging open/close door
                        {
                            AppGen.Inst.LogFile(stLoadRobotControl.iErrNo.ToString() + "  " + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stLoadRobotControl.iErrNo] + "  " + AdeptRecievedRobotErrMsg, LogType.GeneralErr, LogStation.LoadRobot);
                            AppGen.Inst.RobotConnection.stLoadRobotControl.ErrLoged = true;
                        }
                    }
                    else
                    {
                        //GlobalErr "Error In Receive Message ""CommLoadRobot""."
                        RL.ConnSuccess = false;
                    }
                    RL.GetMsg = aux;
                    AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_LOAD_ROBOT, Color.LimeGreen, " ");
                    AppGen.Inst.MDImain.frmRobots.ShowCommString(ROBOT_INDEXES.ENUM_LOAD_ROBOT, GetSend_str.ENUM_GET, aux);
                    aux = "";
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                AppGen.Inst.LogFile(ex.Message, LogType.SystemErr);
            }
        }
        private void commUnloadRobot_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int tmpRecievedRobotErrNum = 0;
            string AdeptRecievedRobotErrMsg = "";
            // If the com port has been closed, do nothing
            if (!commUnloadRobot.IsOpen) return;
            // Read all the data waiting in the buffer
            string aux = commUnloadRobot.ReadLine();

            if (aux.Substring(aux.Length - 1) == "\r")
            {
                if (aux.Substring(0, 5) == "cmd16")   // out of Auto Loop, not Sync  - Only at the end of Auto cycle
                {
                    stUnloadRobotControl.AutoModeFl = false;
                    RU.ConnBusy = false;
                }
                if (RU.sLastSentCmd == aux.Substring(0, 5))
                {
                    RU.ConnBusy = false;
                    if (aux.Substring(0, 5) == "cmd15")     //missed insert is by Vision only in this project
                    {
                        //if (aux.Substring(6, 1) == "0")  //in case insert was not picked
                        //{
                        //    iMissedUnloadCount = +1;
                        //    TotalUnloadMissed =+ 1;
                        //    //tbd update frmTitle
                        //    if ((iMissedUnloadCount > MissedContinueslyLimit) && (AppGen.Inst.MDImain.frmTitle.chkContinueMissed.Checked))
                        //    {
                        //        AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 315;
                        //        AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                        //        //tbd errDesc , lblConvMsg
                        //       TotalUnloadMissed = 0;
                        //    }
                        //    if ((TotalUnloadMissed >= StopAfterLostIns) && (AppGen.Inst.MDImain.frmTitle.chkMaxTotalInTrash.Checked))
                        //    {
                        //        AppGen.Inst.MainCycle.LoadCycleControl.iErrNo = 316;
                        //        AppGen.Inst.MainCycle.LoadCycleControl.bError = true;
                        //        //tbd errDesc , lblConvMsg
                        //        TotalUnloadMissed = 0;
                        //        //tbd- txtMissedUnload.text = 0; (update frmTitle)
                        //    }
                        //}
                        //else    // in case insert was pickd OK
                        //{
                        //    iMissedUnloadCount = 0;
                        //}                                       
                    }
                    else if (aux.Substring(0, 5) == "cmd70")
                    {
                        stUnloadRobotControl.AutoModeFl = true;
                    }
                    else if (aux.Substring(6, 1) == "1")
                    {
                        if (aux.Substring(0, 5) == "cmd23")
                        {
                            //updating the input image (at frmRobots)if the relevent cmd was issued
                            //frmRobots.uUpdateInputLED (Mid(aux, 10, 2))
                            string InputState = (aux.Substring(9, (aux.Length - 9)));
                            AppGen.Inst.MDImain.frmRobots.UpdateFrmRobot(FrmRobotData.InputState2, InputState);
                        }
                        else if (aux.Substring(0, 5) == "cmd34")
                        {
                            // '   checking the robot gripper code
                            //Call uUpdateGrType(CInt(Mid(aux, 9, 2)))
                        }
                        else if (aux.Substring(0, 5) == "cmd40")
                        {
                            //'   retrieving the service tray origin
                            //Call uGetRobotOrigin(ENUM_LOAD_ROBOT1, Mid(aux, 10, Len(aux) - 9))
                        }
                        else if (aux.Substring(0, 5) == "cmd50")  //aftre read
                        {
                            sngCurrentHeight = Convert.ToSingle(aux.Substring(9, (aux.Length - 9)));
                            AppGen.Inst.MDImain.frmRobots.UpdateFrmRobot(FrmRobotData.CurrHight2, Convert.ToString(sngCurrentHeight));
                        }
                        else if (aux.Substring(0, 5) == "cmd51")  //after write
                        {
                            sngCurrentHeight = Convert.ToSingle(aux.Substring(9, (aux.Length - 9)));
                            AppGen.Inst.MDImain.frmRobots.UpdateFrmRobot(FrmRobotData.CurrHight2, Convert.ToString(sngCurrentHeight));
                        }

                        else if (aux.Substring(0, 5) == "cmd98")
                        {

                        }
                        else if (aux.Substring(0, 5) == "cmd99")
                        {
                            stUnloadRobotControl.bDone = true;
                            stUnloadRobotControl.iErrNo = 0;
                        }
                        RU.ConnSuccess = true;
                    }
                    else
                    {
                        RU.ConnSuccess = false;
                        //tbd: RU.ErrNo = aux.Substring(0, 5)
                        if (aux.Substring(0, 5) == "cmd99")
                        {
                            System.Windows.Forms.MessageBox.Show("Unload Robot Must be Lefty!");
                        }
                        //tbd: With frmTitle
                        //    GlobalErr ErrDescrip(RL1.ErrNo)
                        //End With
                    }
                    
                }
                else if (aux.Substring(0, 3) == "999")  //robot responded with a sepcific error
                {
                    string tmpSysErrFl = aux.Split(',')[1];
                    if (tmpSysErrFl.Substring(1,1) == "-")
                    {
                        //Adept system error                    //-500 to -1002 is from robot system error (adept error.trap , like robot collision etc)
                        AdeptRecievedRobotErrMsg = aux.Split(',')[1];
                        tmpRecievedRobotErrNum = Convert.ToInt16(AdeptRecievedRobotErrMsg.Split('{')[0]);                        
                    }
                    else
                    {
                        //Shafir Error                          //200 to 220 is from robot program error (like door open etc)
                        AdeptRecievedRobotErrMsg = "";
                        tmpRecievedRobotErrNum = Convert.ToInt16(aux.Split(',')[1]);
                    }
                    
                    if (tmpRecievedRobotErrNum == 200)  //door open, in load robot erea
                    {
                        stUnloadRobotControl.iErrNo = 401;
                        stUnloadRobotControl.bDoorOpened = true;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo]);
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.UnloadDoorState, "1");
                    }
                    else if (tmpRecievedRobotErrNum == 201)  //door closed, in load robot erea
                    {
                        stUnloadRobotControl.bDoorOpened = false;
                        stUnloadRobotControl.iErrNo = 0;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.UnloadDoorState, "0");
                    }
                    else if (tmpRecievedRobotErrNum == 203 || tmpRecievedRobotErrNum == 204)
                    {
                        stUnloadRobotControl.iErrNo = 402;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo]);
                    }
                    else if (tmpRecievedRobotErrNum == 205)  //Target out of Tray/Carrier range
                    {
                        stUnloadRobotControl.iErrNo = 403;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo]);
                    }
                    else if (tmpRecievedRobotErrNum == -405)  //Adept: "  illigle signal
                    {
                        stUnloadRobotControl.iErrNo = 404;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, (AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg));
                    }
                    else if (tmpRecievedRobotErrNum == -618)  //Adept
                    {
                        stUnloadRobotControl.iErrNo = 405;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                    }
                    else if (tmpRecievedRobotErrNum == -901)  //Adept
                    {
                        stUnloadRobotControl.iErrNo = 406;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                    }
                    else if (tmpRecievedRobotErrNum == -1002)  //Adept
                    {
                        stUnloadRobotControl.iErrNo = 407;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                    }
                    else if (tmpRecievedRobotErrNum == -1033)  //Adept
                    {
                        stUnloadRobotControl.iErrNo = 408;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo] + "; Adept: " + AdeptRecievedRobotErrMsg);
                    }     
                    else
                    {
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblRobotsMsg, "Unload robot error" + tmpRecievedRobotErrNum.ToString());
                    }
                    if ((tmpRecievedRobotErrNum != 200) && (tmpRecievedRobotErrNum != 201))  //not loging open/close door
                    {
                        AppGen.Inst.LogFile(stUnloadRobotControl.iErrNo.ToString() + "  " + AppGen.Inst.ErrorDescription.ErrorDescriptionEng[stUnloadRobotControl.iErrNo] + "  " + AdeptRecievedRobotErrMsg, LogType.GeneralErr, LogStation.UnloadRobot);
                        AppGen.Inst.RobotConnection.stUnloadRobotControl.ErrLoged = true;
                    }                  
                }
                else
                {
                    //GlobalErr "Error In Receive Message ""CommLoadRobot""."
                    RU.ConnSuccess = false;
                }

                RU.GetMsg = aux;
                AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, Color.LimeGreen, " ");
                AppGen.Inst.MDImain.frmRobots.ShowCommString(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, GetSend_str.ENUM_GET, aux);
                aux = "";
            }
        }
        ///
        public void ResetRobotComm(ROBOT_INDEXES robot)
        {
            switch (robot)
            {
                case ROBOT_INDEXES.ENUM_LOAD_ROBOT:
                    RL.ConnBusy = false;
                    RL.ConnSuccess = false;
                    stLoadRobotControl.bDoorOpened = false;
                    AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_LOAD_ROBOT, Color.LimeGreen, "");
                    AppGen.Inst.MDImain.frmRobots.ShowCommString(ROBOT_INDEXES.ENUM_LOAD_ROBOT, GetSend_str.ENUM_CLEAR, "");
                    break;
                case ROBOT_INDEXES.ENUM_UNLOAD_ROBOT:
                    RU.ConnBusy = false;
                    RU.ConnSuccess = false;
                    stUnloadRobotControl.bDoorOpened = false;
                    AppGen.Inst.MDImain.frmRobots.SetBackColorComm(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, Color.LimeGreen, "");
                    AppGen.Inst.MDImain.frmRobots.ShowCommString(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, GetSend_str.ENUM_CLEAR, "");
                    break;
            }
        }

        private void GetRobotPoints(string[] strArg)  //from cmd_41, recieve all robots points(calib and origen)          
        {
            int index = Convert.ToInt16(strArg[1]);
            switch (index)
            {
                case 0:
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[0].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[0].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[0].X = AppGen.Inst.VisionParam.LoadTrayCalibPt[0].X;
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[0].Y = AppGen.Inst.VisionParam.LoadTrayCalibPt[0].Y;
                    break;
                case 1:
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[1].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[1].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[1].X = AppGen.Inst.VisionParam.LoadTrayCalibPt[1].X;
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[1].Y = AppGen.Inst.VisionParam.LoadTrayCalibPt[1].Y;
                    break;
                case 2:
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[2].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[2].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[2].X = AppGen.Inst.VisionParam.LoadTrayCalibPt[2].X;
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[2].Y = AppGen.Inst.VisionParam.LoadTrayCalibPt[2].Y;
                    break;
                case 3:
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[3].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadTrayCalibPt[3].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[3].X = AppGen.Inst.VisionParam.LoadTrayCalibPt[3].X;
                    AppGen.Inst.AppSettings.AppSetLoadTrayCalibPt[3].Y = AppGen.Inst.VisionParam.LoadTrayCalibPt[3].Y;
                    break;
                case 4:
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[0].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[0].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadCarrierCalibPt[0] = AppGen.Inst.VisionParam.LoadCarrierCalibPt[0];  
                    break;
                case 5:
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[1].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[1].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadCarrierCalibPt[1] = AppGen.Inst.VisionParam.LoadCarrierCalibPt[1];  
                    break;
                case 6:
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[2].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[2].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadCarrierCalibPt[2] = AppGen.Inst.VisionParam.LoadCarrierCalibPt[2]; 
                    break;
                case 7:
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[3].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.LoadCarrierCalibPt[3].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetLoadCarrierCalibPt[3] = AppGen.Inst.VisionParam.LoadCarrierCalibPt[3];  
                    break;
                case 8:
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[0].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[0].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadTrayCalibPt[0] = AppGen.Inst.VisionParam.UnloadTrayCalibPt[0]; 
                    break;
                case 9:
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[1].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[1].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadTrayCalibPt[1] = AppGen.Inst.VisionParam.UnloadTrayCalibPt[1]; 
                    break;
                case 10:
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[2].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[2].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadTrayCalibPt[2] = AppGen.Inst.VisionParam.UnloadTrayCalibPt[2]; 
                    break;
                case 11:
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[3].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadTrayCalibPt[3].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadTrayCalibPt[3] = AppGen.Inst.VisionParam.UnloadTrayCalibPt[3];  
                    break;
                case 12:
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[0].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[0].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadCarrierCalibPt[0] = AppGen.Inst.VisionParam.UnloadCarrierCalibPt[0];  
                    break;
                case 13:
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[1].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[1].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadCarrierCalibPt[1] = AppGen.Inst.VisionParam.UnloadCarrierCalibPt[1];  
                    break;
                case 14:
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[2].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[2].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadCarrierCalibPt[2] = AppGen.Inst.VisionParam.UnloadCarrierCalibPt[2]; 
                    break;
                case 15:
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[3].X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.UnloadCarrierCalibPt[3].Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetUnloadCarrierCalibPt[3] = AppGen.Inst.VisionParam.UnloadCarrierCalibPt[3];  
                    break;
                case 16:
                    AppGen.Inst.VisionParam.xyLoadWorldTrayOrigin.X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.xyLoadWorldTrayOrigin.Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetxyLoadWorldTrayOrigin = AppGen.Inst.VisionParam.xyLoadWorldTrayOrigin; 
                    break;
                case 17:
                    AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin.X = Convert.ToDouble(strArg[2]);
                    AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin.Y = Convert.ToDouble(strArg[3]);
                    AppGen.Inst.AppSettings.AppSetxyUnloadWorldTrayOrigin = AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin; 

                    AppGen.Inst.AppSettings.Serialize();
                    break;
            }
        }




        // members (משתנים מקומיים)

        private SerialPort commLoadRobot;
        private SerialPort commUnloadRobot;

        // properties (משתנים שניגשים אליהם מבחוץ)
        //public int Speed { get; set; }   just example..

        public RobotCon RL { get; set; }
        public RobotCon RU { get; set; }
        public RobotControl stLoadRobotControl { get; set; }
        public RobotControl stUnloadRobotControl { get; set; }
        public Single sngTmpX_origin;
        public Single sngTmpY_origin;
        public int iLastLoadInsIndex;                    //'   an integer stating the last insert index taken from the load service tray
        public int iPlaceIndexAtCarrierEnd;              //'   an integer stating the last insert index taken from unload the carrier tray
        public int iLastUnloadPlaceIndex;                //'   an integer stating the last insert index to place on the unload service tray
        public bool bLoadResRecieved;                    //'   a flag stating that a load response was recieved from the load robot    (either insert was loaded properly or not)
        public bool bUnloadResRecieved;                  //'   a flag stating that a unload response was recieved from the unload robot (either insert was unloaded properly or not)
        public bool bResetLoadRobRequested;
        public bool StopCycleTest;
        public bool sendToMeasureFl;
        public bool sendToPickFl;
        public int target;
        //public int iMissedUnloadCount;
        //public int StopAfterLostIns;                     //   'for unload cycle
        //public int MissedContinueslyLimit;               //   'for unload cycle
        //public int TotalUnloadMissed;
        public Single sngCurrentHeight;
        public Single sngCurrentHeight2;

    } // End of RobotConnection Class *************
} // End of namespace Stahli2Robots *************
