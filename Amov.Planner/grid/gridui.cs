using CCWin;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amov.Planner.views;
using MAVTOOL.Utilities;
using MissionPlanner;
using GMap.NET.WindowsForms.Markers;
using static Amov.Planner.views.flightplanner;
using Amov.Planner.Utilities;
using MAVTOOL;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using System.IO;
using AmovPlanner;
using MAVTOOL.Comms;
using System.Xml;

namespace Amov.Planner.grid
{
    public partial class gridui : Skin_Mac
    {

        // Variables
        const double rad2deg = (180 / Math.PI); //弧度转角度
        const double deg2rad = (1.0 / rad2deg); //角度转弧度

        private GridPlugin plugin;
        static public Object thisLock = new Object();

        GMapOverlay routesOverlay;
        GMapOverlay kmlpolygonsoverlay;
        List<PointLatLngAlt> list = new List<PointLatLngAlt>();
        List<PointLatLngAlt> grid;
        bool loadedfromfile = false;
        bool loading = false;

        Dictionary<string, camerainfo> cameras = new Dictionary<string, camerainfo>();

        public string DistUnits = "";
        public string inchpixel = "";
        public string feet_fovH = "";
        public string feet_fovV = "";

        internal PointLatLng MouseDownStart = new PointLatLng();
        internal PointLatLng MouseDownEnd;
        internal PointLatLngAlt CurrentGMapMarkerStartPos;
        PointLatLng currentMousePosition;
        GMapMarker marker;
        GMapMarker CurrentGMapMarker = null;
        GMapMarkerOverlapCount GMapMarkerOverlap = new GMapMarkerOverlapCount(PointLatLng.Empty);
        int CurrentGMapMarkerIndex = 0;
        bool isMouseDown = false;
        bool isMouseDraging = false;
        // Structures
        public struct camerainfo
        {
            public string name;
            public float focallen;
            public float sensorwidth;
            public float sensorheight;
            public float imagewidth;
            public float imageheight;
        }

        public struct GridData
        {
            public List<PointLatLngAlt> poly;
            //simple
            public string camera;
            public decimal alt;
            public decimal angle;
            public bool camdir;
            public decimal speed;
            public bool usespeed;
            public bool autotakeoff;
            public bool autotakeoff_RTL;

            public decimal splitmission;

            public bool internals;
            public bool footprints;
            public bool advanced;

            //options
            public decimal dist;
            public decimal overshoot1;
            public decimal overshoot2;
            public decimal leadin;
            public string startfrom;
            public decimal overlap;
            public decimal sidelap;
            public decimal spacing;
            public bool crossgrid;
            // Copter Settings
            public decimal copter_delay;
            public bool copter_headinghold_chk;
            public decimal copter_headinghold;
            // plane settings
            public bool alternateLanes;
            public decimal minlaneseparation;

            // camera config
            public bool trigdist;
            public bool digicam;
            public bool repeatservo;

            public bool breaktrigdist;

            public decimal repeatservo_no;
            public decimal repeatservo_pwm;
            public decimal repeatservo_cycle;

            // do set servo
            public decimal setservo_no;
            public decimal setservo_low;
            public decimal setservo_high;
        }

