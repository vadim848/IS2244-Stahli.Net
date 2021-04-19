using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Stahli2Robots
{
    public partial class FrmPlcErr : Form
    {
        public FrmPlcErr()
        {
            InitializeComponent();

        }


        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public void RefreshLists()
        {
            //// General
            gridGeneralErrors.Rows.Clear();
            for (int i = 0; i < 10; i++)
            {
                gridGeneralErrors.Rows.Add();
                gridGeneralErrors[0, i].Value = (i + 1).ToString();  //Err No.
                switch (i)
                {
                    case 0:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg1; //qq
                        break;
                    case 1:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg2;
                        break;
                    case 2:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg3;
                        break;
                    case 3:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg4;
                        break;
                    case 4:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg5;
                        break;
                    case 5:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg6;
                        break;
                    case 6:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg7;
                        break;
                    case 7:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg8;
                        break;
                    case 8:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg9;
                        break;
                    case 9:
                        gridGeneralErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.General_PlcErrMsg.Msg10;
                        break;
                }
            }


            //// load
            gridLoadConvErrors.Rows.Clear();
            for (int i = 0; i < 10; i++)
            {
                gridLoadConvErrors.Rows.Add();
                gridLoadConvErrors[0, i].Value = (i + 1).ToString();  //Err No.
                switch (i)
                {
                    case 0:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg1;
                        break;
                    case 1:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg2;
                        break;
                    case 2:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg3;
                        break;
                    case 3:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg4;
                        break;
                    case 4:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg5;
                        break;
                    case 5:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg6;
                        break;
                    case 6:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg7;
                        break;
                    case 7:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg8;
                        break;
                    case 8:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg9;
                        break;
                    case 9:
                        gridLoadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.LoadConv_PlcErrMsg.Msg10;
                        break;
                }
            }
           
            //// unload
            gridUnloadConvErrors.Rows.Clear();
            for (int i = 0; i < 10; i++)
            {
                gridUnloadConvErrors.Rows.Add();
                gridUnloadConvErrors[0, i].Value = (i + 1).ToString();  //Err No.
                switch (i)
                {
                    case 0:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg1;
                        break;
                    case 1:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg2;
                        break;
                    case 2:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg3;
                        break;
                    case 3:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg4;
                        break;
                    case 4:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg5;
                        break;
                    case 5:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg6;
                        break;
                    case 6:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg7;
                        break;
                    case 7:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg8;
                        break;
                    case 8:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg9;
                        break;
                    case 9:
                        gridUnloadConvErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.UnloadConv_PlcErrMsg.Msg10;
                        break;
                }
            }
            
            //// index table
            gridTableIndexErrors.Rows.Clear();
            for (int i = 0; i < 10; i++)
            {
                gridTableIndexErrors.Rows.Add();
                gridTableIndexErrors[0, i].Value = (i + 1).ToString();  //Err No.
                switch (i)
                {
                    case 0:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg1;
                        break;
                    case 1:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg2;
                        break;
                    case 2:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg3;
                        break;
                    case 3:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg4;
                        break;
                    case 4:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg5;
                        break;
                    case 5:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg6;
                        break;
                    case 6:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg7;
                        break;
                    case 7:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg8;
                        break;
                    case 8:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg9;
                        break;
                    case 9:
                        gridTableIndexErrors[1, i].Value = AppGen.Inst.MDImain.frmBeckhoff.IndexTable_PlcErrMsg.Msg10;
                        break;
                }
            }         
        }

        private void FrmPlcErr_Load(object sender, EventArgs e)
        {
            RefreshLists();
        }

        private void cmdRefreshAllErrors_Click(object sender, EventArgs e)
        {
            AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResetErrors = true;  //19.10.14
            AppGen.Inst.MDImain.frmBeckhoff.UpdatePlcData(AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.hResetErrors, AppGen.Inst.MDImain.frmBeckhoff.GeneralControl_PLC.ResetErrors); //19.10.14
           // RefreshLists();
        }
    }
}
