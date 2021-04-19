using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Stahli2Robots
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class FrmRobots : System.Windows.Forms.Form
    {
        #region designer
        private TabControl tabControl1;
        private TabPage tabUnloadRobot;
        private TabPage tabLoadRobot;
        private ListBox lstOutputs;
        private Button cmdDoutOFF;
        private Button cmdDoutOn;
        public TextBox txtRobotSpeed;
        private Button cmdRobotSpeed;
        private Button cmdRobotManu;
        private GroupBox grbHeightStation;
        private Button cmdChangeGripper;
        private Button cmdReadRobotParam;
        private ListBox lstInputs;
        private Button cmdChkInput;
        private Button cmdReleaseInsert;
        private Button cmdHoldInsert;
        private Button cmdSaveRobotParam;
        private Button cmdRobotReset;
        private Label label1;
        public TextBox txtCurrentHeight;
        public TextBox txtCorrectVal;
        private RadioButton OptTrayHeight;
        private RadioButton OptIndexTable;
        private Button cmdChangeHeight;
        private Button cmdReadHeight;
        private Label label2;
        public TextBox txtRobotSpeed2;
        private GroupBox groupBox1;
        private Label label5;
        private Label label6;
        public TextBox txtCurrentHeight2;
        public TextBox txtCorrectVal2;
        private RadioButton OptTrayHeight2;
        private RadioButton OptIndexTable2;
        private Button cmdChangeHeight2;
        private Button cmdReadHeight2;
        private Button cmdChangeGripper2;
        private Button cmdReadRobotParam2;
        private ListBox lstInputs2;
        private Button cmdChkInput2;
        private Button cmdReleaseInsert2;
        private Button cmdHoldInsert2;
        private Button cmdSaveRobotParam2;
        private Button cmdRobotReset2;
        private ListBox lstOutputs2;
        private Button cmdDoutOFF2;
        private Button cmdDoutOn2;
        private Button cmdRobotSpeed2;
        private Button cmdRobotManu2;
        private Panel panel1;
        private GroupBox RobotCommunication;
        public ListBox lstGetMsg;
        public ListBox lstSendMsg;
        public Label lblWait;
        public TextBox txtGetMsg;
        public TextBox txtSendMsg;
        private Label label4;
        private Label label3;
        private Button cmdSendComm;
        private Button cmdStopWaiting;
        private Panel panel2;
        private GroupBox RobotCommunication2;
        public ListBox lstGetMsg2;
        public ListBox lstSendMsg2;
        public Label lblWait2;
        public TextBox txtGetMsg2;
        public TextBox txtSendMsg2;
        private Label label8;
        private Label label9;
        private Button cmdSendComm2;
        private Button cmdStopWaiting2;
        private GroupBox groupBox2;
        private Button cmdSingleCyc;
        private GroupBox groupBox4;
        private Button cmdSingleCyc2;
        private Button cmdTestAutoCycle;
        private Button cmdReadCarrierFile;
        private Button cmdStop;
#endregion
        private Button cmdReadCalibPoints;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        public Microsoft.VisualBasic.PowerPacks.OvalShape SapeRobotInputState;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer2;
        public Microsoft.VisualBasic.PowerPacks.OvalShape SapeRobotInputState2;
        private GroupBox groupBox3;
        private Label label11;
        private Label label10;
        private Label label7;
        private TextBox txtPlaceMaesureOffsetA;
        private TextBox txtPlaceMaesureOffsetY;
        private TextBox txtPlaceMaesureOffsetX;
        private Button cmdMasureOffset;
        private Label label12;
        private TextBox txtPlaceMaesureOffsetZ;
        private Button btnTeachTableHeight2;
        private Button btnTeachTableHeight;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmRobots()
		{
			InitializeComponent();
            // delegats:
            setBackColorCommDelegate = new SetBackColorCommDelegate(SetBackColorCommDelegateFunc);  
            showCommStringDelegate = new ShowCommStringDelegate(ShowCommStringDelegateFunc);
            updateFrmRobotDelegate = new UpdateFrmRobotDelegate(UpdateFrmRobotDelegateFunc);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
           
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRobots));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabUnloadRobot = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPlaceMaesureOffsetZ = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPlaceMaesureOffsetA = new System.Windows.Forms.TextBox();
            this.txtPlaceMaesureOffsetY = new System.Windows.Forms.TextBox();
            this.txtPlaceMaesureOffsetX = new System.Windows.Forms.TextBox();
            this.cmdMasureOffset = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmdSingleCyc2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RobotCommunication2 = new System.Windows.Forms.GroupBox();
            this.lstGetMsg2 = new System.Windows.Forms.ListBox();
            this.lstSendMsg2 = new System.Windows.Forms.ListBox();
            this.lblWait2 = new System.Windows.Forms.Label();
            this.txtGetMsg2 = new System.Windows.Forms.TextBox();
            this.txtSendMsg2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdSendComm2 = new System.Windows.Forms.Button();
            this.cmdStopWaiting2 = new System.Windows.Forms.Button();
            this.txtRobotSpeed2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCurrentHeight2 = new System.Windows.Forms.TextBox();
            this.txtCorrectVal2 = new System.Windows.Forms.TextBox();
            this.OptTrayHeight2 = new System.Windows.Forms.RadioButton();
            this.OptIndexTable2 = new System.Windows.Forms.RadioButton();
            this.cmdChangeHeight2 = new System.Windows.Forms.Button();
            this.cmdReadHeight2 = new System.Windows.Forms.Button();
            this.cmdChangeGripper2 = new System.Windows.Forms.Button();
            this.cmdReadRobotParam2 = new System.Windows.Forms.Button();
            this.lstInputs2 = new System.Windows.Forms.ListBox();
            this.cmdReleaseInsert2 = new System.Windows.Forms.Button();
            this.cmdHoldInsert2 = new System.Windows.Forms.Button();
            this.cmdSaveRobotParam2 = new System.Windows.Forms.Button();
            this.cmdRobotReset2 = new System.Windows.Forms.Button();
            this.lstOutputs2 = new System.Windows.Forms.ListBox();
            this.cmdDoutOFF2 = new System.Windows.Forms.Button();
            this.cmdDoutOn2 = new System.Windows.Forms.Button();
            this.cmdRobotSpeed2 = new System.Windows.Forms.Button();
            this.cmdRobotManu2 = new System.Windows.Forms.Button();
            this.shapeContainer2 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.SapeRobotInputState2 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.cmdChkInput2 = new System.Windows.Forms.Button();
            this.tabLoadRobot = new System.Windows.Forms.TabPage();
            this.cmdReadCalibPoints = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdReadCarrierFile = new System.Windows.Forms.Button();
            this.cmdTestAutoCycle = new System.Windows.Forms.Button();
            this.cmdSingleCyc = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RobotCommunication = new System.Windows.Forms.GroupBox();
            this.lstGetMsg = new System.Windows.Forms.ListBox();
            this.lstSendMsg = new System.Windows.Forms.ListBox();
            this.lblWait = new System.Windows.Forms.Label();
            this.txtGetMsg = new System.Windows.Forms.TextBox();
            this.txtSendMsg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdSendComm = new System.Windows.Forms.Button();
            this.cmdStopWaiting = new System.Windows.Forms.Button();
            this.grbHeightStation = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCurrentHeight = new System.Windows.Forms.TextBox();
            this.txtCorrectVal = new System.Windows.Forms.TextBox();
            this.OptTrayHeight = new System.Windows.Forms.RadioButton();
            this.OptIndexTable = new System.Windows.Forms.RadioButton();
            this.cmdChangeHeight = new System.Windows.Forms.Button();
            this.cmdReadHeight = new System.Windows.Forms.Button();
            this.cmdChangeGripper = new System.Windows.Forms.Button();
            this.cmdReadRobotParam = new System.Windows.Forms.Button();
            this.lstInputs = new System.Windows.Forms.ListBox();
            this.cmdReleaseInsert = new System.Windows.Forms.Button();
            this.cmdHoldInsert = new System.Windows.Forms.Button();
            this.cmdSaveRobotParam = new System.Windows.Forms.Button();
            this.cmdRobotReset = new System.Windows.Forms.Button();
            this.lstOutputs = new System.Windows.Forms.ListBox();
            this.cmdDoutOFF = new System.Windows.Forms.Button();
            this.cmdDoutOn = new System.Windows.Forms.Button();
            this.txtRobotSpeed = new System.Windows.Forms.TextBox();
            this.cmdRobotSpeed = new System.Windows.Forms.Button();
            this.cmdRobotManu = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.SapeRobotInputState = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.cmdChkInput = new System.Windows.Forms.Button();
            this.btnTeachTableHeight2 = new System.Windows.Forms.Button();
            this.btnTeachTableHeight = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabUnloadRobot.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.RobotCommunication2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabLoadRobot.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.RobotCommunication.SuspendLayout();
            this.grbHeightStation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabUnloadRobot);
            this.tabControl1.Controls.Add(this.tabLoadRobot);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 12F);
            this.tabControl1.ItemSize = new System.Drawing.Size(130, 23);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(756, 628);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // tabUnloadRobot
            // 
            this.tabUnloadRobot.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.tabUnloadRobot.Controls.Add(this.btnTeachTableHeight2);
            this.tabUnloadRobot.Controls.Add(this.groupBox3);
            this.tabUnloadRobot.Controls.Add(this.groupBox4);
            this.tabUnloadRobot.Controls.Add(this.panel2);
            this.tabUnloadRobot.Controls.Add(this.txtRobotSpeed2);
            this.tabUnloadRobot.Controls.Add(this.groupBox1);
            this.tabUnloadRobot.Controls.Add(this.cmdChangeGripper2);
            this.tabUnloadRobot.Controls.Add(this.cmdReadRobotParam2);
            this.tabUnloadRobot.Controls.Add(this.lstInputs2);
            this.tabUnloadRobot.Controls.Add(this.cmdReleaseInsert2);
            this.tabUnloadRobot.Controls.Add(this.cmdHoldInsert2);
            this.tabUnloadRobot.Controls.Add(this.cmdSaveRobotParam2);
            this.tabUnloadRobot.Controls.Add(this.cmdRobotReset2);
            this.tabUnloadRobot.Controls.Add(this.lstOutputs2);
            this.tabUnloadRobot.Controls.Add(this.cmdDoutOFF2);
            this.tabUnloadRobot.Controls.Add(this.cmdDoutOn2);
            this.tabUnloadRobot.Controls.Add(this.cmdRobotSpeed2);
            this.tabUnloadRobot.Controls.Add(this.cmdRobotManu2);
            this.tabUnloadRobot.Controls.Add(this.shapeContainer2);
            this.tabUnloadRobot.Controls.Add(this.cmdChkInput2);
            this.tabUnloadRobot.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabUnloadRobot.Location = new System.Drawing.Point(4, 27);
            this.tabUnloadRobot.Name = "tabUnloadRobot";
            this.tabUnloadRobot.Padding = new System.Windows.Forms.Padding(3);
            this.tabUnloadRobot.Size = new System.Drawing.Size(748, 597);
            this.tabUnloadRobot.TabIndex = 0;
            this.tabUnloadRobot.Text = "Unload Robot";
            this.tabUnloadRobot.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtPlaceMaesureOffsetZ);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtPlaceMaesureOffsetA);
            this.groupBox3.Controls.Add(this.txtPlaceMaesureOffsetY);
            this.groupBox3.Controls.Add(this.txtPlaceMaesureOffsetX);
            this.groupBox3.Controls.Add(this.cmdMasureOffset);
            this.groupBox3.Location = new System.Drawing.Point(399, 294);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(225, 90);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label12.Location = new System.Drawing.Point(151, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 16);
            this.label12.TabIndex = 49;
            this.label12.Text = "Z";
            // 
            // txtPlaceMaesureOffsetZ
            // 
            this.txtPlaceMaesureOffsetZ.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaceMaesureOffsetZ.Location = new System.Drawing.Point(116, 24);
            this.txtPlaceMaesureOffsetZ.Name = "txtPlaceMaesureOffsetZ";
            this.txtPlaceMaesureOffsetZ.Size = new System.Drawing.Size(34, 22);
            this.txtPlaceMaesureOffsetZ.TabIndex = 48;
            this.txtPlaceMaesureOffsetZ.Text = "0.0";
            this.txtPlaceMaesureOffsetZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label11.Location = new System.Drawing.Point(205, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 16);
            this.label11.TabIndex = 47;
            this.label11.Text = "A";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label10.Location = new System.Drawing.Point(96, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 16);
            this.label10.TabIndex = 46;
            this.label10.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label7.Location = new System.Drawing.Point(41, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 16);
            this.label7.TabIndex = 45;
            this.label7.Text = "X";
            // 
            // txtPlaceMaesureOffsetA
            // 
            this.txtPlaceMaesureOffsetA.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaceMaesureOffsetA.Location = new System.Drawing.Point(171, 24);
            this.txtPlaceMaesureOffsetA.Name = "txtPlaceMaesureOffsetA";
            this.txtPlaceMaesureOffsetA.Size = new System.Drawing.Size(34, 22);
            this.txtPlaceMaesureOffsetA.TabIndex = 44;
            this.txtPlaceMaesureOffsetA.Text = "0.0";
            this.txtPlaceMaesureOffsetA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPlaceMaesureOffsetY
            // 
            this.txtPlaceMaesureOffsetY.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaceMaesureOffsetY.Location = new System.Drawing.Point(61, 24);
            this.txtPlaceMaesureOffsetY.Name = "txtPlaceMaesureOffsetY";
            this.txtPlaceMaesureOffsetY.Size = new System.Drawing.Size(34, 22);
            this.txtPlaceMaesureOffsetY.TabIndex = 43;
            this.txtPlaceMaesureOffsetY.Text = "0.0";
            this.txtPlaceMaesureOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPlaceMaesureOffsetX
            // 
            this.txtPlaceMaesureOffsetX.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlaceMaesureOffsetX.Location = new System.Drawing.Point(6, 24);
            this.txtPlaceMaesureOffsetX.Name = "txtPlaceMaesureOffsetX";
            this.txtPlaceMaesureOffsetX.Size = new System.Drawing.Size(34, 22);
            this.txtPlaceMaesureOffsetX.TabIndex = 42;
            this.txtPlaceMaesureOffsetX.Text = "0.0";
            this.txtPlaceMaesureOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdMasureOffset
            // 
            this.cmdMasureOffset.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdMasureOffset.Location = new System.Drawing.Point(3, 50);
            this.cmdMasureOffset.Name = "cmdMasureOffset";
            this.cmdMasureOffset.Size = new System.Drawing.Size(216, 34);
            this.cmdMasureOffset.TabIndex = 41;
            this.cmdMasureOffset.Text = "Send Measure Point offset";
            this.cmdMasureOffset.UseVisualStyleBackColor = true;
            this.cmdMasureOffset.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmdSingleCyc2);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 12F);
            this.groupBox4.Location = new System.Drawing.Point(10, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(268, 91);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Develop Tools (Unloading-Robot) :";
            this.groupBox4.Visible = false;
            // 
            // cmdSingleCyc2
            // 
            this.cmdSingleCyc2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdSingleCyc2.Location = new System.Drawing.Point(7, 21);
            this.cmdSingleCyc2.Name = "cmdSingleCyc2";
            this.cmdSingleCyc2.Size = new System.Drawing.Size(142, 29);
            this.cmdSingleCyc2.TabIndex = 4;
            this.cmdSingleCyc2.Text = "Single cycle by vision";
            this.cmdSingleCyc2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RobotCommunication2);
            this.panel2.Location = new System.Drawing.Point(1, 480);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(739, 114);
            this.panel2.TabIndex = 30;
            // 
            // RobotCommunication2
            // 
            this.RobotCommunication2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.RobotCommunication2.Controls.Add(this.lstGetMsg2);
            this.RobotCommunication2.Controls.Add(this.lstSendMsg2);
            this.RobotCommunication2.Controls.Add(this.lblWait2);
            this.RobotCommunication2.Controls.Add(this.txtGetMsg2);
            this.RobotCommunication2.Controls.Add(this.txtSendMsg2);
            this.RobotCommunication2.Controls.Add(this.label8);
            this.RobotCommunication2.Controls.Add(this.label9);
            this.RobotCommunication2.Controls.Add(this.cmdSendComm2);
            this.RobotCommunication2.Controls.Add(this.cmdStopWaiting2);
            this.RobotCommunication2.Font = new System.Drawing.Font("Arial", 12F);
            this.RobotCommunication2.Location = new System.Drawing.Point(5, 7);
            this.RobotCommunication2.Name = "RobotCommunication2";
            this.RobotCommunication2.Size = new System.Drawing.Size(731, 102);
            this.RobotCommunication2.TabIndex = 16;
            this.RobotCommunication2.TabStop = false;
            this.RobotCommunication2.Text = "Unload Robot Communication";
            // 
            // lstGetMsg2
            // 
            this.lstGetMsg2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstGetMsg2.FormattingEnabled = true;
            this.lstGetMsg2.ItemHeight = 14;
            this.lstGetMsg2.Location = new System.Drawing.Point(424, 64);
            this.lstGetMsg2.Name = "lstGetMsg2";
            this.lstGetMsg2.Size = new System.Drawing.Size(297, 32);
            this.lstGetMsg2.TabIndex = 10;
            // 
            // lstSendMsg2
            // 
            this.lstSendMsg2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstSendMsg2.FormattingEnabled = true;
            this.lstSendMsg2.ItemHeight = 14;
            this.lstSendMsg2.Location = new System.Drawing.Point(424, 22);
            this.lstSendMsg2.Name = "lstSendMsg2";
            this.lstSendMsg2.Size = new System.Drawing.Size(297, 32);
            this.lstSendMsg2.TabIndex = 9;
            // 
            // lblWait2
            // 
            this.lblWait2.BackColor = System.Drawing.Color.LimeGreen;
            this.lblWait2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWait2.Location = new System.Drawing.Point(382, 67);
            this.lblWait2.Name = "lblWait2";
            this.lblWait2.Size = new System.Drawing.Size(36, 24);
            this.lblWait2.TabIndex = 8;
            // 
            // txtGetMsg2
            // 
            this.txtGetMsg2.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtGetMsg2.Location = new System.Drawing.Point(65, 69);
            this.txtGetMsg2.Name = "txtGetMsg2";
            this.txtGetMsg2.Size = new System.Drawing.Size(217, 22);
            this.txtGetMsg2.TabIndex = 7;
            // 
            // txtSendMsg2
            // 
            this.txtSendMsg2.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtSendMsg2.Location = new System.Drawing.Point(65, 27);
            this.txtSendMsg2.Name = "txtSendMsg2";
            this.txtSendMsg2.Size = new System.Drawing.Size(279, 22);
            this.txtSendMsg2.TabIndex = 6;
            this.txtSendMsg2.Text = "cmd10,97,537,0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label8.Location = new System.Drawing.Point(7, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 16);
            this.label8.TabIndex = 5;
            this.label8.Text = "Get-->";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label9.Location = new System.Drawing.Point(6, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 16);
            this.label9.TabIndex = 4;
            this.label9.Text = "Send-->";
            // 
            // cmdSendComm2
            // 
            this.cmdSendComm2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdSendComm2.Location = new System.Drawing.Point(357, 26);
            this.cmdSendComm2.Name = "cmdSendComm2";
            this.cmdSendComm2.Size = new System.Drawing.Size(61, 24);
            this.cmdSendComm2.TabIndex = 3;
            this.cmdSendComm2.Text = "Send";
            this.cmdSendComm2.UseVisualStyleBackColor = true;
            this.cmdSendComm2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdStopWaiting2
            // 
            this.cmdStopWaiting2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdStopWaiting2.Location = new System.Drawing.Point(288, 67);
            this.cmdStopWaiting2.Name = "cmdStopWaiting2";
            this.cmdStopWaiting2.Size = new System.Drawing.Size(88, 24);
            this.cmdStopWaiting2.TabIndex = 2;
            this.cmdStopWaiting2.Text = "Stop waiting";
            this.cmdStopWaiting2.UseVisualStyleBackColor = true;
            this.cmdStopWaiting2.Click += new System.EventHandler(this.cmdStopWaiting_Click);
            // 
            // txtRobotSpeed2
            // 
            this.txtRobotSpeed2.Font = new System.Drawing.Font("Arial", 12F);
            this.txtRobotSpeed2.Location = new System.Drawing.Point(278, 113);
            this.txtRobotSpeed2.Name = "txtRobotSpeed2";
            this.txtRobotSpeed2.Size = new System.Drawing.Size(48, 26);
            this.txtRobotSpeed2.TabIndex = 29;
            this.txtRobotSpeed2.Text = "25";
            this.txtRobotSpeed2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtCurrentHeight2);
            this.groupBox1.Controls.Add(this.txtCorrectVal2);
            this.groupBox1.Controls.Add(this.OptTrayHeight2);
            this.groupBox1.Controls.Add(this.OptIndexTable2);
            this.groupBox1.Controls.Add(this.cmdChangeHeight2);
            this.groupBox1.Controls.Add(this.cmdReadHeight2);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F);
            this.groupBox1.Location = new System.Drawing.Point(8, 386);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 83);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Height Robot stations:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.Location = new System.Drawing.Point(116, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "CorrectionValue";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "current Height";
            // 
            // txtCurrentHeight2
            // 
            this.txtCurrentHeight2.Enabled = false;
            this.txtCurrentHeight2.Location = new System.Drawing.Point(6, 46);
            this.txtCurrentHeight2.Name = "txtCurrentHeight2";
            this.txtCurrentHeight2.Size = new System.Drawing.Size(104, 26);
            this.txtCurrentHeight2.TabIndex = 5;
            this.txtCurrentHeight2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCorrectVal2
            // 
            this.txtCorrectVal2.Location = new System.Drawing.Point(118, 46);
            this.txtCorrectVal2.Name = "txtCorrectVal2";
            this.txtCorrectVal2.Size = new System.Drawing.Size(104, 26);
            this.txtCorrectVal2.TabIndex = 4;
            this.txtCorrectVal2.Text = "0.1";
            this.txtCorrectVal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OptTrayHeight2
            // 
            this.OptTrayHeight2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OptTrayHeight2.Location = new System.Drawing.Point(313, 47);
            this.OptTrayHeight2.Name = "OptTrayHeight2";
            this.OptTrayHeight2.Size = new System.Drawing.Size(120, 22);
            this.OptTrayHeight2.TabIndex = 3;
            this.OptTrayHeight2.Tag = ".";
            this.OptTrayHeight2.Text = "Tray Height";
            this.OptTrayHeight2.UseVisualStyleBackColor = true;
            this.OptTrayHeight2.CheckedChanged += new System.EventHandler(this.OptTrayHeight2_CheckedChanged);
            // 
            // OptIndexTable2
            // 
            this.OptIndexTable2.Checked = true;
            this.OptIndexTable2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OptIndexTable2.Location = new System.Drawing.Point(313, 21);
            this.OptIndexTable2.Name = "OptIndexTable2";
            this.OptIndexTable2.Size = new System.Drawing.Size(140, 22);
            this.OptIndexTable2.TabIndex = 2;
            this.OptIndexTable2.TabStop = true;
            this.OptIndexTable2.Text = "Carrier Height";
            this.OptIndexTable2.UseVisualStyleBackColor = true;
            this.OptIndexTable2.CheckedChanged += new System.EventHandler(this.OptIndexTable2_CheckedChanged);
            // 
            // cmdChangeHeight2
            // 
            this.cmdChangeHeight2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdChangeHeight2.Location = new System.Drawing.Point(476, 21);
            this.cmdChangeHeight2.Name = "cmdChangeHeight2";
            this.cmdChangeHeight2.Size = new System.Drawing.Size(78, 47);
            this.cmdChangeHeight2.TabIndex = 1;
            this.cmdChangeHeight2.Text = "Change Value";
            this.cmdChangeHeight2.UseVisualStyleBackColor = true;
            this.cmdChangeHeight2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdReadHeight2
            // 
            this.cmdReadHeight2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdReadHeight2.Location = new System.Drawing.Point(560, 21);
            this.cmdReadHeight2.Name = "cmdReadHeight2";
            this.cmdReadHeight2.Size = new System.Drawing.Size(78, 47);
            this.cmdReadHeight2.TabIndex = 0;
            this.cmdReadHeight2.Text = "Read Value";
            this.cmdReadHeight2.UseVisualStyleBackColor = true;
            this.cmdReadHeight2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdChangeGripper2
            // 
            this.cmdChangeGripper2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdChangeGripper2.Location = new System.Drawing.Point(410, 259);
            this.cmdChangeGripper2.Name = "cmdChangeGripper2";
            this.cmdChangeGripper2.Size = new System.Drawing.Size(205, 34);
            this.cmdChangeGripper2.TabIndex = 27;
            this.cmdChangeGripper2.Text = "Change Gripper";
            this.cmdChangeGripper2.UseVisualStyleBackColor = true;
            this.cmdChangeGripper2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdReadRobotParam2
            // 
            this.cmdReadRobotParam2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdReadRobotParam2.Location = new System.Drawing.Point(126, 367);
            this.cmdReadRobotParam2.Name = "cmdReadRobotParam2";
            this.cmdReadRobotParam2.Size = new System.Drawing.Size(205, 34);
            this.cmdReadRobotParam2.TabIndex = 26;
            this.cmdReadRobotParam2.Text = "Read Parameters";
            this.cmdReadRobotParam2.UseVisualStyleBackColor = true;
            this.cmdReadRobotParam2.Visible = false;
            this.cmdReadRobotParam2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // lstInputs2
            // 
            this.lstInputs2.Font = new System.Drawing.Font("Arial", 12F);
            this.lstInputs2.FormattingEnabled = true;
            this.lstInputs2.ItemHeight = 18;
            this.lstInputs2.Items.AddRange(new object[] {
            "Inputs Descriptions",
            "griper Vucc",
            "Insert sensor"});
            this.lstInputs2.Location = new System.Drawing.Point(410, 229);
            this.lstInputs2.Name = "lstInputs2";
            this.lstInputs2.Size = new System.Drawing.Size(205, 22);
            this.lstInputs2.TabIndex = 25;
            // 
            // cmdReleaseInsert2
            // 
            this.cmdReleaseInsert2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdReleaseInsert2.Location = new System.Drawing.Point(410, 149);
            this.cmdReleaseInsert2.Name = "cmdReleaseInsert2";
            this.cmdReleaseInsert2.Size = new System.Drawing.Size(205, 34);
            this.cmdReleaseInsert2.TabIndex = 23;
            this.cmdReleaseInsert2.Text = "Release Insert";
            this.cmdReleaseInsert2.UseVisualStyleBackColor = true;
            this.cmdReleaseInsert2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdHoldInsert2
            // 
            this.cmdHoldInsert2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdHoldInsert2.Location = new System.Drawing.Point(410, 109);
            this.cmdHoldInsert2.Name = "cmdHoldInsert2";
            this.cmdHoldInsert2.Size = new System.Drawing.Size(205, 34);
            this.cmdHoldInsert2.TabIndex = 22;
            this.cmdHoldInsert2.Text = "Hold Insert";
            this.cmdHoldInsert2.UseVisualStyleBackColor = true;
            this.cmdHoldInsert2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdSaveRobotParam2
            // 
            this.cmdSaveRobotParam2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdSaveRobotParam2.Location = new System.Drawing.Point(130, 299);
            this.cmdSaveRobotParam2.Name = "cmdSaveRobotParam2";
            this.cmdSaveRobotParam2.Size = new System.Drawing.Size(205, 34);
            this.cmdSaveRobotParam2.TabIndex = 21;
            this.cmdSaveRobotParam2.Text = "Save Parameters";
            this.cmdSaveRobotParam2.UseVisualStyleBackColor = true;
            this.cmdSaveRobotParam2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdRobotReset2
            // 
            this.cmdRobotReset2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdRobotReset2.ForeColor = System.Drawing.Color.Red;
            this.cmdRobotReset2.Location = new System.Drawing.Point(130, 259);
            this.cmdRobotReset2.Name = "cmdRobotReset2";
            this.cmdRobotReset2.Size = new System.Drawing.Size(205, 34);
            this.cmdRobotReset2.TabIndex = 20;
            this.cmdRobotReset2.Text = "Robot Reset";
            this.cmdRobotReset2.UseVisualStyleBackColor = true;
            this.cmdRobotReset2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // lstOutputs2
            // 
            this.lstOutputs2.Font = new System.Drawing.Font("Arial", 12F);
            this.lstOutputs2.FormattingEnabled = true;
            this.lstOutputs2.ItemHeight = 18;
            this.lstOutputs2.Items.AddRange(new object[] {
            "Outputs Descriptions",
            "griper Vucc",
            "Insert sensor"});
            this.lstOutputs2.Location = new System.Drawing.Point(130, 229);
            this.lstOutputs2.Name = "lstOutputs2";
            this.lstOutputs2.Size = new System.Drawing.Size(205, 22);
            this.lstOutputs2.TabIndex = 19;
            // 
            // cmdDoutOFF2
            // 
            this.cmdDoutOFF2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdDoutOFF2.Location = new System.Drawing.Point(130, 189);
            this.cmdDoutOFF2.Name = "cmdDoutOFF2";
            this.cmdDoutOFF2.Size = new System.Drawing.Size(205, 34);
            this.cmdDoutOFF2.TabIndex = 18;
            this.cmdDoutOFF2.Text = "Output OFF";
            this.cmdDoutOFF2.UseVisualStyleBackColor = true;
            this.cmdDoutOFF2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdDoutOn2
            // 
            this.cmdDoutOn2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdDoutOn2.Location = new System.Drawing.Point(130, 149);
            this.cmdDoutOn2.Name = "cmdDoutOn2";
            this.cmdDoutOn2.Size = new System.Drawing.Size(205, 34);
            this.cmdDoutOn2.TabIndex = 17;
            this.cmdDoutOn2.Text = "Output ON";
            this.cmdDoutOn2.UseVisualStyleBackColor = true;
            this.cmdDoutOn2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdRobotSpeed2
            // 
            this.cmdRobotSpeed2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdRobotSpeed2.Location = new System.Drawing.Point(130, 109);
            this.cmdRobotSpeed2.Name = "cmdRobotSpeed2";
            this.cmdRobotSpeed2.Size = new System.Drawing.Size(205, 34);
            this.cmdRobotSpeed2.TabIndex = 16;
            this.cmdRobotSpeed2.Text = "Robot Speed";
            this.cmdRobotSpeed2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRobotSpeed2.UseVisualStyleBackColor = true;
            this.cmdRobotSpeed2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // cmdRobotManu2
            // 
            this.cmdRobotManu2.Font = new System.Drawing.Font("Arial", 14.25F);
            this.cmdRobotManu2.ForeColor = System.Drawing.Color.Navy;
            this.cmdRobotManu2.Image = ((System.Drawing.Image)(resources.GetObject("cmdRobotManu2.Image")));
            this.cmdRobotManu2.Location = new System.Drawing.Point(292, 6);
            this.cmdRobotManu2.Name = "cmdRobotManu2";
            this.cmdRobotManu2.Size = new System.Drawing.Size(161, 97);
            this.cmdRobotManu2.TabIndex = 15;
            this.cmdRobotManu2.Text = "Robot Menu";
            this.cmdRobotManu2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdRobotManu2.UseVisualStyleBackColor = true;
            this.cmdRobotManu2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // shapeContainer2
            // 
            this.shapeContainer2.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer2.Name = "shapeContainer2";
            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.SapeRobotInputState2});
            this.shapeContainer2.Size = new System.Drawing.Size(742, 591);
            this.shapeContainer2.TabIndex = 33;
            this.shapeContainer2.TabStop = false;
            // 
            // SapeRobotInputState2
            // 
            this.SapeRobotInputState2.BackgroundImage = global::Stahli2Robots.Properties.Resources.circle_grey01;
            this.SapeRobotInputState2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SapeRobotInputState2.Location = new System.Drawing.Point(578, 190);
            this.SapeRobotInputState2.Name = "SapeRobotInputState2";
            this.SapeRobotInputState2.Size = new System.Drawing.Size(25, 25);
            // 
            // cmdChkInput2
            // 
            this.cmdChkInput2.Font = new System.Drawing.Font("Arial", 12F);
            this.cmdChkInput2.Location = new System.Drawing.Point(410, 189);
            this.cmdChkInput2.Name = "cmdChkInput2";
            this.cmdChkInput2.Size = new System.Drawing.Size(160, 34);
            this.cmdChkInput2.TabIndex = 24;
            this.cmdChkInput2.Text = "Check Input Status";
            this.cmdChkInput2.UseVisualStyleBackColor = true;
            this.cmdChkInput2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // tabLoadRobot
            // 
            this.tabLoadRobot.Controls.Add(this.btnTeachTableHeight);
            this.tabLoadRobot.Controls.Add(this.cmdReadCalibPoints);
            this.tabLoadRobot.Controls.Add(this.groupBox2);
            this.tabLoadRobot.Controls.Add(this.panel1);
            this.tabLoadRobot.Controls.Add(this.grbHeightStation);
            this.tabLoadRobot.Controls.Add(this.cmdChangeGripper);
            this.tabLoadRobot.Controls.Add(this.cmdReadRobotParam);
            this.tabLoadRobot.Controls.Add(this.lstInputs);
            this.tabLoadRobot.Controls.Add(this.cmdReleaseInsert);
            this.tabLoadRobot.Controls.Add(this.cmdHoldInsert);
            this.tabLoadRobot.Controls.Add(this.cmdSaveRobotParam);
            this.tabLoadRobot.Controls.Add(this.cmdRobotReset);
            this.tabLoadRobot.Controls.Add(this.lstOutputs);
            this.tabLoadRobot.Controls.Add(this.cmdDoutOFF);
            this.tabLoadRobot.Controls.Add(this.cmdDoutOn);
            this.tabLoadRobot.Controls.Add(this.txtRobotSpeed);
            this.tabLoadRobot.Controls.Add(this.cmdRobotSpeed);
            this.tabLoadRobot.Controls.Add(this.cmdRobotManu);
            this.tabLoadRobot.Controls.Add(this.shapeContainer1);
            this.tabLoadRobot.Controls.Add(this.cmdChkInput);
            this.tabLoadRobot.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabLoadRobot.Location = new System.Drawing.Point(4, 27);
            this.tabLoadRobot.Name = "tabLoadRobot";
            this.tabLoadRobot.Padding = new System.Windows.Forms.Padding(3);
            this.tabLoadRobot.Size = new System.Drawing.Size(748, 597);
            this.tabLoadRobot.TabIndex = 1;
            this.tabLoadRobot.Text = "Load Robot";
            this.tabLoadRobot.UseVisualStyleBackColor = true;
            // 
            // cmdReadCalibPoints
            // 
            this.cmdReadCalibPoints.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdReadCalibPoints.Location = new System.Drawing.Point(410, 299);
            this.cmdReadCalibPoints.Name = "cmdReadCalibPoints";
            this.cmdReadCalibPoints.Size = new System.Drawing.Size(205, 34);
            this.cmdReadCalibPoints.TabIndex = 17;
            this.cmdReadCalibPoints.Text = "Retrieve Calib points";
            this.cmdReadCalibPoints.UseVisualStyleBackColor = true;
            this.cmdReadCalibPoints.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmdStop);
            this.groupBox2.Controls.Add(this.cmdReadCarrierFile);
            this.groupBox2.Controls.Add(this.cmdTestAutoCycle);
            this.groupBox2.Controls.Add(this.cmdSingleCyc);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox2.Location = new System.Drawing.Point(10, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 100);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Develop Tools (Loading-Robot) :";
            this.groupBox2.Visible = false;
            // 
            // cmdStop
            // 
            this.cmdStop.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdStop.Location = new System.Drawing.Point(7, 69);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(142, 23);
            this.cmdStop.TabIndex = 7;
            this.cmdStop.Text = "Stop";
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdReadCarrierFile
            // 
            this.cmdReadCarrierFile.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdReadCarrierFile.Location = new System.Drawing.Point(155, 21);
            this.cmdReadCarrierFile.Name = "cmdReadCarrierFile";
            this.cmdReadCarrierFile.Size = new System.Drawing.Size(107, 64);
            this.cmdReadCarrierFile.TabIndex = 6;
            this.cmdReadCarrierFile.Text = "Read Carrier File";
            this.cmdReadCarrierFile.UseVisualStyleBackColor = true;
            this.cmdReadCarrierFile.Click += new System.EventHandler(this.cmdReadCarrierFile_Click);
            // 
            // cmdTestAutoCycle
            // 
            this.cmdTestAutoCycle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdTestAutoCycle.Location = new System.Drawing.Point(7, 46);
            this.cmdTestAutoCycle.Name = "cmdTestAutoCycle";
            this.cmdTestAutoCycle.Size = new System.Drawing.Size(142, 21);
            this.cmdTestAutoCycle.TabIndex = 5;
            this.cmdTestAutoCycle.Text = "Test Auto Cycle";
            this.cmdTestAutoCycle.UseVisualStyleBackColor = true;
            this.cmdTestAutoCycle.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdSingleCyc
            // 
            this.cmdSingleCyc.Enabled = false;
            this.cmdSingleCyc.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdSingleCyc.Location = new System.Drawing.Point(7, 21);
            this.cmdSingleCyc.Name = "cmdSingleCyc";
            this.cmdSingleCyc.Size = new System.Drawing.Size(142, 23);
            this.cmdSingleCyc.TabIndex = 4;
            this.cmdSingleCyc.Text = "Single cycle by vision";
            this.cmdSingleCyc.UseVisualStyleBackColor = true;
            this.cmdSingleCyc.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RobotCommunication);
            this.panel1.Location = new System.Drawing.Point(1, 480);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 111);
            this.panel1.TabIndex = 15;
            // 
            // RobotCommunication
            // 
            this.RobotCommunication.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.RobotCommunication.Controls.Add(this.lstGetMsg);
            this.RobotCommunication.Controls.Add(this.lstSendMsg);
            this.RobotCommunication.Controls.Add(this.lblWait);
            this.RobotCommunication.Controls.Add(this.txtGetMsg);
            this.RobotCommunication.Controls.Add(this.txtSendMsg);
            this.RobotCommunication.Controls.Add(this.label4);
            this.RobotCommunication.Controls.Add(this.label3);
            this.RobotCommunication.Controls.Add(this.cmdSendComm);
            this.RobotCommunication.Controls.Add(this.cmdStopWaiting);
            this.RobotCommunication.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.RobotCommunication.Location = new System.Drawing.Point(5, 7);
            this.RobotCommunication.Name = "RobotCommunication";
            this.RobotCommunication.Size = new System.Drawing.Size(731, 102);
            this.RobotCommunication.TabIndex = 16;
            this.RobotCommunication.TabStop = false;
            this.RobotCommunication.Text = "Load Robot Communication";
            // 
            // lstGetMsg
            // 
            this.lstGetMsg.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstGetMsg.FormattingEnabled = true;
            this.lstGetMsg.ItemHeight = 14;
            this.lstGetMsg.Location = new System.Drawing.Point(424, 64);
            this.lstGetMsg.Name = "lstGetMsg";
            this.lstGetMsg.Size = new System.Drawing.Size(297, 32);
            this.lstGetMsg.TabIndex = 10;
            // 
            // lstSendMsg
            // 
            this.lstSendMsg.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstSendMsg.FormattingEnabled = true;
            this.lstSendMsg.ItemHeight = 14;
            this.lstSendMsg.Location = new System.Drawing.Point(424, 22);
            this.lstSendMsg.Name = "lstSendMsg";
            this.lstSendMsg.Size = new System.Drawing.Size(297, 32);
            this.lstSendMsg.TabIndex = 9;
            // 
            // lblWait
            // 
            this.lblWait.BackColor = System.Drawing.Color.LimeGreen;
            this.lblWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblWait.Font = new System.Drawing.Font("Arial", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblWait.Location = new System.Drawing.Point(382, 68);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(36, 24);
            this.lblWait.TabIndex = 8;
            // 
            // txtGetMsg
            // 
            this.txtGetMsg.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtGetMsg.Location = new System.Drawing.Point(65, 70);
            this.txtGetMsg.Name = "txtGetMsg";
            this.txtGetMsg.Size = new System.Drawing.Size(217, 22);
            this.txtGetMsg.TabIndex = 7;
            // 
            // txtSendMsg
            // 
            this.txtSendMsg.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtSendMsg.Location = new System.Drawing.Point(65, 27);
            this.txtSendMsg.Name = "txtSendMsg";
            this.txtSendMsg.Size = new System.Drawing.Size(279, 22);
            this.txtSendMsg.TabIndex = 6;
            this.txtSendMsg.Text = "cmd10,1,2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(7, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Get-->";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Send-->";
            // 
            // cmdSendComm
            // 
            this.cmdSendComm.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdSendComm.Location = new System.Drawing.Point(357, 25);
            this.cmdSendComm.Name = "cmdSendComm";
            this.cmdSendComm.Size = new System.Drawing.Size(61, 24);
            this.cmdSendComm.TabIndex = 3;
            this.cmdSendComm.Text = "Send";
            this.cmdSendComm.UseVisualStyleBackColor = true;
            this.cmdSendComm.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdStopWaiting
            // 
            this.cmdStopWaiting.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdStopWaiting.Location = new System.Drawing.Point(288, 68);
            this.cmdStopWaiting.Name = "cmdStopWaiting";
            this.cmdStopWaiting.Size = new System.Drawing.Size(88, 24);
            this.cmdStopWaiting.TabIndex = 2;
            this.cmdStopWaiting.Text = "Stop waiting";
            this.cmdStopWaiting.UseVisualStyleBackColor = true;
            this.cmdStopWaiting.Click += new System.EventHandler(this.cmdStopWaiting_Click);
            // 
            // grbHeightStation
            // 
            this.grbHeightStation.Controls.Add(this.label2);
            this.grbHeightStation.Controls.Add(this.label1);
            this.grbHeightStation.Controls.Add(this.txtCurrentHeight);
            this.grbHeightStation.Controls.Add(this.txtCorrectVal);
            this.grbHeightStation.Controls.Add(this.OptTrayHeight);
            this.grbHeightStation.Controls.Add(this.OptIndexTable);
            this.grbHeightStation.Controls.Add(this.cmdChangeHeight);
            this.grbHeightStation.Controls.Add(this.cmdReadHeight);
            this.grbHeightStation.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.grbHeightStation.Location = new System.Drawing.Point(8, 386);
            this.grbHeightStation.Name = "grbHeightStation";
            this.grbHeightStation.Size = new System.Drawing.Size(731, 83);
            this.grbHeightStation.TabIndex = 14;
            this.grbHeightStation.TabStop = false;
            this.grbHeightStation.Text = "Height Robot stations:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(116, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "CorrectionValue";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "current Height";
            // 
            // txtCurrentHeight
            // 
            this.txtCurrentHeight.Enabled = false;
            this.txtCurrentHeight.Location = new System.Drawing.Point(6, 46);
            this.txtCurrentHeight.Name = "txtCurrentHeight";
            this.txtCurrentHeight.Size = new System.Drawing.Size(104, 26);
            this.txtCurrentHeight.TabIndex = 5;
            this.txtCurrentHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCorrectVal
            // 
            this.txtCorrectVal.Location = new System.Drawing.Point(118, 46);
            this.txtCorrectVal.Name = "txtCorrectVal";
            this.txtCorrectVal.Size = new System.Drawing.Size(104, 26);
            this.txtCorrectVal.TabIndex = 4;
            this.txtCorrectVal.Text = "0.1";
            this.txtCorrectVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OptTrayHeight
            // 
            this.OptTrayHeight.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OptTrayHeight.Location = new System.Drawing.Point(313, 47);
            this.OptTrayHeight.Name = "OptTrayHeight";
            this.OptTrayHeight.Size = new System.Drawing.Size(132, 22);
            this.OptTrayHeight.TabIndex = 3;
            this.OptTrayHeight.Text = " Tray Height";
            this.OptTrayHeight.UseVisualStyleBackColor = true;
            this.OptTrayHeight.CheckedChanged += new System.EventHandler(this.OptTrayHeight_CheckedChanged);
            // 
            // OptIndexTable
            // 
            this.OptIndexTable.Checked = true;
            this.OptIndexTable.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.OptIndexTable.Location = new System.Drawing.Point(313, 21);
            this.OptIndexTable.Name = "OptIndexTable";
            this.OptIndexTable.Size = new System.Drawing.Size(148, 22);
            this.OptIndexTable.TabIndex = 2;
            this.OptIndexTable.TabStop = true;
            this.OptIndexTable.Text = "Carrier Height";
            this.OptIndexTable.UseVisualStyleBackColor = true;
            this.OptIndexTable.CheckedChanged += new System.EventHandler(this.OptIndexTable_CheckedChanged);
            // 
            // cmdChangeHeight
            // 
            this.cmdChangeHeight.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdChangeHeight.Location = new System.Drawing.Point(476, 21);
            this.cmdChangeHeight.Name = "cmdChangeHeight";
            this.cmdChangeHeight.Size = new System.Drawing.Size(78, 47);
            this.cmdChangeHeight.TabIndex = 1;
            this.cmdChangeHeight.Text = "Change Value";
            this.cmdChangeHeight.UseVisualStyleBackColor = true;
            this.cmdChangeHeight.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdReadHeight
            // 
            this.cmdReadHeight.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdReadHeight.Location = new System.Drawing.Point(560, 21);
            this.cmdReadHeight.Name = "cmdReadHeight";
            this.cmdReadHeight.Size = new System.Drawing.Size(78, 47);
            this.cmdReadHeight.TabIndex = 0;
            this.cmdReadHeight.Text = "Read Value";
            this.cmdReadHeight.UseVisualStyleBackColor = true;
            this.cmdReadHeight.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdChangeGripper
            // 
            this.cmdChangeGripper.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdChangeGripper.Location = new System.Drawing.Point(410, 259);
            this.cmdChangeGripper.Name = "cmdChangeGripper";
            this.cmdChangeGripper.Size = new System.Drawing.Size(205, 34);
            this.cmdChangeGripper.TabIndex = 13;
            this.cmdChangeGripper.Text = "Change Gripper";
            this.cmdChangeGripper.UseVisualStyleBackColor = true;
            this.cmdChangeGripper.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdReadRobotParam
            // 
            this.cmdReadRobotParam.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdReadRobotParam.Location = new System.Drawing.Point(469, 356);
            this.cmdReadRobotParam.Name = "cmdReadRobotParam";
            this.cmdReadRobotParam.Size = new System.Drawing.Size(205, 34);
            this.cmdReadRobotParam.TabIndex = 12;
            this.cmdReadRobotParam.Text = "Read Parameters";
            this.cmdReadRobotParam.UseVisualStyleBackColor = true;
            this.cmdReadRobotParam.Visible = false;
            this.cmdReadRobotParam.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // lstInputs
            // 
            this.lstInputs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstInputs.FormattingEnabled = true;
            this.lstInputs.ItemHeight = 18;
            this.lstInputs.Items.AddRange(new object[] {
            "Inputs Descriptions",
            "griper Vucc",
            "Insert sensor"});
            this.lstInputs.Location = new System.Drawing.Point(410, 229);
            this.lstInputs.Name = "lstInputs";
            this.lstInputs.ScrollAlwaysVisible = true;
            this.lstInputs.Size = new System.Drawing.Size(205, 22);
            this.lstInputs.TabIndex = 11;
            // 
            // cmdReleaseInsert
            // 
            this.cmdReleaseInsert.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdReleaseInsert.Location = new System.Drawing.Point(410, 149);
            this.cmdReleaseInsert.Name = "cmdReleaseInsert";
            this.cmdReleaseInsert.Size = new System.Drawing.Size(205, 34);
            this.cmdReleaseInsert.TabIndex = 9;
            this.cmdReleaseInsert.Text = "Release Insert";
            this.cmdReleaseInsert.UseVisualStyleBackColor = true;
            this.cmdReleaseInsert.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdHoldInsert
            // 
            this.cmdHoldInsert.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdHoldInsert.Location = new System.Drawing.Point(410, 109);
            this.cmdHoldInsert.Name = "cmdHoldInsert";
            this.cmdHoldInsert.Size = new System.Drawing.Size(205, 34);
            this.cmdHoldInsert.TabIndex = 8;
            this.cmdHoldInsert.Text = "Hold Insert";
            this.cmdHoldInsert.UseVisualStyleBackColor = true;
            this.cmdHoldInsert.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdSaveRobotParam
            // 
            this.cmdSaveRobotParam.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdSaveRobotParam.Location = new System.Drawing.Point(130, 299);
            this.cmdSaveRobotParam.Name = "cmdSaveRobotParam";
            this.cmdSaveRobotParam.Size = new System.Drawing.Size(205, 34);
            this.cmdSaveRobotParam.TabIndex = 7;
            this.cmdSaveRobotParam.Text = "Save Parameters";
            this.cmdSaveRobotParam.UseVisualStyleBackColor = true;
            this.cmdSaveRobotParam.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdRobotReset
            // 
            this.cmdRobotReset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdRobotReset.ForeColor = System.Drawing.Color.Red;
            this.cmdRobotReset.Location = new System.Drawing.Point(130, 259);
            this.cmdRobotReset.Name = "cmdRobotReset";
            this.cmdRobotReset.Size = new System.Drawing.Size(205, 34);
            this.cmdRobotReset.TabIndex = 6;
            this.cmdRobotReset.Text = "Robot Reset";
            this.cmdRobotReset.UseVisualStyleBackColor = true;
            this.cmdRobotReset.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // lstOutputs
            // 
            this.lstOutputs.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lstOutputs.FormattingEnabled = true;
            this.lstOutputs.HorizontalScrollbar = true;
            this.lstOutputs.ItemHeight = 18;
            this.lstOutputs.Items.AddRange(new object[] {
            "Outputs Descriptions",
            "griper Vucc",
            "Insert sensor"});
            this.lstOutputs.Location = new System.Drawing.Point(130, 229);
            this.lstOutputs.Name = "lstOutputs";
            this.lstOutputs.Size = new System.Drawing.Size(205, 22);
            this.lstOutputs.TabIndex = 5;
            // 
            // cmdDoutOFF
            // 
            this.cmdDoutOFF.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdDoutOFF.Location = new System.Drawing.Point(130, 189);
            this.cmdDoutOFF.Name = "cmdDoutOFF";
            this.cmdDoutOFF.Size = new System.Drawing.Size(205, 34);
            this.cmdDoutOFF.TabIndex = 4;
            this.cmdDoutOFF.Text = "Output OFF";
            this.cmdDoutOFF.UseVisualStyleBackColor = true;
            this.cmdDoutOFF.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdDoutOn
            // 
            this.cmdDoutOn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdDoutOn.Location = new System.Drawing.Point(130, 149);
            this.cmdDoutOn.Name = "cmdDoutOn";
            this.cmdDoutOn.Size = new System.Drawing.Size(205, 34);
            this.cmdDoutOn.TabIndex = 3;
            this.cmdDoutOn.Text = "Output ON";
            this.cmdDoutOn.UseVisualStyleBackColor = true;
            this.cmdDoutOn.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // txtRobotSpeed
            // 
            this.txtRobotSpeed.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txtRobotSpeed.Location = new System.Drawing.Point(278, 113);
            this.txtRobotSpeed.Name = "txtRobotSpeed";
            this.txtRobotSpeed.Size = new System.Drawing.Size(48, 26);
            this.txtRobotSpeed.TabIndex = 2;
            this.txtRobotSpeed.Text = "25";
            this.txtRobotSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmdRobotSpeed
            // 
            this.cmdRobotSpeed.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdRobotSpeed.Location = new System.Drawing.Point(130, 109);
            this.cmdRobotSpeed.Name = "cmdRobotSpeed";
            this.cmdRobotSpeed.Size = new System.Drawing.Size(205, 34);
            this.cmdRobotSpeed.TabIndex = 1;
            this.cmdRobotSpeed.Text = "Robot Speed";
            this.cmdRobotSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdRobotSpeed.UseVisualStyleBackColor = true;
            this.cmdRobotSpeed.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // cmdRobotManu
            // 
            this.cmdRobotManu.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdRobotManu.ForeColor = System.Drawing.Color.Navy;
            this.cmdRobotManu.Image = ((System.Drawing.Image)(resources.GetObject("cmdRobotManu.Image")));
            this.cmdRobotManu.Location = new System.Drawing.Point(292, 6);
            this.cmdRobotManu.Name = "cmdRobotManu";
            this.cmdRobotManu.Size = new System.Drawing.Size(161, 97);
            this.cmdRobotManu.TabIndex = 0;
            this.cmdRobotManu.Text = "Robot Menu";
            this.cmdRobotManu.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdRobotManu.UseVisualStyleBackColor = true;
            this.cmdRobotManu.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(3, 3);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.SapeRobotInputState});
            this.shapeContainer1.Size = new System.Drawing.Size(742, 591);
            this.shapeContainer1.TabIndex = 18;
            this.shapeContainer1.TabStop = false;
            // 
            // SapeRobotInputState
            // 
            this.SapeRobotInputState.BackgroundImage = global::Stahli2Robots.Properties.Resources.circle_grey01;
            this.SapeRobotInputState.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SapeRobotInputState.Location = new System.Drawing.Point(580, 190);
            this.SapeRobotInputState.Name = "SapeRobotInputState";
            this.SapeRobotInputState.Size = new System.Drawing.Size(25, 25);
            // 
            // cmdChkInput
            // 
            this.cmdChkInput.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmdChkInput.Location = new System.Drawing.Point(410, 189);
            this.cmdChkInput.Name = "cmdChkInput";
            this.cmdChkInput.Size = new System.Drawing.Size(160, 34);
            this.cmdChkInput.TabIndex = 10;
            this.cmdChkInput.Text = "Check Input Status";
            this.cmdChkInput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdChkInput.UseVisualStyleBackColor = true;
            this.cmdChkInput.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // btnTeachTableHeight2
            // 
            this.btnTeachTableHeight2.Font = new System.Drawing.Font("Arial", 12F);
            this.btnTeachTableHeight2.Location = new System.Drawing.Point(130, 339);
            this.btnTeachTableHeight2.Name = "btnTeachTableHeight2";
            this.btnTeachTableHeight2.Size = new System.Drawing.Size(205, 34);
            this.btnTeachTableHeight2.TabIndex = 43;
            this.btnTeachTableHeight2.Text = "Teach table height";
            this.btnTeachTableHeight2.UseVisualStyleBackColor = true;
            this.btnTeachTableHeight2.Click += new System.EventHandler(this.cmdExecBtn2_Click);
            // 
            // btnTeachTableHeight
            // 
            this.btnTeachTableHeight.Font = new System.Drawing.Font("Arial", 12F);
            this.btnTeachTableHeight.Location = new System.Drawing.Point(130, 339);
            this.btnTeachTableHeight.Name = "btnTeachTableHeight";
            this.btnTeachTableHeight.Size = new System.Drawing.Size(205, 34);
            this.btnTeachTableHeight.TabIndex = 20;
            this.btnTeachTableHeight.Text = "Teach table height";
            this.btnTeachTableHeight.UseVisualStyleBackColor = true;
            this.btnTeachTableHeight.Click += new System.EventHandler(this.cmdExecBtn1_Click);
            // 
            // FrmRobots
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(769, 638);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmRobots";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robots";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserCloseForm);
            this.tabControl1.ResumeLayout(false);
            this.tabUnloadRobot.ResumeLayout(false);
            this.tabUnloadRobot.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.RobotCommunication2.ResumeLayout(false);
            this.RobotCommunication2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabLoadRobot.ResumeLayout(false);
            this.tabLoadRobot.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.RobotCommunication.ResumeLayout(false);
            this.RobotCommunication.PerformLayout();
            this.grbHeightStation.ResumeLayout(false);
            this.grbHeightStation.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
	   
