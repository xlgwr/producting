using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using Framework.Libs;
using System.Collections.Specialized;
using DevExpress.Utils;
using MachineSystem.SysDefine;
using DevExpress.XtraEditors.Repository;
using System.Collections;
using MachineSystem.SettingTable;
using DevExpress.XtraEditors;

namespace MachineSystem.TabPage
{
    public partial class frmProdSchedulingManager : Framework.Abstract.frmBaseToolEntryXC
    {
        private Int32? editId;

        DataTable dtdtail = null;
        int intMaxID = 0;
        private List<DateTime> m_lsdatetime=new List<DateTime>();
        private DataTable m_Type;
        private DataTable m_Detail;
        #region 画面初始化
        public frmProdSchedulingManager(string frmTitle, Int32? editId)
        {
            InitializeComponent();
            this.editId = editId;
            this.Text = frmTitle;
            this.ScanMode = editId == null ? Framework.Libs.Common.DataModifyMode.add : Framework.Libs.Common.DataModifyMode.upd;
        }


        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            //tool button 
            setControlStatus();

            this.gridView1.OptionsBehavior.Editable = true;
            this.gridView1.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            this.gridView1.OptionsBehavior.ReadOnly = false;
            this.gridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            repositoryItemComboBox1.ParseEditValue += repositoryItemComboBox1_ParseEditValue;
            repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            try
            {
                if (this.ScanMode == Framework.Libs.Common.DataModifyMode.upd)
                {

                    string sql = "select * from P_Produce_Scheduling where ID=" + this.editId;
                    string sqldtail = @"select a.*,b.pName from P_Produce_Scheduling_detail a
                                        inner join P_Produce_TeamKind b on a.TypeID=b.ID where SchedulingID= " + this.editId + " order by StrDate asc ";
                    DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sql);
                    if (dt.Rows.Count > 0)
                    {
                        this.txtPname.Text = dt.Rows[0]. Field<string>("Pname");
                        this.memoEditPmark.Text = dt.Rows[0].Field<string>("Pmark");
                        DataTable dtdtail = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sqldtail);
                        this.myGrid.DataSource = dtdtail;
                    }
                    else
                    {
                        XtraMsgBox.Show("无法获取！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult = DialogResult.OK;
                    }
                }
                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    string sqldtail = @"select a.*,b.pName from P_Produce_Scheduling_detail a
                                        inner join P_Produce_TeamKind b on a.TypeID=b.ID where 1<>1 ";
                    DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sqldtail);
                    this.myGrid.DataSource = dt;
                }
                //gridColumn1.OptionsColumn.AllowEdit = false;
                repositoryItemComboBox2.SelectedIndexChanged += new EventHandler(repositoryItemComboBox2_SelectedIndexChanged);
                repositoryItemComboBox2.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(repositoryItemComboBox2_ParseEditValue);
               
                GetGridInitComBobox();
               
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
                this.Close();
            }
        }
        void repositoryItemComboBox2_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString();
                e.Handled = true;
            }
        }

        void repositoryItemComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0) return;

            DataTable dt_temp = ((DataView)gridView1.DataSource).ToTable();
            DataRow dr = gridView1.GetFocusedDataRow();
            ComboBoxEdit cbx = (ComboBoxEdit)sender;
            CboItemEntity item = (CboItemEntity)cbx.SelectedItem;
            dt_temp.Rows[gridView1.FocusedRowHandle]["TypeID"] = item.Value.ToString();
            dt_temp.Rows[gridView1.FocusedRowHandle]["pName"] = item.Text.ToString();
            myGrid.DataSource = dt_temp;
  
        }
        public void GetGridInitComBobox()
        {
            //类型
            string str1 = string.Format(@"select * from P_Produce_TeamKind ");
            m_Type = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str1);
            for (int i = 0; i < m_Type.Rows.Count; i++)
            {
                CboItemEntity item = new CboItemEntity();
                item.Value = m_Type.Rows[i]["ID"].ToString();
                item.Text = m_Type.Rows[i]["pName"].ToString();
                repositoryItemComboBox2.Items.Add(item);
            }

            
        }
        void repositoryItemComboBox1_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Handled = true;
        }


        #endregion

        #region 画面按钮功能处理方法

        protected override void SetSaveDataProc(frmBaseToolEntryXC frmbase)
        {
            gridView1.CloseEditor();
            gridView1.UpdateCurrentRow();
            
            base.SetSaveDataProc(frmbase);
        }
        protected override void SetCancelInit(bool isClear)
        {
            this.Close();
        }

        protected override void SetInsertProc(ref int RtnValue)
        {
            //base.SetInsertProc(ref RtnValue);
            InsertOrUpdate();
        }

        public override void SetModifyProc(ref int RtnValue)
        {
            // base.SetModifyProc (ref RtnValue);
            InsertOrUpdate();
        }

        private void InsertOrUpdate()
        {
            try
            {

                m_dicItemData = new StringDictionary();
                m_dicItemData["Pname"] = this.txtPname.Text.Trim();
                m_dicItemData["Pmark"] = this.memoEditPmark.Text.Trim();
                if (this.ScanMode == Common.DataModifyMode.add)
                {
                    m_dicItemData["ID"] = this.GetNextId("P_Produce_Scheduling", "ID").ToString();
                    intMaxID = int.Parse(GetNextId("P_Produce_Scheduling", "ID").ToString());
                    int effCount = SysParam.m_daoCommon.SetInsertDataItem("P_Produce_Scheduling", m_dicItemData);
                    if (effCount > 0)
                    {
                        DataTable dt = ((DataView)this.gridView1.DataSource).ToTable();
                        foreach (DataRow item in dt.Select("", " StrDate ASC "))
                        {
                            if (!(item.IsNull("StrDate") || item.IsNull("EndDate")  || item.IsNull("pName")))
                            {
                                StringDictionary tmpDic = new StringDictionary();
                                tmpDic["SchedulingID"] = m_dicItemData["ID"];
                                tmpDic["StrDate"] = item.Field<DateTime>("StrDate").ToString();
                                tmpDic["EndDate"] = item.Field<DateTime>("EndDate").ToString();
                                if (item.Field<string>("pName").ToString().Equals("白班"))
                                {
                                    tmpDic["TypeID"] ="1";
                                }
                                else
                                {
                                    tmpDic["TypeID"] = "2";
                                }
                                
                                SysParam.m_daoCommon.SetInsertDataItem("P_Produce_Scheduling_detail", tmpDic);
                            }
                        }
                        XtraMsgBox.Show("数据保存成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SysParam.m_daoCommon.WriteLog("排班登记", "新增",this.Text );
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    m_dicItemData["ID"] = this.editId.ToString();
                    StringDictionary whereParamDic = new StringDictionary();
                    whereParamDic["ID"] = this.editId.ToString();
                    int effCount = SysParam.m_daoCommon.SetModifyDataItem("P_Produce_Scheduling", m_dicItemData, whereParamDic);
                    if (effCount > 0)
                    {
                        StringDictionary tmpDic = new StringDictionary();
                        tmpDic["SchedulingID"] = this.editId.ToString();
                        SysParam.m_daoCommon.SetDeleteDataItem("P_Produce_Scheduling_detail", tmpDic, tmpDic);
                        DataTable dt = ((DataView)this.gridView1.DataSource).ToTable();
                        foreach (DataRow item in dt.Select("", " StrDate ASC "))
                        {
                            if (!(item.IsNull("StrDate") || item.IsNull("EndDate") || item.IsNull("pName")))
                            {
                                tmpDic = new StringDictionary();
                                tmpDic["SchedulingID"] = this.editId.ToString();
                                tmpDic["StrDate"] = item.Field<DateTime>("StrDate").ToString();
                                tmpDic["EndDate"] = item.Field<DateTime>("EndDate").ToString();
                                //tmpDic["TypeID"] = item.Field<Int32>("TypeID").ToString();
                                if (item.Field<string>("pName").ToString().Equals("白班"))
                                {
                                    tmpDic["TypeID"] = "1";
                                }
                                else
                                {
                                    tmpDic["TypeID"] = "2";
                                }
                                SysParam.m_daoCommon.SetInsertDataItem("P_Produce_Scheduling_detail", tmpDic);
                            }
                        }

                        XtraMsgBox.Show("数据保存成功！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SysParam.m_daoCommon.WriteLog("排班登记", "修改", this.Text);
                        DialogResult = DialogResult.OK;
                    }
                }

            }
            catch (Exception ex)
            {
                XtraMsgBox.Show("出现异常！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
                //throw ex;
            }

        }
        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            int _rowhandle;
            
            try
            {
                _rowhandle =gridView1.FocusedRowHandle;
                if (_rowhandle<0)
                {
                    return;
                }
               
                    gridView1.DeleteRow(_rowhandle);
                    myGrid.Refresh();
                    myGrid.RefreshDataSource();
                    SetAddDateTime();
              
                //gridView1.CloseEditor();
                //gridView1.UpdateCurrentRow();
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void SetAddDateTime()
        {
            DateTime _begin, _end;
            string _strbegin;
            string _strend;
            try
            {
               
                    if (m_lsdatetime.Count>0)
                    {
                        m_lsdatetime.Clear();
                    }
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (i==(gridView1.RowCount-1))
                        {
                            continue;  
                        }
                        _strbegin = gridView1.GetRowCellValue(i, this.gridColumn2).ToString();
                        _strend = gridView1.GetRowCellValue(i, this.gridColumn3).ToString();
                        _begin = DateTime.Parse(_strbegin);
                        _end = DateTime.Parse(_strend);
                        m_lsdatetime.Add(_begin);
                        m_lsdatetime.Add(_end);
                   }  
                
               
               

                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        #endregion

        #region 共同方法

        private bool IsFieldDuplicated(string table, string keyFieldName, object keyFieldValue, string checkFieldName, object checkFieldValue)
        {
            StringBuilder sb = new StringBuilder("select count(1) as cnt from  ");
            sb.AppendFormat("{0} where 1=1", table);

            if (checkFieldValue.GetType() == typeof(string))
            {
                sb.AppendFormat(" and {0}='{1}'", checkFieldName, checkFieldValue);
            }
            else
            {
                sb.AppendFormat(" and {0}={1} ", checkFieldName, checkFieldValue);
            }
            if (keyFieldValue != null)
            {
                if (keyFieldValue.GetType() == typeof(string))
                {
                    sb.AppendFormat(" and {0}<>'{1}'", keyFieldName, keyFieldValue);
                }
                else
                {
                    sb.AppendFormat(" and {0}<>{1} ", keyFieldName, keyFieldValue);
                }
            }



            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                Int32 count = dt.Rows[0].Field<Int32>(0);
                dt.Dispose();
                return count > 0;
            }
            dt.Dispose();
            return true;
        }

        private Int32 GetNextId(string table, string keyFieldName)
        {
            StringBuilder sb = new StringBuilder("");
            sb.AppendFormat("select ISNULL(MAX({0}),0) as id from {1}", keyFieldName, table);
            DataTable dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(sb.ToString());
            if (dt.Rows.Count > 0)
            {
                Int32 id = dt.Rows[0].Field<Int32>(0);
                dt.Dispose();
                return id + 1;
            }
            dt.Dispose();
            return 1;
        }
        /// <summary>
        /// 画面数据有效检查处理
        /// </summary>
        /// <param name="isSucces"></param>
        protected override void GetInputCheck(ref bool isSucces)
        {
            base.GetInputCheck(ref isSucces);
            if (isSucces)
            {
                if (string.IsNullOrEmpty(this.txtPname .Text))
                {
                    isSucces = false;
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.txtPname, "名称不能为空!");

                }
                else
                {
                    if (this.txtPname.Text.Trim().ToCharArray().Count() > 40)
                    {
                        isSucces = false;
                        DataValid.ShowErrorInfo(this.ErrorInfo, this.txtPname, "名称不能超过40字符!");
                    }

                    //数据库重复判断
                    if (this.ScanMode == Common.DataModifyMode.add)
                    {
                        if (IsFieldDuplicated("P_Produce_Scheduling", "ID", null, "Pname", this.txtPname.Text.Trim()))
                        {
                            isSucces = false;
                            DataValid.ShowErrorInfo(this.ErrorInfo, this.txtPname, "名称已经存在!");
                        }
                    }
                    if (this.ScanMode == Common.DataModifyMode.upd)
                    {
                        if (IsFieldDuplicated("P_Produce_Scheduling", "ID",this.editId , "pName", this.txtPname.Text.Trim()))
                        {
                            isSucces = false;
                            DataValid.ShowErrorInfo(this.ErrorInfo, this.txtPname, "名称已经被使用!");
                        }
                    }
                }
                if (this.memoEditPmark.Text != null && this.memoEditPmark.Text.Trim().ToCharArray().Count() > 1000)
                {
                    isSucces = false;
                    DataValid.ShowErrorInfo(this.ErrorInfo, this.memoEditPmark, "备注不能超过1000字符!");
                }

                GetGridViewCheck(ref isSucces);
            }
        }

        private void GetGridViewCheck(ref bool succes)
        {
            try
            {
                DataTable dt = ((DataView)this.gridView1.DataSource).ToTable();
                DateTime bef = new DateTime(1,1, 1);
                foreach (DataRow item in dt.Select("", " StrDate ASC "))
                {

                    if (item.IsNull("StrDate")  && item.IsNull("EndDate")  && item.IsNull("pName") )
                    {
                        continue;
                    }
                    else if (item.IsNull("StrDate") || item.IsNull("EndDate") || item.IsNull("pName"))
                    {
                        succes = false;
                        XtraMsgBox.Show("开始时间,结束时间,类型 必须都有值！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    else
                    {
                       
                        if (item.Field<DateTime>("StrDate").Subtract(bef).Ticks <= 0)
                        {
                            succes = false;
                            XtraMsgBox.Show("时间区间不能重叠！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        if ( item.Field<DateTime>("EndDate").Subtract(item.Field<DateTime>("StrDate")).Ticks <=0)
                        {
                            succes = false;
                            XtraMsgBox.Show("开始时间必须早于结束时间！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        bef = item.Field<DateTime>("EndDate");
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        private void setControlStatus()
        {
            //tool button 
            this.CancelButtonEnabled = true;
            this.CancelButtonVisibility = true;
            this.DeleteButtonEnabled = false;
            this.DeleteButtonVisibility = false;
            this.EditButtonEnabled = false;
            this.EditButtonVisibility = false;
            this.ExcelButtonEnabled = false;
            this.ExcelButtonVisibility = false;
            this.ExitButtonEnabled = false;
            this.ExitButtonVisibility = false;
            this.ImportButtonEnabled = false;
            this.ImportButtonVisibility = false;
            this.NewButtonEnabled = false;
            this.NewButtonVisibility = false;
            this.PrintButtonEnabled = false;
            this.PrintButtonVisibility = false;
            this.SaveButtonEnabled = true;
            this.SaveButtonVisibility = true;
            this.SearchButtonEnabled = false;
            this.SearchButtonVisibility = false;
            this.RefreshButtonEnabled = false;
            this.RefreshButtonVisibility = false;
        }
        #endregion

        private void btnAddMulti_Click(object sender, EventArgs e)
        {
            bool _chk = false;
            
            try
            {
                //if ((dateOperDate1.EditValue == null || string.IsNullOrEmpty(dateOperDate1.EditValue.ToString())))
                //{
                //    DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate1, "请选择开始时间!");
                //    return;
                //}
                //if ((dateOperDate2.EditValue == null || string.IsNullOrEmpty(dateOperDate2.EditValue.ToString())))
                //{
                //    DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate2, "请选择结束时间!");
                //    return;
                //}
                //if (this.dateOperDate2.DateTime < this.dateOperDate1.DateTime)
                //{
                //    DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate2, "结束时间必须大于开始时间!");
                //    return;
                //}
                if (GetDatetimeCheck())
                {
                     XtraMsgBox.Show("时间区间不能重叠！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                     return;   
                }
                //GetGridViewCheck(ref _chk);
                //if (!_chk) return;
                dtdtail = ((DataView)gridView1.DataSource).ToTable();
                
                if (dtdtail != null)
                {
                    DataRow drGridRowUser = dtdtail.NewRow();

                    drGridRowUser["SchedulingID"] = intMaxID.ToString();
                    //drGridRowUser["StrDate"] = dateOperDate1.EditValue.ToString();
                    //drGridRowUser["EndDate"] = dateOperDate2.EditValue.ToString();
                    //drGridRowUser["TypeID"] = lookUpEditTeamKind.EditValue.ToString();


                    dtdtail.Rows.Add(drGridRowUser);
                    //m_lsdatetime.Add(this.dateOperDate1.DateTime);
                    //m_lsdatetime.Add(this.dateOperDate2.DateTime);
                }

                this.myGrid.DataSource = dtdtail.DefaultView;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
            //if (gridView1.RowCount>1)
            //{
            //    gridView1.SetRowCellValue((gridView1.RowCount-1),gridView1.Columns["Operate"],"");
            //}
        }

        private bool GetDatetimeCheck()
        {
            //bool _result = false;
            //DateTime _dt;
            //DateTime _dtcom = dateOperDate1.DateTime;
            //TimeSpan _ts;
            //try
            //{
            //    if (m_lsdatetime.Count==0)
            //    {
            //        return _result;
            //    }
            //    else
            //    {
            //        for (int i = 0; i < m_lsdatetime.Count; i++)
            //        {
            //            _dt = m_lsdatetime[i];
            //            _ts = _dtcom - _dt;

            //            if (_ts.TotalSeconds<= 1)
            //            {
            //                return true;   
            //            }
            //        }
            //    }

            //    return _result;
            //}
            //catch (Exception ex)
            //{
                
            //    throw ex;
            //}
            return true;
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
          
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "Operate" && e.RowHandle<0)
            {
                RepositoryItemButtonEdit _btn = (RepositoryItemButtonEdit)e.CellValue;
                if (_btn!=null)
                {
                    _btn.Buttons[0].Caption = "";
                }
               
                return;
            }
            if (e.Column.FieldName == "TypeID")
            {
                //string _str = gridView1.GetRowCellDisplayText(e.RowHandle, gridView1.Columns["TypeID"]);
                if (e.RowHandle<0)
                {
                   e.DisplayText = "";
                   return;
                }
                if (e.DisplayText.ToString().Trim()=="1"||e.DisplayText.ToString().Trim()=="白班")
                {
                    e.DisplayText = "白班";
                }
                else if (e.DisplayText.ToString().Trim()=="2"||e.DisplayText.ToString().Trim()=="晚班")
                {
                     e.DisplayText = "晚班";
                }
                else
                {
                    e.DisplayText = "未设置";
                }
                

            }
        }

       

     
    }
}
