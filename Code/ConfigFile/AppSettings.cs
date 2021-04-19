using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace Stahli2Robots
{
    [XmlInclude(typeof(AppSettings))]
    public class AppSettings    // class holding all parameters that needs to be save at AppSetting file (use to be .ini file at old projects)
    {
        public AppSettings()   //constructor
        {          
        }
        public void Init()
        {
            CurrentInsertCode = "tmpOrderName";
            AppSetLoadAddedAngle = 0;
            AppSetUnloadAddedAngle = 0;
            AppSetLoadCarrAddedX = 0;
            AppSetLoadCarrAddedY = 0;
            AppSetOriginXOffset = 0;
            AppSetOriginYOffset = 0;
            AppSetxyLoadWorldTrayOrigin.X = 0;
            AppSetxyLoadWorldTrayOrigin.Y = 0;
            AppSetxyUnloadWorldTrayOrigin.X = 0;
            AppSetxyUnloadWorldTrayOrigin.Y = 0;
            
            AppSetLoadTrayCalibPt.Add(new PointCoord());
            AppSetLoadTrayCalibPt.Add(new PointCoord());
            AppSetLoadTrayCalibPt.Add(new PointCoord());
            AppSetLoadTrayCalibPt.Add(new PointCoord());
            AppSetLoadCarrierCalibPt.Add(new PointCoord());
            AppSetLoadCarrierCalibPt.Add(new PointCoord());
            AppSetLoadCarrierCalibPt.Add(new PointCoord());
            AppSetLoadCarrierCalibPt.Add(new PointCoord());
            AppSetUnloadTrayCalibPt.Add(new PointCoord());
            AppSetUnloadTrayCalibPt.Add(new PointCoord());
            AppSetUnloadTrayCalibPt.Add(new PointCoord());
            AppSetUnloadTrayCalibPt.Add(new PointCoord());
            AppSetUnloadCarrierCalibPt.Add(new PointCoord());
            AppSetUnloadCarrierCalibPt.Add(new PointCoord());
            AppSetUnloadCarrierCalibPt.Add(new PointCoord());
            AppSetUnloadCarrierCalibPt.Add(new PointCoord());

            AppSetUnloadCarrierSliceNo = 0;
            AppSetUnloadedInsCounter = 0;
            AppSetLoadedInsCounter = 0;
            AppSetCounterPerOrder = 0;          
            AppSetInsQnt = 0;
            AppSetTotalUnloadMissedCount = 0;
            AppSetMissPocketTotalCount = 0;

        }
        private void CopyData(AppSettings other)
        {
            CurrentInsertCode = other.CurrentInsertCode;
            AppSetLoadCarrAddedX = other.AppSetLoadCarrAddedX;
            AppSetLoadCarrAddedY = other.AppSetLoadCarrAddedY;
            AppSetLoadAddedAngle = other.AppSetLoadAddedAngle;
            AppSetUnloadAddedAngle = other.AppSetUnloadAddedAngle;
            AppSetOriginXOffset= other.AppSetOriginXOffset;
            AppSetOriginYOffset= other.AppSetOriginYOffset;
            AppSetxyLoadWorldTrayOrigin.X= other.AppSetxyLoadWorldTrayOrigin.X;
            AppSetxyLoadWorldTrayOrigin.Y= other.AppSetxyLoadWorldTrayOrigin.Y;
            AppSetxyUnloadWorldTrayOrigin.X= other.AppSetxyUnloadWorldTrayOrigin.X;
            AppSetxyUnloadWorldTrayOrigin.Y = other.AppSetxyUnloadWorldTrayOrigin.Y;
            for (int i = 0; i < 4; i++)
            {
                AppSetLoadTrayCalibPt[i] = other.AppSetLoadTrayCalibPt[i];
                AppSetLoadCarrierCalibPt[i] = other.AppSetLoadCarrierCalibPt[i];
                AppSetUnloadTrayCalibPt[i] = other.AppSetUnloadTrayCalibPt[i];
                AppSetUnloadCarrierCalibPt[i] = other.AppSetUnloadCarrierCalibPt[i];
            }
            AppSetLoadTrayCurrIndex = other.AppSetLoadTrayCurrIndex;
            AppSetLoadCarrierCurrIndex = other.AppSetLoadCarrierCurrIndex;
            AppSetUnloadCarrierSliceNo = other.AppSetUnloadCarrierSliceNo;
            AppSetUnLoadTrayCurrIndex = other.AppSetUnLoadTrayCurrIndex;
            AppSetCounterPerOrder = other.AppSetCounterPerOrder;
            AppSetLoadedInsCounter = other.AppSetLoadedInsCounter;
            AppSetUnloadedInsCounter = other.AppSetUnloadedInsCounter;
            AppSetTotalUnloadMissedCount = other.AppSetTotalUnloadMissedCount;
            AppSetMissPocketTotalCount = other.AppSetMissPocketTotalCount;
            AppSetLangFl = other.AppSetLangFl;
            AppSetMaxOffsetRangeHeight = other.AppSetMaxOffsetRangeHeight;//added by noam
        }

        public void Serialize() 
        {
            //retrieve parameters that need to be save in AppSetting.XML 
            AppSetLoadCarrAddedX = AppGen.Inst.VisionParam.LoadCarrAddedX;
            AppSetLoadCarrAddedY = AppGen.Inst.VisionParam.LoadCarrAddedY;
            AppSetLoadAddedAngle = AppGen.Inst.VisionParam.LoadAddedAngle;
            AppSetUnloadAddedAngle = AppGen.Inst.VisionParam.UnloadAddedAngle;
            AppSetOriginXOffset = AppGen.Inst.VisionParam.OriginXOffset;
            AppSetOriginYOffset = AppGen.Inst.VisionParam.OriginYOffset;
            AppSetLoadTrayCurrIndex = AppGen.Inst.LoadTray.CurrIndex;
            AppSetLoadCarrierCurrIndex = AppGen.Inst.LoadCarrier.CurrIndex;
            AppSetUnloadCarrierSliceNo = AppGen.Inst.MainCycle.UnloadCarrierSliceNo;
            AppSetUnLoadTrayCurrIndex = AppGen.Inst.UnLoadTray.CurrIndex;
            AppSetCounterPerOrder = AppGen.Inst.MainCycle.CounterPerOrder;
            AppSetLoadedInsCounter = AppGen.Inst.MainCycle.LoadedInsCounter;
            AppSetUnloadedInsCounter = AppGen.Inst.MainCycle.UnloadedInsCounter;
            AppSetTotalUnloadMissedCount = AppGen.Inst.MainCycle.TotalUnloadMissedCount;
            AppSetMissPocketTotalCount = AppGen.Inst.MainCycle.MissPocketTotalCount;
            AppSetLangFl = AppGen.Inst.MDImain.LangFl;
            

            AppGen.Inst.XMLSerialize.Serialize(this, System.IO.Directory.GetCurrentDirectory() + "\\AppSetting\\", "AppConf.xml", true);
        }
        public void DeSerialize()
        {        
            CopyData((AppSettings)AppGen.Inst.XMLSerialize.DeSerialize(this, System.IO.Directory.GetCurrentDirectory() + "\\AppSetting\\", "AppConf.xml", false));

            //using retrieve data from AppSet(xml file) to software parameters 
            AppGen.Inst.VisionParam.LoadTrayCalibPt = AppSetLoadTrayCalibPt;
            AppGen.Inst.VisionParam.LoadCarrierCalibPt = AppSetLoadCarrierCalibPt;
            AppGen.Inst.VisionParam.UnloadTrayCalibPt = AppSetUnloadTrayCalibPt;
            AppGen.Inst.VisionParam.UnloadCarrierCalibPt = AppSetUnloadCarrierCalibPt;
            AppGen.Inst.VisionParam.xyLoadWorldTrayOrigin = AppSetxyLoadWorldTrayOrigin;
            AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin = AppSetxyUnloadWorldTrayOrigin;

            //AppSetLoadCarrAddedX = AppGen.Inst.VisionParam.LoadCarrAddedX;
            AppGen.Inst.VisionParam.LoadCarrAddedX = AppSetLoadCarrAddedX;
            AppGen.Inst.VisionParam.LoadCarrAddedY = AppSetLoadCarrAddedY;
            AppGen.Inst.VisionParam.LoadAddedAngle = AppSetLoadAddedAngle;
            AppGen.Inst.VisionParam.UnloadAddedAngle = AppSetUnloadAddedAngle;
            AppGen.Inst.VisionParam.OriginXOffset = AppSetOriginXOffset;
            AppGen.Inst.VisionParam.OriginYOffset = AppSetOriginYOffset;

            AppGen.Inst.LoadTray.CurrIndex = AppSetLoadTrayCurrIndex;
            AppGen.Inst.LoadCarrier.CurrIndex = AppSetLoadCarrierCurrIndex;
            AppGen.Inst.MainCycle.UnloadCarrierSliceNo = AppSetUnloadCarrierSliceNo;
            AppGen.Inst.UnLoadTray.CurrIndex = AppSetUnLoadTrayCurrIndex;
            AppGen.Inst.MainCycle.CounterPerOrder = AppSetCounterPerOrder;
            AppGen.Inst.MainCycle.LoadedInsCounter = AppSetLoadedInsCounter;
            AppGen.Inst.MainCycle.UnloadedInsCounter=AppSetUnloadedInsCounter;
            AppGen.Inst.MainCycle.TotalUnloadMissedCount = AppSetTotalUnloadMissedCount;
            AppGen.Inst.MainCycle.MissPocketTotalCount = AppSetMissPocketTotalCount;
            AppGen.Inst.MainCycle.SetMaxOffsetRangeHeight = AppSetMaxOffsetRangeHeight; //added by noam
            AppGen.Inst.MDImain.LangFl = AppSetLangFl;
            


            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadAddedAngle, AppGen.Inst.VisionParam.LoadAddedAngle.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadCarrAddedX, AppGen.Inst.VisionParam.LoadCarrAddedX.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadCarrAddedY, AppGen.Inst.VisionParam.LoadCarrAddedY.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.UnloadAddedAngle, AppGen.Inst.VisionParam.UnloadAddedAngle.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.UnloadTrayXoffset, AppGen.Inst.VisionParam.OriginXOffset.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.UnloadTrayYoffset, AppGen.Inst.VisionParam.OriginYOffset.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadTray, AppGen.Inst.LoadTray.CurrIndex.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, AppGen.Inst.MainCycle.UnloadCarrierSliceNo.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexUnloadTray, AppGen.Inst.UnLoadTray.CurrIndex.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.CounterPerOrder, AppGen.Inst.MainCycle.CounterPerOrder.ToString());      
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.LoadedInsCounter, AppGen.Inst.MainCycle.LoadedInsCounter.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.UnloadedInsCounter, AppGen.Inst.MainCycle.UnloadedInsCounter.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.MissedUnloadCount, AppGen.Inst.MainCycle.TotalUnloadMissedCount.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.MissedLoadCount, AppGen.Inst.MainCycle.MissPocketTotalCount.ToString());
        
        }
       
        // members
        public string CurrentInsertCode { get; set; }
        public string AppSetLangFl { get; set; }
        public double AppSetLoadCarrAddedX { get; set; }
        public double AppSetLoadCarrAddedY { get; set; }
        public double AppSetLoadAddedAngle { get; set; }
        public double AppSetUnloadAddedAngle { get; set; }
        public double AppSetOriginXOffset { get; set; }
        public double AppSetOriginYOffset { get; set; }
        
        public int AppSetUnloadedInsCounter { get; set; }
        public int AppSetLoadedInsCounter { get; set; }
        public int AppSetCounterPerOrder { get; set; }
        public int AppSetInsQnt { get; set; }
        public int AppSetTotalUnloadMissedCount { get; set; }
        public int AppSetMissPocketTotalCount { get; set; }
        public int AppSetUnloadCarrierSliceNo { get; set; }
        public int AppSetLoadTrayCurrIndex { get; set; }
        public int AppSetUnLoadTrayCurrIndex { get; set; }
        public int AppSetLoadCarrierCurrIndex { get; set; }
        public double AppSetMaxOffsetRangeHeight { get; set; } //added by noam        

        public PointCoord AppSetxyLoadWorldTrayOrigin = new PointCoord();
        public PointCoord AppSetxyUnloadWorldTrayOrigin = new PointCoord();

        public List<PointCoord> AppSetLoadTrayCalibPt = new List<PointCoord>(); 
        public List<PointCoord> AppSetLoadCarrierCalibPt = new List<PointCoord>();
        public List<PointCoord> AppSetUnloadTrayCalibPt = new List<PointCoord>();
        public List<PointCoord> AppSetUnloadCarrierCalibPt = new List<PointCoord>();
    }
}
