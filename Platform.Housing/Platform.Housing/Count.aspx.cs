using STSdb4.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Count : System.Web.UI.Page
{
    protected Lazy<Entities> db = new Lazy<Entities>(() => new Entities());

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var list = db.Value.Housing_Department.OrderBy(o => o.Ordinal).ToList().Select(o => new HousingCount { 单位 = o, 数量 = 0 }).ToList();
            try
            {
                using (IStorageEngine engine = STSdb.FromFile(Server.MapPath("App_Data/Housing__Count.record")))
                {
                    var table = engine.OpenXTablePortable<Guid, int>("Record");
                    list.ForEach(x =>
                    {
                        try
                        {
                            x.数量 = table[x.单位.Id];
                        }
                        catch
                        {
                        }
                    });
                }
            }
            catch
            {
            }
            chart.PlotArea.XAxis.DataLabelsField = "单位.Name";
            foreach (var item in list)
            {
                chart.PlotArea.XAxis.Items.Add(new AxisItem { LabelText = item.单位.Name });
            }
            var series = new BarSeries { Name = "住房信息数量", DataFieldY = "数量" };
            chart.PlotArea.Series.Add(series);
            chart.DataSource = list;
            chart.DataBind();
       }
    }
}