//    If stLoadRobotControl1.bDoorOpened Then
//        Call ErrDescrip(410, frmTitle.lblRobotsMsg): Exit Function
//    End If

//    If UCase(Left(LocMsg, 3)) <> "CMD" Then LocMsg = "cmd" & LocMsg

//    If RL1.ConnBusy Then
//        StartTime = timeGetTime() 'RL.TimeOutCounter = 3000
//        Do
//            DoEvents
//            If Not RL1.ConnBusy Then
//                GlobalErr "Robot Sending Suspended for : " & (timeGetTime() - StartTime) & " ms"
//                Exit Do
//            End If
//            If (timeGetTime() - StartTime) > RL1.TimeOutCounter Then
//                If RL1.sLastSentCmd = "cmd15" And Left(LocMsg, 5) = "cmd15" Then GlobalErr "No response to previous cmd15 comand. Send command wasn't executed.":  Exit Function
//                GlobalErr "Load Robot connection busy, send command was not executed"
//                Exit Function
//            End If
//        Loop Until False
//    End If

//    If fMainForm.CommLoadRobot1.OutBufferCount > 0 Then fMainForm.CommLoadRobot1.OutBufferCount = 0
//    fMainForm.CommLoadRobot1.Output = LocMsg & vbCrLf
//    RL1.ConnBusy = True
//    If Left(LocMsg, 5) = "cmd70" Then
//        RL1.ConnBusy = False
//    End If
//    RL1.sLastSentCmd = Left$(LocMsg, 5)
//    lblWait(0).BackColor = vbRed: lblWait(0).Caption = "Waiting.."
//    txtSendMsg(0) = LocMsg
//    lstSendMsg(0).AddItem LocMsg: lstSendMsg(0).TopIndex = lstSendMsg(0).ListCount - 1
//    Send_LoadCmd1 = True
//  Exit Function
//CommErr:
//    GlobalErr "Robot - Load 1" & Err.Description
//End Function       


        //Delegates:
        private volatile bool stopCycleTest;
        
        private delegate void SetBackColorCommDelegate(ROBOT_INDEXES robot, Color color, string caption);
        private SetBackColorCommDelegate setBackColorCommDelegate;
        private void SetBackColorCommDelegateFunc(ROBOT_INDEXES robot,Color color, string caption)
        {
            try
            {
                switch (robot)
                {
                    case ROBOT_INDEXES.ENUM_LOAD_ROBOT:
                        lblWait.BackColor = color;
                        lblWait.Text = caption;
                        break;
                    case ROBOT_INDEXES.ENUM_UNLOAD_ROBOT:
                        lblWait2.BackColor = color;
                        lblWait2.Text = caption;
                        break;
                }
            }
            catch { }
        }
        public void SetBackColorComm(ROBOT_INDEXES robot, Color color, string caption)
        {
            try
            {
                this.BeginInvoke(setBackColorCommDelegate, robot, color, caption);
            }
            catch { }
        }

        private delegate void ShowCommStringDelegate(ROBOT_INDEXES robot, GetSend_str SendGet, string str);
        private ShowCommStringDelegate showCommStringDelegate;
        private void ShowCommStringDelegateFunc(ROBOT_INDEXES robot, GetSend_str GetOrSend, string str)
        {
            try
            {
                switch (robot)
                {
                    case ROBOT_INDEXES.ENUM_LOAD_ROBOT:
                        switch (GetOrSend)
                        {
                            case GetSend_str.ENUM_SEND:  //send
                                txtSendMsg.Text = str;
                                //lstSendMsg.Items.Add(str);
                                lstSendMsg.Items.Insert(0, str);
                                break;
                            case GetSend_str.ENUM_GET:  //get
                                txtGetMsg.Text = str;   
                                //lstGetMsg.Items.Add(str);
                                lstGetMsg.Items.Insert(0, str);
                                break;
                            case GetSend_str.ENUM_CLEAR:  //clear
                                txtSendMsg.Text = "";
                                txtGetMsg.Text = "";
                                break;
                        }
                        break;
                    case ROBOT_INDEXES.ENUM_UNLOAD_ROBOT:
                        switch (GetOrSend)
                        {
                            case GetSend_str.ENUM_SEND:  //send
                                txtSendMsg2.Text = str;
                                //lstSendMsg2.Items.Add(str);
                                lstSendMsg2.Items.Insert(0, str);
                                break;
                            case GetSend_str.ENUM_GET:  //get
                                txtGetMsg2.Text = str;
                                //lstGetMsg2.Items.Add(str);
                                lstGetMsg2.Items.Insert(0, str);
                                break;
                            case GetSend_str.ENUM_CLEAR:  //clear
                                txtSendMsg2.Text = "";
                                txtGetMsg2.Text = "";
                                break;
                        }
                        break;
                }
            }
            catch { }
        }
        public void ShowCommString(ROBOT_INDEXES robot, GetSend_str GetOrSend, string str)
        {
            try
            {
                this.Invoke(showCommStringDelegate, robot, GetOrSend, str);
            }
            catch { }
        }

        private void cmdExecBtn1_Click(object sender, EventArgs e)
        {      
            Button btn = sender as Button;
            if (btn == null) return;
            switch (btn.Name)
            {
                case "cmdRobotManu":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "12,0";
                    break;
                case "cmdRobotSpeed":
                    if (Convert.ToInt16(txtRobotSpeed.Text) > 100)
                    {
                        txtRobotSpeed.Text = "100";
                    } 
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "13," + txtRobotSpeed.Text;    
                    break;
                case "cmdDoutOn":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "27," + lstOutputs.SelectedIndex.ToString() + ",1";
                    break;
                case "cmdDoutOFF":
                    AppGen.Inst.RobotConnection.RL.CmdMsg =  "27," + lstOutputs.SelectedIndex.ToString() + ",0";
                    break;
                case "cmdRobotReset":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "99,0";
                    break;
                case "cmdSaveRobotParam":   //TODO:  add popup are you sure you want to save..
                    AppGen.Inst.RobotConnection.RL.CmdMsg= "38,0";
                    break;
                case "cmdReadCalibPoints":   //all calib poins(load and unload robots)
                    //AppGen.Inst.RobotConnection.RL.CmdMsg= "32,0"; old cmd32 and cmd40
                    RetrieveRobotPoints();
                    // here we return becouse string send by the function "RetrieveRobotPoints" and not like other buttons
                    return;
                case "cmdHoldInsert":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "29,1";
                    break;
                case "cmdReleaseInsert":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "29,0";
                    break;
                case "cmdChkInput":
                    AppGen.Inst.RobotConnection.RL.CmdMsg= "23," + lstInputs.SelectedIndex.ToString();
                    break;
                case "cmdChangeGripper":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "33,0";
                    break;
                case "cmdReadRobotParam":   //not implement on this project
                    //AppGen.Inst.RobotConnection.RL.CmdMsg=
                    break;
                case "cmdReadHeight":
                    if (OptTrayHeight.Checked)
                        AppGen.Inst.RobotConnection.RL.CmdMsg = "51,1,";
                    else
                        AppGen.Inst.RobotConnection.RL.CmdMsg = "51,2,";
                    break;
                case "cmdChangeHeight":
                    if (OptTrayHeight.Checked)
                        AppGen.Inst.RobotConnection.RL.CmdMsg = "50,1," + txtCorrectVal.Text;
                    else
                        AppGen.Inst.RobotConnection.RL.CmdMsg = "50,2," + txtCorrectVal.Text;
                    break;
                    //System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    //sw.Start();  // Start The StopWatch ...From 000
                    //while ( AppGen.Inst.RobotConnection.sngCurrentHeight == 0)
                    //{
                    //    Thread.Sleep(100);
                    //    if (sw.ElapsedMilliseconds > 3000)
                    //    {
                    //        sw.Stop(); 
                    //        return;  //Timeout
                    //    }
                    //}
                    //sw.Stop();       //Stop the Timer
                    //return
                case "cmdSendComm":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = txtSendMsg.Text;               
                    break;
                case "cmdSingleCyc":       //developing tool - single cycle by vision coord
               //     string InsertLoc = "10,";
                    //AppGen.Inst.RobotConnection.RL.CmdMsg = InsertLoc = "10," + round(vpInsertLoc(LoadTrayCam).X , 2) + y + z ...     
                    
                    //temp for testing witout vision:
               //     InsertLoc = InsertLoc + " 55,-400,0";
               //     AppGen.Inst.RobotConnection.RL.CmdMsg = InsertLoc; 
                    break;                
                case "cmdTestAutoCycle":
                    //stopCycleTest = false;
                    //CycleTest();
                    //return;                  
                    //InsertLoc = "15,440,-200,0,440,400,0,1";
                    //AppGen.Inst.RobotConnection.RL.CmdMsg = InsertLoc; 
                    break;
                case "cmdStop":
                    //stopCycleTest = true;
                    //return;
                    break;
                case "btnTeachTableHeight":
                    AppGen.Inst.RobotConnection.RL.CmdMsg = "22,0";
                    break;
            }           
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.RL.CmdMsg);
        }
        private void cmdExecBtn2_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            switch (btn.Name)
            {
                case "cmdRobotManu2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "12,0";
                    break;
                case "cmdRobotSpeed2":
                    if (Convert.ToInt16(txtRobotSpeed2.Text) >100)
                    {
                        txtRobotSpeed2.Text = "100";
                    }
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "13," + txtRobotSpeed2.Text;            
                    break;
                case "cmdDoutOn2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "27," + lstOutputs2.SelectedIndex.ToString() + ",1";
                    break;
                case "cmdDoutOFF2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "27," + lstOutputs2.SelectedIndex.ToString() + ",0";
                    break;  //lstOutputs.SelectedIndex.ToString()
                case "cmdRobotReset2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "99,0";
                    break;
                case "cmdSaveRobotParam2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg= "38,0";
                    break;               
                case "cmdHoldInsert2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "29,1";
                    break;
                case "cmdReleaseInsert2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "29,0";
                    break;
                case "cmdChkInput2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg= "23," + lstInputs2.SelectedIndex.ToString();
                    break;
                case "cmdChangeGripper2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "33,0";
                    break;
                case "cmdReadHeight2":
                    if (OptTrayHeight2.Checked)
                        AppGen.Inst.RobotConnection.RU.CmdMsg = "51,4," ;
                    else
                        AppGen.Inst.RobotConnection.RU.CmdMsg = "51,3," ;
                    break;
                case "cmdChangeHeight2":
                     if (OptTrayHeight2.Checked)
                        AppGen.Inst.RobotConnection.RU.CmdMsg = "50,4," + txtCorrectVal2.Text;
                    else
                        AppGen.Inst.RobotConnection.RU.CmdMsg = "50,3," + txtCorrectVal2.Text;
                    break;
                case "cmdMasureOffset":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "62," + txtPlaceMaesureOffsetX.Text + "," + txtPlaceMaesureOffsetY.Text + "," + txtPlaceMaesureOffsetZ.Text + "," + txtPlaceMaesureOffsetA.Text;
                    break;
                case "cmdSendComm2":
                     AppGen.Inst.RobotConnection.RU.CmdMsg = txtSendMsg2.Text;               
                    break;
                case "cmdSingleCyc2":   //developing tool - single cycle by vision coord
                    break;
                case "btnTeachTableHeight2":
                    AppGen.Inst.RobotConnection.RU.CmdMsg = "22,0";
                    break;
            }
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, AppGen.Inst.RobotConnection.RU.CmdMsg);
        }


        private void RetrieveRobotPoints()
        {
            List<string> codes = new List<string>();
            for (int ii = 0; ii < 18; ii++)
            {
                codes.Add("41," + ii.ToString());
            }

            foreach(string code in codes)
            {
                if (!ConnBusyTimeOut()) break;
                AppGen.Inst.RobotConnection.RL.CmdMsg = code;
                AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.RL.CmdMsg);
            }
        }
        
        private void cmdStopWaiting_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;
            switch (btn.Name)
            {
                case "cmdStopWaiting":
                    AppGen.Inst.RobotConnection.ResetRobotComm(ROBOT_INDEXES.ENUM_LOAD_ROBOT);

                    break;
                case "cmdStopWaiting2":
                    AppGen.Inst.RobotConnection.ResetRobotComm(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT);
                    break;
            }
        }
        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void cmdReadCarrierFile_Click(object sender, EventArgs e) //Read(to Master) + Rotate + Offset
        {
            AppGen.Inst.LoadCarrier.SetRotate(AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.rotation);          //result of vision rotation, assing to Loadcarrier
            AppGen.Inst.LoadCarrier.SetOffset(AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointX, AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointY);

            //AppGen.Inst.LoadCarrier.ReadFromFile();
            //AppGen.Inst.LoadCarrier.SetRotate(120.1);
            //AppGen.Inst.LoadCarrier.SetOffset(466.096, 448.408);

            //AppGen.Inst.LoadCarrier2.ReadFromFile();
            //AppGen.Inst.LoadCarrier2.SetRotate(90);   
            //AppGen.Inst.LoadCarrier2.SetOffset(461.284, -117.810);      
        }

        private void OptTrayHeight_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTrayHeight.Checked == false) return;
            AppGen.Inst.RobotConnection.RL.CmdMsg = "51,1,";
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.RL.CmdMsg);
        }
        private void OptIndexTable_CheckedChanged(object sender, EventArgs e)
        {
            if (OptIndexTable.Checked == false) return;
            AppGen.Inst.RobotConnection.RL.CmdMsg = "51,2,";
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.RL.CmdMsg);
        }
        private void OptTrayHeight2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptTrayHeight2.Checked == false) return;
            AppGen.Inst.RobotConnection.RU.CmdMsg = "51,4,";
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, AppGen.Inst.RobotConnection.RU.CmdMsg);
        }
        private void OptIndexTable2_CheckedChanged(object sender, EventArgs e)
        {
            if (OptIndexTable2.Checked == false) return;
            AppGen.Inst.RobotConnection.RU.CmdMsg = "51,3,";
            AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_UNLOAD_ROBOT, AppGen.Inst.RobotConnection.RU.CmdMsg);
        }

        private void CycleTest()
        {
         //   AppGen.Inst.LoadCarrier.CurrIndex = 0;
         ////   AppGen.Inst.LoadCarrier2.CurrIndex = 0;
         //   string InsertLoc = "";
         //   AppGen.Inst.RobotConnection.StopCycleTest = true;


         //   //First Create the instance of Stopwatch Class
         //   System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
         //   sw.Start();  // Start The StopWatch ...From 000

         //   while ((AppGen.Inst.LoadCarrier.CurrIndex < AppGen.Inst.LoadCarrier.NoOfInsAtTray) && (!stopCycleTest))
         //   {
         //       if (AppGen.Inst.RobotConnection.RL.ConnBusy == false)
         //       {
         //           IndexData loadIndex = AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset;
         //           IndexData unLoadIndex = AppGen.Inst.LoadCarrier2.IndexList[AppGen.Inst.LoadCarrier2.CurrIndex].Offset;

         //           InsertLoc = "15," + loadIndex.X.ToString() + "," + loadIndex.Y.ToString() + "," +
         //                               loadIndex.Angle.ToString() + "," + unLoadIndex.X.ToString() +
         //                               "," + unLoadIndex.Y.ToString() + "," + unLoadIndex.Angle.ToString();

         //           AppGen.Inst.RobotConnection.RL.CmdMsg = InsertLoc;
         //           AppGen.Inst.RobotConnection.SendData(ROBOT_INDEXES.ENUM_LOAD_ROBOT, AppGen.Inst.RobotConnection.RL.CmdMsg);

         //           AppGen.Inst.LoadCarrier.CurrIndex++;
         //           AppGen.Inst.LoadCarrier2.CurrIndex++;
         //           InsertLoc = "";
         //           Thread.Sleep(100);
         //       }
         //   }
         //   sw.Stop();       //Stop the Timer
        }

        private delegate void UpdateFrmRobotDelegate(FrmRobotData BoxType, string UpdatedValue);
        private UpdateFrmRobotDelegate updateFrmRobotDelegate;
        private void UpdateFrmRobotDelegateFunc(FrmRobotData BoxType, string UpdatedValue)
        {
            try
            {
                switch (BoxType)
                {
                    case FrmRobotData.CurrHight1:
                        txtCurrentHeight.Text = UpdatedValue;
                        break;
                    case FrmRobotData.CurrHight2:
                        txtCurrentHeight2.Text = UpdatedValue;
                        break;
                    case FrmRobotData.InputState1:                        
                        if (UpdatedValue == "-1")
                        {
                            SapeRobotInputState.BackgroundImage = Properties.Resources.circle_red5;   
                        }
                        else
                        {
                            SapeRobotInputState.BackgroundImage = Properties.Resources.circle_green4;
                        }
                        break;
                    case FrmRobotData.InputState2:
                        if (UpdatedValue == "-1")
                        {
                            //SapeRobotInputState2.BackgroundImage = Properties.Resources.circle_red5;
                        }
                        else
                        {
                            //SapeRobotInputState2.BackgroundImage = Properties.Resources.circle_green4;
                        }
                        break;   
                }
            }
            catch { }
        }
        public void UpdateFrmRobot(FrmRobotData BoxType, string UpdatedValue)
        {
            try
            {
                this.Invoke(updateFrmRobotDelegate, BoxType, UpdatedValue);
            }
            catch { }
        }
        private bool ConnBusyTimeOut()
        {
            try
            {
                System.Diagnostics.Stopwatch sw4 = new System.Diagnostics.Stopwatch();
                sw4.Start();  // Start The StopWatch ...From 000

                while (AppGen.Inst.RobotConnection.RL.ConnBusy)
                {
                    Thread.Sleep(100);
                    if (sw4.ElapsedMilliseconds > 3000)
                    {
                        sw4.Stop(); 
                        return false;  //Timeout
                    }
                }
                sw4.Stop();       //Stop the Timer
                return true;
            }
            catch { }

            return false;    
        }
	}
}
