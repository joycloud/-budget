using GonGinLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUR2007NET
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //----------------------------------------------------------------------------------------
            //              資料環境建立及取得
            //----------------------------------------------------------------------------------------
            GonGinSystemEnquipment SystemEnquipment = new GonGinSystemEnquipment();

            // 取得註冊檔資料(公邦準IP及資料庫名)
            if (!SystemEnquipment.GetLoginRegistry())
            {
                MessageBox.Show("無法由註冊檔取得執行環境設定值,請更新登入程式或請與資訊室連絡 !!", "訊息提示",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            // 取得資料庫連絡字串,並傳遞給 GonGinVariable.SqlConnectString 變數
            SystemEnquipment.GetSqlConnectString(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "(" + System.Environment.MachineName + ")");

            // 取得命令上的參數值
            // Y Y Y Y Y Y Y 20030101 1439
            if (args.Length > 0)
            {
                GonGinVariable.AuthorizatioString = args[0].ToString() + args[1].ToString() + args[2].ToString() + args[3].ToString() + args[4].ToString() + args[5].ToString() + args[6].ToString(); // YYYYYYY
                GonGinVariable.ApplicationUser = args[8].ToString(); // 系統使用者
                if (args[7].ToString() != "20030101")
                {
                    Application.Exit();
                }
                // 取得系統操作者姓名
                GonGinCheckOfDataDuplication GetUserName = new GonGinCheckOfDataDuplication(GonGinVariable.SqlConnectString.ToString(), "PERSON", "PNAME", "PNAME", "PNAME", "PENNO = '" + GonGinVariable.ApplicationUser + "'");
                GonGinVariable.ApplicationUserName = GetUserName.傳回值;
                // 執行表單
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PUR2007F());
            }
            else
            {
                // 2593、1819、3292
                // 允許某些電腦不用帶DOS參數即可不受權限控管(在設計時方便)
                if (System.Environment.MachineName.CompareTo("AC1297") == 0 || System.Environment.MachineName.CompareTo("AC2155-6") == 0)
                {
                    GonGinVariable.AuthorizatioString = "YYYYYYY";
                    GonGinVariable.ApplicationUser = "1819";
                    GonGinVariable.ApplicationUserName = "常唐寶";
                }
                else
                {
                    GonGinVariable.AuthorizatioString = "NNNNNNN";
                    GonGinVariable.ApplicationUser = "XXXX";
                    GonGinVariable.ApplicationUserName = "XXXXXXXX";
                }

                // 取得系統操作者姓名
                GonGinLibrary.GonGinGetPERSON GetUserName = new GonGinGetPERSON(GonGinVariable.SqlConnectString, GonGinVariable.ApplicationUser);
                GonGinVariable.ApplicationUserName = GetUserName.員工姓名;
                GonGinVariable.ApplicationUserDeptNo = GetUserName.部門編號;
                GonGinVariable.ApplicationUserDeptName = GetUserName.部門名稱;

                // 程式設計階段時執行用 ( 設計階段時先將註記拿掉 )
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PUR2007F());
            }
        }
    }
}
