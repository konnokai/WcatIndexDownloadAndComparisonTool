using System;
using System.Windows.Forms;

namespace 清單比對下載器
{
    static class Program
    {
        //CopyRight 孤之界 2018/01
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
