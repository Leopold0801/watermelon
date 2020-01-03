using CCWin;
using GMap.NET.WindowsForms;
using MAVTOOL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amov.Planner
{
    public partial class ConnectForm :Skin_Mac
    {
        public ConnectForm(Device _dv)
        {
            dv = _dv;
            InitializeComponent();
           
        }
        Device dv;
        private void ConnectForm_Load(object sender, EventArgs e)
        {

            if (dv.ConnectType == ConnectType.Com)
            {
                comboBox2.Visible = true;
                comboBox2.DataSource = SerialPort.GetPortNames();
                comboBox2.Text = dv.ComPort;
                label1.Visible = false;
                label2.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                this.Text = "COM";
                comboBox1.Text = "Com";
            }
            else
            {
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                comboBox2.Text = dv.ComPort;
                label1.Visible = true;
                label2.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                this.Text = "TCP";
                comboBox1.Text = "TCP";
                if (!string.IsNullOrEmpty(dv.IP))
                {
                    textBox1.Text = dv.IP;
                    textBox2.Text = dv.Port.ToString();
                }
            }
           
            if (dv.IsConnect)
            {
                onlinebutton.Enabled = false;
                offlinebutton.Enabled = true;
            }
            else
            {
                onlinebutton.Enabled = true;
                offlinebutton.Enabled = false;
            }
        }

        private void Onlinebutton_Click(object sender, EventArgs e)
        {
            //dv.Stop();

            if (comboBox1.Text == "TCP")
            {
                dv.ConnectType = ConnectType.Tcp;
                dv.IP = this.textBox1.Text.Trim();
                dv.Port = Convert.ToInt32(this.textBox2.Text.Trim());
                dv.ConnectState = true;
                DialogResult = DialogResult.OK;
            }
            else
            {
                dv.ConnectType = ConnectType.Com;
                dv.ComPort = comboBox2.Text;//从Combox控件里面获取端口名称
                dv.Baud = Convert.ToInt32(comboBox3.Text);
                dv.ConnectState = true;
                DialogResult = DialogResult.OK;
            }
            //dv.Start();
            //if (!dv.IsConnect)
            //{
            //    MessageBox.Show("连接失败！");
            //}


        }

        public GMapMarker vehiVleMarker;//显示载具类型

        public GMapOverlay realRouteOverlay;//动态显示飞行器层
        private void Offlinebutton_Click(object sender, EventArgs e)
        {
            dv.Stop();
            //realRouteOverlay.Markers.Remove(vehiVleMarker);
            //vehiVleMarker = null; 
           

            if (comboBox1.Text == "TCP")
            {
                dv.ConnectType = ConnectType.Tcp;
                dv.IP = this.textBox1.Text.Trim();
                dv.Port = Convert.ToInt32(this.textBox2.Text.Trim());
                dv.ConnectState = false;
                DialogResult = DialogResult.OK;
            }
            else
            {
                dv.ConnectType = ConnectType.Com;
                dv.ComPort = comboBox2.Text;//从Combox控件里面获取端口名称
                dv.Baud = 115200;
                dv.ConnectState = false;
                
                DialogResult = DialogResult.OK;
            }


         
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "TCP")
            {
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox2.DataSource = SerialPort.GetPortNames();
                label1.Visible = false;
                label2.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                this.Text = "COM";

            }
            else
            {
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                label1.Visible = true;
                label2.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                this.Text = "TCP";


            }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "TCP")
            {
                dv.ConnectType = ConnectType.Tcp;
                dv.IP = this.textBox1.Text.Trim();
                dv.Port = Convert.ToInt32(this.textBox2.Text.Trim());
              
                DialogResult = DialogResult.OK;
            }
            else
            {
                dv.ConnectType = ConnectType.Com;
                dv.ComPort = comboBox2.Text;//从Combox控件里面获取端口名称
                dv.Baud = 115200;
              
                DialogResult = DialogResult.OK;
            }
        }
    }
}
