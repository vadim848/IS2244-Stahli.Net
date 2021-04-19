using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stahli2Robots
{
    public class Calculate
    {
        //asaf
        internal void CalcSearchRadiusReg(out double RadiusSize)
        {
            RadiusSize = ((double.Parse(AppGen.Inst.LoadCarrier.Cellsize.Substring(0, 2)))/2);
        }
        internal void CalcSearchReg(out double ySize, out double xSize)
        {
            ySize = double.Parse(AppGen.Inst.LoadTray.Cellsize.Substring(0, 2));
            xSize = double.Parse(AppGen.Inst.LoadTray.Cellsize.Substring(3, 2));
        }
        public bool ReturnDistance(ref double distance, double x1, double y1, double x2, double y2)
        {
            const double delta = 5;
            double calcMidlePoint;
            double calcDistance = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            if (distance < calcDistance + delta && calcDistance < distance + delta)
            {              
                distance = calcDistance;
                return true;
            }
            else
                return false;
        }

        public double GetRadius(double zeroX, double zeroY, double coupleMildleX, double coupleMildleY)
        {
            return Math.Sqrt((zeroX - coupleMildleX) * (zeroX - coupleMildleX) +
                (zeroY - coupleMildleY) * (zeroY - coupleMildleY));
        }

        public double GetAngle(double coupleMildleX, double coupleMildleY, double midlePointX, double midlePointY)
        {
            CompensateShfit(ref coupleMildleX, ref coupleMildleY, midlePointX, midlePointY);
            double radAngle = Math.Atan((coupleMildleY) / (coupleMildleX));
            if (coupleMildleX > 0 && coupleMildleY > 0) //first quarter
                return (radAngle * (180 / Math.PI));
            else if (coupleMildleX > 0 && coupleMildleY < 0) //fourth quarter
                return (radAngle * (180 / Math.PI) + 360);
            else
                return (radAngle * (180 / Math.PI) + 180);
        }

        public void CompensateShfit(ref double coupleMildleX, ref double coupleMildleY, double midlePointX, double midlePointY)
        {
            coupleMildleX = coupleMildleX - midlePointX;
            coupleMildleY = coupleMildleY - midlePointY;
        }

        public bool CheckCarrierSide(double? Angle1_1, double? Angle1_2, double? Angle2_1, double? Angle2_2,
            double? Angle3_1, double? Angle3_2,double? Angle4_1, double? Angle4_2, ref double? Rotation)
        {
            double? angle1 = null, angle2 = null, angle3 = null, angle4 = null, calcDistance = null;
            if (Angle1_1 != null)
            {
                if (Math.Abs((decimal)(Angle1_1 - Angle1_2)) < 20)
                    angle1 = (Angle1_1 < Angle1_2) ? Angle1_2 : Angle1_1;
                else
                    angle1 = (Angle1_1 > Angle1_2) ? Angle1_2 : Angle1_1;
                Rotation = (double)(angle1 - 5);
            }
            if (Angle2_1 != null)
            {
                if (Math.Abs((decimal)(Angle2_1 - Angle2_2)) < 20)
                    angle2 = (Angle2_1 < Angle2_2) ? Angle2_2 : Angle2_1;
                else
                    angle2 = (Angle2_1 > Angle1_2) ? Angle2_2 : Angle2_1;
                Rotation = (double)(angle2 - 100);
            }
            if (Angle3_1 != null)
            {
                if (Math.Abs((decimal)(Angle3_1 - Angle3_2)) < 20)
                    angle3 = (Angle3_1 < Angle3_2) ? Angle3_2 : Angle3_1;
                else
                    angle3 = (Angle3_1 > Angle3_2) ? Angle3_2 : Angle3_1;
                Rotation = (double)(angle3 - 195);
            }
            if (Angle4_1 != null)
            {
                if (Math.Abs((decimal)(Angle4_1 - Angle4_2)) < 25)  //japan, was <20
                    angle4 = (Angle4_1 < Angle4_2) ? Angle4_2 : Angle4_1;
                else
                    angle4 = (Angle4_1 > Angle4_2) ? Angle4_2 : Angle4_1;
                Rotation = (double)(angle4 - 290);  //japan to remark if 4 hole not working
            }
            if (angle1 != null)
            {
                if (angle2 != null)
                {
                    calcDistance = angle2 - angle1;
                    if (calcDistance < 0) calcDistance += 360;
                    return (calcDistance < 100 && calcDistance > 90) ? true : false;
                }
                else if (angle3 != null)
                {
                    calcDistance = angle3 - angle1;
                    if (calcDistance < 0) calcDistance += 360;
                    return (calcDistance > 185 && calcDistance < 195) ? true : false;
                }
                else if (angle4 != null)
                {
                    calcDistance = angle4 - angle1;
                    if (calcDistance < 0) calcDistance += 360;
                    return (calcDistance > 270 && calcDistance < 290) ? true : false;
                }
            }
            if (angle2 != null)
            {
                if (angle3 != null)
                {
                    calcDistance = angle3 - angle2;
                    if (calcDistance < 0) calcDistance += 360;
                    return (calcDistance < 100 && calcDistance > 90) ? true : false;
                }
                if (angle4 != null)
                {
                    calcDistance = angle4 - angle2;
                    if (calcDistance < 0) calcDistance += 360;
                    return (calcDistance < 200 && calcDistance > 180) ? true : false;
                }
            }
            if (angle3 != null)
            {
                if (angle4 != null)
                {
                    calcDistance = angle4 - angle3;
                    if (calcDistance < 0) calcDistance += 360;
                    return (calcDistance < 110 && calcDistance > 90) ? true : false;
                }
            }
            return false;
        }

        public double FindPolarAngle(PointCoord Point)   //Finding the Angle of unload tray coords(without camera) 
        {
            double dblX, dblY, dTheta;
            try
            {
                dTheta = 0;
                dblX = Point.X;
                dblY = Point.Y;

                //in tray (unlike carrier, we calculate only the positive quter)
                if ((dblX > 0) && (dblY >= 0))
                {
                    dTheta = Math.Atan(dblY / dblX);
                }
                else if ((dblX > 0) && (dblY < 0))
                {
                    dTheta = Math.Atan(dblY / dblX) + 2 * Math.PI;
                }
                else if (dblX < 0)
                {
                    dTheta = Math.Atan(dblY / dblX) + Math.PI;
                }
                else if ((dblX == 0) && (dblY > 0))
                {
                    dTheta = Math.PI / 2;
                }
                else if ((dblX == 0) && (dblY < 0))
                {
                    dTheta = 3 * Math.PI / 2;
                }
                return dTheta;
            }
            catch { return 0; }
        }

        public void RotateCoordByAlfa(ref double Pt_x, ref double Pt_y, double Alfa)   //for unload tray coords(without camera)
        {           
            //this function take ONE point and rotate it by requested alfa
            double dr;
            double dTheta; //specifiec index angle (switching from Cartezian coord sys to Polar coord sys)
            //alfa, is the all tray angle
            try
            {
                 dr = Math.Sqrt(Math.Pow(Pt_x, 2) + Math.Pow(Pt_y, 2));  //  dr=(Pt_x^2+Pt_y^2)^0.5
                 PointCoord tmpPoint = new PointCoord();
                 tmpPoint.X = Pt_x;
                 tmpPoint.Y = Pt_y;
                 dTheta = FindPolarAngle(tmpPoint);

                //back to Cartezian coord sys (with the added angle(alfa))
                 Pt_x = dr * Math.Cos(Alfa + dTheta);
                 Pt_y = dr * Math.Sin(Alfa + dTheta); 
            }
            catch { }
        }
    }
}
