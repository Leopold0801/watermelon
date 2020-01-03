using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MAVTOOL.Controls;

namespace Amov.Planner.views
{
    public partial class devicecheck : MyUserControl, IActivate, IDeactivate
    {
        public devicecheck()
        {
            InitializeComponent();
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuStrip1.Items[0].ForeColor = System.Drawing.Color.Green;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menuStrip1.Items[0].ForeColor = System.Drawing.Color.Black;
        }

        private void tsm_duoji_Click(object sender, EventArgs e)
        {
            // groupBox1.Visible = true;
            panel2.BringToFront();
        }

        private void 设备检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            menuStrip1.Items[1].ForeColor = System.Drawing.Color.Green;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            menuStrip1.Items[1].ForeColor = System.Drawing.Color.Black;
        }
    }
}
