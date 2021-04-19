using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stahli2Robots
{
    public class RobotCon // a struct holding all communication related variables
    {       
            public bool ConnBusy;
            public bool ConnSuccess;
            public string sLastSentCmd;
            public string CmdMsg;
            public string GetMsg;
            public SetupRobot cmd11Val;
            public double TimeOutCounter = 3000;
            public int ErrNo;
            public int iRobotNumber;
    }
}
