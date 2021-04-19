using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Stahli2Robots
{
    public partial class FrmOrderEditor : Form
    {
        private bool editModeFl;
    
        public FrmOrderEditor()
        {
            InitializeComponent();
            editModeFl = false;
            cmdLoadOrder.Enabled = false;
            
            // delegats:  
            loadOrderDataDelegate = new LoadOrderDataDelegate(LoadOrderDataDelegateFunc);
        }
        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            cmdLoadOrder.Enabled = false;
            editModeFl = false;
            this.Hide();
            e.Cancel = true;
        }

        private void cmdLoadOrder_Click(object sender, EventArgs e)  //Reading from OrderParams.xml //AppSetting.XML
        {
            LoadingOrder(txtFileName.Text, false);
        }
        private void cmdSaveOrder_Click(object sender, EventArgs e)  //Savinging to OrderParams.xml //AppSetting.XML
        {
            SavingOrder(txtInsertCode.Text);
            LoadingOrder(txtFileName.Text , false);
        }
        public void LoadingOrder(string OrderName, bool FirstLoad)
        {
            AppGen.Inst.OrderParams.DeSerialize(OrderName);  //from .xml file to OrderParam class
            if (AppGen.Inst.OrderParams.InsertCode != OrderName)
            {
                // return error: order not loaded
            }

            AppGen.Inst.MDImain.frmOrderEditor.LoadOrderData();
            AppGen.Inst.MDImain.frmTitle.LoadOrderData();

            if (AppGen.Inst.MDImain.chkVisionON.Checked)
            {
                AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.InitVision();
                AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.InitVision();
                AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.InitVision();
                
                AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.LoadOrderData();
                AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.LoadOrderData();
                AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.LoadOrderData();
            }         

            AppGen.Inst.AppSettings.CurrentInsertCode = AppGen.Inst.OrderParams.InsertCode;
            AppGen.Inst.AppSettings.Serialize();

            //creating new instance with the apropriate carriers/tarys according to Order
            if (!FirstLoad)
            {
                AppGen.Inst.LoadCarrier.ResetData();
                AppGen.Inst.LoadTray.ResetData();
                AppGen.Inst.UnLoadCarrier.ResetData();
                AppGen.Inst.UnLoadTray.ResetData();

                AppGen.Inst.MainCycle.CounterPerOrder = 0;
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.CounterPerOrder, AppGen.Inst.MainCycle.CounterPerOrder.ToString());  
            }                           
            AppGen.Inst.LoadCarrier.ReadFromFile();          
            AppGen.Inst.LoadTray.ReadFromFile("LoadTray");
            AppGen.Inst.UnLoadCarrier.ReadFromFile("UnloadCarrier");
            AppGen.Inst.MDImain.frmTitle.progBarLoadTray.Maximum = AppGen.Inst.LoadTray.IndexList.Count;   
//----------Unload Tray---------------------------------------------------------------------------------------------------------------------- 
            //reading unload tray file, calculate tray angle, adding calculated angle to tray array and origin, add Rotated origen to Tray array          
            AppGen.Inst.UnLoadTray.ReadFromFile("UnloadTray");
            AppGen.Inst.MDImain.frmTitle.progBarUnloadTray.Maximum = AppGen.Inst.UnLoadTray.IndexList.Count;
            PointCoord VectorDelta = new PointCoord();  //for calculate tray direction (by useing Calib 2 points)
            VectorDelta.X = AppGen.Inst.VisionParam.UnloadTrayCalibPt[0].X - AppGen.Inst.VisionParam.UnloadTrayCalibPt[3].X;
            VectorDelta.Y = AppGen.Inst.VisionParam.UnloadTrayCalibPt[0].Y - AppGen.Inst.VisionParam.UnloadTrayCalibPt[3].Y;
            AppGen.Inst.UnLoadTray.TrayAlfa = Math.Atan(VectorDelta.Y / VectorDelta.X);
            AppGen.Inst.UnLoadTray.TrayAlfa = Math.Abs(AppGen.Inst.UnLoadTray.TrayAlfa) * -1; //always minus (atan can return plus in some cases)
            //AppGen.Inst.UnLoadTray.TrayAlfa = AppGen.Inst.Calculate.FindPolarAngle(VectorDelta);       
            //AppGen.Inst.UnLoadTray.TrayAlfa = -(Math.PI / 2);  // -90 deg from robot coord system   //for testing

            for (int ii = 0; ii < AppGen.Inst.UnLoadTray.IndexList.Count; ii++)
            {
                double X = AppGen.Inst.UnLoadTray.IndexList[ii].X_file;
                double Y = AppGen.Inst.UnLoadTray.IndexList[ii].Y_file;
                AppGen.Inst.Calculate.RotateCoordByAlfa(ref X, ref Y, AppGen.Inst.UnLoadTray.TrayAlfa);
                AppGen.Inst.UnLoadTray.IndexList[ii].X_VisRes = X;
                AppGen.Inst.UnLoadTray.IndexList[ii].Y_VisRes = Y;
                AppGen.Inst.UnLoadTray.IndexList[ii].Angle_VisRes = AppGen.Inst.UnLoadTray.TrayAlfa * (180 / Math.PI);  //converted to degree;
            }
            //double A = AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin.X;
            //double B = AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin.Y;
            //AppGen.Inst.Calculate.RotateCoordByAlfa(ref A, ref B, AppGen.Inst.UnLoadTray.TrayAlfa);
            //AppGen.Inst.VisionParam.xyUnloadWorldTrayOriginRotated.X = A;
            //AppGen.Inst.VisionParam.xyUnloadWorldTrayOriginRotated.Y = B;

            for (int ii = 0; ii < AppGen.Inst.UnLoadTray.IndexList.Count; ii++)
            {
                AppGen.Inst.UnLoadTray.IndexList[ii].X_VisRes += AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin.X;
                AppGen.Inst.UnLoadTray.IndexList[ii].Y_VisRes += AppGen.Inst.VisionParam.xyUnloadWorldTrayOrigin.Y;           
            }   
//----------------------------------------------------------------------------------------------------------------------------------------    

            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadTray, AppGen.Inst.LoadTray.CurrIndex.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexUnloadTray, AppGen.Inst.UnLoadTray.CurrIndex.ToString());
            AppGen.Inst.MainCycle.UnloadCarrierSliceNo = 1;
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, AppGen.Inst.MainCycle.UnloadCarrierSliceNo.ToString());
        }
        private void SavingOrder(string OrderName)
        {
            AppGen.Inst.OrderParams.InsertCode = txtInsertCode.Text;
            AppGen.Inst.OrderParams.InsertDescription = txtDescription.Text;
            AppGen.Inst.OrderParams.InsertSymetry = cmbSymetry.SelectedIndex;
            AppGen.Inst.OrderParams.InsertHeight = double.Parse(txtInsertHeight.Text);
            AppGen.Inst.OrderParams.SensorHeight = double.Parse(txtSensorHeight.Text);         
            AppGen.Inst.OrderParams.InsertHoleDiameter = double.Parse(txtHoleDiameter.Text);
            AppGen.Inst.OrderParams.GripperCode = cmbGrippertype.SelectedIndex;
            AppGen.Inst.OrderParams.ServiceLoadTrayName = txtTrayLoad.Text;
            AppGen.Inst.OrderParams.ServiceUnloadTrayName = txtTrayUnload.Text;
            AppGen.Inst.OrderParams.CarrierName = txtCarrier.Text;
        
            AppGen.Inst.OrderParams.Cam1_Brightness = double.Parse(txtCam1Brightness.Text);                                     
            AppGen.Inst.OrderParams.Cam2_Brightness = double.Parse(txtCam2Brightness.Text);
            AppGen.Inst.OrderParams.Cam3_Brightness = double.Parse(txtCam3Brightness.Text);
            AppGen.Inst.OrderParams.Cam1_Contrast = double.Parse(txtCam1Contrast.Text);
            AppGen.Inst.OrderParams.Cam2_Contrast = double.Parse(txtCam2Contrast.Text);
            AppGen.Inst.OrderParams.Cam3_Contrast = double.Parse(txtCam3Contrast.Text);
            AppGen.Inst.OrderParams.Cam1_Score = double.Parse(txtCam1Score.Text);
            AppGen.Inst.OrderParams.Cam2_Score = double.Parse(txtCam2Score.Text);
            AppGen.Inst.OrderParams.Cam3_Score = double.Parse(txtCam3Score.Text);
            AppGen.Inst.OrderParams.Cam1_Angle = double.Parse(txtCam1Angle.Text);
            AppGen.Inst.OrderParams.Cam3_Angle = double.Parse(txtCam3Angle.Text);
            
            LoadOrderData();
            AppGen.Inst.OrderParams.Serialize(OrderName);
            editModeFl = false;
            this.Width = 464;
        }
        private void cmdEditMode_Click(object sender, EventArgs e)
        {
            if (!editModeFl)
            {
                txtInsertCode.Text = lblInsertCode.Text;
                txtDescription.Text = lblDescription.Text;
                cmbSymetry.SelectedIndex = int.Parse(lblSymetry.Text);
                txtInsertHeight.Text = lblInsertHeight.Text;
                txtSensorHeight.Text = lblSensorHeight.Text;             
                txtHoleDiameter.Text = lblHoleDiameter.Text;
                cmbGrippertype.SelectedIndex = int.Parse(lblGripperCode.Text);
                txtTrayLoad.Text = lblTrayLoad.Text;
                txtTrayUnload.Text = lblTrayUnload.Text;
                txtCarrier.Text = lblCarrier.Text;
                txtCam1Brightness.Text = lblCam1Brightness.Text;
                txtCam2Brightness.Text = lblCam2Brightness.Text;
                txtCam3Brightness.Text = lblCam3Brightness.Text;
                txtCam1Contrast.Text = lblCam1Contrast.Text;
                txtCam2Contrast.Text = lblCam2Contrast.Text;
                txtCam3Contrast.Text = lblCam3Contrast.Text;
                txtCam1Score.Text = lblCam1Score.Text;
                txtCam2Score.Text = lblCam2Score.Text;
                txtCam3Score.Text = lblCam3Score.Text;
                txtCam1Angle.Text = lblCam1Angle.Text;
                txtCam3Angle.Text = lblCam3Angle.Text;

                txtInsertCode.Visible = true;
                txtDescription.Visible = true;
                cmbSymetry.Visible = true;
                txtInsertHeight.Visible = true;
                txtSensorHeight.Visible = true;              
                txtHoleDiameter.Visible = true;
                cmbGrippertype.Visible = true;
                txtTrayLoad.Visible = true;
                txtTrayUnload.Visible = true;
                txtCarrier.Visible = true;
                txtCam1Brightness.Visible = true;
                txtCam2Brightness.Visible = true;
                txtCam3Brightness.Visible = true;
                txtCam1Contrast.Visible = true;
                txtCam2Contrast.Visible = true;
                txtCam3Contrast.Visible = true;
                txtCam1Score.Visible = true;
                txtCam2Score.Visible = true;
                txtCam3Score.Visible = true;
                lblCam1Angle.Visible = true;
                lblCam3Angle.Visible = true;
                cmdSaveOrder.Visible = true;
                editModeFl = true;
                this.Width = 648;
            }
            else
            {
                txtInsertCode.Visible = false;
                txtDescription.Visible = false;
                cmbSymetry.Visible = false;
                txtInsertHeight.Visible = false;
                txtSensorHeight.Visible = false;               
                txtHoleDiameter.Visible = false;
                cmbGrippertype.Visible = false;
                txtTrayLoad.Visible = false;
                txtTrayUnload.Visible = false;
                txtCarrier.Visible = false;
                txtCam1Brightness.Visible = false;
                txtCam2Brightness.Visible = false;
                txtCam3Brightness.Visible = false;
                txtCam1Score.Visible = false;
                txtCam2Score.Visible = false;
                txtCam3Score.Visible = false;
                lblCam1Angle.Visible = false;
                lblCam3Angle.Visible = false;
                cmdSaveOrder.Visible = true;
                editModeFl = false;
                this.Width = 464;
            }
        }

        private void cmdOpenDialog_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = " C:\\PROJECTS\\Stahli.Net\\Bin\\Debug\\Orders\\";
            openFileDialog1.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\Orders\\";
            openFileDialog1.Filter = "XML Files|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = Path.GetFileName(openFileDialog1.FileName);
                cmdLoadOrder.Enabled = true;
            }
        }
        private void cmdOpenDialogTray_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            //openFileDialog2.InitialDirectory = " C:\\PROJECTS\\Stahli.Net\\Bin\\Debug\\TrayFiles\\";
            openFileDialog2.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\TrayFiles\\";
            openFileDialog2.Filter = "Text Files|*.txt";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                txtTrayLoad.Text = Path.GetFileName(openFileDialog2.FileName);
            }
        }
        private void cmdOpenDialogTray2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            //openFileDialog2.InitialDirectory = " C:\\PROJECTS\\Stahli.Net\\Bin\\Debug\\TrayFiles\\";
            openFileDialog2.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\TrayFiles\\";
            openFileDialog2.Filter = "Text Files|*.txt";
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                txtTrayUnload.Text = Path.GetFileName(openFileDialog2.FileName);
            }
        }
        private void cmdOpenDialogCarrier_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog3 = new OpenFileDialog();
            //openFileDialog3.InitialDirectory = " C:\\PROJECTS\\Stahli.Net\\Bin\\Debug\\CarrierFiles\\";
            openFileDialog3.InitialDirectory = System.IO.Directory.GetCurrentDirectory() + "\\CarrierFiles\\";
            openFileDialog3.Filter = "Text Files|*.txt";
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                txtCarrier.Text = Path.GetFileName(openFileDialog3.FileName);
            }
        }

        // delegates
        private delegate void LoadOrderDataDelegate();
        private LoadOrderDataDelegate loadOrderDataDelegate;
        private void LoadOrderDataDelegateFunc()
        {
            try
            {
                lblInsertCode.Text = AppGen.Inst.OrderParams.InsertCode;
                lblDescription.Text = AppGen.Inst.OrderParams.InsertDescription;
                lblSymetry.Text = AppGen.Inst.OrderParams.InsertSymetry.ToString();
                lblInsertHeight.Text = AppGen.Inst.OrderParams.InsertHeight.ToString();
                lblSensorHeight.Text = AppGen.Inst.OrderParams.SensorHeight.ToString();               
                lblHoleDiameter.Text = AppGen.Inst.OrderParams.InsertHoleDiameter.ToString();
                lblGripperCode.Text = AppGen.Inst.OrderParams.GripperCode.ToString();
                lblTrayLoad.Text = AppGen.Inst.OrderParams.ServiceLoadTrayName;
                lblTrayUnload.Text = AppGen.Inst.OrderParams.ServiceUnloadTrayName;
                lblCarrier.Text = AppGen.Inst.OrderParams.CarrierName;
                lblCam1Brightness.Text = AppGen.Inst.OrderParams.Cam1_Brightness.ToString();
                lblCam2Brightness.Text = AppGen.Inst.OrderParams.Cam2_Brightness.ToString();
                lblCam3Brightness.Text = AppGen.Inst.OrderParams.Cam3_Brightness.ToString();
                lblCam1Contrast.Text = AppGen.Inst.OrderParams.Cam1_Contrast.ToString();
                lblCam2Contrast.Text = AppGen.Inst.OrderParams.Cam2_Contrast.ToString();
                lblCam3Contrast.Text = AppGen.Inst.OrderParams.Cam3_Contrast.ToString();
                lblCam1Score.Text = AppGen.Inst.OrderParams.Cam1_Score.ToString();
                lblCam2Score.Text = AppGen.Inst.OrderParams.Cam2_Score.ToString();
                lblCam3Score.Text = AppGen.Inst.OrderParams.Cam3_Score.ToString();
                lblCam1Angle.Text = AppGen.Inst.OrderParams.Cam1_Angle.ToString();
                lblCam3Angle.Text = AppGen.Inst.OrderParams.Cam3_Angle.ToString();
            }
            catch { }
        }
        public void LoadOrderData()
        {
            try
            {
                this.Invoke(loadOrderDataDelegate);
            }
            catch { }
        }



     

       
    }     
}
