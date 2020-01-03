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
using MAVTOOL;
using GMap.NET.WindowsForms;
using MAVTOOL.Utilities;
using GMap.NET.MapProviders;
using MAVTOOL.mavlink;
using System.Collections;
using static MAVLink;
using GMap.NET.WindowsForms.Markers;
using MAVTOOL.Comms;
using System.Drawing.Drawing2D;
using System.IO;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using Amov.Planner.Utilities;
using System.Diagnostics;
using GeoUtility.GeoSystem;
using Amov.Planner.Properties;
using Amov.Planner.grid;
using Amov.Planner.Plugin;
using AmovPlanner;
using System.Globalization;

namespace Amov.Planner.views
{
    public partial class flightplanner : MyUserControl, IActivate, IDeactivate
    {
        SortedList<int, Device> SorlistDevice = new SortedList<int, Device>();
        int selectedrow;
        PointLatLng pointLat = new PointLatLng(); //获取航点对象

        //public Device dv1;//控制器读写设备类       
      

        public SetWayPointClass swpcp = new SetWayPointClass();//航点图层操作显示类

        //private String massag_error;

        GBL_FLAG gbl_flag;// 全局标志位变量表示逻辑处于哪种状态

        //bool key_function_choose = false;//于激活方向键控制

        System.Timers.Timer show_waypoint = new System.Timers.Timer(1500);//实例化Timer类，设置间隔时间为1.5秒，每1.5秒触发实时路径显示

        Tool Tool = new Tool();//自动读取设备的一个类，可以实现软件打开自动扫描设备类。

        Label lab_dis = new Label();//测距控件创建
        Label lab_ang = new Label();

        public flightplanner()
        {
            InitializeComponent();
            gbl_flag = new GBL_FLAG();
            gbl_flag.flight = false;//地面站打开，系统默认为飞控不处于飞行状态
            gbl_flag.bRoutePlan = true;//地面站打开，系统默认飞控不处于航线规划状态

            show_waypoint.Elapsed += new System.Timers.ElapsedEventHandler(theout);//到达时间的时候执行事件；
            show_waypoint.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            show_waypoint.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            gMapControl.OnMarkerLeave += gMapControl_OnMarkerLeave;
            gMapControl.OnMarkerEnter += gMapControl_OnMarkerEnter; //鼠标进入航点标记区域
            gMapControl.OnMarkerClick += gMapControl_OnMarkerClick;
            gMapControl.MouseMove += gMapControl_Move;

            comboBox1.SelectedIndex = 0;

            drawnpolygonsoverlay = new GMapOverlay("drawnpolygons");
            gMapControl.Overlays.Add(drawnpolygonsoverlay);

            rallypointoverlay = new GMapOverlay("rallypoints");
            gMapControl.Overlays.Add(rallypointoverlay);

            polygonsoverlay = new GMapOverlay("polygons");
            gMapControl.Overlays.Add(polygonsoverlay);

            // set current marker
            currentMarker = new GMarkerGoogle(gMapControl.Position, GMarkerGoogleType.red);

            //setup drawnpolgon
            List<PointLatLng> polygonPoints2 = new List<PointLatLng>();
            drawnpolygon = new GMapPolygon(polygonPoints2, "drawnpoly");
            drawnpolygon.Stroke = new Pen(Color.Red, 2); //此处可以更改测绘点连接线的颜色
            drawnpolygon.Fill = Brushes.Transparent;

            lab_dis.AutoSize = true; //dis
            lab_dis.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lab_dis.ForeColor = Color.Blue;
            lab_dis.BackColor = Color.Transparent;
            lab_ang.AutoSize = true; //ang
            lab_ang.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lab_ang.ForeColor = Color.Blue;
            lab_ang.BackColor = Color.Transparent;

        }

        void groupmarkeradd(GMapMarker marker)
        {
            System.Diagnostics.Debug.WriteLine("add marker " + marker.Tag.ToString());
            groupmarkers.Add(int.Parse(marker.Tag.ToString()));
            if (marker is GMapMarkerWP)
            {
                ((GMapMarkerWP)marker).selected = true;
            }
            if (marker is GMapMarkerRect)
            {
                ((GMapMarkerWP)((GMapMarkerRect)marker).InnerMarker).selected = true;
            }
        }

        private void gMapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            int answer;
            try // when dragging item can sometimes be null
            {
                if (item.Tag == null)
                {
                    // home.. etc
                    return;
                }

                if (ModifierKeys == Keys.Control)
                {
                    try
                    {
                        groupmarkeradd(item);

                        //log.Info("add marker to group");
                    }
                    catch (Exception ex)
                    {
                        //log.Error(ex);
                    }
                }
                if (int.TryParse(item.Tag.ToString(), out answer))
                {
                    Commands.CurrentCell = Commands[0, answer - 1];
                }
            }
            catch (Exception ex)
            {
               // log.Error(ex);
            }
        }

        void gMapControl_OnMarkerEnter(GMapMarker item)
        {
            //throw new NotImplementedException();
            if (!isMouseDown)
            {
                if (item is GMapMarkerRect)  //进入测绘点
                {
                    GMapMarkerRect rc = item as GMapMarkerRect;
                    rc.Pen.Color = Color.Red;
                    gMapControl.Invalidate(false);

                    int answer;
                    if (item.Tag != null && rc.InnerMarker != null &&
                        int.TryParse(rc.InnerMarker.Tag.ToString(), out answer))
                    {
                        try
                        {
                            Commands.CurrentCell = Commands[0, answer - 1];
                            item.ToolTipText = "Alt: " + Commands[Alt.Index, answer - 1].Value;
                            item.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                        }
                        catch (Exception ex)
                        {
                            //log.Error(ex);
                        }
                    }

                    CurentRectMarker = rc;
                }
                if (item is GMapMarkerRallyPt) //进入航点
                {
                    CurrentRallyPt = item as GMapMarkerRallyPt; // item as GMapMarkerRallyPt; //as会转换成GMapMarkerRallyPt类型，如果转换失败，则返回null
                }
                //if (item is GMapMarkerAirport)
                //{
                //    // do nothing - readonly
                //    return;
                //}
                //if (item is GMapMarkerPOI)
                //{
                //    CurrentPOIMarker = item as GMapMarkerPOI;
                //}
                if (item is GMapMarkerWP)
                {
                    CurrentGMapMarker = item as GMapMarkerWP;
                   // currentMarker = item as GMapMarkerWP;
                }
                if (item is GMapMarker)
                {
                    //CurrentGMapMarker = item;
                }
            }
        }

        void gMapControl_OnMarkerLeave(GMapMarker item)
        {
            //throw new NotImplementedException();
            if (!isMouseDown)
            {
                if (item is GMapMarkerRect)
                {
                    CurentRectMarker = null;
                    GMapMarkerRect rc = item as GMapMarkerRect;
                    rc.ResetColor();
                    gMapControl.Invalidate(false);
                }
                if (item is GMapMarkerRallyPt)
                {
                    CurrentRallyPt = null;
                }
                //if (item is GMapMarkerPOI)   兴趣点还未添加
                //{
                //    CurrentPOIMarker = null;
                //}
                if (item is GMapMarker)
                {
                    // when you click the context menu this triggers and causes problems
                    CurrentGMapMarker = null;
                }
            }
        }

        private void flightplanner_Load(object sender, EventArgs e)
        {
            this.Commands.DataSource = null;
            //Device dev = new Device();
            //dv1 = dev;
        }

        public void theout(object source, System.Timers.ElapsedEventArgs e)
        {
            swpcp.Enable_Draw = true;
            //  label14.Text = swpc.realRouteOverlay.Polygons.Count.ToString();
        }


        public void Activate()
        {
            //throw new NotImplementedException();
        }

        public void Deactivate()
        {
            //throw new NotImplementedException();
        }

       
        int flagx;
        int flagy;
        //private ContextMenuStrip caidan;
        private void gmap_SetwayPoint_MouseDown(object sender, MouseEventArgs e)
        {
            flagx = e.X;
            flagy = e.Y;
            MouseDownStart = gMapControl.FromLocalToLatLng(e.X, e.Y);

            if (e.Button == MouseButtons.Left && (groupmarkers.Count > 0 || ModifierKeys == Keys.Control))
            {
                // group move
                isMouseDown = true;
                isMouseDraging = false;

                return;
            }
            if (e.Button == MouseButtons.Left)//使得在移动地图时不会添加测绘点
            {
                currentrectmarkermove = true;
            }

            if (e.Button == MouseButtons.Right)//鼠标右键出菜单
            {
                contextMenuStrip1.Show(MousePosition);
            }

            if (e.Button == MouseButtons.Left && ModifierKeys != Keys.Alt && ModifierKeys != Keys.Control)
            {
                isMouseDown = true;
                isMouseDraging = false;

                if (currentMarker.IsVisible)
                {
                    currentMarker.Position = gMapControl.FromLocalToLatLng(e.X, e.Y);
                }
            }

            if (measuredis)
            {
                swpcp.distWPlist.Clear();//清除航点链表 清除内部航点信息
                //swpcp.Remove(currentMarker);//清除界面显示上航点
                swpcp.reDrawAllWP();
                swpcp.distWPlist.Add(new PointLatLngAlt(gMapControl.FromLocalToLatLng(flagx, flagy).Lat, gMapControl.FromLocalToLatLng(flagx, flagy).Lng));
                swpcp.addpolygonmarker_dis(gMapControl.FromLocalToLatLng(flagx, flagy).Lat, gMapControl.FromLocalToLatLng(flagx, flagy).Lng, Color.Blue);
            }

        }
      

        /*功能：地图规划航点，响应鼠标左键的释放事件
         *说明：只有GPS已经定位，家的位置已经获取，航点设置按键打开,GPS家的位置获取，才可以响应航点编辑。
         * 
         */

        private void gmap_SetwayPoint(object sender, EventArgs e)
        {
            if (gbl_flag.bRoutePlan == true)//如果处于航点规划状态
            {


                //if ((a.Button == MouseButtons.Left))//点击鼠标左键
                // {
                if (gbl_flag.markerselected)//如果是移动航点，则不需要做增加航点的工作，只是重新画航线即可
                {
                    //此处标记工作未完成
                }
                else //如果是增加航点，则
                {

                    if (swpcp.totalWPlist.Count == 0)
                    {
                        CustomMessageBox.Show("请先设定家的位置", Strings.ERROR);
                        return;
                    }
                    //将本点追加入航点链表
                    //// 这些值经纬度参数和指令参数是写入正确的，还有默认海拔高度是20M，其他参数是0
                    int index = Commands.Rows.Add();//添加Commands行  

                    DataGridViewComboBoxCell cell = Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[0] as DataGridViewComboBoxCell;
                    cell.Value = (int)(MAVLink.MAV_CMD.WAYPOINT);
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[1].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[2].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[3].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[4].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[5].Value = gMapControl.FromLocalToLatLng(flagx, flagy).Lat;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[6].Value = gMapControl.FromLocalToLatLng(flagx, flagy).Lng;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[7].Value = 20;//默认高度20M
                    swpcp.totalWPlist.Add(new PointLatLngAlt(gMapControl.FromLocalToLatLng(flagx, flagy).Lat, gMapControl.FromLocalToLatLng(flagx, flagy).Lng,/* Commands.Rows.Count,*/ swpcp.totalWPlist.Count/*.ToString()*/));///这里一定不能使用默认航速default_WP_spd
                    swpcp.addpolygonmarker_sec((swpcp.totalWPlist.Count - 1).ToString(), gMapControl.FromLocalToLatLng(flagx, flagy).Lat, gMapControl.FromLocalToLatLng(flagx, flagy).Lng, 20, null);

                }



                //  }
                swpcp.reDrawAllRoute();
                /////////////
            }
        }
        private void CleanWayPoint_button_Click_1(object sender, EventArgs e)
        {

            swpcp.totalWPlist.Clear();//清除航点链表 清除内部航点信息
            swpcp.reDrawAllWP();//清除界面显示上航点
            swpcp.reDrawAllRoute(); //重新画线
            Commands.Rows.Clear();//清除command内容

        }

