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
    public partial class FrmCarrierBuilder : Form
    {
        public FrmCarrierBuilder()
        {
            InitializeComponent();
        }




        private void UserCloseForm(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
