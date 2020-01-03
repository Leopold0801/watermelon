using Amov.Planner.views;
using CCWin;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using MAVTOOL;
using MAVTOOL.Controls;
using MAVTOOL.mavlink;
using MAVTOOL.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MAVLink;
using static MAVTOOL.Device;

namespace Amov.Planner
{
    public partial class AmovPlanner : Skin_Mac
    {
        public delegate void WMDeviceChangeEventHandler(WM_DEVICECHANGE_enum cause);

        /// <summary>
        /// used to call anything as needed.
        /// </summary>
        public static AmovPlanner instance = null;
        public views.flightplanner FlightPlanner;

        SortedList<int, Device> SorlistDevice = new SortedList<int, Device>();

        public static Device dv1;//控制器读写设备类

        //public SetWayPointClass swpc = new SetWayPointClass();//航点图层操作显示类

        private String massag_error;

        GBL_FLAG gbl_flag;// 全局标志位变量表示逻辑处于哪种状态

        bool key_function_choose = false;//于激活方向键控制

        System.Timers.Timer show_waypoint = new System.Timers.Timer(1500);//实例化Timer类，设置间隔时间为1.5秒，每1.5秒触发实时路径显示

        Tool Tool = new Tool();//自动读取设备的一个类，可以实现软件打开自动扫描设备类。
        public AmovPlanner()
        {
           
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            gbl_flag = new GBL_FLAG();
            gbl_flag.flight = false;//地面站打开，系统默认为飞控不处于飞行状态
            gbl_flag.bRoutePlan = false;//地面站打开，系统默认飞控不处于航线规划状态
            massag_error = "";

            //show_waypoint.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
            //show_waypoint.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            //show_waypoint.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            FlightPlanner = new flightplanner();//实例化路径规划类
            devicecheck1.SuspendLayout();// 进入时挂起控件
            flightdata1.SuspendLayout();

            instance = this;

            //for (int i = 1; i <= 5; i++)//Tool类里面做了自动连接的配置，在Set文件中配置了连接类型，程序上电自动连接设备
            //{
            bool _have = false;
                int n = 0;
                for (n = 0; n < Tool.ConfSet.Count; n++)
                {
                //if (Tool.ConfSet[n].DeviceId == 1)
                //{
                    _have = true;
                        break;
                    //}
                }
                if (_have)
                {
                    Device dev = new Device();
                    if (Tool.ConfSet[n].IsComm)
                        dev.ConnectType = ConnectType.Com;
                    else
                        dev.ConnectType = ConnectType.Tcp;
                    dev.linkEvent += Dv1_linkEvent;
                    dev.Index = 1;
                    dev.ConnectState = Tool.ConfSet[n].IsConnect;
                    dev.ComPort = Tool.ConfSet[n].ComPort;

                    dev.IP = Tool.ConfSet[n].IP;
                    dev.Port = Tool.ConfSet[n].Port;
                    dev.Baud = 115200;
                    //if (dev.ConnectState)
                    //{
                    //    dev.Start();
                    //    dev.isOccupy = false;
                    //}
                    SorlistDevice.Add(1, dev);
                    //if (i == 1)
                    //{
                        dv1 = dev;
                    //}

                }
                else
                {
                    Device dev = new Device(ConnectType.Tcp);
                    dev.ConnectState = false;
                    dev.Index = 1;
                    dev.linkEvent += Dv1_linkEvent;
                    dev.Baud = 115200;
                    SorlistDevice.Add(1, dev);
                    Set se = new Set();
                    se.DeviceId = 1;
                    se.IsConnect = false;
                    //if (i == 1)
                    //{
                        dv1 = dev;
                    //}

                    Tool.ConfSet.Add(se);
                }
            //}

            //try
            //{
            //     = ActiveChildForm(dllFormNameWithNameSpace);
            //    if (child != null)
            //        return child;
            //    loading ld = new loading();
            //    Action handler = new Action();
            //    handler.BeginInvoke(null, null);  //在另外一个线程打开，否则会阻塞
            //    Form form = OpenPluginFormInMainDomain(dllFileSimpleName, dllFormNameWithNameSpace, initParam);

            //    if (form != null && form is Form)
            //    {
            //        child = form as Form;
            //        ((Fm11Base)child).RightsList = rightsList.ToLower();
            //        ((Fm11Base)child).OnLoadParams = onLoadParams;
            //        child.Text = tagTitle;
            //        child.MdiParent = (Form)this;
            //        child.FormClosed += Child_FormClosed;
            //        child.Show();
            //        child.WindowState = FormWindowState.Maximized;
            //        this.ActivateMdiChild(child);
            //        if (child.HasChildren)
            //        {
            //            child.Controls[0].Focus();
            //        }
            //        CloseLoadingForm();
            //        return child;
            //    }
            //    else
            //    {
            //        CloseLoadingForm();
            //        return null;
            //        throw new Exception("未找到窗体文件或加载了未知的窗体类型!");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    CloseLoadingForm();
            //    MyMsg.Information("窗体实例化出错,请重试.", ex.Message);
            //    return null;
            //}

        }

