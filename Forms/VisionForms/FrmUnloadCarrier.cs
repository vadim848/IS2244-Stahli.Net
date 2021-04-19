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

namespace Stahli2Robots
{
    public partial class FrmUnloadCarrier : Form
    {        
        #region "Module Level vars"
        CogRectangle PatMaxSearchRegion = new CogRectangle();
        CogTransform2DLinear cogTransform2DLinearCB, cogTransform2DLinearN2N;

        CogImageFileTool ImageFileTool;
        CogPMAlignTool PatMaxTool;
        CogAcqFifoTool AcqFifoTool;
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
        //CogPMAlignTool mPMAlignTool;

        ICogAcqFifo myAcqFifo;

        private CogFrameGrabbers myFrameGrabbers;
        private ICogFrameGrabber myFrameGrabber;

        ICogAcqBrightness brightnessParams;
        ICogAcqContrast contrastParams;

        //private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        CogToolBlock toolBlock = new CogToolBlock();
        public CogPMAlignTool cogPMAlignTool;

        CogCoordinateAxes axes;

        // This is the calibration tool.
        private CogCalibNPointToNPointTool CalibNPointTool;
        // This object holds calibration operator.
        //private CogCalibNPointToNPoint Calib;

        CogPolygon cogPolygon = new CogPolygon();

        //Acquire region params
        ICogAcqROI ROIParams;
        const int MaxXWidth = 3000;
        const int MaxYWidth = 3000;
        const int MinXWidth = 1000;
        const int MinYWidth = 1000;
        const int StartXIndx = 180;
        const int StartYIndx = 0;
        double CurrLineCoord = StartYIndx;
        const int DeltaXFrameMov = 1000;
        const int DeltaYFrameMov = 1000;
        int xMov, yMov = 0;
        public bool flagFullTray = true;
        int countFrames = 0;
        double PixFactor = 6.5;
        //Search region params
        double xSearchReg = 210; //250;
        double ySearchReg = 75;
        
        //CogRectangle PatMaxSearchRegion = new CogRectangle();

        private CogCalibCheckerboardTool calbCheckerBoard;

        //ReadFromMainProj fromMainProject = new ReadFromMainProj();
//        Calculate calculations = new Calculate();

        CogAcqFifoTool cogAcqTool = new CogAcqFifoTool();
        #endregion  


        public FrmUnloadCarrier()
        {
            visionActionDelegate = new VisionActionDelegate(VisionActionDelegateFunc);
            InitializeComponent();
            cogToolBlockEditV21.LocalDisplayVisible = false;
            mIFTool = new CogImageFileTool();
           // mIFTool.Operator.Open(Environment.GetEnvironmentVariable("VPRO_ROOT") + @"\images\coins.idb", CogImageFileModeConstants.Read);
            mAcqTool = new CogAcqFifoTool();
            toolBlock = cogToolBlockEditV21.Subject;
            try
            {
                //toolBlock = CogSerializer.LoadObjectFromFile(@"C:\PROJECTS\Stahli.Net\Bin\Debug\CognexStahli\Camera3.vpp") as CogToolBlock;
                toolBlock = CogSerializer.LoadObjectFromFile(System.IO.Directory.GetCurrentDirectory() + "\\CognexStahli\\Camera3.vpp") as CogToolBlock;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tool block is error");
            }
            cogToolBlockEditV21.Subject = toolBlock;
            cogToolBlockEditV21.Subject.Ran += new EventHandler(Subject_Ran);
            cogToolBlockEditV21.SubjectChanged += new EventHandler(cogToolBlockEditV21_SubjectChanged);
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

            foreach (ICogTool tool in toolBlock.Tools)
            {
                cogPMAlignTool = tool as CogPMAlignTool;
                if (cogPMAlignTool != null) break;
            }

            loadOrderDataDelegate = new LoadOrderDataDelegate(LoadOrderDataDelegateFunc);
            // LoadPatternFromFile();   //13.07.15 (Ziv)
        }

