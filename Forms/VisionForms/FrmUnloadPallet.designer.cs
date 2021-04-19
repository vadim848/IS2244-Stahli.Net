namespace Stahli2Robots
{
    partial class FrmLoadTray
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLoadTray));
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.groupPassword = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdPassword = new System.Windows.Forms.Button();
            this.txtInputPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            this.cmdCloseCognexControl = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdSubmitCalibPoints = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdSaveParamToOrder = new System.Windows.Forms.Button();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contrastUpDown = new System.Windows.Forms.NumericUpDown();
            this.cmdCognexControl = new System.Windows.Forms.Button();
            this.brightnessUpDown = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.ovalShape2 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.ovalShape1 = new Microsoft.VisualBasic.PowerPacks.OvalShape();
            this.frmPatMax = new System.Windows.Forms.GroupBox();
            this.txtPatMaxScoreValue = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rotation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frmImageAcquisitionFrame = new System.Windows.Forms.GroupBox();
            this.txtCurrentIndex = new System.Windows.Forms.TextBox();
            this.cmdTeach = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.optImageAcquisitionOptionImageFile = new System.Windows.Forms.RadioButton();
            this.optImageAcquisitionOptionFrameGrabber = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cmdPatMaxRunCommand = new System.Windows.Forms.Button();
            this.cmdImageAcquisitionNewImageCommand = new System.Windows.Forms.Button();
            this.cmdImageAcquisitionLiveOrOpenCommand = new System.Windows.Forms.Button();
            this.CogDisplay1 = new Cognex.VisionPro.Display.CogDisplay();
            this.VProAppTab = new System.Windows.Forms.TabControl();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.CogImageFileEdit1 = new Cognex.VisionPro.ImageFile.CogImageFileEditV2();
            this.FrameGrabber = new System.Windows.Forms.TabPage();
            this.CogAcqFifoEdit1 = new Cognex.VisionPro.CogAcqFifoEditV2();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.CogPMAlignEdit1 = new Cognex.VisionPro.PMAlign.CogPMAlignEditV2();
            this.ImageAcquisitionCommonDialog = new System.Windows.Forms.OpenFileDialog();
            this.chkDisplayResults = new System.Windows.Forms.CheckBox();
            this.TabPage1.SuspendLayout();
            this.groupPassword.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.frmPatMax.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.frmImageAcquisitionFrame.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CogDisplay1)).BeginInit();
            this.VProAppTab.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CogImageFileEdit1)).BeginInit();
            this.FrameGrabber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CogAcqFifoEdit1)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CogPMAlignEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // TabPage1
            // 
            this.TabPage1.Controls.Add(this.groupPassword);
            this.TabPage1.Controls.Add(this.groupBox2);
            this.TabPage1.Controls.Add(this.button1);
            this.TabPage1.Controls.Add(this.cogRecordDisplay1);
            this.TabPage1.Controls.Add(this.groupBox1);
            this.TabPage1.Controls.Add(this.frmPatMax);
            this.TabPage1.Controls.Add(this.frmImageAcquisitionFrame);
            this.TabPage1.Location = new System.Drawing.Point(4, 22);
            this.TabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Size = new System.Drawing.Size(1343, 784);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "VisionPro";
            // 
            // groupPassword
            // 
            this.groupPassword.Controls.Add(this.cmdCancel);
            this.groupPassword.Controls.Add(this.cmdPassword);
            this.groupPassword.Controls.Add(this.txtInputPassword);
            this.groupPassword.Controls.Add(this.label7);
            this.groupPassword.Location = new System.Drawing.Point(311, 250);
            this.groupPassword.Name = "groupPassword";
            this.groupPassword.Size = new System.Drawing.Size(148, 158);
            this.groupPassword.TabIndex = 42;
            this.groupPassword.TabStop = false;
            this.groupPassword.Text = "Password Alert : ";
            this.groupPassword.Visible = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(21, 127);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(112, 25);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdPassword
            // 
            this.cmdPassword.Location = new System.Drawing.Point(19, 88);
            this.cmdPassword.Name = "cmdPassword";
            this.cmdPassword.Size = new System.Drawing.Size(112, 25);
            this.cmdPassword.TabIndex = 2;
            this.cmdPassword.Text = "Submit";
            this.cmdPassword.UseVisualStyleBackColor = true;
            this.cmdPassword.Click += new System.EventHandler(this.cmdPassword_Click);
            // 
            // txtInputPassword
            // 
            this.txtInputPassword.Location = new System.Drawing.Point(19, 62);
            this.txtInputPassword.Name = "txtInputPassword";
            this.txtInputPassword.Size = new System.Drawing.Size(114, 20);
            this.txtInputPassword.TabIndex = 1;
            this.txtInputPassword.Text = "****";
            this.txtInputPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Please enter Password:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cogToolBlockEditV21);
            this.groupBox2.Controls.Add(this.cmdCloseCognexControl);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.cmdSubmitCalibPoints);
            this.groupBox2.Location = new System.Drawing.Point(758, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 711);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cognex Tools";
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(5, 21);
            this.cogToolBlockEditV21.Margin = new System.Windows.Forms.Padding(2);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(350, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(360, 641);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 41;
            // 
            // cmdCloseCognexControl
            // 
            this.cmdCloseCognexControl.Location = new System.Drawing.Point(6, 667);
            this.cmdCloseCognexControl.Name = "cmdCloseCognexControl";
            this.cmdCloseCognexControl.Size = new System.Drawing.Size(86, 38);
            this.cmdCloseCognexControl.TabIndex = 52;
            this.cmdCloseCognexControl.Text = "Close";
            this.cmdCloseCognexControl.UseVisualStyleBackColor = true;
            this.cmdCloseCognexControl.Click += new System.EventHandler(this.cmdCloseCognexControl_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(256, 667);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 38);
            this.button2.TabIndex = 5;
            this.button2.Text = "Save Cognex Tools";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdSubmitCalibPoints
            // 
            this.cmdSubmitCalibPoints.Location = new System.Drawing.Point(111, 667);
            this.cmdSubmitCalibPoints.Name = "cmdSubmitCalibPoints";
            this.cmdSubmitCalibPoints.Size = new System.Drawing.Size(132, 38);
            this.cmdSubmitCalibPoints.TabIndex = 51;
            this.cmdSubmitCalibPoints.Text = "Submit Calib Points";
            this.cmdSubmitCalibPoints.UseVisualStyleBackColor = true;
            this.cmdSubmitCalibPoints.Click += new System.EventHandler(this.cmdSubmitCalibPoints_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 157);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 26);
            this.button1.TabIndex = 6;
            this.button1.Text = "Find again";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(10, 180);
            this.cogRecordDisplay1.Margin = new System.Windows.Forms.Padding(2);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(743, 547);
            this.cogRecordDisplay1.TabIndex = 39;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSaveParamToOrder);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.Label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.contrastUpDown);
            this.groupBox1.Controls.Add(this.cmdCognexControl);
            this.groupBox1.Controls.Add(this.brightnessUpDown);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.shapeContainer1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.groupBox1.Location = new System.Drawing.Point(573, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(180, 163);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tool Control";
            // 
            // cmdSaveParamToOrder
            // 
            this.cmdSaveParamToOrder.Location = new System.Drawing.Point(125, 132);
            this.cmdSaveParamToOrder.Name = "cmdSaveParamToOrder";
            this.cmdSaveParamToOrder.Size = new System.Drawing.Size(48, 25);
            this.cmdSaveParamToOrder.TabIndex = 39;
            this.cmdSaveParamToOrder.Text = "Save";
            this.cmdSaveParamToOrder.UseVisualStyleBackColor = true;
            this.cmdSaveParamToOrder.Click += new System.EventHandler(this.cmdSaveParamToOrder_Click);
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.DecimalPlaces = 1;
            this.numericUpDown3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numericUpDown3.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown3.Location = new System.Drawing.Point(125, 16);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            361,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(47, 21);
            this.numericUpDown3.TabIndex = 37;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown3_ValueChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(5, 19);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 17);
            this.label6.TabIndex = 36;
            this.label6.Text = "Angle Range:";
            // 
            // Label4
            // 
            this.Label4.Location = new System.Drawing.Point(31, 111);
            this.Label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(76, 15);
            this.Label4.TabIndex = 26;
            this.Label4.Text = "Contrast:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(4, 64);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 20);
            this.label5.TabIndex = 30;
            this.label5.Text = "Approx No. to find";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 24;
            this.label2.Text = "Brightness:";
            // 
            // contrastUpDown
            // 
            this.contrastUpDown.DecimalPlaces = 1;
            this.contrastUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.contrastUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.contrastUpDown.Location = new System.Drawing.Point(126, 109);
            this.contrastUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.contrastUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.contrastUpDown.Name = "contrastUpDown";
            this.contrastUpDown.Size = new System.Drawing.Size(47, 21);
            this.contrastUpDown.TabIndex = 27;
            this.contrastUpDown.ValueChanged += new System.EventHandler(this.contrastUpDown_ValueChanged);
            // 
            // cmdCognexControl
            // 
            this.cmdCognexControl.Location = new System.Drawing.Point(7, 133);
            this.cmdCognexControl.Name = "cmdCognexControl";
            this.cmdCognexControl.Size = new System.Drawing.Size(112, 25);
            this.cmdCognexControl.TabIndex = 35;
            this.cmdCognexControl.Text = "Cognex Control";
            this.cmdCognexControl.UseVisualStyleBackColor = true;
            this.cmdCognexControl.Click += new System.EventHandler(this.cmdCognexControl_Click);
            // 
            // brightnessUpDown
            // 
            this.brightnessUpDown.DecimalPlaces = 1;
            this.brightnessUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.brightnessUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.brightnessUpDown.Location = new System.Drawing.Point(126, 86);
            this.brightnessUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.brightnessUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.brightnessUpDown.Name = "brightnessUpDown";
            this.brightnessUpDown.Size = new System.Drawing.Size(47, 21);
            this.brightnessUpDown.TabIndex = 25;
            this.brightnessUpDown.ValueChanged += new System.EventHandler(this.brightnessUpDown_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numericUpDown2.Location = new System.Drawing.Point(125, 63);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(47, 21);
            this.numericUpDown2.TabIndex = 31;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Accept Score";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(125, 41);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(47, 21);
            this.numericUpDown1.TabIndex = 29;
            this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(2, 18);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.ovalShape2,
            this.ovalShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(176, 143);
            this.shapeContainer1.TabIndex = 38;
            this.shapeContainer1.TabStop = false;
            // 
            // ovalShape2
            // 
            this.ovalShape2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ovalShape2.BackgroundImage")));
            this.ovalShape2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ovalShape2.Location = new System.Drawing.Point(6, 71);
            this.ovalShape2.Name = "ovalShape2";
            this.ovalShape2.Size = new System.Drawing.Size(16, 16);
            // 
            // ovalShape1
            // 
            this.ovalShape1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ovalShape1.BackgroundImage")));
            this.ovalShape1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ovalShape1.Location = new System.Drawing.Point(6, 94);
            this.ovalShape1.Name = "ovalShape1";
            this.ovalShape1.Size = new System.Drawing.Size(16, 16);
            // 
            // frmPatMax
            // 
            this.frmPatMax.Controls.Add(this.txtPatMaxScoreValue);
            this.frmPatMax.Controls.Add(this.Label1);
            this.frmPatMax.Controls.Add(this.dataGridView1);
            this.frmPatMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.frmPatMax.Location = new System.Drawing.Point(312, 10);
            this.frmPatMax.Margin = new System.Windows.Forms.Padding(2);
            this.frmPatMax.Name = "frmPatMax";
            this.frmPatMax.Padding = new System.Windows.Forms.Padding(2);
            this.frmPatMax.Size = new System.Drawing.Size(257, 163);
            this.frmPatMax.TabIndex = 36;
            this.frmPatMax.TabStop = false;
            this.frmPatMax.Text = "Result";
            // 
            // txtPatMaxScoreValue
            // 
            this.txtPatMaxScoreValue.Location = new System.Drawing.Point(122, 22);
            this.txtPatMaxScoreValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtPatMaxScoreValue.Multiline = true;
            this.txtPatMaxScoreValue.Name = "txtPatMaxScoreValue";
            this.txtPatMaxScoreValue.Size = new System.Drawing.Size(56, 22);
            this.txtPatMaxScoreValue.TabIndex = 2;
            this.txtPatMaxScoreValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(4, 22);
            this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(114, 18);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Founded Inserts:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Score,
            this.Rotation,
            this.X,
            this.Y});
            this.dataGridView1.Location = new System.Drawing.Point(6, 47);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(246, 109);
            this.dataGridView1.TabIndex = 40;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 28;
            // 
            // Score
            // 
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            this.Score.ReadOnly = true;
            this.Score.Width = 50;
            // 
            // Rotation
            // 
            this.Rotation.HeaderText = "Rotation";
            this.Rotation.Name = "Rotation";
            this.Rotation.ReadOnly = true;
            this.Rotation.Width = 62;
            // 
            // X
            // 
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.Width = 60;
            // 
            // Y
            // 
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.Width = 60;
            // 
            // frmImageAcquisitionFrame
            // 
            this.frmImageAcquisitionFrame.Controls.Add(this.chkDisplayResults);
            this.frmImageAcquisitionFrame.Controls.Add(this.txtCurrentIndex);
            this.frmImageAcquisitionFrame.Controls.Add(this.cmdTeach);
            this.frmImageAcquisitionFrame.Controls.Add(this.button4);
            this.frmImageAcquisitionFrame.Controls.Add(this.button3);
            this.frmImageAcquisitionFrame.Controls.Add(this.groupBox4);
            this.frmImageAcquisitionFrame.Controls.Add(this.checkBox1);
            this.frmImageAcquisitionFrame.Controls.Add(this.cmdPatMaxRunCommand);
            this.frmImageAcquisitionFrame.Controls.Add(this.cmdImageAcquisitionNewImageCommand);
            this.frmImageAcquisitionFrame.Controls.Add(this.cmdImageAcquisitionLiveOrOpenCommand);
            this.frmImageAcquisitionFrame.Controls.Add(this.CogDisplay1);
            this.frmImageAcquisitionFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.frmImageAcquisitionFrame.Location = new System.Drawing.Point(10, 10);
            this.frmImageAcquisitionFrame.Margin = new System.Windows.Forms.Padding(2);
            this.frmImageAcquisitionFrame.Name = "frmImageAcquisitionFrame";
            this.frmImageAcquisitionFrame.Padding = new System.Windows.Forms.Padding(2);
            this.frmImageAcquisitionFrame.Size = new System.Drawing.Size(296, 163);
            this.frmImageAcquisitionFrame.TabIndex = 35;
            this.frmImageAcquisitionFrame.TabStop = false;
            this.frmImageAcquisitionFrame.Text = "Operation Control";
            // 
            // txtCurrentIndex
            // 
            this.txtCurrentIndex.Location = new System.Drawing.Point(93, 102);
            this.txtCurrentIndex.Name = "txtCurrentIndex";
            this.txtCurrentIndex.Size = new System.Drawing.Size(55, 23);
            this.txtCurrentIndex.TabIndex = 7;
            this.txtCurrentIndex.TextChanged += new System.EventHandler(this.txtCurrentIndex_TextChanged);
            // 
            // cmdTeach
            // 
            this.cmdTeach.Image = ((System.Drawing.Image)(resources.GetObject("cmdTeach.Image")));
            this.cmdTeach.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTeach.Location = new System.Drawing.Point(220, 21);
            this.cmdTeach.Margin = new System.Windows.Forms.Padding(2);
            this.cmdTeach.Name = "cmdTeach";
            this.cmdTeach.Size = new System.Drawing.Size(72, 70);
            this.cmdTeach.TabIndex = 4;
            this.cmdTeach.Text = "Teach";
            this.cmdTeach.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdTeach.UseVisualStyleBackColor = true;
            this.cmdTeach.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(163, 94);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 38);
            this.button4.TabIndex = 7;
            this.button4.Text = "Reset Index";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(4, 94);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(155, 38);
            this.button3.TabIndex = 6;
            this.button3.Text = "Next Index";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.optImageAcquisitionOptionImageFile);
            this.groupBox4.Controls.Add(this.optImageAcquisitionOptionFrameGrabber);
            this.groupBox4.Location = new System.Drawing.Point(193, 137);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(103, 84);
            this.groupBox4.TabIndex = 38;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "For testing";
            this.groupBox4.Visible = false;
            // 
            // optImageAcquisitionOptionImageFile
            // 
            this.optImageAcquisitionOptionImageFile.Location = new System.Drawing.Point(9, 18);
            this.optImageAcquisitionOptionImageFile.Margin = new System.Windows.Forms.Padding(2);
            this.optImageAcquisitionOptionImageFile.Name = "optImageAcquisitionOptionImageFile";
            this.optImageAcquisitionOptionImageFile.Size = new System.Drawing.Size(94, 22);
            this.optImageAcquisitionOptionImageFile.TabIndex = 1;
            this.optImageAcquisitionOptionImageFile.Text = "Image File";
            this.optImageAcquisitionOptionImageFile.Visible = false;
            this.optImageAcquisitionOptionImageFile.CheckedChanged += new System.EventHandler(this.optImageAcquisitionOptionImageFile_CheckedChanged);
            // 
            // optImageAcquisitionOptionFrameGrabber
            // 
            this.optImageAcquisitionOptionFrameGrabber.Checked = true;
            this.optImageAcquisitionOptionFrameGrabber.Location = new System.Drawing.Point(4, 44);
            this.optImageAcquisitionOptionFrameGrabber.Margin = new System.Windows.Forms.Padding(2);
            this.optImageAcquisitionOptionFrameGrabber.Name = "optImageAcquisitionOptionFrameGrabber";
            this.optImageAcquisitionOptionFrameGrabber.Size = new System.Drawing.Size(94, 23);
            this.optImageAcquisitionOptionFrameGrabber.TabIndex = 0;
            this.optImageAcquisitionOptionFrameGrabber.TabStop = true;
            this.optImageAcquisitionOptionFrameGrabber.Text = "Frame Grabber";
            this.optImageAcquisitionOptionFrameGrabber.Visible = false;
            this.optImageAcquisitionOptionFrameGrabber.CheckedChanged += new System.EventHandler(this.optImageAcquisitionOptionImageFile_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(4, 136);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(89, 21);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Full frame";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cmdPatMaxRunCommand
            // 
            this.cmdPatMaxRunCommand.Image = ((System.Drawing.Image)(resources.GetObject("cmdPatMaxRunCommand.Image")));
            this.cmdPatMaxRunCommand.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdPatMaxRunCommand.Location = new System.Drawing.Point(148, 21);
            this.cmdPatMaxRunCommand.Margin = new System.Windows.Forms.Padding(2);
            this.cmdPatMaxRunCommand.Name = "cmdPatMaxRunCommand";
            this.cmdPatMaxRunCommand.Size = new System.Drawing.Size(72, 70);
            this.cmdPatMaxRunCommand.TabIndex = 1;
            this.cmdPatMaxRunCommand.Text = "Find";
            this.cmdPatMaxRunCommand.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdPatMaxRunCommand.Click += new System.EventHandler(this.Subject_Ran);
            // 
            // cmdImageAcquisitionNewImageCommand
            // 
            this.cmdImageAcquisitionNewImageCommand.Image = ((System.Drawing.Image)(resources.GetObject("cmdImageAcquisitionNewImageCommand.Image")));
            this.cmdImageAcquisitionNewImageCommand.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdImageAcquisitionNewImageCommand.Location = new System.Drawing.Point(4, 21);
            this.cmdImageAcquisitionNewImageCommand.Margin = new System.Windows.Forms.Padding(2);
            this.cmdImageAcquisitionNewImageCommand.Name = "cmdImageAcquisitionNewImageCommand";
            this.cmdImageAcquisitionNewImageCommand.Size = new System.Drawing.Size(72, 70);
            this.cmdImageAcquisitionNewImageCommand.TabIndex = 3;
            this.cmdImageAcquisitionNewImageCommand.Text = "Acquire";
            this.cmdImageAcquisitionNewImageCommand.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdImageAcquisitionNewImageCommand.Click += new System.EventHandler(this.cmdImageAcquisitionNewImageCommand_Click);
            // 
            // cmdImageAcquisitionLiveOrOpenCommand
            // 
            this.cmdImageAcquisitionLiveOrOpenCommand.Image = ((System.Drawing.Image)(resources.GetObject("cmdImageAcquisitionLiveOrOpenCommand.Image")));
            this.cmdImageAcquisitionLiveOrOpenCommand.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdImageAcquisitionLiveOrOpenCommand.Location = new System.Drawing.Point(76, 21);
            this.cmdImageAcquisitionLiveOrOpenCommand.Margin = new System.Windows.Forms.Padding(2);
            this.cmdImageAcquisitionLiveOrOpenCommand.Name = "cmdImageAcquisitionLiveOrOpenCommand";
            this.cmdImageAcquisitionLiveOrOpenCommand.Size = new System.Drawing.Size(72, 70);
            this.cmdImageAcquisitionLiveOrOpenCommand.TabIndex = 2;
            this.cmdImageAcquisitionLiveOrOpenCommand.Text = "Video";
            this.cmdImageAcquisitionLiveOrOpenCommand.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdImageAcquisitionLiveOrOpenCommand.Click += new System.EventHandler(this.cmdImageAcquisitionLiveOrOpenCommand_Click);
            // 
            // CogDisplay1
            // 
            this.CogDisplay1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CogDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.CogDisplay1.ColorMapLowerRoiLimit = 0D;
            this.CogDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.CogDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.CogDisplay1.ColorMapUpperRoiLimit = 1D;
            this.CogDisplay1.Location = new System.Drawing.Point(397, 80);
            this.CogDisplay1.Margin = new System.Windows.Forms.Padding(2);
            this.CogDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.CogDisplay1.MouseWheelSensitivity = 1D;
            this.CogDisplay1.Name = "CogDisplay1";
            this.CogDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("CogDisplay1.OcxState")));
            this.CogDisplay1.Size = new System.Drawing.Size(334, 259);
            this.CogDisplay1.TabIndex = 0;
            this.CogDisplay1.Visible = false;
            // 
            // VProAppTab
            // 
            this.VProAppTab.Controls.Add(this.TabPage1);
            this.VProAppTab.Controls.Add(this.TabPage2);
            this.VProAppTab.Controls.Add(this.FrameGrabber);
            this.VProAppTab.Controls.Add(this.TabPage3);
            this.VProAppTab.Location = new System.Drawing.Point(-3, -21);
            this.VProAppTab.Margin = new System.Windows.Forms.Padding(2);
            this.VProAppTab.Name = "VProAppTab";
            this.VProAppTab.SelectedIndex = 0;
            this.VProAppTab.Size = new System.Drawing.Size(1351, 810);
            this.VProAppTab.TabIndex = 6;
            // 
            // TabPage2
            // 
            this.TabPage2.Controls.Add(this.CogImageFileEdit1);
            this.TabPage2.Location = new System.Drawing.Point(4, 22);
            this.TabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Size = new System.Drawing.Size(1343, 784);
            this.TabPage2.TabIndex = 2;
            this.TabPage2.Text = "Image File";
            // 
            // CogImageFileEdit1
            // 
            this.CogImageFileEdit1.AllowDrop = true;
            this.CogImageFileEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CogImageFileEdit1.Location = new System.Drawing.Point(0, 0);
            this.CogImageFileEdit1.Margin = new System.Windows.Forms.Padding(2);
            this.CogImageFileEdit1.MinimumSize = new System.Drawing.Size(367, 0);
            this.CogImageFileEdit1.Name = "CogImageFileEdit1";
            this.CogImageFileEdit1.OutputHighLight = System.Drawing.Color.Lime;
            this.CogImageFileEdit1.Size = new System.Drawing.Size(1343, 784);
            this.CogImageFileEdit1.SuspendElectricRuns = false;
            this.CogImageFileEdit1.TabIndex = 0;
            // 
            // FrameGrabber
            // 
            this.FrameGrabber.Controls.Add(this.CogAcqFifoEdit1);
            this.FrameGrabber.Location = new System.Drawing.Point(4, 22);
            this.FrameGrabber.Margin = new System.Windows.Forms.Padding(2);
            this.FrameGrabber.Name = "FrameGrabber";
            this.FrameGrabber.Size = new System.Drawing.Size(1343, 784);
            this.FrameGrabber.TabIndex = 1;
            this.FrameGrabber.Text = "FrameGrabber";
            // 
            // CogAcqFifoEdit1
            // 
            this.CogAcqFifoEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CogAcqFifoEdit1.Location = new System.Drawing.Point(0, 0);
            this.CogAcqFifoEdit1.Margin = new System.Windows.Forms.Padding(2);
            this.CogAcqFifoEdit1.MinimumSize = new System.Drawing.Size(367, 0);
            this.CogAcqFifoEdit1.Name = "CogAcqFifoEdit1";
            this.CogAcqFifoEdit1.Size = new System.Drawing.Size(1343, 784);
            this.CogAcqFifoEdit1.SuspendElectricRuns = false;
            this.CogAcqFifoEdit1.TabIndex = 0;
            // 
            // TabPage3
            // 
            this.TabPage3.Controls.Add(this.CogPMAlignEdit1);
            this.TabPage3.Location = new System.Drawing.Point(4, 22);
            this.TabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1343, 784);
            this.TabPage3.TabIndex = 3;
            this.TabPage3.Text = "PatMax";
            // 
            // CogPMAlignEdit1
            // 
            this.CogPMAlignEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CogPMAlignEdit1.Location = new System.Drawing.Point(0, 0);
            this.CogPMAlignEdit1.Margin = new System.Windows.Forms.Padding(2);
            this.CogPMAlignEdit1.MinimumSize = new System.Drawing.Size(367, 0);
            this.CogPMAlignEdit1.Name = "CogPMAlignEdit1";
            this.CogPMAlignEdit1.Size = new System.Drawing.Size(1343, 784);
            this.CogPMAlignEdit1.SuspendElectricRuns = false;
            this.CogPMAlignEdit1.TabIndex = 0;
            // 
            // chkDisplayResults
            // 
            this.chkDisplayResults.AutoSize = true;
            this.chkDisplayResults.Location = new System.Drawing.Point(93, 136);
            this.chkDisplayResults.Margin = new System.Windows.Forms.Padding(2);
            this.chkDisplayResults.Name = "chkDisplayResults";
            this.chkDisplayResults.Size = new System.Drawing.Size(119, 21);
            this.chkDisplayResults.TabIndex = 39;
            this.chkDisplayResults.Text = "Display results";
            this.chkDisplayResults.UseVisualStyleBackColor = true;
            this.chkDisplayResults.CheckedChanged += new System.EventHandler(this.chkDisplayResults_CheckedChanged);
            // 
            // FrmLoadTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 752);
            this.Controls.Add(this.VProAppTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "FrmLoadTray";
            this.Text = "Load Tray";
            this.Load += new System.EventHandler(this.LoadPalletForm_Load);
            this.TabPage1.ResumeLayout(false);
            this.groupPassword.ResumeLayout(false);
            this.groupPassword.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.frmPatMax.ResumeLayout(false);
            this.frmPatMax.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.frmImageAcquisitionFrame.ResumeLayout(false);
            this.frmImageAcquisitionFrame.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CogDisplay1)).EndInit();
            this.VProAppTab.ResumeLayout(false);
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CogImageFileEdit1)).EndInit();
            this.FrameGrabber.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CogAcqFifoEdit1)).EndInit();
            this.TabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CogPMAlignEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabPage TabPage1;
        private System.Windows.Forms.TabControl VProAppTab;
        private System.Windows.Forms.TabPage TabPage2;
        private Cognex.VisionPro.ImageFile.CogImageFileEditV2 CogImageFileEdit1;
        internal System.Windows.Forms.TabPage FrameGrabber;
        internal Cognex.VisionPro.CogAcqFifoEditV2 CogAcqFifoEdit1;
        private System.Windows.Forms.TabPage TabPage3;
        private Cognex.VisionPro.PMAlign.CogPMAlignEditV2 CogPMAlignEdit1;
        private System.Windows.Forms.OpenFileDialog ImageAcquisitionCommonDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.NumericUpDown numericUpDown2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.NumericUpDown numericUpDown1;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown contrastUpDown;
        internal System.Windows.Forms.NumericUpDown brightnessUpDown;
        internal System.Windows.Forms.GroupBox frmPatMax;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdTeach;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtPatMaxScoreValue;
        private System.Windows.Forms.Button cmdPatMaxRunCommand;
        internal System.Windows.Forms.GroupBox frmImageAcquisitionFrame;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button cmdImageAcquisitionNewImageCommand;
        private System.Windows.Forms.Button cmdImageAcquisitionLiveOrOpenCommand;
        internal Cognex.VisionPro.Display.CogDisplay CogDisplay1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton optImageAcquisitionOptionImageFile;
        private System.Windows.Forms.RadioButton optImageAcquisitionOptionFrameGrabber;
        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rotation;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.TextBox txtCurrentIndex;
        private System.Windows.Forms.Button cmdSubmitCalibPoints;
        private System.Windows.Forms.Button cmdCognexControl;
        internal System.Windows.Forms.NumericUpDown numericUpDown3;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cmdCloseCognexControl;
        private System.Windows.Forms.GroupBox groupBox2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape2;
        private Microsoft.VisualBasic.PowerPacks.OvalShape ovalShape1;
        private System.Windows.Forms.Button cmdSaveParamToOrder;
        private System.Windows.Forms.GroupBox groupPassword;
        private System.Windows.Forms.Button cmdPassword;
        private System.Windows.Forms.TextBox txtInputPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.CheckBox chkDisplayResults;


    }
}