        public gridui(GridPlugin plugin)
        {
            this.plugin = plugin;
            InitializeComponent();
            loading = true;

            this.WindowState = FormWindowState.Maximized;

            map.MapProvider = flightplanner.Gp;

            kmlpolygonsoverlay = new GMapOverlay("kmlpolygons");
            map.Overlays.Add(kmlpolygonsoverlay);

            routesOverlay = new GMapOverlay("routes");
            map.Overlays.Add(routesOverlay);

            // Map Events
            map.OnMapZoomChanged += new MapZoomChanged(map_OnMapZoomChanged);
            map.OnMarkerEnter += new MarkerEnter(map_OnMarkerEnter);
            map.OnMarkerLeave += new MarkerLeave(map_OnMarkerLeave);
            map.MouseUp += new MouseEventHandler(map_MouseUp);
            map.DragButton = MouseButtons.Left;
            map.OnRouteEnter += new RouteEnter(map_OnRouteEnter);
            map.OnRouteLeave += new RouteLeave(map_OnRouteLeave);

            var points = plugin.Host.FPDrawnPolygon;
            points.Points.ForEach(x => { list.Add(x); });
            points.Dispose();
            if (plugin.Host.config["distunits"] != null)
                DistUnits = plugin.Host.config["distunits"].ToString();

            CMB_startfrom.DataSource = Enum.GetNames(typeof(Grid.StartPosition));
            CMB_startfrom.SelectedIndex = 0;

            // set and angle that is good
            NUM_angle.Value = (decimal)((getAngleOfLongestSide(list) + 360) % 360);
            TXT_headinghold = (Math.Round(NUM_angle.Value)).ToString();

            if (plugin.Host.cs.firmware == MAVTOOL.Device.Firmwares.ArduPlane)
                NUM_UpDownFlySpeed = (decimal)(12 * CurrentState.multiplierspeed);

            map.MapScaleInfoEnabled = true;
            map.ScalePen = new Pen(Color.Orange);

            foreach (var temp in flightdata.kmlpolygons.Polygons)
            {
                kmlpolygonsoverlay.Polygons.Add(new GMapPolygon(temp.Points, "") { Fill = Brushes.Transparent });
            }
            foreach (var temp in flightdata.kmlpolygons.Routes)
            {
                kmlpolygonsoverlay.Routes.Add(new GMapRoute(temp.Points, ""));
            }

            xmlcamera(false, Settings.GetRunningDirectory() + "camerasBuiltin.xml");

            xmlcamera(false, Settings.GetUserDataDirectory() + "cameras.xml");

            loading = false;
        }

