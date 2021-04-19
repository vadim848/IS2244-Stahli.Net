using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stahli2Robots
{
    public class VisionParam
    {
    public VisionParam()
        {           
            LoadTrayCalibPt.Add(new PointCoord());
            LoadTrayCalibPt.Add(new PointCoord());
            LoadTrayCalibPt.Add(new PointCoord());
            LoadTrayCalibPt.Add(new PointCoord());

            LoadCarrierCalibPt.Add(new PointCoord());
            LoadCarrierCalibPt.Add(new PointCoord());
            LoadCarrierCalibPt.Add(new PointCoord());
            LoadCarrierCalibPt.Add(new PointCoord());

            UnloadTrayCalibPt.Add(new PointCoord());
            UnloadTrayCalibPt.Add(new PointCoord());
            UnloadTrayCalibPt.Add(new PointCoord());
            UnloadTrayCalibPt.Add(new PointCoord());

            UnloadCarrierCalibPt.Add(new PointCoord());
            UnloadCarrierCalibPt.Add(new PointCoord());
            UnloadCarrierCalibPt.Add(new PointCoord());
            UnloadCarrierCalibPt.Add(new PointCoord());
        }    
   // properties (משתנים שניגשים אליהם מבחוץ)
    private bool snap_req_fl1;       // flag activate by timer and cmd15 to insure of one shot only every cycle (Robot 1)
    public bool SNAP_req_fl1
    {
        get
        {
            lock ((object)snap_req_fl1)
            {
                return snap_req_fl1;
            }
        }
        set
        {
            lock ((object)snap_req_fl1)
            {
                snap_req_fl1 = value;
            }
        }
    }

    private bool snap_req_fl2;       // flag activate by timer and cmd15 to insure of one shot only every cycle (Robot 1)
    public bool SNAP_req_fl2
    {
        get
        {
            lock ((object)snap_req_fl2)
            {
                return snap_req_fl2;
            }
        }
        set
        {
            lock ((object)snap_req_fl2)
            {
                snap_req_fl2 = value;
            }
        }
    }

    private bool snap_req_fl3;       // flag activate by timer and cmd15 to insure of one shot only every cycle (Robot 2)
    public bool SNAP_req_fl3
    {
        get
        {
            lock ((object)snap_req_fl3)
            {
                return snap_req_fl3;
            }
        }
        set
        {
            lock ((object)snap_req_fl3)
            {
                snap_req_fl3 = value;
            }
        }
    }

    public bool LastTimerBit1;      // flag showing last(timer tik) bit status of snap request from robot1
    public bool LastTimerBit2;      // flag showing last(timer tik) bit status of snap request from robot1
    public bool LastTimerBit3;      // flag showing last(timer tik) bit status of snap request from robot2

    public bool bLiveLoadSnap;         //not relevant?? 'perform load camera live snap (preforming 1st live frame - after that searching on old frame )
    public bool bLiveUnloadSnap;      //not relevant?? 

    public double LoadCarrAddedX;     // Angle added by user to place insert at the right orientation (at Load carrier)
    public double LoadCarrAddedY;     // Angle added by user to place insert at the right orientation (at Load carrier)
    public double LoadAddedAngle;     // Angle added by user to place insert at the right orientation (at Load carrier)
    
    public double UnloadAddedAngle;   // Angle added by user to place insert at the right orientation (at Unload tray)
    public double OriginXOffset;      // X offset added by user to place insert at the right orientation (at Unload tray)
    public double OriginYOffset;      // Y offset added by user to place insert at the right orientation (at Unload tray)

    public PointCoord xyLoadWorldTrayOrigin = new PointCoord();
    public PointCoord xyUnloadWorldTrayOrigin = new PointCoord();
    //public PointCoord xyUnloadWorldTrayOriginRotated = new PointCoord();
        
    public List<PointCoord> LoadTrayCalibPt = new List<PointCoord>();
    public List<PointCoord> LoadCarrierCalibPt = new List<PointCoord>();
    public List<PointCoord> UnloadTrayCalibPt = new List<PointCoord>();
    public List<PointCoord> UnloadCarrierCalibPt = new List<PointCoord>();

   
    }
}
