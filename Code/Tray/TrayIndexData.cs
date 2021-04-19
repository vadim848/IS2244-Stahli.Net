using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Stahli2Robots
{
    public class TrayIndexData
    {
        public TrayIndexData()
        {
            Index = 0;
            IsFound = true;
        }
        public double Index { get; set; }
        public double X_file {get; set;}
        public double Y_file {get; set;}
        public double Angle_file { get; set; }
        public double X_VisRes { get; set; }
        public double Y_VisRes { get; set; }
        public double Angle_VisRes { get; set; }
        public int RowNum {get; set;}
        public int CollumnNum { get; set; }
        public bool IsFound { get; set; }
    }
}
