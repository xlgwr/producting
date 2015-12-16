using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraGrid.Views.Grid;
using Framework.Libs;
using System.Drawing;
using System.Data;

namespace Framework.Abstract
{

   public class GridViewUtil
    {

        #region"变量定义"

        private  DevExpress.XtraGrid.Views.Grid.GridView _ParentGridView ;
        private DevExpress.XtraGrid.Views.Grid.GridView _ChildGridView;
        private DevExpress.XtraGrid.GridControl _GridControlList;

        #endregion

        #region"属性设置"

        [Bindable(true), Category("表格控件对象"), Description("设定表格控件描述")]
        public DevExpress.XtraGrid.GridControl GridControlList
        {
            get
            {
                return this._GridControlList;
            }
            set
            {
                this._GridControlList = value;
            }
        }

        [Bindable(true), Category("主表数据对象"), Description("设定主表数据对象描述")]
        public DevExpress.XtraGrid.Views.Grid.GridView ParentGridView
        {
            get
            {
                return this._ParentGridView;
            }
            set
            {
                this._ParentGridView = value;
            }
        }

        [Bindable(true), Category("子表数据对象"), Description("设定子表数据对象描述")]
        public DevExpress.XtraGrid.Views.Grid.GridView ChildGridView
        {
            get
            {
                return this._ChildGridView;
            }
            set
            {
                this._ChildGridView = value;
            }
        }

        #endregion

        #region 控件样式设置

        /// <summary>
        /// 表格列只读设置
        /// </summary>
        /// <param name="view">表格控件</param>
        /// <param name="ReadOnly">是否只读</param>
        public static void SetGridColumnReadOnly(GridView view, bool ReadOnly)
        {
            for (int i = 0; i < view.Columns.Count; i++)
            {
                view.Columns[i].OptionsColumn.ReadOnly = ReadOnly;
            }
        }

        /// <summary>
        /// 表格列只读设置
        /// </summary>
        /// <param name="view">表格控件</param>
        /// <param name="ReadOnly">是否只读</param>
        /// <param name="obj">指定设置列</param>
        public static void SetGridColumnReadOnly(GridView view, bool ReadOnly, int[] obj)
        {
            for (int i = 0; i < view.Columns.Count; i++)
            {
                for (int j = 0; j < obj.Length; j++)
                {
                    if (i == Convert.ToInt32(obj[j]))
                    {
                        view.Columns[i].OptionsColumn.ReadOnly = ReadOnly;
                    }
                }
            }
        }


        /// <summary>
        /// 初始化数据表格样式
        /// </summary>
        /// <param name="ListView">表格中的ListView对象</param>
        /// <param name="style">表格显示模式</param>
        public static void SetIniGridStyle(DevExpress.XtraGrid.Views.Grid.GridView ListView, Common.enumGridStyle style)
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
            }

            //common._sysrun.CurRowBackcolor = Color.WhiteSmoke;
            ListView.IndicatorWidth = 45;

            switch (style)
            {
                case Common.enumGridStyle.ViewStyle:
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

                case Common.enumGridStyle.InputStyle:

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
                    ListView.IndicatorWidth = 45;
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

        #region 表格全选设置

        /// <summary>
        /// 表格列只读设置
        /// </summary>
        /// <param name="view">表格控件</param>
        /// <param name="ReadOnly">是否只读</param>
        /// <param name="obj">指定设置列</param>
        public static void SetGridSelect(GridView view, string ColumName, bool isSelect)
        {
            if (string.IsNullOrEmpty(ColumName) || view == null || view.RowCount <=0) return;
            
            //view.MoveFirst();

            for (int i = 0; i < view.RowCount; i++)
            {

                view.Focus();
                view.SetRowCellValue(i, ColumName, isSelect);
   
                ////DataRow dr = view.GetFocusedDataRow();
                //dr[ColumName] = isSelect;  
                ////view.MoveNext();
            }
        }

        #endregion
    }

}
