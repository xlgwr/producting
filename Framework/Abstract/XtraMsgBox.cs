using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace Framework.Abstract
{
    public static class XtraMsgBox
    {

        /// <summary>
        /// 记录日志数据
        /// </summary>
        private static ILog log;

        //
        // 摘要:
        //     Displays the XtraMessageBox with the specified text, caption, buttons and
        //     icon.
        //
        // 参数:
        //   text:
        //     A string value that specifies the text to display in the message box.
        //
        //   caption:
        //     A string value that specifies the message box's caption.
        //
        //   buttons:
        //     One of the System.Windows.Forms.MessageBoxButtons values that specify which
        //     buttons to display in the message box.
        //
        //   icon:
        //     One of the System.Windows.Forms.MessageBoxIcon values that specifies which
        //     icon to display in the message box.
        //
        // 返回结果:
        //     One of the System.Windows.Forms.DialogResult values.
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, icon);
        }

        /// <summary>
        /// 异常日志信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <param name="ex"></param>
        /// <param name="FormType"></param>
        /// <returns></returns>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, Exception ex,Type FormType)
        {
            log = LogManager.GetLogger(FormType);
            log.Error(ex);

            return DevExpress.XtraEditors.XtraMessageBox.Show(text, caption, buttons, icon);
        }

    }
}
