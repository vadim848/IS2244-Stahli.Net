using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows;
using System.IO;

namespace Stahli2Robots
{
    public class Carrier
    {
        public Carrier() // constractor
        {
            ResetData();
            ////filePath = @"C:\PROJECTS\Stahli.Net\Bin\Debug\CarrierFiles\";
            //filePath = System.IO.Directory.GetCurrentDirectory() + "\\CarrierFiles\\";
            //IndexList = new List<CarrierIndex>();

            //CurrIndex = 0;
            //NoOfInsAtCircle = new int[5];
            //CircleRadius = new double[5];
            //NoOfCircles = 0;
            

            ////Init Values:
            //NoOfInsAtCircle[1] = 0;       //how many inserts in first cycle
            //NoOfInsAtCircle[2] = 0;       //how many inserts in second cycle
            //NoOfInsAtCircle[3] = 0;       //how many inserts in 3rd cycle
            //NoOfCircles = 0;              //No of cycles
            //CircleRadius[1] = 0;          //radius of first cycle
            //CircleRadius[2] = 0;          //radius of second cycle
            //CircleRadius[3] = 0;          //radius of 3rd cycle
            //LastCircleNo = 0;
        }

        public void ResetData()
        {
            filePath = System.IO.Directory.GetCurrentDirectory() + "\\CarrierFiles\\";
            filePathPHW = System.IO.Directory.GetCurrentDirectory() + "\\CarrierFiles\\";
            IndexList = new List<CarrierIndex>();
            CurrIndex = 0;
            NoOfInsAtCircle = new int[10];
            CircleRadius = new double[10];
            NoOfCircles = 0;


            //Init Values:
            NoOfInsAtCircle[1] = 0;       //how many inserts in first cycle
            NoOfInsAtCircle[2] = 0;       //how many inserts in second cycle
            NoOfInsAtCircle[3] = 0;       //how many inserts in 3rd cycle
            NoOfCircles = 0;              //No of cycles
            CircleRadius[1] = 0;          //radius of first cycle
            CircleRadius[2] = 0;          //radius of second cycle
            CircleRadius[3] = 0;          //radius of 3rd cycle
            LastCircleNo = 0;
        }

        public void ResetMissedList()
        {
            for (int ii = 0; ii < IndexList.Count - 1; ii++)
            {
                IndexList[ii].VisionEmptyPocket = false;
            }
        }
               
