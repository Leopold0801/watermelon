namespace Amov.Planner.grid
{
    partial class gridui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gridui));
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_flighttime = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.lbl_distbetweenlines = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lbl_footprint = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lbl_strips = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.lbl_pictures = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lbl_photoevery = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.lbl_spacing = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lbl_distance = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lbl_grndres = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lbl_gndelev = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lbl_area = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CHK_footprints = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.num_overlap = new System.Windows.Forms.NumericUpDown();
            this.num_sidelap = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.NUM_angle = new System.Windows.Forms.NumericUpDown();
            this.NUM_altitude = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.CMB_startfrom = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.CMB_camera = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.myButton1 = new MAVTOOL.Controls.MyButton();
            this.TXT_sensheight = new System.Windows.Forms.TextBox();
            this.TXT_senswidth = new System.Windows.Forms.TextBox();
            this.TXT_imgheight = new System.Windows.Forms.TextBox();
            this.TXT_imgwidth = new System.Windows.Forms.TextBox();
            this.NUM_focallength = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.FlightPlanner_TrackBar = new MAVTOOL.Controls.MyTrackBar();
            this.chk_crossgrid = new System.Windows.Forms.CheckBox();
            this.CHK_markers = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_overlap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sidelap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_angle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_altitude)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_focallength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlightPlanner_TrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(4, 32);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedArea = ((GMap.NET.RectLatLng)(resources.GetObject("map.SelectedArea")));
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(1058, 917);
            this.map.TabIndex = 0;
            this.map.Zoom = 0D;
            this.map.Load += new System.EventHandler(this.map_Load);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(730, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 917);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(332, 917);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(324, 888);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基础配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_flighttime);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.lbl_distbetweenlines);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.lbl_footprint);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.lbl_strips);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.lbl_pictures);
            this.groupBox2.Controls.Add(this.label34);
            this.groupBox2.Controls.Add(this.lbl_photoevery);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.lbl_spacing);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.lbl_distance);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.lbl_grndres);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.lbl_gndelev);
            this.groupBox2.Controls.Add(this.label40);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.lbl_area);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 521);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(318, 364);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "航测参数";
            // 
            // lbl_flighttime
            // 
            this.lbl_flighttime.AutoSize = true;
            this.lbl_flighttime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_flighttime.Location = new System.Drawing.Point(117, 292);
            this.lbl_flighttime.Name = "lbl_flighttime";
            this.lbl_flighttime.Size = new System.Drawing.Size(39, 15);
            this.lbl_flighttime.TabIndex = 42;
            this.lbl_flighttime.Text = "0.00";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label31.Location = new System.Drawing.Point(10, 292);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(105, 15);
            this.label31.TabIndex = 41;
            this.label31.Text = "预计飞行时间:";
            // 
            // lbl_distbetweenlines
            // 
            this.lbl_distbetweenlines.AutoSize = true;
            this.lbl_distbetweenlines.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_distbetweenlines.Location = new System.Drawing.Point(117, 262);
            this.lbl_distbetweenlines.Name = "lbl_distbetweenlines";
            this.lbl_distbetweenlines.Size = new System.Drawing.Size(39, 15);
            this.lbl_distbetweenlines.TabIndex = 40;
            this.lbl_distbetweenlines.Text = "0.00";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label25.Location = new System.Drawing.Point(10, 262);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(75, 15);
            this.label25.TabIndex = 39;
            this.label25.Text = "航带间距:";
            // 
            // lbl_footprint
            // 
            this.lbl_footprint.AutoSize = true;
            this.lbl_footprint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_footprint.Location = new System.Drawing.Point(117, 232);
            this.lbl_footprint.Name = "lbl_footprint";
            this.lbl_footprint.Size = new System.Drawing.Size(39, 15);
            this.lbl_footprint.TabIndex = 38;
            this.lbl_footprint.Text = "0.00";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label30.Location = new System.Drawing.Point(9, 232);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(75, 15);
            this.label30.TabIndex = 37;
            this.label30.Text = "相幅面积:";
            // 
            // lbl_strips
            // 
            this.lbl_strips.AutoSize = true;
            this.lbl_strips.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_strips.Location = new System.Drawing.Point(117, 202);
            this.lbl_strips.Name = "lbl_strips";
            this.lbl_strips.Size = new System.Drawing.Size(39, 15);
            this.lbl_strips.TabIndex = 36;
            this.lbl_strips.Text = "0.00";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label33.Location = new System.Drawing.Point(9, 202);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(75, 15);
            this.label33.TabIndex = 35;
            this.label33.Text = "航带数量:";
            // 
            // lbl_pictures
            // 
            this.lbl_pictures.AutoSize = true;
            this.lbl_pictures.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_pictures.Location = new System.Drawing.Point(117, 172);
            this.lbl_pictures.Name = "lbl_pictures";
            this.lbl_pictures.Size = new System.Drawing.Size(39, 15);
            this.lbl_pictures.TabIndex = 34;
            this.lbl_pictures.Text = "0.00";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label34.Location = new System.Drawing.Point(9, 172);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(75, 15);
            this.label34.TabIndex = 33;
            this.label34.Text = "照片数量:";
            // 
            // lbl_photoevery
            // 
            this.lbl_photoevery.AutoSize = true;
            this.lbl_photoevery.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_photoevery.Location = new System.Drawing.Point(117, 142);
            this.lbl_photoevery.Name = "lbl_photoevery";
            this.lbl_photoevery.Size = new System.Drawing.Size(39, 15);
            this.lbl_photoevery.TabIndex = 32;
            this.lbl_photoevery.Text = "0.00";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label35.Location = new System.Drawing.Point(10, 142);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(105, 15);
            this.label35.TabIndex = 31;
            this.label35.Text = "拍摄间隔时间:";
            // 
            // lbl_spacing
            // 
            this.lbl_spacing.AutoSize = true;
            this.lbl_spacing.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_spacing.Location = new System.Drawing.Point(117, 112);
            this.lbl_spacing.Name = "lbl_spacing";
            this.lbl_spacing.Size = new System.Drawing.Size(39, 15);
            this.lbl_spacing.TabIndex = 30;
            this.lbl_spacing.Text = "0.00";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label27.Location = new System.Drawing.Point(10, 112);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(113, 15);
            this.label27.TabIndex = 29;
            this.label27.Text = "拍摄间隔距离: ";
            // 
            // lbl_distance
            // 
            this.lbl_distance.AutoSize = true;
            this.lbl_distance.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_distance.Location = new System.Drawing.Point(117, 83);
            this.lbl_distance.Name = "lbl_distance";
            this.lbl_distance.Size = new System.Drawing.Size(39, 15);
            this.lbl_distance.TabIndex = 28;
            this.lbl_distance.Text = "0.00";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(10, 82);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(83, 15);
            this.label23.TabIndex = 27;
            this.label23.Text = "作业距离: ";
            // 
            // lbl_grndres
            // 
            this.lbl_grndres.AutoSize = true;
            this.lbl_grndres.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_grndres.Location = new System.Drawing.Point(117, 52);
            this.lbl_grndres.Name = "lbl_grndres";
            this.lbl_grndres.Size = new System.Drawing.Size(39, 15);
            this.lbl_grndres.TabIndex = 26;
            this.lbl_grndres.Text = "0.00";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label29.Location = new System.Drawing.Point(10, 52);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(105, 15);
            this.label29.TabIndex = 25;
            this.label29.Text = "地面分辨率： ";
            // 
            // lbl_gndelev
            // 
            this.lbl_gndelev.AutoSize = true;
            this.lbl_gndelev.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_gndelev.Location = new System.Drawing.Point(117, 322);
            this.lbl_gndelev.Name = "lbl_gndelev";
            this.lbl_gndelev.Size = new System.Drawing.Size(39, 15);
            this.lbl_gndelev.TabIndex = 24;
            this.lbl_gndelev.Text = "0.00";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label40.Location = new System.Drawing.Point(10, 322);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(75, 15);
            this.label40.TabIndex = 23;
            this.label40.Text = "地面高程:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label22.Location = new System.Drawing.Point(9, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(83, 15);
            this.label22.TabIndex = 4;
            this.label22.Text = "作业面积: ";
            // 
            // lbl_area
            // 
            this.lbl_area.AutoSize = true;
            this.lbl_area.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_area.Location = new System.Drawing.Point(117, 21);
            this.lbl_area.Name = "lbl_area";
            this.lbl_area.Size = new System.Drawing.Size(39, 15);
            this.lbl_area.TabIndex = 3;
            this.lbl_area.Text = "0.00";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CHK_markers);
            this.groupBox1.Controls.Add(this.chk_crossgrid);
            this.groupBox1.Controls.Add(this.CHK_footprints);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.num_overlap);
            this.groupBox1.Controls.Add(this.num_sidelap);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.NUM_angle);
            this.groupBox1.Controls.Add(this.NUM_altitude);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CMB_startfrom);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.CMB_camera);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 512);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "航测配置";
            // 
            // CHK_footprints
            // 
            this.CHK_footprints.AutoSize = true;
            this.CHK_footprints.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CHK_footprints.Location = new System.Drawing.Point(119, 266);
            this.CHK_footprints.Name = "CHK_footprints";
            this.CHK_footprints.Size = new System.Drawing.Size(89, 19);
            this.CHK_footprints.TabIndex = 64;
            this.CHK_footprints.Text = "拍摄范围";
            this.CHK_footprints.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(21, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 15);
            this.label8.TabIndex = 60;
            this.label8.Text = "航向重叠率 [%]";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(21, 178);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 15);
            this.label15.TabIndex = 61;
            this.label15.Text = "旁向重叠率 [%]";
            // 
            // num_overlap
            // 
            this.num_overlap.DecimalPlaces = 1;
            this.num_overlap.Location = new System.Drawing.Point(157, 150);
            this.num_overlap.Name = "num_overlap";
            this.num_overlap.Size = new System.Drawing.Size(51, 25);
            this.num_overlap.TabIndex = 62;
            this.num_overlap.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // num_sidelap
            // 
            this.num_sidelap.DecimalPlaces = 1;
            this.num_sidelap.Location = new System.Drawing.Point(157, 176);
            this.num_sidelap.Name = "num_sidelap";
            this.num_sidelap.Size = new System.Drawing.Size(51, 25);
            this.num_sidelap.TabIndex = 63;
            this.num_sidelap.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(21, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 54;
            this.label4.Text = "角度[deg]";
            // 
            // NUM_angle
            // 
            this.NUM_angle.Location = new System.Drawing.Point(157, 115);
            this.NUM_angle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.NUM_angle.Name = "NUM_angle";
            this.NUM_angle.Size = new System.Drawing.Size(51, 25);
            this.NUM_angle.TabIndex = 53;
            // 
            // NUM_altitude
            // 
            this.NUM_altitude.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NUM_altitude.Location = new System.Drawing.Point(157, 84);
            this.NUM_altitude.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUM_altitude.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUM_altitude.Name = "NUM_altitude";
            this.NUM_altitude.Size = new System.Drawing.Size(51, 25);
            this.NUM_altitude.TabIndex = 51;
            this.NUM_altitude.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(21, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 50;
            this.label1.Text = "飞行高度[m]";
            // 
            // CMB_startfrom
            // 
            this.CMB_startfrom.FormattingEnabled = true;
            this.CMB_startfrom.Location = new System.Drawing.Point(157, 52);
            this.CMB_startfrom.Name = "CMB_startfrom";
            this.CMB_startfrom.Size = new System.Drawing.Size(92, 23);
            this.CMB_startfrom.TabIndex = 49;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(21, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 48;
            this.label6.Text = "开始位置";
            // 
            // CMB_camera
            // 
            this.CMB_camera.FormattingEnabled = true;
            this.CMB_camera.Location = new System.Drawing.Point(157, 18);
            this.CMB_camera.Name = "CMB_camera";
            this.CMB_camera.Size = new System.Drawing.Size(117, 23);
            this.CMB_camera.TabIndex = 47;
            this.CMB_camera.SelectedIndexChanged += new System.EventHandler(this.CMB_camera_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(21, 21);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 15);
            this.label26.TabIndex = 46;
            this.label26.Text = "相机选择";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(324, 888);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "相机配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.myButton1);
            this.groupBox3.Controls.Add(this.TXT_sensheight);
            this.groupBox3.Controls.Add(this.TXT_senswidth);
            this.groupBox3.Controls.Add(this.TXT_imgheight);
            this.groupBox3.Controls.Add(this.TXT_imgwidth);
            this.groupBox3.Controls.Add(this.NUM_focallength);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 430);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "相机参数";
            // 
            // myButton1
            // 
            this.myButton1.BGGradTop = System.Drawing.Color.DodgerBlue;
            this.myButton1.Location = new System.Drawing.Point(98, 196);
            this.myButton1.Name = "myButton1";
            this.myButton1.Outline = System.Drawing.Color.DodgerBlue;
            this.myButton1.Size = new System.Drawing.Size(75, 23);
            this.myButton1.TabIndex = 78;
            this.myButton1.Text = "保存";
            this.myButton1.UseVisualStyleBackColor = true;
            // 
            // TXT_sensheight
            // 
            this.TXT_sensheight.Location = new System.Drawing.Point(164, 142);
            this.TXT_sensheight.Name = "TXT_sensheight";
            this.TXT_sensheight.Size = new System.Drawing.Size(51, 25);
            this.TXT_sensheight.TabIndex = 77;
            this.TXT_sensheight.Text = "4.62";
            // 
            // TXT_senswidth
            // 
            this.TXT_senswidth.Location = new System.Drawing.Point(164, 116);
            this.TXT_senswidth.Name = "TXT_senswidth";
            this.TXT_senswidth.Size = new System.Drawing.Size(51, 25);
            this.TXT_senswidth.TabIndex = 76;
            this.TXT_senswidth.Text = "6.16";
            // 
            // TXT_imgheight
            // 
            this.TXT_imgheight.Location = new System.Drawing.Point(164, 90);
            this.TXT_imgheight.Name = "TXT_imgheight";
            this.TXT_imgheight.Size = new System.Drawing.Size(51, 25);
            this.TXT_imgheight.TabIndex = 75;
            this.TXT_imgheight.Text = "3456";
            // 
            // TXT_imgwidth
            // 
            this.TXT_imgwidth.Location = new System.Drawing.Point(164, 64);
            this.TXT_imgwidth.Name = "TXT_imgwidth";
            this.TXT_imgwidth.Size = new System.Drawing.Size(51, 25);
            this.TXT_imgwidth.TabIndex = 74;
            this.TXT_imgwidth.Text = "4608";
            // 
            // NUM_focallength
            // 
            this.NUM_focallength.DecimalPlaces = 1;
            this.NUM_focallength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NUM_focallength.Location = new System.Drawing.Point(164, 38);
            this.NUM_focallength.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.NUM_focallength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUM_focallength.Name = "NUM_focallength";
            this.NUM_focallength.Size = new System.Drawing.Size(51, 25);
            this.NUM_focallength.TabIndex = 73;
            this.NUM_focallength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(28, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 15);
            this.label9.TabIndex = 72;
            this.label9.Text = "传感器高 [mm]";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(28, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 15);
            this.label10.TabIndex = 71;
            this.label10.Text = "传感器宽 [mm]";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(28, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 15);
            this.label14.TabIndex = 70;
            this.label14.Text = "像素高 [Pixels]";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(28, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 15);
            this.label13.TabIndex = 69;
            this.label13.Text = "像素宽 [Pixels]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(28, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 15);
            this.label11.TabIndex = 68;
            this.label11.Text = "焦距 [mm]";
            // 
            // FlightPlanner_TrackBar
            // 
            this.FlightPlanner_TrackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.FlightPlanner_TrackBar.LargeChange = 0.005F;
            this.FlightPlanner_TrackBar.Location = new System.Drawing.Point(674, 32);
            this.FlightPlanner_TrackBar.Maximum = 0.01F;
            this.FlightPlanner_TrackBar.Minimum = 0F;
            this.FlightPlanner_TrackBar.Name = "FlightPlanner_TrackBar";
            this.FlightPlanner_TrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.FlightPlanner_TrackBar.Size = new System.Drawing.Size(56, 917);
            this.FlightPlanner_TrackBar.SmallChange = 0.001F;
            this.FlightPlanner_TrackBar.TabIndex = 6;
            this.FlightPlanner_TrackBar.TickFrequency = 0.001F;
            this.FlightPlanner_TrackBar.Value = 0F;
            // 
            // chk_crossgrid
            // 
            this.chk_crossgrid.AutoSize = true;
            this.chk_crossgrid.Location = new System.Drawing.Point(119, 231);
            this.chk_crossgrid.Name = "chk_crossgrid";
            this.chk_crossgrid.Size = new System.Drawing.Size(89, 19);
            this.chk_crossgrid.TabIndex = 65;
            this.chk_crossgrid.Text = "十字航线";
            this.chk_crossgrid.UseVisualStyleBackColor = true;
            // 
            // CHK_markers
            // 
            this.CHK_markers.AutoSize = true;
            this.CHK_markers.Location = new System.Drawing.Point(120, 303);
            this.CHK_markers.Name = "CHK_markers";
            this.CHK_markers.Size = new System.Drawing.Size(59, 19);
            this.CHK_markers.TabIndex = 66;
            this.CHK_markers.Text = "航点";
            this.CHK_markers.UseVisualStyleBackColor = true;
            // 
            // gridui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 953);
            this.Controls.Add(this.FlightPlanner_TrackBar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.map);
            this.Name = "gridui";
            this.Text = "gridui";
            this.Load += new System.EventHandler(this.gridui_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_overlap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_sidelap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_angle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_altitude)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_focallength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FlightPlanner_TrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private MAVTOOL.Controls.MyTrackBar FlightPlanner_TrackBar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown num_overlap;
        private System.Windows.Forms.NumericUpDown num_sidelap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown NUM_angle;
        private System.Windows.Forms.NumericUpDown NUM_altitude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CMB_startfrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CMB_camera;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.GroupBox groupBox3;
        private MAVTOOL.Controls.MyButton myButton1;
        private System.Windows.Forms.TextBox TXT_sensheight;
        private System.Windows.Forms.TextBox TXT_senswidth;
        private System.Windows.Forms.TextBox TXT_imgheight;
        private System.Windows.Forms.TextBox TXT_imgwidth;
        private System.Windows.Forms.NumericUpDown NUM_focallength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_flighttime;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lbl_distbetweenlines;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lbl_footprint;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lbl_strips;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label lbl_pictures;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lbl_photoevery;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label lbl_spacing;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lbl_distance;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lbl_grndres;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lbl_gndelev;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lbl_area;
        private System.Windows.Forms.CheckBox CHK_footprints;
        private System.Windows.Forms.CheckBox chk_crossgrid;
        private System.Windows.Forms.CheckBox CHK_markers;
    }
}