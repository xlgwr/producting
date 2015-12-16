using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Collections;
using System.Data;

namespace Framework.FileOperate
{
    /// <summary>
    /// rwconfig 的摘要说明。
    /// </summary>
    public class RWConfig
    {
                //声明读写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
       
        [DllImport("kernel32.dll")]
        public extern static int GetPrivateProfileSectionNamesA(byte[] buffer, int iLen, string fileName);

        public string _Path; //INI文件名
        public RWConfig() { }

        //类的构造函数，传递INI文件名
        /// <summary>
        /// 读取Ini文件的对象构造函数
        /// </summary>
        /// <param name="_INIPath">文件名</param>
        public RWConfig(string _INIPath)
        {
            _Path = _INIPath;
        }

        ////读INI文件 
        ///// <summary>
        ///// 读取Ini文件方法
        ///// </summary>
        ///// <param name="Section">区域名称</param>
        ///// <param name="Key">项目名称</param>
        ///// <returns></returns>
        //public string ReadTextFile(string Section, string Key)
        //{
        //    StringBuilder strTemp = new StringBuilder(9000);
        //    int i = GetPrivateProfileString(Section, Key, "", strTemp, 9000, this._Path);
        //    return (strTemp.ToString().Trim().Equals("") ? Key : strTemp.ToString());
        //}

        //读INI文件 
        /// <summary>
        /// 读取Ini文件方法
        /// </summary>
        /// <param name="Section">区域名称</param>
        /// <param name="Key">项目名称</param>
        /// <returns>返回读取结果</returns>
        public string ReadTextFile(string Section, string Key)
        {
            Byte[] Buffer = new Byte[400];
            int bufLen = GetPrivateProfileString(Section, Key, "", Buffer, Buffer.GetUpperBound(0), this._Path);

            //以Utf-8的编码来显示的编码方式，否则无法支持日文操作系统
            System.Text.Encoding enc=System.Text.Encoding.UTF8;
            string s = enc.GetString(Buffer);
            return s.Replace("\0","").Trim();
  

        }



        //写INI文件 
        /// <summary>
        /// 写Ini文件方法
        /// </summary>
        /// <param name="Section">区域名称</param>
        /// <param name="Key">项目名称</param>
        /// <param name="Value">读取值</param>
        public void WriteTextFile(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this._Path);
        }
        //写INI文件 
        /// <summary>
        /// 写Ini文件方法
        /// </summary>
        /// <param name="Section">区域名称</param>
        /// <param name="Key">项目名称</param>
        /// <param name="Value">读取值</param>
        public void WriteTextFile(string Section, string Key, string Value,string path)
        {
            WritePrivateProfileString(Section, Key, Value, this._Path);
        }
        /// <summary>         
        /// 读取INI文件中某个section下所有的Key
        /// </summary>         
        /// <param name="sectionName"></param>
        /// <returns></returns>         
        public ArrayList ReadKeys(string sectionName, string strPath)
        {
            byte[] buffer = new byte[5120];
            int rel = GetPrivateProfileString(sectionName, null, "", buffer, buffer.GetUpperBound(0), strPath);
            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }
        /// <summary>
        /// 返回该配置文件中所有Section名称的集合   
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public ArrayList ReadSections(string strPath)
        {
            byte[] buffer = new byte[65535];
            int rel = GetPrivateProfileSectionNamesA(buffer, buffer.GetUpperBound(0), strPath);
            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }
        /// <summary>
        /// 读取ini文件，根据Section和Key读取value
        /// </summary>
        /// <param name="Section"></param>
        /// <param name=VarKey.common.ValueMember></param>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key, string strPath)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = GetPrivateProfileString(Section, Key, "", temp, 255, strPath);
             
            return temp.ToString();
           


        }
        public void ClearSection(string Section, string strPath)
        {
            IniWriteValue(Section, null, null, strPath);
        }
        /// <summary>
        /// 写INI文件         
        /// </summary>
        /// <param name="Section"></param>
        /// <param name=VarKey.common.ValueMember></param>
        /// <param name=VarKey.common.DisplayMember></param>        
        public void IniWriteValue(string Section, string Key, string Value, string strPath)
        {
            WritePrivateProfileString(Section, Key, Value, strPath);
        }
    }
}