        private void xmlcamera(bool write, string filename)
        {
            bool exists = File.Exists(filename);

            if (write || !exists)
            {
                try
                {
                    XmlTextWriter xmlwriter = new XmlTextWriter(filename, Encoding.ASCII);
                    xmlwriter.Formatting = Formatting.Indented;

                    xmlwriter.WriteStartDocument();

                    xmlwriter.WriteStartElement("Cameras");

                    foreach (string key in cameras.Keys)
                    {
                        try
                        {
                            if (key == "")
                                continue;
                            xmlwriter.WriteStartElement("Camera");
                            xmlwriter.WriteElementString("name", cameras[key].name);
                            xmlwriter.WriteElementString("flen", cameras[key].focallen.ToString(new System.Globalization.CultureInfo("en-US")));
                            xmlwriter.WriteElementString("imgh", cameras[key].imageheight.ToString(new System.Globalization.CultureInfo("en-US")));
                            xmlwriter.WriteElementString("imgw", cameras[key].imagewidth.ToString(new System.Globalization.CultureInfo("en-US")));
                            xmlwriter.WriteElementString("senh", cameras[key].sensorheight.ToString(new System.Globalization.CultureInfo("en-US")));
                            xmlwriter.WriteElementString("senw", cameras[key].sensorwidth.ToString(new System.Globalization.CultureInfo("en-US")));
                            xmlwriter.WriteEndElement();
                        }
                        catch { }
                    }

                    xmlwriter.WriteEndElement();

                    xmlwriter.WriteEndDocument();
                    xmlwriter.Close();

                }
                catch (Exception ex) { CustomMessageBox.Show(ex.ToString()); }
            }
            else
            {
                try
                {
                    using (XmlTextReader xmlreader = new XmlTextReader(filename))
                    {
                        while (xmlreader.Read())
                        {
                            xmlreader.MoveToElement();
                            try
                            {
                                switch (xmlreader.Name)
                                {
                                    case "Camera":
                                        {
                                            camerainfo camera = new camerainfo();

                                            while (xmlreader.Read())
                                            {
                                                bool dobreak = false;
                                                xmlreader.MoveToElement();
                                                switch (xmlreader.Name)
                                                {
                                                    case "name":
                                                        camera.name = xmlreader.ReadString();
                                                        break;
                                                    case "imgw":
                                                        camera.imagewidth = float.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                        break;
                                                    case "imgh":
                                                        camera.imageheight = float.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                        break;
                                                    case "senw":
                                                        camera.sensorwidth = float.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                        break;
                                                    case "senh":
                                                        camera.sensorheight = float.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                        break;
                                                    case "flen":
                                                        camera.focallen = float.Parse(xmlreader.ReadString(), new System.Globalization.CultureInfo("en-US"));
                                                        break;
                                                    case "Camera":
                                                        cameras[camera.name] = camera;
                                                        dobreak = true;
                                                        break;
                                                }
                                                if (dobreak)
                                                    break;
                                            }
                                            string temp = xmlreader.ReadString();
                                        }
                                        break;
                                    case "Config":
                                        break;
                                    case "xml":
                                        break;
                                    default:
                                        if (xmlreader.Name == "") // line feeds
                                            break;
                                        //config[xmlreader.Name] = xmlreader.ReadString();
                                        break;
                                }
                            }
                            catch (Exception ee) { Console.WriteLine(ee.Message); } // silent fail on bad entry
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Bad Camera File: " + ex.ToString()); } // bad config file

                // populate list
                foreach (var camera in cameras.Values)
                {
                    if (!CMB_camera.Items.Contains(camera.name))
                        CMB_camera.Items.Add(camera.name);
                }
            }
        }

        double getAngleOfLongestSide(List<PointLatLngAlt> list)
        {
            if (list.Count == 0)
                return 0;
            double angle = 0;
            double maxdist = 0;
            PointLatLngAlt last = list[list.Count - 1];
            foreach (var item in list)
            {
                if (item.GetDistance(last) > maxdist)
                {
                    angle = item.GetBearing(last);
                    maxdist = item.GetDistance(last);
                }
                last = item;
            }

            return (angle + 360) % 360;
        }

        private void map_OnRouteLeave(GMapRoute item)
        {

        }

        private void map_OnRouteEnter(GMapRoute item)
        {

        }

        private void map_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void map_OnMarkerLeave(GMapMarker item)
        {

        }

        private void map_OnMarkerEnter(GMapMarker item)
        {

        }

        private void map_OnMapZoomChanged()
        {

        }

        public SetWayPointClass swpcp = new SetWayPointClass();//航点图层操作显示类

        public const double BEIJING_LAT = 39.958436;
        public const double BEIJING_LNG = 116.309175;
        private void map_Load(object sender, EventArgs e)
        {
            //************************************主界面GMAP地图的加载项目 * ****************************************
            GMaps.Instance.PrimaryCache = new MyImageCache();
            //gMap.MapProvider = GMap.NET.MapProviders.AMapProvider.Instance; ;//选择地图为高德卫星地图(GMap.NET.MapProviders.GoogleChinaMapProvider.Instance);
            //gMap.MapProvider = GMap.NET.MapProviders.GoogleChinaSatelliteMapProvider.Instance;
            //gMap.MapProvider = GMap.NET.MapProviders.GoogleChinaTerrainMapProvider.Instance;
            map.MapProvider = GMap.NET.MapProviders.GoogleChinaMapProvider.Instance;
            //gMapControl.MapProvider = GMap.NET.MapProviders
            //谷歌地图的地图更新最好，一般的地面站都选用谷歌中国卫星地图
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            //gMap.SetPositionByKeywords("Chengdu, China");//设定初始中心位置为Chengdu，成都坐标为西纬30.67度，东经104.06
            map.Position = new PointLatLng(BEIJING_LAT, BEIJING_LNG);//位置北京
            map.ShowCenter = true;//不显示中心的红色十字
            //gMapControl.DragButton = System.Windows.Forms.MouseButtons.Left;  //左键拖动地图            
            //设置地图分辨率信息
            map.MaxZoom = 30;
            map.MinZoom = 3;
            map.Zoom = 15;

            //创建一个飞行动画层，用于动态显示飞行器的飞行状态
            //Bitmap bitmap = new Bitmap(50, 50);
            //Graphics g = Graphics.FromImage(bitmap);
            //g.Clear(Color.Black);

            //swpcp.realVehicleOverlay = new GMapOverlay("realvehicle");
            //map.Overlays.Add(swpcp.realVehicleOverlay);


            //创建一个marker层，用于标记航点
            swpcp.markersOverlay_sec = new GMapOverlay("markers_sec");
            map.Overlays.Add(swpcp.markersOverlay_sec);

        }

        private void gridui_Load(object sender, EventArgs e)
        {
            loading = true;
            //if (!loadedfromfile)      加载参数  应当后期重新修改新的设置方式
            //    loadsettings();

            //TRK_zoom.Value = (float)map.Zoom;

            label1.Text += " (" + CurrentState.DistanceUnit + ")";
            //label24.Text += " (" + CurrentState.SpeedUnit + ")";

            loading = false;

            domainUpDown1_ValueChanged(this, null);

        }

   

        //void loadsettings()
        //{
        //    if (plugin.Host.config.ContainsKey("grid_camera"))
        //    {
        //        loadsetting("grid_alt", NUM_altitude);
        //        //  loadsetting("grid_angle", NUM_angle);
        //        loadsetting("grid_camdir", CHK_camdirection);
        //        loadsetting("grid_usespeed", CHK_usespeed);
        //        loadsetting("grid_speed", NUM_UpDownFlySpeed);
        //        loadsetting("grid_autotakeoff", CHK_toandland);
        //        loadsetting("grid_autotakeoff_RTL", CHK_toandland_RTL);

        //        loadsetting("grid_dist", NUM_Distance);
        //        loadsetting("grid_overshoot1", NUM_overshoot);
        //        loadsetting("grid_overshoot2", NUM_overshoot2);
        //        loadsetting("grid_leadin", NUM_leadin);
        //        loadsetting("grid_startfrom", CMB_startfrom);
        //        loadsetting("grid_overlap", num_overlap);
        //        loadsetting("grid_sidelap", num_sidelap);
        //        loadsetting("grid_spacing", NUM_spacing);
        //        loadsetting("grid_crossgrid", chk_crossgrid);

        //        // Should probably be saved as one setting, and us logic
        //        loadsetting("grid_trigdist", rad_trigdist);
        //        loadsetting("grid_digicam", rad_digicam);
        //        loadsetting("grid_repeatservo", rad_repeatservo);
        //        loadsetting("grid_breakstopstart", chk_stopstart);

        //        loadsetting("grid_repeatservo_no", NUM_reptservo);
        //        loadsetting("grid_repeatservo_pwm", num_reptpwm);
        //        loadsetting("grid_repeatservo_cycle", NUM_repttime);

        //        // camera last to it invokes a reload
        //        loadsetting("grid_camera", CMB_camera);

        //        // Copter Settings
        //        loadsetting("grid_copter_delay", NUM_copter_delay);
        //        //loadsetting("grid_copter_headinghold_chk", CHK_copter_headinghold);

        //        // Plane Settings
        //        loadsetting("grid_min_lane_separation", NUM_Lane_Dist);

        //        loadsetting("grid_internals", CHK_internals);
        //        loadsetting("grid_footprints", CHK_footprints);
        //        loadsetting("grid_advanced", CHK_advanced);
        //    }
        //}

        //void loadsetting(string key, decimal item)
        //{
        //    // soft fail on bad param
        //    try
        //    {
        //        if (plugin.Host.config.ContainsKey(key))
        //        {
        //            if (item is NumericUpDown)
        //            {
        //                ((NumericUpDown)item).Value = decimal.Parse(plugin.Host.config[key].ToString());
        //            }
        //            else if (item is ComboBox)
        //            {
        //                ((ComboBox)item).Text = plugin.Host.config[key].ToString();
        //            }
        //            else if (item is CheckBox)
        //            {
        //                ((CheckBox)item).Checked = bool.Parse(plugin.Host.config[key].ToString());
        //            }
        //            else if (item is RadioButton)
        //            {
        //                ((RadioButton)item).Checked = bool.Parse(plugin.Host.config[key].ToString());
        //            }
        //        }
        //    }
        //    catch { }
        //}

        GMapOverlay drawnpolygonsoverlay;
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


        private void CMB_camera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cameras.ContainsKey(CMB_camera.Text))
            {
                camerainfo camera = cameras[CMB_camera.Text];

                NUM_focallength.Value = (decimal)camera.focallen;
                TXT_imgheight.Text = camera.imageheight.ToString();
                TXT_imgwidth.Text = camera.imagewidth.ToString();
                TXT_sensheight.Text = camera.sensorheight.ToString();
                TXT_senswidth.Text = camera.sensorwidth.ToString();

                //NUM_Distance.Enabled = false;
            }

            GMapMarkerOverlap.Clear();

            domainUpDown1_ValueChanged(null, null);
        }
        string TXT_cmpixel;
        string TXT_fovH;
        string TXT_fovV;
        void doCalc()
        {
            try
            {
                // entered values
                float flyalt = (float)CurrentState.fromDistDisplayUnit((float)NUM_altitude.Value);
                int imagewidth = int.Parse(TXT_imgwidth.Text);
                int imageheight = int.Parse(TXT_imgheight.Text);

                int overlap = (int)num_overlap.Value;
                int sidelap = (int)num_sidelap.Value;

                double viewwidth = 0;
                double viewheight = 0;

                //getFOV(flyalt, ref viewwidth, ref viewheight);

                TXT_fovH = viewwidth.ToString("#.#");
                TXT_fovV = viewheight.ToString("#.#");
                // Imperial
                feet_fovH = (viewwidth * 3.2808399f).ToString("#.#");
                feet_fovV = (viewheight * 3.2808399f).ToString("#.#");

                //    mm  / pixels * 100
                TXT_cmpixel = ((viewheight / imageheight) * 100).ToString("0.00 cm");
                // Imperial
                inchpixel = (((viewheight / imageheight) * 100) * 0.393701).ToString("0.00 inches");

                //NUM_spacing.ValueChanged -= domainUpDown1_ValueChanged;
                //NUM_Distance.ValueChanged -= domainUpDown1_ValueChanged;

                //if (CHK_camdirection.Checked)
                //{
                //    NUM_spacing.Value = (decimal)((1 - (overlap / 100.0f)) * viewheight);
                //    NUM_Distance.Value = (decimal)((1 - (sidelap / 100.0f)) * viewwidth);
                //}
                //else
                //{
                //    NUM_spacing.Value = (decimal)((1 - (overlap / 100.0f)) * viewwidth);
                //    NUM_Distance.Value = (decimal)((1 - (sidelap / 100.0f)) * viewheight);
                //}
                //NUM_spacing.ValueChanged += domainUpDown1_ValueChanged;
                //NUM_Distance.ValueChanged += domainUpDown1_ValueChanged;
            }
            catch { return; }
        }

        double calcpolygonarea(List<PointLatLngAlt> polygon)
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

            IProjectedCoordinateSystem utm = ProjectedCoordinateSystem.WGS84_UTM(utmzone, polygon[0].Lat < 0 ? false : true);

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

            return Math.Abs(answer);
        }

