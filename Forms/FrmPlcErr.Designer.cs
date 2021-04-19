namespace Stahli2Robots
{
    partial class FrmPlcErr
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
            this.cmdRefreshAllErrors = new System.Windows.Forms.Button();
            this.grpUnloadConv = new System.Windows.Forms.GroupBox();
            this.gridUnloadConvErrors = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpLoadConv = new System.Windows.Forms.GroupBox();
            this.gridLoadConvErrors = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rgpTable = new System.Windows.Forms.GroupBox();
            this.gridTableIndexErrors = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gridGeneralErrors = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpUnloadConv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUnloadConvErrors)).BeginInit();
            this.grpLoadConv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLoadConvErrors)).BeginInit();
            this.rgpTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTableIndexErrors)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGeneralErrors)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdRefreshAllErrors
            // 
            this.cmdRefreshAllErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRefreshAllErrors.Location = new System.Drawing.Point(189, 743);
            this.cmdRefreshAllErrors.Name = "cmdRefreshAllErrors";
            this.cmdRefreshAllErrors.Size = new System.Drawing.Size(185, 28);
            this.cmdRefreshAllErrors.TabIndex = 3;
            this.cmdRefreshAllErrors.Text = "Reset errors";
            this.cmdRefreshAllErrors.UseVisualStyleBackColor = true;
            this.cmdRefreshAllErrors.Click += new System.EventHandler(this.cmdRefreshAllErrors_Click);
            // 
            // grpUnloadConv
            // 
            this.grpUnloadConv.Controls.Add(this.gridUnloadConvErrors);
            this.grpUnloadConv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpUnloadConv.ForeColor = System.Drawing.Color.Black;
            this.grpUnloadConv.Location = new System.Drawing.Point(12, 550);
            this.grpUnloadConv.Name = "grpUnloadConv";
            this.grpUnloadConv.Size = new System.Drawing.Size(514, 175);
            this.grpUnloadConv.TabIndex = 6;
            this.grpUnloadConv.TabStop = false;
            this.grpUnloadConv.Text = "Unload conveyor";
            // 
            // gridUnloadConvErrors
            // 
            this.gridUnloadConvErrors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridUnloadConvErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUnloadConvErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.gridUnloadConvErrors.Location = new System.Drawing.Point(6, 18);
            this.gridUnloadConvErrors.Margin = new System.Windows.Forms.Padding(2);
            this.gridUnloadConvErrors.Name = "gridUnloadConvErrors";
            this.gridUnloadConvErrors.RowHeadersVisible = false;
            this.gridUnloadConvErrors.Size = new System.Drawing.Size(501, 150);
            this.gridUnloadConvErrors.TabIndex = 42;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "No.";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 28;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Unload Conveyor Error";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 450;
            // 
            // grpLoadConv
            // 
            this.grpLoadConv.Controls.Add(this.gridLoadConvErrors);
            this.grpLoadConv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLoadConv.ForeColor = System.Drawing.Color.Black;
            this.grpLoadConv.Location = new System.Drawing.Point(12, 369);
            this.grpLoadConv.Name = "grpLoadConv";
            this.grpLoadConv.Size = new System.Drawing.Size(514, 175);
            this.grpLoadConv.TabIndex = 5;
            this.grpLoadConv.TabStop = false;
            this.grpLoadConv.Text = "Load conveyor";
            // 
            // gridLoadConvErrors
            // 
            this.gridLoadConvErrors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridLoadConvErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLoadConvErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.gridLoadConvErrors.Location = new System.Drawing.Point(6, 18);
            this.gridLoadConvErrors.Margin = new System.Windows.Forms.Padding(2);
            this.gridLoadConvErrors.Name = "gridLoadConvErrors";
            this.gridLoadConvErrors.RowHeadersVisible = false;
            this.gridLoadConvErrors.Size = new System.Drawing.Size(501, 150);
            this.gridLoadConvErrors.TabIndex = 42;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "No.";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 28;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Load Conveyor Error";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 450;
            // 
            // rgpTable
            // 
            this.rgpTable.Controls.Add(this.gridTableIndexErrors);
            this.rgpTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rgpTable.ForeColor = System.Drawing.Color.Black;
            this.rgpTable.Location = new System.Drawing.Point(12, 188);
            this.rgpTable.Name = "rgpTable";
            this.rgpTable.Size = new System.Drawing.Size(514, 175);
            this.rgpTable.TabIndex = 4;
            this.rgpTable.TabStop = false;
            this.rgpTable.Text = "Index Table";
            // 
            // gridTableIndexErrors
            // 
            this.gridTableIndexErrors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridTableIndexErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTableIndexErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Score});
            this.gridTableIndexErrors.Location = new System.Drawing.Point(6, 18);
            this.gridTableIndexErrors.Margin = new System.Windows.Forms.Padding(2);
            this.gridTableIndexErrors.Name = "gridTableIndexErrors";
            this.gridTableIndexErrors.RowHeadersVisible = false;
            this.gridTableIndexErrors.Size = new System.Drawing.Size(501, 150);
            this.gridTableIndexErrors.TabIndex = 41;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 28;
            // 
            // Score
            // 
            this.Score.HeaderText = "Index Table Error";
            this.Score.Name = "Score";
            this.Score.ReadOnly = true;
            this.Score.Width = 450;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gridGeneralErrors);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(11, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 175);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General  Errors";
            // 
            // gridGeneralErrors
            // 
            this.gridGeneralErrors.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridGeneralErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridGeneralErrors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.gridGeneralErrors.Location = new System.Drawing.Point(6, 18);
            this.gridGeneralErrors.Margin = new System.Windows.Forms.Padding(2);
            this.gridGeneralErrors.Name = "gridGeneralErrors";
            this.gridGeneralErrors.RowHeadersVisible = false;
            this.gridGeneralErrors.Size = new System.Drawing.Size(501, 150);
            this.gridGeneralErrors.TabIndex = 41;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "No.";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 28;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "General Error";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 450;
            // 
            // FrmPlcErr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 785);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpUnloadConv);
            this.Controls.Add(this.grpLoadConv);
            this.Controls.Add(this.rgpTable);
            this.Controls.Add(this.cmdRefreshAllErrors);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPlcErr";
            this.Text = "FrmPlcErr";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserCloseForm);
            this.Load += new System.EventHandler(this.FrmPlcErr_Load);
            this.grpUnloadConv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridUnloadConvErrors)).EndInit();
            this.grpLoadConv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLoadConvErrors)).EndInit();
            this.rgpTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTableIndexErrors)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGeneralErrors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdRefreshAllErrors;
        private System.Windows.Forms.GroupBox grpUnloadConv;
        private System.Windows.Forms.GroupBox grpLoadConv;
        private System.Windows.Forms.GroupBox rgpTable;
        private System.Windows.Forms.DataGridView gridUnloadConvErrors;
        private System.Windows.Forms.DataGridView gridLoadConvErrors;
        private System.Windows.Forms.DataGridView gridTableIndexErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gridGeneralErrors;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}