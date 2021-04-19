using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stahli2Robots
{
    public class CarrierIndex
    {
        public CarrierIndex() 
        {
            Master = new IndexData();
            Rotate = new IndexData();
            Offset = new IndexData();
            VisionRes = new IndexData();
            VisionEmptyPocket = false;
        }

        //public void SetRotate(double angle)
        //{
        //    Rotate.Angle += Master.Angle + angle + 3;
        //    Rotate.X += Master.X  + 5;
        //    Rotate.Y += Master.Y + 5;
        //}
        //public void SetOffset(double CenterX, double CenterY)
        //{
        //    Offset.X = Rotate.X + CenterX;
        //    Offset.Y = Rotate.Y + CenterY;
        //}

        // members
        public IndexData Master { get; set; }
        public IndexData Rotate { get; set; }
        public IndexData Offset { get; set; }
        public IndexData VisionRes { get; set; }
        public bool VisionEmptyPocket { get; set; }
        
        // more data for reprecent index life
    }
}
