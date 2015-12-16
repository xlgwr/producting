namespace MachineSystem.UserControls
{
    partial class UserPersonNull
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
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (System.Exception ex)
            {
                //throw;
            }

        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblType = new System.Windows.Forms.Label();
            this.lblRemind = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape3 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape8 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape11 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape10 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape7 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape6 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape5 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape4 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.White;
            this.lblType.Font = new System.Drawing.Font("宋体", 7F);
            this.lblType.Location = new System.Drawing.Point(38, 47);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 11);
            this.lblType.TabIndex = 355;
            this.lblType.Text = "申请中";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblType.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lblType.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblRemind
            // 
            this.lblRemind.BackColor = System.Drawing.Color.White;
            this.lblRemind.Font = new System.Drawing.Font("宋体", 7F);
            this.lblRemind.Location = new System.Drawing.Point(38, 36);
            this.lblRemind.Name = "lblRemind";
            this.lblRemind.Size = new System.Drawing.Size(35, 11);
            this.lblRemind.TabIndex = 343;
            this.lblRemind.Text = "提醒";
            this.lblRemind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRemind.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lblRemind.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.White;
            this.lblTime.Font = new System.Drawing.Font("宋体", 7F);
            this.lblTime.Location = new System.Drawing.Point(38, 25);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 11);
            this.lblTime.TabIndex = 342;
            this.lblTime.Text = "08:30";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTime.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lblTime.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(2, 70);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(72, 12);
            this.lblUserName.TabIndex = 338;
            this.lblUserName.Text = "邝锦玲";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserName.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lblUserName.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblUserID
            // 
            this.lblUserID.BackColor = System.Drawing.Color.White;
            this.lblUserID.Font = new System.Drawing.Font("宋体", 9F);
            this.lblUserID.Location = new System.Drawing.Point(2, 58);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(72, 12);
            this.lblUserID.TabIndex = 339;
            this.lblUserID.Text = "F01737";
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserID.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lblUserID.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(2, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(72, 13);
            this.lblTitle.TabIndex = 340;
            this.lblTitle.Text = "班长-1";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lblTitle.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.White;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 7F);
            this.lblStatus.Location = new System.Drawing.Point(38, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 11);
            this.lblStatus.TabIndex = 336;
            this.lblStatus.Text = "正常";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lblStatus.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape1
            // 
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.SelectionColor = System.Drawing.Color.Black;
            this.lineShape1.X1 = 43;
            this.lineShape1.X2 = 70;
            this.lineShape1.Y1 = 41;
            this.lineShape1.Y2 = 41;
            this.lineShape1.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape1.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape3
            // 
            this.lineShape3.Cursor = System.Windows.Forms.Cursors.Default;
            this.lineShape3.Name = "lineShape2";
            this.lineShape3.SelectionColor = System.Drawing.Color.Black;
            this.lineShape3.X1 = 43;
            this.lineShape3.X2 = 70;
            this.lineShape3.Y1 = 55;
            this.lineShape3.Y2 = 55;
            this.lineShape3.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape3.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape2
            // 
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.SelectionColor = System.Drawing.Color.Black;
            this.lineShape2.X1 = 43;
            this.lineShape2.X2 = 70;
            this.lineShape2.Y1 = 28;
            this.lineShape2.Y2 = 28;
            this.lineShape2.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape2.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape8,
            this.lineShape11,
            this.lineShape10,
            this.lineShape7,
            this.lineShape6,
            this.lineShape5,
            this.lineShape4,
            this.lineShape1,
            this.lineShape3,
            this.lineShape2});
            this.shapeContainer1.Size = new System.Drawing.Size(77, 86);
            this.shapeContainer1.TabIndex = 356;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape8
            // 
            this.lineShape8.Name = "lineShape8";
            this.lineShape8.SelectionColor = System.Drawing.Color.Black;
            this.lineShape8.X1 = 43;
            this.lineShape8.X2 = 43;
            this.lineShape8.Y1 = 62;
            this.lineShape8.Y2 = 18;
            this.lineShape8.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape8.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape11
            // 
            this.lineShape11.Name = "lineShape11";
            this.lineShape11.SelectionColor = System.Drawing.Color.Black;
            this.lineShape11.X1 = 2;
            this.lineShape11.X2 = 87;
            this.lineShape11.Y1 = 2;
            this.lineShape11.Y2 = 2;
            this.lineShape11.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape11.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape10
            // 
            this.lineShape10.Name = "lineShape10";
            this.lineShape10.SelectionColor = System.Drawing.Color.Black;
            this.lineShape10.X1 = 88;
            this.lineShape10.X2 = 88;
            this.lineShape10.Y1 = 2;
            this.lineShape10.Y2 = 92;
            this.lineShape10.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape10.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape7
            // 
            this.lineShape7.Name = "lineShape7";
            this.lineShape7.SelectionColor = System.Drawing.Color.Black;
            this.lineShape7.X1 = 2;
            this.lineShape7.X2 = 2;
            this.lineShape7.Y1 = 2;
            this.lineShape7.Y2 = 92;
            this.lineShape7.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape7.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape6
            // 
            this.lineShape6.Name = "lineShape6";
            this.lineShape6.SelectionColor = System.Drawing.Color.Black;
            this.lineShape6.X1 = 2;
            this.lineShape6.X2 = 70;
            this.lineShape6.Y1 = 16;
            this.lineShape6.Y2 = 16;
            this.lineShape6.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape6.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape5
            // 
            this.lineShape5.Name = "lineShape5";
            this.lineShape5.SelectionColor = System.Drawing.Color.Black;
            this.lineShape5.X1 = 2;
            this.lineShape5.X2 = 87;
            this.lineShape5.Y1 = 84;
            this.lineShape5.Y2 = 84;
            this.lineShape5.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape5.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lineShape4
            // 
            this.lineShape4.Name = "lineShape4";
            this.lineShape4.SelectionColor = System.Drawing.Color.Black;
            this.lineShape4.X1 = 3;
            this.lineShape4.X2 = 60;
            this.lineShape4.Y1 = 68;
            this.lineShape4.Y2 = 68;
            this.lineShape4.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.lineShape4.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::MachineSystem.Properties.Resources._1;
            this.pictureBox1.Location = new System.Drawing.Point(2, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 69);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 335;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.UserPersonNull_Click);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // UserPersonNull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.shapeContainer1);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblRemind);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUserID);
            this.Name = "UserPersonNull";
            this.Size = new System.Drawing.Size(77, 86);
            this.Click += new System.EventHandler(this.UserPersonNull_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblType;
        public System.Windows.Forms.Label lblRemind;
        public System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserID;
        public System.Windows.Forms.PictureBox pictureBox1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape3;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.Label lblStatus;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape4;
        public System.Windows.Forms.Label lblTitle;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape6;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape5;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape7;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape11;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape10;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape8;
    }
}
