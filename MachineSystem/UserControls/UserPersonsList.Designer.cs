namespace MachineSystem.UserControls
{
    partial class UserPersonsList
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
            this.label3 = new System.Windows.Forms.Label();
            this.lblRealCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblStandardCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitleName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "实配:";
            this.label3.Click += new System.EventHandler(this.UserPersonsList_Click);
            // 
            // lblRealCount
            // 
            this.lblRealCount.AutoSize = true;
            this.lblRealCount.Location = new System.Drawing.Point(37, 62);
            this.lblRealCount.Name = "lblRealCount";
            this.lblRealCount.Size = new System.Drawing.Size(17, 12);
            this.lblRealCount.TabIndex = 13;
            this.lblRealCount.Text = "19";
            this.lblRealCount.Click += new System.EventHandler(this.UserPersonsList_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "人";
            this.label7.Click += new System.EventHandler(this.UserPersonsList_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(54, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "人";
            this.label6.Click += new System.EventHandler(this.UserPersonsList_Click);
            // 
            // lblStandardCount
            // 
            this.lblStandardCount.AutoSize = true;
            this.lblStandardCount.Location = new System.Drawing.Point(36, 42);
            this.lblStandardCount.Name = "lblStandardCount";
            this.lblStandardCount.Size = new System.Drawing.Size(17, 12);
            this.lblStandardCount.TabIndex = 10;
            this.lblStandardCount.Text = "19";
            this.lblStandardCount.Click += new System.EventHandler(this.UserPersonsList_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "标配:";
            this.label2.Click += new System.EventHandler(this.UserPersonsList_Click);
            // 
            // lblTitleName
            // 
            this.lblTitleName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleName.Location = new System.Drawing.Point(1, 0);
            this.lblTitleName.Name = "lblTitleName";
            this.lblTitleName.Size = new System.Drawing.Size(77, 42);
            this.lblTitleName.TabIndex = 9;
            this.lblTitleName.Text = "班长";
            this.lblTitleName.Click += new System.EventHandler(this.UserPersonsList_Click);
            // 
            // UserPersonsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRealCount);
            this.Controls.Add(this.lblTitleName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblStandardCount);
            this.Name = "UserPersonsList";
            this.Size = new System.Drawing.Size(77, 84);
            this.Click += new System.EventHandler(this.UserPersonsList_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRealCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStandardCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitleName;
    }
}
