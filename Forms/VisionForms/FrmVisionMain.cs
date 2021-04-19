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
