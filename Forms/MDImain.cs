using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO.Ports;
using System.Diagnostics;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslateText;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;




namespace Stahli2Robots
{
	public class MDImain : System.Windows.Forms.Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, ProcessWindowStyle nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr hwnd, bool enabled);

        #region veriables Defenation
        private System.Windows.Forms.MainMenu mainMenu1;
		int count=0;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItemAcs;
		private System.Windows.Forms.MenuItem menuItemHoriz;
		private System.Windows.Forms.MenuItem menuItemVert;
		private System.Windows.Forms.MenuItem menuItemAI;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemNew;
		private System.Windows.Forms.MenuItem menuItemClose;
		private System.Windows.Forms.MenuItem menuItemWindow;
		private System.Windows.Forms.MenuItem menuItemMax;
        private System.Windows.Forms.MenuItem menuItemMin;
        private System.Windows.Forms.MenuItem menuItem1;
        private GroupBox grpService;
        public CheckBox chkErrLog;
        public CheckBox chkVisionON;
        public CheckBox chkRobotsOn;
        public CheckBox chkPlcON;
        private ComboBox cmbWarnnings;
        private Label lblTableMode;
        public ToolStripButton _tbr_Main;
        private ToolStripSeparator toolStripSeparator17;
        public ToolStripButton _tbr_Conv;
        private ToolStripSeparator toolStripSeparator16;
        public ToolStripButton _tbr_Robot;
        private ToolStripSeparator toolStripSeparator14;
        public ToolStripButton _tbr_Camera;
        private ToolStripSeparator toolStripSeparator4;
        public ToolStripButton _tbr_Tray;
        private ToolStripSeparator toolStripSeparator15;
        public ToolStripButton _tbr_Carr;
        private ToolStripSeparator toolStripSeparator13;
        public ToolStripButton _tbr_Order;
        private ToolStripSeparator toolStripSeparator19;
        public ToolStripButton _tbr_PLC;
        private ToolStripSeparator toolStripSeparator21;
        public ToolStripButton _tbr_ES_Rel;
        private ToolStripSeparator toolStripSeparator12;
        public ToolStripButton _tbr_Exit;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        public ToolStrip tbr;
        public CheckBox chkWithStacker;
        public CheckBox chkDebugMode;
        private ToolStripLabel toolStripLabel1;
        private MenuItem menuItem2;
        private IContainer components;
        #endregion

        #region Translation Variables
        ResourceManager rm;
        private MenuItem menuItem6;
        private MenuItem menuItem7;
        private MenuItem menuItem8;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        public ToolStripButton _tbr_OpenDoor;
        clsReadExelandUpdate ReadAndUpdate;
        #endregion

        //constructor (for initial the class 'MDImain')
        public MDImain()
		{
			InitializeComponent();

            afterModeChangedDelegate = new AfterModeChangedDelegate(AfterModeChangedDelegateFunc);
		}

        //methods
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDImain));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemNew = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemClose = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemWindow = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemAI = new System.Windows.Forms.MenuItem();
            this.menuItemAcs = new System.Windows.Forms.MenuItem();
            this.menuItemHoriz = new System.Windows.Forms.MenuItem();
            this.menuItemVert = new System.Windows.Forms.MenuItem();
            this.menuItemMax = new System.Windows.Forms.MenuItem();
            this.menuItemMin = new System.Windows.Forms.MenuItem();
            this.grpService = new System.Windows.Forms.GroupBox();
            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.chkWithStacker = new System.Windows.Forms.CheckBox();
            this.chkErrLog = new System.Windows.Forms.CheckBox();
            this.chkVisionON = new System.Windows.Forms.CheckBox();
            this.chkRobotsOn = new System.Windows.Forms.CheckBox();
            this.chkPlcON = new System.Windows.Forms.CheckBox();
            this.cmbWarnnings = new System.Windows.Forms.ComboBox();
            this.lblTableMode = new System.Windows.Forms.Label();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbr = new System.Windows.Forms.ToolStrip();
            this._tbr_Main = new System.Windows.Forms.ToolStripButton();
            this._tbr_Conv = new System.Windows.Forms.ToolStripButton();
            this._tbr_Robot = new System.Windows.Forms.ToolStripButton();
            this._tbr_Camera = new System.Windows.Forms.ToolStripButton();
            this._tbr_Tray = new System.Windows.Forms.ToolStripButton();
            this._tbr_Carr = new System.Windows.Forms.ToolStripButton();
            this._tbr_Order = new System.Windows.Forms.ToolStripButton();
            this._tbr_PLC = new System.Windows.Forms.ToolStripButton();
            this._tbr_OpenDoor = new System.Windows.Forms.ToolStripButton();
            this._tbr_ES_Rel = new System.Windows.Forms.ToolStripButton();
            this._tbr_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.grpService.SuspendLayout();
            this.tbr.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemWindow,
            this.menuItem5});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemNew,
            this.menuItem1,
            this.menuItemClose,
            this.menuItem2});
            this.menuItemFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.menuItemFile.Text = "&File";
            // 
            // menuItemNew
            // 
            this.menuItemNew.Index = 0;
            this.menuItemNew.Text = "&New";
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MergeOrder = 4;
            this.menuItem1.Text = "Close All";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItemClose
            // 
            this.menuItemClose.Index = 2;
            this.menuItemClose.MergeOrder = 5;
            this.menuItemClose.Text = "Cl&ose";
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7,
            this.menuItem8,
            this.menuItem3,
            this.menuItem4});
            this.menuItem2.RadioCheck = true;
            this.menuItem2.Text = "Language";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Write Control Name to EXEL";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "Create Res File from EXEL";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuItem8.Text = "Update UI";
            this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 3;
            this.menuItem3.Text = "Hebrew";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 4;
            this.menuItem4.Text = "English";
            // 
            // menuItemWindow
            // 
            this.menuItemWindow.Index = 1;
            this.menuItemWindow.MdiList = true;
            this.menuItemWindow.Text = "&Window";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAI,
            this.menuItemAcs,
            this.menuItemHoriz,
            this.menuItemVert,
            this.menuItemMax,
            this.menuItemMin});
            this.menuItem5.Text = "&Layout";
            // 
            // menuItemAI
            // 
            this.menuItemAI.Index = 0;
            this.menuItemAI.Text = "Arrange&Icons";
            this.menuItemAI.Click += new System.EventHandler(this.menuItemAI_Click);
            // 
            // menuItemAcs
            // 
            this.menuItemAcs.Index = 1;
            this.menuItemAcs.Text = "&Cascade";
            this.menuItemAcs.Click += new System.EventHandler(this.menuItemCas_Click);
            // 
            // menuItemHoriz
            // 
            this.menuItemHoriz.Index = 2;
            this.menuItemHoriz.Text = "Arrange &Horizontal";
            this.menuItemHoriz.Click += new System.EventHandler(this.menuItemHoriz_Click);
            // 
            // menuItemVert
            // 
            this.menuItemVert.Index = 3;
            this.menuItemVert.Text = "Arrange &Vertical";
            this.menuItemVert.Click += new System.EventHandler(this.menuItemVert_Click);
            // 
            // menuItemMax
            // 
            this.menuItemMax.Index = 4;
            this.menuItemMax.Text = "Ma&ximize all";
            this.menuItemMax.Click += new System.EventHandler(this.menuItemMax_Click);
            // 
            // menuItemMin
            // 
            this.menuItemMin.Index = 5;
            this.menuItemMin.Text = "Mi&nimize all";
            this.menuItemMin.Click += new System.EventHandler(this.menuItemMin_Click);
            // 
            // grpService
            // 
            this.grpService.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grpService.Controls.Add(this.chkDebugMode);
            this.grpService.Controls.Add(this.chkWithStacker);
            this.grpService.Controls.Add(this.chkErrLog);
            this.grpService.Controls.Add(this.chkVisionON);
            this.grpService.Controls.Add(this.chkRobotsOn);
            this.grpService.Controls.Add(this.chkPlcON);
            this.grpService.Location = new System.Drawing.Point(3, 615);
            this.grpService.Name = "grpService";
            this.grpService.Size = new System.Drawing.Size(985, 42);
            this.grpService.TabIndex = 12;
            this.grpService.TabStop = false;
            this.grpService.Text = "Service";
            this.grpService.Visible = false;
            // 
            // chkDebugMode
            // 
            this.chkDebugMode.AutoSize = true;
            this.chkDebugMode.Location = new System.Drawing.Point(429, 19);
            this.chkDebugMode.Name = "chkDebugMode";
            this.chkDebugMode.Size = new System.Drawing.Size(88, 17);
            this.chkDebugMode.TabIndex = 17;
            this.chkDebugMode.Text = "Debug Mode";
            this.chkDebugMode.UseVisualStyleBackColor = true;
            // 
            // chkWithStacker
            // 
            this.chkWithStacker.AutoSize = true;
            this.chkWithStacker.Checked = true;
            this.chkWithStacker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWithStacker.Location = new System.Drawing.Point(335, 19);
            this.chkWithStacker.Name = "chkWithStacker";
            this.chkWithStacker.Size = new System.Drawing.Size(88, 17);
            this.chkWithStacker.TabIndex = 16;
            this.chkWithStacker.Text = "With Stacker";
            this.chkWithStacker.UseVisualStyleBackColor = true;
            // 
            // chkErrLog
            // 
            this.chkErrLog.AutoSize = true;
            this.chkErrLog.Location = new System.Drawing.Point(241, 19);
            this.chkErrLog.Name = "chkErrLog";
            this.chkErrLog.Size = new System.Drawing.Size(88, 17);
            this.chkErrLog.TabIndex = 15;
            this.chkErrLog.Text = "Error Log ON";
            this.chkErrLog.UseVisualStyleBackColor = true;
            // 
            // chkVisionON
            // 
            this.chkVisionON.AutoSize = true;
            this.chkVisionON.Checked = true;
            this.chkVisionON.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVisionON.Location = new System.Drawing.Point(162, 19);
            this.chkVisionON.Name = "chkVisionON";
            this.chkVisionON.Size = new System.Drawing.Size(73, 17);
            this.chkVisionON.TabIndex = 14;
            this.chkVisionON.Text = "Vision ON";
            this.chkVisionON.UseVisualStyleBackColor = true;
            // 
            // chkRobotsOn
            // 
            this.chkRobotsOn.AutoSize = true;
            this.chkRobotsOn.Checked = true;
            this.chkRobotsOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRobotsOn.Location = new System.Drawing.Point(77, 19);
            this.chkRobotsOn.Name = "chkRobotsOn";
            this.chkRobotsOn.Size = new System.Drawing.Size(79, 17);
            this.chkRobotsOn.TabIndex = 13;
            this.chkRobotsOn.Text = "Robots ON";
            this.chkRobotsOn.UseVisualStyleBackColor = true;
            // 
            // chkPlcON
            // 
            this.chkPlcON.AutoSize = true;
            this.chkPlcON.Checked = true;
            this.chkPlcON.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlcON.Location = new System.Drawing.Point(6, 19);
            this.chkPlcON.Name = "chkPlcON";
            this.chkPlcON.Size = new System.Drawing.Size(65, 17);
            this.chkPlcON.TabIndex = 12;
            this.chkPlcON.Text = "PLC ON";
            this.chkPlcON.UseVisualStyleBackColor = true;
            // 
            // cmbWarnnings
            // 
            this.cmbWarnnings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbWarnnings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWarnnings.ForeColor = System.Drawing.Color.Red;
            this.cmbWarnnings.FormattingEnabled = true;
            this.cmbWarnnings.Location = new System.Drawing.Point(178, 76);
            this.cmbWarnnings.Name = "cmbWarnnings";
            this.cmbWarnnings.Size = new System.Drawing.Size(811, 24);
            this.cmbWarnnings.TabIndex = 13;
            this.cmbWarnnings.Text = "CMB-Warnnings";
            this.cmbWarnnings.Visible = false;
            // 
            // lblTableMode
            // 
            this.lblTableMode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTableMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTableMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTableMode.Location = new System.Drawing.Point(3, 76);
            this.lblTableMode.Name = "lblTableMode";
            this.lblTableMode.Size = new System.Drawing.Size(173, 24);
            this.lblTableMode.TabIndex = 15;
            this.lblTableMode.Text = "Table mode:";
            this.lblTableMode.Visible = false;
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 73);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 73);
            // 
            // tbr
            // 
            this.tbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._tbr_Main,
            this.toolStripSeparator17,
            this._tbr_Conv,
            this.toolStripSeparator16,
            this._tbr_Robot,
            this.toolStripSeparator14,
            this._tbr_Camera,
            this.toolStripSeparator4,
            this._tbr_Tray,
            this.toolStripSeparator15,
            this._tbr_Carr,
            this.toolStripSeparator13,
            this._tbr_Order,
            this.toolStripSeparator19,
            this._tbr_PLC,
            this.toolStripSeparator21,
            this._tbr_OpenDoor,
            this.toolStripSeparator1,
            this._tbr_ES_Rel,
            this.toolStripSeparator12,
            this._tbr_Exit,
            this.toolStripSeparator2,
            this.toolStripSeparator3,
            this.toolStripLabel1});
            this.tbr.Location = new System.Drawing.Point(0, 0);
            this.tbr.Name = "tbr";
            this.tbr.Size = new System.Drawing.Size(993, 73);
            this.tbr.TabIndex = 1;
            // 
            // _tbr_Main
            // 
            this._tbr_Main.AutoSize = false;
            this._tbr_Main.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Main.Image")));
            this._tbr_Main.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Main.Name = "_tbr_Main";
            this._tbr_Main.Size = new System.Drawing.Size(70, 70);
            this._tbr_Main.Text = "Main";
            this._tbr_Main.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Main.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_Conv
            // 
            this._tbr_Conv.AutoSize = false;
            this._tbr_Conv.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Conv.Image")));
            this._tbr_Conv.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Conv.Name = "_tbr_Conv";
            this._tbr_Conv.Size = new System.Drawing.Size(70, 70);
            this._tbr_Conv.Text = "Conveyors";
            this._tbr_Conv.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Conv.ToolTipText = "Conveyors and Index table";
            this._tbr_Conv.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_Robot
            // 
            this._tbr_Robot.AutoSize = false;
            this._tbr_Robot.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Robot.Image")));
            this._tbr_Robot.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Robot.Name = "_tbr_Robot";
            this._tbr_Robot.Size = new System.Drawing.Size(70, 70);
            this._tbr_Robot.Text = "Robot";
            this._tbr_Robot.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Robot.ToolTipText = "Robots";
            this._tbr_Robot.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_Camera
            // 
            this._tbr_Camera.AutoSize = false;
            this._tbr_Camera.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Camera.Image")));
            this._tbr_Camera.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Camera.Name = "_tbr_Camera";
            this._tbr_Camera.Size = new System.Drawing.Size(70, 70);
            this._tbr_Camera.Text = "Camera";
            this._tbr_Camera.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._tbr_Camera.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Camera.ToolTipText = "Cameras";
            this._tbr_Camera.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_Tray
            // 
            this._tbr_Tray.AutoSize = false;
            this._tbr_Tray.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Tray.Image")));
            this._tbr_Tray.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Tray.Name = "_tbr_Tray";
            this._tbr_Tray.Size = new System.Drawing.Size(70, 70);
            this._tbr_Tray.Text = "Trays";
            this._tbr_Tray.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._tbr_Tray.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Tray.ToolTipText = "Servise Trays";
            this._tbr_Tray.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_Carr
            // 
            this._tbr_Carr.AutoSize = false;
            this._tbr_Carr.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Carr.Image")));
            this._tbr_Carr.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Carr.Name = "_tbr_Carr";
            this._tbr_Carr.Size = new System.Drawing.Size(70, 70);
            this._tbr_Carr.Text = "Carrier";
            this._tbr_Carr.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Carr.ToolTipText = "Carrier";
            this._tbr_Carr.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_Order
            // 
            this._tbr_Order.AutoSize = false;
            this._tbr_Order.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Order.Image")));
            this._tbr_Order.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Order.Name = "_tbr_Order";
            this._tbr_Order.Size = new System.Drawing.Size(70, 70);
            this._tbr_Order.Text = "Order";
            this._tbr_Order.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Order.ToolTipText = "Edit Order";
            this._tbr_Order.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_PLC
            // 
            this._tbr_PLC.AutoSize = false;
            this._tbr_PLC.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_PLC.Image")));
            this._tbr_PLC.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_PLC.Name = "_tbr_PLC";
            this._tbr_PLC.Size = new System.Drawing.Size(70, 70);
            this._tbr_PLC.Text = "PLC";
            this._tbr_PLC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_PLC.ToolTipText = "PLC";
            this._tbr_PLC.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_OpenDoor
            // 
            this._tbr_OpenDoor.AutoSize = false;
            this._tbr_OpenDoor.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_OpenDoor.Image")));
            this._tbr_OpenDoor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_OpenDoor.ImageTransparentColor = System.Drawing.Color.Transparent;
            this._tbr_OpenDoor.Name = "_tbr_OpenDoor";
            this._tbr_OpenDoor.Size = new System.Drawing.Size(70, 70);
            this._tbr_OpenDoor.Text = "Door Locked";
            this._tbr_OpenDoor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_OpenDoor.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_ES_Rel
            // 
            this._tbr_ES_Rel.AutoSize = false;
            this._tbr_ES_Rel.BackColor = System.Drawing.Color.WhiteSmoke;
            this._tbr_ES_Rel.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_ES_Rel.Image")));
            this._tbr_ES_Rel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_ES_Rel.Name = "_tbr_ES_Rel";
            this._tbr_ES_Rel.Size = new System.Drawing.Size(70, 70);
            this._tbr_ES_Rel.Text = "E.S Release";
            this._tbr_ES_Rel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_ES_Rel.ToolTipText = "E.S Release";
            this._tbr_ES_Rel.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // _tbr_Exit
            // 
            this._tbr_Exit.AutoSize = false;
            this._tbr_Exit.Image = ((System.Drawing.Image)(resources.GetObject("_tbr_Exit.Image")));
            this._tbr_Exit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this._tbr_Exit.Name = "_tbr_Exit";
            this._tbr_Exit.Size = new System.Drawing.Size(70, 70);
            this._tbr_Exit.Text = "Exit";
            this._tbr_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tbr_Exit.ToolTipText = "Exit";
            this._tbr_Exit.Click += new System.EventHandler(this._tbr_button_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Image = global::Stahli2Robots.Properties.Resources.Shafir;
            this.toolStripLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(66, 70);
            // 
            // MDImain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(993, 659);
            this.Controls.Add(this.grpService);
            this.Controls.Add(this.lblTableMode);
            this.Controls.Add(this.cmbWarnnings);
            this.Controls.Add(this.tbr);
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "MDImain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stahli";
            this.Load += new System.EventHandler(this.Form_Load);
            this.grpService.ResumeLayout(false);
            this.grpService.PerformLayout();
            this.tbr.ResumeLayout(false);
            this.tbr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		[STAThread]
		static void Main() 
		{
            Application.Run(new MDImain());         
		}
		private void menuItemClose_Click(object sender, System.EventArgs e)
		{
			//Gets the currently active MDI child window.
			Form a = this.ActiveMdiChild;
			//Close the MDI child window
			a.Close();
		}		
		private void menuItemAI_Click(object sender, System.EventArgs e)
		{
			//Arrange MDI child icons within the client region of the MDI parent form.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
		}
		private void menuItemCas_Click(object sender, System.EventArgs e)
		{
			//Cascade all child forms.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
		}
		private void menuItemHoriz_Click(object sender, System.EventArgs e)
		{
			//Tile all child forms horizontally.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
		}
		private void menuItemVert_Click(object sender, System.EventArgs e)
		{			
			//Tile all child forms vertically.
			this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
		}
		private void menuItemMax_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Maximized
			foreach (Form chform in charr)
				chform.WindowState=FormWindowState.Maximized;
		}
		private void menuItemMin_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Minimized
			foreach (Form chform in charr)
				chform.WindowState=FormWindowState.Minimized;
		}
		private void menuItem1_Click(object sender, System.EventArgs e)
		{
			//Gets forms that represent the MDI child forms 
			//that are parented to this form in an array
			Form [] charr= this.MdiChildren;

			//for each child form set the window state to Minimized
			foreach (Form chform in charr)
				chform.Close();
		}        
        private void _tbr_button_Click(object sender, EventArgs e)
        {
            ToolStripButton toolStripButton = sender as ToolStripButton; 

             switch (toolStripButton.Name)
            {
                case "_tbr_Main":
                    break;
                case "_tbr_Conv":
                    AppGen.Inst.MDImain.frmAssemblies.RefreshForm();                  
                    frmAssemblies.Show();
                    frmAssemblies.BringToFront();
                    break;
                case "_tbr_Robot":
                    frmRobots.Show();
                    frmRobots.BringToFront();
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "13," + AppGen.Inst.MDImain.frmRobots.txtRobotSpeed.Text;
                    AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.RL.CmdMsg);
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "13," + AppGen.Inst.MDImain.frmRobots.txtRobotSpeed2.Text;
                    AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, AppGen.Inst.RobotConnection.RU.CmdMsg);
                    break;
                case "_tbr_Camera":
                   if (chkVisionON.Checked)
                    {
                        frmVisionMain.Show();
                        frmVisionMain.BringToFront();
                    }
                    break;
                case "_tbr_Tray":
                    //Process.Start("C:\\Project\\TrayBuild\\LappingTray.exe");                    
                    Process.Start("C:\\Project\\TBuilder\\ServiceTray\\ServTray.exe");                
                 break;
                case "_tbr_Carr":
                   //Process.Start("C:\\Project\\CarrierBuild\\TraysStahli.exe");
                 Process.Start("C:\\Project\\TBuilder\\CarrierTray\\TraysStahli.exe");  
                   break;
                case "_tbr_Order":
                    frmOrderEditor.Show();
                    frmOrderEditor.BringToFront();
                    break;
                case "_tbr_PLC":
                    ////EXAMPLE:  Process[] processList = Process.GetProcessesByName("notepad++");
