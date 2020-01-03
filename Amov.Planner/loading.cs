using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Amov.Planner
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
          //  FileWatchEvent += new SendDataLoadFinishMsg(ListenEvent);
        }

        //private void ListenEvent(object sender, EventArgs e)
        //{
        //    this.Close();//将当前窗体隐藏或关闭
        //}

        //打开函数
        private void ShowLoadingForm()
        {
            if (Debugger.IsAttached)
            {
                return;
            }
            loading fm20Loading = new loading
            {
                Name = "loading" + DateTime.Now.Ticks
            };
            Thread.Sleep(100);
            fm20Loading.ShowDialog();
            return;
        }

        //关闭函数
        private void CloseLoadingForm()
        {
            if (Debugger.IsAttached) return;
            for (int i = (Application.OpenForms.Count - 1); i >= 0; i--)
            {
                Form tForm = Application.OpenForms[i];
                string fmName = tForm.GetType().Name;
                if (fmName.Substring(11) == "loading")
                {
                    tForm.Close();
                }
            }
        }

    }
}
