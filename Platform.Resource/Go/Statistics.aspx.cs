using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoCatalog : HomoryResourcePage
    {
        private List<ViewStatistics> _s;

        protected List<ViewStatistics> S
        {
            get
            {
                if (_s == null)
                    _s = HomoryContext.Value.ViewStatistics.ToList();
                return _s;
            }
        }

        private List<Catalog> _c;

        protected List<Catalog> C
        {
            get
            {
                if (_c == null)
                        _c = HomoryContext.Value.Catalog.Where(o => (o.Type == CatalogType.年级_高中 || o.Type == CatalogType.年级_小学 || o.Type == CatalogType.年级_初中 || o.Type == CatalogType.年级_幼儿园) && o.State < State.审核).OrderBy(o => o.Ordinal).ToList();
                return _c;
            }
        }

        private List<Department> _d;

        protected List<Department> D
        {
            get
            {
                if (_d == null)
                        _d = HomoryContext.Value.Department.Where(o => o.Type == DepartmentType.学校 && o.State < State.审核).OrderBy(o => o.Ordinal).ToList();
                return _d;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                repeater.DataSource = S.Join(C, o => o.GradeId, o => o.Id, (x, y) => y).Distinct().OrderBy(o => o.Ordinal).ToList();
                repeater.DataBind();
            }
        }

        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var c = e.Item.FindControl("RadarAreaChart") as RadHtmlChart;
            Catalog d = e.Item.DataItem as Catalog;
            c.ChartTitle.Text = d.Name;
            c.PlotArea.XAxis.Items.Clear();
            int[][] values = new int[4][];
            bool filtered = !string.IsNullOrEmpty(Request.QueryString["Filtered"]) && Request.QueryString["Filtered"].Equals("True", StringComparison.OrdinalIgnoreCase);
            for (int i = 0; i<D.Count; i++)
            {
                if (i == 0)
                {
                    values[0] = new int[D.Count];
                    values[1] = new int[D.Count];
                    values[2] = new int[D.Count];
                    values[3] = new int[D.Count];
                }
                values[0][i] = S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.视频 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).HasValue ? S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.视频 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).Value : 0;
                values[1][i] = S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.文章 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).HasValue ? S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.文章 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).Value : 0;
                values[2][i] = S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.课件 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).HasValue ? S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.课件 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).Value : 0;
                values[3][i] = S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.试卷 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).HasValue ? S.Where(o => o.GradeId == d.Id && o.Type == ResourceType.试卷 && o.CampusId == D[i].Id && o.Count.HasValue).Sum(o => o.Count).Value : 0;
                if (filtered)
                {
                    if (values[0][i] + values[1][i] + values[2][i] + values[3][i] > 0)
                    {
                        c.PlotArea.XAxis.Items.Add(D[i].Name);
                    }
                }
                else
                    c.PlotArea.XAxis.Items.Add(D[i].Name);
            }
            c.PlotArea.Series.Clear();
            if (filtered)
            {
                BuildChart(c, "发布视频", 0xf4, 0x52, 0x62, values[0].Where(o => o > 0).ToArray());
                BuildChart(c, "发布文章", 0xf2, 0xe1, 0x23, values[1].Where(o => o > 0).ToArray());
                BuildChart(c, "发布课件", 0x2d, 0xc1, 0xec, values[2].Where(o => o > 0).ToArray());
                BuildChart(c, "发布试卷", 0x2d, 0xc1, 0xec, values[3].Where(o => o > 0).ToArray());
            }
            else
            {
                BuildChart(c, "发布视频", 0xf4, 0x52, 0x62, values[0]);
                BuildChart(c, "发布文章", 0xf2, 0xe1, 0x23, values[1]);
                BuildChart(c, "发布课件", 0x2d, 0xc1, 0xec, values[2]);
                BuildChart(c, "发布试卷", 0x2d, 0xc1, 0xec, values[3]);
            }
        }

        protected void BuildChart(RadHtmlChart chart, string name, int r, int g, int b, int[] values)
        {
            RadarColumnSeries item = new RadarColumnSeries();
            item.Name = name;
            item.Stacked = true;
            item.Appearance.FillStyle.BackgroundColor = System.Drawing.Color.FromArgb(r, g, b);
            item.LabelsAppearance.Visible = false;
            foreach (var value in values)
                item.SeriesItems.Add(value);
            chart.PlotArea.Series.Add(item);
        }
    }
}