///////test1///////---------------------------------------------------------------------------------------
                    //Process[] processList = Process.GetProcessesByName("twincut");
                    //if (processList.Count() > 0)
                    //{
                    //    //EXAMPLE:  IntPtr HWND = FindWindow(null, "notepad++");
                    //    IntPtr HWND = FindWindow(null, "twincut");
                    //    System.Threading.Thread.Sleep(1000);

                    //    ShowWindow(HWND, ProcessWindowStyle.Maximized);
                    //    EnableWindow(HWND, true);
                    //}
                    //else
                    //{
                    //   //EXAMPLE:  Process.Start("C:\\Project\\Stahli.Net 07.01.15\\Adept\\28.12.14\\Robot.lc");
                    //    Process.Start("C:\\Project\\PLC\\IS2244 PLC\\PLC\\PLC_HMI_V0.0.bat");
                    //}
///////test2///////---------------------------------------------------------------------------------------
                    ProcessStartInfo ProcessInfo;
                    Process process;      
                    ProcessInfo = new ProcessStartInfo("C:\\Project\\PLC\\IS2244 PLC\\PLC\\PLC_HMI_V0.0.bat");
                    ProcessInfo.CreateNoWindow = true;
                    ProcessInfo.UseShellExecute = false;                  
                    process = Process.Start(ProcessInfo);

