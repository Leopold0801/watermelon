using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            basicFlightInfo1.Pitch = trackBar2.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            basicFlightInfo1.Bank = trackBar1.Value;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            basicFlightInfo1.AirSpeed = trackBar3.Value;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            basicFlightInfo1.Altitude = trackBar4.Value;
        }
    }
}