        public void InitVision()
        {
            SettingUp = false;
            ImageFileTool = CogImageFileEdit1.Subject;
            ImageFileTool.Ran += ImageFileTool_Ran;
            //Set reference to CogAcqFifoTool created by Edit Control
            //The Acq Fifo Edit Control creates its subject when its AutoCreateTool property is True
            AcqFifoTool = CogAcqFifoEdit1.Subject;
            AcqFifoTool.Ran += AcqFifoTool_Ran;

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
            //PatMaxTool.SearchRegion = PatMaxSearchRegion;
            //PatMaxSearchRegion.SetCenterWidthHeight(320, 240, 640, 480);
            //PatMaxSearchRegion.GraphicDOFEnable = CogRectangleDOFConstants.Position | CogRectangleDOFConstants.Size;
            //PatMaxSearchRegion.Interactive = true;
            numericUpDown1.Value = Convert.ToDecimal(cogPMAlignTool.RunParams.AcceptThreshold);
            PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
            numericUpDown2.Value = Convert.ToDecimal(cogPMAlignTool.RunParams.ApproximateNumberToFind);
            PatMaxTool.RunParams.ApproximateNumberToFind = Convert.ToInt32(numericUpDown2.Value);
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
            if (brightnessParams != null)
                brightnessUpDown.Value = Convert.ToDecimal(brightnessParams.Brightness);
            if (contrastParams != null)
                contrastUpDown.Value = Convert.ToDecimal(contrastParams.Contrast);
            try
            {
                cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Input image wasn't set");
            }
        }

        private void UnloadCarierForm_Load(object sender, EventArgs e)
        {
        }

        private void cmdImageAcquisitionLiveOrOpenCommand_Click(System.Object sender, System.EventArgs e)
		{
			CogDisplay1.StaticGraphics.Clear();
			CogDisplay1.InteractiveGraphics.Clear();

            cogRecordDisplay1.StaticGraphics.Clear();
            cogRecordDisplay1.InteractiveGraphics.Clear();
            ROIParams = myAcqFifo.OwnedROIParams;
            //if (AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray)
            //{
            ROIParams.SetROIXYWidthHeight(StartXIndx, StartYIndx, MaxXWidth, MaxYWidth);
                //PatMaxSearchRegion.SetXYWidthHeight(0, 0, 349.08, 296.54);
                //cogPMAlignTool.SearchRegion = PatMaxSearchRegion;
            //}
            //else
            //{
            //   // ROIParams.SetROIXYWidthHeight(Convert.ToInt32(StartXIndx * PixFactor), Convert.ToInt32(CurrLineCoord * PixFactor), Convert.ToInt32((xSearchReg + 10) * PixFactor), Convert.ToInt32((ySearchReg + 10) * PixFactor));
            //    try
            //    {
            //        CurrLineCoord = StartYIndx + (((AppGen.Inst.MainCycle.UnloadCarrierSliceNo - 1) * ySearchReg));      //added 11.06.14 by asaf
            //       //111111 ROIParams.SetROIXYWidthHeight(Convert.ToInt32(StartXIndx * 2.5), Convert.ToInt32(CurrLineCoord), xSearchReg - 1100, ySearchReg + 150);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "PatMax Setup Error");
            //    }
               
            //}
			//"Live Video"  & "Stop Live" button when Frame Grabber option is selected.
			//Using our EnableAll & DisableAll subroutine to force the user stop live
			//video before doing anything else.
			if (optImageAcquisitionOptionFrameGrabber.Checked == true) {
                if (cogRecordDisplay1.LiveDisplayRunning)
                {
                    cogRecordDisplay1.StopLiveDisplay();
					EnableAll(settingLiveVideo);
					AcqFifoTool.Run();

                    cogRecordDisplay1.Fit(true);
                    cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;

				} else if ((AcqFifoTool.Operator != null)) 
                {
                    AcqFifoTool.Run();

                    cogRecordDisplay1.Fit(true);
                    cogRecordDisplay1.StartLiveDisplay(AcqFifoTool.Operator, false);
                    cogRecordDisplay1.Fit(true);
                    cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
					DisableAll(settingLiveVideo);
                    cogRecordDisplay1.Fit(true);
				}

			} else {
				//"Open File" button when image file option is selected
				//DrawingEnabled is used to simply hide the image while the Fit is performed.
				//This prevents the image from being diplayed at the initial zoom factor
				//prior to fit being called.
				try {
					DialogResult result = ImageAcquisitionCommonDialog.ShowDialog();
					if (result != System.Windows.Forms.DialogResult.Cancel) {
						ImageFileTool.Operator.Open(ImageAcquisitionCommonDialog.FileName, CogImageFileModeConstants.Read);
                        cogRecordDisplay1.DrawingEnabled = false;
						ImageFileTool.Run();
                        cogRecordDisplay1.Fit(true);
                        cogRecordDisplay1.DrawingEnabled = true;
					}
				} catch (CogException cogex) {
					MessageBox.Show("Following Specific Cognex Error Occured:" + cogex.Message);
				} catch (Exception ex) {
					MessageBox.Show("Following Error Occured:" + ex.Message);
				}
                cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image; //!!!!! Neet to solve why pic doesn't get into tool
			}
            cogRecordDisplay1.Fit(true);
		}

