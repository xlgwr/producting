using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MachineSystem.form.Pad
{
    public partial class frmOvertimeHours : Framework.Abstract.frmBaseXC
    {
        /// <summary>
        /// 加班时数
        /// </summary>
        public string m_Hours = "0";

        public frmOvertimeHours()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void btnAddHour_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtHour.Text.Trim()) <= 8)
            {
                txtHour.Text = (int.Parse(txtHour.Text.Trim()) + 1).ToString();
            }
        }

        private void btnMinusHour_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtHour.Text.Trim()) > 0)
            {
                txtHour.Text = (int.Parse(txtHour.Text.Trim()) - 1).ToString();
            }
        }

        private void btnAddHour2_Click(object sender, EventArgs e)
        {
            int hour = int.Parse(txtHour.Text.Trim()) + int.Parse(txtHour2.Text.Trim());

            if ((int.Parse(txtHour.Text.Trim()) == 0 || int.Parse(txtHour.Text.Trim()) == 1))
            {
                if (int.Parse(txtHour2.Text.Trim()) == 9)
                {
                    txtHour.Text = (int.Parse(txtHour.Text) + 1).ToString();
                    txtHour2.Text = "0";
                }
                else if (hour <= 23)
                {
                    txtHour2.Text = (int.Parse(txtHour2.Text.Trim()) + 1).ToString();
                }
            }
            else if (int.Parse(txtHour.Text.Trim()) == 2 && int.Parse(txtHour2.Text.Trim()) <= 3)
            {
                txtHour2.Text = (int.Parse(txtHour2.Text.Trim()) + 1).ToString();
            }
            else if (int.Parse(txtHour.Text.Trim()) <= 8 && int.Parse(txtHour2.Text.Trim()) <= 8)
            {
                txtHour2.Text = (int.Parse(txtHour2.Text.Trim()) + 1).ToString();
            }
        }

        private void btnMinusHour2_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtHour2.Text.Trim()) > 0)
            {
                txtHour2.Text = (int.Parse(txtHour2.Text.Trim()) - 1).ToString();
            }
        }

        private void btnAddSecond_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtSecond.Text.Trim()) <= 50)
            {
                txtSecond.Text = (int.Parse(txtSecond.Text.Trim()) + 25).ToString();
            }
        }

        private void btnMinusSecond_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtSecond.Text.Trim()) >= 25)
            {
                txtSecond.Text = (int.Parse(txtSecond.Text.Trim()) - 25).ToString();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            m_Hours = txtHour.Text.Trim() + txtHour2.Text.Trim() + "." + txtSecond.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtHour_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
