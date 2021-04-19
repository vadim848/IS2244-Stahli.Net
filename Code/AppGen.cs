using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Stahli2Robots
{
    public class AppGen
    {
        private AppGen() { }   //constructor (private becouse can be created only once)
        private void Init()
        {
            robotConnection = new RobotConnection();
            visionParam = new VisionParam();            
            calculate = new Calculate();
            mainCycle = new Stahli2Robots.MainCycle();
            errorDescription = new ErrorDescription();

////////////////////////////
            loadCarrier = new Carrier();
            loadTray = new Tray();
            unLoadTray = new Tray();     //no camera
            unLoadCarrier = new Tray();
            cam1 = new VisionState();
            cam2 = new VisionState();
            cam3 = new VisionState();
////////////////////////////
            
            xmlSerialize = new Stahli2Robots.XMLSerialize();
            appSetting = new AppSettings();
            orderParams = new OrderParams();

            appSetting.Init();
            orderParams.Init();
        }
        //---- 15.06.09 ----
        internal double newRotation;
        //---- -------- ----
        public int bLoadReciepe { get; set; }//added by noam
        public void OnInitApp()
        {
            mainCycle.OnInitApp();
        }
        public void OnClosingApp()
        {
            mainCycle.OnClosingApp();
        }

        private static AppGen inst = null;   
        public static AppGen Inst
        {
            get
            {
                if (inst == null)
                {
                    inst = new AppGen();
                    inst.Init();
                }
                return inst;
            }
        }

        // methods  ( AppGen -פונקציות פנימיות לשימושים כללים מקומיים ל-  )


        public void LogFile(string msg, LogType msgType, LogStation station = LogStation.General)  //if None, defult: LogStation.General
        {
            if (AppGen.inst.mdimain == null) return;
            string source_msg = "";
            if (string.IsNullOrEmpty(msg)) return;
            if ((msgType != LogType.SystemErr) && (!AppGen.inst.mdimain.chkErrLog.Checked)) return;
           
            try
            {
                if ((msgType == LogType.SystemErr) || (msgType == LogType.GeneralErr))  //if system error, then add where does the error came from (subroutine name)
                {
                    try
                    {
                        source_msg = "";
                        System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
                        System.Diagnostics.StackFrame sf = st.GetFrame(1);
                        source_msg = sf.GetMethod().DeclaringType.UnderlyingSystemType.Name + "()  " + sf.GetMethod().Name;
                    }
                    catch { }
                }

                string path = System.IO.Directory.GetCurrentDirectory() + "\\LogFiles\\" + msgType.ToString() + "\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                switch (msgType)
                {
                    case LogType.SystemErr:
                        path += msgType.ToString() + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"; 
                        if (!File.Exists(path))
                        {
                            var file = File.Create(path);
                            file.Close();
                            File.AppendAllText(path, "Source" + "," + "System Messege" + "," + "Time" + "\n");
                        }
                        msg = source_msg + "," + msg + "," + DateTime.Now.ToString("HH:mm");
                        File.AppendAllText(path, msg + "\n");
                        break;                  
                    case LogType.GeneralErr:
                        path += msgType.ToString() + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"; 
                        if (!File.Exists(path))
                        {
                            var file = File.Create(path);
                            file.Close();
                            File.AppendAllText(path, "Station" + "," + "Error Messege" + "," + "Source" + "," + "Time" + "\n");
                        }
                        //recieved msg is "station","error" 
                        msg = station + "," + msg + "," + source_msg + "," + DateTime.Now.ToString("HH:mm");
                        File.AppendAllText(path, msg + "\n");
                        break;     
                    case LogType.Production:
                        path += msgType.ToString() + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"; 
                        if (!File.Exists(path))
                        {
                            var file = File.Create(path);
                            file.Close();
                            File.AppendAllText(path, "Order Name" + "," + "Action" + "," + "  Machine State  " + "," + "Insert Counter" + "," + "Time" + "\n");                       
                        }
                        msg = AppGen.Inst.OrderParams.InsertCode.ToString() + "," + msg + "," + AppGen.inst.mainCycle.MainProccesState.ToString()  +  "," +  AppGen.inst.mainCycle.CounterPerOrder  + "," + DateTime.Now.ToString("HH:mm");
                        File.AppendAllText(path, msg + "\n");
                        break;
                    case LogType.InsertMeasure:
                        path += msgType.ToString() + DateTime.Now.ToString("dd-MM-yyyy") + ".csv"; 
                        if (!File.Exists(path))
                        {
                            var file = File.Create(path);
                            file.Close();
                            File.AppendAllText(path, "Order Name" + "," + "Measure Result"  + "," + "Insert Counter" + "," + "Time" + "\n");                       
                        }
                        msg = AppGen.Inst.OrderParams.InsertCode.ToString() + "," + msg + ","  +  AppGen.inst.mainCycle.CounterPerOrder  + "," + DateTime.Now.ToString("HH:mm");
                        File.AppendAllText(path, msg + "\n");
                        break;
                }
            }
            catch { Exception ex; }
        }

        // members

        private XMLSerialize xmlSerialize;
        public XMLSerialize XMLSerialize
        {
            get
            {
                return xmlSerialize;
            }
            set
            {
                xmlSerialize = value;
            }
        }

        private AppSettings appSetting;
        public AppSettings AppSettings
        {
            get
            {
                return appSetting;
            }
            set
            {
                appSetting = value;
            }
        }

        private OrderParams orderParams;
        public OrderParams OrderParams
        {
            get
            {
                return orderParams;
            }
            set
            {
                orderParams = value;
            }
        }
        
        private ErrorDescription errorDescription;
        public ErrorDescription ErrorDescription
        {
            get
            {
                return errorDescription;
            }
            set
            {
                errorDescription = value;
            }
        }
        
        private RobotConnection robotConnection;
        public RobotConnection RobotConnection
        {
            get
            {
                return robotConnection;
            }
        }

        private VisionParam visionParam;
        public VisionParam VisionParam
        {
            get
            {
                return visionParam;
            }
        }

        private Calculate calculate;
        public Calculate Calculate
        {
            get
            {
                return calculate;
            }
        }


        private MDImain mdimain;  //for delegate usage
        public MDImain MDImain
        {
            get
            {
                return mdimain;
            }
            set
            {
                mdimain = value;
            }
        }

        private MainCycle mainCycle;
        public MainCycle MainCycle
        {
            get
            {
                return mainCycle;
            }
        }

        private Carrier loadCarrier;
        public Carrier LoadCarrier
        {
            get
            {
                return loadCarrier;
            }
        }

        //private Carrier loadCarrier2;
        //public Carrier LoadCarrier2
        //{
        //    get
        //    {
        //        return loadCarrier2;
        //    }
        //}

        private Tray unLoadCarrier;
        public Tray UnLoadCarrier
        {
            get
            {
                return unLoadCarrier;
            }
        }

        private Tray loadTray;
        public Tray LoadTray
        {
            get
            {
                return loadTray;
            }
        }

        private Tray unLoadTray;
        public Tray UnLoadTray
        {
            get
            {
                return unLoadTray;
            }
        }


        private VisionState cam1;
        public VisionState Cam1
        {
            get
            {
                return cam1;
            }
        }
        private VisionState cam2;
        public VisionState Cam2
        {
            get
            {
                return cam2;
            }
        }
        private VisionState cam3;
        public VisionState Cam3
        {
            get
            {
                return cam3;
            }
        }



    }
}
