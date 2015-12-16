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
using Framework.FileOperate;
using DevExpress.XtraEditors;

namespace Framework.Libs
{
    public partial class Common
    {

        #region 配置信息处理

        /// <summary>
        /// 取得当前程序的当前路径
        /// </summary>
        /// <param name="_Path">路径字符串</param>
        /// <returns>当前路径</returns>
        public static string GetSolutionPath(string _Path)
        {
            return (_Path.Substring(_Path.Length - 1, 1) != @"\") ? _Path + @"\" : _Path;
        }

        /// <summary>
        /// 读取系统配置信息
        /// </summary>
        public static void GetIniData()
        {
            //try
            //{
            SysRun sysSetup = (SysRun)Serial.DeserializeBinary(Application.StartupPath + @"\" + Common._settingfilename);
            
            Common._sysrun = sysSetup;
            _rwconfig = new RWConfig(Common.GetSolutionPath(Application.StartupPath) + @"Language\" + Common._sysrun.Language.ToString() + ".ini");
            //RWLang = new rwconfig(@"G:\Qouter 的副本\Quoter\Quoter\bin\Debug\Language");
            //RWLang = new rwconfig(Common.GetSolutionPath(Application.StartupPath) + @"Language\chinese.ini");
            sysSetup = null;
            //}
            //catch (Exception ex)
            //{
            //    XtraMsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

        }

        #endregion

        #region 本地化窗体


        /// <summary>
        /// 对窗体的所有控件处理
        /// </summary>
        /// <param name="_frmname"></param>
        /// <param name="_ctl"></param>
        public static void GetAllControls(string _frmname, Control _ctl) 
        {

            if (_ctl is DevExpress.XtraEditors.SimpleButton)
            {

                GetTextValue(_frmname, _ctl);
            }
            else if (_ctl is DevExpress.XtraEditors.LabelControl)
            {
                GetTextValue(_frmname, _ctl);
            }

            else if (_ctl is DevExpress.XtraEditors.ButtonEdit )
            {

                ((DevExpress.XtraEditors.ButtonEdit)_ctl).Properties.Buttons[0].Caption = _ctl.Tag.ToString();
                ((DevExpress.XtraEditors.ButtonEdit)_ctl).Font = Common._sysrun.ButtonEditFont;
                ((DevExpress.XtraEditors.ButtonEdit)_ctl).Properties.Buttons[0].Appearance.Font = Common._sysrun.ButtonEditFont;
            }

            else
            {
                //获取容器控件描述
                GetContainerControl(_frmname, _ctl);
            }


            if (_ctl.Controls != null)
            {
                foreach (Control _ctlSub in _ctl.Controls)
                {
                    //对窗体的所有控件处理
                    GetAllControls(_frmname, _ctlSub);
                }
            }

        } 

        /// <summary>
        /// 获取控件描述
        /// </summary>
        /// <param name="_frmname"></param>
        /// <param name="_ctl"></param>
        public static void GetTextValue(string _frmname, Control _ctl)
        {

            string strText;

            strText = _ctl.Tag.ToString();
            _ctl.Text = strText == "" ? _ctl.Text : strText;
            _ctl.Font = Common._sysrun.ButtonEditFont;
        }
       
        /// <summary>
        /// 获取容器控件描述
        /// </summary>
        /// <param name="_frmname"></param>
        /// <param name="_ctl"></param>
        public static void GetContainerControl(string _frmname, Control _ctl)
        {
            string strCaption;

            switch (_ctl.GetType().ToString().Trim())
            {

                case "DevExpress.XtraGrid.GridControl":

                    DevExpress.XtraGrid.Views.Grid.GridView ListView = ((DevExpress.XtraGrid.Views.Grid.GridView)((DevExpress.XtraGrid.GridControl)_ctl).DefaultView);
                    if (ListView.Columns.Count > 0)
                    {
                        for (int col = 0; col < ListView.Columns.Count; col++)
                        {
                            ListView.Columns[col].Caption = ListView.Columns[col].Name;// Common.GetLanguageWord(_frmname, ListView.Columns[col].Name);
                            ListView.Columns[col].AppearanceCell.Font = Common._sysrun.GridFont;
                            ListView.Columns[col].AppearanceHeader.Font = Common._sysrun.GridFont;
                        }
                    }
                    break;


                case "System.Windows.Forms.ToolStrip":

                    ((System.Windows.Forms.ToolStrip)_ctl).Font = Common._sysrun.ButtonEditFont;
                    for (int col = 0; col < ((System.Windows.Forms.ToolStrip)(_ctl)).Items.Count; col++)
                    {
                        ((System.Windows.Forms.ToolStrip)(_ctl)).Items[col].Text = ((System.Windows.Forms.ToolStrip)(_ctl)).Items[col].Name;
                        if (((System.Windows.Forms.ToolStrip)(_ctl)).Items[col].GetType().ToString() == "System.Windows.Forms.ToolStripDropDownButton")
                        {
                            System.Windows.Forms.ToolStripDropDownButton dropButton = (System.Windows.Forms.ToolStripDropDownButton)((System.Windows.Forms.ToolStrip)(_ctl)).Items[col];
                            if (dropButton.DropDownItems.Count > 0)
                            {
                                for (int row = 0; row < dropButton.DropDownItems.Count; row++)
                                {
                                    dropButton.DropDownItems[row].Text = dropButton.DropDownItems[row].Name.ToString().Trim();// GetLanguageWord(_frmname, dropButton.DropDownItems[row].Name.ToString().Trim());
                                    dropButton.DropDownItems[row].Font = Common._sysrun.ButtonEditFont;
                                }
                            }

                        }
                    }
                    break;

                case "DevExpress.XtraBars.BarDockControl":

                    if (((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Items.Count > 0)
                    {


                        for (int index = 0; index < ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Items.Count; index++)
                        {
                            if (((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Items[index].Tag != null)
                            {
                                strCaption = ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Items[index].Name.ToString();//GetLanguageWord(_frmname, ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Items[index].Tag == null ? "" : ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Items[index].Name.ToString());
                                if (strCaption != "") ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Items[index].Caption = strCaption;
                            }
                        }

                        for (int col = 0; col < ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.RepositoryItems.Count; col++)
                        {
                            if (((DevExpress.XtraBars.BarDockControl)_ctl).Manager.RepositoryItems[col].GetType().ToString() == "DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit")
                            {
                                DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit reItem = ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.RepositoryItems[col] as DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit;
                                //如果用于oracle，要加上以下语句
                                //reItem.DisplayMember = reItem.DisplayMember.ToLower();
                                //reItem.ValueMember = reItem.ValueMember.ToLower();
                                reItem.PopupWidth = 350;
                                reItem.DropDownRows = 12;
                                reItem.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
                                if (reItem.Columns.Count >= 1)
                                {
                                    for (int i = 0; i < reItem.Columns.Count; i++)
                                    {
                                        strCaption = reItem.Columns[i].Caption;// GetLanguageWord(_frmname, reItem.Columns[i].Caption);
                                        if (strCaption != "") reItem.Columns[i].Caption = strCaption;
                                    }
                                }
                                //reItem.Columns[0].FieldName = reItem.Columns[0].FieldName.ToLower();

                            }
                        }
                        ((DevExpress.XtraBars.BarDockControl)_ctl).Appearance.Font = Common._sysrun.ButtonEditFont;
                        ((DevExpress.XtraBars.BarDockControl)_ctl).Manager.Bars[0].Appearance.Font = Common._sysrun.ButtonEditFont;  //可通过此属性取得bar的集合，很有用
                    }
                    break;

                case "DevExpress.XtraTab.XtraTabControl":

                    Font _PageFont;

                    if (((DevExpress.XtraTab.XtraTabControl)_ctl).TabPages.Count > 0)
                    {
                        for (int index = 0; index < ((DevExpress.XtraTab.XtraTabControl)_ctl).TabPages.Count; index++)
                        {
                            if (((DevExpress.XtraTab.XtraTabControl)_ctl).TabPages[index].Tag != null)
                            {

                                ((DevExpress.XtraTab.XtraTabControl)_ctl).TabPages[index].Text = ((DevExpress.XtraTab.XtraTabControl)_ctl).TabPages[index].Name.ToString();// GetLanguageWord(_frmname, ((DevExpress.XtraTab.XtraTabControl)_ctl).TabPages[index].Name.ToString());

                                _PageFont = Common._sysrun.ButtonEditFont;
                                if (_PageFont != null)
                                {
                                    _PageFont = new Font(Common._sysrun.ButtonEditFont.Name, 15.75F, FontStyle.Bold);
                                    ((DevExpress.XtraTab.XtraTabControl)_ctl).TabPages[index].Appearance.Header.Font = _PageFont;
                                }

                            }
                        }


                        ((DevExpress.XtraTab.XtraTabControl)_ctl).Font = Common._sysrun.ButtonEditFont;
                    }
                    break;


            }

        }

        #endregion

        #region 表格样式设置

        /// <summary>
        /// 初始化表格数据样式
        /// </summary>
        /// <param name="ListView">ListView对象</param>
        /// <param name="style">显示模式</param>
        public static void SetIniGridInfo(DevExpress.XtraGrid.Views.Grid.GridView ListView, enumGridStyle style)
        {

            //设定小数位数
            for (int i = 0; i < ListView.Columns.Count; i++)
            {
                //if (ListView.Columns[i].ColumnType.ToString() == "System.Decimal")
                //{
                //    //ListView.Columns[i].SummaryItem.DisplayFormat = "{0:c}";
                //    ListView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //    ListView.Columns[i].DisplayFormat.FormatString = "n4";
                //    ListView.Columns[i].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                //    ListView.Columns[i].AppearanceHeader.Options.UseTextOptions = true;
                //设置自动筛选行默认条件为包含（%条件%）
                ListView.Columns[i].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
                //如果是超级用户，则不能过滤数据表格
                if (Common._personid != Common._Administrator)
                {
                    ListView.Columns[i].OptionsFilter.AllowFilter = false;
                    ListView.OptionsFilter.AllowColumnMRUFilterList = false;
                    ListView.OptionsFilter.AllowFilterEditor = false;
                    ListView.OptionsFilter.AllowMRUFilterList = false;
                }
                //}
            }

            //Common._sysrun.CurRowBackcolor = Color.WhiteSmoke;

            ListView.IndicatorWidth = 40;

            switch (style)
            {

                case enumGridStyle.ViewStyle:

                    ListView.OptionsView.ColumnAutoWidth = false;
                    ListView.OptionsView.EnableAppearanceEvenRow = false;
                    ListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                    ListView.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    ListView.Appearance.FocusedRow.BackColor = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.FocusedRow.BackColor2 = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.FocusedRow.ForeColor = Color.Black;

                    ListView.Appearance.SelectedRow.BackColor = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.SelectedRow.BackColor2 = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.SelectedRow.ForeColor = Color.Black;

                    ListView.Appearance.HideSelectionRow.BackColor = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.HideSelectionRow.BackColor2 = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
                    //ListView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                    //ListView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
                    //ListView.Appearance.OddRow.BackColor = System.Drawing.Color.White;
                    //ListView.Appearance.OddRow.BackColor2 = System.Drawing.Color.White;

                    //ListView.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
                    //ListView.Appearance.EvenRow.BackColor2 = System.Drawing.Color.WhiteSmoke;


                    //ListView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Beige;
                    //ListView.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.Beige;
                    //ListView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
                    for (int col = 0; col < ListView.Columns.Count; col++)
                    {
                        ListView.Columns[col].OptionsColumn.AllowEdit = false;
                        ListView.Columns[col].OptionsColumn.AllowFocus = true;


                    }


                    break;


                case enumGridStyle.InputStyle:

                    //ListView.OptionsBehavior.Editable = true;

                    // 默认当单击单元格或进入单元格时，选中单元格内容
                    ListView.OptionsBehavior.AutoSelectAllInEditor = true;
                    // 自动建立列
                    ListView.OptionsBehavior.AutoPopulateColumns = true;

                    //View Options选项
                    //默认显示过滤面板，即当Header能定制过滤，是否显示过滤面板
                    ListView.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
                    // 默认不显示分组面板
                    ListView.OptionsView.ShowGroupPanel = false;
                    // 默认显示指示器
                    ListView.OptionsView.ShowIndicator = true;
                    // 默认显示列的总宽度和视图的宽度不相等
                    ListView.OptionsView.ColumnAutoWidth = false;
                    // 默认显示视图脚
                    //ListView.OptionsView.ShowFooter =true ;

                    // Print Options选项
                    // 不能自动宽度
                    ListView.OptionsPrint.AutoWidth = false;

                    // Cell Navigation Options选项
                    // 按Enter进入下一列
                    ListView.OptionsNavigation.EnterMoveNextColumn = true;

                    for (int col = 0; col < ListView.Columns.Count; col++)
                    {
                        ListView.Columns[col].OptionsColumn.AllowEdit = true;
                        ListView.Columns[col].OptionsColumn.AllowFocus = true;

                    }

                    // 焦点自动到新行
                    ListView.OptionsNavigation.AutoFocusNewRow = true;
                    ListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.CellFocus;

                    //ListView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;

                    ListView.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                    //ListView.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
                    //ListView.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.White;
                    //ListView.Appearance.FocusedRow.ForeColor = Color.Black;

                    //ListView.Appearance.SelectedRow.BackColor = System.Drawing.Color.White;
                    //ListView.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.White;
                    //ListView.Appearance.SelectedRow.ForeColor = Color.Black;

                    //ListView.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
                    //ListView.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;

                    break;

                default:

                    //StyleFormatCondition styleCondition = new StyleFormatCondition();
                    //ListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
                    //ListView.OptionsSelection.EnableAppearanceFocusedRow = false;
                    //ListView.OptionsSelection.EnableAppearanceFocusedCell = false;
                    //ListView
                    //// Adds the style condition to the collection
                    //ListView.FormatConditions.Add(styleCondition);
                    //styleCondition.Value1 = 0;
                    //styleCondition.Value2 = 0;
                    //// Modifying the appearance settings
                    //styleCondition.Appearance.BackColor = Color.White;
                    //styleCondition.Appearance.Font = new Font(ListView.Appearance.Row.Font, FontStyle.Bold);
                    //styleCondition.Appearance.ForeColor = Color.White;
                    //styleCondition.Condition = FormatConditionEnum.Equal;
                    //styleCondition.ApplyToRow = false;

                    ListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
                    ListView.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    ListView.Appearance.FocusedRow.BackColor = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.FocusedRow.BackColor2 = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.FocusedRow.ForeColor = Color.Black;

                    ListView.Appearance.SelectedRow.BackColor = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.SelectedRow.BackColor2 = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.SelectedRow.ForeColor = Color.Black;

                    ListView.Appearance.HideSelectionRow.BackColor = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.HideSelectionRow.BackColor2 = Common._sysrun.CurRowBackcolor;
                    ListView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;

                    ListView.OptionsView.ColumnAutoWidth = false;
                    ListView.OptionsView.EnableAppearanceEvenRow = false;
                    ListView.IndicatorWidth = 40;
                    ListView.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
                    ListView.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto;
                    for (int col = 0; col < ListView.Columns.Count; col++)
                    {
                        ListView.Columns[col].OptionsColumn.AllowEdit = false;
                        ListView.Columns[col].OptionsColumn.AllowFocus = true;

                    }
                    //ListView.Appearance.OddRow.BackColor = System.Drawing.Color.White;
                    //ListView.Appearance.OddRow.BackColor2 = System.Drawing.Color.White;

                    //ListView.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
                    //ListView.Appearance.EvenRow.BackColor2 = System.Drawing.Color.WhiteSmoke;

                    //ListView.Appearance.FocusedRow.BackColor = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.FocusedRow.ForeColor = Color.White;

                    //ListView.Appearance.SelectedRow.BackColor = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.SelectedRow.BackColor2 = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.SelectedRow.ForeColor = Color.White;

                    //ListView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;

                    //ListView.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.HideSelectionRow.BackColor2 = System.Drawing.Color.RoyalBlue;
                    //ListView.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;

                    ListView.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    ListView.Appearance.HeaderPanel.Options.UseTextOptions = true;
                    ListView.OptionsView.RowAutoHeight = false;
                    ListView.ColumnPanelRowHeight = 30;
                    ////设定小数位数
                    //for (int i = 0; i < ListView.Columns.Count; i++)
                    //{
                    //    if (ListView.Columns[i].ColumnType.ToString() == "System.Decimal")
                    //    {
                    //        //ListView.Columns[i].SummaryItem.DisplayFormat = "{0:c}";
                    //        ListView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    //        ListView.Columns[i].DisplayFormat.FormatString = "f1";
                    //        ListView.Columns[i].AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    //        ListView.Columns[i].AppearanceHeader.Options.UseTextOptions = true;
                    //    }

                    //}
                    break;
            }

        }

        #endregion

        #region 其他控件样式设置

        #endregion


    }
}
