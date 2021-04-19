using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Stahli2Robots
{
    [XmlInclude(typeof(OrderParams))]
    public class OrderParams    // class holding all parameters that needs to be save per Order
    {
        public OrderParams()   //constructor
        {
        }
        public void Init()
        {
            InsertCode = "";
            InsertDescription = "";
            ProductCode = "";
            InsertSymetry =0;
            InsertHeight = 0;
            SensorHeight = 5;
            InsertHoleDiameter = 0;
            GripperCode = 0;
            ServiceLoadTrayName = "";
            ServiceUnloadTrayName = "";
            CarrierName = "";
            Cam1_Brightness = 0.0;
            Cam2_Brightness = 0.0;
            Cam3_Brightness = 0.0;
            Cam1_Contrast = 0.0;
            Cam2_Contrast = 0.0;
            Cam3_Contrast = 0.0;
            Cam1_Trashhold = 0.0;
            Cam2_Trashhold = 0.0;
            Cam3_Trashhold = 0.0;
            Cam1_Score = 0.0;
            Cam2_Score = 0.0;
            Cam3_Score = 0.0;
            Cam1_Angle = 0.0;
            Cam3_Angle = 0.0;
        }
        private void CopyData(OrderParams other)
        {
            InsertCode = other.InsertCode;
            InsertDescription = other.InsertDescription;
            ProductCode = other.ProductCode;
            InsertSymetry = other.InsertSymetry;
            InsertHeight = other.InsertHeight;
            SensorHeight = other.SensorHeight;           
            InsertHoleDiameter = other.InsertHoleDiameter;
            GripperCode = other.GripperCode;
            ServiceLoadTrayName = other.ServiceLoadTrayName;
            ServiceUnloadTrayName = other.ServiceUnloadTrayName;
            CarrierName = other.CarrierName;
            Cam1_Brightness = other.Cam1_Brightness;
            Cam2_Brightness = other.Cam2_Brightness;
            Cam3_Brightness = other.Cam3_Brightness;
            Cam1_Contrast = other.Cam1_Contrast;
            Cam2_Contrast = other.Cam2_Contrast;
            Cam3_Contrast = other.Cam3_Contrast;
            Cam1_Trashhold = other.Cam1_Trashhold;
            Cam2_Trashhold = other.Cam2_Trashhold;
            Cam3_Trashhold = other.Cam3_Trashhold;
            Cam1_Score = other.Cam1_Score;
            Cam2_Score = other.Cam2_Score;
            Cam3_Score = other.Cam3_Score;
            Cam1_Angle = other.Cam1_Angle;
            Cam3_Angle = other.Cam3_Angle;
        }

        public void Serialize(string OrderName)
        {
            //AppGen.Inst.XMLSerialize.Serialize(this, @"C:\PROJECTS\Stahli.Net\Bin\Debug\Orders\", OrderName, true);
            AppGen.Inst.XMLSerialize.Serialize(this, System.IO.Directory.GetCurrentDirectory() + "\\Orders\\", OrderName, true);
        }
        public void DeSerialize(string OrderName)
        {        
            //CopyData((OrderParams)AppGen.Inst.XMLSerialize.DeSerialize(this, @"C:\PROJECTS\Stahli.Net\Bin\Debug\Orders\", OrderName, false));
            CopyData((OrderParams)AppGen.Inst.XMLSerialize.DeSerialize(this, System.IO.Directory.GetCurrentDirectory() + "\\Orders\\", OrderName, false));
        }

        // members
        public string InsertCode { get; set; }
        public string InsertDescription { get; set; }
        public string ProductCode { get; set; }
        public int InsertSymetry { get; set; }
        public double InsertHeight { get; set; }
        public double SensorHeight { get; set; }     
        public double InsertHoleDiameter { get; set; }
        public int GripperCode { get; set; }
        public string ServiceLoadTrayName { get; set; }
        public string ServiceUnloadTrayName { get; set; }
        public string CarrierName { get; set; }
        public double Cam1_Brightness { get; set; }
        public double Cam2_Brightness { get; set; }
        public double Cam3_Brightness { get; set; }
        public double Cam1_Contrast { get; set; }
        public double Cam2_Contrast { get; set; }
        public double Cam3_Contrast { get; set; }
        public double Cam1_Trashhold { get; set; }
        public double Cam2_Trashhold { get; set; }
        public double Cam3_Trashhold { get; set; }
        public double Cam1_Score { get; set; }
        public double Cam2_Score { get; set; }
        public double Cam3_Score { get; set; }
        public double Cam1_Angle { get; set; }
        public double Cam3_Angle { get; set; }

    }
}