        double dislat;
        double dislng;
        private void SetHome_button_Click_1(object sender, EventArgs e)
        {
            if (swpcp.totalWPlist.Count == 0)
            {
                swpcp.totalWPlist.Add(new PointLatLngAlt(gMapControl.Position.Lat, gMapControl.Position.Lng/* Commands.Rows.Count, "H"*/));
                swpcp.addpolygonmarker_sec("H", gMapControl.Position.Lat, gMapControl.Position.Lng, 20, null);
            }
            else if (pointLat.Lat != swpcp.totalWPlist[0].Lat)
            {
                DialogResult result = MessageBox.Show("是否重新设置家的位置？", "提示", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    CleanWayPoint_button_Click_1(sender, e);
                    swpcp.totalWPlist.Add(new PointLatLngAlt(gMapControl.Position.Lat, gMapControl.Position.Lng/* Commands.Rows.Count, "H"*/));
                    swpcp.addpolygonmarker_sec("H", gMapControl.Position.Lat, gMapControl.Position.Lng, 20, null);
                    dislat = gMapControl.Position.Lat;//解决因为存入航点后精度不够的问题
                    dislng = gMapControl.Position.Lng;
                }
            }
        }
        private string FormatDistance(double distInKM, bool toMeterOrFeet)
        {
            string sunits = MAVTOOL.Comms.Settings.Instance["distunits"];
            Common.distances units = Common.distances.Meters;

            if (sunits != null)
                try
                {
                    units = (Common.distances)Enum.Parse(typeof(Common.distances), sunits);
                }
                catch (Exception)
                {
                }

            switch (units)
            {
                case Common.distances.Feet:
                    return toMeterOrFeet
                        ? string.Format((distInKM * 3280.8399).ToString("0.00 ft"))
                        : string.Format((distInKM * 0.621371).ToString("0.0000 miles"));
                case Common.distances.Meters:
                default:
                    return toMeterOrFeet
                        ? string.Format((distInKM * 1000).ToString("0.00 m"))
                        : string.Format(distInKM.ToString("0.0000 km"));
            }
        }

        PointLatLngAlt mouseposdisplay = new PointLatLngAlt(0, 0);
        public List<PointLatLngAlt> pointlist = new List<PointLatLngAlt>(); // used to calc distance
        public List<PointLatLngAlt> fullpointlist = new List<PointLatLngAlt>();
        private ComponentResourceManager rm = new ComponentResourceManager(typeof(flightplanner));

        public void SetMouseDisplay(double lat, double lng, int alt)
        {
            mouseposdisplay.Lat = lat;
            mouseposdisplay.Lng = lng;
            mouseposdisplay.Alt = alt;
            lab_N.Text = mouseposdisplay.Lat + "° N";
            lab_E.Text = mouseposdisplay.Lng + "° E";
            //coords1.Lat = 
            //coords1.Lng = mouseposdisplay.Lng;
            //var altdata = srtm.getAltitude(mouseposdisplay.Lat, mouseposdisplay.Lng, gMapControl.Zoom);
            //coords1.Alt = altdata.alt;
            //coords1.AltSource = altdata.altsource;

            try
            {
                PointLatLng last;

                if (pointlist.Count == 0 || pointlist[pointlist.Count - 1] == null)
                    return;

                last = pointlist[pointlist.Count - 1];

                double lastdist = gMapControl.MapProvider.Projection.GetDistance(last, currentMarker.Position);

                double lastbearing = 0;

                if (pointlist.Count > 0)
                {
                    lastbearing = gMapControl.MapProvider.Projection.GetBearing(last, currentMarker.Position);
                }

                lbl_prevdist.Text = rm.GetString("lbl_prevdist.Text") + ": " + FormatDistance(lastdist, true) + " AZ: " +
                                    lastbearing.ToString("0");

                // 0 is home
                if (pointlist[0] != null)
                {
                    double homedist = gMapControl.MapProvider.Projection.GetDistance(currentMarker.Position, pointlist[0]);

                    lbl_homedist.Text = rm.GetString("lbl_homedist.Text") + ": " + FormatDistance(homedist, true);
                }
            }
            catch (Exception ex)
            {
                // log.Error(ex);
            }
        }

        public const double BEIJING_LAT = 39.958436;
        public const double BEIJING_LNG = 116.309175;
        private void gMapControl_Load(object sender, EventArgs e)
        {

            //************************************主界面GMAP地图的加载项目 * ****************************************
            GMaps.Instance.PrimaryCache = new MyImageCache();
            //gMap.MapProvider = GMap.NET.MapProviders.AMapProvider.Instance; ;//选择地图为高德卫星地图(GMap.NET.MapProviders.GoogleChinaMapProvider.Instance);
            //gMap.MapProvider = GMap.NET.MapProviders.GoogleChinaSatelliteMapProvider.Instance;
            //gMap.MapProvider = GMap.NET.MapProviders.GoogleChinaTerrainMapProvider.Instance;
            gMapControl.MapProvider = GMap.NET.MapProviders.GoogleChinaMapProvider.Instance;
            //gMapControl.MapProvider = GMap.NET.MapProviders
            //谷歌地图的地图更新最好，一般的地面站都选用谷歌中国卫星地图
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            //gMap.SetPositionByKeywords("Chengdu, China");//设定初始中心位置为Chengdu，成都坐标为西纬30.67度，东经104.06
            gMapControl.Position = new PointLatLng(BEIJING_LAT, BEIJING_LNG);//位置北京
            gMapControl.ShowCenter = true;//不显示中心的红色十字
            //gMapControl.DragButton = System.Windows.Forms.MouseButtons.Left;  //左键拖动地图            
            //设置地图分辨率信息
            gMapControl.MaxZoom = 30;
            gMapControl.MinZoom = 3;
            gMapControl.Zoom = 15;

            //创建一个飞行动画层，用于动态显示飞行器的飞行状态
            Bitmap bitmap = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);

            swpcp.realVehicleOverlay = new GMapOverlay("realvehicle");
            gMapControl.Overlays.Add(swpcp.realVehicleOverlay);


            //创建实时航路层，用于显示实时航线
            swpcp.realRouteOverlay = new GMapOverlay("realroute");
            gMapControl.Overlays.Add(swpcp.realRouteOverlay);

            //创建一个marker层，用于标记航点
            swpcp.markersOverlay_sec = new GMapOverlay("markers_sec");
            gMapControl.Overlays.Add(swpcp.markersOverlay_sec);



