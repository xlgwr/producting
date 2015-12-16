using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Framework.Libs
{
    [Serializable]
    public class SysRun
    {

        private Common.DataSourceType _strDataSourceType;
        [Bindable(true), DefaultValueAttribute(Common.DataSourceType.SQLServer), Category("一般参数设定"), DescriptionAttribute("数据源类型")]
        public Common.DataSourceType DataSourceType
        {
            get
            {
                return _strDataSourceType;
            }
            set
            {
                if (_strDataSourceType == value)
                    return;
                _strDataSourceType = value;
            }
        }

        private string _strServerName = "";
        [Bindable(true), DefaultValueAttribute(""), Category("数据库设定"), DescriptionAttribute("服务器名称")]
        public string ServerName
        {
            get
            {
                if (_strServerName == null)
                    _strServerName = "";
                return _strServerName;
            }
            set
            {
                if (_strServerName == null)
                    _strServerName = "";
                else
                    _strServerName = value;
            }
        }

        private string _strDataBaseName = "";
        [Bindable(true), DefaultValueAttribute(""), Category("数据库设定"), DescriptionAttribute("数据库名称")]
        public string DataBaseName
        {
            get
            {
                if (_strDataBaseName == null)
                    _strDataBaseName = "";
                return _strDataBaseName;
            }
            set
            {
                if (_strDataBaseName == null)
                    _strDataBaseName = "";
                else
                    _strDataBaseName = value;
            }
        }

        private string _strUserName="";
        [Bindable(true), DefaultValueAttribute(""), Category("数据库设定"), DescriptionAttribute("登录用户")]
        public string UserName
        {
            get
            {
                if (_strUserName == null)
                    _strUserName = "";
                return _strUserName;
            }
            set
            {
 
                if (_strUserName == null)
                    _strUserName = "";
                else
                    _strUserName = value;
            }
        }

        private string _strPassWord = "";
        [Bindable(true), DefaultValueAttribute(""), Category("数据库设定"), DescriptionAttribute("密码")]
        public string PassWord
        {
            get
            {

                if (_strPassWord == null)
                    _strPassWord = "";
                return _strPassWord;
            }
            set
            {
                if (_strPassWord == null)
                    _strPassWord = "";
                else
                    _strPassWord = value;
            }
        }

        private Common.Language _Language;
        [Bindable(true), Category("一般参数设定"), DescriptionAttribute("显示语言")]
        public Common.Language Language
        {
            get
            {
                return _Language;
            }
            set
            {
                if (_Language == value)
                    return;
                _Language = value;
            }
        }


        //定义各窗体中grid中各单元格的字体
        private Font _GridFont;
        [Bindable(true), Category("字体设定"), DescriptionAttribute("系统中数据显示表格字体相关设定")]
        public Font GridFont
        {
            get
            {
                return _GridFont;
            }
            set
            {
                if (_GridFont == value)
                    return;
                _GridFont = value;
            }
        }

        //定义各窗体bar上的字体
        private Font _ButtonEditFont;
        [Bindable(true), Category("字体设定"), DescriptionAttribute("系统中数据显示文本框,工具栏等相关项目的字体设定")]
        public Font ButtonEditFont
        {
            get
            {
                return _ButtonEditFont;
            }
            set
            {
                if (_ButtonEditFont == value)
                    return;
                _ButtonEditFont = value;
            }
        }

        //数据表格当前行背景色
        private System.Drawing.Color _CurRowBColor;
        [Bindable(true), DefaultValue(2), Category("一般参数设定"), DescriptionAttribute("数据表格当前行背景色")]
        public System.Drawing.Color CurRowBackcolor
        {
            get
            {
                return _CurRowBColor;
            }
            set
            {
                if (_CurRowBColor == value)
                    return;
                _CurRowBColor = value;
            }
        }

        //数据表格可编辑列的背景色
        private System.Drawing.Color _EditColumnBackColor;
        [Bindable(true), DefaultValue(2), Category("一般参数设定"), DescriptionAttribute("数据表格可编辑列的背景色")]
        public System.Drawing.Color EditColumnBackColor
        {
            get
            {
                return _EditColumnBackColor;
            }
            set
            {
                if (_EditColumnBackColor == value)
                    return;
                _EditColumnBackColor = value;
            }
        }



        //窗体皮肤属性
        private Common.FormSkin _formskin = Common.FormSkin.Xmas2008Blue ;
        [Bindable(true), DefaultValueAttribute(Common.FormSkin.Springtime), Category("一般参数设定"), DescriptionAttribute("设定软件的皮肤")]
        public Common.FormSkin FormSkin
        {
            get
            {
                return _formskin;
            }
            set
            {
                if (_formskin == value)
                    return;
                _formskin = value;
            }
        }

        private string _strMessageInfo;
        [Bindable(true), DefaultValueAttribute(""), Category("一般参数设定"), DescriptionAttribute("提示信息")]
        public string MessageInfo
        {
            get
            {
                return _strMessageInfo;
            }
            set
            {
                if (_strMessageInfo == value)
                    return;
                _strMessageInfo = value;
            }
        }

    }
}