        public void ReadFromFile()
        {
            const int Cir = 1;
            const int Pos = 2;
            const int xPlace = 3;
            const int yPlace = 4;
            const int Angle = 5;
            const int startLine = 6;
          
            try
            {
                string path = filePath + AppGen.Inst.OrderParams.CarrierName;
                if (!System.IO.File.Exists(path))
                {
                    System.Windows.Forms.MessageBox.Show("CARRIER File not found");
                    return;
                }

                string[] lines = System.IO.File.ReadAllLines(path);
                IndexList.Clear();
                CarrierIndex carrierIndex;

                for (int ii = startLine; ii < lines.Length ; ii++)
                {
                    string line = lines[ii];
                    if (line == "") {break;}  //end of carrier
                    string[] parts = line.Split(("").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    carrierIndex = new CarrierIndex();
                    carrierIndex.Master.X = double.Parse(parts[xPlace]);
                    carrierIndex.Master.Y = double.Parse(parts[yPlace]);
                    carrierIndex.Master.Cir = int.Parse(parts[Cir]);
                    carrierIndex.Master.Pos = int.Parse(parts[Pos]);
                    carrierIndex.Master.Angle = double.Parse(parts[Angle]);

                    IndexList.Add(carrierIndex);

                    if (LastCircleNo < carrierIndex.Master.Cir)
                    {
                        //start Old
                       // CircleRadius[carrierIndex.Master.Cir] = carrierIndex.Master.X;    //first index in circle is the radius (x=radius, y=0)
                        //end old
                        //start New
                        CircleRadius[carrierIndex.Master.Cir] = Math.Sqrt(Math.Pow(carrierIndex.Master.X, 2) + Math.Pow(carrierIndex.Master.Y, 2));    //first index in circle is the radius of srqt of Y and Y <<new>>
                        //end New

                    }
                    LastCircleNo = carrierIndex.Master.Cir;
                    NoOfCircles = carrierIndex.Master.Cir;
                    NoOfInsAtCircle[carrierIndex.Master.Cir] += 1;    //how many inserts in each cycle


                    //if (ii == startLine)           //first index is the radius (x=radius, y=0)
                    //{
                    //    CircleRadius[1] = carrierIndex.Master.X;
                    //}
                   // CircleRadius[1] = Math.Max(1,2);
                    //if (CurrCycle < carrierIndex.Master.Cir)
                    //{
                    //    CircleRadius[CurrCycle] = carrierIndex.Master.X;
                    //}
                    //CurrCycle = carrierIndex.Master.Cir;
                    //NoOfCircles = Math.Max(NoOfCircles, carrierIndex.Master.Cir);

                    //////////////////////////////////
                    //tbd: read from file!!!:
                    //NoOfInsAtCircle[1] = 15;      //how many inserts in first cycle
                    //NoOfInsAtCircle[2] = 8;       //how many inserts in second cycle

                    //NoOfCircles = 2;              //No of circles

                    //CircleRadius[1] = 86.9;       //radius of first cycle
                    //CircleRadius[2] = 50.1;       //radius of second cycle
                    //////////////////////////////////
                }
            }
            catch { }
        }

        private string cellsize;
        public string Cellsize
        {
            get
            {
                try
                {
                    //word.TrimEnd(charsToTrim)
                    string strTemp = (AppGen.Inst.OrderParams.CarrierName.TrimEnd('t', 'x', 't'));
                    strTemp = strTemp + "phw";
                    //string path = filePathPHW + AppGen.Inst.OrderParams.ServiceTrayName;                   
                    using (StreamReader sr = new StreamReader(filePathPHW + strTemp))
                    {
                        cellsize = sr.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "No .phw file");
                    return null;
                }
                return (cellsize);
            }
            //to do: check condition
            set { cellsize = value; }
        }


        public void SetRotate(double? angle)  //same value different index not working!! only first one!!!
        {
            //If Not fMainForm.optVisionProOn Then Exit Sub 

            int CircleIndex, j, iInsIndx;
            double dblX , dblY;          
            double dTheta = 0;   // Theta = ArcCos(X/r) - is the initial angle (in polar representation) of the current insert coordinate [rad]
            angle = angle * Math.PI / 180;   //present angle in Radian mode
        //    angle = angle + Math.PI;  //tbd: delet added PI..!!   reading carrier file is ot good??  2nd circle like upside down..
            iInsIndx = 0;
            for ( CircleIndex = 1; CircleIndex <= NoOfCircles; CircleIndex++)
	        {
                for (j = 0; j <= NoOfInsAtCircle[CircleIndex]-1; )
                {
                    dblX = IndexList[iInsIndx].Master.X;
                    dblY = IndexList[iInsIndx].Master.Y;

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
                    if (angle != null)
                    {
                        IndexList[iInsIndx].Rotate.X = Math.Round(CircleRadius[CircleIndex] * Math.Cos(dTheta + (double)angle), 3);
                        IndexList[iInsIndx].Rotate.Y = Math.Round(CircleRadius[CircleIndex] * Math.Sin(dTheta + (double)angle), 3);
                        IndexList[iInsIndx].Rotate.Angle = Math.Round((double)(IndexList[iInsIndx].Master.Angle / 180 * Math.PI + angle), 3);

                        IndexList[iInsIndx].Rotate.Cir = IndexList[iInsIndx].Master.Cir;
                        IndexList[iInsIndx].Rotate.Pos = IndexList[iInsIndx].Master.Pos;

                        iInsIndx = iInsIndx + 1;
                        j++;
                    }
                }
            }
        } 
        public void SetOffset(double CenterX, double CenterY)
        {
            int CircleIndex, j, iInsIndx;
            iInsIndx = 0;
            for (CircleIndex = 1; CircleIndex <= NoOfCircles; CircleIndex++)
            {
                for (j = 0; j <= NoOfInsAtCircle[CircleIndex] - 1; )
                {
                    IndexList[iInsIndx].Offset.X = IndexList[iInsIndx].Rotate.X + CenterX;
                    IndexList[iInsIndx].Offset.Y = IndexList[iInsIndx].Rotate.Y + CenterY;
                    IndexList[iInsIndx].Offset.Cir = IndexList[iInsIndx].Rotate.Cir;
                    IndexList[iInsIndx].Offset.Pos = IndexList[iInsIndx].Rotate.Pos;
                    IndexList[iInsIndx].Offset.Angle = IndexList[iInsIndx].Rotate.Angle;
                    iInsIndx = iInsIndx + 1;
                    j++;
                }
            }
        }

        // members
        public List<CarrierIndex> IndexList;
        public int CurrIndex;
        private int LastCircleNo;
        private string filePath, filePathPHW = ""; 

        public int NoOfInsAtTray
        {
            get
            {
                return IndexList.Count();
            }
        }                        // the number of available insert location within the carrier tray
        private int NoOfCircles { get; set; }               // the number of circles in the carrier tray
        private int[] NoOfInsAtCircle { get; set; }         // arbitrary max circle number of 10 as for now
        private double[] CircleRadius { get; set; }            // the radius [mm] of each carrier tray circle
    }
}