        public enum WM_DEVICECHANGE_enum
        {
            DBT_CONFIGCHANGECANCELED = 0x19,
            DBT_CONFIGCHANGED = 0x18,
            DBT_CUSTOMEVENT = 0x8006,
            DBT_DEVICEARRIVAL = 0x8000,
            DBT_DEVICEQUERYREMOVE = 0x8001,
            DBT_DEVICEQUERYREMOVEFAILED = 0x8002,
            DBT_DEVICEREMOVECOMPLETE = 0x8004,
            DBT_DEVICEREMOVEPENDING = 0x8003,
            DBT_DEVICETYPESPECIFIC = 0x8005,
            DBT_DEVNODES_CHANGED = 0x7,
            DBT_QUERYCHANGECONFIG = 0x17,
            DBT_USERDEFINED = 0xFFFF,
        }

        private void AmovPlanner_Load(object sender, EventArgs e)
        {
            AmovPlanner amov = new AmovPlanner();
            amov.WindowState = FormWindowState.Maximized;
            //************************************MAVLink对象* ****************************************
            //dv1 = new Device(ConnectType.Tcp);//打开了设备，创建了数据读取类和用于更新UI的事件和委托函数
            // dv1.linkEvent += Dv1_linkEvent;//绑定一个委托事件，用于读写线程( SerialReader)来触发这个UI显示事件     
            //this.Commandspanel.Visible = false  
            flightdata1.SuspendLayout();
            devicecheck1.SuspendLayout();
            flightdata1.Visible = false;
            devicecheck1.Visible = false;
            //System.Threading.Thread.Sleep(100000);占用较长时间
            //数据加载完成通过实践通知

            // SendMsg(sender, e);

        }

        //public void theout(object source, System.Timers.ElapsedEventArgs e)
        //{
        //    swpc.Enable_Draw = true;
        //    //  label14.Text = swpc.realRouteOverlay.Polygons.Count.ToString();
        //}


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

            flightdata1.pitchAndBank1.Bank = MAV.cs.roll;
            flightdata1.pitchAndBank1.Pitch = -MAV.cs.pitch;
            flightdata1.airSpeedIndicator1.AirSpeed = (int)MAV.cs.groundspeed;//地速airspeed 空速
            flightdata1.altitudeMeter1.Altitude = MAV.cs.alt;//高度
            flightdata1.LinkqualityGcsLab.Text = MAV.cs.linkqualitygcs.ToString() + "%";//显示数传连接质量

            flightdata1.lab_alt.Text = MAV.cs.alt.ToString("0.##");//高度
            flightdata1.lab_gs.Text = MAV.cs.groundspeed.ToString("0.##");//地速

