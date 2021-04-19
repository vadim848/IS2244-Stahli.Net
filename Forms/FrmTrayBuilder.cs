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
    public partial class FrmTrayBuilder : Form
    {
        public FrmTrayBuilder()
        {
            InitializeComponent();
            SchemModeFl = true;
        }

        private void cmbTrayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            switch (cmbTrayType.SelectedIndex)
            {
                case 0:
                    picTrayType.Image = Properties.Resources.OrdineryTray;
                    dataGridView1.Columns[0].HeaderText = "Tray Name";
                    dataGridView1.Columns[1].HeaderText = "No of Ereas";
                    dataGridView1.Columns[2].HeaderText = "X dist. Between Ereas";
                    dataGridView1.Columns[3].HeaderText = "No. of Rows";
                    dataGridView1.Columns[4].HeaderText = "No. of Columns PerEreas";
                    dataGridView1.Columns[5].HeaderText = "X Distance between Columns";
                    dataGridView1.Columns[6].HeaderText = "Y Distance between Rows";
                    dataGridView1.Columns[7].HeaderText = "X Coord of first place";
                    dataGridView1.Columns[8].HeaderText = "Y Coord of first place";
                    dataGridView1.Columns[9].HeaderText = "No of Grippers";
                    dataGridView1.Columns[10].HeaderText = "One by One flage";
                    dataGridView1.Columns[11].HeaderText = "Angle of Gripper";
                    break;
                case 1:
                    picTrayType.Image = Properties.Resources.TriangleTray;
                    dataGridView1.Columns[0].HeaderText = "Tray Name";
                    dataGridView1.Columns[1].HeaderText = "No of Rows";
                    dataGridView1.Columns[2].HeaderText = "No of Columns";
                    dataGridView1.Columns[3].HeaderText = "X Offset 1";
                    dataGridView1.Columns[4].HeaderText = "X Offset 2";
                    dataGridView1.Columns[5].HeaderText = "Y Offset of first row";
                    dataGridView1.Columns[6].HeaderText = "X step";
                    dataGridView1.Columns[7].HeaderText = "Y step";
                    dataGridView1.Columns[8].HeaderText = "";
                    dataGridView1.Columns[9].HeaderText = "";
                    dataGridView1.Columns[10].HeaderText = "";
                    dataGridView1.Columns[11].HeaderText = "";
                    break;
                case 2:
                    picTrayType.Image = Properties.Resources.Triangle90Tray;
                    dataGridView1.Columns[0].HeaderText = "Tray Name";
                    dataGridView1.Columns[1].HeaderText = "No of Rows";
                    dataGridView1.Columns[2].HeaderText = "No of Columns";
                    dataGridView1.Columns[3].HeaderText = "X Offset of first row";
                    dataGridView1.Columns[4].HeaderText = "Y Offset first";
                    dataGridView1.Columns[5].HeaderText = "Y Offset Second";
                    dataGridView1.Columns[6].HeaderText = "X step";
                    dataGridView1.Columns[7].HeaderText = "Y step";
                    dataGridView1.Columns[8].HeaderText = "";
                    dataGridView1.Columns[9].HeaderText = "";
                    dataGridView1.Columns[10].HeaderText = "";
                    dataGridView1.Columns[11].HeaderText = "";
                    break;
                case 3:
                    picTrayType.Image = Properties.Resources.RombsTray;
                    dataGridView1.Columns[0].HeaderText = "Tray Name";
                    dataGridView1.Columns[1].HeaderText = "No of Rows";
                    dataGridView1.Columns[2].HeaderText = "Insert No. in odd rows";
                    dataGridView1.Columns[3].HeaderText = "Insert No. in even rows";
                    dataGridView1.Columns[4].HeaderText = "X Offset of first row";
                    dataGridView1.Columns[5].HeaderText = "Y Offset of first row";
                    dataGridView1.Columns[6].HeaderText = "X from first to second column";
                    dataGridView1.Columns[7].HeaderText = "Y from first to second row";
                    dataGridView1.Columns[8].HeaderText = "X step";
                    dataGridView1.Columns[9].HeaderText = "Y step";
                    dataGridView1.Columns[10].HeaderText = "Angle of hand rotation";
                    dataGridView1.Columns[11].HeaderText = "number of Grippers";
                    break;
            }
        }

        private void cmdPreviewTrayFile_Click(object sender, EventArgs e)
        {
            if (SchemModeFl)
            {
                cmdPreviewTrayFile.Text = "Back to Tray Schem";
                SchemModeFl = false;
                picTrayType.Image = Properties.Resources.attention;
            }
            else
            {
                cmdPreviewTrayFile.Text = "Preview Tray File";
                SchemModeFl = true;
                switch (cmbTrayType.SelectedIndex)
                {
                    case 0:
                        picTrayType.Image = Properties.Resources.OrdineryTray;
                        break;
                    case 1:
                        picTrayType.Image = Properties.Resources.TriangleTray;
                        break;
                    case 2:
                        picTrayType.Image = Properties.Resources.Triangle90Tray;
                        break;
                    case 3:
                        picTrayType.Image = Properties.Resources.RombsTray;
                        break;
                }
            }
            
        }
        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }


        // members:
        bool SchemModeFl;
    }     
}
