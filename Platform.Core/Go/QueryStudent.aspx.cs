using System;
using System.Linq;
using Homory.Model;
using Telerik.Web.UI;
using Telerik.Charting;
using System.Drawing;
using System.Collections.Generic;
using Telerik.Charting.Styles;

namespace Go
{
	public partial class GoQueryStudent : HomoryCorePageWithGrid
	{
        private const string Right = "QueryStudent";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadInit();
                BindChartDataSource();
                LogOp(OperationType.查询);
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

        protected void BindChartDataSource()
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
                var width = 0;
                foreach (var s in l)
                {
                    lA.Add(new Calced(s, HomoryContext.Value.ViewQueryStudent.Count(o => o.学校 == s && o.状态 == "启用")));
                    width++;
                }
                foreach (Calced item in lA)
                {
                    this.BuildSeriesItem(this.c.Series[SeriesIndexes[0]], (double)item.Num, item.Num.ToString());
                }
                this.c.Width = 150 + 200 * width;
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

        protected static string[] SeriesNames = new string[] { "学生" };
        protected static int[] SeriesIndexes = new int[] { 0 };

        private void LoadInit()
		{
			loading.InitialDelayTime = int.Parse("Busy".FromWebConfig());
		}

        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                grid.DataSource = HomoryContext.Value.ViewQueryStudent.OrderByDescending(o => o.届).ThenBy(o => o.学校).ThenBy(o => o.班级).ThenBy(o => o.学号).ToList();
            }
            else
            {
                var campus = CurrentCampus.Name;
                grid.DataSource = HomoryContext.Value.ViewQueryStudent.Where(o => o.学校 == campus).OrderByDescending(o => o.届).ThenBy(o => o.学校).ThenBy(o => o.班级).ThenBy(o => o.学号).ToList();
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

        protected void grid_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName != RadGrid.ExportToExcelCommandName) return;
            grid.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            grid.ExportSettings.IgnorePaging = true;
            grid.ExportSettings.ExportOnlyData = true;
            grid.ExportSettings.FileName = "Student";
            grid.ExportSettings.OpenInNewWindow = true;
            grid.MasterTableView.ExportToExcel();
        }

		protected override string PageRight
		{
			get { return Right; }
		}
	}
}
