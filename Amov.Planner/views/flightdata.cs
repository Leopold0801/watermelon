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
using GMap.NET;
using GMap.NET.WindowsForms;
using static MAVLink;
using MAVTOOL;
using MAVTOOL.mavlink;
using GMap.NET.MapProviders;

namespace Amov.Planner.views
{
    public partial class flightdata : MyUserControl, IActivate, IDeactivate
    {
        SortedList<int, Device> SorlistDevice = new SortedList<int, Device>();

        public static flightdata instance;
        //  public static GMapControl mymap;
        public Device dv1;//控制器读写设备类

        public SetWayPointClass swpcd = new SetWayPointClass();//航点图层操作显示类

        private String massag_error;

        GBL_FLAG gbl_flag;// 全局标志位变量表示逻辑处于哪种状态

        bool key_function_choose = false;//于激活方向键控制

        System.Timers.Timer show_waypoint = new System.Timers.Timer(1500);//实例化Timer类，设置间隔时间为1.5秒，每1.5秒触发实时路径显示

        Tool Tool = new Tool();//自动读取设备的一个类，可以实现软件打开自动扫描设备类。

        public flightdata()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            gbl_flag = new GBL_FLAG();
            gbl_flag.flight = false;//地面站打开，系统默认为飞控不处于飞行状态
            gbl_flag.bRoutePlan = false;//地面站打开，系统默认飞控不处于航线规划状态
            massag_error = "";

            show_waypoint.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
            show_waypoint.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            show_waypoint.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；


            //for (int i = 1; i <= 5; i++)//Tool类里面做了自动连接的配置，在Set文件中配置了连接类型，程序上电自动连接设备
            //{
            //    bool _have = false;
            //    int n = 0;
            //    for (n = 0; n < Tool.ConfSet.Count; n++)
            //    {
            //        if (Tool.ConfSet[n].DeviceId == i)
            //        {
            //            _have = true;
            //            break;
            //        }
            //    }
            //    if (_have)
            //    {
            //        Device dev = new Device();
            //        if (Tool.ConfSet[n].IsComm)
            //            dev.ConnectType = ConnectType.Com;
            //        else
            //            dev.ConnectType = ConnectType.Tcp;
            //        dev.linkEvent += Dv1_linkEvent;
            //        dev.Index = i;
            //        dev.ConnectState = Tool.ConfSet[n].IsConnect;
            //        dev.ComPort = Tool.ConfSet[n].ComPort;

            //        dev.IP = Tool.ConfSet[n].IP;
            //        dev.Port = Tool.ConfSet[n].Port;
            //        dev.Baud = 115200;
            //        if (dev.ConnectState)
            //        {
            //            dev.Start();
            //            dev.isOccupy = false;
            //        }
            //        SorlistDevice.Add(i, dev);
            //        if (i == 1)
            //        {
            //            dv1 = dev;
            //        }

            //    }
            //    else
            //    {
            //        Device dev = new Device(ConnectType.Tcp);
            //        dev.ConnectState = false;
            //        dev.Index = i;
            //        dev.linkEvent += Dv1_linkEvent;
            //        dev.Baud = 115200;
            //        SorlistDevice.Add(i, dev);
            //        Set se = new Set();
            //        se.DeviceId = i;
            //        se.IsConnect = false;
            //        if (i == 1)
            //        {
            //            dv1 = dev;
            //        }

            //        Tool.ConfSet.Add(se);
            //    }
            //}
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate()
        {
            throw new NotImplementedException();
        }

        private void flightdata_Load(object sender, EventArgs e)
        {
            AmovPlanner amov = new AmovPlanner();
            amov.WindowState = FormWindowState.Maximized;
            FlightData_TrackBar.Minimum = gMap.MapProvider.MinZoom;
            FlightData_TrackBar.Maximum = 24;
            FlightData_TrackBar.Value = (float)gMap.Zoom;
            //************************************MAVLink对象* ****************************************
            //dv1 = new Device(ConnectType.Tcp);//打开了设备，创建了数据读取类和用于更新UI的事件和委托函数
            // dv1.linkEvent += Dv1_linkEvent;//绑定一个委托事件，用于读写线程( SerialReader)来触发这个UI显示事件     
            //this.Commandspanel.Visible = false;

        }
       
        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            swpcd.Enable_Draw = true;
            //  label14.Text = swpc.realRouteOverlay.Polygons.Count.ToString();
        }

        /// <summary>
        /// 设置飞行参数
        /// </summary>
        /// <param name="MAV"></param>
        /// <param name="IsCome"></param>
        /// <param name="Ip"></param>
        private void Dv1_linkEvent(MAVState MAV, bool IsCome, string Ip)
        {

            if (IsCome)
            {
                if (Ip != dv1.ComPort)
                {
                    return;
                }
            }
            else
            {
                if (Ip != dv1.IP)
                {
                    return;
                }
            }
            pitchAndBank1.Bank = MAV.cs.roll;
            pitchAndBank1.Pitch = -MAV.cs.pitch;
            airSpeedIndicator1.AirSpeed = (int)MAV.cs.groundspeed;
            altitudeMeter1.Altitude = MAV.cs.alt;
   

          
            //System_modelabel.Text = MAV.cs.mode;

            //Firmware_label.Text = MAV.cs.firmware.ToString();


            //if (dv1.comPort.isconnect)
            //{
            //    //Connect_button.BackgroundImage = global::Amov.Planner.Properties.Resources.链接;
            //}

            //LinkqualityGcsLab.Text = MAV.cs.linkqualitygcs.ToString() + "%";//显示数传连接质量

            //Gps_nmulabel.Text = MAV.cs.satcount.ToString();//显示GPS卫星数量
            //Hdop_label.Text = MAV.cs.gpshdop.ToString();//显示GPS水平定位因子

            //if (MAV.cs.gpshdop <= 3)//如果定位因子小于3，载具才会在GPS上显示
            //{
            //    gMap.Position = new PointLatLng(MAV.cs.lat, MAV.cs.lng);
            //    swpc.addvehiclemarker(MAV.cs.lat, MAV.cs.lng, MAV.cs.yaw);//在地图上显示载具
            //}
            //if (massag_error != MAV.cs.messageHigh)//如果这次的报错和上次的不一样，就更新本次的报错内容，防止textbox重复循环打印报错
            //{
            //    Error_box.AppendText("错误/警告:" + dv1.comPort.MAV.cs.messageHigh + "\r\n");

            //}
            //massag_error = MAV.cs.messageHigh;
            //if (MAV.cs.gpshdop < 3 && MAV.cs.satcount > 6)//GPS信号比较良好的情况下实时显示载具轨迹
            //{
            //    swpc.DrawWaypoint(gMap.Position, swpc.realRouteOverlay);

            //}

            //if (MAV.cs.armed == true) Arm_button.BackgroundImage = global::Amov.Planner.Properties.Resources._7_1已解锁;
        }