		private void cmdImageAcquisitionNewImageCommand_Click(System.Object sender, System.EventArgs e) //aquire
		{
           
            //ROIParams = myAcqFifo.OwnedROIParams;
            //if (AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray)
            //{
            //    //PatMaxSearchRegion.SetXYWidthHeight(0, 0, 349.08, 296.54);
            //    //cogPMAlignTool.SearchRegion = PatMaxSearchRegion;
            //    ROIParams.SetROIXYWidthHeight(StartXIndx, StartYIndx, MaxXWidth, MaxYWidth);
            //}
            //else
            //{                
            //    try
            //    {
            //        CurrLineCoord = StartYIndx + (((AppGen.Inst.MainCycle.UnloadCarrierSliceNo - 1) * ySearchReg));      //added 11.06.14 by asaf
            //        ROIParams.SetROIXYWidthHeight(Convert.ToInt32(StartXIndx*2.5), Convert.ToInt32(CurrLineCoord), xSearchReg-1100, ySearchReg+150);                  
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "PatMax Setup Error");
            //    }
            //}
            ROIParams = myAcqFifo.OwnedROIParams;
            if (AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray)
            {
                PatMaxSearchRegion.SetXYWidthHeight(-239.175, -480.662, 237.161, 220.133);  //tbd: replace to parameters
                cogPMAlignTool.SearchRegion = PatMaxSearchRegion;
            }
            else
            {      //tbd: replace to parameters
                cogTransform2DLinearN2N = CalibNPointTool.Calibration.GetComputedUncalibratedFromCalibratedTransform() as CogTransform2DLinear;
                double OverLap = 0;
                switch (AppGen.Inst.MainCycle.UnloadCarrierSliceNo)
                {
                    case 1:
                        OverLap = 0;
                        break;
                    case 2:
                        OverLap = -30;
                        break;
                    case 3:
                        OverLap = -50;
                        break;
                    case 4:
                        OverLap = -80;
                        break;
                }

                CurrLineCoord = -480 + (((AppGen.Inst.MainCycle.UnloadCarrierSliceNo - 1) * ySearchReg + OverLap));
                PatMaxSearchRegion.SetXYWidthHeight(-220, CurrLineCoord, xSearchReg, ySearchReg);  //tbd: replace to parameters

                cogPMAlignTool.SearchRegion = PatMaxSearchRegion;

            }
            AcqFifoTool.Run();

