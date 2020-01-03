namespace Amov.Planner.views
{
    partial class flightdata
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
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(flightdata));
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.panel1_gmap = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.lab_gs = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lab_wd = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lab_yaw = new System.Windows.Forms.Label();
            this.lab_alt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LinkqualityGcsLab = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.Gps_nmulabel = new System.Windows.Forms.Label();
            this.Batterylabel = new System.Windows.Forms.Label();
            this.Hdop_label = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.Linkqualitygcs = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.altitudeMeter1 = new AttitudeInstrument.AltitudeMeter();
            this.airSpeedIndicator1 = new AttitudeInstrument.AirSpeedIndicator();
            this.pitchAndBank1 = new AttitudeInstrument.PitchAndBank();
            this.FlightData_TrackBar = new MAVTOOL.Controls.MyTrackBar();
            this.panel1_gmap.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlightData_TrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // gMap
            // 
            this.gMap.Bearing = 0F;
            this.gMap.CanDragMap = true;
            this.gMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemmory = 5;
            this.gMap.Location = new System.Drawing.Point(0, 0);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 2;
            this.gMap.MinZoom = 2;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedArea = ((GMap.NET.RectLatLng)(resources.GetObject("gMap.SelectedArea")));
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(1061, 805);
            this.gMap.TabIndex = 0;
            this.gMap.Zoom = 0D;
            this.gMap.Load += new System.EventHandler(this.gMap_Load);
            // 
            // panel1_gmap
            // 
            this.panel1_gmap.Controls.Add(this.FlightData_TrackBar);
            this.panel1_gmap.Controls.Add(this.gMap);
            this.panel1_gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1_gmap.Location = new System.Drawing.Point(0, 0);
            this.panel1_gmap.Name = "panel1_gmap";
            this.panel1_gmap.Size = new System.Drawing.Size(1061, 805);
            this.panel1_gmap.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.altitudeMeter1);
            this.panel2.Controls.Add(this.airSpeedIndicator1);
            this.panel2.Controls.Add(this.pitchAndBank1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(359, 805);
            this.panel2.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel7.Controls.Add(this.label28);
            this.panel7.Controls.Add(this.lab_gs);
            this.panel7.Controls.Add(this.label27);
            this.panel7.Controls.Add(this.label26);
            this.panel7.Controls.Add(this.label25);
            this.panel7.Controls.Add(this.label24);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.label18);
            this.panel7.Location = new System.Drawing.Point(3, 529);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(353, 73);
            this.panel7.TabIndex = 45;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.Color.Red;
            this.label28.Location = new System.Drawing.Point(130, 43);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(39, 19);
            this.label28.TabIndex = 45;
            this.label28.Text = "0.0";
            // 
            // lab_gs
            // 
            this.lab_gs.AutoSize = true;
            this.lab_gs.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gs.ForeColor = System.Drawing.Color.Red;
            this.lab_gs.Location = new System.Drawing.Point(261, 43);
            this.lab_gs.Name = "lab_gs";
            this.lab_gs.Size = new System.Drawing.Size(39, 19);
            this.lab_gs.TabIndex = 34;
            this.lab_gs.Text = "0.0";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(310, 43);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(39, 19);
            this.label27.TabIndex = 44;
            this.label27.Text = "m/s";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.ForeColor = System.Drawing.Color.Red;
            this.label26.Location = new System.Drawing.Point(174, 43);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(39, 19);
            this.label26.TabIndex = 43;
            this.label26.Text = "m/s";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(263, 15);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(69, 19);
            this.label25.TabIndex = 42;
            this.label25.Text = "地速：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label24.Location = new System.Drawing.Point(133, 15);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(69, 19);
            this.label24.TabIndex = 41;
            this.label24.Text = "空速：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(3, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 19);
            this.label9.TabIndex = 3;
            this.label9.Text = "升降速度：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(49, 43);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 19);
            this.label17.TabIndex = 39;
            this.label17.Text = "m/s";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(7, 43);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 19);
            this.label18.TabIndex = 40;
            this.label18.Text = "0.0";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.label21);
            this.panel6.Controls.Add(this.lab_wd);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Controls.Add(this.label10);
            this.panel6.Location = new System.Drawing.Point(4, 450);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(351, 75);
            this.panel6.TabIndex = 45;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Red;
            this.label16.Location = new System.Drawing.Point(48, 44);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 19);
            this.label16.TabIndex = 39;
            this.label16.Text = "m";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.Color.Red;
            this.label22.Location = new System.Drawing.Point(312, 44);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(19, 19);
            this.label22.TabIndex = 43;
            this.label22.Text = "m";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(259, 44);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 19);
            this.label21.TabIndex = 42;
            this.label21.Text = "0.0";
            // 
            // lab_wd
            // 
            this.lab_wd.AutoSize = true;
            this.lab_wd.BackColor = System.Drawing.Color.Transparent;
            this.lab_wd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_wd.ForeColor = System.Drawing.Color.Red;
            this.lab_wd.Location = new System.Drawing.Point(4, 44);
            this.lab_wd.Name = "lab_wd";
            this.lab_wd.Size = new System.Drawing.Size(39, 19);
            this.lab_wd.TabIndex = 35;
            this.lab_wd.Text = "0.0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(2, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(109, 19);
            this.label12.TabIndex = 6;
            this.label12.Text = "航行距离：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(173, 44);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 19);
            this.label20.TabIndex = 39;
            this.label20.Text = "m";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(129, 44);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(39, 19);
            this.label19.TabIndex = 41;
            this.label19.Text = "0.0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(243, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 19);
            this.label11.TabIndex = 5;
            this.label11.Text = "下一距离：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(117, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 19);
            this.label10.TabIndex = 4;
            this.label10.Text = "离家距离：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.LinkqualityGcsLab);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(4, 234);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 373);
            this.panel1.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel5.Controls.Add(this.label33);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.lab_yaw);
            this.panel5.Controls.Add(this.lab_alt);
            this.panel5.Location = new System.Drawing.Point(-1, 142);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(352, 71);
            this.panel5.TabIndex = 44;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(5, 36);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(66, 19);
            this.label33.TabIndex = 39;
            this.label33.Text = "未识别";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(313, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 19);
            this.label14.TabIndex = 38;
            this.label14.Text = "m";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Red;
            this.label13.Location = new System.Drawing.Point(179, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 19);
            this.label13.TabIndex = 37;
            this.label13.Text = "m";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(241, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "雷达高度：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(134, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 19);
            this.label7.TabIndex = 1;
            this.label7.Text = "高度：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(10, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 19);
            this.label6.TabIndex = 0;
            this.label6.Text = "模式：";
            // 
            // lab_yaw
            // 
            this.lab_yaw.AutoSize = true;
            this.lab_yaw.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_yaw.ForeColor = System.Drawing.Color.Red;
            this.lab_yaw.Location = new System.Drawing.Point(256, 36);
            this.lab_yaw.Name = "lab_yaw";
            this.lab_yaw.Size = new System.Drawing.Size(39, 19);
            this.lab_yaw.TabIndex = 36;
            this.lab_yaw.Text = "0.0";
            // 
            // lab_alt
            // 
            this.lab_alt.AutoSize = true;
            this.lab_alt.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_alt.ForeColor = System.Drawing.Color.Red;
            this.lab_alt.Location = new System.Drawing.Point(153, 36);
            this.lab_alt.Name = "lab_alt";
            this.lab_alt.Size = new System.Drawing.Size(19, 19);
            this.lab_alt.TabIndex = 33;
            this.lab_alt.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 19);
            this.label2.TabIndex = 39;
            this.label2.Text = "电压2：";
            // 
            // LinkqualityGcsLab
            // 
            this.LinkqualityGcsLab.AutoSize = true;
            this.LinkqualityGcsLab.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.LinkqualityGcsLab.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LinkqualityGcsLab.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LinkqualityGcsLab.Location = new System.Drawing.Point(305, 20);
            this.LinkqualityGcsLab.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkqualityGcsLab.Name = "LinkqualityGcsLab";
            this.LinkqualityGcsLab.Size = new System.Drawing.Size(36, 24);
            this.LinkqualityGcsLab.TabIndex = 29;
            this.LinkqualityGcsLab.Text = "0%";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.Gps_nmulabel);
            this.panel3.Controls.Add(this.Batterylabel);
            this.panel3.Controls.Add(this.Hdop_label);
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.Linkqualitygcs);
            this.panel3.Location = new System.Drawing.Point(0, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(349, 65);
            this.panel3.TabIndex = 37;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button6.Location = new System.Drawing.Point(119, 8);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(77, 40);
            this.button6.TabIndex = 26;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // Gps_nmulabel
            // 
            this.Gps_nmulabel.AutoSize = true;
            this.Gps_nmulabel.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Gps_nmulabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Gps_nmulabel.Location = new System.Drawing.Point(205, 30);
            this.Gps_nmulabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Gps_nmulabel.Name = "Gps_nmulabel";
            this.Gps_nmulabel.Size = new System.Drawing.Size(20, 19);
            this.Gps_nmulabel.TabIndex = 32;
            this.Gps_nmulabel.Text = "0";
            // 
            // Batterylabel
            // 
            this.Batterylabel.AutoSize = true;
            this.Batterylabel.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Batterylabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Batterylabel.Location = new System.Drawing.Point(61, 17);
            this.Batterylabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Batterylabel.Name = "Batterylabel";
            this.Batterylabel.Size = new System.Drawing.Size(36, 24);
            this.Batterylabel.TabIndex = 30;
            this.Batterylabel.Text = "0%";
            // 
            // Hdop_label
            // 
            this.Hdop_label.AutoSize = true;
            this.Hdop_label.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Hdop_label.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Hdop_label.Location = new System.Drawing.Point(205, 9);
            this.Hdop_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Hdop_label.Name = "Hdop_label";
            this.Hdop_label.Size = new System.Drawing.Size(42, 19);
            this.Hdop_label.TabIndex = 31;
            this.Hdop_label.Text = "0.0";
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button8.BackgroundImage")));
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.button8.Location = new System.Drawing.Point(13, 16);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(40, 32);
            this.button8.TabIndex = 28;
            this.button8.UseVisualStyleBackColor = false;
            // 
            // Linkqualitygcs
            // 
            this.Linkqualitygcs.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.Linkqualitygcs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Linkqualitygcs.BackgroundImage")));
            this.Linkqualitygcs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Linkqualitygcs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Linkqualitygcs.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.Linkqualitygcs.Location = new System.Drawing.Point(264, 13);
            this.Linkqualitygcs.Margin = new System.Windows.Forms.Padding(4);
            this.Linkqualitygcs.Name = "Linkqualitygcs";
            this.Linkqualitygcs.Size = new System.Drawing.Size(33, 35);
            this.Linkqualitygcs.TabIndex = 27;
            this.Linkqualitygcs.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel4.Controls.Add(this.label32);
            this.panel4.Controls.Add(this.label31);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label30);
            this.panel4.Controls.Add(this.label29);
            this.panel4.Controls.Add(this.label23);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(0, 71);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(350, 69);
            this.panel4.TabIndex = 43;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(312, 42);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(28, 19);
            this.label32.TabIndex = 46;
            this.label32.Text = "％";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label31.ForeColor = System.Drawing.Color.Red;
            this.label31.Location = new System.Drawing.Point(294, 42);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(19, 19);
            this.label31.TabIndex = 40;
            this.label31.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label5.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(278, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 42;
            this.label5.Text = "油门：";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label30.ForeColor = System.Drawing.Color.Red;
            this.label30.Location = new System.Drawing.Point(219, 44);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(19, 19);
            this.label30.TabIndex = 44;
            this.label30.Text = "A";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label29.ForeColor = System.Drawing.Color.Red;
            this.label29.Location = new System.Drawing.Point(219, 16);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(19, 19);
            this.label29.TabIndex = 43;
            this.label29.Text = "A";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.ForeColor = System.Drawing.Color.Red;
            this.label23.Location = new System.Drawing.Point(88, 44);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 19);
            this.label23.TabIndex = 42;
            this.label23.Text = "N/A";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(88, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 19);
            this.label15.TabIndex = 40;
            this.label15.Text = "N/A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label4.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(132, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 19);
            this.label4.TabIndex = 41;
            this.label4.Text = "电流2：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 38;
            this.label1.Text = "电压1：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label3.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(132, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 19);
            this.label3.TabIndex = 40;
            this.label3.Text = "电流1：";
            // 
            // altitudeMeter1
            // 
            this.altitudeMeter1.Altitude = 0D;
            this.altitudeMeter1.Location = new System.Drawing.Point(282, 4);
            this.altitudeMeter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.altitudeMeter1.Name = "altitudeMeter1";
            this.altitudeMeter1.Size = new System.Drawing.Size(77, 223);
            this.altitudeMeter1.TabIndex = 4;
            // 
            // airSpeedIndicator1
            // 
            this.airSpeedIndicator1.AirSpeed = 0D;
            this.airSpeedIndicator1.Location = new System.Drawing.Point(4, 4);
            this.airSpeedIndicator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.airSpeedIndicator1.Name = "airSpeedIndicator1";
            this.airSpeedIndicator1.Size = new System.Drawing.Size(65, 221);
            this.airSpeedIndicator1.TabIndex = 0;
            // 
            // pitchAndBank1
            // 
            this.pitchAndBank1.Bank = 0D;
            this.pitchAndBank1.Location = new System.Drawing.Point(69, 4);
            this.pitchAndBank1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pitchAndBank1.Name = "pitchAndBank1";
            this.pitchAndBank1.Pitch = 0D;
            this.pitchAndBank1.Size = new System.Drawing.Size(213, 223);
            this.pitchAndBank1.TabIndex = 1;
            // 
            // FlightData_TrackBar
            // 
            this.FlightData_TrackBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.FlightData_TrackBar.LargeChange = 0.005F;
            this.FlightData_TrackBar.Location = new System.Drawing.Point(1005, 0);
            this.FlightData_TrackBar.Maximum = 0.01F;
            this.FlightData_TrackBar.Minimum = 0F;
            this.FlightData_TrackBar.Name = "FlightData_TrackBar";
            this.FlightData_TrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.FlightData_TrackBar.Size = new System.Drawing.Size(56, 805);
            this.FlightData_TrackBar.SmallChange = 0.001F;
            this.FlightData_TrackBar.TabIndex = 1;
            this.FlightData_TrackBar.TickFrequency = 0.001F;
            this.FlightData_TrackBar.Value = 0F;
            this.FlightData_TrackBar.Scroll += new System.EventHandler(this.FlightData_TrackBar_Scroll);
            // 
            // flightdata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1_gmap);
            this.Name = "flightdata";
            this.Size = new System.Drawing.Size(1061, 805);
            this.Load += new System.EventHandler(this.flightdata_Load);
            this.VisibleChanged += new System.EventHandler(this.flightdata_VisibleChanged);
            this.panel1_gmap.ResumeLayout(false);
            this.panel1_gmap.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlightData_TrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.Panel panel1_gmap;
        private System.Windows.Forms.Panel panel2;
        public AttitudeInstrument.AirSpeedIndicator airSpeedIndicator1;
        public AttitudeInstrument.AltitudeMeter altitudeMeter1;
        public AttitudeInstrument.PitchAndBank pitchAndBank1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lab_wd;
        public System.Windows.Forms.Label Hdop_label;
        public System.Windows.Forms.Label Batterylabel;
        private System.Windows.Forms.Button button8;
        public System.Windows.Forms.Label LinkqualityGcsLab;
        private System.Windows.Forms.Button Linkqualitygcs;
        private System.Windows.Forms.Button button6;
        public System.Windows.Forms.Label Gps_nmulabel;
        public System.Windows.Forms.Label lab_alt;
        public System.Windows.Forms.Label lab_gs;
        public System.Windows.Forms.Label lab_yaw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        public System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label32;
        public System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label33;
        private MAVTOOL.Controls.MyTrackBar FlightData_TrackBar;
    }
}
