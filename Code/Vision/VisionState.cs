using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stahli2Robots
{
    public class VisionState
    {
        public VisionState()  //constractor
        {
        sErrMsg = "";
        iErrNo = 0;
        bError = false;
        bMsgDisplayed = false;
        }

        // properties (משתנים שניגשים אליהם מבחוץ)
        public string sErrMsg;
        public int iErrNo;
        public bool bError;
        public bool bMsgDisplayed;    
    }
}
