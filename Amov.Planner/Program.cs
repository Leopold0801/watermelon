using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amov.Planner
{
 
    static class Program
    {

        //internal static Thread Thread;
        //public static loading loading;
        //public static DateTime starttime = DateTime.Now;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
       {
            //AmovPlanner amov = new AmovPlanner();
            //Thread threadl = new Thread(new ThreadStart(Start_Load));
            //threadl.Start();            
            //Thread threada = new Thread(new ThreadStart(start_amov));
            //threada.Start();
            //threada.Join();

            //threadl.Abort();
            //threada.Abort();
            Application.EnableVisualStyles();
               Application.SetCompatibleTextRenderingDefault(false);
               Application.Run(new AmovPlanner()); //运行主窗体

            //System.Windows.Forms.Application.ThreadException += Application_ThreadException;  //捕获线程异常

            // loading loading = new loading();
            // loading.Show();
            // Application.DoEvents();//在加载大量数据时能够有一个数据加载的提示，不至于系统出现假死的现象
            // Application.DoEvents();
            // try
            //  {
            //System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            //      Thread.CurrentThread.Name = "Base Thread";

            // }
            //  catch (Exception ex)
            //  {
            // log.Fatal("Fatal app exception", ex);
            //   Console.WriteLine(ex.ToString());

            //   Console.WriteLine("\nPress any key to exit!");
            //    Console.ReadLine();
            //  }


        }

        //static void start_amov()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new AmovPlanner()); //运行主窗体
        //}

        //static void Start_Load()
        //{
        //    loading loading = new loading();
        //    loading.Show();
        //}



    }
}
