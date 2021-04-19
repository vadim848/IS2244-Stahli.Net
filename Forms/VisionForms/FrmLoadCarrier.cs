using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
    public partial class FrmLoadCarrier : Form
    {
        #region "Module Level vars"

        //CogAcqFifoTool AcqFifoTool;
        CogImageFileTool ImageFileTool;
        CogPMAlignTool PatMaxTool;
        CogAcqFifoTool AcqFifoTool;
        CogAcqFifoTool cogAcqFifoTool;

        //Flag for "VisionPro Demo" tab indicating that user is currently setting up a
        //tool.  Also used to indicate in live video mode.  If user selects "Setup"
        //then the GUI controls are disabled except for the interactive graphics
        //required for setup as well as the "OK" button used to complete the Setup.
        CogTransform2DLinear cogTransform2DLinearN2N;
        bool SettingUp;
        // values passed to EnableAll & DisableAll subroutines which
        //indicates what is being setup thus determining which Buttons on the GUI
        //should be left enabled.
        public enum SettingUpConstants : int
        {
            settingUpPatMax,
            settingLiveVideo
        }

        SettingUpConstants settingUpPatMax = SettingUpConstants.settingUpPatMax;
        SettingUpConstants settingLiveVideo = SettingUpConstants.settingLiveVideo;
        CogImageFileTool mIFTool;
        CogAcqFifoTool mAcqTool;


        //long numPass = 0;
        //long numFail = 0;

        CogPMAlignTool mPMAlignTool;

        ICogAcqFifo myAcqFifo;

        private CogFrameGrabbers myFrameGrabbers;
        private ICogFrameGrabber myFrameGrabber;

        ICogAcqBrightness brightnessParams;
        ICogAcqContrast contrastParams;
        //CogPMAlignToolCenter

        //private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        CogToolBlock toolBlock = new CogToolBlock();
        public CogPMAlignTool cogPMAlignTool, cogPMAlignToolCenter;
        public CogPMAlignTool cogPMAlignToolFindOneInsert;  //asaf
        public CogCreateLinePerpendicularTool cogCreateLinePerpendicularTool;

        CogCoordinateAxes axes, axesCenter;

        // This is the calibration tool.
        private CogCalibNPointToNPointTool CalibNPointTool;
        // This object holds calibration operator.
        private CogCalibNPointToNPoint Calib;

        CogPolygon cogPolygon = new CogPolygon();

        //Acquire region params
        ICogAcqROI ROIParams;
        const int MaxXWidth = 4000;
        const int MaxYWidth = 4000;
        const int MinXWidth = 4000;
        const int MinYWidth = 4000;
        const int StartXIndx = 200;//320;
        const int StartYIndx = 0;//61;
        const int DeltaXFrameMov = 1000;
        const int DeltaYFrameMov = 1000;
        int xMov, yMov = 0;
        public bool flagFullTray = true;
        int countFrames = 0;
        //Search region params
        double xSearchReg, ySearchReg;
        double RadiusSearchReg;  //asaf

        int CurrIndex;   //use to be called frameIndex;


        //CogRectangle PatMaxSearchRegion = new CogRectangle();

        private CogCalibCheckerboardTool calbCheckerBoard;

        CogCircle PatMaxSearchRegion = new CogCircle();


        //ReadFromMainProj fromMainProject = new ReadFromMainProj();
        //        Calculate calculations = new Calculate();

        //25.03.14
        CogAcqFifoTool cogAcqTool = new CogAcqFifoTool();
        CogCreateSegmentAvgSegsTool cogCreateSegmentAvgSegsTool = new CogCreateSegmentAvgSegsTool();


        #endregion

        public FrmLoadCarrier()
        {
            visionActionDelegate = new VisionActionDelegate(VisionActionDelegateFunc);
            InitializeComponent();
            //this.TopLevel = false;

            //Add any initialization after the InitializeComponent() call
            cogToolBlockEditV21.LocalDisplayVisible = false;
            mIFTool = new CogImageFileTool();
         //   mIFTool.Operator.Open(Environment.GetEnvironmentVariable("VPRO_ROOT") + @"\images\coins.idb", CogImageFileModeConstants.Read);
            mAcqTool = new CogAcqFifoTool();


            //17.02.14
            toolBlock = cogToolBlockEditV21.Subject;
            try
            {
                //toolBlock = CogSerializer.LoadObjectFromFile(@"C:\PROJECTS\Stahli.Net\Bin\Debug\CognexStahli\Camera2.vpp") as CogToolBlock;
                toolBlock = CogSerializer.LoadObjectFromFile(System.IO.Directory.GetCurrentDirectory() + "\\CognexStahli\\Camera2.vpp") as CogToolBlock;
                //if it is new insert, upload a defult
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tool block is error");
            }

            if (toolBlock != null)
            {
                cogToolBlockEditV21.Subject = toolBlock;
                cogToolBlockEditV21.Subject.Ran += new EventHandler(Subject_Ran);
                cogToolBlockEditV21.SubjectChanged += new EventHandler(cogToolBlockEditV21_SubjectChanged);
            }

            foreach (ICogTool tool in toolBlock.Tools)
            {
                calbCheckerBoard = tool as CogCalibCheckerboardTool;
                if (calbCheckerBoard != null) break;
            }

            foreach (ICogTool tool in toolBlock.Tools)
            {
                CalibNPointTool = tool as CogCalibNPointToNPointTool;
                if (CalibNPointTool != null) break;
            }

            cogPMAlignTool = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolTechHoles"] as CogPMAlignTool;
            cogPMAlignToolCenter = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolCenter"] as CogPMAlignTool;
            cogPMAlignToolFindOneInsert = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolFindOneInsert"] as CogPMAlignTool;

            // LoadPatternFromFile();   //13.07.15 (Ziv)
            loadOrderDataDelegate = new LoadOrderDataDelegate(LoadOrderDataDelegateFunc);
        }

        public void InitVision()
        {
            SettingUp = false;
            //Set reference to CogImageFileTool created by Edit Control
            //The Image File Edit Control creates its subject when its AutoCreateTool property is True
            ImageFileTool = CogImageFileEdit1.Subject;
            ImageFileTool.Ran += ImageFileTool_Ran;   //HERE
            //Set reference to CogAcqFifoTool created by Edit Control
            //The Acq Fifo Edit Control creates its subject when its AutoCreateTool property is True
            AcqFifoTool = CogAcqFifoEdit1.Subject;
            AcqFifoTool.Ran += AcqFifoTool_Ran;

            //25.03.14
            cogAcqTool = CogAcqFifoEdit1.Subject;
            cogAcqTool = cogToolBlockEditV21.Subject.Tools["CogAcqFifoTool1"] as CogAcqFifoTool;
            cogAcqTool.Run();

            //Operator will be Nothing if no Frame Grabber is available.  Disable the Frame Grabber
            //option on the "VisionPro Demo" tab if no frame grabber available.
            if (AcqFifoTool.Operator == null)
            {
                optImageAcquisitionOptionFrameGrabber.Enabled = false;
            }

            //Initialize the Dialog box for the "Open File" button on the "VisionPro Demo" tab.
            ImageAcquisitionCommonDialog.Filter = ImageFileTool.Operator.FilterText;
            ImageAcquisitionCommonDialog.CheckFileExists = true;
            ImageAcquisitionCommonDialog.ReadOnlyChecked = true;

            //AutoCreateTool for the PMAlign edit control is False, therefore, we must create
            //a PMAlign tool and set the subject of the control to reference the new tool.
            PatMaxTool = new CogPMAlignTool();
            PatMaxTool.Changed += PatMaxTool_Changed;
            CogPMAlignEdit1.Subject = PatMaxTool;

            //Change the default Train Region to center of a 640x480 image & change the DOFs
            //so that Skew is not enabled.  Note - TrainRegion is of type ICogRegion, therefore,
            //we must use a CogRectangleAffine reference in order to call CogRectangleAffine
            //properties.
            CogRectangleAffine PatMaxTrainRegion = default(CogRectangleAffine);
            PatMaxTrainRegion = PatMaxTool.Pattern.TrainRegion as CogRectangleAffine;
            //PatMaxTrainRegion = cogPMAlignTool.Pattern.TrainRegion as CogRectangleAffine;
            if ((PatMaxTrainRegion != null))
            {
                PatMaxTrainRegion.SetCenterLengthsRotationSkew(320, 240, 100, 100, 0, 0);
                PatMaxTrainRegion.GraphicDOFEnable = CogRectangleAffineDOFConstants.Position | CogRectangleAffineDOFConstants.Rotation | CogRectangleAffineDOFConstants.Size;
            }

            //Create a SearchRegion that uses the entire image (assumes 640x480)
            //Note that by default the SearchRegion is Nothing and PMAlign will search the entire
            //image anyway.  This is added for sample code purposes & to graphically show that the
            //entire image is being used.
            //CogRectangle PatMaxSearchRegion = new CogRectangle();
            //PatMaxTool.SearchRegion = PatMaxSearchRegion;
            //cogPMAlignTool.SearchRegion = PatMaxSearchRegion;
            //PatMaxSearchRegion.SetCenterWidthHeight(320, 240, 640, 480);
            //PatMaxSearchRegion.GraphicDOFEnable = CogRectangleDOFConstants.Position | CogRectangleDOFConstants.Size;
            //PatMaxSearchRegion.Interactive = true;


            numericUpDown1.Value = Convert.ToDecimal(cogPMAlignTool.RunParams.AcceptThreshold);
            PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);

            numericUpDown2.Value = Convert.ToDecimal(cogPMAlignTool.RunParams.ApproximateNumberToFind);
            PatMaxTool.RunParams.ApproximateNumberToFind = Convert.ToInt32(numericUpDown2.Value);


            AppGen.Inst.Calculate.CalcSearchReg(out xSearchReg, out ySearchReg);
            AppGen.Inst.Calculate.CalcSearchRadiusReg(out RadiusSearchReg);

            myAcqFifo = AcqFifoTool.Operator;
            try
            {
                myFrameGrabber = cogAcqTool.Operator.FrameGrabber;
                myAcqFifo = cogAcqTool.Operator;
                AcqFifoTool.Operator = myAcqFifo;
                myAcqFifo.OwnedContrastParams.Contrast = cogAcqTool.Operator.OwnedContrastParams.Contrast;
                myAcqFifo.OwnedBrightnessParams.Brightness = cogAcqTool.Operator.OwnedBrightnessParams.Brightness;
                brightnessParams = myAcqFifo.OwnedBrightnessParams;
                contrastParams = myAcqFifo.OwnedContrastParams;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            //brightnessParams = myAcqFifo.OwnedBrightnessParams; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (brightnessParams != null)
                brightnessUpDown.Value = Convert.ToDecimal(brightnessParams.Brightness);
            //contrastParams = myAcqFifo.OwnedContrastParams; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (contrastParams != null)
                contrastUpDown.Value = Convert.ToDecimal(contrastParams.Contrast);
            try
            {
                cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmdImageAcquisitionLiveOrOpenCommand_Click(System.Object sender, System.EventArgs e)
        {

            //Clear graphics, assuming a new image will be in the display once user
            //completes either Live Video or Open File operation, therefore, graphics
            //will be out of sync.
            CogDisplay1.StaticGraphics.Clear();
            CogDisplay1.InteractiveGraphics.Clear();

            cogRecordDisplay1.StaticGraphics.Clear();
            cogRecordDisplay1.InteractiveGraphics.Clear();

            ROIParams = myAcqFifo.OwnedROIParams;

            ROIParams.SetROIXYWidthHeight(StartXIndx, StartYIndx, MaxXWidth, MaxYWidth);

            //"Live Video"  & "Stop Live" button when Frame Grabber option is selected.
            //Using our EnableAll & DisableAll subroutine to force the user stop live
            //video before doing anything else.
            if (optImageAcquisitionOptionFrameGrabber.Checked == true)
            {
                if (cogRecordDisplay1.LiveDisplayRunning)
                {
                    cogRecordDisplay1.StopLiveDisplay();
                    EnableAll(settingLiveVideo);
                    //Run the AcqFifoTool so that all of the sample app images get the last
                    AcqFifoTool.Run();

                    cogRecordDisplay1.Fit(true);
                    cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;

                }
                else if ((AcqFifoTool.Operator != null))
                {
                    AcqFifoTool.Run();

                    cogRecordDisplay1.Fit(true);
                    cogRecordDisplay1.StartLiveDisplay(AcqFifoTool.Operator, false);
                    cogRecordDisplay1.Fit(true);
                    cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
                    DisableAll(settingLiveVideo);
                    cogRecordDisplay1.Fit(true);
                }

            }
            else
            {
                //"Open File" button when image file option is selected
                //DrawingEnabled is used to simply hide the image while the Fit is performed.
                //This prevents the image from being diplayed at the initial zoom factor
                //prior to fit being called.
                try
                {
                    DialogResult result = ImageAcquisitionCommonDialog.ShowDialog();
                    if (result != System.Windows.Forms.DialogResult.Cancel)
                    {
                        ImageFileTool.Operator.Open(ImageAcquisitionCommonDialog.FileName, CogImageFileModeConstants.Read);
                        cogRecordDisplay1.DrawingEnabled = false;
                        ImageFileTool.Run();
                        cogRecordDisplay1.Fit(true);
                        cogRecordDisplay1.DrawingEnabled = true;
                    }
                }
                catch (CogException cogex)
                {
                    MessageBox.Show("Following Specific Cognex Error Occured:" + cogex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Following Error Occured:" + ex.Message);
                }
                cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image; //!!!!! Neet to solve why pic doesn't get into tool
            }
            cogRecordDisplay1.Fit(true);

        }

        private void cmdImageAcquisitionNewImageCommand_Click(System.Object sender, System.EventArgs e)
        {
            cbxRotationResults.Enabled = false;
            cbxHolesResult.Enabled = false;


            ROIParams = myAcqFifo.OwnedROIParams;

            ROIParams.SetROIXYWidthHeight(StartXIndx, StartYIndx, MaxXWidth, MaxYWidth);

            if (optImageAcquisitionOptionFrameGrabber.Checked == true)
            {
                AcqFifoTool.Run();

                cogRecordDisplay1.Fit(true);



            }
            else
            {
                ImageFileTool.Run();








            }


            cogRecordDisplay1.Fit(true);
            cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;






        }

        private void optImageAcquisitionOptionFrameGrabber_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (optImageAcquisitionOptionFrameGrabber.Checked == true)
            {
                cmdImageAcquisitionLiveOrOpenCommand.Text = "Live Video";
                cmdImageAcquisitionNewImageCommand.Text = "Acquire";
            }
            else
            {
                cmdImageAcquisitionLiveOrOpenCommand.Text = "Open File";
                cmdImageAcquisitionNewImageCommand.Text = "Next Image";
            }
            cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
        }

        private void optImageAcquisitionOptionImageFile_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if (optImageAcquisitionOptionImageFile.Checked == true)
            {
                cmdImageAcquisitionLiveOrOpenCommand.Text = "Open File";
                cmdImageAcquisitionNewImageCommand.Text = "Next Image";
            }
            else
            {
                cmdImageAcquisitionLiveOrOpenCommand.Text = "Live Video";
                cmdImageAcquisitionNewImageCommand.Text = "Acquire";
            }
            cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
        }

        private void cmdPatMaxSetupCommand_Click(System.Object sender, System.EventArgs e)
        {
            cogRecordDisplay1.InteractiveGraphics.Add(cogPMAlignTool.Pattern.TrainRegion as ICogGraphicInteractive, "test", false);
            // Add an origin graphic with tip text to distinguish them
            axes = new CogCoordinateAxes();
            axes.Transform = cogPMAlignTool.Pattern.Origin;
            //axes.TipText = "PatMax Pattern Origin " + i.ToString();
            axes.GraphicDOFEnable = CogCoordinateAxesDOFConstants.All &
                ~CogCoordinateAxesDOFConstants.Skew;
            axes.Interactive = true;
            // Add a standard VisionPro "manipulable" mouse cursor.
            axes.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
            axes.XAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
            axes.YAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
            cogRecordDisplay1.InteractiveGraphics.Add((ICogGraphicInteractive)axes, "test", false);

            cogRecordDisplay1.InteractiveGraphics.Add(cogPMAlignToolCenter.Pattern.TrainRegion as ICogGraphicInteractive, "test", false);
            // Add an origin graphic with tip text to distinguish them
            axesCenter = new CogCoordinateAxes();
            axesCenter.Transform = cogPMAlignToolCenter.Pattern.Origin;
            //axes.TipText = "PatMax Pattern Origin " + i.ToString();
            axesCenter.GraphicDOFEnable = CogCoordinateAxesDOFConstants.All &
                ~CogCoordinateAxesDOFConstants.Skew;
            axesCenter.Interactive = true;
            // Add a standard VisionPro "manipulable" mouse cursor.
            axesCenter.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
            axesCenter.XAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
            axesCenter.YAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
            cogRecordDisplay1.InteractiveGraphics.Add((ICogGraphicInteractive)axesCenter, "test", false);
        }
        private void DisableAll(SettingUpConstants butThis)
        {
            //Disable all of the frames (Disables controls within frame)
            //  frmImageAcquisitionFrame.Enabled = false;
            frmPatMax.Enabled = false;
            //Disable all of the tabs except "VisionPro Demo" tab.
            VProAppTab.TabPages[1].Enabled = false;
            VProAppTab.TabPages[2].Enabled = false;
            VProAppTab.TabPages[3].Enabled = false;
            //Based on what the user is doing, Re-enable appropriate frame and disable
            //specific controls within the frame.
            if (butThis == settingUpPatMax)
            {
                frmPatMax.Enabled = true;
                //cmdPatMaxSetupCommand.Text = "OK";
                cmdTeach.Text = "OK";
                cmdPatMaxRunCommand.Enabled = false;
            }
            else if (butThis == settingLiveVideo)
            {
                //    frmImageAcquisitionFrame.Enabled = true;
                cmdImageAcquisitionLiveOrOpenCommand.Text = "Stop Live";
                cmdImageAcquisitionNewImageCommand.Enabled = false;
                optImageAcquisitionOptionFrameGrabber.Enabled = false;
                optImageAcquisitionOptionImageFile.Enabled = false;
            }
        }
        //Enable all of the GUI controls when done a task.  Example, done setting up PMAlign.
        private void EnableAll(SettingUpConstants butThis)
        {
            //   frmImageAcquisitionFrame.Enabled = true;
            frmPatMax.Enabled = true;
            VProAppTab.TabPages[1].Enabled = true;
            VProAppTab.TabPages[2].Enabled = true;
            VProAppTab.TabPages[3].Enabled = true;
            if (butThis == settingUpPatMax)
            {
                //cmdPatMaxSetupCommand.Text = "Setup";
                cmdTeach.Text = "Teach";
                cmdPatMaxRunCommand.Enabled = true;
            }
            else if (butThis == settingLiveVideo)
            {
                cmdImageAcquisitionLiveOrOpenCommand.Text = "Live Video";
                cmdImageAcquisitionNewImageCommand.Enabled = true;
                optImageAcquisitionOptionFrameGrabber.Enabled = true;
                optImageAcquisitionOptionImageFile.Enabled = true;
            }
        }
        int static_AcqFifoTool_Ran_numacqs;
        private void AcqFifoTool_Ran(object sender, System.EventArgs e)
        {

            cogRecordDisplay1.InteractiveGraphics.Clear();
            cogRecordDisplay1.StaticGraphics.Clear();
            //cogRecordDisplay1.Image = AcqFifoTool.OutputImage;

            calbCheckerBoard = cogToolBlockEditV21.Subject.Tools["CogCalibCheckerboardTool1"] as CogCalibCheckerboardTool;
            calbCheckerBoard.InputImage = AcqFifoTool.OutputImage;
            calbCheckerBoard.Run();
            //-----------

            //16.02.14
            CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            CalibNPointTool.InputImage = calbCheckerBoard.OutputImage;
            CalibNPointTool.Run();
            //----------
            //cogPMAlignTool = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolTechHoles"] as CogPMAlignTool;
            cogRecordDisplay1.Image = cogPMAlignTool.InputImage;
            cogPMAlignTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
            cogCreateSegmentAvgSegsTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey; //21.07.14
            //----
            cogPMAlignToolCenter.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
            //----

            PatMaxTool.InputImage = AcqFifoTool.OutputImage as CogImage8Grey;
            PatMaxTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
            ImageFileTool.InputImage = AcqFifoTool.OutputImage;
            // Run the garbage collector to free unused images
            static_AcqFifoTool_Ran_numacqs += 1;
            if (static_AcqFifoTool_Ran_numacqs > 4)
            {
                GC.Collect();
                static_AcqFifoTool_Ran_numacqs = 0;
            }

        }


        //Handles ImageFileTool.Ran

        private void ImageFileTool_Ran(object sender, System.EventArgs e)
        {

            cogRecordDisplay1.InteractiveGraphics.Clear();
            cogRecordDisplay1.StaticGraphics.Clear();
            cogRecordDisplay1.Image = ImageFileTool.OutputImage;
            PatMaxTool.InputImage = ImageFileTool.OutputImage as CogImage8Grey;
        }

        //If PMAlign results have changed then update the Score & Region graphic.
        //Handles PatMaxTool.Changed
        private void PatMaxTool_Changed(object sender, Cognex.VisionPro.CogChangedEventArgs e)
        {

            //If FunctionalArea And cogFA_Tool_Results Then
            if ((Cognex.VisionPro.Implementation.CogToolBase.SfCreateLastRunRecord
           | Cognex.VisionPro.Implementation.CogToolBase.SfRunStatus) > 0)
            {
                cogRecordDisplay1.StaticGraphics.Clear();
                //Note, Results will be nothing if Run failed.
                if (PatMaxTool.Results == null)
                {
                    txtPatMaxScoreValue.Text = "N/A";
                }
                else if (PatMaxTool.Results.Count > 0)
                {
                    //Passing result does not imply Pattern is found, must check count.
                    txtPatMaxScoreValue.Text = PatMaxTool.Results[0].Score.ToString("g3");
                    txtPatMaxScoreValue.Refresh();
                    CogCompositeShape resultGraphics = default(CogCompositeShape);
                    resultGraphics = PatMaxTool.Results[0].CreateResultGraphics(CogPMAlignResultGraphicConstants.MatchRegion);
                    cogRecordDisplay1.InteractiveGraphics.Add(resultGraphics, "test", false);
                }
                else
                {

                    txtPatMaxScoreValue.Text = "N/A";
                }
            }










        }

        // delegates
        private delegate void LoadOrderDataDelegate();
        private LoadOrderDataDelegate loadOrderDataDelegate;
        private void LoadOrderDataDelegateFunc()
        {
            try
            {

                brightnessUpDown.Value = Convert.ToDecimal(AppGen.Inst.OrderParams.Cam2_Brightness);
                numericUpDown1.Value = Convert.ToDecimal(AppGen.Inst.OrderParams.Cam2_Score);
                contrastUpDown.Value = Convert.ToDecimal(AppGen.Inst.OrderParams.Cam2_Contrast);
                LoadPatternFromFile();    //  13.07.15  (Ziv)

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

        private void brightnessUpDown_ValueChanged(object sender, EventArgs e)
        {
            myFrameGrabbers = new CogFrameGrabbers();
            myFrameGrabber = myFrameGrabbers[0];

            myAcqFifo = AcqFifoTool.Operator;

            myAcqFifo.OwnedBrightnessParams.Brightness = cogAcqTool.Operator.OwnedBrightnessParams.Brightness;
            brightnessParams = myAcqFifo.OwnedBrightnessParams;
            if (brightnessParams != null)
            {
                brightnessParams.Brightness = Convert.ToDouble(brightnessUpDown.Value);
                myAcqFifo.OwnedBrightnessParams.Brightness = brightnessParams.Brightness;
                try
                {
                    cogAcqTool.Operator.OwnedBrightnessParams.Brightness = Convert.ToDouble(brightnessUpDown.Value);  //brightnessParams.Brightness;
                }
                catch { }
            }

        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {           
            PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
            cogPMAlignTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
            cogPMAlignToolCenter.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
        }
        private void contrastUpDown_ValueChanged(object sender, EventArgs e)
        {
            myAcqFifo = AcqFifoTool.Operator;
            contrastParams.Contrast = Convert.ToDouble(contrastUpDown.Value);
            cogAcqTool.Operator.OwnedContrastParams.Contrast = contrastParams.Contrast;
        }
        //17.03.14


        List<TrayIndexData> UnfoundInsertList = new List<TrayIndexData>();
        public double? rotation = null;  //22/05/14  public for using in MainCycle
        public double midlePointX = 0, midlePointY = 0;
        public int? midleID = null;

        public void Subject_Ran(object sender, EventArgs e)
        {
            
            cbxHolesResult.Enabled = true;

            //17.6
            AcqFifoTool.Run(); //17.6Roey
            //25.03.14
            cogAcqFifoTool = cogToolBlockEditV21.Subject.Tools["CogAcqFifoTool1"] as CogAcqFifoTool;
            //17.02.14p

            //            calbCheckerBoard.InputImage = cogToolBlockEditV21.Subject.Inputs["Image"].Value as CogImage8Grey;
            //
            calbCheckerBoard = cogToolBlockEditV21.Subject.Tools["CogCalibCheckerboardTool1"] as CogCalibCheckerboardTool;
            //calbCheckerBoard.InputImage = cogToolBlockEditV21.Subject.Inputs["Image"].Value as CogImage8Grey;
            //calbCheckerBoard.Run();
            ////-----------

            ////16.02.14
            CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            //CalibNPointTool.InputImage = calbCheckerBoard.OutputImage;
            //CalibNPointTool.Run();
            ////----------
            cogPMAlignTool = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolTechHoles"] as CogPMAlignTool;
            cogPMAlignToolCenter = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolCenter"] as CogPMAlignTool;

            //cogPMAlignTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
            //cogRecordDisplay1.Image = cogPMAlignTool.InputImage;
            cogPMAlignTool.Run();  //Remove it ????? 18.7 Roey
            cogPMAlignToolCenter.Run();  //Remove it ????? 18.7 Roey

            cogCreateSegmentAvgSegsTool = cogToolBlockEditV21.Subject.Tools["CogCreateSegmentAvgSegsTool1"] as CogCreateSegmentAvgSegsTool;
            cogCreateSegmentAvgSegsTool.Run();
            //cogCreateSegmentAvgSegsTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;



            dataGridView1.Rows.Clear();

            try
            {
               
                const int delta = 48;
                rotation = 0;
                double distance = 0;
                double? angle1stHole = null, angle2ndHole = null, angle3rdHole = null, angle4thHole = null,
                angle5thHole = null, angle6thHole = null, angle7thHole = null, angle8thHole = null;
                midleID = null;
                midlePointX = 0;
                midlePointY = 0;
                bool IsCarrierSideOK;
                if (cogPMAlignToolCenter.Results.Count != 1)
                {
                    midlePointX = 0;
                    midlePointY = 0;
                    rotation = null;
                    if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running)
                    {       
                        MessageBox.Show("Problem locating center hole");                                              
                    }
                    else
                    {
                        AppGen.Inst.Cam2.bError = true;
                        AppGen.Inst.Cam2.iErrNo = 502;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblCamerasMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam2.iErrNo]);
                    }
                    return;
                }
                else
                {
                    midlePointX = cogPMAlignToolCenter.Results[0].GetPose().TranslationX;
                    midlePointY = cogPMAlignToolCenter.Results[0].GetPose().TranslationY;
                    midleID = cogPMAlignToolCenter.Results[0].ID;
                }

                if (midleID == null)
                {
                    if (AppGen.Inst.MainCycle.MainProccesState == ProcessStatus.Running)
                    {
                        AppGen.Inst.Cam2.bError = true;
                        AppGen.Inst.Cam2.iErrNo = 501;
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.lblCamerasMsg, AppGen.Inst.ErrorDescription.ErrorDescriptionEng[AppGen.Inst.Cam2.iErrNo]);
                    }
                }
                int counterMatchHoles = 0;

                for (int i = 0; i < cogPMAlignTool.Results.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = cogPMAlignTool.Results[i].ID.ToString();
                    dataGridView1[1, i].Value = cogPMAlignTool.Results[i].Score.ToString();
                    dataGridView1[2, i].Value = cogPMAlignTool.Results[i].GetPose().Rotation * (180 / Math.PI);
                    dataGridView1[3, i].Value = cogPMAlignTool.Results[i].GetPose().TranslationX;
                    dataGridView1[4, i].Value = cogPMAlignTool.Results[i].GetPose().TranslationY;

                    if (CheckIfHoleLegal(cogPMAlignTool.Results[i].GetPose().TranslationX,
                        cogPMAlignTool.Results[i].GetPose().TranslationY,
                        midlePointX, midlePointY))
                    {
                        for (int jj = i + 1; jj < cogPMAlignTool.Results.Count; jj++)
                        {
                            if (CheckIfHoleLegal(cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                cogPMAlignTool.Results[jj].GetPose().TranslationY,
                                midlePointX, midlePointY))
                            {

                                distance = 10;
                                if (AppGen.Inst.Calculate.ReturnDistance(ref distance,
                                    cogPMAlignTool.Results[i].GetPose().TranslationX,
                                    cogPMAlignTool.Results[i].GetPose().TranslationY,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationY))
                                {
                                    angle1stHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[i].GetPose().TranslationX,
                                        cogPMAlignTool.Results[i].GetPose().TranslationY, midlePointX, midlePointY);
                                    angle2ndHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                        cogPMAlignTool.Results[jj].GetPose().TranslationY, midlePointX, midlePointY);
                                    counterMatchHoles++;
                                    break;
                                }

                                distance = 20;
                                if (AppGen.Inst.Calculate.ReturnDistance(ref distance,
                                    cogPMAlignTool.Results[i].GetPose().TranslationX,
                                    cogPMAlignTool.Results[i].GetPose().TranslationY,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationY))
                                {
                                    angle3rdHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[i].GetPose().TranslationX,
                                        cogPMAlignTool.Results[i].GetPose().TranslationY, midlePointX, midlePointY);
                                    angle4thHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                        cogPMAlignTool.Results[jj].GetPose().TranslationY, midlePointX, midlePointY);
                                    counterMatchHoles++;
                                    break;
                                }
                                distance = 30;
                                if (AppGen.Inst.Calculate.ReturnDistance(ref distance,
                                    cogPMAlignTool.Results[i].GetPose().TranslationX,
                                    cogPMAlignTool.Results[i].GetPose().TranslationY,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationY))
                                {
                                    angle5thHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[i].GetPose().TranslationX,
                                        cogPMAlignTool.Results[i].GetPose().TranslationY, midlePointX, midlePointY);
                                    angle6thHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                        cogPMAlignTool.Results[jj].GetPose().TranslationY, midlePointX, midlePointY);
                                    counterMatchHoles++;
                                    break;
                                }
                                distance = 40;
                                if (AppGen.Inst.Calculate.ReturnDistance(ref distance,
                                    cogPMAlignTool.Results[i].GetPose().TranslationX,
                                    cogPMAlignTool.Results[i].GetPose().TranslationY,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                    cogPMAlignTool.Results[jj].GetPose().TranslationY))
                                {
                                    angle7thHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[i].GetPose().TranslationX,
                                        cogPMAlignTool.Results[i].GetPose().TranslationY, midlePointX, midlePointY);
                                    angle8thHole = AppGen.Inst.Calculate.GetAngle(cogPMAlignTool.Results[jj].GetPose().TranslationX,
                                        cogPMAlignTool.Results[jj].GetPose().TranslationY, midlePointX, midlePointY);
                                    counterMatchHoles++;
                                    break;
                                }
                            }
                        }
                    }
                }
                if ((counterMatchHoles < 2) || (counterMatchHoles > 4))
                {
                    if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running)
                    {
                        MessageBox.Show("Problem to identify matched holes");
                    }                
                    counterMatchHoles = 0;
                    rotation = null;
                    return;
                }
                IsCarrierSideOK = AppGen.Inst.Calculate.CheckCarrierSide(angle1stHole, angle2ndHole, angle3rdHole, angle4thHole,
                angle5thHole, angle6thHole,angle7thHole,angle8thHole, ref rotation);
                if (cbxCarrier.Checked == true)
                {
                    if (IsCarrierSideOK != true)
                    {
                        if (AppGen.Inst.MainCycle.MainProccesState != ProcessStatus.Running)
                        {
                            MessageBox.Show("Carrier side is wrong");
                        }         
                        rotation = null;
                        return;
                    }



                }
                cbxRotationResults.Enabled = true;
                rotation = (rotation >= 0) ? rotation : rotation + 360;
                double tempRotation = (double)rotation;
                cogCreateSegmentAvgSegsTool.SegmentA.SetStartLengthRotation(midlePointX, midlePointY, 130, (tempRotation) / 57.2957795);
                cogCreateSegmentAvgSegsTool.SegmentB.SetStartLengthRotation(midlePointX, midlePointY, 130, 0);
                cogCreateSegmentAvgSegsTool.Run();

                if (cbxRotationResults.Checked)
                    cogRecordDisplay1.Record = cogCreateSegmentAvgSegsTool.CreateLastRunRecord();
                else
                    cogRecordDisplay1.Record = cogPMAlignTool.CreateLastRunRecord(); //ADD CENTER ??? 19.8
                cogRecordDisplay1.Fit(true);
                //rotation = (rotation >= 0) ? rotation : rotation + 360;
                tbxRotationAngle.Text = String.Format("{0:0.00}", rotation);
                txtPatMaxScoreValue.Text = String.Format("{0:0.00}", cogPMAlignTool.Results.Count.ToString());
                //---- 15.06.09 ----
                if (rotation != null)
                {
                    AppGen.Inst.newRotation = (double)rotation;
                }
                //---- -------- ----
                //asaf:
                AppGen.Inst.LoadCarrier.SetRotate(AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.rotation);
                AppGen.Inst.LoadCarrier.SetOffset(AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointX, AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointY);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PatMax Setup Error");
            }

            try
            {
                //if (cogPMAlignTool.Results.Count == 0)
                //{
                //    UnfoundInsertList.Add(insertList[CurrIndex]);  //CurrIndex= 0 always in this form
                //}
            }
            catch (Exception)
            {
                MessageBox.Show("PatMax Setup Error");
            }

        }

        void cogToolBlockEditV21_SubjectChanged(object sender, EventArgs e)
        {
            // The application is meant to be used with the TB.vpp so whenever the user changes the TB
            // We disable the run once button
            //btnRun.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)//Teach
        {
            if (!SettingUp)
            {
                ////17.6s
                //cogPMAlignTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
                //cogRecordDisplay1.Image = cogPMAlignTool.InputImage;
                ////17.6e
                cogPMAlignTool.Pattern.TrainImage = CalibNPointTool.OutputImage as CogImage8Grey;
                cogPMAlignToolCenter.Pattern.TrainImage = CalibNPointTool.OutputImage as CogImage8Grey;
                //While setting up PMAlign, disable other GUI controls.
                SettingUp = true;
                DisableAll(settingUpPatMax);
                //Add TrainRegion to display's interactive graphics
                //Add SearchRegion to display's static graphics for display only.
                cogRecordDisplay1.InteractiveGraphics.Clear();
                cogRecordDisplay1.StaticGraphics.Clear();




                //17.6Roey---------------------------------------------------
                //cogRecordDisplay1.Record = cogPMAlignTool.InputImage;
                //Roey----------------------------------------------------------
                cogRecordDisplay1.InteractiveGraphics.Add(cogPMAlignTool.Pattern.TrainRegion as ICogGraphicInteractive, "test", false);
                cogRecordDisplay1.InteractiveGraphics.Add(cogPMAlignToolCenter.Pattern.TrainRegion as ICogGraphicInteractive, "testCenter", false);














                //Now
                axes = new CogCoordinateAxes();
                axes.Transform = cogPMAlignTool.Pattern.Origin;
                axes.GraphicDOFEnable = CogCoordinateAxesDOFConstants.All &
                    ~CogCoordinateAxesDOFConstants.Skew;
                axes.Interactive = true;
                // Add a standard VisionPro "manipulable" mouse cursor.
                axes.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                axes.XAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                axes.YAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                cogRecordDisplay1.InteractiveGraphics.Add((ICogGraphicInteractive)axes, "test", false);

                //-----




                axesCenter = new CogCoordinateAxes();
                axesCenter.Transform = cogPMAlignToolCenter.Pattern.Origin;
                axesCenter.GraphicDOFEnable = CogCoordinateAxesDOFConstants.All &
                    ~CogCoordinateAxesDOFConstants.Skew;
                axesCenter.Interactive = true;
                // Add a standard VisionPro "manipulable" mouse cursor.
                axesCenter.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                axesCenter.XAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                axesCenter.YAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                cogRecordDisplay1.InteractiveGraphics.Add((ICogGraphicInteractive)axesCenter, "testCenter", false);
                //-----



                //if ((cogPMAlignTool.SearchRegion != null))
                //{
                //    //cogPMAlignTool.SearchRegion.FitToImage(cogRecordDisplay1.Image, 0.99,999.6);
                //    cogRecordDisplay1.StaticGraphics.Add(cogPMAlignTool.SearchRegion as ICogGraphic, "test");
                //    cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
                //}

                //OK has been pressed, completing Setup.
            }
            else
            {

                SettingUp = false;
                cogRecordDisplay1.InteractiveGraphics.Clear();
                cogRecordDisplay1.StaticGraphics.Clear();
                //Make sure we catch errors from Train, since they are likely.  For example,
                //No InputImage, No Pattern Features, etc.
                try
                {
                    //CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
                    //CalibNPointTool.Run();
                    //cogPMAlignTool.InputImage = (CogImage8Grey)CalibNPointTool.OutputImage;
                    cogPMAlignTool.Pattern.Train();
                    cogPMAlignTool.Run();

                    //--
                    cogPMAlignToolCenter.Pattern.Train();
                    cogPMAlignToolCenter.Run();
                    //--

                    cogRecordDisplay1.Record = cogPMAlignTool.CreateLastRunRecord();   //Add Center 19.8

                }
                catch (CogException cogex)
                {


















                    MessageBox.Show("Following Specific Cognex Error Occured:" + cogex.Message);







                }


                ////------------------------------------------
                //SettingUp = false;
                //cogRecordDisplay1.InteractiveGraphics.Clear();
                //cogRecordDisplay1.StaticGraphics.Clear();
                ////Make sure we catch errors from Train, since they are likely.  For example,
                ////No InputImage, No Pattern Features, etc.
                //try
                //{
                //    //16.02.14






                //    //CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
                //    //CalibNPointTool.Run();
                //    //_______________
                //    cogPMAlignTool.Pattern.Train();
                //}
                //catch (CogException cogex)
                //{
                //    MessageBox.Show("Following Specific Cognex Error Occured:" + cogex.Message);




                //}
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "PatMax Setup Error");




                }
                EnableAll(settingUpPatMax);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //CogSerializer.SaveObjectToFile(toolBlock, @"C:\PROJECTS\Stahli.Net\Bin\Debug\CognexStahli\Camera2.vpp"); //need to save with insernt name as given by asaf);
            CogSerializer.SaveObjectToFile(toolBlock, System.IO.Directory.GetCurrentDirectory() + "\\CognexStahli\\Camera2.vpp");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //CurrIndex++;
            switch (countFrames)
            {
                case 0:
                    //xMov += DeltaXFrameMov;
                    xMov = DeltaXFrameMov;
                    yMov = 0;
                    countFrames = 1;
                    break;
                case 1:
                    xMov = 0;
                    yMov = DeltaYFrameMov;
                    countFrames = 2;
                    break;
                case 2:
                    xMov = DeltaXFrameMov;
                    yMov = DeltaYFrameMov;
                    countFrames = 3;
                    break;
                case 3:
                    xMov = 0;
                    yMov = 0;
                    countFrames = 0;
                    break;
                default:
                    break;








            }
            xMov += DeltaXFrameMov;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            xMov = 0;
            yMov = 0;
            countFrames = 0;
            CurrIndex++;
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            PatMaxTool.RunParams.ApproximateNumberToFind = Convert.ToInt32(numericUpDown2.Value);
            cogPMAlignTool.RunParams.ApproximateNumberToFind = Convert.ToInt32(numericUpDown2.Value);
            cogPMAlignToolCenter.RunParams.ApproximateNumberToFind = 1;
        }

        int indexRunAgain = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            //foreach (var item in UnfoundInsertList)
            //{
            if (indexRunAgain < UnfoundInsertList.Count)
            {







                if (indexRunAgain == 0)
                {
                    numericUpDown1.Value -= Convert.ToDecimal(0.05);
                    PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                    cogPMAlignTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                    cogPMAlignToolCenter.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);

                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < UnfoundInsertList.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1[0, i].Value = UnfoundInsertList[i].Index.ToString("f2");
                        dataGridView1[3, i].Value = UnfoundInsertList[i].X_VisRes.ToString("f2");
                        dataGridView1[4, i].Value = UnfoundInsertList[i].Y_VisRes.ToString("f2");
                    }
                }

                ROIParams.SetROIXYWidthHeight(StartXIndx + Convert.ToInt32(UnfoundInsertList[indexRunAgain].X_VisRes * 6.5),//2.478),
                    StartYIndx - 55 + Convert.ToInt32(UnfoundInsertList[indexRunAgain].Y_VisRes * 6.5), // 2.375),
                    Convert.ToInt32(xSearchReg * 4 * 2), Convert.ToInt32(ySearchReg * 4 * 2));


                AcqFifoTool.Run();

                cogRecordDisplay1.Fit(true);
                cogRecordDisplay1.Fit(true);
                cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
                indexRunAgain++;
            }
            else
            {
                MessageBox.Show("No more unfound inserts");
                indexRunAgain = 0;
                numericUpDown1.Value += Convert.ToDecimal(0.05);
                PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                cogPMAlignTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                cogPMAlignToolCenter.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                dataGridView1.Rows.Clear();
            }











            //}
        }
        private void cbxHolesResult_CheckedChanged(object sender, EventArgs e)
        {
            grpbxRotationResults.Visible = false;
            //tbxRotationAngle.Text = "N/A";
            dataGridView1.Visible = true;
            cogRecordDisplay1.Record = cogPMAlignTool.CreateLastRunRecord();  //Add center ???? 19.8
            cogRecordDisplay1.Fit(true);
        }
        private void cbxRotationResults_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            grpbxRotationResults.Visible = true;
            //tbxRotationAngle.Text = null;
            cogRecordDisplay1.Record = cogCreateSegmentAvgSegsTool.CreateLastRunRecord();
            cogRecordDisplay1.Fit(true);







        }


        private delegate void VisionActionDelegate(VisionActionType Action);
        private VisionActionDelegate visionActionDelegate;
        private void VisionActionDelegateFunc(VisionActionType Action)
        {
            try
            {
                switch (Action)
                {
                    case VisionActionType.ImageAcquisition:
                        cmdImageAcquisitionNewImageCommand_Click(null, null);
                        break;
                    case VisionActionType.RunTool:
                        Subject_Ran(null, null);
                        break;
                    case VisionActionType.AutoPreScan:
                        AutoPreScan(null, null);
                        break;
                    case VisionActionType.AutoMissedScan:
                        AutoMissedScan(null, null);
                        break;






                }
            }
            catch { }
        }
        public void VisionAction(VisionActionType Action)
        {
            try
            {
                this.Invoke(visionActionDelegate, Action);

            }
            catch { }
        }


        private void cmdSubmitCalibPoints_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Did you Retrieve Updated Calibrated Points from robot?", "Alert", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                for (int i = 1; i <= 4; i++)
                {
                    CalibNPointTool.Calibration.SetRawCalibratedPoint(i - 1, AppGen.Inst.VisionParam.LoadCarrierCalibPt[i - 1].X,
                        AppGen.Inst.VisionParam.LoadCarrierCalibPt[i - 1].Y);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //( cogToolBlockEditV21.Subject.Tools["CogAcqFifoTool1"] as CogAcqFifoTool).;

            // cogPMAlignTool.
        }
        private void cmdCognexControl_Click(object sender, EventArgs e)
        {
            groupPassword.Visible = true;
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            groupPassword.Visible = false;
        }
        private void cmdPassword_Click(object sender, EventArgs e)
        {
            if (txtInputPassword.Text == "1111")
            {
                //Maneger form size: 
                AppGen.Inst.MDImain.frmVisionMain.Width = 1163;
                AppGen.Inst.MDImain.frmVisionMain.Height = 790;
                this.Width = 1163;
                this.Height = 790;
                groupPassword.Visible = false;
                txtInputPassword.Text = "****";





            }
        }
        private void cmdCloseCognexControl_Click(object sender, EventArgs e)
        {
            //User form size:              
            AppGen.Inst.MDImain.frmVisionMain.Width = 774;
            AppGen.Inst.MDImain.frmVisionMain.Height = 790;
            this.Width = 774;
            this.Height = 790;
        }
        private void cmdSaveParamToOrder_Click(object sender, EventArgs e)
        {
            AppGen.Inst.OrderParams.Cam2_Brightness = Convert.ToDouble(brightnessUpDown.Value);
            AppGen.Inst.OrderParams.Cam2_Score = Convert.ToDouble(numericUpDown1.Value);
            AppGen.Inst.OrderParams.Cam2_Contrast = Convert.ToDouble(contrastUpDown.Value);
            AppGen.Inst.OrderParams.Serialize(AppGen.Inst.OrderParams.InsertCode);

            AppGen.Inst.MDImain.frmOrderEditor.lblCam2Brightness.Text = AppGen.Inst.OrderParams.Cam2_Brightness.ToString();
            AppGen.Inst.MDImain.frmOrderEditor.lblCam2Contrast.Text = AppGen.Inst.OrderParams.Cam2_Score.ToString();
            AppGen.Inst.MDImain.frmOrderEditor.lblCam2Score.Text = AppGen.Inst.OrderParams.Cam2_Contrast.ToString();
        }

        private void chkFullFrame_CheckedChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.flagFullTray = chkFullFrame.Checked;
        }
        private void cmdResetIndex_Click(object sender, EventArgs e)
        {
            AppGen.Inst.LoadCarrier.CurrIndex = 0;
            txtCurrentIndex.Text = (AppGen.Inst.LoadCarrier.CurrIndex).ToString();
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
            cmdFindInserts_Click(null, null);
        }
        private void cmdNextIndex_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.flagFullTray = false;
            AppGen.Inst.LoadCarrier.CurrIndex += 1;
            if (AppGen.Inst.LoadCarrier.CurrIndex >= AppGen.Inst.LoadCarrier.IndexList.Count)
            {
                AppGen.Inst.LoadCarrier.CurrIndex = 0;


            }
            txtCurrentIndex.Text = Convert.ToString(AppGen.Inst.LoadCarrier.CurrIndex);
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
            cmdFindInserts_Click(null, null);
        }
        private void txtCurrentIndex_TextChanged(object sender, EventArgs e)
        {
            AppGen.Inst.LoadCarrier.CurrIndex = Convert.ToUInt16(txtCurrentIndex.Text);
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
        }

        private void cmdTeachInsert_Click(object sender, EventArgs e)
        {
            if (!SettingUp)
            {
                try
                {
                cogPMAlignToolFindOneInsert.Pattern.TrainImage = CalibNPointTool.OutputImage as CogImage8Grey;
                SettingUp = true;
                // DisableAll(settingUpPatMax);
                cmdTeachInsert.Text = "OK";

                cogRecordDisplay1.InteractiveGraphics.Clear();
                cogRecordDisplay1.StaticGraphics.Clear();

                cogRecordDisplay1.InteractiveGraphics.Add(cogPMAlignToolFindOneInsert.Pattern.TrainRegion as ICogGraphicInteractive, "test", false);

                //Now
                axes = new CogCoordinateAxes();
                axes.Transform = cogPMAlignToolFindOneInsert.Pattern.Origin;
                axes.GraphicDOFEnable = CogCoordinateAxesDOFConstants.All &
                    ~CogCoordinateAxesDOFConstants.Skew;
                axes.Interactive = true;
                // Add a standard VisionPro "manipulable" mouse cursor.
                axes.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                axes.XAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                axes.YAxisLabel.MouseCursor = CogStandardCursorConstants.ManipulableGraphic;
                cogRecordDisplay1.InteractiveGraphics.Add((ICogGraphicInteractive)axes, "test", false);


                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


            }
            else
            {
                cmdTeachInsert.Text = "Teach";
                SettingUp = false;
                cogRecordDisplay1.InteractiveGraphics.Clear();
                cogRecordDisplay1.StaticGraphics.Clear();
                try
                {
                    cogPMAlignToolFindOneInsert.Pattern.Train();
                    cogPMAlignToolFindOneInsert.Run();
                    cogRecordDisplay1.Record = cogPMAlignToolFindOneInsert.CreateLastRunRecord();

                    //SavePatternToFile();
                    //save the teach pattern according to order name (ziv)
                    AppGen.Inst.MDImain.frmVisionMain.savePattern("Camera2", AppGen.Inst.OrderParams.InsertCode, cogPMAlignTool.Pattern); 
                  

                }
                catch (CogException cogex)
                {
                    MessageBox.Show("Following Specific Cognex Error Occured:" + cogex.Message);






                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "PatMax Setup Error");

                }
                EnableAll(settingUpPatMax);

            }
        }
        public void cmdFindInserts_Click(object sender, EventArgs e)
        {
            AcqFifoTool.Run();
            cogAcqFifoTool = cogToolBlockEditV21.Subject.Tools["CogAcqFifoTool1"] as CogAcqFifoTool;
            calbCheckerBoard = cogToolBlockEditV21.Subject.Tools["CogCalibCheckerboardTool1"] as CogCalibCheckerboardTool;
            CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            cogTransform2DLinearN2N = CalibNPointTool.Calibration.GetComputedUncalibratedFromCalibratedTransform() as CogTransform2DLinear;

            if (AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.flagFullTray)   //find all inserts
            {
                PatMaxSearchRegion.CenterX = AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointX; // 378;  //center carrier
                PatMaxSearchRegion.CenterY = AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointY; // -164;
                PatMaxSearchRegion.Radius = 110;  //carrier radius            

            }
            else                                                             //find One insert      
            {
                PatMaxSearchRegion.CenterX = AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.X; //378;
                PatMaxSearchRegion.CenterY = AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.Y; //-164;
                PatMaxSearchRegion.Radius = RadiusSearchReg; //RadiusSearchReg;  //tbd - recieve from pwh file

            }

          //  cogPMAlignToolFindOneInsert = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolFindOneInsert"] as CogPMAlignTool;
            cogPMAlignToolFindOneInsert.SearchRegion = PatMaxSearchRegion;
            cogPMAlignToolFindOneInsert.Run();
            if (cogPMAlignToolFindOneInsert.Results == null)
            {
                MessageBox.Show("Please Find Carrier Orientation first!");
                return;










            }
            cogRecordDisplay1.Record = cogPMAlignToolFindOneInsert.CreateLastRunRecord();
            cogRecordDisplay1.Fit(true);
            txtPatMaxScoreValue.Text = String.Format("{0:0.00}", cogPMAlignToolFindOneInsert.Results.Count.ToString());
        }
        private void AutoPreScan(object sender, EventArgs e)  //search insert at full fram, if find more then zero, search for the first empty index
        {
            AcqFifoTool.Run();
            cogAcqFifoTool = cogToolBlockEditV21.Subject.Tools["CogAcqFifoTool1"] as CogAcqFifoTool;
            calbCheckerBoard = cogToolBlockEditV21.Subject.Tools["CogCalibCheckerboardTool1"] as CogCalibCheckerboardTool;
            CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            cogTransform2DLinearN2N = CalibNPointTool.Calibration.GetComputedUncalibratedFromCalibratedTransform() as CogTransform2DLinear;

            PatMaxSearchRegion.CenterX = AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointX;




            PatMaxSearchRegion.CenterY = AppGen.Inst.MDImain.frmVisionMain.FrmLoadCarrier.midlePointY;
            PatMaxSearchRegion.Radius = 110;  //carrier radius 




            cogPMAlignToolFindOneInsert = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolFindOneInsert"] as CogPMAlignTool;
            cogPMAlignToolFindOneInsert.SearchRegion = PatMaxSearchRegion;
            cogPMAlignToolFindOneInsert.Run();
            if (cogPMAlignToolFindOneInsert.Results == null)
            {
                //tbd auto cycle messege handling
                MessageBox.Show("Please Find Carrier Orientation first!");
                return;
            }
            if (cogPMAlignToolFindOneInsert.Results.Count == 0)
            {
                return;  //no insert in carrier, do nothing
            }
            else  //inserts exist, find the first empty index, to start placing
            {
                AppGen.Inst.LoadCarrier.CurrIndex = 0;
                for (int ii = 0; ii < AppGen.Inst.LoadCarrier.IndexList.Count; ii++)
                {
                    PatMaxSearchRegion.CenterX = AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.X;    //378;
                    PatMaxSearchRegion.CenterY = AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.Y;    //-164;
                    PatMaxSearchRegion.Radius = RadiusSearchReg; // recieved from pwh file
                    cogPMAlignToolFindOneInsert = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolFindOneInsert"] as CogPMAlignTool;
                    cogPMAlignToolFindOneInsert.SearchRegion = PatMaxSearchRegion;
                    cogPMAlignToolFindOneInsert.Run();
                    if (cogPMAlignToolFindOneInsert.Results.Count >= 1)
                    {
                        AppGen.Inst.LoadCarrier.CurrIndex += 1;
                        txtCurrentIndex.Text = AppGen.Inst.LoadCarrier.CurrIndex.ToString();
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
                    }
                    else
                    {
                        txtCurrentIndex.Text = AppGen.Inst.LoadCarrier.CurrIndex.ToString();
                        AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaIndexLoadCarrier, AppGen.Inst.LoadCarrier.CurrIndex.ToString());
                        return;   //out of loop, CurrIndex is in the first empty pocket
                    }
                }
            }
        }

        private void AutoMissedScan(object sender, EventArgs e)  //search empty pocket after finish loading carrier 
        {
            AcqFifoTool.Run();
            cogAcqFifoTool = cogToolBlockEditV21.Subject.Tools["CogAcqFifoTool1"] as CogAcqFifoTool;
            calbCheckerBoard = cogToolBlockEditV21.Subject.Tools["CogCalibCheckerboardTool1"] as CogCalibCheckerboardTool;
            CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            cogTransform2DLinearN2N = CalibNPointTool.Calibration.GetComputedUncalibratedFromCalibratedTransform() as CogTransform2DLinear;

            AppGen.Inst.LoadCarrier.CurrIndex = 0;
            AppGen.Inst.MainCycle.MissPocketCarrCount = 0;
            for (int ii = 0; ii < AppGen.Inst.LoadCarrier.IndexList.Count; ii++)
            {
                PatMaxSearchRegion.CenterX = AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.X;
                PatMaxSearchRegion.CenterY = AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].Offset.Y;
                PatMaxSearchRegion.Radius = RadiusSearchReg; //recieved from pwh file
                cogPMAlignToolFindOneInsert = cogToolBlockEditV21.Subject.Tools["CogPMAlignToolFindOneInsert"] as CogPMAlignTool;
                cogPMAlignToolFindOneInsert.SearchRegion = PatMaxSearchRegion;
                cogPMAlignToolFindOneInsert.Run();
                if (cogPMAlignToolFindOneInsert.Results.Count == 0)
                {
                    AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].VisionEmptyPocket = true;
                    AppGen.Inst.MainCycle.MissPocketCarrCount += 1;
                    AppGen.Inst.MainCycle.MissPocketTotalCount = AppGen.Inst.MainCycle.MissPocketTotalCount + AppGen.Inst.MainCycle.MissPocketCarrCount;
                }
                else
                {
                    AppGen.Inst.LoadCarrier.IndexList[AppGen.Inst.LoadCarrier.CurrIndex].VisionEmptyPocket = false;
                }
                AppGen.Inst.LoadCarrier.CurrIndex += 1;
            }
            AppGen.Inst.LoadCarrier.CurrIndex = 0;
        }

        private void cmdPreScan_Click(object sender, EventArgs e)
        {
            AutoMissedScan(null, null);
        }
        private void cmdFindNext_Click(object sender, EventArgs e)
        {
            AutoPreScan(null, null);
            cmdFindInserts_Click(null, null);
        }

       // const double minLegalDistance = 108.5, maxLegalLength = 111.5;
        const double minLegalDistance = 107.5, maxLegalLength = 111.0;
        private bool CheckIfHoleLegal(double foundXPosition, double foundYPosition, double centerXPosition,
            double centerYPosition)
        {
            double distance = Math.Sqrt((foundXPosition-centerXPosition)*(foundXPosition-centerXPosition)+
                (foundYPosition-centerYPosition)*(foundYPosition-centerYPosition));
            if ((distance < maxLegalLength) && (distance > minLegalDistance))
                return true;    
            return false;
        }
        void SavePatternToFile()
        {
            try
            {

                string path = System.IO.Directory.GetCurrentDirectory() + "\\CognexStahli\\PATTERN2.vpp";
                if (string.IsNullOrEmpty(path)) return;

                //path += "PMAlign.vpp";
                CogSerializer.SaveObjectToFile(cogPMAlignToolFindOneInsert.Pattern, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadPatternFromFile()
        {
            // Ziv 14.07.15
            cogPMAlignTool.Pattern = AppGen.Inst.MDImain.frmVisionMain.loadPattern("Camera2", AppGen.Inst.OrderParams.InsertCode);   //13.07.15 (Ziv)
            if (cogPMAlignTool.Pattern == null)
            {
                cogPMAlignTool.Pattern = new CogPMAlignPattern();
            }

            ////try
            ////{
            ////    string path = System.IO.Directory.GetCurrentDirectory() + "\\CognexStahli\\PATTERN3.vpp";
            ////    if (string.IsNullOrEmpty(path)) return;

            ////    //path += "PMAlign.vpp";

            ////    if (System.IO.File.Exists(path))
            ////    {
            ////        //CogPMAlignTool.InputImage = null;
            ////        CogPMAlignPattern pattern = CogSerializer.LoadObjectFromFile(path) as Cognex.VisionPro.PMAlign.CogPMAlignPattern;
            ////        if (pattern != null) cogPMAlignTool.Pattern = pattern;
            ////    }
            ////}
            ////catch (Exception ex)
            ////{
            ////    MessageBox.Show(ex.Message);
            ////}
        }

    }
}
