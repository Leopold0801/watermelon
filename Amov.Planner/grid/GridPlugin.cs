using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Amov.Planner.grid;
using GMap.NET.WindowsForms;
using MAVTOOL;

namespace Amov.Planner
{
    public class GridPlugin : Amov.Planner.Plugin.Plugin
    {

        ToolStripMenuItem but;

        public override string Name
        {
            get { return "Grid"; }
        }

        public override string Version
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public override string Author
        {
            get { return "Michael Oborne"; }
        }

        public override bool Init()
        {
            return true;
        }

        public override bool Loaded()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gridui));
            var temp = (string)(resources.GetObject("$this.Text"));

            but = new ToolStripMenuItem(temp);
            but.Click += but_Click;

            bool hit = false;
            ToolStripItemCollection col = Host.FPMenuMap.Items;
            int index = col.Count;
            foreach (ToolStripItem item in col)
            {
                if (item.Text.Equals(Strings.AutoWP))
                {
                    index = col.IndexOf(item);
                    ((ToolStripMenuItem)item).DropDownItems.Add(but);
                    hit = true;
                    break;
                }
            }

            if (hit == false)
                col.Add(but);

            return true;
        }

        public void but_Click(object sender, EventArgs e)
        {
            using (var gd = new gridui(this))
            {
                //MissionPlanner.Utilities.ThemeManager.ApplyThemeTo(gridui);

                if (Host.FPDrawnPolygon != null && Host.FPDrawnPolygon.Points.Count > 2)
                {
                    gd.ShowDialog();
                }
                else
                {
                    if (
                        CustomMessageBox.Show("No polygon defined. Load a file?", "Load File", MessageBoxButtons.YesNo) ==
                        DialogResult.Yes)
                    {
                        gd.LoadGrid();
                        gd.ShowDialog();
                    }
                    else
                    {
                        CustomMessageBox.Show("Please define a polygon.", "Error");
                    }
                }
            }
        }

        public override bool Exit()
        {
            return true;
        }
    }
}