///////test3///////---------------------------------------------------------------------------------------

                  //  System.Diagnostics.Process.Start("C:\\Project\\PLC\\IS2244 PLC\\PLC\\PLC_HMI_V0.0.bat");

                    break;
                case "_tbr_ES_Rel":
                    AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Req_EmergencyRelease = true;
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hReq_EmergencyRelease, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Req_EmergencyRelease);
                    break;
                case "_tbr_OpenDoor":
                    if (AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Req_Lock)
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Req_Lock = false;
                        AppGen.Inst.MDImain._tbr_OpenDoor.Checked = true;
                    }
                    else
                    {
                        AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Req_Lock = true;
                        AppGen.Inst.MDImain._tbr_OpenDoor.Checked = false;
                    }
                    AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hReq_Lock, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.Req_Lock);
                    break;
                case "_tbr_Exit":
                    AppGen.Inst.AppSettings.Serialize();
                    this.Close();
                    break;
                default:
                    break;
            }
        }
     
        // members (משתנים מקומיים)
       

        // properties (משתנים שניגשים אליהם מבחוץ)
        public FrmAssemblies frmAssemblies;
        public FrmBeckhoff frmBeckhoff;
        public FrmOrderEditor frmOrderEditor;
        public FrmRobots frmRobots;
        public FrmStahliIO frmStahliIO;
        public FrmTitle frmTitle;        
        public FrmVisionMain frmVisionMain;
        public FrmPlcErr frmPlcErr;
        public FrmTrayBuilder frmTrayBuilder;
        public FrmCarrierBuilder frmCarrierBuilder;
        public string LangFl;
      
        // delegates:
        private delegate void AfterModeChangedDelegate();
        private AfterModeChangedDelegate afterModeChangedDelegate;
        private void AfterModeChangedDelegateFunc()
        {
            try
            {
                AppGen.Inst.MDImain.frmTitle.AfterModeChanged();
                //ToolBar:
                tbr.Items["_tbr_robot"].Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
                tbr.Items["_tbr_Tray"].Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
                tbr.Items["_tbr_Carr"].Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
                tbr.Items["_tbr_Order"].Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
                tbr.Items["_tbr_PLC"].Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
                tbr.Items["_tbr_OpenDoor"].Enabled = (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Stop);
            }
            catch { }
        }
        public void AfterModeChanged()
        {
            try
            {
                this.Invoke(afterModeChangedDelegate);
            }
            catch { }
        }
        
        //form Events
        private void Form_Load(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain = this;
            WindowState = FormWindowState.Maximized;


            frmAssemblies = new FrmAssemblies();
            frmAssemblies.MdiParent = this;
            frmBeckhoff = new FrmBeckhoff();
            frmBeckhoff.MdiParent = this;
            frmOrderEditor = new FrmOrderEditor();
            frmOrderEditor.MdiParent = this;
            frmTrayBuilder = new FrmTrayBuilder();
            frmTrayBuilder.MdiParent = this;
            frmCarrierBuilder = new FrmCarrierBuilder();
            frmCarrierBuilder.MdiParent = this;
            frmRobots = new FrmRobots();
            frmRobots.MdiParent = this;
            frmStahliIO = new FrmStahliIO();
            frmStahliIO.MdiParent = this;
            frmTitle = new FrmTitle();
            frmTitle.MdiParent = this;
            frmPlcErr = new FrmPlcErr();
            frmPlcErr.MdiParent = this;

            if (chkVisionON.Checked)
            {
                frmVisionMain = new FrmVisionMain();
                frmVisionMain.MdiParent = this;
            }
            #region -- Translation Initialization --
            rm = new ResourceManager("Stahli2Robots.bin.Debug.Res", typeof(MDImain).Assembly);
            //ReadAndUpdate = new clsReadExelandUpdate(System.IO.Directory.GetCurrentDirectory() + "\\AppSetting\\", "TranslatControlTable.xls", this, rm, false);
            ReadAndUpdate = new clsReadExelandUpdate(@"C:\Users\asaff\Desktop", "TranslatControlTable.xls", this, rm, false);
            #endregion

            frmTitle.Show();
            
            //loading last Order:
            AppGen.Inst.AppSettings.DeSerialize();
            AppGen.Inst.MDImain.frmOrderEditor.LoadingOrder(AppGen.Inst.AppSettings.CurrentInsertCode , true);
            AppGen.Inst.OnInitApp();
            if (LangFl == "Eng")
            {
                //menuItem3.Checked = true;
                //menuItem4.Checked = false;
            }
            else
            {
                //menuItem3.Checked = false;
                //menuItem4.Checked = true;
            }
            

            // initial ALL:
            //init Robot Port:
            if (chkRobotsOn.Checked)
            {
                AppGen.Inst.RobotConnection.OpenRobotPorts();  
            }
            //init PLC:
            if (chkPlcON.Checked)
            {

            }
            //init Log File:
            if (chkErrLog.Checked)
            {

            }

            if (chkDebugMode.Checked)
            {
                AppGen.Inst.MDImain.frmTitle.txtStepLoad.Visible = true;
                AppGen.Inst.MDImain.frmTitle.txtStepUnload.Visible = true;
                AppGen.Inst.MDImain.frmTitle.cmdLoadCarrierReady.Visible = true;
                AppGen.Inst.MDImain.frmTitle.cmdLoadTrayReady.Visible = true;
                AppGen.Inst.MDImain.frmTitle.cmdUnloadCarrierReady.Visible = true;
                AppGen.Inst.MDImain.frmTitle.cmdUnloadTrayReady.Visible = true;

                AppGen.Inst.MDImain.frmTitle.picClearToSnap1.Visible = true;
                AppGen.Inst.MDImain.frmTitle.picClearToSnap2.Visible = true;
                AppGen.Inst.MDImain.frmTitle.picClearToSnap3.Visible = true;

                AppGen.Inst.MDImain.frmTitle.cmdDebugLoadingCycle.Visible = true;
                AppGen.Inst.MDImain.frmTitle.cmdDebugUnloadingCycle.Visible = true;

                AppGen.Inst.MDImain.frmTitle.grpDebug.Visible = true;
            }
            if (chkWithStacker.Checked)
            {
              //  AppGen.Inst.MDImain.frmTitle.picStacker.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeCoverStacker.Visible = false;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState31.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState32.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState33.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState34.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState35.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState36.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState37.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState38.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState39.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState40.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState41.Visible = true;
                AppGen.Inst.MDImain.frmTitle.SapeSliceState42.Visible = true;
            }
        }

        void MDImain_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppGen.Inst.OnClosingApp();
        }
        private void menuItem3_Click(object sender, EventArgs e)
        {
            LangFl = "Eng";
            //menuItem3.Checked = true;
            //menuItem4.Checked = false;
        }
        private void menuItem4_Click(object sender, EventArgs e)
        {
            LangFl = "Heb";
            //menuItem3.Checked = false;
            //menuItem4.Checked = true;
        }
        private void menuItem6_Click(object sender, EventArgs e)
        {
            ReadAndUpdate.WriteControlsNameToXls();
        }
        private void menuItem7_Click(object sender, EventArgs e)
        {
            ReadAndUpdate.UpdateExelData();
        }
        private void menuItem8_Click(object sender, EventArgs e)
        {
            ReadAndUpdate.UpdateUI();
        }
    }
}