            cogRecordDisplay1.Fit(true);
            cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
            if ((cogPMAlignTool.SearchRegion != null) && !AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray)
            {
                //cogPMAlignTool.SearchRegion.FitToImage(cogRecordDisplay1.Image, 0.990,0.999);
                cogRecordDisplay1.StaticGraphics.Add(cogPMAlignTool.SearchRegion as ICogGraphic, "test");
                cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
            }
        }

		private void optImageAcquisitionOptionFrameGrabber_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (optImageAcquisitionOptionFrameGrabber.Checked == true) {
				cmdImageAcquisitionLiveOrOpenCommand.Text = "Live Video";
                cmdImageAcquisitionNewImageCommand.Text = "Acquire";
			} else {
				cmdImageAcquisitionLiveOrOpenCommand.Text = "Open File";
				cmdImageAcquisitionNewImageCommand.Text = "Next Image";
			}
            cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
		}

		private void optImageAcquisitionOptionImageFile_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (optImageAcquisitionOptionImageFile.Checked == true) {
				cmdImageAcquisitionLiveOrOpenCommand.Text = "Open File";
				cmdImageAcquisitionNewImageCommand.Text = "Next Image";
			} else {
				cmdImageAcquisitionLiveOrOpenCommand.Text = "Live Video";
                cmdImageAcquisitionNewImageCommand.Text = "Acquire";
			}
            cogToolBlockEditV21.Subject.Inputs["Image"].Value = cogRecordDisplay1.Image;
		}

		private void cmdPatMaxSetupCommand_Click(System.Object sender, System.EventArgs e)
		{
                    cogRecordDisplay1.InteractiveGraphics.Add(cogPMAlignTool.Pattern.TrainRegion as ICogGraphicInteractive, "test", false);
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
		}
		#region "Module Level Routines"
		//Disable GUI controls when forcing the user to complete a task before moving on
		//to something new.  Example, Setting up PMAlign.
        //!_____________________________________________________________________________!//Start
		private void DisableAll(SettingUpConstants butThis)
		{
			//Disable all of the frames (Disables controls within frame)
		//	frmImageAcquisitionFrame.Enabled = false;
			frmPatMax.Enabled = false;
			//Disable all of the tabs except "VisionPro Demo" tab.
			VProAppTab.TabPages[1].Enabled = false;
			VProAppTab.TabPages[2].Enabled = false;
			VProAppTab.TabPages[3].Enabled = false;
			//Based on what the user is doing, Re-enable appropriate frame and disable
			//specific controls within the frame.
			if (butThis == settingUpPatMax) {
				frmPatMax.Enabled = true;
				//cmdPatMaxSetupCommand.Text = "OK";
                cmdTeach.Text = "OK";
				cmdPatMaxRunCommand.Enabled = false;
			} else if (butThis == settingLiveVideo) {
			//	frmImageAcquisitionFrame.Enabled = true;
				cmdImageAcquisitionLiveOrOpenCommand.Text = "Stop Live";
				cmdImageAcquisitionNewImageCommand.Enabled = false;
				optImageAcquisitionOptionFrameGrabber.Enabled = false;
				optImageAcquisitionOptionImageFile.Enabled = false;
			}
		}
		//Enable all of the GUI controls when done a task.  Example, done setting up PMAlign.
		private void EnableAll(SettingUpConstants butThis)
		{
			//frmImageAcquisitionFrame.Enabled = true;
			frmPatMax.Enabled = true;
			VProAppTab.TabPages[1].Enabled = true;
			VProAppTab.TabPages[2].Enabled = true;
			VProAppTab.TabPages[3].Enabled = true;
			if (butThis == settingUpPatMax) {
				//cmdPatMaxSetupCommand.Text = "Setup";
                cmdTeach.Text = "Teach";
				cmdPatMaxRunCommand.Enabled = true;
			} else if (butThis == settingLiveVideo) {
				cmdImageAcquisitionLiveOrOpenCommand.Text = "Live Video";
				cmdImageAcquisitionNewImageCommand.Enabled = true;
				optImageAcquisitionOptionFrameGrabber.Enabled = true;
				optImageAcquisitionOptionImageFile.Enabled = true;
			}
		}
		#endregion
		#region "Cognex Objects Events"
        //!_____________________________________________________________________________!//End

		//Pass AcqFifo OutputImage to the PatMax tool & the Display on "VisionPro" tab.
		//Also, pass OutputImage to the InputImage of ImageFile tool so that this
		//sample application can be used to Record a image file from frame grabber.
		// Handles AcqFifoTool.Ran

		int static_AcqFifoTool_Ran_numacqs;
        //!_____________________________________________________________________________!//Start
		private void AcqFifoTool_Ran(object sender, System.EventArgs e)
		{
            cogRecordDisplay1.InteractiveGraphics.Clear();
            cogRecordDisplay1.StaticGraphics.Clear();
            calbCheckerBoard = cogToolBlockEditV21.Subject.Tools["CogCalibCheckerboardTool1"] as CogCalibCheckerboardTool;
            calbCheckerBoard.InputImage = AcqFifoTool.OutputImage;
            calbCheckerBoard.Run();
            CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            CalibNPointTool.InputImage = calbCheckerBoard.OutputImage;
            CalibNPointTool.Run();
            cogRecordDisplay1.Image = cogPMAlignTool.InputImage;
            cogPMAlignTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
			PatMaxTool.InputImage = AcqFifoTool.OutputImage as CogImage8Grey;
            PatMaxTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
			ImageFileTool.InputImage = AcqFifoTool.OutputImage;
			static_AcqFifoTool_Ran_numacqs += 1;
			if (static_AcqFifoTool_Ran_numacqs > 4) {
				GC.Collect();
				static_AcqFifoTool_Ran_numacqs = 0;
			}

		}


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
           | Cognex.VisionPro.Implementation.CogToolBase.SfRunStatus) > 0) {
               cogRecordDisplay1.StaticGraphics.Clear();
				//Note, Results will be nothing if Run failed.
				if (PatMaxTool.Results == null) {
					txtPatMaxScoreValue.Text = "N/A";
				} else if (PatMaxTool.Results.Count > 0) {
					//Passing result does not imply Pattern is found, must check count.
					txtPatMaxScoreValue.Text = PatMaxTool.Results[0].Score.ToString("g3");
					txtPatMaxScoreValue.Refresh();
					CogCompositeShape resultGraphics = default(CogCompositeShape);
					resultGraphics = PatMaxTool.Results[0].CreateResultGraphics(CogPMAlignResultGraphicConstants.MatchRegion);
                    cogRecordDisplay1.InteractiveGraphics.Add(resultGraphics, "test", false);
				} else {
					txtPatMaxScoreValue.Text = "N/A";
				}
			}
		}
		#endregion
        private void frmPatInspSamp_FormClosing(object sender, FormClosingEventArgs e)
        {
            PatMaxTool.Changed -= PatMaxTool_Changed;
            if ((PatMaxTool != null))
                PatMaxTool.Dispose();
        }

        private void ImageAcquisitionCommonDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);     
            cogPMAlignTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);     
        }

        private void CogPMAlignEdit1_Load(object sender, EventArgs e)
        {

        }

        private void contrastUpDown_ValueChanged(object sender, EventArgs e)
        {
            myAcqFifo = AcqFifoTool.Operator;
            contrastParams.Contrast = Convert.ToDouble(contrastUpDown.Value); 
            cogAcqTool.Operator.OwnedContrastParams.Contrast = contrastParams.Contrast;
        }
        

        void cogToolBlockEditV21_SubjectChanged(object sender, EventArgs e)
        {
            // The application is meant to be used with the TB.vpp so whenever the user changes the TB
            // We disable the run once button
            //btnRun.Enabled = false;
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)  //Teach
        {         
            if (!SettingUp)
            {
                if ((!AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray))
                {
                    MessageBox.Show("Please acquire full frame");
                    return;
                }
                cogPMAlignTool.Pattern.TrainImage = CalibNPointTool.OutputImage as CogImage8Grey;
                //While setting up PMAlign, disable other GUI controls.
                SettingUp = true;
                DisableAll(settingUpPatMax);
                //Add TrainRegion to display's interactive graphics
                //Add SearchRegion to display's static graphics for display only.
                cogRecordDisplay1.InteractiveGraphics.Clear();
                cogRecordDisplay1.StaticGraphics.Clear();

                cogRecordDisplay1.InteractiveGraphics.Add(cogPMAlignTool.Pattern.TrainRegion as ICogGraphicInteractive, "test", false);
                
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



                

                //if ((cogPMAlignTool.SearchRegion != null))
                //{
                //    //cogPMAlignTool.SearchRegion.FitToImage(cogRecordDisplay1.Image, 0.990,0.999);
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
                    CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
                    CalibNPointTool.Run();
                    cogPMAlignTool.Pattern.Train();


                    //SavePatternToFile();
                    //save the teach pattern according to order name (ziv)
                    AppGen.Inst.MDImain.frmVisionMain.savePattern("Camera3", AppGen.Inst.OrderParams.InsertCode, cogPMAlignTool.Pattern); 
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
        private void button2_Click(object sender, EventArgs e)  //Base function - SaveCogTool
        {
            //CogSerializer.SaveObjectToFile(toolBlock,@"C:\PROJECTS\Stahli.Net\Bin\Debug\CognexStahli\Camera3.vpp"); //need to save with insernt name as given by asaf);
            CogSerializer.SaveObjectToFile(toolBlock, System.IO.Directory.GetCurrentDirectory() + "\\CognexStahli\\Camera3.vpp"); //need to save with insernt name as given by asaf);
        }
        public void Subject_Ran(object sender, EventArgs e)     //Run tool
        {
            
            cogAcqTool = cogToolBlockEditV21.Subject.Tools["CogAcqFifoTool1"] as CogAcqFifoTool;
            calbCheckerBoard.InputImage = cogToolBlockEditV21.Subject.Inputs["Image"].Value as CogImage8Grey;
            calbCheckerBoard = cogToolBlockEditV21.Subject.Tools["CogCalibCheckerboardTool1"] as CogCalibCheckerboardTool;
            calbCheckerBoard.Run();
            CalibNPointTool = cogToolBlockEditV21.Subject.Tools["CogCalibNPointToNPointTool1"] as CogCalibNPointToNPointTool;
            CalibNPointTool.InputImage = calbCheckerBoard.OutputImage;
            cogPMAlignTool = cogToolBlockEditV21.Subject.Tools["CogPMAlignTool1"] as CogPMAlignTool;
            cogPMAlignTool.InputImage = CalibNPointTool.OutputImage as CogImage8Grey;
            cogRecordDisplay1.Image = cogPMAlignTool.InputImage;
            cogPMAlignTool.Run();
            cogRecordDisplay1.Record = cogPMAlignTool.CreateLastRunRecord();
            cogRecordDisplay1.Fit(true);
            if (dataGridView1.Visible == false) dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();


            TrayIndexData TIS;
            AppGen.Inst.UnLoadCarrier.CurrIndex = 0;
            AppGen.Inst.UnLoadCarrier.IndexList.Clear();        
           
            try
            {
                for (int i = 0; i < cogPMAlignTool.Results.Count; i++)
                {
                    TIS = new TrayIndexData();
                    dataGridView1.Rows.Add();
                    dataGridView1[0, i].Value = cogPMAlignTool.Results[i].ID.ToString();
                    dataGridView1[1, i].Value = cogPMAlignTool.Results[i].Score.ToString();
                    dataGridView1[2, i].Value = cogPMAlignTool.Results[i].GetPose().Rotation * (180 / Math.PI);
                    dataGridView1[3, i].Value = cogPMAlignTool.Results[i].GetPose().TranslationX;
                    dataGridView1[4, i].Value = cogPMAlignTool.Results[i].GetPose().TranslationY;

                    TIS.X_VisRes = cogPMAlignTool.Results[i].GetPose().TranslationX;
                    TIS.Y_VisRes = cogPMAlignTool.Results[i].GetPose().TranslationY;
                    TIS.Angle_VisRes = cogPMAlignTool.Results[i].GetPose().Rotation * (180 / Math.PI);

                    AppGen.Inst.UnLoadCarrier.IndexList.Add(TIS);

                }
                txtPatMaxScoreValue.Text = cogPMAlignTool.Results.Count.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("No Input Image available for setup.", "PatMax Setup Error");
            }
        }
        private void button3_Click(object sender, EventArgs e)  //next 
        {
            if (AppGen.Inst.MainCycle.UnloadCarrierSliceNo >= 4)
            {
                AppGen.Inst.MainCycle.UnloadCarrierSliceNo = 1;
                txtCurrentIndex.Text = (AppGen.Inst.MainCycle.UnloadCarrierSliceNo).ToString();
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, AppGen.Inst.MainCycle.UnloadCarrierSliceNo.ToString());
                cmdImageAcquisitionNewImageCommand_Click(null, null);
                //CurrLineCoord = StartYIndx;

            }
            else
            {
                AppGen.Inst.MainCycle.UnloadCarrierSliceNo += 1;
                txtCurrentIndex.Text = (AppGen.Inst.MainCycle.UnloadCarrierSliceNo).ToString();
                AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, AppGen.Inst.MainCycle.UnloadCarrierSliceNo.ToString());
                cmdImageAcquisitionNewImageCommand_Click(null, null);
               // CurrLineCoord = ((AppGen.Inst.MainCycle.UnloadCarrierSliceNo) * ySearchReg);
            }
           // CurrLineCoord = StartYIndx + (((AppGen.Inst.MainCycle.UnloadCarrierSliceNo - 1) * ySearchReg));
            xMov += DeltaXFrameMov;
        }
        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkBox1.Checked)
        //    {
        //        flagFullTray = true;
        //    }
        //    else
        //    {
        //        flagFullTray = false;
        //    }
        //}
        private void button4_Click(object sender, EventArgs e)
        {
            CurrLineCoord = StartYIndx;
            AppGen.Inst.MainCycle.UnloadCarrierSliceNo = 1;
            txtCurrentIndex.Text = (AppGen.Inst.MainCycle.UnloadCarrierSliceNo).ToString();
            AppGen.Inst.MDImain.frmTitle.UpdateFrmTitle(FrmTitleData.AreaSliceUnloadCarrier, AppGen.Inst.MainCycle.UnloadCarrierSliceNo.ToString());
            cmdImageAcquisitionNewImageCommand_Click(null, null);
        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)  //In base - NumberToFindValue
        {
            PatMaxTool.RunParams.ApproximateNumberToFind = Convert.ToInt32(numericUpDown2.Value);//<------double NumToFindValue
            cogPMAlignTool.RunParams.ApproximateNumberToFind = Convert.ToInt32(numericUpDown2.Value);//<------double NumToFindValue
        }        
        int indexRunAgain = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {          
                if (indexRunAgain == 0)
                {
                    numericUpDown1.Value -= Convert.ToDecimal(0.05);
                    PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                    cogPMAlignTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);

                    dataGridView1.Rows.Clear();
                    CurrLineCoord = StartYIndx;
                }
                cmdImageAcquisitionNewImageCommand_Click(sender, e);           
                CurrLineCoord += ySearchReg;
                if (CurrLineCoord > 3000)
                {
                    CurrLineCoord = StartYIndx;
                }
                if (indexRunAgain == 0)
                {
                    MessageBox.Show("No more unfound inserts");
                    indexRunAgain = 0;
                    numericUpDown1.Value += Convert.ToDecimal(0.05);
                    PatMaxTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                    cogPMAlignTool.RunParams.AcceptThreshold = Convert.ToDouble(numericUpDown1.Value);
                    dataGridView1.Rows.Clear();
                }
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
                cogAcqTool.Operator.OwnedBrightnessParams.Brightness = brightnessParams.Brightness;
            }
        }

        // delegates
        private delegate void LoadOrderDataDelegate();
        private LoadOrderDataDelegate loadOrderDataDelegate;
        private void LoadOrderDataDelegateFunc()
        {
            try
            {
                brightnessUpDown.Value = Convert.ToDecimal(AppGen.Inst.OrderParams.Cam3_Brightness);
                numericUpDown1.Value = Convert.ToDecimal(AppGen.Inst.OrderParams.Cam3_Score);
                contrastUpDown.Value = Convert.ToDecimal(AppGen.Inst.OrderParams.Cam3_Contrast);
                numericUpDown3.Value = Convert.ToDecimal(AppGen.Inst.OrderParams.Cam3_Angle);
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

        private delegate void VisionActionDelegate(VisionActionType Action);
        private VisionActionDelegate visionActionDelegate;
        private void VisionActionDelegateFunc(VisionActionType Action)
        {
            try
            {
                switch (Action)
                {
                    case VisionActionType.ImageAcquisition:
                        cmdImageAcquisitionNewImageCommand_Click(null,null);
                        break;
                    case VisionActionType.RunTool:
                        Subject_Ran(null,null);
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
                    CalibNPointTool.Calibration.SetRawCalibratedPoint(i-1, AppGen.Inst.VisionParam.UnloadCarrierCalibPt[i-1].X,
                        AppGen.Inst.VisionParam.UnloadCarrierCalibPt[i-1].Y);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
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

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            cogPMAlignTool.RunParams.ZoneAngle.Low = (-(Convert.ToDouble(numericUpDown3.Value))) * Math.PI / 180; //convert to radian
            cogPMAlignTool.RunParams.ZoneAngle.High = (Convert.ToDouble(numericUpDown3.Value)) * Math.PI / 180;
        }

        private void cmdSaveParamToOrder_Click(object sender, EventArgs e)
        {      
            AppGen.Inst.OrderParams.Cam3_Brightness = Convert.ToDouble(brightnessUpDown.Value);
            AppGen.Inst.OrderParams.Cam3_Score = Convert.ToDouble(numericUpDown1.Value);
            AppGen.Inst.OrderParams.Cam3_Contrast = Convert.ToDouble(contrastUpDown.Value);
            AppGen.Inst.OrderParams.Cam3_Angle = Convert.ToDouble(numericUpDown3.Value);
            AppGen.Inst.OrderParams.Serialize(AppGen.Inst.OrderParams.InsertCode);

            AppGen.Inst.MDImain.frmOrderEditor.lblCam3Brightness.Text = AppGen.Inst.OrderParams.Cam3_Brightness.ToString();
            AppGen.Inst.MDImain.frmOrderEditor.lblCam3Score.Text = AppGen.Inst.OrderParams.Cam3_Score.ToString();
            AppGen.Inst.MDImain.frmOrderEditor.lblCam3Contrast.Text = AppGen.Inst.OrderParams.Cam3_Contrast.ToString();
            AppGen.Inst.MDImain.frmOrderEditor.lblCam3Angle.Text = AppGen.Inst.OrderParams.Cam3_Angle.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmVisionMain.FrmUnloadCarrier.flagFullTray = checkBox1.Checked;
        }
        void SavePatternToFile()
        {
            try
            {

                string path = System.IO.Directory.GetCurrentDirectory() + "\\CognexStahli\\PATTERN1.vpp";
                if (string.IsNullOrEmpty(path)) return;

                //path += "PMAlign.vpp";
                CogSerializer.SaveObjectToFile(cogPMAlignTool.Pattern, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadPatternFromFile()
        {
            // Ziv 14.07.15
            cogPMAlignTool.Pattern = AppGen.Inst.MDImain.frmVisionMain.loadPattern("Camera3", AppGen.Inst.OrderParams.InsertCode);   //13.07.15 (Ziv)
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

