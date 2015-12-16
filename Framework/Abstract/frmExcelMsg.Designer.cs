namespace Framework.Abstract
{
    partial class ExcelMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelMsg));
            this.btnfolder = new DevExpress.XtraEditors.SimpleButton();
            this.btnfile = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.lblMsgInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnfolder
            // 
            this.btnfolder.Location = new System.Drawing.Point(4, 125);
            this.btnfolder.Name = "btnfolder";
            this.btnfolder.Size = new System.Drawing.Size(140, 46);
            this.btnfolder.TabIndex = 169;
            this.btnfolder.Text = "打开所在的文件夹";
            this.btnfolder.Click += new System.EventHandler(this.btnfolder_Click);
            // 
            // btnfile
            // 
            this.btnfile.Location = new System.Drawing.Point(145, 125);
            this.btnfile.Name = "btnfile";
            this.btnfile.Size = new System.Drawing.Size(140, 46);
            this.btnfile.TabIndex = 169;
            this.btnfile.Text = "打开导出文件";
            this.btnfile.Click += new System.EventHandler(this.btnfile_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(286, 125);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(140, 46);
            this.simpleButton3.TabIndex = 169;
            this.simpleButton3.Text = "确定";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // lblMsgInfo
            // 
            this.lblMsgInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblMsgInfo.Location = new System.Drawing.Point(12, 9);
            this.lblMsgInfo.Name = "lblMsgInfo";
            this.lblMsgInfo.Size = new System.Drawing.Size(408, 107);
            this.lblMsgInfo.TabIndex = 170;
            this.lblMsgInfo.Text = "label1";
            this.lblMsgInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ExcelMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 178);
            this.Controls.Add(this.lblMsgInfo);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.btnfile);
            this.Controls.Add(this.btnfolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExcelMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmExcelMsg";
            this.Load += new System.EventHandler(this.frmExcelMsg_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnfolder;
        private DevExpress.XtraEditors.SimpleButton btnfile;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private System.Windows.Forms.Label lblMsgInfo;
    }
}