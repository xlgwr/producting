using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;
using MachineSystem.SysDefine;
using Framework.Libs;
using MachineSystem.form.UserSystem;
using System.Collections;

namespace MachineSystem.TabPage
{
    public partial class frmAttendanceDayReport : Framework.Abstract.frmSearchBasic2	
    {

        #region 变量定义
        /// <summary>
        /// 数据表
        /// </summary>
        DataTable m_tblDataList = new DataTable();

        /// <summary>
        /// 界面初始化标示
        /// </summary>
        bool isLoad = true;
        private Hashtable m_hsDate;
        private DateTime m_Date;
        private DateTime m_DateEnd;
        //private int m_Year = DateTime.Now.Year;
        private ArrayList m_arWeekend = new ArrayList();
        private DateTime dtBegin, dtEnd;
        #endregion


        #region 画面初始化
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmAttendanceDayReport()
        {
            InitializeComponent();

            m_hsDate = new Hashtable();
            ////////////////
            EditButtonVisibility = false;
            DeleteButtonVisibility = false;
            SelectAllButtonVisibility = false;
            SelectOffButtonVisibility = false;
        }
        
        /// <summary>
        /// 窗体初始化处理
        /// </summary>
        public override void SetFormValue()
        {
            DataTable _dt;
            try
            {
                base.SetFormValue();
                string strSql = "";
                //向别 
                if (Common._Administrator == Common._personid)
                {
                    strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_User WHERE JobForID<>0 and JobForName<>'' order by JobForName");
                }
                else
                {
                    strSql = string.Format(@"select distinct JobForID,JobForName from V_Produce_User_Line where  UserID ='{0}' AND  JobForID<>0 order by JobForName", Common._personid);
                }

                DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);

                //默认选中
                DataRow dr = dt_temp.NewRow();
                dr[0] = "-1";
                dr[1] = "全部";
                dt_temp.Rows.InsertAt(dr, 0);
                lookJobFor.Properties.DataSource = dt_temp.DefaultView;
                lookJobFor.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
                lookJobFor.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
                if (dt_temp.Rows.Count > 0)
                {
                    lookJobFor.EditValue = dt_temp.Rows[0][0].ToString();
                }
                lookJobFor.ItemIndex = 0;
                lookJobFor.Properties.BestFit();

                //班别
                SysParam.m_daoCommon.SetLoopUpEdit(TableNames.P_Produce_Team, lookProduce_Team, true, EnumDefine.DefalutItemAllNo, EnumDefine.DefalutItemAllText);
 
                
                //获取下拉框数据
                GetComboBox();
                isLoad = false;
                dateOperDate1.EditValue=DateTime.Now.AddDays(1 - DateTime.Now.Day);
                dateOperDate2.EditValue = DateTime.Now;
                m_DateEnd = DateTime.Now;
                CreateHeader();

            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #endregion

        #region 画面按钮功能处理方法
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="frmBaseToolXC"></param>
        protected override void SetSearchProc(frmSearchBasic2 frmBaseToolXC)
        {
            if ((dateOperDate1.EditValue == null || string.IsNullOrEmpty(dateOperDate1.EditValue.ToString())))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate1, "请选择开始时间!");
                return;
            }
            if ((dateOperDate2.EditValue == null || string.IsNullOrEmpty(dateOperDate2.EditValue.ToString())))
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate2, "请选择结束时间!");
                return;
            }

            dtBegin = DateTime.Parse(dateOperDate1.EditValue.ToString());
            dtEnd = DateTime.Parse(dateOperDate2.EditValue.ToString());
            if (dtEnd < dtBegin)
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.dateOperDate2, "结束时间须大于开始时间!");
                return;
            }
            if (lookJobFor.EditValue.ToString()=="-1")
            {
                DataValid.ShowErrorInfo(this.ErrorInfo, this.lookJobFor, "向别必须选择!");
                return;
            }

            base.SetSearchProc(frmBaseToolXC);

            //GetDspDataList();
        }

        /// <summary>
        /// 修改界面
        /// </summary>
        public override void SetModifyInit()
        {
            base.SetModifyInit(); 
            
            try
            {
                frmEditProduce_User frm = new frmEditProduce_User();
                frm.ScanMode = Common.DataModifyMode.upd;
                frm.dr = gridView1.GetFocusedDataRow();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    GetDspDataList();
                }
            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("画面初始化失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }

        #endregion

        #region 事件处理方法

        /// <summary>
        /// 选择向别加载该向别下的工程别
        /// </summary>
        private void lookJobFor_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where=" where 1=1 ";
            if (lookJobFor.EditValue.ToString()!="-1")
            {
                str_where+=" and JobForID='"+lookJobFor.EditValue.ToString()+"'";
            }
            string str_sql = string.Format(@"select DISTINCT ProjectID,ProjectName FROM V_Produce_Para " + str_where + " order by ProjectName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookProject.Properties.DataSource = dt_temp.DefaultView;
            lookProject.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookProject.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookProject.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookProject.ItemIndex = 0;
            lookProject.Properties.BestFit();
        }

        /// <summary>
        /// 选择工程别加载该向别下的Line别
        /// </summary>
        private void lookProject_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = "";
            if (lookJobFor.EditValue.ToString()!="-1")
            {
                str_where = " JobForID ='" + lookJobFor.EditValue.ToString() + "'";
            }
            if (lookProject.EditValue.ToString() != "-1")
            {
                if (str_where != "") 
                {
                    str_where += " and ";
                }
                str_where = str_where + " ProjectID='" + lookProject.EditValue.ToString() + "'";
            }
            if (str_where != "")
            {
                str_where = " where " + str_where;
            }
            string str_sql = string.Format(@"select DISTINCT LineID,LineName FROM V_Produce_Para " + str_where + " order by LineName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookLine.Properties.DataSource = dt_temp.DefaultView;
            lookLine.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookLine.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookLine.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookLine.ItemIndex = 0;
            lookLine.Properties.BestFit();
        }

        /// <summary>
        /// 选择Line别加载该向别下的班别
        /// </summary>
        private void lookLine_EditValueChanged(object sender, EventArgs e)
        {
            if (isLoad) return;

            string str_where = "";
            if (lookJobFor.EditValue.ToString()!="-1")
            {
                str_where += " and JobForID='" + lookJobFor.EditValue.ToString() + "'";
            }
            if (lookProject.EditValue.ToString() != "-1")
            {
                str_where += " and ProjectID='" + lookProject.EditValue.ToString() + "'";
            }
            if (lookLine.EditValue.ToString() != "-1")
            {
                str_where += " and LineID='" + lookLine.EditValue.ToString() + "'";
            }
            if (str_where != "")
            {
                str_where = " where 1=1 " + str_where;
            }
            string str_sql = string.Format(@"select DISTINCT TeamID as ID,TeamName as pName FROM V_Produce_Para " + str_where + " order by TeamName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            lookProduce_Team.Properties.DataSource = dt_temp.DefaultView;
            lookProduce_Team.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookProduce_Team.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;
            if (dt_temp.Rows.Count > 0)
            {
                lookProduce_Team.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookProduce_Team.ItemIndex = 0;
            lookProduce_Team.Properties.BestFit();
        }

        /// <summary>
        /// 选择班别加载该向别下的关位
        /// </summary>
        private void lookProduce_Team_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;

            string str_where = "";
            if (lookJobFor.EditValue.ToString() != "-1")
            {
                str_where += " and JobForID='" + lookJobFor.EditValue.ToString() + "'";
            }
            if (lookProject.EditValue.ToString() != "-1")
            {
                str_where += " and ProjectID='" + lookProject.EditValue.ToString() + "'";
            }
            if (lookLine.EditValue.ToString() != "-1")
            {
                str_where += " and LineID='" + lookLine.EditValue.ToString() + "'";
            }
            if (lookProduce_Team.EditValue!= null)
            {
                if (lookProduce_Team.EditValue.ToString() != "-1")
                {
                    str_where += " and TeamID='" + lookProduce_Team.EditValue.ToString() + "'";
                }
            }
            if (str_where != "")
            {
                str_where = " where 1=1 " + str_where;
            }
            //string str_sql = string.Format(@"Select DISTINCT GuanweiNames From Produce_Guanwei " + str_where + " Order by GuanweiNames");
            string str_sql = string.Format(@"select Distinct T.GuanweiName from (
                                                    (select p.*,g.pName as GuanweiName from Produce_Guanwei p inner 
                                                        join P_Produce_Guanwei g on p.GuanweiID=g.ID) )T" + str_where + " Order by GuanweiName");
            DataTable dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);

            
        }

        /// <summary>
        /// 选择向别-班别加载该向别下的关位
        /// </summary>
        private void lookmyteamName_EditValueChanged(object sender, EventArgs e)
        {

            if (isLoad) return;
 
        }

        #endregion

        

        /// <summary>
        /// 获取表格信息一览
        /// </summary>
        protected override void GetDspDataList()
        {
            try
            {
 
                CreateHeader();
                SetViewData();

            }
            catch (Exception ex)
            {

                XtraMsgBox.Show("数据加载失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error, ex, this.GetType());
            }
        }
        #region 共同方法

        private void SetViewData()
        {
           
            DataTable _dt,_dttemp;
            string _sql = "select * from V_Attend_Result_Info as a where 1=1 ";
            string _cardtime=string.Empty;
            DataRow[] _dr,_drid;
            DataView _dv;
            string _key;
            string[] _param={"UserID"};
            int _okcount = 0;
            ArrayList _ar = new ArrayList();
            try
            {
                if (m_tblDataList.Rows.Count>0)
                {
                    m_tblDataList.Rows.Clear();
                }
               
                //向别
                if (lookJobFor.EditValue.ToString() != "-1")
                {
                    _sql += " and JobForID='" + lookJobFor.EditValue.ToString() + "'";
                }
                //工程别
                if (lookProject.EditValue.ToString() != "-1")
                {
                    _sql += " and ProjectID='" + lookProject.EditValue.ToString() + "'";
                }
                if (lookLine.EditValue.ToString() != "-1")
                {
                    _sql += " and LineID='" + lookLine.EditValue.ToString() + "'";
                }
                //班别
                if (lookProduce_Team.EditValue != null)
                {
                    if (lookProduce_Team.EditValue.ToString() != "-1")
                    {
                        _sql += " and TeamID='" + lookProduce_Team.EditValue.ToString() + "'";
                    }
                }
                _sql += " and AttendDate between '" + DateTime.Parse(dateOperDate1.EditValue.ToString()).ToString("yyyy-MM-dd") +"'" +
                    " and  '" + DateTime.Parse(dateOperDate2.EditValue.ToString()).ToString("yyyy-MM-dd") + "'";

                if (Common._personid != Common._Administrator)
                {// &&Common._myTeamName != "" && Common._myTeamName != null
                    _sql += " and myTeamName='" + Common._myTeamName + "'";
                }
                _dt = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(_sql);
                if (_dt.Rows.Count == 0) return;
                _dv = _dt.DefaultView;
                _dttemp = _dv.ToTable(true, _param);
                if (_dttemp.Rows.Count == 0) return;
                for (int k = 0; k < _dttemp.Rows.Count; k++)
                {
                    _ar.Add(_dttemp.Rows[k]["UserID"].ToString());
                }

                for (int i = 0; i < _ar.Count; i++)//所有人
                {
                    _sql = "UserID='" + _ar[i].ToString()+"'";
                    _drid = _dt.Select(_sql);

                    DataRow _datarow = m_tblDataList.NewRow();
                    _datarow["myTeamName"] = _drid[0]["myTeamName"].ToString();
                    _datarow["UserNM"] = _drid[0]["UserNM"].ToString();
                    _datarow["UserID"] = _drid[0]["UserID"].ToString();
                    _datarow["GuanweiNM"] = _drid[0]["GuanweiNM"].ToString();
                    _okcount = 0;
                    for (int j = 0; j < m_hsDate.Count; j++)//所有日期
                    {
                        _cardtime = string.Empty;
                      
                        _sql = "UserID='" + _drid[0]["UserID"].ToString() + "'" +
                            //" and  AttendDate ='" + m_Date.AddDays(1).ToString("yyyy-MM-dd") + "'";
                        " and AttendDate ='" + DateTime.Parse(m_Date.Year.ToString() + "/" + m_hsDate[(j + 1).ToString()]).ToString("yyyy-MM-dd") + "'";
                        _dr = _dt.Select(_sql);
                        _key = m_hsDate[(j + 1).ToString()].ToString();
                        if (_dr.Length > 0)
                        {
                            _cardtime = _dr[0]["CardTime"].ToString();
                            if (!string.IsNullOrEmpty(_cardtime))
                            {
                                _okcount++;
                                _datarow[(m_Date.Day + j).ToString()] = "正常";
                               
                            }
                            else
                            {
                                _datarow[(m_Date.Day + j).ToString()] = string.Empty;
                            }
                        }
                        else
                        {
                             
                           _datarow[(m_Date.Day + j).ToString()] = string.Empty;
                           
                        }
                        
                         
                    }
                    _datarow["Sum"] = _okcount.ToString();
                    m_tblDataList.Rows.Add(_datarow);
                }

                gridControl1.DataSource = m_tblDataList;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void CreateHeader()
        {
            DateTime _dt;
            string _currentDdate = string.Format("{0}/{1}", DateTime.Now.Month, DateTime.Now.Day);
            TimeSpan _ts;
            try
            {
                _dt = dateOperDate1.DateTime;//DateTime.Now.AddDays(1 - DateTime.Now.Day);
                 _ts = dateOperDate2.DateTime - _dt;
                 m_DateEnd = dateOperDate2.DateTime;
                //-向别-班别
                 if (!m_tblDataList.Columns.Contains("myTeamName"))
                {
                    m_tblDataList.Columns.Add(new DataColumn("myTeamName", typeof(string)));
                  
                }
                //姓名
                if (!m_tblDataList.Columns.Contains("UserNM"))
                {
                    m_tblDataList.Columns.Add(new DataColumn("UserNM", typeof(string)));
                                
                }
               //编号
                if (!m_tblDataList.Columns.Contains("UserID"))
                {
                    m_tblDataList.Columns.Add(new DataColumn("UserID", typeof(string)));
              
                }
                //关位
                if (!m_tblDataList.Columns.Contains("GuanweiNM"))
                {
                    m_tblDataList.Columns.Add(new DataColumn("GuanweiNM", typeof(string)));
                
                }
                //当前月日
                if (!m_tblDataList.Columns.Contains("Sum"))
                {
                    m_tblDataList.Columns.Add(new DataColumn("Sum", typeof(string)));
                
                }
               
                m_Date = _dt;
                CreateColumns(_ts);

               
                gridControl1.Width = (int)(_ts.TotalDays + 1) * 96 +96+50;
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        private void CreateColumns(TimeSpan _ts)
        {
            DevExpress.XtraGrid.Columns.GridColumn cl;
            string _month, _day;
            DateTime _time=m_Date;
            string _date;
            DateTime _timetemp;
            int _totaldays = 0;
            try
            {
                this.gridView1.Columns.Clear();   
                if (m_hsDate.Count>0)
                    {
                        m_hsDate.Clear();
                    }
                    if (m_arWeekend.Count > 0)
                    {
                         m_arWeekend.Clear();
                    }
                    cl = new DevExpress.XtraGrid.Columns.GridColumn();
                    cl.Caption = "向别-班别";
                    cl.FieldName = "myTeamName";
                    cl.Visible = true;
                    cl.Width = 96;
                    cl.OptionsColumn.AllowMove = false;
                    cl.OptionsColumn.AllowSize = false;
                    cl.OptionsFilter.AllowAutoFilter = false;
                    cl.OptionsFilter.AllowFilter = false;
                    //this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col });
                    cl.VisibleIndex = gridView1.Columns.Count;
                    this.gridView1.Columns.Add(cl);
                    cl = new DevExpress.XtraGrid.Columns.GridColumn();
                    cl.Caption = "姓名";
                    cl.FieldName = "UserNM";
                    cl.Visible = true;
                    cl.Width = 96;
                    cl.OptionsColumn.AllowMove = false;
                    cl.OptionsColumn.AllowSize = false;
                    cl.OptionsFilter.AllowAutoFilter = false;
                    cl.OptionsFilter.AllowFilter = false;
                    //this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col });
                    cl.VisibleIndex = gridView1.Columns.Count;
                    this.gridView1.Columns.Add(cl);
                    cl = new DevExpress.XtraGrid.Columns.GridColumn();
                    cl.Caption = "编号";
                    cl.FieldName = "UserID";
                    cl.Visible = true;
                    cl.Width = 96;
                    cl.OptionsColumn.AllowMove = false;
                    cl.OptionsColumn.AllowSize = false;
                    cl.OptionsFilter.AllowAutoFilter = false;
                    cl.OptionsFilter.AllowFilter = false;
                    //this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col });
                    cl.VisibleIndex = gridView1.Columns.Count;
                    this.gridView1.Columns.Add(cl);
                    cl = new DevExpress.XtraGrid.Columns.GridColumn();
                    cl.Caption = "关位";
                    cl.FieldName = "GuanweiNM";
                    cl.Visible = true;
                    cl.Width = 96;
                    cl.OptionsColumn.AllowMove = false;
                    cl.OptionsColumn.AllowSize = false;
                    cl.OptionsFilter.AllowAutoFilter = false;
                    cl.OptionsFilter.AllowFilter = false;
                    //this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col });
                    cl.VisibleIndex = gridView1.Columns.Count;
                    this.gridView1.Columns.Add(cl);
                    ///////////////当前日期
                    
                    cl = new DevExpress.XtraGrid.Columns.GridColumn();
                    cl.Caption = m_DateEnd.Month.ToString() + "/" + m_DateEnd.Day.ToString();
                    cl.FieldName = "Sum";
                    cl.Visible = true;
                    cl.Width = 96;
                    cl.OptionsColumn.AllowMove = false;
                    cl.OptionsColumn.AllowSize = false;
                    cl.OptionsFilter.AllowAutoFilter = false;
                    cl.OptionsFilter.AllowFilter = false;
                    //this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col });
                    cl.VisibleIndex = gridView1.Columns.Count;
                    this.gridView1.Columns.Add(cl);
                    _totaldays = ((int)_ts.TotalDays + 1);
                    for (int i = 0; i < _totaldays; i++)
                    {
                        if (!m_tblDataList.Columns.Contains((m_Date.Day + i).ToString()))
                        {
                            m_tblDataList.Columns.Add(new DataColumn((m_Date.Day + i).ToString(), typeof(string)));
                        
                        }
                        //string ss = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(_time.DayOfWeek).Substring(2);
                        //_time.AddDays(1);
                        //string ss1 = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(_time.DayOfWeek).Substring(2);
                        _date = string.Format("{0}/{1}/{2}", m_Date.Year, m_Date.Month, (m_Date.Day + i));
                        _timetemp = DateTime.Parse(_date);
                        if (_timetemp.DayOfWeek.ToString().Equals("Saturday") || _timetemp.DayOfWeek.ToString().Equals("Sunday"))
                        {
                            m_arWeekend.Add((m_Date.Day + i).ToString());   
                        }
                        _month =  m_Date.Month.ToString();
                        _day = (m_Date.Day + i).ToString();
                        
                        m_hsDate[(i + 1).ToString()] = _month + "/" + _day;
                        cl = new DevExpress.XtraGrid.Columns.GridColumn();
                        cl.Caption = (m_Date.Day + i).ToString();
                        cl.FieldName = (m_Date.Day + i).ToString();
                            cl.Visible = true;
                            cl.Width = 96;
                            cl.OptionsColumn.AllowMove = false;
                            cl.OptionsColumn.AllowSize = false;
                            cl.OptionsFilter.AllowAutoFilter = false;
                            cl.OptionsFilter.AllowFilter = false;
                            //this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { col });
                            cl.VisibleIndex = gridView1.Columns.Count;
                            
                            this.gridView1.Columns.Add(cl);
                    }
               
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }


        public void GetComboBox() 
        {
            DataTable dt_temp = new DataTable();
            //工程别
            string strSql = string.Format(@"Select DISTINCT ProjectID,ProjectName From V_Produce_Para Order by ProjectName");
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
            lookProject.Properties.DataSource = dt_temp.DefaultView;
            lookProject.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookProject.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            //默认选中
            DataRow dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            if (dt_temp.Rows.Count > 0)
            {
                lookProject.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookProject.ItemIndex = 0;
            lookProject.Properties.BestFit();
            
            //Line别
            strSql = string.Format(@"Select DISTINCT LineID,LineName FROM V_Produce_Para order by LineName");
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
            lookLine.Properties.DataSource = dt_temp.DefaultView;
            lookLine.Properties.ValueMember = dt_temp.Columns[0].ColumnName;
            lookLine.Properties.DisplayMember = dt_temp.Columns[1].ColumnName;

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "-1";
            dr[1] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);

            if (dt_temp.Rows.Count > 0)
            {
                lookLine.EditValue = dt_temp.Rows[0][0].ToString();
            }
            lookLine.ItemIndex = 0;
            lookLine.Properties.BestFit();

            //向别-班别
            strSql = string.Format(@"Select distinct myteamName from [V_Produce_Para] where myteamName is not null order by myteamName");
            dt_temp = SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(strSql);
           

            //默认选中
            dr = dt_temp.NewRow();
            dr[0] = "全部";
            dt_temp.Rows.InsertAt(dr, 0);
  
        }
        #endregion

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            Color _newColor;
            if (m_arWeekend.Contains(e.Column.FieldName))
            {
                _newColor = Color.FromArgb(255, 165, 0);
                e.Appearance.BackColor = _newColor;
            }
            if (e.Column.FieldName.Equals("Sum"))
            {
                  _newColor = Color.FromArgb(0, 255, 255);
                e.Appearance.BackColor = _newColor;
            }
        }


    }
}
