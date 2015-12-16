using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Framework.Abstract;

namespace Framework.Libs
{
    public partial class Common 
    {

        //2007/12/27保存listview的新布局
        public static void SaveLayout(DevExpress.XtraGrid.Views.Grid.GridView ListView)
        {
            try
            {
                DevExpress.Utils.OptionsLayoutGrid opt = new DevExpress.Utils.OptionsLayoutGrid();
                opt.StoreAllOptions = false;
                opt.Columns.StoreAllOptions = false;
                opt.Columns.StoreLayout = true;
                //opt.Columns.StoreAppearance = true;
                opt.StoreVisualOptions = true;
                opt.StoreDataSettings = false;
                ListView.OptionsLayout.Assign(opt);
                ListView.SaveLayoutToXml(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm().Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml");

            }
            catch (Exception ex)
            {
                //TODO::sysParameter.MessageInf
                XtraMsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static void SaveLayout(DevExpress.XtraGrid.Views.Grid.GridView ListView, string PageName, string frmName)
        {
            try
            {
                DevExpress.Utils.OptionsLayoutGrid opt = new DevExpress.Utils.OptionsLayoutGrid();
                opt.StoreAllOptions = false;
                opt.Columns.StoreAllOptions = true;
                opt.Columns.StoreLayout = true;
                opt.Columns.StoreAppearance = true;
                opt.StoreVisualOptions = true;
                opt.StoreDataSettings = false;
                ListView.OptionsLayout.Assign(opt);
                ListView.SaveLayoutToXml(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" 
                    + Application.ProductName + "."
                    + frmName + "."
                    + PageName + "."
                    + ListView.GridControl.Name + "." + ListView.Name + ".xml");

            }
            catch (Exception ex)
            {
                //TODO::sysParameter.MessageInf
                XtraMsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static void SaveLayout(DevExpress.XtraGrid.Views.Grid.GridView ListView, string frmName)
        {
            try
            {
                DevExpress.Utils.OptionsLayoutGrid opt = new DevExpress.Utils.OptionsLayoutGrid();
                opt.StoreAllOptions = false;
                opt.Columns.StoreAllOptions = true;
                opt.Columns.StoreLayout = true;
                opt.Columns.StoreAppearance = true;
                opt.StoreVisualOptions = true;
                opt.StoreDataSettings = false;
                ListView.OptionsLayout.Assign(opt);
                ListView.SaveLayoutToXml(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm().Name + "." + frmName + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml");

            }
            catch (Exception ex)
            {
                //TODO::sysParameter.MessageInf
                XtraMsgBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static void ReStoreLayOut(DevExpress.XtraGrid.Views.Grid.GridView ListView, string frmName)
        {
            if (System.IO.File.Exists(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm ().Name + "." + frmName + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
                ListView.RestoreLayoutFromXml(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm ().Name + "." + frmName + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml");
        }

        public static void ReStoreLayOut(DevExpress.XtraGrid.Views.Grid.GridView ListView)
        {
            if (System.IO.File.Exists(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm ().Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
                ListView.RestoreLayoutFromXml(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm ().Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml");
        }

        public static void ReStoreLayOut(DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView ListView)
        {
            if (System.IO.File.Exists(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm ().Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
                ListView.RestoreLayoutFromXml(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" + Application.ProductName + "." + ListView.GridControl.FindForm ().Name + "." + ListView.GridControl.Name + "." + ListView.Name + ".xml");
        }

        public static void SaveFormLayOut(Form frmStance, DevExpress.XtraLayout.LayoutControl layOut, Control grpHead)
        {
            //layOut.SaveLayoutToXml(Common.sysParameter.FormLayOutPath + "\\" + Application.ProductName + "."  + frmStance.Name  + ".xml");
            Common._rwconfig.WriteTextFile(frmStance.Name, "grpHead", grpHead.Height.ToString());

        }

        public static void RestoreFormLayOut(Form frmStance, DevExpress.XtraLayout.LayoutControl layOut, Control grpHead)
        {
            //if (System.IO.File.Exists(Common.sysParameter.FormLayOutPath + "\\" + Application.ProductName + "." + frmStance.Name + ".xml"))
            //   layOut.RestoreLayoutFromXml(Common.sysParameter.FormLayOutPath + "\\" + Application.ProductName + "." + frmStance.Name + ".xml");
            string strLocation = frmStance.Name;// Common.GetLanguageWord(frmStance.Name, "grphead");
            if (!strLocation.Equals("0"))
                grpHead.Height = Convert.ToInt32(strLocation);
        }

        public static void ReStoreLayOut(DevExpress.XtraGrid.Views.Grid.GridView ListView, string PageName, string frmName)
        {
            if (System.IO.File.Exists(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\" 
                                    + Application.ProductName + "."
                                    + frmName + "."
                                    + PageName + "."
                                    + ListView.GridControl.Name + "." + ListView.Name + ".xml"))
                ListView.RestoreLayoutFromXml(Common.GetSolutionPath(Application.StartupPath) + @"GridLayout\"
                                    + Application.ProductName + "."
                                    + frmName + "."
                                    + PageName + "."
                                    + ListView.GridControl.Name + "." + ListView.Name + ".xml");
        }
    
    }
}
