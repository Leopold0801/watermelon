namespace Amov.Planner.views
{
    partial class flightplanner
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (drawnpolygon != null)
                drawnpolygon.Dispose();
            if (currentMarker != null)
                currentMarker.Dispose();
            if (drawnpolygonsoverlay != null)
                drawnpolygonsoverlay.Dispose();
            if (center != null)
                center.Dispose();
            if (wppolygon != null)
                wppolygon.Dispose();

            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flightplanner));
            this.gMapControl = new GMap.NET.WindowsForms.GMapControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TXT_DefaultAlt = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TXT_WPRad = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Commands = new System.Windows.Forms.DataGridView();
            this.Command = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.coordNorthing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coordEasting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coordZone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MGRS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Param1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Param2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Param3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Param4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Angle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Grad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TagData = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Up = new System.Windows.Forms.DataGridViewImageColumn();
            this.Down = new System.Windows.Forms.DataGridViewImageColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.but_Clearroutes = new System.Windows.Forms.Button();
            this.but_Measuringconfig = new System.Windows.Forms.Button();
            this.but_Cleararea = new System.Windows.Forms.Button();
            this.but_Drawarea = new System.Windows.Forms.Button();
            this.but_Quickdraw = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.but_Loadfile = new System.Windows.Forms.Button();
            this.but_Savefile = new System.Windows.Forms.Button();
            this.but_Writewp = new System.Windows.Forms.Button();
            this.but_Updatelocation = new System.Windows.Forms.Button();
            this.but_Homelocation = new System.Windows.Forms.Button();
            this.but_wpread = new System.Windows.Forms.Button();
            this.lab_N = new System.Windows.Forms.Label();
            this.lab_E = new System.Windows.Forms.Label();
            this.FlightPlanner_TrackBar = new MAVTOOL.Controls.MyTrackBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertwp = new System.Windows.Forms.ToolStripMenuItem();
            this.deletewp = new System.Windows.Forms.ToolStripMenuItem();
            this.loiter = new System.Windows.Forms.ToolStripMenuItem();
            this.loiter_forever = new System.Windows.Forms.ToolStripMenuItem();
            this.loiter_time = new System.Windows.Forms.ToolStripMenuItem();
            this.loiter_circle = new System.Windows.Forms.ToolStripMenuItem();
            this.deletelist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.drawpolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.addpolygonpoint = new System.Windows.Forms.ToolStripMenuItem();
            this.clearpolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.savepolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.loadpolygon = new System.Windows.Forms.ToolStripMenuItem();
            this.areatoolstripmenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadkmlfile = new System.Windows.Forms.ToolStripMenuItem();
            this.writekml = new System.Windows.Forms.ToolStripMenuItem();
            this.clearkml = new System.Windows.Forms.ToolStripMenuItem();
            this.land = new System.Windows.Forms.ToolStripMenuItem();
            this.saveandload = new System.Windows.Forms.ToolStripMenuItem();
            this.savewaypoints = new System.Windows.Forms.ToolStripMenuItem();
            this.loadwaypoints = new System.Windows.Forms.ToolStripMenuItem();
            this.poi = new System.Windows.Forms.ToolStripMenuItem();
            this.poiadd = new System.Windows.Forms.ToolStripMenuItem();
            this.poidelete = new System.Windows.Forms.ToolStripMenuItem();
            this.sethome = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_prevdist = new System.Windows.Forms.Label();
            this.lbl_homedist = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Commands)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlightPlanner_TrackBar)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gMapControl
            // 
            this.gMapControl.Bearing = 0F;
            this.gMapControl.CanDragMap = true;
            this.gMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl.GrayScaleMode = false;
            this.gMapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl.LevelsKeepInMemmory = 5;
            this.gMapControl.Location = new System.Drawing.Point(0, 0);
            this.gMapControl.Margin = new System.Windows.Forms.Padding(2);
            this.gMapControl.MarkersEnabled = true;
            this.gMapControl.MaxZoom = 2;
            this.gMapControl.MinZoom = 2;
            this.gMapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl.Name = "gMapControl";
            this.gMapControl.NegativeMode = false;
            this.gMapControl.PolygonsEnabled = true;
            this.gMapControl.RetryLoadTile = 0;
            this.gMapControl.RoutesEnabled = true;
            this.gMapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl.SelectedArea = ((GMap.NET.RectLatLng)(resources.GetObject("gMapControl.SelectedArea")));
            this.gMapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl.ShowTileGridLines = false;
            this.gMapControl.Size = new System.Drawing.Size(640, 421);
            this.gMapControl.TabIndex = 0;
            this.gMapControl.Zoom = 0D;
            this.gMapControl.Load += new System.EventHandler(this.gMapControl_Load);
            this.gMapControl.Paint += new System.Windows.Forms.PaintEventHandler(this.gMapControl_Paint);
            this.gMapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gmap_SetwayPoint_MouseDown);
            this.gMapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gMapControl_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TXT_DefaultAlt);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.TXT_WPRad);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.Commands);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(150, 221);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 200);
            this.panel1.TabIndex = 1;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(224, 24);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 75;
            this.checkBox2.Text = "网络";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(224, 4);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 74;
            this.checkBox1.Text = "测距";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(607, 38);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 73;
            this.label10.Text = "cm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(591, 38);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 72;
            this.label9.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(506, 38);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 71;
            this.label8.Text = "地面分辨率：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(371, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 70;
            this.label7.Text = "0.000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 69;
            this.label6.Text = "任务里程（km）：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 68;
            this.label5.Text = "分钟";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(164, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 67;
            this.label4.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 66;
            this.label3.Text = "预计航时：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 65;
            this.label2.Text = "任务概要：";
            // 
            // TXT_DefaultAlt
            // 
            this.TXT_DefaultAlt.Location = new System.Drawing.Point(182, 6);
            this.TXT_DefaultAlt.Margin = new System.Windows.Forms.Padding(2);
            this.TXT_DefaultAlt.Name = "TXT_DefaultAlt";
            this.TXT_DefaultAlt.Size = new System.Drawing.Size(31, 21);
            this.TXT_DefaultAlt.TabIndex = 62;
            this.TXT_DefaultAlt.Text = "100";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(116, 9);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 64;
            this.label15.Text = "默认高度：";
            // 
            // TXT_WPRad
            // 
            this.TXT_WPRad.Location = new System.Drawing.Point(64, 4);
            this.TXT_WPRad.Margin = new System.Windows.Forms.Padding(2);
            this.TXT_WPRad.Name = "TXT_WPRad";
            this.TXT_WPRad.Size = new System.Drawing.Size(28, 21);
            this.TXT_WPRad.TabIndex = 61;
            this.TXT_WPRad.Text = "30";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 9);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 63;
            this.label14.Text = "航点半径：";
            // 
            // Commands
            // 
            this.Commands.AllowUserToAddRows = false;
            this.Commands.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Commands.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Command,
            this.coordNorthing,
            this.coordEasting,
            this.coordZone,
            this.MGRS,
            this.Param1,
            this.Param2,
            this.Param3,
            this.Param4,
            this.Lat,
            this.Lon,
            this.Alt,
            this.Angle,
            this.Dist,
            this.Grad,
            this.AZ,
            this.TagData,
            this.Up,
            this.Down,
            this.Delete});
            this.Commands.Location = new System.Drawing.Point(0, 52);
            this.Commands.Margin = new System.Windows.Forms.Padding(2);
            this.Commands.Name = "Commands";
            this.Commands.RowTemplate.Height = 27;
            this.Commands.Size = new System.Drawing.Size(640, 143);
            this.Commands.TabIndex = 8;
            this.Commands.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_CellContentClick);
            this.Commands.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_CellEndEdit);
            this.Commands.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Commands_DataError);
            this.Commands.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.Commands_DefaultValuesNeeded);
            this.Commands.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Commands_EditingControlShowing);
            this.Commands.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Commands_RowEnter_1);
            this.Commands.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.Commands_RowsAdded);
            this.Commands.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Commands_RowValidating);
            // 
            // Command
            // 
            this.Command.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.Command.HeaderText = "命令";
            this.Command.MinimumWidth = 100;
            this.Command.Name = "Command";
            this.Command.ToolTipText = "APM Command";
            // 
            // coordNorthing
            // 
            this.coordNorthing.HeaderText = "北距";
            this.coordNorthing.Name = "coordNorthing";
            this.coordNorthing.Visible = false;
            // 
            // coordEasting
            // 
            this.coordEasting.HeaderText = "东距";
            this.coordEasting.Name = "coordEasting";
            this.coordEasting.Visible = false;
            // 
            // coordZone
            // 
            this.coordZone.HeaderText = "Zone";
            this.coordZone.Name = "coordZone";
            this.coordZone.Visible = false;
            // 
            // MGRS
            // 
            this.MGRS.HeaderText = "MGRS";
            this.MGRS.Name = "MGRS";
            this.MGRS.Visible = false;
            // 
            // Param1
            // 
            this.Param1.HeaderText = "";
            this.Param1.Name = "Param1";
            this.Param1.Width = 10;
            // 
            // Param2
            // 
            this.Param2.HeaderText = "加速度角";
            this.Param2.MinimumWidth = 60;
            this.Param2.Name = "Param2";
            this.Param2.Width = 60;
            // 
            // Param3
            // 
            this.Param3.HeaderText = "通过距离";
            this.Param3.MinimumWidth = 60;
            this.Param3.Name = "Param3";
            this.Param3.Width = 60;
            // 
            // Param4
            // 
            this.Param4.HeaderText = "";
            this.Param4.Name = "Param4";
            this.Param4.Width = 10;
            // 
            // Lat
            // 
            this.Lat.HeaderText = "纬度";
            this.Lat.MinimumWidth = 100;
            this.Lat.Name = "Lat";
            // 
            // Lon
            // 
            this.Lon.HeaderText = "经度";
            this.Lon.MinimumWidth = 100;
            this.Lon.Name = "Lon";
            // 
            // Alt
            // 
            this.Alt.HeaderText = "高度";
            this.Alt.MinimumWidth = 100;
            this.Alt.Name = "Alt";
            // 
            // Angle
            // 
            this.Angle.HeaderText = "角度";
            this.Angle.MinimumWidth = 50;
            this.Angle.Name = "Angle";
            this.Angle.Width = 50;
            // 
            // Dist
            // 
            this.Dist.HeaderText = "距离";
            this.Dist.MinimumWidth = 60;
            this.Dist.Name = "Dist";
            this.Dist.Width = 60;
            // 
            // Grad
            // 
            this.Grad.HeaderText = "方位角";
            this.Grad.MinimumWidth = 50;
            this.Grad.Name = "Grad";
            this.Grad.Width = 50;
            // 
            // AZ
            // 
            this.AZ.HeaderText = "AZ";
            this.AZ.Name = "AZ";
            this.AZ.Visible = false;
            // 
            // TagData
            // 
            this.TagData.HeaderText = "Tagdata";
            this.TagData.Name = "TagData";
            this.TagData.Visible = false;
            // 
            // Up
            // 
            this.Up.HeaderText = "Up";
            this.Up.MinimumWidth = 60;
            this.Up.Name = "Up";
            this.Up.Width = 60;
            // 
            // Down
            // 
            this.Down.HeaderText = "Down";
            this.Down.MinimumWidth = 60;
            this.Down.Name = "Down";
            this.Down.Width = 60;
            // 
            // Delete
            // 
            this.Delete.HeaderText = "删除";
            this.Delete.MinimumWidth = 50;
            this.Delete.Name = "Delete";
            this.Delete.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(288, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择地图：";
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "GoogleChinaMapProvider",
            "GoogleChinaHybridMapProvider",
            "GoogleChinaSatelliteMapProvider",
            "AMapProvider"});
            this.comboBox1.Location = new System.Drawing.Point(353, 0);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(150, 421);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.but_Clearroutes);
            this.tabPage1.Controls.Add(this.but_Measuringconfig);
            this.tabPage1.Controls.Add(this.but_Cleararea);
            this.tabPage1.Controls.Add(this.but_Drawarea);
            this.tabPage1.Controls.Add(this.but_Quickdraw);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(142, 395);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "测绘";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // but_Clearroutes
            // 
            this.but_Clearroutes.Location = new System.Drawing.Point(32, 201);
            this.but_Clearroutes.Margin = new System.Windows.Forms.Padding(2);
            this.but_Clearroutes.Name = "but_Clearroutes";
            this.but_Clearroutes.Size = new System.Drawing.Size(70, 25);
            this.but_Clearroutes.TabIndex = 4;
            this.but_Clearroutes.Text = "清理航线";
            this.but_Clearroutes.UseVisualStyleBackColor = true;
            this.but_Clearroutes.Click += new System.EventHandler(this.but_Clearroutes_Click);
            // 
            // but_Measuringconfig
            // 
            this.but_Measuringconfig.Location = new System.Drawing.Point(32, 154);
            this.but_Measuringconfig.Margin = new System.Windows.Forms.Padding(2);
            this.but_Measuringconfig.Name = "but_Measuringconfig";
            this.but_Measuringconfig.Size = new System.Drawing.Size(70, 25);
            this.but_Measuringconfig.TabIndex = 3;
            this.but_Measuringconfig.Text = "航测配置";
            this.but_Measuringconfig.UseVisualStyleBackColor = true;
            this.but_Measuringconfig.Click += new System.EventHandler(this.but_Measuringconfig_Click);
            // 
            // but_Cleararea
            // 
            this.but_Cleararea.Location = new System.Drawing.Point(32, 107);
            this.but_Cleararea.Margin = new System.Windows.Forms.Padding(2);
            this.but_Cleararea.Name = "but_Cleararea";
            this.but_Cleararea.Size = new System.Drawing.Size(70, 25);
            this.but_Cleararea.TabIndex = 2;
            this.but_Cleararea.Text = "清理测区";
            this.but_Cleararea.UseVisualStyleBackColor = true;
            this.but_Cleararea.Click += new System.EventHandler(this.but_Cleararea_Click);
            // 
            // but_Drawarea
            // 
            this.but_Drawarea.Location = new System.Drawing.Point(32, 62);
            this.but_Drawarea.Margin = new System.Windows.Forms.Padding(2);
            this.but_Drawarea.Name = "but_Drawarea";
            this.but_Drawarea.Size = new System.Drawing.Size(70, 25);
            this.but_Drawarea.TabIndex = 1;
            this.but_Drawarea.Text = "绘制测区";
            this.but_Drawarea.UseVisualStyleBackColor = true;
            this.but_Drawarea.Click += new System.EventHandler(this.but_Drawarea_Click);
            // 
            // but_Quickdraw
            // 
            this.but_Quickdraw.Location = new System.Drawing.Point(32, 19);
            this.but_Quickdraw.Margin = new System.Windows.Forms.Padding(2);
            this.but_Quickdraw.Name = "but_Quickdraw";
            this.but_Quickdraw.Size = new System.Drawing.Size(70, 25);
            this.but_Quickdraw.TabIndex = 0;
            this.but_Quickdraw.Text = "快速绘制";
            this.but_Quickdraw.UseVisualStyleBackColor = true;
            this.but_Quickdraw.Click += new System.EventHandler(this.but_Quickdraw_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.but_Loadfile);
            this.tabPage2.Controls.Add(this.but_Savefile);
            this.tabPage2.Controls.Add(this.but_Writewp);
            this.tabPage2.Controls.Add(this.but_Updatelocation);
            this.tabPage2.Controls.Add(this.but_Homelocation);
            this.tabPage2.Controls.Add(this.but_wpread);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(142, 395);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "航线";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // but_Loadfile
            // 
            this.but_Loadfile.Location = new System.Drawing.Point(32, 223);
            this.but_Loadfile.Margin = new System.Windows.Forms.Padding(2);
            this.but_Loadfile.Name = "but_Loadfile";
            this.but_Loadfile.Size = new System.Drawing.Size(68, 25);
            this.but_Loadfile.TabIndex = 7;
            this.but_Loadfile.Text = "加载文件";
            this.but_Loadfile.UseVisualStyleBackColor = true;
            // 
            // but_Savefile
            // 
            this.but_Savefile.Location = new System.Drawing.Point(32, 183);
            this.but_Savefile.Margin = new System.Windows.Forms.Padding(2);
            this.but_Savefile.Name = "but_Savefile";
            this.but_Savefile.Size = new System.Drawing.Size(68, 25);
            this.but_Savefile.TabIndex = 6;
            this.but_Savefile.Text = "保存文件";
            this.but_Savefile.UseVisualStyleBackColor = true;
            // 
            // but_Writewp
            // 
            this.but_Writewp.Location = new System.Drawing.Point(32, 140);
            this.but_Writewp.Margin = new System.Windows.Forms.Padding(2);
            this.but_Writewp.Name = "but_Writewp";
            this.but_Writewp.Size = new System.Drawing.Size(68, 25);
            this.but_Writewp.TabIndex = 5;
            this.but_Writewp.Text = "写入航线";
            this.but_Writewp.UseVisualStyleBackColor = true;
            this.but_Writewp.Click += new System.EventHandler(this.but_Writewp_Click);
            // 
            // but_Updatelocation
            // 
            this.but_Updatelocation.Location = new System.Drawing.Point(32, 58);
            this.but_Updatelocation.Margin = new System.Windows.Forms.Padding(2);
            this.but_Updatelocation.Name = "but_Updatelocation";
            this.but_Updatelocation.Size = new System.Drawing.Size(68, 25);
            this.but_Updatelocation.TabIndex = 4;
            this.but_Updatelocation.Text = "更新位置";
            this.but_Updatelocation.UseVisualStyleBackColor = true;
            // 
            // but_Homelocation
            // 
            this.but_Homelocation.Location = new System.Drawing.Point(32, 17);
            this.but_Homelocation.Margin = new System.Windows.Forms.Padding(2);
            this.but_Homelocation.Name = "but_Homelocation";
            this.but_Homelocation.Size = new System.Drawing.Size(68, 25);
            this.but_Homelocation.TabIndex = 3;
            this.but_Homelocation.Text = "家的位置";
            this.but_Homelocation.UseVisualStyleBackColor = true;
            this.but_Homelocation.Click += new System.EventHandler(this.but_Homelocation_Click);
            // 
            // but_wpread
            // 
            this.but_wpread.Location = new System.Drawing.Point(32, 98);
            this.but_wpread.Margin = new System.Windows.Forms.Padding(2);
            this.but_wpread.Name = "but_wpread";
            this.but_wpread.Size = new System.Drawing.Size(68, 25);
            this.but_wpread.TabIndex = 2;
            this.but_wpread.Text = "读取航点";
            this.but_wpread.UseVisualStyleBackColor = true;
            this.but_wpread.Click += new System.EventHandler(this.but_wpread_Click);
            // 
            // lab_N
            // 
            this.lab_N.AutoSize = true;
            this.lab_N.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_N.Location = new System.Drawing.Point(154, 0);
            this.lab_N.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lab_N.Name = "lab_N";
            this.lab_N.Size = new System.Drawing.Size(53, 20);
            this.lab_N.TabIndex = 3;
            this.lab_N.Text = "label1";
            // 
            // lab_E
            // 
            this.lab_E.AutoSize = true;
            this.lab_E.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_E.Location = new System.Drawing.Point(269, 0);
            this.lab_E.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lab_E.Name = "lab_E";
            this.lab_E.Size = new System.Drawing.Size(53, 20);
            this.lab_E.TabIndex = 4;
            this.lab_E.Text = "label1";
            // 
            // FlightPlanner_TrackBar
            // 
            this.FlightPlanner_TrackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.FlightPlanner_TrackBar.LargeChange = 0.005F;
            this.FlightPlanner_TrackBar.Location = new System.Drawing.Point(595, 0);
            this.FlightPlanner_TrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.FlightPlanner_TrackBar.Maximum = 0.01F;
            this.FlightPlanner_TrackBar.Minimum = 0F;
            this.FlightPlanner_TrackBar.Name = "FlightPlanner_TrackBar";
            this.FlightPlanner_TrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.FlightPlanner_TrackBar.Size = new System.Drawing.Size(45, 421);
            this.FlightPlanner_TrackBar.SmallChange = 0.001F;
            this.FlightPlanner_TrackBar.TabIndex = 5;
            this.FlightPlanner_TrackBar.TickFrequency = 0.001F;
            this.FlightPlanner_TrackBar.Value = 0F;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertwp,
            this.deletewp,
            this.loiter,
            this.deletelist,
            this.toolStripSeparator1,
            this.drawpolygon,
            this.land,
            this.saveandload,
            this.poi,
            this.sethome});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 208);
            // 
            // insertwp
            // 
            this.insertwp.Name = "insertwp";
            this.insertwp.Size = new System.Drawing.Size(172, 22);
            this.insertwp.Text = "插入航点";
            this.insertwp.Click += new System.EventHandler(this.insertwp_Click);
            // 
            // deletewp
            // 
            this.deletewp.Name = "deletewp";
            this.deletewp.Size = new System.Drawing.Size(172, 22);
            this.deletewp.Text = "删除航点";
            this.deletewp.Click += new System.EventHandler(this.deletewp_Click);
            // 
            // loiter
            // 
            this.loiter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loiter_forever,
            this.loiter_time,
            this.loiter_circle});
            this.loiter.Name = "loiter";
            this.loiter.Size = new System.Drawing.Size(172, 22);
            this.loiter.Text = "盘旋点增加";
            // 
            // loiter_forever
            // 
            this.loiter_forever.Name = "loiter_forever";
            this.loiter_forever.Size = new System.Drawing.Size(100, 22);
            this.loiter_forever.Text = "永远";
            this.loiter_forever.Click += new System.EventHandler(this.loiter_forever_Click);
            // 
            // loiter_time
            // 
            this.loiter_time.Name = "loiter_time";
            this.loiter_time.Size = new System.Drawing.Size(100, 22);
            this.loiter_time.Text = "时间";
            this.loiter_time.Click += new System.EventHandler(this.loiter_time_Click);
            // 
            // loiter_circle
            // 
            this.loiter_circle.Name = "loiter_circle";
            this.loiter_circle.Size = new System.Drawing.Size(100, 22);
            this.loiter_circle.Text = "绕圈";
            // 
            // deletelist
            // 
            this.deletelist.Name = "deletelist";
            this.deletelist.Size = new System.Drawing.Size(172, 22);
            this.deletelist.Text = "清除航线";
            this.deletelist.Click += new System.EventHandler(this.deletelist_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // drawpolygon
            // 
            this.drawpolygon.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addpolygonpoint,
            this.clearpolygon,
            this.savepolygon,
            this.loadpolygon,
            this.areatoolstripmenuitem,
            this.loadkmlfile,
            this.writekml,
            this.clearkml});
            this.drawpolygon.Name = "drawpolygon";
            this.drawpolygon.Size = new System.Drawing.Size(172, 22);
            this.drawpolygon.Text = "绘制多边形";
            // 
            // addpolygonpoint
            // 
            this.addpolygonpoint.Name = "addpolygonpoint";
            this.addpolygonpoint.Size = new System.Drawing.Size(126, 22);
            this.addpolygonpoint.Text = "添加";
            this.addpolygonpoint.Click += new System.EventHandler(this.addpolygonpoint_Click);
            // 
            // clearpolygon
            // 
            this.clearpolygon.Name = "clearpolygon";
            this.clearpolygon.Size = new System.Drawing.Size(126, 22);
            this.clearpolygon.Text = "清除";
            this.clearpolygon.Click += new System.EventHandler(this.clearpolygon_Click);
            // 
            // savepolygon
            // 
            this.savepolygon.Name = "savepolygon";
            this.savepolygon.Size = new System.Drawing.Size(126, 22);
            this.savepolygon.Text = "保存";
            this.savepolygon.Click += new System.EventHandler(this.savepolygon_Click);
            // 
            // loadpolygon
            // 
            this.loadpolygon.Name = "loadpolygon";
            this.loadpolygon.Size = new System.Drawing.Size(126, 22);
            this.loadpolygon.Text = "加载";
            this.loadpolygon.Click += new System.EventHandler(this.loadpolygon_Click);
            // 
            // areatoolstripmenuitem
            // 
            this.areatoolstripmenuitem.Name = "areatoolstripmenuitem";
            this.areatoolstripmenuitem.Size = new System.Drawing.Size(126, 22);
            this.areatoolstripmenuitem.Text = "面积";
            this.areatoolstripmenuitem.Click += new System.EventHandler(this.areatoolstripmenuitem_Click);
            // 
            // loadkmlfile
            // 
            this.loadkmlfile.Name = "loadkmlfile";
            this.loadkmlfile.Size = new System.Drawing.Size(126, 22);
            this.loadkmlfile.Text = "加载KML";
            // 
            // writekml
            // 
            this.writekml.Name = "writekml";
            this.writekml.Size = new System.Drawing.Size(126, 22);
            this.writekml.Text = "绘制KML";
            // 
            // clearkml
            // 
            this.clearkml.Name = "clearkml";
            this.clearkml.Size = new System.Drawing.Size(126, 22);
            this.clearkml.Text = "清除KML";
            // 
            // land
            // 
            this.land.Name = "land";
            this.land.Size = new System.Drawing.Size(172, 22);
            this.land.Text = "备降点";
            // 
            // saveandload
            // 
            this.saveandload.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savewaypoints,
            this.loadwaypoints});
            this.saveandload.Name = "saveandload";
            this.saveandload.Size = new System.Drawing.Size(172, 22);
            this.saveandload.Text = "航点加载/保存";
            // 
            // savewaypoints
            // 
            this.savewaypoints.Name = "savewaypoints";
            this.savewaypoints.Size = new System.Drawing.Size(124, 22);
            this.savewaypoints.Text = "保存文件";
            // 
            // loadwaypoints
            // 
            this.loadwaypoints.Name = "loadwaypoints";
            this.loadwaypoints.Size = new System.Drawing.Size(124, 22);
            this.loadwaypoints.Text = "加载文件";
            // 
            // poi
            // 
            this.poi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.poiadd,
            this.poidelete});
            this.poi.Name = "poi";
            this.poi.Size = new System.Drawing.Size(172, 22);
            this.poi.Text = "标记点";
            // 
            // poiadd
            // 
            this.poiadd.Name = "poiadd";
            this.poiadd.Size = new System.Drawing.Size(100, 22);
            this.poiadd.Text = "添加";
            // 
            // poidelete
            // 
            this.poidelete.Name = "poidelete";
            this.poidelete.Size = new System.Drawing.Size(100, 22);
            this.poidelete.Text = "删除";
            // 
            // sethome
            // 
            this.sethome.Name = "sethome";
            this.sethome.Size = new System.Drawing.Size(172, 22);
            this.sethome.Text = "设置家在当前中心";
            this.sethome.Click += new System.EventHandler(this.sethome_Click);
            // 
            // lbl_prevdist
            // 
            this.lbl_prevdist.AutoSize = true;
            this.lbl_prevdist.Location = new System.Drawing.Point(164, 30);
            this.lbl_prevdist.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_prevdist.Name = "lbl_prevdist";
            this.lbl_prevdist.Size = new System.Drawing.Size(47, 12);
            this.lbl_prevdist.TabIndex = 6;
            this.lbl_prevdist.Text = "label11";
            // 
            // lbl_homedist
            // 
            this.lbl_homedist.AutoSize = true;
            this.lbl_homedist.Location = new System.Drawing.Point(166, 51);
            this.lbl_homedist.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_homedist.Name = "lbl_homedist";
            this.lbl_homedist.Size = new System.Drawing.Size(47, 12);
            this.lbl_homedist.TabIndex = 7;
            this.lbl_homedist.Text = "label11";
            // 
            // flightplanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_homedist);
            this.Controls.Add(this.lbl_prevdist);
            this.Controls.Add(this.FlightPlanner_TrackBar);
            this.Controls.Add(this.lab_E);
            this.Controls.Add(this.lab_N);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.gMapControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "flightplanner";
            this.Size = new System.Drawing.Size(640, 421);
            this.Load += new System.EventHandler(this.flightplanner_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Commands)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlightPlanner_TrackBar)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public GMap.NET.WindowsForms.GMapControl gMapControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lab_N;
        private System.Windows.Forms.Label lab_E;
        private System.Windows.Forms.Button but_wpread;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button but_Clearroutes;
        private System.Windows.Forms.Button but_Measuringconfig;
        private System.Windows.Forms.Button but_Cleararea;
        private System.Windows.Forms.Button but_Drawarea;
        private System.Windows.Forms.Button but_Quickdraw;
        private System.Windows.Forms.Button but_Loadfile;
        private System.Windows.Forms.Button but_Savefile;
        private System.Windows.Forms.Button but_Writewp;
        private System.Windows.Forms.Button but_Updatelocation;
        private System.Windows.Forms.Button but_Homelocation;
        public System.Windows.Forms.TextBox TXT_DefaultAlt;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox TXT_WPRad;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private MAVTOOL.Controls.MyTrackBar FlightPlanner_TrackBar;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem insertwp;
        private System.Windows.Forms.ToolStripMenuItem deletewp;
        private System.Windows.Forms.ToolStripMenuItem loiter;
        private System.Windows.Forms.ToolStripMenuItem loiter_forever;
        private System.Windows.Forms.ToolStripMenuItem loiter_time;
        private System.Windows.Forms.ToolStripMenuItem loiter_circle;
        private System.Windows.Forms.ToolStripMenuItem drawpolygon;
        private System.Windows.Forms.ToolStripMenuItem land;
        private System.Windows.Forms.ToolStripMenuItem saveandload;
        private System.Windows.Forms.ToolStripMenuItem poi;
        private System.Windows.Forms.ToolStripMenuItem sethome;
        private System.Windows.Forms.ToolStripMenuItem deletelist;
        private System.Windows.Forms.ToolStripMenuItem addpolygonpoint;
        private System.Windows.Forms.ToolStripMenuItem clearpolygon;
        private System.Windows.Forms.ToolStripMenuItem savepolygon;
        private System.Windows.Forms.ToolStripMenuItem loadpolygon;
        private System.Windows.Forms.ToolStripMenuItem areatoolstripmenuitem;
        private System.Windows.Forms.ToolStripMenuItem loadkmlfile;
        private System.Windows.Forms.ToolStripMenuItem writekml;
        private System.Windows.Forms.ToolStripMenuItem clearkml;
        private System.Windows.Forms.ToolStripMenuItem savewaypoints;
        private System.Windows.Forms.ToolStripMenuItem loadwaypoints;
        private System.Windows.Forms.ToolStripMenuItem poiadd;
        private System.Windows.Forms.ToolStripMenuItem poidelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label lbl_prevdist;
        private System.Windows.Forms.Label lbl_homedist;
        private System.Windows.Forms.DataGridView Commands;
        private System.Windows.Forms.DataGridViewComboBoxColumn Command;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordNorthing;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordEasting;
        private System.Windows.Forms.DataGridViewTextBoxColumn coordZone;
        private System.Windows.Forms.DataGridViewTextBoxColumn MGRS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Param1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Param2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Param3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Param4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lon;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Angle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dist;
        private System.Windows.Forms.DataGridViewTextBoxColumn Grad;
        private System.Windows.Forms.DataGridViewTextBoxColumn AZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagData;
        private System.Windows.Forms.DataGridViewImageColumn Up;
        private System.Windows.Forms.DataGridViewImageColumn Down;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
    }
}