            flightdata1.lab_wd.Text = MAV.cs.wp_dist.ToString("0.##");//航点距离

            flightdata1.lab_yaw.Text = MAV.cs.yaw.ToString("0.##");//偏航
            
            flightdata1.label33.Text = MAV.cs.mode;
            flightdata1.Gps_nmulabel.Text = MAV.cs.satcount.ToString();//显示GPS卫星数量
            flightdata1.Hdop_label.Text = MAV.cs.gpshdop.ToString();//显示GPS水平定位因子


            if (dv1.comPort.isconnect)
            {
                //Connect_button.BackgroundImage = global::Amov.Planner.Properties.Resources.链接;
            }


            if (MAV.cs.gpshdop <= 3)//如果定位因子小于3，载具才会在GPS上显示
            {
                //flightplanner1.gMapControl.Position = new PointLatLng(MAV.cs.lat, MAV.cs.lng);
                //if(vehiVleMarker)
                flightplanner1.swpcp.addvehiclemarker(MAV.cs.lat, MAV.cs.lng, MAV.cs.yaw);//在地图上显示载具
                flightdata1.gMap.Position = new PointLatLng(MAV.cs.lat, MAV.cs.lng);
                //if(vehiVleMarker)
                flightdata1.swpcd.addvehiclemarker(MAV.cs.lat, MAV.cs.lng, MAV.cs.yaw);//在地图上显示载具

            }
            if (massag_error != MAV.cs.messageHigh)//如果这次的报错和上次的不一样，就更新本次的报错内容，防止textbox重复循环打印报错
            {
                Error_box.AppendText("错误/警告:" + dv1.comPort.MAV.cs.messageHigh + "\r\n");

            }
            massag_error = MAV.cs.messageHigh;
            if (MAV.cs.gpshdop < 3 && MAV.cs.satcount > 6)//GPS信号比较良好的情况下实时显示载具轨迹
            {
               // swpc.DrawWaypoint(flightdata1.Position, swpc.realRouteOverlay);

            }

            if (MAV.cs.armed == true) Arm_button.BackgroundImage = global::Amov.Planner.Properties.Resources._7_1已解锁;

        }

        private void LinkqualityGcsLab_Click(object sender, EventArgs e)
        {

        }

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

        private void Land_Button_Click(object sender, EventArgs e)
        {

        }

        //返航
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

        private void flightdata_button_Click(object sender, EventArgs e)
        {
            flightdata1.ResumeLayout();
            if (flightdata1.Visible == true)
            {

            }
            else
            {
                //devicecheck1.Close();
                //devicecheck1.Dispose();
                // flightplanner1.SuspendLayout();

                devicecheck1.Visible = false;
                flightplanner1.Visible = false;
               // flightdata1.Refresh();
                flightdata1.Visible = true;
                //flightdata1.Focus();
            }
            

            //swpc.totalWPlist = flightplanner.Rwp;
            //swpc.reDrawAllWP();
            //swpc.reDrawAllRoute();

        }

        private ArrayList TabPageText = new ArrayList();//使用TabPageText保存所有tabpage的表头标题
                                                        //this.TabPageText.Add(this.tabpage1.Text);

        private void button1_Click(object sender, EventArgs e)
        {
            //  this.Commandspanel.Visible = false;
        }


