using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using MachineSystem.form.Master;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Resources;

namespace MachineSystem.SysDefine
{
    public class EnumDefine
    {
        

        #region 变量定义

        /// <summary>
        /// 选择列的列名
        /// </summary>
        public static string SlctValue = "SlctValue";

        public static string SlctButton = "SlctButton";

        /// <summary>
        /// 默认全部(Key：-1)
        /// </summary>
        public static string DefalutItemAllNo = "-1";
        /// <summary>
        /// >默认全部(Value：全部)
        /// </summary>
        public static string DefalutItemAllText = "全部";
        /// <summary>
        /// 默认(-请选择-)
        /// </summary>
        public static string DefaultPleaseSelect = "-请选择-";
        /// <summary>
        /// 系统监视的ID
        /// </summary>
        public static string MonitorType = "7";

        /// <summary>
        /// 模板文件名

        /// </summary>
        public static string ModelFileName = "ProductMode.xls";


        /// <summary>
        /// 系统版本号
        /// </summary>
        public const string VersionNo = "1.0.1";
        /// <summary>
        /// 版本小号
        /// </summary>
        public  static  string VersionNos = "2.0.0";

        /// <summary>
        /// 运行地址//
        /// </summary>
        public static string StartupPath = "2.0.0";

        /// <summary>
        /// 已报废(-2)
        /// </summary>
        public const string StockInResult = "-2";
        /// <summary>
        /// 监视画面的设备数量(目前为6,后面添加)
        /// </summary>
        public const int MonitDevice = 6;
        /// <summary>
        /// 未确认
        /// </summary>
        public const String NotConfirm = "0";
        // <summary>
        /// 已确认
        /// </summary>
        public const String HasConfirm = "1";
        /// <summary>
        /// 系统监视时,判断系统是否死机的时间间隔倍数
        /// </summary>
        public const int multiple = 2;


        #region 是否试模
        /// <summary>
        /// 是否试模--是
        /// </summary>
        public const string TestProduce = "1";
        /// <summary>
        /// 是否试模--否
        /// </summary>
        public const string NoTestProduce ="0";
        #endregion

        #region excel的定义

        /// <summary>
        /// 盘点信息的sheet名(必须存在)
        /// </summary>
        public static string InventoryExcel_Sheet2 = "InventoryCheckInfo";
        /// <summary>
        /// 盘点信息的excel模版文件名

        /// </summary>
        public static string InventoryExcelName = "InventoryCheck.xls";
        /// <summary>
        /// 客户订单的sheet名

        /// </summary>
        public static string CustomerExcel_Sheet2 = "CustomerOrderInfo";
        /// <summary>
        /// 客户订单excel模版文件名

        /// </summary>
        public static string CustomerOrderExcelName = "CustomerOrder.xls";
        /// <summary>
        /// 组别信息的sheet名
        /// </summary>
        public static string AreaInfo_sheet2 = "AreaInfo";
        /// <summary>
        /// 原料信息的sheet名

        /// </summary>
        public static string MaterialInfo_sheet2 = "MaterialsInfo";
        /// <summary>
        /// 交货地信息的sheet名

        /// </summary>
        public static string DeliveryInfo_sheet2 = "DeliveryInfo";
        /// <summary>
        /// 产品原材料信息sheet名

        /// </summary>
        public static string ProductRawMaterialsInfo_sheet2 = "ProductRawMaterialsInfo";

        /// <summary>
        /// 指示单原料sheet名
        /// </summary>
        public static string InstructionRawMaterialsInfo_sheet2 = "InstructionRawMaterialsInfo";

        /// <summary>
        /// 指示单原料sheet名
        /// </summary>
        public static string PlanOrder_sheet2 = "PlanOrder";
        /// <summary>
        /// 免许sheet名
        /// </summary>
        public static string License = "License";
        /// <summary>
        /// 产品信息sheet名
        /// </summary>
        public static string ProductInfoInfo_sheet2 = "ProductInfo";
        /// <summary>
        /// 产品客户信息sheet名

        /// </summary>
        public static string ProductCustomerInfo_sheet2 = "ProductCustomerInfo";
        /// <summary>
        /// 成型机信息的sheet名

        /// </summary>
        public static string DeviceInfo_sheet2 = "DeviceInfo";

        /// <summary>
        /// 产品模具关系的sheet名

        /// </summary>
        public static string ProductMoldInfo_sheet2 = "ProductMoldInfo";
        /// <summary>
        /// 客户信息的sheet
        /// </summary>
        public static string CustomerInfo_sheet2 = "CustomerInfo";
        /// <summary>
        /// 模具信息的sheet
        /// </summary>
        public static string MoldInfo_sheet2 = "MoldInfo";

        public static string ProductStockInInfo_sheet2 = "ProductStockInInfo";

        /// <summary>
        /// 辅材信息sheet
        /// </summary>
        public static string AuxiliarymaterialInfo_sheet2 = "AuxiliarymaterialInfo";

        /// <summary>
        /// 产品辅材绑定信息sheet
        /// </summary>
        public static string AssistMaterialsbindingInfo_sheet2 = "AssistMaterialsbindingInfo";

        /// <summary>
        /// 月末在库数sheet
        /// </summary>
        public static string MaterialImportExcelList_sheet2 = "MaterialImport";

        /// <summary>
        /// 原料管理入库EXCEL导入sheet
        /// </summary>
        public static string MaterialManageImportExcel_sheet2 = "RawMaterialsStorage";
        
        #endregion

        #region 查询日期的定义

        /// <summary>
        /// 查询条件开始日期

        /// </summary>
        public static DateTime SearchStartDate = DateTime.Now.Date;
        /// <summary>
        /// 查询条件结束日期

        /// </summary>
        public static DateTime SearchEndDate = DateTime.Now.Date.AddDays(1);
        #endregion

        #endregion

    }

    public enum pFlag
    {
        /// <summary>
        /// 人员调入(新人调入，永久性调入)	
        /// </summary>
        pflag1 = 1,

        /// <summary>
        /// 2 人员调出(永久性调出)
        /// </summary>
        pflag2 = 2,

        /// <summary>
        /// 关位调整（永久关位调整）
        /// </summary>
        pflag3 = 3,

        /// <summary>
        /// 支援调出（临时）
        /// </summary>
        pflag4 = 4,

        /// <summary>
        /// 支援调入（临时）
        /// </summary>
        pflag5 = 5,

        /// <summary>
        /// 替关调整（临时）
        /// </summary>
        pflag6 = 6
    }

    public static class CommParg
    {

        public static frmMenu mfrmMenu;
    }
}
