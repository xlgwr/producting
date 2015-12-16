using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Text.RegularExpressions;
using Framework.Abstract;

using System.Diagnostics;
using System.Collections.Specialized;
using DevExpress.XtraEditors.DXErrorProvider;

namespace Framework.Libs
{
   public partial class Common
   {
        
       #region  画面输入控件处理

       /// <summary>
       /// 获取选择行数据显示
       /// </summary>
       /// <param name="dr"></param>
       public static void SetGridRowData(Control ctl, DataRow dr)
       {

           StringDictionary dicItemData = new StringDictionary();

           if (ctl==null ||dr == null) return;

           DataColumnCollection columns;
           columns = dr.Table.Columns;

           for (int i = 0; i < dr.ItemArray.Length; i++)
           {
               if (dr[i] != null)
               {
                   dicItemData[columns[i].ColumnName] = dr[i].ToString();
               }
           }

           Common.SetGroupData(ctl, ref dicItemData);
       }

        /// <summary>
        /// 将数据表格选中行数据显示编辑项目中
        /// </summary>
        /// <param name="dr">行对象</param>
        /// <param name="ctl">控件对象</param>
        public static void SetGroupData(Control ctl, ref StringDictionary sd)
        {

            if (ctl == null) return;

            if (ctl.Tag!=null && !string.IsNullOrEmpty(ctl.Tag.ToString()))
            switch (ctl.GetType().ToString())
            {
                case "DevExpress.XtraEditors.ButtonEdit":
                    ctl.Text = sd[ctl.Tag.ToString()];
                    break;
                case "DevExpress.XtraEditors.TextEdit":
                    ctl.Text = sd[ctl.Tag.ToString()];
                    break;
                case "DevExpress.XtraEditors.SpinEdit":
                    ctl.Text = sd[ctl.Tag.ToString()];
                    break;
                case "DevExpress.XtraEditors.MemoEdit":
                    ctl.Text = sd[ctl.Tag.ToString()];
                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    ((DevExpress.XtraEditors.ComboBoxEdit)ctl).Text = sd[ctl.Tag.ToString()];
                    break;
                case "DevExpress.XtraEditors.CalcEdit":
                    ((DevExpress.XtraEditors.CalcEdit)ctl).Value = Convert.ToDecimal(sd[ctl.Tag.ToString()]);
                    break;
                case "DevExpress.XtraEditors.CheckEdit":
                    ((DevExpress.XtraEditors.CheckEdit)ctl).Checked = Convert.ToBoolean(sd[ctl.Tag.ToString()]); 
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    ((DevExpress.XtraEditors.DateEdit)ctl).EditValue = sd[ctl.Tag.ToString()] == null ? null : sd[ctl.Tag.ToString()];
                    break;
                case "DevExpress.XtraEditors.RadioGroup":
                    ((DevExpress.XtraEditors.RadioGroup)ctl).EditValue = Convert.ToBoolean( sd[ctl.Tag.ToString()]);
                    break;
                case "DevExpress.XtraEditors.LookUpEdit":
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                    ////((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = false;
                    //((DevExpress.XtraEditors.LookUpEdit)child).Text = dr[i].ToString();
                    ((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue = sd[ctl.Tag.ToString()] == null ? null : sd[ctl.Tag.ToString()];
     
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = true;
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
                    break;
                case "DevExpress.XtraEditors.TimeEdit":
                    ((DevExpress.XtraEditors.TimeEdit)ctl).EditValue = sd[ctl.Tag.ToString()];
                    break;
            }
     

            if (ctl.Controls != null
                && ctl.GetType().ToString() != "DevExpress.XtraGrid.GridControl")
            {
                foreach (Control _ctlSub in ctl.Controls)
                {
                    //对窗体的所有控件处理
                    SetGroupData(_ctlSub, ref sd);
                }
            }
        }


        /// <summary>
        /// 获取画面输入数据信息
        /// </summary>
        /// <param name="dr">行对象</param>
        /// <param name="ctl">控件对象</param>
        public static void GetGroupDataSearch(Control ctl, ref StringDictionary sd)
        {
            if (ctl == null) return;

            if (ctl.Tag != null && !string.IsNullOrEmpty(ctl.Tag.ToString()))
            switch (ctl.GetType().ToString())
            {
                case "DevExpress.XtraEditors.ButtonEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.TextEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.SpinEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.MemoEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    if (((DevExpress.XtraEditors.ComboBoxEdit)ctl).SelectedIndex>=0)
                    {
                        sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.ComboBoxEdit)ctl).EditValue.ToString().Trim();
                    }
                    break;
                case "DevExpress.XtraEditors.CalcEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.CalcEdit)ctl).Value.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.CheckEdit":

                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.CheckEdit)ctl).Checked.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.DateEdit)ctl).EditValue.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.RadioGroup":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.RadioGroup)ctl).EditValue.ToString().Trim();
                    break;

                case "DevExpress.XtraEditors.LookUpEdit":
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                    ////((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = false;
                    //((DevExpress.XtraEditors.LookUpEdit)child).Text = dr[i].ToString();

                    if (((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue != null && ((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue.ToString () != DefineValue.DefalutItemAllNo)
                    {
                        sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue.ToString().Trim();
                    }

                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = true;
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
                    break;
                case "DevExpress.XtraEditors.TimeEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.TimeEdit)ctl).EditValue.ToString().Trim();
                    break;
            }

            if (ctl.Controls != null
                && ctl.GetType().ToString() != "DevExpress.XtraGrid.GridControl")
            {
                foreach (Control _ctlSub in ctl.Controls)
                {
                    //对窗体的所有控件处理
                    GetGroupDataSearch(_ctlSub, ref sd);
                }
            }
        }


        /// <summary>
        /// 获取画面输入数据信息
        /// </summary>
        /// <param name="dr">行对象</param>
        /// <param name="ctl">控件对象</param>
        public static void GetGroupData(Control ctl, ref StringDictionary sd)
        {
            if (ctl == null) return;

            if (ctl.Tag!=null && !string.IsNullOrEmpty (ctl.Tag.ToString()))

            switch (ctl.GetType().ToString() )
            {
                case "DevExpress.XtraEditors.ButtonEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.TextEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.SpinEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.MemoEdit":
                    sd[ctl.Tag.ToString()] = ctl.Text.Trim();
                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.ComboBoxEdit)ctl).EditValue.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.CalcEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.CalcEdit)ctl).Value.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.CheckEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.CheckEdit)ctl).Checked.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.DateEdit)ctl).EditValue.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.RadioGroup":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.RadioGroup)ctl).EditValue.ToString().Trim();
                    break;
                case "DevExpress.XtraEditors.LookUpEdit":
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
                    ////((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = false;
                    //((DevExpress.XtraEditors.LookUpEdit)child).Text = dr[i].ToString();

                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue == null ? sd[ctl.Tag.ToString()] : ((DevExpress.XtraEditors.LookUpEdit)ctl).EditValue.ToString().Trim();
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = true;
                    //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
                    break;
                case "DevExpress.XtraEditors.TimeEdit":
                    sd[ctl.Tag.ToString()] = ((DevExpress.XtraEditors.TimeEdit)ctl).EditValue.ToString().Trim();
                    break;
            }

            if (ctl.Controls != null 
                && ctl.GetType().ToString() != "DevExpress.XtraGrid.GridControl")
            {
                foreach (Control _ctlSub in ctl.Controls)
                {
                    //对窗体的所有控件处理
                    GetGroupData(_ctlSub,ref sd);
                }
            }
        }

        /// <summary>
        /// 清除画面输入数据信息
        /// </summary>
        /// <param name="dr">行对象</param>
        /// <param name="ctl">控件对象</param>
        public static void ClearGroupData(Control ctl)
        {

           if (ctl==null) return ;

            if (ctl.Tag != null && !string.IsNullOrEmpty(ctl.Tag.ToString()))
            if (ctl.Tag != null && ctl.Visible == true)
            {
                switch (ctl.GetType().ToString())
                {

                    case "DevExpress.XtraEditors.ButtonEdit":
                        ctl.Text = "";
                        break;
                    case "DevExpress.XtraEditors.TextEdit":
                        ctl.Text = "";
                        break;
                    case "DevExpress.XtraEditors.SpinEdit":
                        ctl.Text = "";
                        break;
                    case "DevExpress.XtraEditors.MemoEdit":
                        ctl.Text ="";
                        break;
                    case "DevExpress.XtraEditors.ComboBoxEdit":
                        ((DevExpress.XtraEditors.ComboBoxEdit)ctl).SelectedIndex  = 0;
                        break;
                    case "DevExpress.XtraEditors.CalcEdit":
                        ((DevExpress.XtraEditors.CalcEdit)ctl).Value = 0;
                        break;
                    case "DevExpress.XtraEditors.CheckEdit":
                        ((DevExpress.XtraEditors.CheckEdit)ctl).Checked =  false;
                        break;
                    case "DevExpress.XtraEditors.DateEdit":
                        ((DevExpress.XtraEditors.DateEdit)ctl).EditValue = "";
                        break;
                    case "DevExpress.XtraEditors.RadioGroup":
                        ((DevExpress.XtraEditors.RadioGroup)ctl).SelectedIndex =0;
                        break;
                    case "DevExpress.XtraEditors.TimeEdit":
                        ((DevExpress.XtraEditors.TimeEdit)ctl).EditValue = "";
                        break;
                }
            }

            if (ctl.Controls != null)
            {
                foreach (Control _ctlSub in ctl.Controls)
                {
                    //对窗体的所有控件处理
                    ClearGroupData(_ctlSub);
                }
            }
    
        }

       
        /// <summary>
        /// 清除画面输入异常信息
        /// </summary>
        /// <param name="ctl">控件对象</param>
        public static void ClearGroupErr(DXValidationProvider validData, Control ctl)
        {

            if (ctl == null) return;

            if (ctl.Tag != null && ctl.Visible == true)
            {
                validData.RemoveControlError(ctl);
            }

            if (ctl.Controls != null)
            {
                foreach (Control _ctlSub in ctl.Controls)
                {
                    //清除画面输入异常信息
                    ClearGroupErr(validData,_ctlSub);
                }
            }

        }

        #endregion

       #region 控件属性设置

        /// <summary>
        /// 设定工具栏的有效性
        /// </summary>
        /// <param name="mode">编辑的状态</param>
        public static void SetCmdControl(Common.DataModifyMode mode, frmBaseToolXC frmbase, int RecordCount)
        {
            try
            {
                if (mode == Common.DataModifyMode.dsp)  //如果是查看
                {
                    frmbase.NewButtonEnabled = true;
                    frmbase.PrintButtonEnabled = true;
                    frmbase.ExcelButtonEnabled = true;
                    frmbase.SaveButtonEnabled = false;
                    frmbase.CancelButtonEnabled = false;
                    frmbase.SearchButtonEnabled = true;
                    frmbase.DeleteButtonEnabled = true;
                    frmbase.EditButtonEnabled = true;

                    if (RecordCount == 0)
                    {

                        frmbase.PrintButtonEnabled = false;
                        frmbase.ExcelButtonEnabled = false;
                        frmbase.ImportButtonEnabled = false;
                        frmbase.SearchButtonEnabled = false;
                        frmbase.SaveButtonEnabled = false;
                        frmbase.CancelButtonEnabled = false;
                        frmbase.EditButtonEnabled = false;
                        frmbase.DeleteButtonEnabled = false;

                    }
                }
                else
                {
                    frmbase.NewButtonEnabled = false;
                    frmbase.PrintButtonEnabled = false;
                    frmbase.ImportButtonEnabled = false;
                    frmbase.SaveButtonEnabled = true;
                    frmbase.CancelButtonEnabled = true;
                    frmbase.ExcelButtonEnabled = false;
                    frmbase.SearchButtonEnabled = false;
                    frmbase.EditButtonEnabled = false;
                    frmbase.DeleteButtonEnabled = false;

                }
            }
            catch (Exception ex)
            {
                XtraMsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 设定窗体控件的可读性
        /// </summary>
        /// <param name="frmStance">窗体实例</param>
        /// <param name="onlyread">是否只读，true只读，否则可编辑</param>
        public static void SetFormReadOnly(Control ctl, bool onlyread)
        {
            if (ctl == null) return;

            switch (ctl.GetType().ToString().Trim())
            {
                case "DevExpress.XtraEditors.ButtonEdit":
                    ((DevExpress.XtraEditors.ButtonEdit)ctl).Properties.Enabled = !onlyread;
                    break;
                case "DevExpress.XtraEditors.TimeEdit":
                    ((DevExpress.XtraEditors.TimeEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "DevExpress.XtraEditors.SimpleButton":
                    ((DevExpress.XtraEditors.SimpleButton)ctl).Enabled = !onlyread;
                    break;
                case "DevExpress.XtraEditors.CheckedListBoxControl":
                    ((DevExpress.XtraEditors.CheckedListBoxControl)ctl).Enabled = !onlyread;
                    break;
                case "DevExpress.XtraEditors.MemoEdit":
                    ((DevExpress.XtraEditors.MemoEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "DevExpress.XtraEditors.LookUpEdit":
                    ((DevExpress.XtraEditors.LookUpEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    ((DevExpress.XtraEditors.DateEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "DevExpress.XtraEditors.CalcEdit":
                    ((DevExpress.XtraEditors.CalcEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "DevExpress.XtraEditors.CheckEdit":
                    ((DevExpress.XtraEditors.CheckEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "DevExpress.XtraEditors.TextEdit":
                    ((DevExpress.XtraEditors.TextEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "DevExpress.XtraEditors.SpinEdit":
                    ((DevExpress.XtraEditors.SpinEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
                case "System.Windows.Forms.RadioButton":
                    ((System.Windows.Forms.RadioButton)ctl).Enabled = !onlyread;
                    break;
                case "DevExpress.XtraEditors.RadioGroup":
                    ((DevExpress.XtraEditors.RadioGroup)ctl).Properties.ReadOnly = onlyread;

                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    ((DevExpress.XtraEditors.ComboBoxEdit)ctl).Properties.ReadOnly = onlyread;
                    break;
            }

            if (ctl.Controls != null)
            {
                foreach (Control _ctlSub in ctl.Controls)
                {
                    //SetReadOnlyControl
                    SetFormReadOnly(_ctlSub, onlyread);
                }
            }
        }

        /// <summary>
        /// 设定窗体控件的焦点
        /// </summary>
        /// <param name="frmStance">控件实例</param>
        public static void SetContorlFocus(Control ctl)
        {
            if (ctl == null) return;
            if (!ctl.Focused)
                 ctl.Focus();

            switch (ctl.GetType().ToString().Trim())
            {
                case "DevExpress.XtraEditors.TextEdit":
                    ((DevExpress.XtraEditors.TextEdit)ctl).SelectAll();
                    break;
                case "DevExpress.XtraEditors.SpinEdit":
                    ((DevExpress.XtraEditors.SpinEdit)ctl).SelectAll();
                    break;
                 case "DevExpress.XtraEditors.ButtonEdit":

                    ((DevExpress.XtraEditors.ButtonEdit)ctl).SelectAll();

                    break;
                case "DevExpress.XtraEditors.TimeEdit":

                    ((DevExpress.XtraEditors.TimeEdit)ctl).SelectAll();
                    break;

                case "DevExpress.XtraEditors.CheckedListBoxControl":

                    ((DevExpress.XtraEditors.CheckedListBoxControl)ctl).SelectAll();
                    break;

                case "DevExpress.XtraEditors.MemoEdit":
                    ((DevExpress.XtraEditors.MemoEdit)ctl).SelectAll();
                    break;

                case "DevExpress.XtraEditors.LookUpEdit":
                    ((DevExpress.XtraEditors.LookUpEdit)ctl).SelectAll();
                    break;

                case "DevExpress.XtraEditors.DateEdit":
                    ((DevExpress.XtraEditors.DateEdit)ctl).SelectAll();
                    break;

                case "DevExpress.XtraEditors.CalcEdit":
                    ((DevExpress.XtraEditors.CalcEdit)ctl).SelectAll();
                    break;

                case "DevExpress.XtraEditors.CheckEdit":
                    ((DevExpress.XtraEditors.CheckEdit)ctl).SelectAll();
                    break;
                case "DevExpress.XtraEditors.RadioGroup":
                    ((DevExpress.XtraEditors.RadioGroup)ctl).SelectAll();

                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    ((DevExpress.XtraEditors.ComboBoxEdit)ctl).SelectAll();
                    break;
            }
        }

        /// <summary>
        /// 清空窗体中的控件
        /// </summary>
        /// <param name="frmStance">画面对象</param>
        public static void ClsFormControl(System.Windows.Forms.Form frmStance)
        {
            foreach (System.Windows.Forms.Control trl in frmStance.Controls)
            {
                ClsControl(trl);

            }
        }

        /// <summary>
        /// 清除窗体控件内容
        /// </summary>
        /// <param name="trl">父控件对蟻E/param>
        private static void ClsControl(Control trl)
        {
            switch (trl.GetType().ToString().Trim())
            {
                case "DevExpress.XtraEditors.GroupControl":
                    if (trl.HasChildren)
                        foreach (Control childControl in trl.Controls)
                        {
                            ClsControl(childControl);
                        }
                    break;

                case "DevExpress.XtraLayout.LayoutControl":
                    if (trl.HasChildren)
                        foreach (Control childControl in trl.Controls)
                        {
                            ClsControl(childControl);
                        }

                    break;

                case "DevExpress.XtraEditors.XtraScrollableControl":
                    if (trl.HasChildren)
                        foreach (Control childControl in trl.Controls)
                        {
                            ClsControl(childControl);
                        }

                    break;
                case "DevExpress.XtraEditors.ComboBoxEdit":
                    trl.Text = "";
                    break;
                case "DevExpress.XtraEditors.ButtonEdit":
                    trl.Text = "";
                    break;
                case "DevExpress.XtraEditors.MemoEdit":
                    trl.Text = "";
                    break;
                case "DevExpress.XtraEditors.LookUpEdit":
                    ((DevExpress.XtraEditors.LookUpEdit)trl).EditValue = null;
                    //((DevExpress.XtraEditors.LookUpEdit)trl).Text = "";
                    //((DevExpress.XtraEditors.LookUpEdit)trl).Properties.ShowHeader = false;
                    break;
                case "DevExpress.XtraEditors.DateEdit":
                    ((DevExpress.XtraEditors.DateEdit)trl).EditValue = null;
                    break;
                case "DevExpress.XtraEditors.CalcEdit":
                    ((DevExpress.XtraEditors.CalcEdit)trl).Value = 0;
                    //((DevExpress.XtraEditors.CalcEdit)trl).Text ="0";
                    break;
                case "DevExpress.XtraEditors.CheckEdit":
                    ((DevExpress.XtraEditors.CheckEdit)trl).Checked = false;
                    break;
                case "DevExpress.XtraEditors.TextEdit":
                    ((DevExpress.XtraEditors.TextEdit)trl).Text = "";
                    break;

                case "DevExpress.XtraEditors.SpinEdit":
                    ((DevExpress.XtraEditors.SpinEdit)trl).Text = "";
                    break;

                case "DevExpress.XtraEditors.CheckedListBoxControl":
                    try
                    {
                        ((DevExpress.XtraEditors.CheckedListBoxControl)trl).BeginUpdate();

                        int i = 0;
                        while (((DevExpress.XtraEditors.CheckedListBoxControl)trl).GetItem(i) != null)
                        {
                            ((DevExpress.XtraEditors.CheckedListBoxControl)trl).SetItemCheckState(i, CheckState.Unchecked);
                            i++;
                        }
                    }
                    catch { }
                    finally
                    {
                        ((DevExpress.XtraEditors.CheckedListBoxControl)trl).EndUpdate();
                    }
                    break;
            }


        }

        #endregion

       #region 数据加密处理

        /// <summary>
        ///// 数据加密处理
        ///// </summary>
        ///// <param name="_password"></param>
        ///// <returns></returns>
        //public static string EncryptPassWord(string _password)
        //{
        //    return (new Security(Security.CryptoTypes.encTypeDES)).Encrypt(_password);
        //}

        ///// <summary>
        ///// 数据解密处理
        ///// </summary>
        ///// <param name="_password"></param>
        ///// <returns></returns>
        //public static string DecryptPassWord(string _password)
        //{
        //    return (new Security(Security.CryptoTypes.encTypeDES)).Decrypt(_password);
        //}

        ///// <summary>
        ///// 数据加密处理(用户密码)
        ///// </summary>
        ///// <param name="_password"></param>
        ///// <returns></returns>
        //public static string UserEncryptPassWord(string _password)
        //{
        //    return Encryption.Encode(_password);
        //}

        ///// <summary>
        ///// 数据解密处理(用户密码)
        ///// </summary>
        ///// <param name="_password"></param>
        ///// <returns></returns>
        //public static string UserDecryptPassWord(string _password)
        //{
        //    return Encryption.Decode(_password);
        //}

        #endregion

       #region 获取Guid处理

        /// <summary>
        /// 获取Guid处理
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            return System .Guid .NewGuid().ToString ().Replace ("-","").ToUpper ();
        }

       #endregion

       #region 获取日期时间
       /// <summary>
       /// 返回起始时间(默认当前时间减一天)
       /// </summary>
       /// <returns></returns>
        public static DateTime GetStartDate()
        {
            return DateTime.Now.AddDays(-1);
        }
        /// <summary>
        /// 返回终止时间(默认当前时间加一天)
        /// </summary>
        /// <returns></returns>
        public static DateTime GetEndDate()
        {
            return DateTime.Now.AddDays(1);
        }


       #endregion


       // /// <summary>
       ///// 连接数据縼E
       ///// </summary>
       // public static void ConnectData()
       // {
       //     try
       //     {
       //         Cursor currentCursor = Cursor.Current;
       //         Cursor.Current = Cursors.WaitCursor;
       //         //RFIDDeliverySystem.Iaccessdb MyDBAccess = RFIDDeliverySystem.dbFactory.CreateObject(Common.DataSourceType.SQLSERVER);
       //         ////MyDBAccess.iniParameter(Common.sysParameter.ServerName, Common.sysParameter.DataBaseName, Common.sysParameter.UserName, Common.sysParameter.PassWord);
       //         //Cursor.Current = currentCursor;
       //         //if (MyDBAccess.ConnectDB())
       //         //{
       //         //    if (Common._its_accessdb == null)
       //         //    {
       //         //        Common._its_accessdb = MyDBAccess;
       //         //    }
       //         //    //"TPiCS DataBase Connect Successful!"
       //         //    XtraMsgBox.Show(Common.GetLanguageWord("MESSAGE", "M0030"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         //    return;
       //         //}
       //         //else
       //         //{
       //         //    //"TPiCS DataBase Connect failed!"
       //         //    XtraMsgBox.Show(Common.GetLanguageWord("MESSAGE", "M0031"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         //    return;
       //         //}

       //     }
       //     catch (Exception ex)
       //     {
       //         XtraMsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //     }
       // }

       // /// <summary>
       // /// 紒E槭荼裰惺欠翊嬖谑?
       // /// </summary>
       // /// <param name="viewt"></param>
       // /// <returns></returns>
       // public static Boolean CheckExistsData(DevExpress.XtraGrid.Views.Grid.GridView viewt)
       // {
       //     if (viewt.RowCount != 0)
       //         return true;
       //     else
       //     {
       //         //"No data in ListGrid!"
       //         //XtraMsgBox.Show(Common.GetLanguageWord("MESSAGE", "M0069"), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
       //         return false;
       //     }
       // }

       ///// <summary>
       ///// 竵E率荼裰械男?
       ///// </summary>
       ///// <param name="viewt"></param>
       // public static void UpdateListView(DevExpress.XtraGrid.Views.Grid.GridView viewt)
       // {
       //     if (viewt.State == DevExpress.XtraGrid.Views.Grid.GridState.Editing)
       //     {
       //         Decimal bol = (Decimal)viewt.EditingValue;
       //         viewt.SetFocusedValue(bol);
       //         viewt.UpdateCurrentRow();
       //     }
       // }

       ///// <summary>
       ///// 加密
       ///// </summary>
       ///// <param name="_password"></param>
       ///// <returns></returns>
       // //public static string EncryptPassWord(string _password)
       // //{
       // //    //return (new security(security.CryptoTypes.encTypeDES)).Encrypt(_password);
       // //}

       ///// <summary>
       ///// 解密
       ///// </summary>
       ///// <param name="_password"></param>
       ///// <returns></returns>
       // //public static string DecryptPassWord(string _password)
       // //{
       // //    //return (new security(security.CryptoTypes.encTypeDES)).Decrypt(_password);
       // //}

       // /// <summary>
       // /// 显示表格数据处理
       // /// </summary>
       // /// <param name="dt">sql语句</param>
       // /// <param name="ListGrid">表格对象</param>
       // /// <param name="ListView">数据视图</param>
       // /// <param name="mode">设定第一列是否为只读,true为可以修改</param>
       // public static void SubShowList(DataTable  dt, DevExpress.XtraGrid.GridControl ListGrid, DevExpress.XtraGrid.Views.Grid.GridView ListView, bool mode)
       // {

       //     //ListView.Columns.Clear();
        
       //     ListGrid.DataSource = dt.DefaultView;
       //     for (int col = 0; col < ListView.Columns.Count; col++)
       //     {
       //         ListView.Columns[col].OptionsColumn.AllowEdit = false;
       //         ListView.Columns[col].OptionsColumn.AllowFocus = false;
       //         ListView.Columns[col].Name = dt.Columns[col].ColumnName.ToString();

       //     }
       //     if (mode)
       //     {
       //         ListView.Columns[0].OptionsColumn.AllowEdit = true;
       //         ListView.Columns[0].OptionsColumn.AllowFocus = true;
       //     }

       //     //if (System.IO.File.Exists(sysParameter.LayOutPath + "\\" + Application.ProductName + "." + ListView.GridControl.Parent.Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
       //     //{
       //     //    ReStoreLayOut(ListView);
       //     //}
       // }

       ///// <summary>
       ///// 显示表格数据处理
       ///// </summary>
       ///// <param name="strSql">sql语句</param>
       ///// <param name="ListGrid">表格对象</param>
       ///// <param name="ListView">数据视图</param>
       ///// <param name="mode">设定第一列是否为只读,true为可以修改</param>
       // public static void SubShowList(string strSql, DevExpress.XtraGrid.GridControl ListGrid, DevExpress.XtraGrid.Views.Grid.GridView ListView, bool mode)
       // {
       //     ListView.Columns.Clear();
       //     //DataTable dt = Common._its_accessdb.GetRecord(strSql).ADODataRst;
            
       //     //ListGrid.DataSource = dt.DefaultView;
       //     //for (int col = 0; col < ListView.Columns.Count; col++)
       //     //{
       //     //    ListView.Columns[col].OptionsColumn.AllowEdit = false;
       //     //    ListView.Columns[col].OptionsColumn.AllowFocus = false;
       //     //    ListView.Columns[col].Name = dt.Columns[col].ColumnName.ToString();

       //     //}
       //     //if (mode)
       //     //{
       //     //    ListView.Columns[0].OptionsColumn.AllowEdit = true;
       //     //    ListView.Columns[0].OptionsColumn.AllowFocus = true;
       //     //}
            
       //     //if (System.IO.File.Exists(sysParameter.LayOutPath + "\\" + Application.ProductName + "." + ListView.GridControl.Parent.Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
       //     //{
       //     //    ReStoreLayOut(ListView);
       //     //}
            
       // }

       ///// <summary>
       // /// 显示表格数据处理
       ///// </summary>
       ///// <param name="strSql">sql语句</param>
       ///// <param name="ListGrid">表格对象</param>
       ///// <param name="ListView">数据数据视图</param>
       ///// <param name="mode">设定是否可以修改，true时可以修改</param>
       // public static void SubShowList(string strSql, DevExpress.XtraGrid.GridControl ListGrid, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView ListView, bool mode)
       // {
       //     //ListView.Columns.Clear();
       //     //DataTable dt = Common._its_accessdb.GetRecord(strSql).ADODataRst;
       //     //ListGrid.DataSource = dt.DefaultView ;
       //     //for (int col = 0; col < ListView.Columns.Count; col++)
       //     //{
       //     //    ListView.Columns[col].OptionsColumn.AllowEdit = false;
       //     //    ListView.Columns[col].OptionsColumn.AllowFocus = false;
       //     //    ListView.Columns[col].Name = dt.Columns[col].ColumnName.ToString();

       //     //}
       //     //if (mode)
       //     //{
       //     //    ListView.Columns[0].OptionsColumn.AllowEdit = true;
       //     //    ListView.Columns[0].OptionsColumn.AllowFocus = true;
       //     //}
       //     //ListView.BestFitColumns();
       //     //if (System.IO.File.Exists(sysParameter.LayOutPath + "\\" + Application.ProductName + "." + ListView.GridControl.Parent.Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
       //     //{
       //     //    ReStoreLayOut(ListView);
       //     //}
       // }

       ///// <summary>
       ///// 显示表格数据处理
       ///// </summary>
       ///// <param name="strSql">sql语句</param>
       ///// <param name="ListGrid">表格对象</param>
       ///// <param name="ListView">数据数据视图</param>
       // /// <param name="mode">设定第一列是否为只读,true为可以修改</param>
       ///// <param name="clearColumn"></param>
       // public static void SubShowList(string strSql, DevExpress.XtraGrid.GridControl ListGrid, DevExpress.XtraGrid.Views.Grid.GridView ListView, bool mode,bool clearColumn)
       // {
       //     //if(clearColumn)
       //     //   ListView.Columns.Clear();
       //     //DataTable dt = Common._its_accessdb.GetRecord(strSql).ADODataRst;
       //     //ListGrid.DataSource = dt.DefaultView ;
       //     //for (int col = 0; col < ListView.Columns.Count; col++)
       //     //{
       //     //    ListView.Columns[col].OptionsColumn.AllowEdit = false;
       //     //    ListView.Columns[col].OptionsColumn.AllowFocus = false;
       //     //    ListView.Columns[col].Name = dt.Columns[col].ColumnName.ToString();
       //     //}
       //     //if (mode)
       //     //{
       //     //    ListView.Columns[0].OptionsColumn.AllowEdit = true;
       //     //    ListView.Columns[0].OptionsColumn.AllowFocus = true;
       //     //}
       //     //ListView.BestFitColumns();
       //     //if (System.IO.File.Exists(sysParameter.LayOutPath + "\\" + Application.ProductName + "." + ListView.GridControl.Parent.Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
       //     //{
       //     //    ReStoreLayOut(ListView);
       //     //}
       // }

       ///// <summary>
       ///// 显示表格数据处理
       ///// </summary>
       ///// <param name="dt">dtatable对蟻E/param>
       ///// <param name="ListGrid">表格对象</param>
       ///// <param name="ListView">数据数据视图</param>
       ///// <param name="mode">设定所有列是否只读，true为可以修改</param>
       // public static void SubShowList(DataTable dt, DevExpress.XtraGrid.GridControl ListGrid, DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView ListView, bool mode)
       // {
       //     ListView.Columns.Clear();
       //     ListGrid.DataSource = dt.DefaultView ;
       //     for (int col = 0; col < ListView.Columns.Count; col++)
       //     {
       //         ListView.Columns[col].OptionsColumn.AllowEdit = mode;
       //         ListView.Columns[col].OptionsColumn.AllowFocus = mode;
       //         ListView.Columns[col].Name = dt.Columns[col].ColumnName.ToString().ToLower();
       //     }
       //     //if (mode)
       //     //{
       //     //    ListView.Columns[0].OptionsColumn.AllowEdit = true;
       //     //    ListView.Columns[0].OptionsColumn.AllowFocus = true;
       //     //}
       //     //ListView.BestFitColumns();
       // }

       ///// <summary>
       // /// 显示表格数据处理
       ///// </summary>
       // /// <param name="dt">dtatable对蟻E/param>
       // /// <param name="ListGrid">表格对象</param>
       // /// <param name="ListView">数据数据视图</param>
       // /// <param name="mode">设定第一列是否为只读,true为可以修改</param>
       ///// <param name="clearColumn">是否事先清除所有列，true为清除</param>
       // public static void SubShowList(DataTable dt, DevExpress.XtraGrid.GridControl ListGrid, DevExpress.XtraGrid.Views.Grid.GridView ListView, bool mode,bool clearColumn)
       // {
       //     if(clearColumn)
       //        ListView.Columns.Clear();
       //     //DataTable dt = Common._its_accessdb.GetRecord(strSql).ADODataRst;
       //     ListGrid.DataSource = dt.DefaultView ;
       //     for (int col = 0; col < ListView.Columns.Count; col++)
       //     {
       //         ListView.Columns[col].OptionsColumn.AllowEdit = false;
       //         ListView.Columns[col].OptionsColumn.AllowFocus = false;
       //         ListView.Columns[col].Name = dt.Columns[col].ColumnName.ToString();
       //     }
       //     if (mode)
       //     {
       //         ListView.Columns[0].OptionsColumn.AllowEdit = true;
       //         ListView.Columns[0].OptionsColumn.AllowFocus = true;
       //     }
       //     //ListView.BestFitColumns();
       // }

       // /// <summary>
       // /// 生成Insert觼E丒
       // /// </summary>
       // /// <param name="dt">DataTable对蟻E/param>
       // /// <param name="dr">数据行对蟻E/param>
       // /// <param name="strTableName">要写葋E氖荼?/param>
       // /// <returns>返回一个Insert觼E丒/returns>
       // public static string subCreateInsertSql(DataTable dt, DataRow dr, string strTableName)
       // {
       //     //string strExists = "";
       //     ////生成insert 字段列眮E
       //     //string strSql = "insert into " + strTableName + "(";
       //     //string strField = "";
       //     //string strValue = "";
       //     ////生成values字段列眮E
       //     ////DataTable dtBom = Common._its_accessdb.GetRecord("select * from " + strTableName + " where 1=2").ADODataRst;

       //     //for (int i = 0; i < dt.Columns.Count; i++)
       //     //{

       //     //    strExists = "";
       //     //    for (int j = 0; j < dtBom.Columns.Count; j++)
       //     //    {
       //     //        if (dt.Columns[i].Caption.ToString().ToLower() == dtBom.Columns[j].Caption.ToString().ToLower())
       //     //        {
       //     //            strExists = dtBom.Columns[j].DataType.ToString();
       //     //            break;
       //     //        }
       //     //    }
       //     //    if (strExists != "")
       //     //    {
       //     //        strField += dt.Columns[i].Caption.Trim() + ",";
       //     //        strValue += (strExists == "System.String" ? "'" + dr[i].ToString() + "'," : dr[i].ToString() + ",");
       //     //    }
       //     //}
       //     ////strField = strField.Substring(0, strField.Length - 1);
       //     ////strValue = strValue.Substring(0, strValue.Length - 1);
       //     ////strSql += strField + ") values(" + strValue
       //     ////    + Convert.ToString(Common.sysParameter.DataSourceType.ToString() == Common.DataSourceType.ORACLE.ToString() ? ");" : ") ");
       //     //return strSql;
       //     return "";

       // }

       

       // //public static void subShowReport(string ReportName,string billNo,string CreateMan)
       // //{
       // //    //Common.report.LoadReportFromFile( sysParameter.ReportPath + "\\" + ReportName);
       // //    //FastReport.TfrxADODatabase database = new FastReport.TfrxADODatabase();
       // //    //database = (FastReport.TfrxADODatabase)report.FindObject("ADODatabase1");
       // //    ////"Provider=SQLOLEDB.1;Password=iamsa;Persist Security Info=True;User ID=sa;Initial Catalog=RFIDKeySystemtest;Data Source=wfpby;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=WFPBY;Use Encryption for Data=False;Tag with column collation when possible=False";
       // //    ////database.ConnectionString="Provider=sqloledb;Data Source=Aron1;Initial Catalog=pubs;User Id=sa;Password=asdasd;"
       // //    //string strConnection = "Provider=SQLOLEDB.1;"
       // //    //                         + "Password=" + Common.sysParameter.PassWord.Trim() + ";"
       // //    //                         + "Persist Security Info=True;User ID=" + Common.sysParameter.UserName.Trim() + ";"
       // //    //                         + "Initial Catalog=" + Common.sysParameter.DataBaseName.Trim() + ";"
       // //    //                         + "Data Source=" + Common.sysParameter.ServerName.Trim().ToLower() + ";"
       // //    //                         + "Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;"
       // //    //                         + "Workstation ID=" + Common.sysParameter.ServerName.Trim().ToUpper() + ";"
       // //    //                         + "Use Encryption for Data=False;Tag with column collation when possible=False";
       // //    //database.ConnectionString = strConnection;
       // //    //database.Connected = true;

       // //    ////report.SetVariable("ConnectionString", strConnection);        //增加连接字符串参数)
       // //    //Common.report.SetVariable("billno", billNo);
       // //    //Common.report.SetVariable("CreateMan", CreateMan);
       // //    ////string result = "";
       // //    ////string connectionString = ConfigurationManager.AppSettings["connectionStringReport"];
       // //    ////_frx.SetVariable("connectionstring", connectionString);//设置连接字符串参数
       // //    ////report.SetVariable("ConnectionString", "'Provider=SQLOLEDB.1;Password=iamsa;Persist Security Info=True;User ID=sa;Initial Catalog=RFIDDeliverySystem;Data Source=WFPBY;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=WFPBY;Use Encryption for Data=False;Tag with column collation when possible=False'");        //增加连接字符串参数
       // //    ////report.SetVariable("ConnectionString", "'Provider=SQLOLEDB.1;Password=iamsa;Persist Security Info=True;User ID=sa;Initial Catalog=RFIDDeliverySystem;Data Source=wfpby;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=WFPBY;Use Encryption for Data=False;Tag with column collation when possible=False'");        //增加连接字符串参数
       // //    //Common.report.ShowReport();
       // //}





       ///// <summary>
       ///// 将数据柄择中的一行数据显示在输葋E丶?
       ///// </summary>
       ///// <param name="dr">行对蟻E/param>
       ///// <param name="ctl">父控件对蟻E/param>
       // public static void SetGridData(DataRow dr, Control ctl)
       // {
       //     foreach (Control child in ctl.Controls)
       //     {
       //         //if (child.Tag != null)
       //         for (int i = 0; i < dr.Table.Columns.Count; i++)
       //         {
       //             if (child.Tag != null && child.Tag.ToString().ToLower() == dr.Table.Columns[i].ColumnName.ToLower())
       //                 switch (child.GetType().ToString())
       //                 {
       //                     case "DevExpress.XtraEditors.ButtonEdit":
       //                     case "DevExpress.XtraEditors.TextEdit":
       //                     case "DevExpress.XtraEditors.MemoEdit":
       //                         child.Text = dr[i].ToString();
       //                         break;
       //                     case "DevExpress.XtraEditors.ComboBoxEdit":

       //                         if (((DevExpress.XtraEditors.ComboBoxEdit)child).Properties.Tag != null)
       //                         {
       //                             ((DevExpress.XtraEditors.ComboBoxEdit)child).Text = dr[i].ToString();
       //                         }
       //                         else {
       //                             ((DevExpress.XtraEditors.ComboBoxEdit)child).SelectedIndex = Convert.ToInt32(dr[i]);
       //                         }
                                
       //                         break;
       //                     case "DevExpress.XtraEditors.CalcEdit":
       //                         ((DevExpress.XtraEditors.CalcEdit)child).Value = Convert.ToDecimal(dr[i]);
       //                         //((DevExpress.XtraEditors.CalcEdit)child).Text = Convert.ToDecimal(dr[i]).ToString("#,##0.00%");
       //                         break;
       //                     case "DevExpress.XtraEditors.CheckEdit":
       //                         ((DevExpress.XtraEditors.CheckEdit)child).Checked = Convert.ToInt32(dr[i]) == 1 ? true : false;
       //                         //((DevExpress.XtraEditors.CalcEdit)child).Text = Convert.ToDecimal(dr[i]).ToString("#,##0.00%");
       //                         break;
       //                     case "DevExpress.XtraEditors.DateEdit":
       //                         ((DevExpress.XtraEditors.DateEdit)child).EditValue  = dr[i]==null?null:dr[i].ToString();
       //                         break;
       //                     case "DevExpress.XtraEditors.LookUpEdit":
       //                         ((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never;
       //                         //((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = false;
       //                         ((DevExpress.XtraEditors.LookUpEdit)child).Text = dr[i].ToString();
       //                         ((DevExpress.XtraEditors.LookUpEdit)child).EditValue = dr[i].ToString();
       //                         ((DevExpress.XtraEditors.LookUpEdit)child).Properties.ReadOnly = true;
       //                         ((DevExpress.XtraEditors.LookUpEdit)child).Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick;
       //                         break;
       //                     case "DevExpress.XtraEditors.RadioGroup":
       //                         ((DevExpress.XtraEditors.RadioGroup)child).SelectedIndex = Convert.ToInt32(dr["status"]);
       //                         break;
       //                 }

       //         }
       //     }
       // }



       // /// <summary>
       // ///紒E槟臣ピ裎眨蛳允敬笮畔?
       // /// </summary>
       // /// <param name="view">数据视图</param>
       // /// <param name="colFieldName">列的字段名称</param>
       // /// <param name="MessageId">显示信息ID号，可以通Ini文件取得</param>
       // /// <param name="e">事件参数</param>
       // /// <returns>返回是否有代陙E/returns>
       // public static bool  subSetColumnError(GridView view, string colFieldName, string MessageId, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
       // {
       //     //if (view.GetFocusedRowCellValue(view.Columns[colFieldName]).ToString() == "")
       //     //{
       //     //    view.SetColumnError(view.Columns[colFieldName], Common.GetLanguageWord("ValidMessage", MessageId), DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information);
       //     //    e.Valid = false;
       //     //    return false;
       //     //}
       //     //else
       //     //{
       //     //    view.SetColumnError(view.Columns[colFieldName], "");
       //     //    e.Valid = true;
       //     return true;
       //     //}
          
       // }

       // public static bool subSetColumnErrorZero(GridView view, int colIndex, string MessageId, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
       // {
       //     //if ((decimal)view.GetFocusedRowCellValue(view.Columns[colIndex]) <= 0)
       //     //{
       //     //    view.SetColumnError(view.Columns[colIndex], Common.GetLanguageWord("ValidMessage", MessageId), DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information);
       //     //    e.Valid = false;
       //     //    return false;
       //     //}
       //     //else
       //     //{
       //     //    view.SetColumnError(view.Columns[colIndex], "");
       //     //    e.Valid = true;
       //     return true;
       //     //}
          

       // }


       ///// <summary>
       ///// 清除代牦提示
       ///// </summary>
       ///// <param name="view">数据视图</param>
       ///// <param name="colIndex">列索引</param>
       // /// <param name="e">LIstViewCellChanged事件发生参数</param>
       // public static void subClearColumnError(GridView view, int colIndex, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
       // {
       //     if (e.Column.Equals(view.Columns[colIndex]) && e.Value.ToString() != "")
       //     {
       //         view.SetColumnError(view.Columns[colIndex], "");
       //     }
       // }

       ///// <summary>
       ///// 用于cellchanged事件验证
       ///// </summary>
       ///// <param name="view"></param>
       ///// <param name="fieldName"></param>
       ///// <param name="MessageId"></param>
       ///// <param name="e"></param>
       ///// <returns></returns>
       // public static void subSetColumnError(GridView view, string fieldName, string MessageId, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
       // {
       //     //if (e.Column.FieldName.ToLower() == fieldName.ToLower())
       //     //    if ((string)e.Value != "")
       //     //        view.SetColumnError(e.Column, "");

       //     //    else
       //             //view.SetColumnError(e.Column, Common.GetLanguageWord("ValidMessage", MessageId), DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information);

       // }


       // /// <summary>
       // /// 用于cellchanged事件验证
       // /// </summary>
       // /// <param name="view"></param>
       // /// <param name="fieldName"></param>
       // /// <param name="MessageId"></param>
       // /// <param name="e"></param>
       // /// <returns></returns>
       // public static void subSetColumnErrorZero(GridView view, string fieldName, string MessageId, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
       // {
       //     //if (e.Column.FieldName.ToLower() == fieldName.ToLower())
       //     //    if ((decimal)e.Value>0)
       //     //        view.SetColumnError(e.Column, "");

       //     //    else
       //     //        view.SetColumnError(e.Column, Common.GetLanguageWord("ValidMessage", MessageId), DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information);

       // }



       // public static void setCheckListBox(string sql, DevExpress.XtraEditors.CheckedListBoxControl lue)
       // {

       //     DataTable dt = new DataTable();
       //     //dt = Common._its_accessdb.GetRecord(sql).ADODataRst;

       //     //if (dt == null)
       //     //    return;

       //     lue.DataSource = dt;
       // }

       // public static void SetDataSource(string formName, Control ctl)
       // {
       //     //string sql;
       //     //foreach (Control child in ctl.Controls)
       //     //{
       //     //    if (child.GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
       //     //    {
       //     //        rwconfig rw = new rwconfig(Common.GetSolutionPath(Application.StartupPath) + @"SqlDataSource\SQL.INI");
       //     //        sql = rw.ReadTextFile(formName, child.Name);
       //     //        //((DevExpress.XtraEditors.LookUpEdit)child).Enabled = false;
                   
       //     //        ((DevExpress.XtraEditors.LookUpEdit)child).Properties.DataSource = Common._its_accessdb.GetRecord(sql).ADODataRst.DefaultView;
       //     //        ((DevExpress.XtraEditors.LookUpEdit)child).Properties.PopupWidth = 250;
       //     //        ((DevExpress.XtraEditors.LookUpEdit)child).Properties.DropDownRows = 12;
       //     //        //((DevExpress.XtraEditors.LookUpEdit)child).Enabled = true;
                   
       //     //    }
       //     //}
       // }


       // //public static string GetMaxNoteNo(string tableType)
       // //{
       // //    int iniItem = 1000000;
       // //    string strSql = "select maxno from maxnoteno where [type]='" + tableType + "'";
       // //    int maxno = Convert.ToInt32(Common._its_accessdb.GetRecord(strSql).ADODataRst.Rows[0][0]);
       // //    int maxnoteno = iniItem + maxno + 1;
       // //    return maxnoteno.ToString().Substring(1, maxnoteno.ToString().Length - 1);
       // //}
       ///// <summary>
       ///// 通过关联按钮取得主从柄炫息
       ///// </summary>
       ///// <param name="edit">按钮文本縼E/param>
       ///// <param name="masterView">主柄搃ew</param>
       ///// <param name="detailView">从柄搃ew</param>
       ///// <param name="frmMain">窗虂E/param>
       // public static void SelectBasicData(DevExpress.XtraEditors.ButtonEdit edit, string masterView, string detailView, System.Windows.Forms.Form frmMain)
       // {
            
       //     frmMain.Tag = null;
       //     //frmSearchBasic frmStance = new frmSearchBasic(masterView, detailView, frmMain, edit.Name);
       //     //frmStance.ShowDialog();
       //     if (frmMain.Tag != null)
       //     {
       //         edit.Text = ((DataRow)frmMain.Tag)[0].ToString();
       //     }
            
       // }

       // /// <summary>
       // /// 通过关联按钮取得主从柄炫息
       // /// </summary>
       // /// <param name="edit">按钮文本縼E/param>
       // /// <param name="masterView">主柄搃ew</param>
       // /// <param name="detailView">从柄搃ew</param>
       // /// <param name="frmMain">窗虂E/param>
       // public static string SelectBasicData( string masterView, string detailView, System.Windows.Forms.Form frmMain)
       // {

       //     //frmMain.Tag = null;
       //     ////frmSearchBasic frmStance = new frmSearchBasic(masterView, detailView, frmMain, "Insert");
       //     //frmStance.ShowDialog();
       //     //if (frmMain.Tag != null)
       //     //{
       //     //    return ((DataRow)frmMain.Tag)["itemid"].ToString();
       //     //}
       //     return null;

       // }



       //public static void SetGridColumnAllowFocus(GridView view,int[] obj,bool allowFocus)
       // {
       //     for (int i = 0; i < view.Columns.Count; i++)
       //     {
       //         for (int j = 0; j < obj.Length; j++)
       //         {
       //             if (i == Convert.ToInt32(obj[j]))
       //             {
       //                 view.Columns[i].OptionsColumn.AllowFocus = allowFocus;
       //             }
       //         }
       //     }
       // }

       ///// <summary>
       ///// 设定%格式
       ///// </summary>
       ///// <param name="view"></param>
       ///// <param name="colIndex"></param>
       //public static void SetGridColumnPercentFormat(GridView view,int[] colIndex)
       // {
       //     for (int i = 0; i < colIndex.Length; i++)
       //     {
       //         view.Columns[colIndex[i]].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //         view.Columns[colIndex[i]].DisplayFormat.FormatString = "#,##0.00%";
       //     }
         
       // }

       ///// <summary>
       ///// 设定%格式
       ///// </summary>
       ///// <param name="view"></param>
       ///// <param name="colIndex"></param>
       //public static void SetGridColumnPercentFormat(DevExpress.XtraGrid.Columns.GridColumn[] column)
       //{
       //    for (int i = 0; i < column.Length; i++)
       //    {
       //        column[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //        column[i].DisplayFormat.FormatString = "#,##0.00%";
       //    }

       //}

       // public static void SetGridFootSum(GridView view, int colIndex )
       // {
       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem();
       //     view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     view.Columns[colIndex].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
       //     view.Columns[colIndex].SummaryItem.DisplayFormat = "{0:N2}";
       //     //view.Columns[colIndex].SummaryItem = sumItem;


       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem(view.Columns[colIndex]);
       //     //view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     //view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     //sumItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
       //     //sumItem.DisplayFormat = "{0:N2}";
       //     //sumItem.Tag = view.Columns[colIndex].FieldName;
       //     ////sumItem.SummaryItem = sumItem;
       // }

       // public static void SetGridFootPercentFormat(DevExpress.XtraGrid.Columns.GridColumn[] column)
       // {
       //     for (int i = 0; i < column.Length; i++)
       //     {
       //         column[i].SummaryItem.DisplayFormat = "{0:#,##0.00%}";
       //     }

       // }


       // public static void SetGridFootMin(GridView view, int colIndex)
       // {
       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem();
       //     view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     view.Columns[colIndex].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min;
       //     view.Columns[colIndex].SummaryItem.DisplayFormat = "{0:N2}";
       //     //view.Columns[colIndex].SummaryItem = sumItem;


       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem(view.Columns[colIndex]);
       //     //view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     //view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     //sumItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
       //     //sumItem.DisplayFormat = "{0:N2}";
       //     //sumItem.Tag = view.Columns[colIndex].FieldName;
       //     ////sumItem.SummaryItem = sumItem;
       // }

       // public static void SetGridFootMin(GridView view, int colIndex,string formatString)
       // {
       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem();
       //     view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     view.Columns[colIndex].DisplayFormat.FormatString = formatString;
       //     view.Columns[colIndex].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Min;
       //     view.Columns[colIndex].SummaryItem.DisplayFormat = "{0:#,##0.00%}";
       //     //view.Columns[colIndex].SummaryItem = sumItem;


       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem(view.Columns[colIndex]);
       //     //view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     //view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     //sumItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
       //     //sumItem.DisplayFormat = "{0:N2}";
       //     //sumItem.Tag = view.Columns[colIndex].FieldName;
       //     ////sumItem.SummaryItem = sumItem;
       // }

       // public static void SetGridFootMax(GridView view, int colIndex)
       // {
       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem();
       //     view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     view.Columns[colIndex].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
       //     view.Columns[colIndex].SummaryItem.DisplayFormat = "{0:N2}";
       //     //view.Columns[colIndex].SummaryItem = sumItem;


       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem(view.Columns[colIndex]);
       //     //view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     //view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     //sumItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
       //     //sumItem.DisplayFormat = "{0:N2}";
       //     //sumItem.Tag = view.Columns[colIndex].FieldName;
       //     ////sumItem.SummaryItem = sumItem;
       // }

       // public static void SetGridFootMax(GridView view, int colIndex, string formatString)
       // {
       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem();
       //     view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     view.Columns[colIndex].DisplayFormat.FormatString = formatString;
       //     view.Columns[colIndex].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Max;
       //     view.Columns[colIndex].SummaryItem.DisplayFormat = "{0:#,##0.00%}";
       //     //view.Columns[colIndex].SummaryItem = sumItem;


       //     //DevExpress.XtraGrid.GridColumnSummaryItem sumItem = new GridColumnSummaryItem(view.Columns[colIndex]);
       //     //view.Columns[colIndex].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //     //view.Columns[colIndex].DisplayFormat.FormatString = "#,##0.00";
       //     //sumItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
       //     //sumItem.DisplayFormat = "{0:N2}";
       //     //sumItem.Tag = view.Columns[colIndex].FieldName;
       //     ////sumItem.SummaryItem = sumItem;
       // }

       // public static void SetGridNumericFormat(DevExpress.XtraGrid.Views.Grid.GridView ListView, int[] ColCount,int[] DecimalCount)
       // {
       //     for (int i = 0; i < ColCount.Length; i++)
       //     {
       //         ListView.Columns[ColCount[i]].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //         ListView.Columns[ColCount[i]].DisplayFormat.FormatString = "n" + DecimalCount[i].ToString();
       //     }
           
       // }

       // public static void SetGridNumericFormat(DevExpress.XtraGrid.Columns.GridColumn[] column)
       // {
       //     for (int i = 0; i < column.Length; i++)
       //     {
       //         column[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
       //         column[i].DisplayFormat.FormatString = "{0:#,##0.00}";
       //     }


       // }






       // public static void SetGridNotSort(GridView view)
       // {
       //     view.ClearSorting();
       //     view.OptionsMenu.EnableColumnMenu = false;
       //     view.OptionsMenu.EnableFooterMenu = false;
       //     view.OptionsMenu.EnableGroupPanelMenu = false;
       //     for (int i = 0; i < view.Columns.Count; i++)
       //     {
       //         view.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
       //     }
       // }

       // public static void subShowReport(string ReportName, string billNo, string CreateMan)
       // {
       //     //Common.report.LoadReportFromFile(sysParameter.ReportPath + "\\" + ReportName);
       //     //FastReport.TfrxADODatabase database = new FastReport.TfrxADODatabase();
       //     //database = (FastReport.TfrxADODatabase)report.FindObject("ADODatabase1");
       //     //"Provider=SQLOLEDB.1;Password=iamsa;Persist Security Info=True;User ID=sa;Initial Catalog=XGSTOCKtest;Data Source=wfpby;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=WFPBY;Use Encryption for Data=False;Tag with column collation when possible=False";
       //     //database.ConnectionString="Provider=sqloledb;Data Source=Aron1;Initial Catalog=pubs;User Id=sa;Password=asdasd;"
       //     //string strConnection = "Provider=SQLOLEDB.1;"
       //     //                         + "Password=" + Common.sysParameter.PassWord.Trim() + ";"
       //     //                         + "Persist Security Info=True;User ID=" + Common.sysParameter.UserName.Trim() + ";"
       //     //                         + "Initial Catalog=" + Common.sysParameter.DataBaseName.Trim() + ";"
       //     //                         + "Data Source=" + Common.sysParameter.ServerName.Trim().ToLower() + ";"
       //     //                         + "Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;"
       //     //                         + "Workstation ID=" + Common.sysParameter.ServerName.Trim().ToUpper() + ";"
       //     //                         + "Use Encryption for Data=False;Tag with column collation when possible=False";
       //     //string strConnection = "Provider=SQLOLEDB.1;Password=" + Common.sysParameter.PassWord.Trim()
       //     //              + ";Persist Security Info=True;  User ID=" + Common.sysParameter.UserName.Trim()
       //     //              + "; Initial Catalog=" + Common.sysParameter.DataBaseName.Trim()
       //     //              + "; Data Source=" + Common.sysParameter.ServerName.Trim().ToLower();

       //     //string strConnection = "Provider=SQLOLEDB.1;Password=iamsa;Persist Security Info=True;User ID=sa;Initial Catalog=Quo;Data Source=wfpby\\wfp2005";

       //     //database.ConnectionString = strConnection;
       //     //database.Connected = true;
       //     //report.SetVariable("ConnectionString", strConnection);        //增加连接字符串参数)
       //     //Common.report.SetVariable("billno", billNo);
       //     //Common.report.SetVariable("CreateUser", CreateMan);
       //     ////string result = "";
       //     ////string connectionString = ConfigurationManager.AppSettings["connectionStringReport"];
       //     ////_frx.SetVariable("connectionstring", connectionString);//设置连接字符串参数
       //     ////report.SetVariable("ConnectionString", "'Provider=SQLOLEDB.1;Password=iamsa;Persist Security Info=True;User ID=sa;Initial Catalog=XGSTOCK;Data Source=WFPBY;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=WFPBY;Use Encryption for Data=False;Tag with column collation when possible=False'");        //增加连接字符串参数
       //     ////report.SetVariable("ConnectionString", "'Provider=SQLOLEDB.1;Password=iamsa;Persist Security Info=True;User ID=sa;Initial Catalog=XGSTOCK;Data Source=wfpby;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=WFPBY;Use Encryption for Data=False;Tag with column collation when possible=False'");        //增加连接字符串参数
       //     //Common.report.ShowReport();
       // }

       // public static void SetFormPower(Form objForm)
       // {
       //     //int _rowindex;
       //     //string _code;
       //     //if (objForm is frmBaseToolXC)
       //     //{
       //     //    _code = objForm.Name.Replace("frm", "fun");
       //     //    //DataRow[] drTmp = Common._userpower.Select("code='" + _code + "'");
       //     //    if (drTmp.Length < 1)
       //     //    {
       //     //        ((frmBaseToolXC)objForm).NewButtonVisibility = false;
       //     //        ((frmBaseToolXC)objForm).EditButtonVisibility = false;
       //     //        ((frmBaseToolXC)objForm).DeleteButtonVisibility = false;
       //     //        ((frmBaseToolXC)objForm).PrintButtonVisibility = false;
       //     //        ((frmBaseToolXC)objForm).ImportButtonVisibility = false;
       //     //        ((frmBaseToolXC)objForm).ExcelButtonVisibility= false;
                  
       //     //    }
       //     //    else
       //     //    {
       //     //        //DataRow _dr = drTmp[0];
       //     //        //((frmBaseToolXC)objForm).NewButtonVisibility = ((int)_dr["ALLOW_ADD"] == 1) ? true : false;
       //     //        //((frmBaseToolXC)objForm).EditButtonVisibility = ((int)_dr["ALLOW_EDT"] == 1) ? true : false;
       //     //        //((frmBaseToolXC)objForm).DeleteButtonVisibility = ((int)_dr["ALLOW_DEL"] == 1) ? true : false;
       //     //        //((frmBaseToolXC)objForm).PrintButtonVisibility = ((int)_dr["ALLOW_PRN"] == 1) ? true : false;
       //     //        ////((RFIDDeliverySystem.frmBaseToolXC)objForm).ImportButtonVisible = ((int)_dr["ALLOW_IMP"] == 1) ? true : false;
       //     //        ////((RFIDDeliverySystem.frmBaseToolXC)objForm).ExportButtonVisible = ((int)_dr["ALLOW_EXP"] == 1) ? true : false;
       //     //        //((frmBaseToolXC)objForm).ExcelButtonVisibility = ((int)_dr["ALLOW_EXP"] == 1) ? true : false;
                  
       //     //    }
       //     }

       // }

       // ///// <summary>
       // ///// 判断是否是yyyyMMdd型的时紒E
       // ///// </summary>
       // ///// <param name="strDate">字符串</param>
       // ///// <returns>如果正确，则返回諄E/returns>
       // //public static bool IsDate(string strDate)
       // //{
       // //    if (Regex.IsMatch(strDate, @"^([12]\d{3})(0\d|1[0-2])([0-2]\d|3[01])$"))
       // //        return true;
       // //    else
       // //        return false;
       // //}

       // #region  判断字符串是否是正确的 IP 地址格式 public static bool IsIp(string s)

       // /// <summary> 
       // /// 判断字符串是否是正确的 IP 地址格式
       // /// </summary>
       // //public static bool IsIp(string s)
       // //{
       // //    string pattern = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$";
       // //    return Regex.IsMatch(s, pattern);
       // //}
       // #endregion

        /// <summary>
        /// 添加数据表列处理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public static DataTable setDataColumnAdd(DataTable dt, String ColumnName, String Caption, Type type)
        //{

        //    //if (null == dt)
        //    //{

        //    //    dt = new DataTable();
        //    //}
            
        //    //DataColumn dc = new DataColumn(ColumnName, type);
        //    //dc.Caption = Caption;

        //    //dt.Columns.Add(dc);

        //    return dt;
        //}

        /// <summary>
        /// 添加数据表列处理
        /// </summary>
        /// <param name="dt"></param>
        ///// <returns></returns>
        //public static DataRow setDataColumnValue( DataRow dr, String ColumnName, object value)
        //{

        //    //dr[ColumnName] = value;
        //    return dr;
         
        }
    }
