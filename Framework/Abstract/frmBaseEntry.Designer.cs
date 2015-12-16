namespace Framework.Abstract
{
    partial class frmBaseEntry
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
            ((System.ComponentModel.ISupportInitialize)(this.validData)).BeginInit();
            this.SuspendLayout();
            // 
            // frmBaseEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "frmBaseEntry";
            this.Text = "frmBaseEntry";
            this.Load += new System.EventHandler(this.frmBaseEntry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.validData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}