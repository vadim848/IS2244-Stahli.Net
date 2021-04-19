using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows;

namespace Stahli2Robots
{
    public class Tray
    {
        public Tray()   // constractor
	    {
            ResetData();       
	    }

        public void ResetData()
        {
            filePathTXT = System.IO.Directory.GetCurrentDirectory() + "\\TrayFiles\\";
            filePathPHW = System.IO.Directory.GetCurrentDirectory() + "\\TrayFiles\\";
            IndexList = new List<TrayIndexData>();
            CurrIndex = 0;
            InsertCountAtServiceRow = 0;
            TrayAlfa = 0;
        }

        public void ResetUnfoundeInsertList()
        {
            for (int ii = 0; ii < IndexList.Count-1; ii++)
            {
                IndexList[ii].IsFound = true;
            }
        }



        public void ReadFromFile(string Tray)
        {
            const int indexPlace = 0;
            const int row = 2;
            const int col = 3;
            const int xPlace = 4;
            const int yPlace = 5;
            const int startLine = 5;

            try
            {
                string path = "";
                if (Tray == "LoadTray")
                {
                    path = filePathTXT + AppGen.Inst.OrderParams.ServiceLoadTrayName;
                }
                else
                {
                    path = filePathTXT + AppGen.Inst.OrderParams.ServiceUnloadTrayName;
                }
                
                if (!System.IO.File.Exists(path))
                {
                    System.Windows.Forms.MessageBox.Show("TRAY File not found");
                    return;
                }

                string[] lines = System.IO.File.ReadAllLines(path);
                InsertCountAtServiceRow = 0; 
                for (int ii = startLine; ii < lines.Length; ii++)
                {
                    string line = lines[ii];
                    string[] parts = line.Split(("").ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    TrayIndexData trayIndexData = new TrayIndexData();
                    trayIndexData.Index = int.Parse(parts[indexPlace]);
                    trayIndexData.RowNum = int.Parse(parts[row]);
                    trayIndexData.CollumnNum = int.Parse(parts[col]);
                    trayIndexData.X_file = double.Parse(parts[xPlace]);
                    trayIndexData.Y_file = double.Parse(parts[yPlace]);
                    IndexList.Add(trayIndexData);
                    if (trayIndexData.CollumnNum > InsertCountAtServiceRow) InsertCountAtServiceRow = trayIndexData.CollumnNum;            
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
                    string strTemp = (AppGen.Inst.OrderParams.ServiceLoadTrayName.TrimEnd('t','x','t'));
                    strTemp = strTemp + "phw";
                    //string path = filePathPHW + AppGen.Inst.OrderParams.ServiceTrayName;                   
                    using (StreamReader sr = new StreamReader(filePathPHW + strTemp))
                    {
                        cellsize = sr.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "No .phw file" );
                    return null;
                }
                return (cellsize);
            }
            //to do: check condition
            set { cellsize = value; }
        }

        //members:
        //public List<double> yAxis = new List<double>();
        private string filePathTXT = "", filePathPHW = "";        
        public List<TrayIndexData> IndexList;
        public int CurrIndex;
        public int InsertCountAtServiceRow;
        public double TrayAlfa; 
    }
}