            /// 绑定航点cmd
            Dictionary<int, string> cmds = new Dictionary<int, string>();
            foreach (MAV_CMD suit in Enum.GetValues(typeof(MAVLink.MAV_CMD)))
            {
                cmds.Add((int)suit, suit.ToString());
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = cmds;
            Command.DataSource = bs;
            Command.DisplayMember = "Value";
            Command.ValueMember = "Key";

            //if (swpcp.totalWPlist.Count == 0)
            //{
            swpcp.totalWPlist.Add(new PointLatLngAlt(gMapControl.Position.Lat, gMapControl.Position.Lng/* Commands.Rows.Count, "H"*/));
            swpcp.addpolygonmarker_sec("H", gMapControl.Position.Lat, gMapControl.Position.Lng, 20, null);
            //}
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        // etc
        readonly Random rnd = new Random();
        string mobileGpsLog = string.Empty;
        //GMapMarkerRect CurentRectMarker;
        // GMapMarkerRallyPt CurrentRallyPt;
        //GMapMarkerPOI CurrentPOIMarker;
        GMapMarker CurrentGMapMarker;
        bool isMouseDown;
        bool isMouseDraging;
        bool isMouseClickOffMenu;
        PointLatLng MouseDownStart;
        internal PointLatLng MouseDownEnd;
        List<int> groupmarkers = new List<int>();

        // marker
        GMapMarker currentMarker;
        GMapMarker center = new GMarkerGoogle(new PointLatLng(0.0, 0.0), GMarkerGoogleType.none);

        public static GMapOverlay objectsoverlay; // where the markers a drawn
        int movex;
        int movey;

        //读取航点位置
        private void but_wpread_Click(object sender, EventArgs e)
        {
            getWPs();
            Rwp = swpcp.totalWPlist; //此信息可能需要修改
        }

        //定义航点列表属性
        private static List<MAVTOOL.Utilities.PointLatLngAlt> rwp;
        public static List<MAVTOOL.Utilities.PointLatLngAlt> Rwp
        {
            get
            {
                if (rwp == null)
                {
                    rwp = new List<MAVTOOL.Utilities.PointLatLngAlt>();
                }

                return rwp;
            }
            set
            {
                if (value != rwp)
                {
                    if (rwp != null)
                    {
                        rwp.Clear();
                    }

                    rwp = value;
                    //  this.RaisePropertyChangedEvent("OptionalCollection");
                }
            }

        }

        //设置地图属性
        private static GMapProvider gp;
        public static GMapProvider Gp
        {

            get
            {
                return gp;
            }
            set
            {
                //if (value != gp)
                //{
                //    if (gp != null)
                //    {
                //        rwp.Clear();
                //    }

                gp = value;
                //  this.RaisePropertyChangedEvent("OptionalCollection");

            }


        }

        //地图切换conbobox控件
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                gMapControl.MapProvider = GMap.NET.MapProviders.GoogleChinaMapProvider.Instance;
                Gp = gMapControl.MapProvider;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                gMapControl.MapProvider = GMap.NET.MapProviders.GoogleChinaHybridMapProvider.Instance;
                Gp = gMapControl.MapProvider;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                gMapControl.MapProvider = GMap.NET.MapProviders.GoogleChinaSatelliteMapProvider.Instance;
                Gp = gMapControl.MapProvider;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                gMapControl.MapProvider = GMap.NET.MapProviders.AMapProvider.Instance;
                Gp = gMapControl.MapProvider;
            }
            gMapControl.Invalidate();
        }

        //将当前地图中心位置设置到家的位置
        private void but_Homelocation_Click(object sender, EventArgs e)
        {
            if (swpcp.totalWPlist.Count == 0)
                MessageBox.Show("还未设置家的位置！");
            else
                gMapControl.Position = new PointLatLng(swpcp.totalWPlist[0].Lat, swpcp.totalWPlist[0].Lng);
        }

        //航点写入按钮功能
        private void but_Writewp_Click(object sender, EventArgs e)
        {
            for (int a = 1; a < Commands.Rows.Count - 1; a++)
            {
                for (int b = 0; b < Commands.ColumnCount - 0; b++)
                {
                    double answer;
                    if (b >= 1 && b <= 7)
                    {
                        if (!double.TryParse(Commands[b, a].Value.ToString(), out answer))
                        {
                            CustomMessageBox.Show("There are errors in your mission");
                            return;
                        }
                    }

                    string i = Commands.Rows[a].Cells[Command.Index].Value.ToString();
                    int g = Command.Index;

                    if (Commands.Rows[a].Cells[Command.Index].Value.ToString().Contains("UNKNOWN"))
                        continue;

                    ushort cmd =
                       (ushort)
                               Enum.Parse(typeof(MAVLink.MAV_CMD),
                                   Commands.Rows[a].Cells[Command.Index].Value.ToString(), false);
                    if (cmd < (ushort)MAVLink.MAV_CMD.LAST &&
                                        double.Parse(Commands[Alt.Index, a].Value.ToString()) < 10)
                    {
                        if (cmd != (ushort)MAVLink.MAV_CMD.TAKEOFF &&
                            cmd != (ushort)MAVLink.MAV_CMD.LAND &&
                            cmd != (ushort)MAVLink.MAV_CMD.RETURN_TO_LAUNCH)
                        {
                            CustomMessageBox.Show("Low alt on WP#" + (a + 1) +
                                                  "\nPlease reduce the alt warning, or increase the altitude");
                            return;
                        }
                    }
                }
            }

            ProgressReporterDialogue frmProgressReporter = new ProgressReporterDialogue//新建航点写入对话框
            {
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Sending WP's"
            };

            frmProgressReporter.DoWork += saveWPs;//航点写入函数
            frmProgressReporter.UpdateProgressAndStatus(-1, "Sending WP's");
            frmProgressReporter.RunBackgroundOperationAsync();
            frmProgressReporter.Dispose();
            this.Focus();
        }

        //从command中读取航点位置并存储
        Locationwp DataViewtoLocationwp(int a)
        {
            try
            {
                Locationwp temp = new Locationwp();
                if (Commands.Rows[a].Cells[Command.Index].Value.ToString().Contains("UNKNOWN"))
                {
                    temp.id = (ushort)Commands.Rows[a].Cells[Command.Index].Tag;
                }
                else
                {
                    temp.id =
                        (ushort)
                                Enum.Parse(typeof(MAVLink.MAV_CMD),
                                    Commands.Rows[a].Cells[Command.Index].Value.ToString(),
                                    false);
                }
                temp.p1 = float.Parse(Commands.Rows[a].Cells[Param1.Index].Value.ToString());

                temp.alt =
                    (float)
                        (double.Parse(Commands.Rows[a].Cells[Alt.Index].Value.ToString()) / CurrentState.multiplierdist);
                temp.lat = (double.Parse(Commands.Rows[a].Cells[Lat.Index].Value.ToString()));
                temp.lng = (double.Parse(Commands.Rows[a].Cells[Lon.Index].Value.ToString()));

                temp.p2 = (float)(double.Parse(Commands.Rows[a].Cells[Param2.Index].Value.ToString()));
                temp.p3 = (float)(double.Parse(Commands.Rows[a].Cells[Param3.Index].Value.ToString()));
                temp.p4 = (float)(double.Parse(Commands.Rows[a].Cells[Param4.Index].Value.ToString()));

                // temp.Tag = Commands.Rows[a].Cells[TagData.Index].Value;

                return temp;
            }
            catch (Exception ex)
            {
                throw new FormatException("Invalid number on row " + (a + 1).ToString(), ex);
            }
        }

        List<Locationwp> GetCommandList()
        {

            List<Locationwp> commands = new List<Locationwp>();

            for (int a = 0; a < Commands.Rows.Count; a++)
            {

                var temp = DataViewtoLocationwp(a);
                commands.Add(temp);
            }

            return commands;
        }

        void saveWPs(object sender, ProgressWorkerEventArgs e, object passdata = null)
        {
            MAVLinkInterface port = AmovPlanner.dv1.comPort;//得到MAVLINK类
            try
            {


                if (!port.BaseStream.IsOpen)
                {
                    throw new Exception("Please connect first!");
                }

                port.giveComport = true;
                int a = 0;

                // define the home point
                Locationwp home = new Locationwp();
                try
                {
                    home.id = (byte)MAVLink.MAV_CMD.WAYPOINT;//写入家的位置
                    home.lat = gMapControl.Position.Lat;
                    home.lng = gMapControl.Position.Lng;
                    home.alt = 20 / CurrentState.multiplierdist; // use saved home
                }
                catch
                {
                    throw new Exception("Your home location is invalid");
                }

                // log
                //log.Info("wps values " + MainV2.comPort.MAV.wps.Values.Count);
                //log.Info("cmd rows " + (Commands.Rows.Count + 1)); // + home

                // check for changes / future mod to send just changed wp's
                if (port.MAV.wps.Values.Count == (Commands.Rows.Count + 1))
                {
                    Hashtable wpstoupload = new Hashtable();

                    a = -1;
                    foreach (var item in port.MAV.wps.Values)
                    {
                        // skip home
                        if (a == -1)
                        {
                            a++;
                            continue;
                        }

                        MAVLink.mavlink_mission_item_t temp = DataViewtoLocationwp(a);

                        if (temp.command == item.command &&
                            temp.x == item.x &&
                            temp.y == item.y &&
                            temp.z == item.z &&
                            temp.param1 == item.param1 &&
                            temp.param2 == item.param2 &&
                            temp.param3 == item.param3 &&
                            temp.param4 == item.param4
                            )
                        {
                            //   log.Info("wp match " + (a + 1));
                        }
                        else
                        {
                            // log.Info("wp no match" + (a + 1));
                            wpstoupload[a] = "";
                        }

                        a++;
                    }
                }

                bool use_int = (port.MAV.cs.capabilities & (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;

                // set wp total
                ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set total wps ");

                ushort totalwpcountforupload = (ushort)(Commands.Rows.Count + 1);

                if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
                {
                    totalwpcountforupload--;
                }

                port.setWPTotal(totalwpcountforupload); // + home

                // set home location - overwritten/ignored depending on firmware.
                ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set home");

                // upload from wp0
                a = 0;

                if (port.MAV.apname != MAVLink.MAV_AUTOPILOT.PX4)
                {
                    try
                    {
                        var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
                        if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        {
                            if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                            {
                                CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
                                return;
                            }
                        }
                        a++;
                    }
                    catch (TimeoutException)
                    {
                        use_int = false;
                        // added here to prevent timeout errors
                        port.setWPTotal(totalwpcountforupload);
                        var homeans = port.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
                        if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        {
                            if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                            {
                                CustomMessageBox.Show(Strings.ErrorRejectedByMAV, Strings.ERROR);
                                return;
                            }
                        }
                        a++;
                    }
                }
                else
                {
                    use_int = false;
                }

                // define the default frame.
                MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

                // get the command list from the datagrid
                var commandlist = GetCommandList();

                // process commandlist to the mav
                for (a = 1; a <= commandlist.Count; a++)
                {
                    var temp = commandlist[a - 1];

                    ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(a * 100 / Commands.Rows.Count,
                        "Setting WP " + a);

                    // make sure we are using the correct frame for these commands
                    if (temp.id < (ushort)MAVLink.MAV_CMD.LAST || temp.id == (ushort)MAVLink.MAV_CMD.DO_SET_HOME)
                    {

                        frame = MAVLink.MAV_FRAME.GLOBAL;

                    }

                    // handle current wp upload number
                    int uploadwpno = a;
                    if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
                        uploadwpno--;

                    // try send the wp
                    MAVLink.MAV_MISSION_RESULT ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);

                    // we timed out while uploading wps/ command wasnt replaced/ command wasnt added
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
                    {
                        // resend for partial upload
                        port.setWPPartialUpdate((ushort)(uploadwpno), totalwpcountforupload);
                        // reupload this point.
                        ans = port.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);
                    }

                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE)
                    {
                        e.ErrorMessage = "Upload failed, please reduce the number of wp's";
                        return;
                    }
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
                    {
                        e.ErrorMessage =
                            "Upload failed, mission was rejected byt the Mav,\n item had a bad option wp# " + a + " " +
                            ans;
                        return;
                    }
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                    {
                        // invalid sequence can only occur if we failed to see a response from the apm when we sent the request.
                        // or there is io lag and we send 2 mission_items and get 2 responces, one valid, one a ack of the second send

                        // the ans is received via mission_ack, so we dont know for certain what our current request is for. as we may have lost the mission_request

                        // get requested wp no - 1;
                        a = port.getRequestedWPNo() - 1;

                        continue;
                    }
                    if (ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                    {
                        e.ErrorMessage = "Upload wps failed " + Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()) +
                                         " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString());
                        return;
                    }
                }

                port.setWPACK();

                ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(95, "Setting params");

                // m
                port.setParam("WP_RADIUS", float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist);

                // cm's
                port.setParam("WPNAV_RADIUS", float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist * 100.0);

                try
                {
                    port.setParam(new[] { "LOITER_RAD", "WP_LOITER_RAD" },
                       60f / CurrentState.multiplierdist);
                }
                catch
                {
                }

                ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(100, "Done.");
            }
            catch (Exception ex)
            {
                port.giveComport = false;//com口释放标志，读线程运行
                throw;
            }

            port.giveComport = false;//com口释放标志，读线程运行
        }


        void getWPs()
        {
            List<Locationwp> cmds = new List<Locationwp>();
            swpcp.totalWPlist.Clear();
            swpcp.wayPointLists.Clear();
            swpcp.Clear();

            try
            {
                MAVLinkInterface port = AmovPlanner.dv1.comPort;

                if (!port.BaseStream.IsOpen)
                {
                    throw new Exception("Please Connect First!");
                }

                port.giveComport = true;

                //  log.Info("Getting Home");

                //  ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Getting WP count");

                if (port.MAV.apname == MAVLink.MAV_AUTOPILOT.PX4)
                {
                    try
                    {
                        cmds.Add(port.getHomePosition());
                    }
                    catch (TimeoutException)
                    {
                        // blank home
                        cmds.Add(new Locationwp() { id = (ushort)MAVLink.MAV_CMD.WAYPOINT });
                    }
                }

                //  log.Info("Getting WP #");

                int cmdcount = port.getWPCount();

                for (ushort a = 0; a < cmdcount; a++)
                {
                    //if (((ProgressReporterDialogue)sender).doWorkArgs.CancelRequested)
                    //{
                    //    ((ProgressReporterDialogue)sender).doWorkArgs.CancelAcknowledged = true;
                    //    throw new Exception("Cancel Requested");
                    //}

                    //log.Info("Getting WP" + a);
                    //((ProgressReporterDialogue)sender).UpdateProgressAndStatus(a * 100 / cmdcount, "Getting WP " + a);
                    cmds.Add(port.getWP(a));
                }

                port.setWPACK();
                swpcp.totalWPlist.Add(new PointLatLngAlt(gMapControl.Position.Lat, gMapControl.Position.Lng, Commands.Rows.Count, "H"));
                Commands.Rows.Clear();
                for (int i = 1; i < cmds.Count; i++)
                {
                    //// 这些值经纬度参数和指令参数是写入正确的，还有默认海拔高度是20M，其他参数是0
                    int index = Commands.Rows.Add();//添加Commands行  

                    DataGridViewComboBoxCell cell = Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[0] as DataGridViewComboBoxCell;
                    cell.Value = (int)(MAVLink.MAV_CMD.WAYPOINT);
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[1].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[2].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[3].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[4].Value = 0;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[5].Value = cmds[i].lat;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[6].Value = cmds[i].lng;
                    Commands.Rows[swpcp.totalWPlist.Count - 1].Cells[7].Value = 20;//默认高度20M

                    swpcp.totalWPlist.Add(new PointLatLngAlt(cmds[i].lat, cmds[i].lng, Commands.Rows.Count, swpcp.totalWPlist.Count.ToString()));///这里一定不能使用默认航速default_WP_spd
                    swpcp.addpolygonmarker_sec((swpcp.totalWPlist.Count - 1).ToString(), cmds[i].lat, cmds[i].lng, 20, null);
                }


                swpcp.reDrawAllRoute();

                //   swpc.addpolygonmarker_sec("H", gMap.Position.Lat, gMap.Position.Lng, 20, null);


            }
            catch
            {
                //  throw;
            }


        }

        private void insertwp_Click(object sender, EventArgs e)
        {
            gmap_SetwayPoint(sender, e);
        }

        GMapMarkerRect CurentRectMarker;
        GMapMarkerRallyPt CurrentRallyPt;
        static GMapOverlay rallypointoverlay;
        private void deletewp_Click(object sender, EventArgs e)
        {
            //CleanWayPoint_button_Click_1(sender, e);
            int no = 0;
            if (CurentRectMarker != null)  //删除测绘点条件
            {
                if (int.TryParse(CurentRectMarker.InnerMarker.Tag.ToString(), out no))
                {
                    try
                    {
                        Commands.Rows.RemoveAt(no - 1); // home is 0
                    }
                    catch (Exception ex)
                    {
                        //log.Error(ex);
                        CustomMessageBox.Show("error selecting wp, please try again.");
                    }
                }
                else if (int.TryParse(CurentRectMarker.InnerMarker.Tag.ToString().Replace("grid", ""), out no))
                {
                    try
                    {
                        drawnpolygon.Points.RemoveAt(no - 1); //删除测绘点
                        drawnpolygonsoverlay.Markers.Clear();

                        int a = 1;
                        foreach (PointLatLng pnt in drawnpolygon.Points)
                        {
                            addpolygonmarkergrid(a.ToString(), pnt.Lng, pnt.Lat, 0);
                            a++;
                        }

                        gMapControl.UpdatePolygonLocalPosition(drawnpolygon);

                        gMapControl.Invalidate();
                    }
                    catch (Exception ex)
                    {
                        // log.Error(ex);
                        CustomMessageBox.Show("Remove point Failed. Please try again.");
                    }
                }
            }
            else if (CurrentRallyPt != null) //删除航点条件
            {
                rallypointoverlay.Markers.Remove(CurrentRallyPt);
                gMapControl.Invalidate(true);

                CurrentRallyPt = null;
            }
            else if (groupmarkers.Count > 0)
            {
                for (int a = Commands.Rows.Count; a > 0; a--)
                {
                    try
                    {
                        if (groupmarkers.Contains(a))
                            Commands.Rows.RemoveAt(a - 1); // home is 0
                    }
                    catch (Exception ex)
                    {
                        //log.Error(ex);
                        CustomMessageBox.Show("error selecting wp, please try again.");
                    }
                }

                groupmarkers.Clear();
            }


            if (currentMarker != null)
                CurentRectMarker = null;

            writeKML();
        }

        private void sethome_Click(object sender, EventArgs e)
        {
            SetHome_button_Click_1(sender, e);
        }

        bool grid;
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            grid = checkBox2.Checked;
            gMapControl.Invalidate();
        }

        PointLatLng startmeasure;
        //PointLatLng mousemove;
        public GMapOverlay polygonsoverlay; // where the track is drawn
        private void gMapControl_Paint(object sender, PaintEventArgs e)
        {
            // draw utm grid
            if (grid)   //pointlatlngalt中搜索implicit operator PointLatLngAlt(GeoUtility.GeoSystem.Geographic geo)
            {
                if (gMapControl.Zoom < 10)
                    return;

                var rect = gMapControl.ViewArea;

                var plla1 = new PointLatLngAlt(rect.LocationTopLeft);
                var plla2 = new PointLatLngAlt(rect.LocationRightBottom);

                var center = new PointLatLngAlt(rect.LocationMiddle);

                var zone = center.GetUTMZone();

                var utm1 = plla1.ToUTM(zone);
                var utm2 = plla2.ToUTM(zone);

                var deltax = utm1[0] - utm2[0];
                var deltay = utm1[1] - utm2[1];

                //if (deltax)

                var gridsize = 1000.0;


                if (Math.Abs(deltax) / 100000 < 40)
                    gridsize = 100000;

                if (Math.Abs(deltax) / 10000 < 40)
                    gridsize = 10000;

                if (Math.Abs(deltax) / 1000 < 40)
                    gridsize = 1000;

                if (Math.Abs(deltax) / 100 < 40)
                    gridsize = 100;


                // round it - x
                utm1[0] = utm1[0] - (utm1[0] % gridsize);
                // y
                utm2[1] = utm2[1] - (utm2[1] % gridsize);

                // x's
                for (double x = utm1[0]; x < utm2[0]; x += gridsize)
                {
                    var p1 = gMapControl.FromLatLngToLocal(PointLatLngAlt.FromUTM(zone, x, utm1[1]));
                    var p2 = gMapControl.FromLatLngToLocal(PointLatLngAlt.FromUTM(zone, x, utm2[1]));

                    int x1 = (int)p1.X;
                    int y1 = (int)p1.Y;
                    int x2 = (int)p2.X;
                    int y2 = (int)p2.Y;

                    e.Graphics.DrawLine(new Pen(gMapControl.SelectionPen.Color, 1), x1, y1, x2, y2);
                }

                // y's
                for (double y = utm2[1]; y < utm1[1]; y += gridsize)
                {
                    var p1 = gMapControl.FromLatLngToLocal(PointLatLngAlt.FromUTM(zone, utm1[0], y));
                    var p2 = gMapControl.FromLatLngToLocal(PointLatLngAlt.FromUTM(zone, utm2[0], y));

                    int x1 = (int)p1.X;
                    int y1 = (int)p1.Y;
                    int x2 = (int)p2.X;
                    int y2 = (int)p2.Y;

                    e.Graphics.DrawLine(new Pen(gMapControl.SelectionPen.Color, 1), x1, y1, x2, y2);
                }
            }

            //polyicon.Location = new Point(10, 100);
            //polyicon.Paint(e.Graphics);

            //测距勾选后判断事件
            if (measuredis == true)
            {
                List<PointLatLng> polygonPoints = new List<PointLatLng>();

                if (swpcp.distWPlist.Count == 0)
                {
                    if (swpcp.totalWPlist.Count != 0)
                    {
                        startmeasure.Lat = swpcp.totalWPlist[0].Lat;
                        startmeasure.Lng = swpcp.totalWPlist[0].Lng;

                        polygonPoints.Add(startmeasure);
                        polygonPoints.Add(point);
                        polygonsoverlay.Polygons.Clear();//clear line

                        GMapPolygon line = new GMapPolygon(polygonPoints, "");
                        line.Stroke = new Pen(Color.Green, 4);
                     
                        polygonsoverlay.Polygons.Add(line);//add line
                        gMapControl.Invalidate();

                        lab_dis.Text ="距离：" + FormatDistance(gMapControl.MapProvider.Projection.GetDistance(startmeasure, point), true) + "m";
                        lab_ang.Text = "角度：" + gMapControl.MapProvider.Projection.GetBearing(startmeasure, point).ToString("0") + "°";
                        lab_dis.Location = new System.Drawing.Point(movex + 20,movey + 20);//set label location
                        lab_ang.Location = new System.Drawing.Point(movex + 20, movey + 50);
                        gMapControl.Controls.Add(lab_dis);
                        gMapControl.Controls.Add(lab_ang);
                        gMapControl.Invalidate();

                        polygonsoverlay.Markers.Clear();
                        startmeasure = new PointLatLng();

                    }
                    else
                        return;
                }
                else
                {
                    startmeasure.Lat = swpcp.distWPlist[0].Lat;
                    startmeasure.Lng = swpcp.distWPlist[0].Lng;
                    //polygonsoverlay.Markers.Add(new GMarkerGoogle(MouseDownStart, GMarkerGoogleType.red));
                    //gMapControl.Invalidate();

                    //mousemove = gMapControl.FromLocalToLatLng(MousePosition.X, MousePosition.Y);//获取鼠标移动的经纬度
                    polygonPoints.Add(startmeasure);
                    polygonPoints.Add(point);
                    polygonsoverlay.Polygons.Clear();

                    GMapPolygon line = new GMapPolygon(polygonPoints, "");
                    line.Stroke = new Pen(Color.Green, 4);

                    polygonsoverlay.Polygons.Remove(line);
                    polygonsoverlay.Polygons.Add(line);
                    //polygonsoverlay.Markers.Add(new GMarkerGoogle(MouseDownStart, GMarkerGoogleType.red));
                    gMapControl.Invalidate();

                    lab_dis.Text = "距离：" + FormatDistance(gMapControl.MapProvider.Projection.GetDistance(startmeasure, point), true) + "m";
                    lab_ang.Text = "角度：" + gMapControl.MapProvider.Projection.GetBearing(startmeasure, point).ToString("0") + "°";
                    lab_dis.Location = new System.Drawing.Point(movex + 20, movey + 20);
                    lab_ang.Location = new System.Drawing.Point(movex + 20, movey + 50);
                    gMapControl.Controls.Add(lab_dis);
                    gMapControl.Controls.Add(lab_ang);
                    polygonsoverlay.Markers.Clear();
                    startmeasure = new PointLatLng();
                }
            }
            else
            {
                try
                {
                    polygonsoverlay.Polygons.Clear();//取消测距后完全清除图层
                    swpcp.distWPlist.Clear();//清除航点链表 清除内部航点信息
                    swpcp.reDrawAllWP();
                    gMapControl.Controls.Remove(lab_dis);
                    gMapControl.Controls.Remove(lab_ang);
                }
                catch(Exception ex)
                {
                } 
            }
                

            e.Graphics.ResetTransform();

        }

        bool polygongridmode;
        GMapOverlay drawnpolygonsoverlay;
        internal GMapPolygon drawnpolygon;
        private void addpolygonpoint_Click(object sender, EventArgs e)
        {
            if ( polygongridmode== false)
            {
                CustomMessageBox.Show(
                    "You will remain in polygon mode until you clear the polygon or create a grid/upload a fence");
            }

            polygongridmode = true;

            List<PointLatLng> polygonPoints = new List<PointLatLng>();
            if (drawnpolygonsoverlay.Polygons.Count == 0)
            {
                drawnpolygon.Points.Clear();
                drawnpolygonsoverlay.Polygons.Add(drawnpolygon);
            }

            drawnpolygon.Fill = Brushes.Transparent; //Transparent

            // remove full loop is exists
            if (drawnpolygon.Points.Count > 1 &&
                drawnpolygon.Points[0] == drawnpolygon.Points[drawnpolygon.Points.Count - 1])
                drawnpolygon.Points.RemoveAt(drawnpolygon.Points.Count - 1 ); // unmake a full loop

            drawnpolygon.Points.Add(new PointLatLng(MouseDownStart.Lat, MouseDownStart.Lng));

            addpolygonmarkergrid(drawnpolygon.Points.Count.ToString(), MouseDownStart.Lng, MouseDownStart.Lat, 0);
            gMapControl.UpdatePolygonLocalPosition(drawnpolygon);

            gMapControl.Invalidate();
        }

        private void addpolygonmarkergrid(string tag, double lng, double lat, int alt)
        {
            try
            {
                PointLatLng point = new PointLatLng(lat, lng);
                GMarkerGoogle m = new GMarkerGoogle(point, GMarkerGoogleType.red);
                m.ToolTipMode = MarkerTooltipMode.Never;
                m.ToolTipText = "grid" + tag;
                m.Tag = "grid" + tag;

                //MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
                GMapMarkerRect mBorders = new GMapMarkerRect(point);
                {
                    mBorders.InnerMarker = m;
                }

                drawnpolygonsoverlay.Markers.Add(m);
                drawnpolygonsoverlay.Markers.Add(mBorders);
            }
            catch (Exception ex)
            {
               // log.Info(ex.ToString());
            }
        }

        /// <summary>
        /// used to add a marker to the map display
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <param name="alt"></param>
        private void addpolygonmarker(string tag, double lng, double lat, double alt, Color? color)
        {
            try
            {
                PointLatLng point = new PointLatLng(lat, lng);
                GMapMarkerWP m = new GMapMarkerWP(point, tag);
                m.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                m.ToolTipText = "Alt: " + alt.ToString("0");
                m.Tag = tag;

                int wpno = -1;
                if (int.TryParse(tag, out wpno))
                {
                    // preselect groupmarker
                    if (groupmarkers.Contains(wpno))
                        m.selected = true;
                }

                //MissionPlanner.GMapMarkerRectWPRad mBorders = new MissionPlanner.GMapMarkerRectWPRad(point, (int)float.Parse(TXT_WPRad.Text), MainMap);
                GMapMarkerRect mBorders = new GMapMarkerRect(point);
                {
                    mBorders.InnerMarker = m;
                    mBorders.Tag = tag;
                    mBorders.wprad = (int)(float.Parse(TXT_WPRad.Text) / CurrentState.multiplierdist);
                    if (color.HasValue)
                    {
                        mBorders.Color = color.Value;
                    }
                }

                objectsoverlay.Markers.Add(m);
                objectsoverlay.Markers.Add(mBorders);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// used to override the drawing of the waypoint box bounding
        /// </summary>
        [Serializable]
        public class GMapMarkerRect : GMapMarker
        {
            public Pen Pen = new Pen(Brushes.White, 2);

            public Color Color
            {
                get { return Pen.Color; }
                set
                {
                    if (!initcolor.HasValue) initcolor = value;
                    Pen.Color = value;
                }
            }

            Color? initcolor = null;

            public GMapMarker InnerMarker;

            public int wprad = 0;

            public void ResetColor()
            {
                if (initcolor.HasValue)
                    Color = initcolor.Value;
                else
                    Color = Color.White;
            }

            public GMapMarkerRect(PointLatLng p)
                : base(p)
            {
                Pen.DashStyle = DashStyle.Dash;

                // do not forget set Size of the marker
                // if so, you shall have no event on it ;}
                Size = new System.Drawing.Size(50, 50);
                Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2 - 20);
            }

            public override void OnRender(Graphics g)
            {
                base.OnRender(g);

                if (wprad == 0 || Overlay.Control == null)
                    return;

                // if we have drawn it, then keep that color
                if (!initcolor.HasValue)
                    Color = Color.White;

                // undo autochange in mouse over
                //if (Pen.Color == Color.Blue)
                //  Pen.Color = Color.White;

                double width =
                    (Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
                        Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0)) * 1000.0);
                double height =
                    (Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
                        Overlay.Control.FromLocalToLatLng(Overlay.Control.Height, 0)) * 1000.0);
                double m2pixelwidth = Overlay.Control.Width / width;
                double m2pixelheight = Overlay.Control.Height / height;

                GPoint loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * wprad * 2)), LocalPosition.Y);
                // MainMap.FromLatLngToLocal(wpradposition);

                if (m2pixelheight > 0.5 && !double.IsInfinity(m2pixelheight))
                    g.DrawArc(Pen,
                        new System.Drawing.Rectangle(
                            LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
                            LocalPosition.Y - Offset.Y - (int)Math.Abs(loc.X - LocalPosition.X) / 2,
                            (int)Math.Abs(loc.X - LocalPosition.X), (int)Math.Abs(loc.X - LocalPosition.X)), 0, 360);
            }
        }

        private void clearpolygon_Click(object sender, EventArgs e)
        {
            polygongridmode = false;
            if (drawnpolygon == null)
                return;
            drawnpolygon.Points.Clear();
            drawnpolygonsoverlay.Markers.Clear();
            gMapControl.Invalidate();

            //writeKML();
        }

        private void savepolygon_Click(object sender, EventArgs e)
        {
            if (drawnpolygon.Points.Count == 0)
            {
                return;
            }


            using (SaveFileDialog sf = new SaveFileDialog())
            {
                sf.Filter = "Polygon (*.poly)|*.poly";
                sf.ShowDialog();
                if (sf.FileName != "")
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter(sf.OpenFile());

                        sw.WriteLine("#saved by Mission Planner " + Application.ProductVersion);

                        if (drawnpolygon.Points.Count > 0)
                        {
                            foreach (var pll in drawnpolygon.Points)
                            {
                                sw.WriteLine(pll.Lat + " " + pll.Lng);
                            }

                            PointLatLng pll2 = drawnpolygon.Points[0];

                            sw.WriteLine(pll2.Lat + " " + pll2.Lng);
                        }

                        sw.Close();
                    }
                    catch
                    {
                        CustomMessageBox.Show("Failed to write fence file");
                    }
                }
            }
        }

        /// <summary>
        /// used to adjust existing point in the datagrid including "H" 调整包括家的兴趣点
        /// </summary>
        /// <param name="pointno"></param>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="alt"></param>
        public void callMeDrag(string pointno, double lat, double lng, int alt)
        {
            if (pointno == "")
            {
                return;
            }

            // dragging a WP
            if (pointno == "H")
            {
                // auto update home alt
                //TXT_homealt.Text = (srtm.getAltitude(lat, lng).alt * CurrentState.multiplierdist).ToString();

                //TXT_homelat.Text = lat.ToString();
                //TXT_homelng.Text = lng.ToString();
                return;
            }

            if (pointno == "Tracker Home")
            {
                AmovPlanner.dv1.comPort.MAV.cs.TrackerLocation = new PointLatLngAlt(lat, lng, alt, "");
                return;
            }

            try
            {
                selectedrow = int.Parse(pointno) - 1;
                Commands.CurrentCell = Commands[1, selectedrow];
                // depending on the dragged item, selectedrow can be reset 
                selectedrow = int.Parse(pointno) - 1;
            }
            catch
            {
                return;
            }

           // setfromMap(lat, lng, alt);
        }

        ///// <summary>
        ///// Actualy Sets the values into the datagrid and verifys height if turned on
        ///// </summary>
        ///// <param name="lat"></param>
        ///// <param name="lng"></param>
        ///// <param name="alt"></param>
        //public void setfromMap(double lat, double lng, int alt, double p1 = 0)
        //{
        //    if (selectedrow > Commands.RowCount)
        //    {
        //        CustomMessageBox.Show("Invalid coord, How did you do this?");
        //        return;
        //    }

        //    try
        //    {
        //        // get current command list
        //        var currentlist = GetCommandList();
        //        // add history
        //        history.Add(currentlist);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomMessageBox.Show("A invalid entry has been detected\n" + ex.Message, Strings.ERROR);
        //    }

        //    // remove more than 20 revisions
        //    if (history.Count > 20)
        //    {
        //        history.RemoveRange(0, history.Count - 20);
        //    }

        //    DataGridViewTextBoxCell cell;
        //    if (alt == -2 && Commands.Columns[Alt.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6] /*"Alt"*/))
        //    {
        //        if (CHK_verifyheight.Checked && (altmode)CMB_altmode.SelectedValue != altmode.Terrain) //Drag with verifyheight // use srtm data
        //        {
        //            cell = Commands.Rows[selectedrow].Cells[Alt.Index] as DataGridViewTextBoxCell;
        //            float ans;
        //            if (float.TryParse(cell.Value.ToString(), out ans))
        //            {
        //                ans = (int)ans;

        //                DataGridViewTextBoxCell celllat =
        //                    Commands.Rows[selectedrow].Cells[Lat.Index] as DataGridViewTextBoxCell;
        //                DataGridViewTextBoxCell celllon =
        //                    Commands.Rows[selectedrow].Cells[Lon.Index] as DataGridViewTextBoxCell;
        //                int oldsrtm =
        //                    (int)
        //                        ((srtm.getAltitude(double.Parse(celllat.Value.ToString()),
        //                            double.Parse(celllon.Value.ToString())).alt) * CurrentState.multiplierdist);
        //                int newsrtm = (int)((srtm.getAltitude(lat, lng).alt) * CurrentState.multiplierdist);
        //                int newh = (int)(ans + newsrtm - oldsrtm);

        //                cell.Value = newh;

        //                cell.DataGridView.EndEdit();
        //            }
        //        }
        //    }
        //    if (Commands.Columns[Lat.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][4] /*"Lat"*/))
        //    {
        //        cell = Commands.Rows[selectedrow].Cells[Lat.Index] as DataGridViewTextBoxCell;
        //        cell.Value = lat.ToString("0.0000000");
        //        cell.DataGridView.EndEdit();
        //    }
        //    if (Commands.Columns[Lon.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][5] /*"Long"*/))
        //    {
        //        cell = Commands.Rows[selectedrow].Cells[Lon.Index] as DataGridViewTextBoxCell;
        //        cell.Value = lng.ToString("0.0000000");
        //        cell.DataGridView.EndEdit();
        //    }
        //    if (alt != -1 && alt != -2 &&
        //        Commands.Columns[Alt.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6] /*"Alt"*/))
        //    {
        //        cell = Commands.Rows[selectedrow].Cells[Alt.Index] as DataGridViewTextBoxCell;

        //        {
        //            double result;
        //            bool pass = double.TryParse(TXT_homealt.Text, out result);

        //            if (pass == false)
        //            {
        //                CustomMessageBox.Show("You must have a home altitude");
        //                string homealt = "100";
        //                if (DialogResult.Cancel == InputBox.Show("Home Alt", "Home Altitude", ref homealt))
        //                    return;
        //                TXT_homealt.Text = homealt;
        //            }
        //            int results1;
        //            if (!int.TryParse(TXT_DefaultAlt.Text, out results1))
        //            {
        //                CustomMessageBox.Show("Your default alt is not valid");
        //                return;
        //            }

        //            if (results1 == 0)
        //            {
        //                string defalt = "100";
        //                if (DialogResult.Cancel == InputBox.Show("Default Alt", "Default Altitude", ref defalt))
        //                    return;
        //                TXT_DefaultAlt.Text = defalt;
        //            }
        //        }

        //        cell.Value = TXT_DefaultAlt.Text;

        //        float ans;
        //        if (float.TryParse(cell.Value.ToString(), out ans))
        //        {
        //            ans = (int)ans;
        //            if (alt != 0) // use passed in value;
        //                cell.Value = alt.ToString();
        //            if (ans == 0) // default
        //                cell.Value = 50;
        //            if (ans == 0 && (MainV2.comPort.MAV.cs.firmware == MainV2.Firmwares.ArduCopter2))
        //                cell.Value = 15;

        //            // not online and verify alt via srtm
        //            if (CHK_verifyheight.Checked) // use srtm data
        //            {
        //                // is absolute but no verify
        //                if ((altmode)CMB_altmode.SelectedValue == altmode.Absolute)
        //                {
        //                    //abs
        //                    cell.Value =
        //                        ((srtm.getAltitude(lat, lng).alt) * CurrentState.multiplierdist +
        //                         int.Parse(TXT_DefaultAlt.Text)).ToString();
        //                }
        //                else if ((altmode)CMB_altmode.SelectedValue == altmode.Terrain)
        //                {
        //                    cell.Value = int.Parse(TXT_DefaultAlt.Text);
        //                }
        //                else
        //                {
        //                    //relative and verify
        //                    cell.Value =
        //                        ((int)(srtm.getAltitude(lat, lng).alt) * CurrentState.multiplierdist +
        //                         int.Parse(TXT_DefaultAlt.Text) -
        //                         (int)
        //                             srtm.getAltitude(MainV2.comPort.MAV.cs.HomeLocation.Lat,
        //                                 MainV2.comPort.MAV.cs.HomeLocation.Lng).alt * CurrentState.multiplierdist)
        //                            .ToString();
        //                }
        //            }

        //            cell.DataGridView.EndEdit();
        //        }
        //        else
        //        {
        //            CustomMessageBox.Show("Invalid Home or wp Alt");
        //            cell.Style.BackColor = Color.Red;
        //        }
        //    }

        //    // convert to utm
        //    convertFromGeographic(lat, lng);

        //    // Add more for other params
        //    if (Commands.Columns[Param1.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][1] /*"Delay"*/))
        //    {
        //        cell = Commands.Rows[selectedrow].Cells[Param1.Index] as DataGridViewTextBoxCell;
        //        cell.Value = p1;
        //        cell.DataGridView.EndEdit();
        //    }

        //    writeKML();
        //    Commands.EndEdit();
        //}

        /// <summary>
        /// used to write a KML, update the Map view polygon, and update the row headers
        /// </summary>
        public void writeKML()
        {

            // this is to share the current mission with the data tab
            pointlist = new List<PointLatLngAlt>();

            fullpointlist.Clear();

            Debug.WriteLine(DateTime.Now);
            //try
            //{
                if (objectsoverlay != null) // hasnt been created yet
                {
                    objectsoverlay.Markers.Clear();
                }
            //}
        }

            private void loadpolygon_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Polygon (*.poly)|*.poly";
                fd.ShowDialog();
                if (File.Exists(fd.FileName))
                {
                    StreamReader sr = new StreamReader(fd.OpenFile());

                    drawnpolygonsoverlay.Markers.Clear();
                    drawnpolygonsoverlay.Polygons.Clear();
                    drawnpolygon.Points.Clear();

                    int a = 0;

                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line.StartsWith("#"))
                        {
                        }
                        else
                        {
                            string[] items = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                            if (items.Length < 2)
                                continue;

                            drawnpolygon.Points.Add(new PointLatLng(double.Parse(items[0]), double.Parse(items[1])));
                            addpolygonmarkergrid(drawnpolygon.Points.Count.ToString(), double.Parse(items[1]),
                                double.Parse(items[0]), 0);

                            a++;
                        }
                    }

                    // remove loop close
                    if (drawnpolygon.Points.Count > 1 &&
                        drawnpolygon.Points[0] == drawnpolygon.Points[drawnpolygon.Points.Count - 1])
                    {
                        drawnpolygon.Points.RemoveAt(drawnpolygon.Points.Count - 1);
                    }

                    drawnpolygonsoverlay.Polygons.Add(drawnpolygon);

                    gMapControl.UpdatePolygonLocalPosition(drawnpolygon);

                    gMapControl.Invalidate();

                    gMapControl.ZoomAndCenterMarkers(drawnpolygonsoverlay.Id);
                }
            }
        }

        ////面积
        private void areatoolstripmenuitem_Click(object sender, EventArgs e)
        {
            double aream2 = Math.Abs(calcpolygonarea(drawnpolygon.Points));

            double areaa = aream2 * 0.000247105;

            double areaha = aream2 * 1e-4;

            double areasqf = aream2 * 10.7639;

            CustomMessageBox.Show(
                "测区面积: " + aream2.ToString("0") + " 平方米\n\t" ,//+ areaa.ToString("0.00") + " Acre\n\t" +
                //areaha.ToString("0.00") + " Hectare\n\t" + areasqf.ToString("0") + " sqf", 
                "面积");
        }

        double calcpolygonarea(List<PointLatLng> polygon)
        {
            // should be a closed polygon
            // coords are in lat long
            // need utm to calc area

            if (polygon.Count == 0)
            {
                CustomMessageBox.Show("Please define a polygon!");
                return 0;
            }

            // close the polygon
            if (polygon[0] != polygon[polygon.Count - 1])
                polygon.Add(polygon[0]); // make a full loop

            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();

            GeographicCoordinateSystem wgs84 = GeographicCoordinateSystem.WGS84;

            int utmzone = (int)((polygon[0].Lng - -186.0) / 6.0);

            IProjectedCoordinateSystem utm = ProjectedCoordinateSystem.WGS84_UTM(utmzone,
                polygon[0].Lat < 0 ? false : true);

            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84, utm);

            double prod1 = 0;
            double prod2 = 0;

            for (int a = 0; a < (polygon.Count - 1); a++)
            {
                double[] pll1 = { polygon[a].Lng, polygon[a].Lat };
                double[] pll2 = { polygon[a + 1].Lng, polygon[a + 1].Lat };

                double[] p1 = trans.MathTransform.Transform(pll1);
                double[] p2 = trans.MathTransform.Transform(pll2);

                prod1 += p1[0] * p2[1];
                prod2 += p1[1] * p2[0];
            }

            double answer = (prod1 - prod2) / 2;

            if (polygon[0] == polygon[polygon.Count - 1])
                polygon.RemoveAt(polygon.Count - 1); // unmake a full loop

            return answer;
        }

        private void deletelist_Click(object sender, EventArgs e)
        {
            CleanWayPoint_button_Click_1(sender, e);
        }
        
        Amov.Planner.Controls.Icon.Polygon polyicon = new Amov.Planner.Controls.Icon.Polygon();

        private void gMapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if(polygongridmode == true && e.Button == MouseButtons.Left && currentrectmarkermove)
            {
                addpolygonpoint_Click(sender, e);
            }

            // check if the mouse up happend over our button           后续在添加
            if (polyicon.Rectangle.Contains(e.Location))
            {
                polyicon.IsSelected = !polyicon.IsSelected;

                if (e.Button == MouseButtons.Right)
                {
                    polyicon.IsSelected = false;
                    clearpolygon_Click(this, null);

                    contextMenuStrip1.Visible = false;

                    return;
                }

                if (polyicon.IsSelected)
                {
                    polygongridmode = true;
                }
                else
                {
                    polygongridmode = false;
                }

                return;
            }

            MouseDownEnd = gMapControl.FromLocalToLatLng(e.X, e.Y);

            // Console.WriteLine("MainMap MU");

            if (e.Button == MouseButtons.Right) // ignore right clicks
            {
                return;
            }

            if (isMouseDown) // mouse down on some other object and dragged to here.
            {
                // drag finished, update poi db
                //if (CurrentPOIMarker != null) //兴趣点暂时未添加
                //{
                //    POI.POIMove(CurrentPOIMarker);
                //    CurrentPOIMarker = null;
                //}

                if (e.Button == MouseButtons.Left)
                {
                    isMouseDown = false;
                }
                if (ModifierKeys == Keys.Control)
                {
                    // group select wps
                    GMapPolygon poly = new GMapPolygon(new List<PointLatLng>(), "temp");

                    poly.Points.Add(MouseDownStart);
                    poly.Points.Add(new PointLatLng(MouseDownStart.Lat, MouseDownEnd.Lng));
                    poly.Points.Add(MouseDownEnd);
                    poly.Points.Add(new PointLatLng(MouseDownEnd.Lat, MouseDownStart.Lng));

                    foreach (var marker in objectsoverlay.Markers)
                    {
                        if (poly.IsInside(marker.Position))
                        {
                            try
                            {
                                if (marker.Tag != null)
                                {
                                    groupmarkeradd(marker);
                                }
                            }
                            catch (Exception ex)
                            {
                                //log.Error(ex);
                            }
                        }
                    }

                    isMouseDraging = false;
                    return;
                }
                if (!isMouseDraging)
                {
                    //if (CurentRectMarker != null)
                    //{
                    //    // cant add WP in existing rect
                    //}
                    //else
                    //{
                    //    //AddWPToMap(currentMarker.Position.Lat, currentMarker.Position.Lng, 0);
                    //}
                }
                else //拖拉事件
                {
                    if (groupmarkers.Count > 0)
                    {
                        Dictionary<string, PointLatLng> dest = new Dictionary<string, PointLatLng>();

                        foreach (var markerid in groupmarkers)
                        {
                            for (int a = 0; a < objectsoverlay.Markers.Count; a++)
                            {
                                var marker = objectsoverlay.Markers[a];

                                if (marker.Tag != null && marker.Tag.ToString() == markerid.ToString())
                                {
                                    dest[marker.Tag.ToString()] = marker.Position;
                                    break;
                                }
                            }
                        }

                        foreach (KeyValuePair<string, PointLatLng> item in dest)
                        {
                            var value = item.Value;
                            //quickadd = true;
                            callMeDrag(item.Key, value.Lat, value.Lng, -1);
                            //quickadd = false;
                        }

                        gMapControl.SelectedArea = RectLatLng.Empty;
                        groupmarkers.Clear();
                        // redraw to remove selection
                        writeKML();

                        CurentRectMarker = null;
                    }

                    if (CurentRectMarker != null && CurentRectMarker.InnerMarker != null)
                    {
                        if (CurentRectMarker.InnerMarker.Tag.ToString().Contains("grid"))
                        {
                            try
                            {
                                drawnpolygon.Points[
                                    int.Parse(CurentRectMarker.InnerMarker.Tag.ToString().Replace("grid", "")) - 1] =
                                    new PointLatLng(MouseDownEnd.Lat, MouseDownEnd.Lng);
                                gMapControl.UpdatePolygonLocalPosition(drawnpolygon);
                                gMapControl.Invalidate();
                            }
                            catch (Exception ex)
                            {
                                //log.Error(ex);
                            }
                        }
                        else
                        {
                            callMeDrag(CurentRectMarker.InnerMarker.Tag.ToString(), currentMarker.Position.Lat,
                            currentMarker.Position.Lng, -2);
                        }
                        CurentRectMarker = null;
                    }
                }
            }

            isMouseDraging = false;
        }

        bool measuredis;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            measuredis = checkBox1.Checked;
            gMapControl.Invalidate();
        }

        /// <summary>
        /// used to redraw the polygon
        /// </summary>
        void RegeneratePolygon()
        {
            List<PointLatLng> polygonPoints = new List<PointLatLng>();

            if (objectsoverlay == null)
                return;

            foreach (GMapMarker m in objectsoverlay.Markers)
            {
                if (m is GMapMarkerRect)
                {
                    if (m.Tag == null)
                    {
                        m.Tag = polygonPoints.Count;
                        polygonPoints.Add(m.Position);
                    }
                }
            }

            if (wppolygon == null)
            {
                wppolygon = new GMapPolygon(polygonPoints, "polygon test");
                polygonsoverlay.Polygons.Add(wppolygon);
            }
            else
            {
                wppolygon.Points.Clear();
                wppolygon.Points.AddRange(polygonPoints);

                wppolygon.Stroke = new Pen(Color.Yellow, 4);
                wppolygon.Stroke.DashStyle = DashStyle.Custom;
                wppolygon.Fill = Brushes.Transparent;

                if (polygonsoverlay.Polygons.Count == 0)
                {
                    polygonsoverlay.Polygons.Add(wppolygon);
                }
                else
                {
                    lock (thisLock)
                    {
                        gMapControl.UpdatePolygonLocalPosition(wppolygon);
                    }
                }
            }
        }

        PointLatLng point;
        static public Object thisLock = new Object();
        // polygons
        GMapPolygon wppolygon;
        GMapPolygon geofencepolygon;
        bool currentrectmarkermove;
        private void gMapControl_Move(object sender, MouseEventArgs e)
        {
            point = gMapControl.FromLocalToLatLng(e.X, e.Y);
            movex = e.X;
            movey = e.Y;
            if (MouseDownStart == point)
                return;

            //  Console.WriteLine("MainMap MM " + point);

            currentMarker.Position = point;  //

            if (!isMouseDown) //未按下则更新鼠标位置
            {
                // update mouse pos display
                SetMouseDisplay(point.Lat, point.Lng, 0);
            }

            //draging
            if (e.Button == MouseButtons.Left && isMouseDown)  //拖拉地图，左键按下以及长按
            {
                isMouseDraging = true; //鼠标拖拉地图
                currentrectmarkermove = false;
                if (CurrentRallyPt != null) 
                {
                    PointLatLng pnew = gMapControl.FromLocalToLatLng(e.X, e.Y); //更新当前点集到新的位置

                    CurrentRallyPt.Position = pnew;
                }
                else if (groupmarkers.Count > 0)  //groupmarker列表信息，有航点 但没有选中点
                {
                    // group drag

                    double latdif = MouseDownStart.Lat - point.Lat;
                    double lngdif = MouseDownStart.Lng - point.Lng;

                    MouseDownStart = point;

                    Hashtable seen = new Hashtable();

                    foreach (var markerid in groupmarkers)
                    {
                        if (seen.ContainsKey(markerid))
                            continue;

                        seen[markerid] = 1; //添加哈希表 markid 对应 1
                        for (int a = 0; a < objectsoverlay.Markers.Count; a++)
                        {
                            var marker = objectsoverlay.Markers[a];

                            if (marker.Tag != null && marker.Tag.ToString() == markerid.ToString())
                            {
                                var temp = new PointLatLng(marker.Position.Lat, marker.Position.Lng);
                                temp.Offset(latdif, -lngdif);
                                marker.Position = temp;
                            }
                        }
                    }
                }
                else if (CurentRectMarker != null) // left click pan 是否选中点
                {
                    try
                    {
                        
                        // check if this is a grid point
                        if (CurentRectMarker.InnerMarker.Tag.ToString().Contains("grid")) //是否是测绘点
                        {
                            drawnpolygon.Points[
                                int.Parse(CurentRectMarker.InnerMarker.Tag.ToString().Replace("grid", "")) - 1] =
                                new PointLatLng(point.Lat, point.Lng);
                            gMapControl.UpdatePolygonLocalPosition(drawnpolygon);
                            gMapControl.Invalidate();
                        }
                    }
                    catch (Exception ex)
                    {
                        // log.Error(ex);
                    }

                    PointLatLng pnew = gMapControl.FromLocalToLatLng(e.X, e.Y); //重新更新新点位置

                    // adjust polyline point while we drag 调整画线
                    try
                    {
                        if (CurrentGMapMarker != null && CurrentGMapMarker.Tag is int)
                        {
                            int? pIndex = (int?)CurentRectMarker.Tag;
                            if (pIndex.HasValue)
                            {
                                if (pIndex < wppolygon.Points.Count)
                                {
                                    wppolygon.Points[pIndex.Value] = pnew;
                                    lock (thisLock)
                                    {
                                        gMapControl.UpdatePolygonLocalPosition(wppolygon);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //log.Error(ex);
                    }

                    // update rect and marker pos.
                    if (currentMarker.IsVisible)
                    {
                        currentMarker.Position = pnew;
                    }
                    CurentRectMarker.Position = pnew;

                    if (CurentRectMarker.InnerMarker != null)
                    {
                        CurentRectMarker.InnerMarker.Position = pnew;
                    }
                }
                //else if (CurrentPOIMarker != null)
                //{
                //    PointLatLng pnew = gMapControl.FromLocalToLatLng(e.X, e.Y);

                //    CurrentPOIMarker.Position = pnew;
                //}
                else if (CurrentGMapMarker != null) //选中航点
                {
                    
                    PointLatLng pnew = gMapControl.FromLocalToLatLng(e.X, e.Y);

                    CurrentGMapMarker.Position = pnew;
                }
                else if (ModifierKeys == Keys.Control)
                {
                    // draw selection box
                    double latdif = MouseDownStart.Lat - point.Lat;
                    double lngdif = MouseDownStart.Lng - point.Lng;

                    gMapControl.SelectedArea = new RectLatLng(Math.Max(MouseDownStart.Lat, point.Lat),
                        Math.Min(MouseDownStart.Lng, point.Lng), Math.Abs(lngdif), Math.Abs(latdif));
                }
                else // left click pan
                {
                    double latdif = MouseDownStart.Lat - point.Lat;
                    double lngdif = MouseDownStart.Lng - point.Lng;

                    try
                    {
                        lock (thisLock)
                        {
                            if (!isMouseClickOffMenu)
                                gMapControl.Position = new PointLatLng(gMapControl.Position.Lat + latdif,
                                   gMapControl.Position.Lng + lngdif);
                        }
                    }
                    catch (Exception ex)
                    {
                        //log.Error(ex);
                    }
                }

            }
            else if (e.Button == MouseButtons.None)
            {
                isMouseDown = false;
            }

        }

        private void but_Drawarea_Click(object sender, EventArgs e)
        {
            polygongridmode = true;
            currentrectmarkermove = true;
        }

        private void but_Cleararea_Click(object sender, EventArgs e)
        {
            clearpolygon_Click(sender, e);
        }

        private void but_Clearroutes_Click(object sender, EventArgs e)
        {
            CleanWayPoint_button_Click_1(sender, e);
        }

        private void ChangeColumnHeader(string command)
        {
            //try
            //{
            //    if (cmdParamNames.ContainsKey(command))
            //        for (int i = 1; i <= 7; i++)
            //            Commands.Columns[i].HeaderText = cmdParamNames[command][i - 1];
            //    else
            //        for (int i = 1; i <= 7; i++)
            //            Commands.Columns[i].HeaderText = "setme";
            //}
            //catch (Exception ex)
            //{
            //    CustomMessageBox.Show(ex.ToString());
            //}
        }

        private void loiter_forever_Click(object sender, EventArgs e)
        {
            selectedrow = Commands.Rows.Add();

            Commands.Rows[selectedrow].Cells[Command.Index].Value = MAVLink.MAV_CMD.永久盘旋.ToString();

            ChangeColumnHeader(MAVLink.MAV_CMD.永久盘旋.ToString());

            setfromMap(MouseDownEnd.Lat, MouseDownEnd.Lng, (int)float.Parse(TXT_DefaultAlt.Text));
        }

        public enum altmode
        {
            Relative = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT,
            Absolute = MAVLink.MAV_FRAME.GLOBAL,
            Terrain = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT
        }

        /// <summary>
        /// Actualy Sets the values into the datagrid and verifys height if turned on
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lng"></param>
        /// <param name="alt"></param>
        public void setfromMap(double lat, double lng, int alt, double p1 = 0)
        {
            //if (selectedrow > Commands.RowCount)
            //{
            //    CustomMessageBox.Show("Invalid coord, How did you do this?");
            //    return;
            //}

            //try
            //{
            //    // get current command list
            //    var currentlist = GetCommandList();
            //    // add history
            //    //history.Add(currentlist);
            //}
            //catch (Exception ex)
            //{
            //    CustomMessageBox.Show("A invalid entry has been detected\n" + ex.Message, Strings.ERROR);
            //}

            //// remove more than 20 revisions
            ////if (history.Count > 20)
            ////{
            ////    history.RemoveRange(0, history.Count - 20);
            ////}

            //DataGridViewTextBoxCell cell;
            //if (alt == -2 && Commands.Columns[Alt.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6] /*"Alt"*/))
            //{
            //    if (CHK_verifyheight.Checked && (altmode)CMB_altmode.SelectedValue != altmode.Terrain) //Drag with verifyheight // use srtm data
            //    {
            //        cell = Commands.Rows[selectedrow].Cells[Alt.Index] as DataGridViewTextBoxCell;
            //        float ans;
            //        if (float.TryParse(cell.Value.ToString(), out ans))
            //        {
            //            ans = (int)ans;

            //            DataGridViewTextBoxCell celllat =
            //                Commands.Rows[selectedrow].Cells[Lat.Index] as DataGridViewTextBoxCell;
            //            DataGridViewTextBoxCell celllon =
            //                Commands.Rows[selectedrow].Cells[Lon.Index] as DataGridViewTextBoxCell;
            //            int oldsrtm =
            //                (int)
            //                    ((srtm.getAltitude(double.Parse(celllat.Value.ToString()),
            //                        double.Parse(celllon.Value.ToString())).alt) * CurrentState.multiplierdist);
            //            int newsrtm = (int)((srtm.getAltitude(lat, lng).alt) * CurrentState.multiplierdist);
            //            int newh = (int)(ans + newsrtm - oldsrtm);

            //            cell.Value = newh;

            //            cell.DataGridView.EndEdit();
            //        }
            //    }
            //}
            //if (Commands.Columns[Lat.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][4] /*"Lat"*/))
            //{
            //    cell = Commands.Rows[selectedrow].Cells[Lat.Index] as DataGridViewTextBoxCell;
            //    cell.Value = lat.ToString("0.0000000");
            //    cell.DataGridView.EndEdit();
            //}
            //if (Commands.Columns[Lon.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][5] /*"Long"*/))
            //{
            //    cell = Commands.Rows[selectedrow].Cells[Lon.Index] as DataGridViewTextBoxCell;
            //    cell.Value = lng.ToString("0.0000000");
            //    cell.DataGridView.EndEdit();
            //}
            //if (alt != -1 && alt != -2 &&
            //    Commands.Columns[Alt.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][6] /*"Alt"*/))
            //{
            //    cell = Commands.Rows[selectedrow].Cells[Alt.Index] as DataGridViewTextBoxCell;

            //    {
            //        double result;
            //        bool pass = double.TryParse(TXT_homealt.Text, out result);

            //        if (pass == false)
            //        {
            //            CustomMessageBox.Show("You must have a home altitude");
            //            string homealt = "100";
            //            if (DialogResult.Cancel == InputBox.Show("Home Alt", "Home Altitude", ref homealt))
            //                return;
            //            TXT_homealt.Text = homealt;
            //        }
            //        int results1;
            //        if (!int.TryParse(TXT_DefaultAlt.Text, out results1))
            //        {
            //            CustomMessageBox.Show("Your default alt is not valid");
            //            return;
            //        }

            //        if (results1 == 0)
            //        {
            //            string defalt = "100";
            //            if (DialogResult.Cancel == InputBox.Show("Default Alt", "Default Altitude", ref defalt))
            //                return;
            //            TXT_DefaultAlt.Text = defalt;
            //        }
            //    }

            //    cell.Value = TXT_DefaultAlt.Text;

            //    float ans;
            //    if (float.TryParse(cell.Value.ToString(), out ans))
            //    {
            //        ans = (int)ans;
            //        if (alt != 0) // use passed in value;
            //            cell.Value = alt.ToString();
            //        if (ans == 0) // default
            //            cell.Value = 50;
            //        if (ans == 0 && (AmovPlanner.dv1.comPort.MAV.cs.firmware == Device.Firmwares.ArduCopter2))
            //            cell.Value = 15;

            //        // not online and verify alt via srtm
            //        if (CHK_verifyheight.Checked) // use srtm data
            //        {
            //            // is absolute but no verify
            //            if ((altmode)CMB_altmode.SelectedValue == altmode.Absolute)
            //            {
            //                //abs
            //                cell.Value =
            //                    ((srtm.getAltitude(lat, lng).alt) * CurrentState.multiplierdist +
            //                     int.Parse(TXT_DefaultAlt.Text)).ToString();
            //            }
            //            else if ((altmode)CMB_altmode.SelectedValue == altmode.Terrain)
            //            {
            //                cell.Value = int.Parse(TXT_DefaultAlt.Text);
            //            }
            //            else
            //            {
            //                //relative and verify
            //                cell.Value =
            //                    ((int)(srtm.getAltitude(lat, lng).alt) * CurrentState.multiplierdist +
            //                     int.Parse(TXT_DefaultAlt.Text) -
            //                     (int)
            //                         srtm.getAltitude(MainV2.comPort.MAV.cs.HomeLocation.Lat,
            //                             MainV2.comPort.MAV.cs.HomeLocation.Lng).alt * CurrentState.multiplierdist)
            //                        .ToString();
            //            }
            //        }

            //        cell.DataGridView.EndEdit();
            //    }
            //    else
            //    {
            //        CustomMessageBox.Show("Invalid Home or wp Alt");
            //        cell.Style.BackColor = Color.Red;
            //    }
            //}

            //// convert to utm
            //convertFromGeographic(lat, lng);

            //// Add more for other params
            //if (Commands.Columns[Param1.Index].HeaderText.Equals(cmdParamNames["WAYPOINT"][1] /*"Delay"*/))
            //{
            //    cell = Commands.Rows[selectedrow].Cells[Param1.Index] as DataGridViewTextBoxCell;
            //    cell.Value = p1;
            //    cell.DataGridView.EndEdit();
            //}

            //writeKML();
            //Commands.EndEdit();
        }

        //command needs
        void setgradanddistandaz()
        {
            int a = 0;
            PointLatLngAlt last = AmovPlanner.dv1.comPort.MAV.cs.HomeLocation;
            foreach (var lla in pointlist)
            {
                if (lla == null)
                    continue;
                try
                {
                    if (lla.Tag != null && lla.Tag != "H" && !lla.Tag.Contains("ROI"))
                    {
                        double height = lla.Alt - last.Alt;
                        double distance = lla.GetDistance(last) * CurrentState.multiplierdist;
                        double grad = height / distance;

                        Commands.Rows[int.Parse(lla.Tag) - 1].Cells[Grad.Index].Value =
                            (grad * 100).ToString("0.0");

                        Commands.Rows[int.Parse(lla.Tag) - 1].Cells[Angle.Index].Value =
                            ((180.0 / Math.PI) * Math.Atan(grad)).ToString("0.0");

                        Commands.Rows[int.Parse(lla.Tag) - 1].Cells[Dist.Index].Value =
                            (Math.Sqrt(Math.Pow(distance, 2) + Math.Pow(height, 2))).ToString("0.0");

                        Commands.Rows[int.Parse(lla.Tag) - 1].Cells[AZ.Index].Value =
                            ((lla.GetBearing(last) + 180) % 360).ToString("0");
                    }
                }
                catch (Exception ex)
                {
                    //log.Error(ex);
                }
                a++;
                last = lla;
            }
        }

        void convertFromMGRS(int rowindex)
        {
            try
            {
                var mgrs = Commands[MGRS.Index, rowindex].Value.ToString();

                MGRS temp = new MGRS(mgrs);

                var convert = temp.ConvertTo<Geographic>();

                if (convert.Latitude == 0 || convert.Longitude == 0)
                    return;

                Commands[Lat.Index, rowindex].Value = convert.Latitude.ToString();
                Commands[Lon.Index, rowindex].Value = convert.Longitude.ToString();

            }
            catch (Exception ex)
            {
               // log.Error(ex);
                return;
            }
        }

        void convertFromUTM(int rowindex)
        {
            try
            {
                var zone = int.Parse(Commands[coordZone.Index, rowindex].Value.ToString());

                var east = double.Parse(Commands[coordEasting.Index, rowindex].Value.ToString());

                var north = double.Parse(Commands[coordNorthing.Index, rowindex].Value.ToString());

                if (east == 0 && north == 0)
                {
                    return;
                }

                var utm = new utmpos(east, north, zone);

                Commands[Lat.Index, rowindex].Value = utm.ToLLA().Lat;
                Commands[Lon.Index, rowindex].Value = utm.ToLLA().Lng;

            }
            catch (Exception ex)
            {
                //log.Error(ex);
                return;
            }
        }

        private void convertFromGeographic(double lat, double lng)
        {
            if (lat == 0 && lng == 0)
            {
                return;
            }

            // always update other systems, incase user switchs while planning
            try
            {
                //UTM
                var temp = new PointLatLngAlt(lat, lng);
                int zone = temp.GetUTMZone();
                var temp2 = temp.ToUTM();
                Commands[coordZone.Index, selectedrow].Value = zone;
                Commands[coordEasting.Index, selectedrow].Value = temp2[0].ToString("0.000");
                Commands[coordNorthing.Index, selectedrow].Value = temp2[1].ToString("0.000");
            }
            catch (Exception ex)
            {
               // log.Error(ex);
            }
            try
            {
                //MGRS
                Commands[MGRS.Index, selectedrow].Value = ((MGRS)new Geographic(lng, lat)).ToString();
            }
            catch (Exception ex)
            {
               // log.Error(ex);
            }
        }

        /// <summary>
        /// Used to update column headers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Commands_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (quickadd)
            //    return;
            try
            {
                selectedrow = e.RowIndex;
                string option = Commands[Command.Index, selectedrow].EditedFormattedValue.ToString();
                string cmd;
                try
                {
                    if (Commands[Command.Index, selectedrow].Value != null)
                        cmd = Commands[Command.Index, selectedrow].Value.ToString();
                    else
                        cmd = option;
                }
                catch
                {
                    cmd = option;
                }
                //Console.WriteLine("editformat " + option + " value " + cmd);
                ChangeColumnHeader(cmd);

                if (cmd == "WAYPOINT")
                {
                }

                //  writeKML();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.ToString());
            }
        }

        void Commands_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // update row headers
            ((ComboBox)sender).ForeColor = Color.White;
            ChangeColumnHeader(((ComboBox)sender).Text);
            try
            {
                // default takeoff to non 0 alt
                if (((ComboBox)sender).Text == "TAKEOFF")
                {
                    if (Commands.Rows[selectedrow].Cells[Alt.Index].Value != null &&
                        Commands.Rows[selectedrow].Cells[Alt.Index].Value.ToString() == "0")
                        Commands.Rows[selectedrow].Cells[Alt.Index].Value = TXT_DefaultAlt.Text;
                }

                // default to take shot
                if (((ComboBox)sender).Text == "DO_DIGICAM_CONTROL")
                {
                    if (Commands.Rows[selectedrow].Cells[Lat.Index].Value != null &&
                        Commands.Rows[selectedrow].Cells[Lat.Index].Value.ToString() == "0")
                        Commands.Rows[selectedrow].Cells[Lat.Index].Value = (1).ToString();
                }

                if (((ComboBox)sender).Text == "UNKNOWN")
                {
                    string cmdid = "-1";
                    if (InputBox.Show("Mavlink ID", "Please enter the command ID", ref cmdid) == DialogResult.OK)
                    {
                        if (cmdid != "-1")
                        {
                            Commands.Rows[selectedrow].Cells[Command.Index].Tag = ushort.Parse(cmdid);
                        }
                    }
                }

                for (int i = 0; i < Commands.ColumnCount; i++)
                {
                    DataGridViewCell tcell = Commands.Rows[selectedrow].Cells[i];
                    if (tcell.GetType() == typeof(DataGridViewTextBoxCell))
                    {
                        if (tcell.Value.ToString() == "?")
                            tcell.Value = "0";
                    }
                }
            }
            catch (Exception ex)
            {
               // log.Error(ex);
            }
        }
        //command-------------------------------------------------
        /// <summary>
        /// used to control buttons in the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Commands_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;
                if (e.ColumnIndex == Delete.Index && (e.RowIndex + 0) < Commands.RowCount) // delete
                {
                    //quickadd = true;
                    // mono fix
                    Commands.CurrentCell = null;
                    Commands.Rows.RemoveAt(e.RowIndex);
                    //quickadd = false;
                    writeKML();
                }
                if (e.ColumnIndex == Up.Index && e.RowIndex != 0) // up
                {
                    DataGridViewRow myrow = Commands.CurrentRow;
                    Commands.Rows.Remove(myrow);
                    Commands.Rows.Insert(e.RowIndex - 1, myrow);
                    writeKML();
                }
                if (e.ColumnIndex == Down.Index && e.RowIndex < Commands.RowCount - 1) // down
                {
                    DataGridViewRow myrow = Commands.CurrentRow;
                    Commands.Rows.Remove(myrow);
                    Commands.Rows.Insert(e.RowIndex + 1, myrow);
                    writeKML();
                }
                setgradanddistandaz();
            }
            catch (Exception)
            {
                CustomMessageBox.Show("Row error");
            }
        }

        private void Commands_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // we have modified a utm coords
            if (e.ColumnIndex == coordZone.Index ||
                e.ColumnIndex == coordNorthing.Index ||
                e.ColumnIndex == coordEasting.Index)
            {
                convertFromUTM(e.RowIndex);
            }

            if (e.ColumnIndex == MGRS.Index)
            {
                convertFromMGRS(e.RowIndex);
            }

            // we have modified a ll coord
            if (e.ColumnIndex == Lat.Index ||
                e.ColumnIndex == Lon.Index)
            {
                try
                {
                    var lat = double.Parse(Commands.Rows[e.RowIndex].Cells[Lat.Index].Value.ToString());
                    var lng = double.Parse(Commands.Rows[e.RowIndex].Cells[Lon.Index].Value.ToString());
                    convertFromGeographic(lat, lng);
                }
                catch (Exception ex)
                {
                   // log.Error(ex);
                    CustomMessageBox.Show("Invalid Lat/Lng, please fix", Strings.ERROR);
                }
            }

            Commands_RowEnter(null,
                new DataGridViewCellEventArgs(Commands.CurrentCell.ColumnIndex, Commands.CurrentCell.RowIndex));

            writeKML();
        }

        private void Commands_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = false;
            e.ThrowException = false;
        }

        private void Commands_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[Delete.Index].Value = "X";
            e.Row.Cells[Up.Index].Value = Resources.front;
            e.Row.Cells[Down.Index].Value = Resources.back;
        }

        private void Commands_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                var temp = ((ComboBox)e.Control);
                ((ComboBox)e.Control).SelectionChangeCommitted -= Commands_SelectionChangeCommitted;
                ((ComboBox)e.Control).SelectionChangeCommitted += Commands_SelectionChangeCommitted;
                ((ComboBox)e.Control).ForeColor = Color.White;
                ((ComboBox)e.Control).BackColor = Color.FromArgb(0x43, 0x44, 0x45);
                Debug.WriteLine("Setting event handle");
            }
        }

        private void Commands_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            //if (quickadd)
            //    return;
            try
            {
                selectedrow = e.RowIndex;
                string option = Commands[Command.Index, selectedrow].EditedFormattedValue.ToString();
                string cmd;
                try
                {
                    if (Commands[Command.Index, selectedrow].Value != null)
                        cmd = Commands[Command.Index, selectedrow].Value.ToString();
                    else
                        cmd = option;
                }
                catch
                {
                    cmd = option;
                }
                //Console.WriteLine("editformat " + option + " value " + cmd);
                ChangeColumnHeader(cmd);

                if (cmd == "WAYPOINT")
                {
                }

                //  writeKML();
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.ToString());
            }
        }

        private void Commands_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int i = 0; i < Commands.ColumnCount; i++)
            {
                DataGridViewCell tcell = Commands.Rows[e.RowIndex].Cells[i];
                if (tcell.GetType() == typeof(DataGridViewTextBoxCell))
                {
                    if (tcell.Value == null)
                        tcell.Value = "0";
                }
            }

            DataGridViewComboBoxCell cell = Commands.Rows[e.RowIndex].Cells[Command.Index] as DataGridViewComboBoxCell;
            if (cell.Value == null)
            {
                cell.Value = "WAYPOINT";
                cell.DropDownWidth = 200;
                Commands.Rows[e.RowIndex].Cells[Delete.Index].Value = "X";
                //if (!quickadd)
                //{
                //    Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex - 0)); // do header labels
                //    Commands_RowValidating(sender, new DataGridViewCellCancelEventArgs(0, e.RowIndex));
                //    // do default values
                //}
            }

            //if (quickadd)
            //    return;

            try
            {
                Commands.CurrentCell = Commands.Rows[e.RowIndex].Cells[0];

                if (Commands.Rows.Count > 1)
                {
                    if (Commands.Rows[e.RowIndex - 1].Cells[Command.Index].Value.ToString() == "WAYPOINT")
                    {
                        Commands.Rows[e.RowIndex].Selected = true; // highlight row
                    }
                    else
                    {
                        Commands.CurrentCell = Commands[1, e.RowIndex - 1];
                        //Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex-1));
                    }
                }
            }
            catch (Exception)
            {
            }
            // Commands.EndEdit();
        }

        private void Commands_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            selectedrow = e.RowIndex;
            Commands_RowEnter(sender, new DataGridViewCellEventArgs(0, e.RowIndex - 0));
            // do header labels - encure we dont 0 out valid colums
            int cols = Commands.Columns.Count;
            for (int a = 1; a < cols; a++)
            {
                DataGridViewTextBoxCell cell;
                cell = Commands.Rows[selectedrow].Cells[a] as DataGridViewTextBoxCell;

                if (Commands.Columns[a].HeaderText.Equals("") && cell != null && cell.Value == null)
                {
                    cell.Value = "0";
                }
                else
                {
                    if (cell != null && (cell.Value == null || cell.Value.ToString() == ""))
                    {
                        cell.Value = "?";
                    }
                }
            }
        }

        private void but_Measuringconfig_Click(object sender, EventArgs e)
        {
            //gridui gd = new gridui();
            GridPlugin grid = new GridPlugin();
            grid.Host = new PluginHost();
            grid.but_Click(sender, e);
            //if (drawnpolygon.Points.Count > 1)
            //{
            //    if(swpcp.totalWPlist.Count < 2)
            //        gd.Show();
            //    else
            //    {
            //        DialogResult result = MessageBox.Show("已有航线，是否再次生成航线?", "", MessageBoxButtons.OKCancel);
            //        if (result == DialogResult.OK)
            //        {
            //            Dispose();
            //            gd.Show();
            //        }
                    
            //    }   
            //}
            //else
            //    MessageBox.Show("请先规划区域。");

        }

        private void but_Quickdraw_Click(object sender, EventArgs e)
        {

        }

        private void loiter_time_Click(object sender, EventArgs e)
        {
            string time = "5";
            if (DialogResult.Cancel == InputBox.Show("Loiter Time", "Loiter Time", ref time))
                return;

            selectedrow = Commands.Rows.Add();

            Commands.Rows[selectedrow].Cells[Command.Index].Value = MAVLink.MAV_CMD.时间盘旋.ToString();

            Commands.Rows[selectedrow].Cells[Param1.Index].Value = time;

            ChangeColumnHeader(MAVLink.MAV_CMD.时间盘旋.ToString());

            setfromMap(MouseDownEnd.Lat, MouseDownEnd.Lng, (int)float.Parse(TXT_DefaultAlt.Text));

            writeKML();
        }

        private void panel1_ExpandClick(object sender, EventArgs e)
        {
            Commands.AutoResizeColumns();
        }

        private void but_Savefile_Click(object sender, EventArgs e)
        {
            SaveFile_Click(null, null);
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            savewaypoints();
            
        }
        internal string wpfilename;
        private void savewaypoints()
        {
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.Filter = "Mission|*.waypoints;*.txt|Mission JSON|*.mission";
                fd.DefaultExt = ".waypoints";
                fd.FileName = wpfilename;
                DialogResult result = fd.ShowDialog();
                string file = fd.FileName;
                if (file != "")
                {
                    try
                    {
                        if (file.EndsWith(".mission"))
                        {
                            var list = GetCommandList();
                            Locationwp home = new Locationwp();
                            //try
                            //{
                            //    home.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
                            //    home.lat = (double.Parse(TXT_homelat.Text));
                            //    home.lng = (double.Parse(TXT_homelng.Text));
                            //    home.alt = (float.Parse(TXT_homealt.Text) / CurrentState.multiplierdist); // use saved home
                            //}
                            //catch { }

                            list.Insert(0, home);

                            //var format = MissionFile.ConvertFromLocationwps(list, (byte)(altmode)CMB_altmode.SelectedValue);

                            //MissionFile.WriteFile(file, format);
                            return;
                        }

                        StreamWriter sw = new StreamWriter(file);
                        //sw.WriteLine("QGC WPL 110");
                        //try
                        //{
                        //    sw.WriteLine("0\t1\t0\t16\t0\t0\t0\t0\t" +
                        //                 double.Parse(TXT_homelat.Text).ToString("0.000000", new CultureInfo("en-US")) +
                        //                 "\t" +
                        //                 double.Parse(TXT_homelng.Text).ToString("0.000000", new CultureInfo("en-US")) +
                        //                 "\t" +
                        //                 double.Parse(TXT_homealt.Text).ToString("0.000000", new CultureInfo("en-US")) +
                        //                 "\t1");
                        //}
                        //catch (Exception ex)
                        //{
                        //    log.Error(ex);
                        //    sw.WriteLine("0\t1\t0\t0\t0\t0\t0\t0\t0\t0\t0\t1");
                        //}
                        for (int a = 0; a < Commands.Rows.Count - 0; a++)
                        {
                            ushort mode = 0;

                            if (Commands.Rows[a].Cells[0].Value.ToString() == "UNKNOWN")
                            {
                                mode = (ushort)Commands.Rows[a].Cells[Command.Index].Tag;
                            }
                            else
                            {
                                mode =
                                (ushort)
                                    (MAVLink.MAV_CMD)
                                        Enum.Parse(typeof(MAVLink.MAV_CMD), Commands.Rows[a].Cells[Command.Index].Value.ToString());
                            }

                            sw.Write((a + 1)); // seq
                            sw.Write("\t" + 0); // current
                            //sw.Write("\t" + CMB_altmode.SelectedValue); //frame 
                            sw.Write("\t" + mode);
                            sw.Write("\t" +
                                     double.Parse(Commands.Rows[a].Cells[Param1.Index].Value.ToString())
                                         .ToString("0.000000", new CultureInfo("en-US")));
                            sw.Write("\t" +
                                     double.Parse(Commands.Rows[a].Cells[Param2.Index].Value.ToString())
                                         .ToString("0.000000", new CultureInfo("en-US")));
                            sw.Write("\t" +
                                     double.Parse(Commands.Rows[a].Cells[Param3.Index].Value.ToString())
                                         .ToString("0.000000", new CultureInfo("en-US")));
                            sw.Write("\t" +
                                     double.Parse(Commands.Rows[a].Cells[Param4.Index].Value.ToString())
                                         .ToString("0.000000", new CultureInfo("en-US")));
                            sw.Write("\t" +
                                     double.Parse(Commands.Rows[a].Cells[Lat.Index].Value.ToString())
                                         .ToString("0.000000", new CultureInfo("en-US")));
                            sw.Write("\t" +
                                     double.Parse(Commands.Rows[a].Cells[Lon.Index].Value.ToString())
                                         .ToString("0.000000", new CultureInfo("en-US")));
                            sw.Write("\t" +
                                     (double.Parse(Commands.Rows[a].Cells[Alt.Index].Value.ToString()) /
                                      CurrentState.multiplierdist).ToString("0.000000", new CultureInfo("en-US")));
                            sw.Write("\t" + 1);
                            sw.WriteLine("");
                        }
                        sw.Close();

                        lbl_wpfile.Text = "Saved " + Path.GetFileName(file);
                    }
                    catch (Exception)
                    {
                        CustomMessageBox.Show(Strings.ERROR);
                    }
                }
            }
        }
    }
}
