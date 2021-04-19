using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stahli2Robots
{
    public class BasicCycle
    {
        public BasicCycle()
        {
            // constructor
        }
        public volatile bool bStart;                                  //   flag stating the cycle's start
        public volatile bool bRun;                                    //   flag stating that the cycle is currently running
        public volatile bool bPause;                                  //   flag stating that the cycle is paused
        public volatile bool bError;                                  //   cycle error flag
        public volatile bool bRunInit;                                //   init sequence run request issued
        public volatile bool bInitDone;                               //   initialize process done flag
        public volatile bool bRunSetup;                               //   setup sequence run request issued
        public volatile bool bSetupDone;                              //   setup process done flag  (setup Tray)
        public volatile bool bSetupDone2;                             //   setup process done flag  (setup conveyore)
        public volatile bool bStepByStep;                             //   flag stating cycle will be performed step by step
        public volatile bool bMask;                                   //   a flag stating the this cycle is being masked
        public volatile bool bDone;                                   //   a flag stating that the cycle was done successfully
        public volatile bool bRobotActive;                            //   a flag stating that roobot is active
        public volatile bool bMsgDisplayed;                           //   a flag stating that the most recent massage had been displayed       
        public volatile bool ErrLoged;                                // flag to Log every error only once
        public int iErrNo { get; set; }                               //   cycle's error number
        public int iStep { get; set; }                                //   integer holding the cycle's step
        public int iFaultStep { get; set; }                           //   integer holding the cycle's fault step
        //public double lTimeSample { get; set; }                       //   a curtain cycle time sample
        public double lTimeOutLimit { get; set; }                     //   the maximum step time allowed
        public string sErrMsg { get; set; }                           //   cycle's error msg   
    }
}
