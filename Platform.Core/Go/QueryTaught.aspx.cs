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
	public partial class GoQueryTaught : HomoryCorePageWithGrid
	{
        private const string Right = "QueryTaught";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                BindChartDataSource();
                LogOp(OperationType.查询);
            }
        }

        protected void BindChartDataSource()
        {
            List<Calced> source;
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                source = HomoryContext.Value.ViewQueryTaught.GroupBy(o => o.课程名称).Select(o => new Calced { Name = o.Key, Num = o.Count() }).ToList();
            }
            else
            {
                var campus = CurrentCampus.Name;
                source = HomoryContext.Value.ViewQueryTaught.Where(o => o.学校 == campus).GroupBy(o => o.课程名称).Select(o => new Calced { Name = o.Key, Num = o.Count() }).ToList();
            }
            c.Series.Clear();
            var cs = new ChartSeries { DataLabelsColumn = "Name", DataYColumn = "Num", Type = ChartSeriesType.Pie };
            cs.Appearance.ShowLabelConnectors = true;
            c.Series.Add(cs);
            c.Series[0].Appearance.LegendDisplayMode = ChartSeriesLegendDisplayMode.ItemLabels;
            c.DataSource = source;
            c.DataBind();
            foreach (var p in c.Series[0].Items)
            {
                p.Label.TextBlock.Text = string.Format("{0} （{1}）", p.Label.TextBlock.Text , p.YValue.ToString());
            }
        }

        protected string[] SeriesNames
        {
            get
            {
                return HomoryContext.Value.ViewQueryTaught.Select(o => o.学校).OrderBy(o => o).Distinct().ToArray();
            }
        }

        protected int[] SeriesIndexes
        {
            get
            {
                var count = HomoryContext.Value.ViewQueryTaught.Select(o => o.学校).OrderBy(o => o).Distinct().Count();
                int[] r = new int[count];
                for (var i = 0; i < count; i++)
                {
                    r[i] = i;
                }
                return r;
            }
        }


        protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
		{
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                grid.DataSource = HomoryContext.Value.ViewQueryTaught.ToList();
            }
            else
            {
                var campus = CurrentCampus.Name;
                grid.DataSource = HomoryContext.Value.ViewQueryTaught.Where(o => o.学校 == campus).ToList();
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
            grid.ExportSettings.FileName = "Taught";
            grid.ExportSettings.OpenInNewWindow = true;
            grid.MasterTableView.ExportToExcel();
        }

		protected override string PageRight
		{
			get { return Right; }
		}
	}
}