        public void LoadGrid()
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(GridData));

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "*.grid|*.grid";
                ofd.ShowDialog();

                if (File.Exists(ofd.FileName))
                {
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        var test = (GridData)reader.Deserialize(sr);

                        loading = true;
                        loadgriddata(test);
                        loading = false;
                    }
                }
            }
        }
        decimal NUM_spacing;
        decimal NUM_Distance;
        decimal NUM_overshoot;
        decimal NUM_overshoot2;
        decimal NUM_leadin;
        decimal NUM_Lane_Dist;
        string TXT_headinghold;
        decimal NUM_UpDownFlySpeed;
        void loadgriddata(GridData griddata)
        {
            list = griddata.poly;

            CMB_camera.Text = griddata.camera;
            NUM_altitude.Value = griddata.alt;
            NUM_angle.Value = griddata.angle;
            //CHK_camdirection.Checked = griddata.camdir;
            // CHK_usespeed.Checked = griddata.usespeed;
            NUM_UpDownFlySpeed = griddata.speed;
            //CHK_toandland.Checked = griddata.autotakeoff;
            // CHK_toandland_RTL.Checked = griddata.autotakeoff_RTL;
            //NUM_split.Value = griddata.splitmission;


            NUM_dist.Value = griddata.dist;
            NUM_overshoot = griddata.overshoot1;
            NUM_overshoot2 = griddata.overshoot2;
            NUM_leadin = griddata.leadin;
            CMB_startfrom.Text = griddata.startfrom;
            num_overlap.Value = griddata.overlap;
            num_sidelap.Value = griddata.sidelap;
            NUM_spacing = griddata.spacing;
            //chk_crossgrid.Checked = griddata.crossgrid;

            // rad_trigdist.Checked = griddata.trigdist;
            //rad_digicam.Checked = griddata.digicam;
            //rad_repeatservo.Checked = griddata.repeatservo;
            //chk_stopstart.Checked = griddata.breaktrigdist;

            // NUM_reptservo.Value = griddata.repeatservo_no;
            // num_reptpwm.Value = griddata.repeatservo_pwm;
            // NUM_repttime.Value = griddata.repeatservo_cycle;

            // num_setservono.Value = griddata.setservo_no;
            // num_setservolow.Value = griddata.setservo_low;
            // num_setservohigh.Value = griddata.setservo_high;

            // Copter Settings
            // NUM_copter_delay.Value = griddata.copter_delay;
            // CHK_copter_headinghold.Checked = griddata.copter_headinghold_chk;
            TXT_headinghold = griddata.copter_headinghold.ToString();

            // Plane Settings
            NUM_Lane_Dist = griddata.minlaneseparation;

            // update display options last
            // CHK_internals.Checked = griddata.internals;
            // CHK_footprints.Checked = griddata.footprints;
            // CHK_advanced.Checked = griddata.advanced;

            loadedfromfile = true;
        }

        void AddDrawPolygon()
        {
            List<PointLatLng> list2 = new List<PointLatLng>();

            list.ForEach(x => { list2.Add(x); });

            var poly = new GMapPolygon(list2, "poly");
            poly.Stroke = new Pen(Color.Red, 2);
            poly.Fill = Brushes.Transparent;

            routesOverlay.Polygons.Add(poly);

            foreach (var item in list)
            {
                routesOverlay.Markers.Add(new GMarkerGoogle(item, GMarkerGoogleType.red));
            }
        }
        void getFOV(double flyalt, ref double fovh, ref double fovv)
        {
            double focallen = (double)NUM_focallength.Value;
            double sensorwidth = double.Parse(TXT_senswidth.Text);
            double sensorheight = double.Parse(TXT_sensheight.Text);

            // scale      mm / mm
            double flscale = (1000 * flyalt) / focallen;

            //   mm * mm / 1000
            double viewwidth = (sensorwidth * flscale / 1000);
            double viewheight = (sensorheight * flscale / 1000);

            float fovh1 = (float)(Math.Atan(sensorwidth / (2 * focallen)) * rad2deg * 2);
            float fovv1 = (float)(Math.Atan(sensorheight / (2 * focallen)) * rad2deg * 2);

            fovh = viewwidth;
            fovv = viewheight;
        }

        void getFOVangle(ref double fovh, ref double fovv)
        {
            double focallen = (double)NUM_focallength.Value;
            double sensorwidth = double.Parse(TXT_senswidth.Text);
            double sensorheight = double.Parse(TXT_sensheight.Text);

            fovh = (float)(Math.Atan(sensorwidth / (2 * focallen)) * rad2deg * 2);
            fovv = (float)(Math.Atan(sensorheight / (2 * focallen)) * rad2deg * 2);
        }

        float routetotal = 0;
        private void domainUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (loading)
                return;

            if (CMB_camera.Text != "")
            {
                doCalc();
            }

            // new grid system test

            grid = Grid.CreateGrid(list, CurrentState.fromDistDisplayUnit((double)NUM_altitude.Value),
                (double)NUM_dist.Value, (double)NUM_spacing, (double)NUM_angle.Value,
                (double)NUM_overshoot, (double)NUM_overshoot2,
                (Grid.StartPosition)Enum.Parse(typeof(Grid.StartPosition), CMB_startfrom.Text), false,
                (float)NUM_Lane_Dist, (float)NUM_leadin);

            map.HoldInvalidation = true;

            routesOverlay.Routes.Clear();
            routesOverlay.Polygons.Clear();
            routesOverlay.Markers.Clear();

            GMapMarkerOverlap.Clear();

            if (grid.Count == 0)
            {
                return;
            }

            if (chk_crossgrid.Checked)
            {
                // add crossover
                Grid.StartPointLatLngAlt = grid[grid.Count - 1];

                grid.AddRange(Grid.CreateGrid(list, CurrentState.fromDistDisplayUnit((double)NUM_altitude.Value),
                    (double)NUM_Distance, (double)NUM_spacing, (double)NUM_angle.Value + 90.0,
                    (double)NUM_overshoot, (double)NUM_overshoot2,
                    Grid.StartPosition.Point, false,
                    (float)NUM_Lane_Dist, (float)NUM_leadin));
            }

            //if (CHK_boundary.Checked)
            AddDrawPolygon();

            int strips = 0;
            int images = 0;
            int a = 1;
            PointLatLngAlt prevpoint = grid[0];
            float routetotal = 0;
            List<PointLatLng> segment = new List<PointLatLng>();
            double maxgroundelevation = double.MinValue;
            double mingroundelevation = double.MaxValue;
            double startalt = plugin.Host.cs.HomeAlt;

            foreach (var item in grid)
            {
                double currentalt = srtm.getAltitude(item.Lat, item.Lng).alt;
                mingroundelevation = Math.Min(mingroundelevation, currentalt);
                maxgroundelevation = Math.Max(maxgroundelevation, currentalt);

                if (item.Tag == "M")
                {
                    images++;

                    //if (CHK_internals.Checked)   MP中未直接打勾的就先暂时认为是不用选中
                    //{
                    //    routesOverlay.Markers.Add(new GMarkerGoogle(item, GMarkerGoogleType.green) { ToolTipText = a.ToString(), ToolTipMode = MarkerTooltipMode.OnMouseOver });
                    //    a++;

                    //    segment.Add(prevpoint);
                    //    segment.Add(item);
                    //    prevpoint = item;
                    //}
                    try
                    {
                        if (TXT_fovH != "")
                        {
                            if (CHK_footprints.Checked)
                            {
                                double fovh = double.Parse(TXT_fovH);
                                double fovv = double.Parse(TXT_fovV);

                                getFOV(item.Alt + startalt - currentalt, ref fovh, ref fovv);

                                double startangle = 0;

                                //if (!CHK_camdirection.Checked)   MP中直接打勾的视为必选项
                                //{
                                    startangle = 90;
                                //}

                                double angle1 = startangle - (Math.Sin((fovh / 2.0) / (fovv / 2.0)) * rad2deg);
                                double dist1 = Math.Sqrt(Math.Pow(fovh / 2.0, 2) + Math.Pow(fovv / 2.0, 2));

                                double bearing = (double)NUM_angle.Value;

                                //if (CHK_copter_headinghold.Checked)
                                //{
                                //    bearing = Convert.ToInt32(TXT_headinghold);
                                //}

                                double fovha = 0;
                                double fovva = 0;
                                getFOVangle(ref fovha, ref fovva);
                                var itemcopy = new PointLatLngAlt(item);
                                itemcopy.Alt += startalt;
                                var temp = ImageProjection.calc(itemcopy, 0, 0, bearing + startangle, fovha, fovva);

                                List<PointLatLng> footprint = new List<PointLatLng>();
                                footprint.Add(temp[0]);
                                footprint.Add(temp[1]);
                                footprint.Add(temp[2]);
                                footprint.Add(temp[3]);

                                GMapPolygon poly = new GMapPolygon(footprint, a.ToString());
                                poly.Stroke =
                                    new Pen(Color.FromArgb(250 - ((a * 5) % 240), 250 - ((a * 3) % 240), 250 - ((a * 9) % 240)), 1);
                                poly.Fill = new SolidBrush(Color.Transparent);

                                GMapMarkerOverlap.Add(poly);

                                routesOverlay.Polygons.Add(poly);
                                a++;
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    if (item.Tag != "SM" && item.Tag != "ME")
                        strips++;

                    if (CHK_markers.Checked)
                    {
                        var marker = new GMapMarkerWP(item, a.ToString()) { ToolTipText = a.ToString(), ToolTipMode = MarkerTooltipMode.OnMouseOver };
                        routesOverlay.Markers.Add(marker);
                    }

                    segment.Add(prevpoint);
                    segment.Add(item);
                    prevpoint = item;
                    a++;
                }
                GMapRoute seg = new GMapRoute(segment, "segment" + a.ToString());
                seg.Stroke = new Pen(Color.Yellow, 4);
                seg.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                seg.IsHitTestVisible = true;
                routetotal = routetotal + (float)seg.Distance;
                //if (CHK_grid)
                //{
                routesOverlay.Routes.Add(seg);
                //}
                //else
                //{
                //    seg.Dispose();
                //}

                segment.Clear();
            }

            if (CHK_footprints.Checked)
                routesOverlay.Markers.Add(GMapMarkerOverlap);
            /*      Old way of drawing route, incase something breaks using segments
            GMapRoute wproute = new GMapRoute(list2, "GridRoute");
            wproute.Stroke = new Pen(Color.Yellow, 4);
            if (chk_grid.Checked)
                routesOverlay.Routes.Add(wproute);
            */

            // turn radrad = tas^2 / (tan(angle) * G)
            // float v_sq = (float)(((float)NUM_UpDownFlySpeed.Value / CurrentState.multiplierspeed) * ((float)NUM_UpDownFlySpeed.Value / CurrentState.multiplierspeed));
            // float turnrad = (float)(v_sq / (float)(9.808f * Math.Tan(35 * deg2rad)));

            // Update Stats 
            if (DistUnits == "Feet")
            {
                // Area
                float area = (float)calcpolygonarea(list) * 10.7639f; // Calculate the area in square feet
                lbl_area.Text = area.ToString("#") + " ft^2";
                if (area < 21780f)
                {
                    lbl_area.Text = area.ToString("#") + " ft^2";
                }
                else
                {
                    area = area / 43560f;
                    if (area < 640f)
                    {
                        lbl_area.Text = area.ToString("0.##") + " acres";
                    }
                    else
                    {
                        area = area / 640f;
                        lbl_area.Text = area.ToString("0.##") + " miles^2";
                    }
                }

                // Distance
                float distance = routetotal * 3280.84f; // Calculate the distance in feet
                if (distance < 5280f)
                {
                    lbl_distance.Text = distance.ToString("#") + " ft";
                }
                else
                {
                    distance = distance / 5280f;
                    lbl_distance.Text = distance.ToString("0.##") + " miles";
                }

                lbl_spacing.Text = (NUM_spacing * 3.2808399m).ToString("#") + " ft";
                lbl_grndres.Text = inchpixel;
                lbl_distbetweenlines.Text = (NUM_Distance * 3.2808399m).ToString("0.##") + " ft";
                lbl_footprint.Text = feet_fovH + " x " + feet_fovV + " ft";
                //lbl_turnrad.Text = (turnrad * 2 * 3.2808399).ToString("0") + " ft";
                lbl_gndelev.Text = (mingroundelevation * 3.2808399).ToString("0") + "-" + (maxgroundelevation * 3.2808399).ToString("0") + " ft";
            }
            else
            {
                // Meters
                lbl_area.Text = calcpolygonarea(list).ToString("#") + " m^2";
                lbl_distance.Text = routetotal.ToString("0.##") + " km";
                lbl_spacing.Text = NUM_spacing.ToString("#") + " m";
                lbl_grndres.Text = TXT_cmpixel;
                lbl_distbetweenlines.Text = NUM_Distance.ToString("0.##") + " m";
                lbl_footprint.Text = TXT_fovH + " x " + TXT_fovV + " m";
                //lbl_turnrad.Text = (turnrad * 2).ToString("0") + " m";
                lbl_gndelev.Text = mingroundelevation.ToString("0") + "-" + maxgroundelevation.ToString("0") + " m";

            }

            try
            {
                // speed m/s
                var speed = ((float)NUM_UpDownFlySpeed / CurrentState.multiplierspeed);
                // cmpix cm/pixel
                var cmpix = float.Parse(TXT_cmpixel.TrimEnd(new[] { 'c', 'm', ' ' }));
                // m pix = m/pixel
                var mpix = cmpix * 0.01;
                // gsd / 2.0
                var minmpix = mpix / 2.0;
                // min sutter speed
                var minshutter = speed / minmpix;
                //lbl_minshutter.Text = "1/" + (minshutter - minshutter % 1).ToString();
            }
            catch { }

            double flyspeedms = CurrentState.fromSpeedDisplayUnit((double)NUM_UpDownFlySpeed);

            lbl_pictures.Text = images.ToString();
            lbl_strips.Text = ((int)(strips / 2)).ToString();
            double seconds = ((routetotal * 1000.0) / ((flyspeedms) * 0.8));
            // reduce flying speed by 20 %
            lbl_flighttime.Text = secondsToNice(seconds);
            seconds = ((routetotal * 1000.0) / (flyspeedms));
            lbl_photoevery.Text = secondsToNice(((double)NUM_spacing / flyspeedms));
            map.HoldInvalidation = false;
            if (!isMouseDown && sender != NUM_angle)
                map.ZoomAndCenterMarkers("routes");

            CalcHeadingHold();

            map.Invalidate();
        }
        private void CalcHeadingHold()
        {
            int previous = (int)Math.Round(Convert.ToDecimal(((UpDownBase)NUM_angle).Text)); //((UpDownBase)sender).Text
            int current = (int)Math.Round(NUM_angle.Value);

            int change = current - previous;

            if (change > 0) // Positive change
            {
                int val = Convert.ToInt32(TXT_headinghold) + change;
                if (val > 359)
                {
                    val = val - 360;
                }
                TXT_headinghold = val.ToString();
            }

            if (change < 0) // Negative change
            {
                int val = Convert.ToInt32(TXT_headinghold) + change;
                if (val < 0)
                {
                    val = val + 360;
                }
                TXT_headinghold = val.ToString();
            }
        }

        string secondsToNice(double seconds)
        {
            if (seconds < 0)
                return "Infinity Seconds";

            double secs = seconds % 60;
            int mins = (int)(seconds / 60) % 60;
            int hours = (int)(seconds / 3600) % 24;

            if (hours > 0)
            {
                return hours + ":" + mins.ToString("00") + ":" + secs.ToString("00") + " Hours";
            }
            else if (mins > 0)
            {
                return mins + ":" + secs.ToString("00") + " Minutes";
            }
            else
            {
                return secs.ToString("0.00") + " Seconds";
            }
        }
    } 
}

