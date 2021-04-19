using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stahli2Robots
{
    public class RobotControl  //a struct defining the robot control variables
    {      
        public bool bError;          //   cycle error flag
        public bool bRunInit;        //init sequence run request issued
        public bool bSetupDone;       //initialize process done flag
        //public string sErrMsg;       //cycle's error msg
        public int iErrNo;            //cycle's error number
        public bool bMsgDisplayed;   //a flag stating that the most recent massage had been displayed
        public bool bDone;           // a flag stating that robot had done its latest job
        public bool bRobPicked;      //a flag stating that the robot had just picked the target
        public bool bRobPlaced;      //a flag stating that the robot had just placed the target
        public bool bDoorOpened;     //this flag states that robot door is opened and that robot is not listening to the comm. port
        public bool AutoModeFl;      // indicate if robot in Auto mode (by cmd70)
        public bool ErrLoged;         // flag for log error only once on chkerr timer
    }
}
