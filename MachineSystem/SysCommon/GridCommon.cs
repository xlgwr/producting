using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Data;
using Framework.DataAccess;
using System.Data.SqlClient;
using Framework.Libs;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Drawing;

namespace MachineSystem.SysCommon
{
    class GridCommon
    {

        /// <summary>
        /// 根据编号获取name值
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="NOCollom">编号列（包含值）</param>
        /// <param name="NameCollom">name列名</param>
        /// <returns></returns>
        public static string GetNameByNO(daoCommon dao, string tablename, StringDictionary NOCollom, string NameCollom)
        {
            string Collom = string.Empty;
            try
            {
                DataTable dt = dao.GetTableInfo(tablename, NOCollom, NOCollom, new StringDictionary(), "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    Collom = dt.Rows[0][NameCollom].ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Collom;
        }

        /// <summary>
        /// add by xlg 2015-12-07
        /// </summary>
        public static string GetUserImage(daoCommon dao, string strUserID)
        {
            //考勤系统头像目录
            string AtPathDir = Application.StartupPath + "\\" + Common.AtPathDir + "\\";
            var nullPath = AtPathDir + "01.png";
            try
            {
                if (strUserID == "")
                {
                    return nullPath;
                }
                var currPath = AtPathDir + strUserID + ".jpg";

                if (!File.Exists(currPath))
                {
                    //add by xlg 2015-12-07
                    return strUserID;
                    //end by xlg
                }
                else
                {
                    if (!Program._dicCheckImage.ContainsKey(currPath))
                    {
                        FileInfo tmpfile = new FileInfo(currPath);
                        if (tmpfile.Length < 512)
                        {
                            File.Delete(currPath);
                            return nullPath;
                        }
                        var tmpimage = Image.FromFile(currPath);
                        Program._dicCheckImage.Add(currPath, tmpimage);
                    }

                    return currPath;
                }
            }
            catch (Exception ex)
            {
                return AtPathDir;
                //throw ex;
            }
            return AtPathDir;
        }

        /// <summary>
        /// 取得用户头像 old
        /// </summary>
        public static string GetUserImageOld(daoCommon dao, string strUserID)
        {
            //考勤系统头像目录
            string AtPathDir = Application.StartupPath + "\\" + Common.AtPathDir;
            try
            {
                if (strUserID == "")
                {
                    AtPathDir = AtPathDir + "01.png";
                    //add by xlg 2015-12-07
                    return AtPathDir;
                }
                //ip是10.71开始：EmHttpIpRoom；ip是192.168开始：EmHttpIpWork；
                //string serverIp = Common.EmHttpIpRoom;//办公室
                //string serverIp = Common.EmHttpIpWork;//车间
                if (!File.Exists(AtPathDir + strUserID + ".jpg"))
                {


                    //考勤系统人员头像文件不存在 
                    //serverIp = "\\\\192.168.1.16";
                    ////int ret = NetworkSharedFolder.Connect(serverIp + "\\1", AtPathDir, "ISEC", "123");
                    //string server = serverIp + "\\1\\" + strUserID + ".jpg";
                    ////int ret = NetworkSharedFolder.Connect(serverIp + EmPathDir, AtPathDir, "administrator", "i3b4m6");
                    ////string server = serverIp + EmPathDir + strUserID + ".jpg";
                    //if (File.Exists(server))
                    ////if (true)
                    //{//从人事系统复制头像到考勤系统：根据用户登录IP判断，
                    //    //DownLoadSoft(AtPathDir, server, strUserID + ".jpg");
                    //    System.IO.File.Copy(server, AtPathDir + strUserID + ".jpg", true);

                    //    AtPathDir = AtPathDir + strUserID + ".jpg";
                    //}
                    //else
                    //{
                    //    AtPathDir = AtPathDir + "01.png";
                    //}

                    AtPathDir = AtPathDir + "01.png";
                }
                else
                {
                    AtPathDir = AtPathDir + strUserID + ".jpg";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return AtPathDir;
        }
        /// <summary>         /// 下载         
        /// </summary>        
        //private static bool DownLoadSoft(string DownloadPath, string FullFilePath, string FileName)
        //{
        //    string path = DownloadPath.Remove(DownloadPath.Length - 1);
        //    bool flag = false;           
        //    try
        //    { 
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        using (FileStream fs = new FileStream(DownloadPath + FileName, FileMode.Create, FileAccess.Write)) 
        //        {
        //            //创建请求                     
        //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(FullFilePath);
        //            //接收响应 
        //            HttpWebResponse response = (HttpWebResponse)request.GetResponse(); 
        //            //输出流
        //            Stream responseStream = response.GetResponseStream();
        //            byte[] bufferBytes = new byte[10000];//缓冲字节数组 
        //            int bytesRead = -1;
        //            while ((bytesRead = responseStream.Read(bufferBytes, 0, bufferBytes.Length)) > 0)
        //            {
        //                fs.Write(bufferBytes, 0, bytesRead); 
        //            }       
        //            if (fs.Length>0)      
        //            {                   
        //                flag = true;            
        //            }                    
        //            //关闭写入         
        //            fs.Flush();          
        //            fs.Close();         
        //        }            
        //    }            
        //    catch (Exception exp)   
        //    {                 //返回错误消息  
        //    }            
        //    return flag;       
        //}


        /// <summary>
        /// 人员调动数据更新
        /// </summary>
        /// <param name="AttendDate"></param>
        /// <param name="JobForID"></param>
        /// <param name="ProjectID"></param>
        /// <param name="LineID"></param>
        /// <param name="TeamID"></param>
        /// <param name="UserID"></param>
        /// <param name="pFlag">调动类型</param>
        /// <returns></returns>
        public static int SetTeamChangeSQL(string AttendDate, string JobForID, string ProjectID, string LineID, string TeamID,string UserID,string pFlag)
        {
            int result = 0;
            try
            {
                StringDictionary dicItemName = new StringDictionary();
                StringDictionary dicPrimarName = new StringDictionary();
                string str_sql = string.Empty;
                DataTable dt_temp=new DataTable();

                //删除Attend_Total_Result表中对应UserID的信息
                str_sql = string.Format(@"DELETE t1 FROM Attend_Total_Result t1  WHERE t1.AttendDate=convert(VARCHAR(10),'{0}') 
                                               AND t1.JobForID='{1}' AND t1.ProjectID='{2}' AND t1.LineID='{3}' AND t1.TeamID='{4}' AND t1.UserID='{5}'",
                                               AttendDate, JobForID, ProjectID, LineID, TeamID, UserID);
                dicItemName["AttendDate"]=AttendDate;
                dicItemName["JobForID"]=JobForID;
                dicItemName["ProjectID"]=ProjectID;
                dicItemName["LineID"]=LineID;
                dicItemName["TeamID"]=TeamID;
                dicItemName["UserID"]=UserID;
                dicPrimarName["AttendDate"] = "true";
                dicPrimarName["JobForID"] = "true";
                dicPrimarName["ProjectID"] = "true";
                dicPrimarName["LineID"] = "true";
                dicPrimarName["TeamID"] = "true";
                dicPrimarName["UserID"] = "true";
                result=SysParam.m_daoCommon.SetDeleteDataItem("Attend_Total_Result", dicItemName, dicPrimarName);

                //查询V_Attend_Move_i表的人员调动情况并且在V_Produce_User是在职人员，向别班别线别班别都相同
                str_sql = string.Format(@"select distinct
                                              H2.ID,H2.UserID,H2.UserName,H1.Sex,H2.JobForID,
                                              H2.ProjectID,H2.LineID,H2.TeamID,H2.GuanweiID,H2.GuanweiSite , 
                                              H2.StrDate,H2.EndDate,H2.MoveStatus,
                                              ISNULL(H1.AttendUnit,0) AS AttendUnit,H1.StatusNames AS AttendType,'' AS AttendMemo,H1.StatusIDs,
                                              H1.StatusNames,H2.OperID
                                             FROM V_Attend_Move_i H2
                                              LEFT JOIN V_Produce_User H1  --调动视图
                                              ON H2.UserID=H1.UserID --向别
                                                AND H2.ProjectID=H1.ProjectID AND H2.LineID=H1.LineID AND H2.TeamID=H1.TeamID AND H1.User_Status='在职'
                                             where 1=1
                                                AND H2.PFlag in ('{0}') 
                                                AND CONVERT(VARCHAR(10), H2.EndDate,120) ='4000-01-01'  and H2.UserID='{1}'",
                                             pFlag,UserID);
                dt_temp=SysParam.m_daoCommon.GetTableInfoBySqlNoWhere(str_sql);
                if (dt_temp.Rows.Count > 0)
                {
                    //将查询到的V_Attend_Move_i表中的数据插入到Attend_Total_Result
                    dicItemName.Clear();
                    dicPrimarName.Clear();
                    dicItemName["AttendDate"] = AttendDate;
                    dicItemName["UserID"] = dt_temp.Rows[0]["UserID"].ToString();
                    dicItemName["UserNM"] = dt_temp.Rows[0]["UserNM"].ToString();
                    dicItemName["Sex"] = dt_temp.Rows[0]["Sex"].ToString();
                    dicItemName["JobForID"] = dt_temp.Rows[0]["JobForID"].ToString();
                    dicItemName["ProjectID"] = dt_temp.Rows[0]["ProjectID"].ToString();
                    dicItemName["LineID"] = dt_temp.Rows[0]["LineID"].ToString();
                    dicItemName["TeamID"] = dt_temp.Rows[0]["TeamID"].ToString();
                    dicItemName["GuanweiID"] = dt_temp.Rows[0]["GuanweiID"].ToString();
                    dicItemName["GuanweiSite"] = dt_temp.Rows[0]["GuanweiSite"].ToString();
                    dicItemName["TeamSetID"] = dt_temp.Rows[0]["TeamSetID"].ToString();
                    dicItemName["AttendType"] = dt_temp.Rows[0]["AttendType"].ToString();
                    dicItemName["AttendMemo"] = dt_temp.Rows[0]["AttendMemo"].ToString();
                    dicItemName["CardTime"] = "";
                    dicItemName["LastDate"] = "";
                    dicItemName["AttendWork"] = dt_temp.Rows[0]["AttendWork"].ToString();
                    dicItemName["StatusID"] = dt_temp.Rows[0]["StatusID"].ToString();
                    dicItemName["StatusName"] = dt_temp.Rows[0]["StatusName"].ToString();
                    dicItemName["OperID"] = dt_temp.Rows[0]["OperID"].ToString();
                    result = SysParam.m_daoCommon.SetInsertDataItem("Attend_Total_Result", dicItemName);
                }

            }
            catch (Exception ex)
            {
                return result;
                throw ex;
            }

            return result;
        }

    }
}
public class SetImage
{
    public PictureBox pictureBox { get; set; }
    public string strUserID { get; set; }
}
