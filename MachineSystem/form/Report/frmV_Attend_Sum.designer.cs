namespace MachineSystem.TabPage
{
    partial class frmV_Attend_Sum
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAttendDetail = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateOperDate1 = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblInCnt = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAttendCnt = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblVacateCnt = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblAttendRate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnLineExChange = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblEnReplaceJobsCnt = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblAlredyReplaceJobsCnt = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblReplaceJobsCnt = new System.Windows.Forms.Label();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAttendDetail);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1149, 105);
            this.panel1.TabIndex = 0;
            // 
            // btnAttendDetail
            // 
            this.btnAttendDetail.Location = new System.Drawing.Point(969, 38);
            this.btnAttendDetail.Name = "btnAttendDetail";
            this.btnAttendDetail.Size = new System.Drawing.Size(147, 57);
            this.btnAttendDetail.TabIndex = 2;
            this.btnAttendDetail.Text = "出勤详细";
            this.btnAttendDetail.UseVisualStyleBackColor = true;
            this.btnAttendDetail.Click += new System.EventHandler(this.btnAttendDetail_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightCoral;
            this.panel2.Controls.Add(this.dateOperDate1);
            this.panel2.Location = new System.Drawing.Point(724, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(239, 55);
            this.panel2.TabIndex = 1;
            // 
            // dateOperDate1
            // 
            this.dateOperDate1.EditValue = "";
            this.dateOperDate1.Location = new System.Drawing.Point(3, 17);
            this.dateOperDate1.Name = "dateOperDate1";
            this.dateOperDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateOperDate1.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateOperDate1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateOperDate1.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateOperDate1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateOperDate1.Properties.Mask.EditMask = "";
            this.dateOperDate1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateOperDate1.Properties.MaxLength = 30;
            this.dateOperDate1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateOperDate1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateOperDate1.Size = new System.Drawing.Size(233, 21);
            this.dateOperDate1.TabIndex = 80;
            this.dateOperDate1.TabStop = false;
            this.dateOperDate1.Tag = "OperDate";
            this.dateOperDate1.EditValueChanged += new System.EventHandler(this.dateOperDate1_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(312, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "制造部人员出勤情况";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(0, 131);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 373);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(138, 132);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(83, 33);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "在籍 ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(414, 131);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 33);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "出勤";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(703, 131);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(83, 33);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "休假 ";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(973, 131);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(99, 33);
            this.labelControl4.TabIndex = 5;
            this.labelControl4.Text = "出勤率";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGreen;
            this.panel3.Controls.Add(this.lblInCnt);
            this.panel3.Location = new System.Drawing.Point(101, 177);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(171, 91);
            this.panel3.TabIndex = 6;
            // 
            // lblInCnt
            // 
            this.lblInCnt.AutoSize = true;
            this.lblInCnt.Font = new System.Drawing.Font("黑体", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblInCnt.Location = new System.Drawing.Point(36, 7);
            this.lblInCnt.Name = "lblInCnt";
            this.lblInCnt.Size = new System.Drawing.Size(0, 67);
            this.lblInCnt.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightGreen;
            this.panel4.Controls.Add(this.lblAttendCnt);
            this.panel4.Location = new System.Drawing.Point(358, 176);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(185, 91);
            this.panel4.TabIndex = 7;
            // 
            // lblAttendCnt
            // 
            this.lblAttendCnt.AutoSize = true;
            this.lblAttendCnt.Font = new System.Drawing.Font("黑体", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAttendCnt.Location = new System.Drawing.Point(42, 8);
            this.lblAttendCnt.Name = "lblAttendCnt";
            this.lblAttendCnt.Size = new System.Drawing.Size(0, 67);
            this.lblAttendCnt.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightCoral;
            this.panel5.Controls.Add(this.lblVacateCnt);
            this.panel5.Location = new System.Drawing.Point(646, 176);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(198, 91);
            this.panel5.TabIndex = 8;
            // 
            // lblVacateCnt
            // 
            this.lblVacateCnt.AutoSize = true;
            this.lblVacateCnt.Font = new System.Drawing.Font("黑体", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVacateCnt.Location = new System.Drawing.Point(66, 8);
            this.lblVacateCnt.Name = "lblVacateCnt";
            this.lblVacateCnt.Size = new System.Drawing.Size(0, 67);
            this.lblVacateCnt.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightGreen;
            this.panel6.Controls.Add(this.lblAttendRate);
            this.panel6.Location = new System.Drawing.Point(910, 176);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(225, 91);
            this.panel6.TabIndex = 9;
            // 
            // lblAttendRate
            // 
            this.lblAttendRate.AutoSize = true;
            this.lblAttendRate.Font = new System.Drawing.Font("黑体", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAttendRate.Location = new System.Drawing.Point(22, 15);
            this.lblAttendRate.Name = "lblAttendRate";
            this.lblAttendRate.Size = new System.Drawing.Size(0, 60);
            this.lblAttendRate.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(69, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 38);
            this.label3.TabIndex = 10;
            this.label3.Text = "替关者状况";
            // 
            // btnLineExChange
            // 
            this.btnLineExChange.Location = new System.Drawing.Point(358, 287);
            this.btnLineExChange.Name = "btnLineExChange";
            this.btnLineExChange.Size = new System.Drawing.Size(185, 57);
            this.btnLineExChange.TabIndex = 11;
            this.btnLineExChange.Text = "Line替关对应";
            this.btnLineExChange.UseVisualStyleBackColor = true;
            this.btnLineExChange.Click += new System.EventHandler(this.btnLineExChange_Click);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightGreen;
            this.panel7.Controls.Add(this.lblEnReplaceJobsCnt);
            this.panel7.Location = new System.Drawing.Point(646, 409);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(198, 92);
            this.panel7.TabIndex = 14;
            // 
            // lblEnReplaceJobsCnt
            // 
            this.lblEnReplaceJobsCnt.AutoSize = true;
            this.lblEnReplaceJobsCnt.Font = new System.Drawing.Font("黑体", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblEnReplaceJobsCnt.Location = new System.Drawing.Point(64, 6);
            this.lblEnReplaceJobsCnt.Name = "lblEnReplaceJobsCnt";
            this.lblEnReplaceJobsCnt.Size = new System.Drawing.Size(0, 67);
            this.lblEnReplaceJobsCnt.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.LightGreen;
            this.panel8.Controls.Add(this.lblAlredyReplaceJobsCnt);
            this.panel8.Location = new System.Drawing.Point(358, 411);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(185, 91);
            this.panel8.TabIndex = 13;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // lblAlredyReplaceJobsCnt
            // 
            this.lblAlredyReplaceJobsCnt.AutoSize = true;
            this.lblAlredyReplaceJobsCnt.Font = new System.Drawing.Font("黑体", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAlredyReplaceJobsCnt.Location = new System.Drawing.Point(57, 5);
            this.lblAlredyReplaceJobsCnt.Name = "lblAlredyReplaceJobsCnt";
            this.lblAlredyReplaceJobsCnt.Size = new System.Drawing.Size(0, 67);
            this.lblAlredyReplaceJobsCnt.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.LightGreen;
            this.panel9.Controls.Add(this.lblReplaceJobsCnt);
            this.panel9.Location = new System.Drawing.Point(94, 415);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(171, 86);
            this.panel9.TabIndex = 12;
            this.panel9.Paint += new System.Windows.Forms.PaintEventHandler(this.panel9_Paint);
            // 
            // lblReplaceJobsCnt
            // 
            this.lblReplaceJobsCnt.AutoSize = true;
            this.lblReplaceJobsCnt.Font = new System.Drawing.Font("黑体", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReplaceJobsCnt.Location = new System.Drawing.Point(43, 8);
            this.lblReplaceJobsCnt.Name = "lblReplaceJobsCnt";
            this.lblReplaceJobsCnt.Size = new System.Drawing.Size(0, 67);
            this.lblReplaceJobsCnt.TabIndex = 0;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl5.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseForeColor = true;
            this.labelControl5.Location = new System.Drawing.Point(650, 359);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(165, 33);
            this.labelControl5.TabIndex = 17;
            this.labelControl5.Text = "未替关人数";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(356, 363);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(165, 33);
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "已替关人数";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("黑体", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelControl7.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Appearance.Options.UseForeColor = true;
            this.labelControl7.Location = new System.Drawing.Point(101, 360);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(132, 33);
            this.labelControl7.TabIndex = 15;
            this.labelControl7.Text = "替关人数";
            // 
            // frmV_Attend_Sum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButtonEnabled = false;
            this.ClientSize = new System.Drawing.Size(1149, 504);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.btnLineExChange);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.panel1);
            this.CopyAddEnabled = false;
            this.CopyAddVisibility = false;
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmV_Attend_Sum";
            this.NewButtonVisibility = false;
            this.SaveButtonEnabled = false;
            this.SaveButtonVisibility = false;
            this.SearchButtonVisibility = false;
            this.SelectAllButtonEnabled = false;
            this.SelectAllButtonVisibility = false;
            this.SelectOffButtonEnabled = false;
            this.SelectOffButtonVisibility = false;
            this.Text = "制造部Line出勤状况";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.splitterControl1, 0);
            this.Controls.SetChildIndex(this.labelControl1, 0);
            this.Controls.SetChildIndex(this.labelControl2, 0);
            this.Controls.SetChildIndex(this.labelControl3, 0);
            this.Controls.SetChildIndex(this.labelControl4, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.panel6, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.btnLineExChange, 0);
            this.Controls.SetChildIndex(this.panel9, 0);
            this.Controls.SetChildIndex(this.panel8, 0);
            this.Controls.SetChildIndex(this.panel7, 0);
            this.Controls.SetChildIndex(this.labelControl7, 0);
            this.Controls.SetChildIndex(this.labelControl6, 0);
            this.Controls.SetChildIndex(this.labelControl5, 0);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateOperDate1.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAttendDetail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit dateOperDate1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLineExChange;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.Label lblInCnt;
        private System.Windows.Forms.Label lblAttendCnt;
        private System.Windows.Forms.Label lblVacateCnt;
        private System.Windows.Forms.Label lblAttendRate;
        private System.Windows.Forms.Label lblEnReplaceJobsCnt;
        private System.Windows.Forms.Label lblAlredyReplaceJobsCnt;
        private System.Windows.Forms.Label lblReplaceJobsCnt;
    }
}