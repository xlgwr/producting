namespace MachineSystem.UserControls
{
    partial class UserPerson
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
            this.lblType = new System.Windows.Forms.Label();
            this.lblRemind = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblUserID = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.userPicture1 = new MachineSystem.UserControls.UserPicture();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.White;
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblType.Font = new System.Drawing.Font("宋体", 7F);
            this.lblType.Location = new System.Drawing.Point(39, 47);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(38, 13);
            this.lblType.TabIndex = 355;
            this.lblType.Text = "申请中";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblType.Click += new System.EventHandler(this.UserPerson_Click);
            this.lblType.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblRemind
            // 
            this.lblRemind.BackColor = System.Drawing.Color.White;
            this.lblRemind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRemind.Font = new System.Drawing.Font("宋体", 7F);
            this.lblRemind.Location = new System.Drawing.Point(39, 36);
            this.lblRemind.Name = "lblRemind";
            this.lblRemind.Size = new System.Drawing.Size(38, 13);
            this.lblRemind.TabIndex = 343;
            this.lblRemind.Text = "提醒";
            this.lblRemind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRemind.Click += new System.EventHandler(this.UserPerson_Click);
            this.lblRemind.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblTime
            // 
            this.lblTime.BackColor = System.Drawing.Color.White;
            this.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTime.Font = new System.Drawing.Font("宋体", 7F);
            this.lblTime.Location = new System.Drawing.Point(39, 26);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(38, 13);
            this.lblTime.TabIndex = 342;
            this.lblTime.Text = "08:30";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTime.Click += new System.EventHandler(this.UserPerson_Click);
            this.lblTime.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.Color.White;
            this.lblUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUserName.Location = new System.Drawing.Point(2, 71);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(75, 14);
            this.lblUserName.TabIndex = 338;
            this.lblUserName.Text = "邝锦玲";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserName.Click += new System.EventHandler(this.UserPerson_Click);
            this.lblUserName.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblUserID
            // 
            this.lblUserID.BackColor = System.Drawing.Color.White;
            this.lblUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUserID.Font = new System.Drawing.Font("宋体", 9F);
            this.lblUserID.Location = new System.Drawing.Point(2, 59);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(75, 13);
            this.lblUserID.TabIndex = 339;
            this.lblUserID.Text = "F01737";
            this.lblUserID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserID.Click += new System.EventHandler(this.UserPerson_Click);
            this.lblUserID.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(2, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(75, 13);
            this.lblTitle.TabIndex = 340;
            this.lblTitle.Text = "班长-1";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.UserPerson_Click);
            this.lblTitle.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.White;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Font = new System.Drawing.Font("宋体", 7F);
            this.lblStatus.Location = new System.Drawing.Point(39, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 13);
            this.lblStatus.TabIndex = 336;
            this.lblStatus.Text = "正常";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Click += new System.EventHandler(this.UserPerson_Click);
            this.lblStatus.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::MachineSystem.Properties.Resources._01;
            this.pictureBox1.Location = new System.Drawing.Point(3, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 335;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.UserPerson_Click);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.lblType_DoubleClick);
            // 
            // userPicture1
            // 
            this.userPicture1.BackColor = System.Drawing.Color.Black;
            this.userPicture1.BackgroundImage = global::MachineSystem.Properties.Resources._01;
            this.userPicture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.userPicture1.bgColor = System.Drawing.Color.Empty;
            this.userPicture1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.userPicture1.ImageUrl = "";
            this.userPicture1.Location = new System.Drawing.Point(2, 14);
            this.userPicture1.Name = "userPicture1";
            this.userPicture1.Size = new System.Drawing.Size(39, 46);
            this.userPicture1.TabIndex = 357;
            this.userPicture1.AllEventClick += new MachineSystem.UserControls.UserPicture.AllEvent(this.userPicture1_DoubleClick);
            this.userPicture1.PictureEventClick += new MachineSystem.UserControls.UserPicture.PictureEvent(this.UserPerson_Click);
            this.userPicture1.Click += new System.EventHandler(this.UserPerson_Click);
            this.userPicture1.DoubleClick += new System.EventHandler(this.userPicture1_DoubleClick);
            // 
            // UserPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.userPicture1);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblRemind);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblUserID);
            this.Name = "UserPerson";
            this.Size = new System.Drawing.Size(80, 89);
            this.Click += new System.EventHandler(this.UserPerson_Click);
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
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Label lblTitle;
        private UserPicture userPicture1;
    }
}
