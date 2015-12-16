namespace MachineSystem.UserControls
{
    partial class LineControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblSearchDate = new DevExpress.XtraEditors.LabelControl();
            this.linkLabelGuanwei = new System.Windows.Forms.LinkLabel();
            this.linkLabelChar = new System.Windows.Forms.LinkLabel();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.lblSearchDate);
            this.panelControl1.Controls.Add(this.linkLabelGuanwei);
            this.panelControl1.Controls.Add(this.linkLabelChar);
            this.panelControl1.Controls.Add(this.lblUserID);
            this.panelControl1.Controls.Add(this.lblTitle);
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(139, 106);
            this.panelControl1.TabIndex = 0;
            // 
            // lblSearchDate
            // 
            this.lblSearchDate.Location = new System.Drawing.Point(84, 60);
            this.lblSearchDate.Name = "lblSearchDate";
            this.lblSearchDate.Size = new System.Drawing.Size(0, 14);
            this.lblSearchDate.TabIndex = 345;
            // 
            // linkLabelGuanwei
            // 
            this.linkLabelGuanwei.AutoSize = true;
            this.linkLabelGuanwei.BackColor = System.Drawing.Color.LightBlue;
            this.linkLabelGuanwei.Location = new System.Drawing.Point(82, 84);
            this.linkLabelGuanwei.Name = "linkLabelGuanwei";
            this.linkLabelGuanwei.Size = new System.Drawing.Size(29, 12);
            this.linkLabelGuanwei.TabIndex = 344;
            this.linkLabelGuanwei.TabStop = true;
            this.linkLabelGuanwei.Text = "关位";
            this.linkLabelGuanwei.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGuanwei_LinkClicked);
            // 
            // linkLabelChar
            // 
            this.linkLabelChar.AutoSize = true;
            this.linkLabelChar.BackColor = System.Drawing.Color.LightBlue;
            this.linkLabelChar.Location = new System.Drawing.Point(32, 84);
            this.linkLabelChar.Name = "linkLabelChar";
            this.linkLabelChar.Size = new System.Drawing.Size(29, 12);
            this.linkLabelChar.TabIndex = 343;
            this.linkLabelChar.TabStop = true;
            this.linkLabelChar.Text = "图表";
            this.linkLabelChar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelChar_LinkClicked);
            // 
            // lblUserID
            // 
            this.lblUserID.BackColor = System.Drawing.Color.LightBlue;
            this.lblUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUserID.Location = new System.Drawing.Point(0, 77);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(137, 27);
            this.lblUserID.TabIndex = 342;
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Silver;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(0, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(137, 75);
            this.lblTitle.TabIndex = 341;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "LineControl";
            this.Size = new System.Drawing.Size(139, 106);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.LinkLabel linkLabelGuanwei;
        private System.Windows.Forms.LinkLabel linkLabelChar;
        private System.Windows.Forms.Label lblUserID;
        private DevExpress.XtraEditors.LabelControl lblSearchDate;
    }
}
