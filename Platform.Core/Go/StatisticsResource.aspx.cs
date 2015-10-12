using Homory.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using Telerik.Charting;
using Telerik.Charting.Styles;
using Telerik.Web.UI;

namespace Go
{
    public partial class GoStatisticsResource : HomoryCorePageWithGrid
	{
        private const string Right = "StatisticsResource";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadInit();
				grid.Rebind();
                LogOp(OperationType.查询);
            }
		}

		private void LoadInit()
		{
			var year = DateTime.Today.Year;
            if (HomoryContext.Value.ViewQueryResource.Count() == 0)
                return;
            for (var i = year; i >= HomoryContext.Value.ViewQueryResource.Min(o => o.年份); i--)
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

        protected void BindChartDataSource(List<ViewQueryResource> source)
        {
            this.UnDisplayTitle();
            this.ClearSeries();
            this.BuildSeries();
            Dictionary<Guid, string> l = new Dictionary<Guid, string>();
            foreach (var s in HomoryContext.Value.Department.Where(o => o.State == State.启用 && o.Type == DepartmentType.学校).OrderBy(o => o.Ordinal).Select(o => new { o.Id, o.Name }).ToList())
            {
                if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
                {
                    l.Add(s.Id, s.Name);
                }
                else
                {
                    var campus = CurrentCampus.Id;
                    if (s.Id == campus)
                        l.Add(s.Id, s.Name);
                }
            }
            if (l.Count() > 0)
            {
                List<Calced> l0 = new List<Calced>();
                List<Calced> l1 = new List<Calced>();
                var width = 0;
                foreach (var s in l)
                {
                    l0.Add(new Calced(s.Value, source.Where(o => o.CampusId == s.Key).Sum(o => o.下载资源 + o.发布文章 + o.发布视频 + o.发布试卷 + o.发布课件 + o.回复评论 + o.收藏资源 + o.浏览资源 + o.评定资源 + o.评论资源)));
                    l1.Add(new Calced(s.Value, source.Where(o => o.CampusId == s.Key).Sum(o => o.获得积分)));
                    width++;
                }
                var array = new List<Calced>[] { l0, l1 };
                for (int _index = 0; _index < array.Length; _index++)
                {
                    foreach (Calced item in array[_index])
                    {
                        this.BuildSeriesItem(this.c.Series[SeriesIndexes[_index]], (double)item.Num, item.Num.ToString());
                    }
                }
                this.c.Width = 100 + 200 * width;
                this.c.PlotArea.XAxis.AutoScale = false;
                this.c.PlotArea.XAxis.Clear();
                this.c.PlotArea.XAxis.AddRange(1, l.Count, 1);
                for (int i = 0; i < l.Count; i++)
                {
                    this.c.PlotArea.XAxis[i].TextBlock.Appearance.AutoTextWrap = AutoTextWrap.True;
                    this.c.PlotArea.XAxis[i].TextBlock.Appearance.TextProperties.Color = (i % 2) == 0 ? Color.Silver : Color.YellowGreen;
                    this.c.PlotArea.XAxis[i].TextBlock.Text = l.ElementAt(i).Value;
                }
            }
        }

        protected static string[] SeriesNames = new string[] { "互动量", "荣誉值" };
        protected static int[] SeriesIndexes = new int[] { 0, 1 };

        protected void grid_OnNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (combo.SelectedIndex < 0 || comboX.SelectedIndex < 0)
            {
                grid.DataSource = null;
                return;
            }
            var year = int.Parse(combo.SelectedItem.Value);
            var month = int.Parse(comboX.SelectedItem.Value);
            List<ViewQueryResource> query;
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                query = month == 0
             ? HomoryContext.Value.ViewQueryResource.Where(o => o.年份 == year).ToList()
             : HomoryContext.Value.ViewQueryResource.Where(o => o.年份 == year && o.月份 == month).ToList();
            }
            else
            {
                var cid = CurrentCampus == null ? (Guid?)null : CurrentCampus.Id;
                if (cid == null)
                {
                    query = new List<ViewQueryResource>();
                }
                else
                {
                    query = month == 0
                 ? HomoryContext.Value.ViewQueryResource.Where(o => o.年份 == year && o.CampusId == CurrentCampus.Id).ToList()
                 : HomoryContext.Value.ViewQueryResource.Where(o => o.年份 == year && o.月份 == month && o.CampusId == CurrentCampus.Id).ToList();
                }
            }
            var list = new List<ViewQueryResource>();
            foreach (var g in query.GroupBy(o => o.Id))
            {
                var obj = new ViewQueryResource();
                obj.学校 = g.First().学校;
                obj.教师 = g.First().教师;
                obj.CampusId = g.First().CampusId;
                obj.Id = g.First().Id;
                obj.下载资源 = g.Sum(o => o.下载资源);
                obj.发布文章 = g.Sum(o => o.发布文章);
                obj.发布视频 = g.Sum(o => o.发布视频);
                obj.发布试卷 = g.Sum(o => o.发布试卷);
                obj.发布课件 = g.Sum(o => o.发布课件);
                obj.回复评论 = g.Sum(o => o.回复评论);
                obj.收藏资源 = g.Sum(o => o.收藏资源);
                obj.浏览资源 = g.Sum(o => o.浏览资源);
                obj.获得积分 = g.Sum(o => o.获得积分);
                obj.评定资源 = g.Sum(o => o.评定资源);
                obj.评论资源 = g.Sum(o => o.评论资源);
                list.Add(obj);
            }
            grid.DataSource = list;
            if (CurrentRights.Contains(HomoryCoreConstant.RightGlobal))
            {
                BindChartDataSource(list);
            }
            else
            {
                BindChartDataSource(list);
            }
        }
    }
}