        /*功能：地图规划航点，响应鼠标左键的释放事件
          *说明：只有GPS已经定位，家的位置已经获取，航点设置按键打开，才可以响应航点编辑。
          * 
          */
        private void gmap_SetwayPoint_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//点击鼠标左键
            {

            }
            if (e.Button == MouseButtons.Right)//鼠标右键出菜单
            {

            }
        }

        /*功能：地图规划航点，响应鼠标右键的释放事件
         *说明：只有GPS已经定位，家的位置已经获取，航点设置按键打开,GPS家的位置获取，才可以响应航点编辑。
         * 
         */




        //  public void setfromMap(double lat, double lng, int alt, double p1 = 0)
        //  {
        //      if (selectedrow > Commands.RowCount)
        //      {
        //          CustomMessageBox.Show("Invalid coord, How did you do this?");
        //          return;
        //      }
        //      MAVLinkInterface port = dv1.comPort;//得到MAVLINK类


        //      DataGridViewTextBoxCell cell;


        //          cell = Commands.Rows[selectedrow].Cells[5] as DataGridViewTextBoxCell;
        //          cell.Value = lat.ToString("0.0000000");
        //          cell.DataGridView.EndEdit();

        //     // if (Commands.Columns[Lon.Index].HeaderText.Equals("Long"))
        //      {
        //          cell = Commands.Rows[selectedrow].Cells[6] as DataGridViewTextBoxCell;
        //          cell.Value = lng.ToString("0.0000000");
        //          cell.DataGridView.EndEdit();
        //      }
        //      if (alt != -1 && alt != -2 &&
        //          Commands.Columns[Alt.Index].HeaderText.Equals("Alt"))
        //      {
        //          cell = Commands.Rows[selectedrow].Cells[Alt.Index] as DataGridViewTextBoxCell;

        //          {
        //              double result;
        //              bool pass = double.TryParse(TXT_DefaultAlt.Text, out result);

        //              if (pass == false)
        //              {
        //                  CustomMessageBox.Show("You must have a home altitude");
        //                  string homealt = "100";
        //                  if (DialogResult.Cancel == InputBox.Show("Home Alt", "Home Altitude", ref homealt))
        //                      return;
        //                  TXT_DefaultAlt.Text = homealt;
        //              }
        //              int results1;
        //              if (!int.TryParse(TXT_DefaultAlt.Text, out results1))
        //              {
        //                  CustomMessageBox.Show("Your default alt is not valid");
        //                  return;
        //              }

        //              if (results1 == 0)
        //              {
        //                  string defalt = "100";
        //                  if (DialogResult.Cancel == InputBox.Show("Default Alt", "Default Altitude", ref defalt))
        //                      return;
        //                  TXT_DefaultAlt.Text = defalt;
        //              }
        //          }

        //          cell.Value = TXT_DefaultAlt.Text;

        //          float ans;
        //          if (float.TryParse(cell.Value.ToString(), out ans))
        //          {
        //              ans = (int)ans;
        //              if (alt != 0) // use passed in value;
        //                  cell.Value = alt.ToString();
        //              if (ans == 0) // default
        //                  cell.Value = 50;


        //              if (ans == 0 && (port.MAV.cs.firmware == Firmwares.ArduCopter2))
        //                  cell.Value = 15;


        //              cell.DataGridView.EndEdit();
        //          }
        //          else
        //          {
        //              CustomMessageBox.Show("Invalid Home or wp Alt");
        //              cell.Style.BackColor = Color.Red;
        //          }
        //      }

        //      // convert to utm
        ////      convertFromGeographic(lat, lng);

        //      // Add more for other params
        //      if (Commands.Columns[Param1.Index].HeaderText.Equals("Delay"))
        //      {
        //          cell = Commands.Rows[selectedrow].Cells[Param1.Index] as DataGridViewTextBoxCell;
        //          cell.Value = p1;
        //          cell.DataGridView.EndEdit();
        //      }

        //   //   writeKML();
        //      Commands.EndEdit();
        // 



        //void saveWPs(object sender, ProgressWorkerEventArgs e, object passdata = null)
        //{
        //    MAVLinkInterface port = dv1.comPort;//得到MAVLINK类
        //    try
        //    {


        //        if (!port.BaseStream.IsOpen)
        //        {
        //            throw new Exception("Please connect first!");
        //        }

        //        port.giveComport = true;
        //        int a = 0;

        //        // define the home point
        //        Locationwp home = new Locationwp();
        //        try
        //        {
        //            home.id = (byte)MAVLink.MAV_CMD.WAYPOINT;//写入家的位置
        //            home.lat = gMap.Position.Lat;
        //            home.lng = gMap.Position.Lng;
        //            home.alt = 20 / CurrentState.multiplierdist; // use saved home
        //        }
        //        catch
        //        {
        //            throw new Exception("Your home location is invalid");
        //        }

        //        // log
        //        //log.Info("wps values " + MainV2.comPort.MAV.wps.Values.Count);
        //        //log.Info("cmd rows " + (Commands.Rows.Count + 1)); // + home

        //        // check for changes / future mod to send just changed wp's
        //        if (port.MAV.wps.Values.Count == (Commands.Rows.Count + 1))
        //        {
        //            Hashtable wpstoupload = new Hashtable();

        //            a = -1;
        //            foreach (var item in port.MAV.wps.Values)
        //            {
        //                // skip home
        //                if (a == -1)
        //                {
        //                    a++;
        //                    continue;
        //                }

        //                MAVLink.mavlink_mission_item_t temp = DataViewtoLocationwp(a);

        //                if (temp.command == item.command &&
        //                    temp.x == item.x &&
        //                    temp.y == item.y &&
        //                    temp.z == item.z &&
        //                    temp.param1 == item.param1 &&
        //                    temp.param2 == item.param2 &&
        //                    temp.param3 == item.param3 &&
        //                    temp.param4 == item.param4
        //                    )
        //                {
        //                    //   log.Info("wp match " + (a + 1));
        //                }
        //                else
        //                {
        //                    // log.Info("wp no match" + (a + 1));
        //                    wpstoupload[a] = "";
        //                }

        //                a++;
        //            }
        //        }

        //        bool use_int = (port.MAV.cs.capabilities & (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;

        //        // set wp total
        //        ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set total wps ");

        //        ushort totalwpcountforupload = (ushort)(Commands.Rows.Count + 1);

        //        if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
        //        {
        //            totalwpcountforupload--;
        //        }

        //        port.setWPTotal(totalwpcountforupload); // + home

        //        // set home location - overwritten/ignored depending on firmware.
        //        ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set home");

        //        // upload from wp0
        //        a = 0;

        //        if (port.MAV.apname != MAVLink.MAV_AUTOPILOT.PX4)
        //        {
        //            try
        //            {
        //                var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
        //                if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
        //                {
        //                    if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
        //                    {
        //                        CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
        //                        return;
        //                    }
        //                }
        //                a++;
        //            }
        //            catch (TimeoutException)
        //            {
        //                use_int = false;
        //                // added here to prevent timeout errors
        //                port.setWPTotal(totalwpcountforupload);
        //                var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
        //                if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
        //                {
        //                    if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
        //                    {
        //                        CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
        //                        return;
        //                    }
        //                }
        //                a++;
        //            }
        //        }
        //        else
        //        {
        //            use_int = false;
        //        }

        //        // define the default frame.
        //        MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

        //        // get the command list from the datagrid
        //        var commandlist = GetCommandList();

        //        // process commandlist to the mav
        //        for (a = 1; a <= commandlist.Count; a++)
        //        {
        //            var temp = commandlist[a - 1];

        //            ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(a * 100 / Commands.Rows.Count,
        //                "Setting WP " + a);

        //            // make sure we are using the correct frame for these commands
        //            if (temp.id < (ushort)MAVLink.MAV_CMD.LAST || temp.id == (ushort)MAVLink.MAV_CMD.DO_SET_HOME)
        //            {

        //                frame = MAVLink.MAV_FRAME.GLOBAL;

        //            }

        //            // handle current wp upload number
        //            int uploadwpno = a;
        //            if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
        //                uploadwpno--;

        //            // try send the wp
        //            MAVLink.MAV_MISSION_RESULT ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);

        //            // we timed out while uploading wps/ command wasnt replaced/ command wasnt added
        //            if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
        //            {
        //                // resend for partial upload
        //                port.setWPPartialUpdate((ushort)(uploadwpno), totalwpcountforupload);
        //                // reupload this point.
        //                ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);
        //            }

        //            if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE)
        //            {
        //                e.ErrorMessage = "Upload failed, please reduce the number of wp's";
        //                return;
        //            }
        //            if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
        //            {
        //                e.ErrorMessage =
        //                    "Upload failed, mission was rejected byt the Mav,\n item had a bad option wp# " + a + " " +
        //                    ans;
        //                return;
        //            }
        //            if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
        //            {
        //                // invalid sequence can only occur if we failed to see a response from the apm when we sent the request.
        //                // or there is io lag and we send 2 mission_items and get 2 responces, one valid, one a ack of the second send

        //                // the ans is received via mission_ack, so we dont know for certain what our current request is for. as we may have lost the mission_request

        //                // get requested wp no - 1;
        //                a = port.getRequestedWPNo() - 1;

        //                continue;
        //            }
        //            if (ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
        //            {
        //                e.ErrorMessage = "Upload wps failed " + Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()) +
        //                                 " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString());
        //                return;
        //            }
        //        }

        //        port.setWPACK();

        //        ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(95, "Setting params");

        //        // m
        //        port.setParam("WP_RADIUS", float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist);

        //        // cm's
        //        port.setParam("WPNAV_RADIUS", float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist * 100.0);

        //        try
        //        {
        //            port.setParam(new[] { "LOITER_RAD", "WP_LOITER_RAD" },
        //               60f / CurrentState.multiplierdist);
        //        }
        //        catch
        //        {
        //        }

        //        ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(100, "Done.");
        //    }
        //    catch (Exception ex)
        //    {
        //        port.giveComport = false;//com口释放标志，读线程运行
        //        throw;
        //    }

        //    port.giveComport = false;//com口释放标志，读线程运行
        //}




        private void BUT_back_Mouse(object sender, MouseEventArgs e)
        {
            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();


            rc.chan1_raw = (ushort)1480;

            rc.chan2_raw = (ushort)1480;

            //ushort number = ushort.Parse(ch_textBox.Text);

            //   rc.chan3_raw = (ushort)(1480 - number);

            rc.target_component = dv1.comPort.MAV.compid;
            rc.target_system = dv1.comPort.MAV.sysid;
            dv1.comPort.sendPacket(rc, rc.target_system, rc.target_component);//发送RC的mavlink消息指令包
            System.Threading.Thread.Sleep(20);

        }
        private void BUT_right_Mouse(object sender, MouseEventArgs e)
        {
            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();

            //ushort number = ushort.Parse(ch_textBox.Text);

            // rc.chan1_raw = (ushort)(1480 + number);

            rc.chan2_raw = (ushort)1480;

            rc.chan3_raw = (ushort)1480;

            rc.target_component = dv1.comPort.MAV.compid;
            rc.target_system = dv1.comPort.MAV.sysid;
            dv1.comPort.sendPacket(rc, rc.target_system, rc.target_component);//发送RC的mavlink消息指令包
            System.Threading.Thread.Sleep(20);
        }
        private void BUT_left_Mouse(object sender, MouseEventArgs e)
        {
            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();

            // ushort number = ushort.Parse(ch_textBox.Text);

            //  rc.chan1_raw = (ushort)(1480 - number);

            rc.chan2_raw = (ushort)1480;

            rc.chan3_raw = (ushort)1480;

            rc.target_component = dv1.comPort.MAV.compid;
            rc.target_system = dv1.comPort.MAV.sysid;
            dv1.comPort.sendPacket(rc, rc.target_system, rc.target_component);//发送RC的mavlink消息指令包
            System.Threading.Thread.Sleep(20);
        }
        private void BUT_front_Mouse(object sender, MouseEventArgs e)
        {
            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();

            rc.chan1_raw = (ushort)1480;

            rc.chan2_raw = (ushort)1480;

            // ushort number = ushort.Parse(ch_textBox.Text);

            //  rc.chan3_raw = (ushort)(1480 + number);

            rc.target_component = dv1.comPort.MAV.compid;
            rc.target_system = dv1.comPort.MAV.sysid;
            dv1.comPort.sendPacket(rc, rc.target_system, rc.target_component);//发送RC的mavlink消息指令包
            System.Threading.Thread.Sleep(20);

        }

        private void Function_Key_Up()
        {
            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();
            rc.chan1_raw = (ushort)1480;

            rc.chan2_raw = (ushort)1480;

            rc.chan3_raw = (ushort)1480;

            rc.target_component = dv1.comPort.MAV.compid;
            rc.target_system = dv1.comPort.MAV.sysid;
            dv1.comPort.sendPacket(rc, rc.target_system, rc.target_component);//发送RC的mavlink消息指令包
            System.Threading.Thread.Sleep(20);
        }

        private void BUT_back_Up(object sender, MouseEventArgs e)
        {
            Function_Key_Up();
        }
        private void BUT_right_Up(object sender, MouseEventArgs e)
        {
            Function_Key_Up();
        }
        private void BUT_left_Up(object sender, MouseEventArgs e)
        {
            Function_Key_Up();
        }
        private void BUT_front_Up(object sender, MouseEventArgs e)
        {
            Function_Key_Up();
        }

        private void SetModebutton_Click(object sender, EventArgs e)
        {
            if (dv1.comPort.MAV.cs.failsafe)
            {
                if (CustomMessageBox.Show("You are in failsafe, are you sure?", "Failsafe", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }
            //  dv1.comPort.setMode(ModecomboBox.Text);
        }

        private void devicecheck_Click(object sender, EventArgs e)
        {
            devicecheck1.ResumeLayout();
            if (devicecheck1.Visible == true)
            {

            }
            else
            {
                //flightplanner1.Close();
                //flightplanner1.Dispose();
                flightdata1.Visible = false;
                flightplanner1.Visible = false;
                devicecheck1.Visible = true;
            }
        }

        private void Waypoint_button_Click(object sender, EventArgs e)
        {
            if (flightplanner1.Visible == true)
            {

            }
            else
            {
                flightdata1.Visible = false;
                devicecheck1.Visible = false;
                flightplanner1.Visible = true;
            }
        }

        private void but_connect_Click(object sender, EventArgs e)
        {
            Device dv = SorlistDevice[1];
            dv1 = dv;
            ConnectForm connectForm = new ConnectForm(dv1);
            connectForm.ShowDialog();
            for (int i = 0; i < Tool.ConfSet.Count; i++)
            {
                Set set = Tool.ConfSet[i];
                if (set.DeviceId == 1)
                {
                    set.IP = dv.IP;
                    set.Port = dv.Port;
                    set.IsConnect = dv.ConnectState;
                    set.IsComm = dv.ConnectType == ConnectType.Com ? true : false;
                    set.ComPort = dv.ComPort;
                    Tool.Serializer();
                }
                //if (DialogResult == DialogResult.OK)
                //{
                //    but_connect.BackColor = System.Drawing.Color.Green;
                //}
                //else
                //    but_connect.BackColor = System.Drawing.Color.Red;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //for (int i = 1; i <= SorlistDevice.Count; i++)
            //{
            Device dv = SorlistDevice.Values[0];
            if (dv.ConnectState)
            {
                if (dv.Index == 1)
                {
                    if (dv.IsConnect)
                    {
                        but_connect.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        if (!dv.isOccupy)
                        {
                            dv.Stop();
                            dv.Start();
                        }
                        but_connect.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                if (dv.Index == 1)
                    but_connect.BackColor = System.Drawing.Color.Red;


            }
            //}
        }

        //窗体关闭事件
        private void AmovPlanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出吗?", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                Dispose();
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }

        }

     
    }
}

    