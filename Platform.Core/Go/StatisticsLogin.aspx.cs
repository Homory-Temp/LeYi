using System;
using System.Globalization;
using System.Linq;
using Homory.Model;
using Telerik.Web.UI;
using System.Collections.Generic;
using Telerik.Charting;
using System.Drawing;
using Telerik.Charting.Styles;

namespace Go
{
    public partial class GoStatisticsLogin : HomoryCorePageWithGrid
    {
        private const string Right = "StatisticsLogin";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInit();
                LogOp(OperationType.查询);
            }
        }

        protected void grid_OnInit(object sender, EventArgs e)
        {
            InitFilterMenu(grid.FilterMenu);
        }

        protected void InitFilterMenu(GridFilterMenu menu)
        {
            menu.Items.Clear();
            var itemNoFilter = new RadMenuItem { Text = "全部", Value = "NoFilter" };
            menu.Items.Add(itemNoFilter);
            var itemContains = new RadMenuItem { Text = "模糊查找", Value = "Contains" };
            menu.Items.Add(itemContains);
            var itemEqualTo = new RadMenuItem { Text = "精确查找", Value = "EqualTo" };
            menu.Items.Add(itemEqualTo);
        }

        protected void BindChartDataSource2(List<ViewQuerySign> source)
        {
            List<Calced> l = new List<Calced>();
            l.Add(new Calced { Name = "登录成功", Num = source.Count(o => o.Login == true) });
            l.Add(new Calced { Name = "登录失败", Num = source.Count(o => o.Login == false) });
            c2.Series.Clear();
            var cs = new ChartSeries { DataLabelsColumn = "Name", DataYColumn = "Num", Type = ChartSeriesType.Pie };
            cs.Appearance.ShowLabelConnectors = true;
            c2.Series.Add(cs);
            c2.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
            c2.DataSource = l;
            c2.DataBind();
            foreach (var p in c2.Series[0].Items)
            {
                p.Label.TextBlock.Text = string.Format("{0} （{1}）", p.Label.TextBlock.Text, p.YValue.ToString());
            }
        }

        protected void BindChartDataSource3(List<ViewQuerySign> source)
        {
            List<Calced> l = new List<Calced>();
            l.Add(new Calced { Name = "IE", Num = source.Count(o => o.Browser == "InternetExplorer") });
            l.Add(new Calced { Name = "Firefox", Num = source.Count(o => o.Browser == "Firefox") });
            l.Add(new Calced { Name = "Chrome", Num = source.Count(o => o.Browser == "Chrome") });
            c3.Series.Clear();
            var cs = new ChartSeries { DataLabelsColumn = "Name", DataYColumn = "Num", Type = ChartSeriesType.Pie };
            cs.Appearance.ShowLabelConnectors = true;
            c3.Series.Add(cs);
            c3.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
            c3.DataSource = l;
            c3.DataBind();
            foreach (var p in c3.Series[0].Items)
            {
                p.Label.TextBlock.Text = string.Format("{0} （{1}）", p.Label.TextBlock.Text, p.YValue.ToString());
            }
        }

        protected void UnDisplayTitle()
        {
            this.c.ChartTitle.Appearance.Visible = false;
        }

        protected void ClearSeries()
        {
            this.c.Chart.Series.Clear();
        }

        protected void BuildSeries()
        {
            foreach (string s in SeriesNames)
            {
                this.BuildSeries(s);
            }
        }

        protected void BuildSeries(string title)
        {
            ChartSeries series = new ChartSeries(title);
            this.c.Chart.Series.Add(series);
        }

        protected void BuildSeriesItem(ChartSeries series, double count, string label)
        {
            ChartSeriesItem item = new ChartSeriesItem(count, label);
            item.Label.TextBlock.Appearance.TextProperties.Color = Color.HotPink;
            item.Label.TextBlock.Appearance.TextProperties.Font = new Font("Arial", 12, FontStyle.Bold);
            series.Items.Add(item);
        }

        protected void BindChartDataSource(List<ViewQuerySign> source)
        {
            this.UnDisplayTitle();
            this.ClearSeries();
            this.BuildSeries();
            List<string> l = new List<string>();
            foreach (var s in HomoryContext.Value.Department.Where(o => o.State == State.启用 && o.Type == DepartmentType.学校).OrderBy(o => o.Ordinal).Select(o => o.Name).ToList())
            {
                if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
                {
                    l.Add(s);
                }
                else
                {
                    var campus = CurrentCampus.Name;
                    if (s == campus)
                        l.Add(s);
                }
            }
            if (l.Count() > 0)
            {
                List<Calced> lA = new List<Calced>();
                List<Calced> lB = new List<Calced>();
                List<Calced> lC = new List<Calced>();
                List<Calced> lD = new List<Calced>();
                List<Calced> lE = new List<Calced>();
                var width = 0;
                foreach (var s in l)
                {
                    lA.Add(new Calced(s, source.Count(o => o.Name == s && o.Login == true)));
                    lB.Add(new Calced(s, source.Count(o => o.Name == s && o.Login == false)));
                    lC.Add(new Calced(s, source.Count(o => o.Name == s && o.Browser == "InternetExplorer")));
                    lD.Add(new Calced(s, source.Count(o => o.Name == s && o.Browser == "Firefox")));
                    lE.Add(new Calced(s, source.Count(o => o.Name == s && o.Browser == "Chrome")));
                    width++;
                }
                foreach (Calced item in lA)
                {
                    this.BuildSeriesItem(this.c.Series[SeriesIndexes[0]], (double)item.Num, item.Num.ToString());
                }
                foreach (Calced item in lB)
                {
                    this.BuildSeriesItem(this.c.Series[SeriesIndexes[1]], (double)item.Num, item.Num.ToString());
                }
                foreach (Calced item in lC)
                {
                    this.BuildSeriesItem(this.c.Series[SeriesIndexes[2]], (double)item.Num, item.Num.ToString());
                }
                foreach (Calced item in lD)
                {
                    this.BuildSeriesItem(this.c.Series[SeriesIndexes[3]], (double)item.Num, item.Num.ToString());
                }
                foreach (Calced item in lE)
                {
                    this.BuildSeriesItem(this.c.Series[SeriesIndexes[4]], (double)item.Num, item.Num.ToString());
                }
                this.c.Width = 100 + 220 * width;
                this.c.PlotArea.XAxis.AutoScale = false;
                this.c.PlotArea.XAxis.Clear();
                this.c.PlotArea.XAxis.AddRange(1, l.Count, 1);
                for (int i = 0; i < l.Count; i++)
                {
                    this.c.PlotArea.XAxis[i].TextBlock.Appearance.AutoTextWrap = AutoTextWrap.True;
                    this.c.PlotArea.XAxis[i].TextBlock.Appearance.TextProperties.Color = (i % 2) == 0 ? Color.Silver : Color.YellowGreen;
                    this.c.PlotArea.XAxis[i].TextBlock.Text = l.ElementAt(i);
                }
            }
        }

        protected static string[] SeriesNames = new string[] { "登录成功", "登录失败", "IE", "FireFox", "Chrome" };
        protected static int[] SeriesIndexes = new int[] { 0, 1, 2, 3, 4 };


        private void LoadInit()
        {
            loading.InitialDelayTime = int.Parse("Busy".FromWebConfig());
            var year = DateTime.Today.Year;
            if (HomoryContext.Value.SignLog.Count() == 0)
                return;
            for (var i = year; i >= HomoryContext.Value.SignLog.Min(o => o.Time).Year; i--)
            {
                combo.Items.Add(new RadComboBoxItem { Text = i.ToString(CultureInfo.InvariantCulture), Value = i.ToString(CultureInfo.InvariantCulture) });
            }
            combo.SelectedIndex = 0;
        }

        protected void combo_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            grid.Rebind();
        }

        protected override string PageRight
        {
            get { return Right; }
        }

        public class ChartItem
        {
            public int True { get; set; }
            public int False { get; set; }
            public int Month { get; set; }
            public int IE { get; set; }
            public int FF { get; set; }
            public int CH { get; set; }
        }

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (combo.SelectedIndex < 0)
                LoadInit();
            int y = int.Parse(combo.SelectedItem.Value);
            List<ViewQuerySign> list;
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                list = HomoryContext.Value.ViewQuerySign.ToList().Where(o => o.Time.Year == y).OrderByDescending(o => o.Id).ToList();
                fullChart.Visible = true;
                BindChartDataSource(list);
            }
            else
            {
                var campus = CurrentCampus.Name;
                list = HomoryContext.Value.ViewQuerySign.ToList().Where(o => o.Name == campus && o.Time.Year == y).OrderByDescending(o => o.Id).ToList();
                fullChart.Visible = false;
                c.DataSource = null;
            }
            grid.DataSource = list;
            BindChartDataSource2(list);
            BindChartDataSource3(list);
        }
    }
}
