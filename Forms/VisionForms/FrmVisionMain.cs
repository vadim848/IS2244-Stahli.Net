using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.PatInspect;
using Cognex.VisionPro.PMAlign;
using Cognex.VisionPro.Exceptions;
using Cognex.VisionPro.ToolBlock;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.Dimensioning;



namespace Stahli2Robots
{
    public partial class FrmVisionMain : Form
    {
        FrmLoadCarrier frmLoadCarrier = new FrmLoadCarrier();
        FrmLoadTray frmLoadtray = new FrmLoadTray();
        FrmUnloadCarrier frmUnloadCarrier = new FrmUnloadCarrier();


        public FrmLoadCarrier FrmLoadCarrier
        {
            get { return frmLoadCarrier; }
            set { frmLoadCarrier = value; }
        }
        public FrmLoadTray FrmLoadtray
        {
            get { return frmLoadtray; }
            set { frmLoadtray = value; }
        }
        public FrmUnloadCarrier FrmUnloadCarrier
        {
            get { return frmUnloadCarrier; }
            set { frmUnloadCarrier = value; }
        }

        List<Form> cameraForms = new List<Form>();
        ArrayList formList = new ArrayList();

        public FrmVisionMain()
        {
            InitializeComponent();          
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AddNewTab(frmLoadtray);
            AddNewTab(frmLoadCarrier);
            AddNewTab(frmUnloadCarrier);
        }

        private void AddNewTab(Form frm)
        {
            frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            TabPage tab = new TabPage(frm.Text);
            frm.TopLevel = false;
            frm.Parent = tab;
            frm.Visible = true;
            tabControl1.TabPages.Add(tab);
            frm.Dock = DockStyle.Fill;
            tabControl1.SelectedTab = tab;
            formList.Add(frm);
        }

        //Ziv 13.7.15
        public void savePattern(string cameraName, string orderName, CogPMAlignPattern pattern)
        {

            string path;// = Application.StartupPath;
            path = Directory.GetCurrentDirectory();

            if (path.ToLower().EndsWith(@"\bin\release")) path = path.ToLower().Replace(@"\bin\release", @"\bin\debug");

            if (!(path.ToLower().EndsWith(@"\bin\debug"))) return;

            path += ("\\CognexStahli\\" + cameraName);

            try
            {
                if (string.IsNullOrEmpty(path)) return;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                path += ("\\" +orderName + ".vpp");

                CogSerializer.SaveObjectToFile(pattern, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        //Ziv 13.7.15
        public CogPMAlignPattern loadPattern(string cameraName, string orderName)        
        {
            string path = Application.StartupPath;

            if (path.ToLower().EndsWith(@"\bin\release")) path = path.ToLower().Replace(@"\bin\release", @"\bin\debug");

            if (!(path.ToLower().EndsWith(@"\bin\debug"))) return null;

            path += ("\\CognexStahli\\" + cameraName +"\\" + orderName +".vpp");

            try
            {
                if (string.IsNullOrEmpty(path)) return null;
                //if (!Directory.Exists(path)) Directory.CreateDirectory(path);
              //  path += ("\\" + orderName + ".vpp");

                CogPMAlignPattern pattern = CogSerializer.LoadObjectFromFile(path) as Cognex.VisionPro.PMAlign.CogPMAlignPattern;
                if (pattern != null) return pattern;
                else
                {
                    MessageBox.Show("Error loading " + cameraName + "pattern");
                    return pattern;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null ;
            }
        }

        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
	        {
                case 0:
                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadtray.VisionAction(VisionActionType.ImageAcquisition); 
                    break;
                case 1:
                    AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.VisionAction(VisionActionType.ImageAcquisition); 
                    break;
                case 2:
                    AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.VisionAction(VisionActionType.ImageAcquisition); 
                    break;
	        }        
        }
    }
}