        //解锁按键事件
        private void Arm_button_Click(object sender, EventArgs e)
        {
            if (!dv1.comPort.BaseStream.IsOpen)
                return;

            // arm the MAV
            try
            {
                if (dv1.comPort.MAV.cs.armed)
                    if (CustomMessageBox.Show("Are you sure you want to Disarm?", "Disarm?", MessageBoxButtons.YesNo) !=
                        DialogResult.Yes)
                        return;

                bool ans = dv1.comPort.doARM(!dv1.comPort.MAV.cs.armed);//设置解锁上锁
                if (ans == false)
                    CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
            }
            catch
            {
                CustomMessageBox.Show(Strings.ErrorNoResponce, Strings.ERROR);
            }
        }

        //一键返航事件
        private void Rtl_button_Click(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).Enabled = false;
                dv1.comPort.setMode("RTL");//设置一键返航

                //mavlink_set_mode_t mode = new mavlink_set_mode_t();
                //mode.base_mode = 1;
                //mode.custom_mode = 1;//这个值来设置模式切换，自稳，增稳.....
                //dv1.comPort.setMode(dv1.comPort.MAV.sysid, dv1.comPort.MAV.compid,mode,0);
            }
            catch
            {
                CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR);
            }
           ((Button)sender).Enabled = true;
        }

        //自动模式事件
        private void Auto_button_Click(object sender, EventArgs e)
        {
            try
            {
                ((Button)sender).Enabled = false;
                dv1.comPort.setMode("Auto");//设置自动模式
            }
            catch
            {
                CustomMessageBox.Show(Strings.CommandFailed, Strings.ERROR);
            }
          ((Button)sender).Enabled = true;
        }

        //地图加载事件
        public const double BEIJING_LAT = 39.958436;
        public const double BEIJING_LNG = 116.309175;
        private void gMap_Load(object sender, EventArgs e)
        {
            //************************************主界面GMAP地图的加载项目 * ****************************************
            GMaps.Instance.PrimaryCache = new MyImageCache();
            //gMap.MapProvider = GMap.NET.MapProviders.AMapProvider.Instance; ;//选择地图为高德卫星地图(GMap.NET.MapProviders.GoogleChinaMapProvider.Instance);
            //gMap.MapProvider = GMap.NET.MapProviders.GoogleChinaSatelliteMapProvider.Instance;
            //gMap.MapProvider = GMap.NET.MapProviders.GoogleChinaTerrainMapProvider.Instance;
            gMap.MapProvider = GMap.NET.MapProviders.GoogleChinaMapProvider.Instance;
            //谷歌地图的地图更新最好，一般的地面站都选用谷歌中国卫星地图
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            //gMap.SetPositionByKeywords("Chengdu, China");//设定初始中心位置为Chengdu，成都坐标为西纬30.67度，东经104.06
            gMap.Position = new PointLatLng(BEIJING_LAT, BEIJING_LNG);//位置北京
            gMap.ShowCenter = true;//不显示中心的红色十字
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left;  //左键拖动地图            
            //设置地图分辨率信息
            gMap.MaxZoom = 30;
            gMap.MinZoom = 3;
            gMap.Zoom = 15;

            //创建一个飞行动画层，用于动态显示飞行器的飞行状态
            Bitmap bitmap = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);

            swpcd.realVehicleOverlay = new GMapOverlay("realvehicle");
            gMap.Overlays.Add(swpcd.realVehicleOverlay);


            //创建实时航路层，用于显示实时航线
            swpcd.realRouteOverlay = new GMapOverlay("realroute");
            gMap.Overlays.Add(swpcd.realRouteOverlay);

            //创建一个marker层，用于标记航点
            swpcd.markersOverlay_sec = new GMapOverlay("markers_sec");
            gMap.Overlays.Add(swpcd.markersOverlay_sec);

            

        }

        private void flightdata_VisibleChanged(object sender, EventArgs e)
        {
            if(this.Visible == true)
            {
                swpcd.totalWPlist = flightplanner.Rwp;
                swpcd.reDrawAllWP();
                swpcd.reDrawAllRoute();
                gMap.MapProvider = flightplanner.Gp;
            }
        }

        private void FlightData_TrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (gMap.MaxZoom + 1 == (double)FlightData_TrackBar.Value)
                {
                    gMap.Zoom = FlightData_TrackBar.Value - .1;
                }
                else
                {
                    gMap.Zoom = FlightData_TrackBar.Value;
                }

               // UpdateOverlayVisibility();
            }
            catch
            {
            }
        }
    }
}
