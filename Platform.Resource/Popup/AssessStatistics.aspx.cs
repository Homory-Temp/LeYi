using Homory.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Popup_AssessStatistics : HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageInit();
        }
    }

    protected void PageInit(){

        var Id = Guid.Parse(Request.QueryString["Id"].ToString());

        List<View_ResourceAssess> ResourceAssess = HomoryContext.Value.View_ResourceAssess.Where(o => o.RId == Id).ToList();

        DataTable dt = ConvertResourceAssess(ResourceAssess);

        grid.DataSource = dt.Rows.Count == 0 ? null : dt;
        grid.DataBind();

    }

    public class AssessItem
    {
        public string Name { get; set; }
        public decimal Score { get; set; }
    }


    protected void DoChart(List<AssessItem> obj, List<View_ResourceAssess> ResourceAssess)
    {
        var c = chart;
        c.PlotArea.XAxis.Items.Clear();

        decimal[] nums = new decimal[obj.Count];

        for (var _p = 0; _p < obj.Count; _p++)
        {
            c.PlotArea.XAxis.Items.Add(obj[_p].Name);
            int ic = 0;
            int iv = 0;
            for (var _q = 0; _q < ResourceAssess.Count; _q++)
            {
                var x = JsonConvert.DeserializeObject<List<int>>(ResourceAssess[_q].ResultContent);
                ic++;
                iv += x[_p];
            }
            try
            {
                nums[_p] = decimal.Round(decimal.Divide(iv, ic), 1, MidpointRounding.AwayFromZero);
            }
            catch
            {
                nums[_p] = 0;
            }
        }

        BuildChart(c, obj, nums);

        //for (var _p = 0; _p < obj.Count; _p++)
        //{
        //    BuildChart(c, obj[_p].Name, nums[_p], _p, obj.Count);
        //}
    }

    protected void BuildChart(RadHtmlChart chart, List<AssessItem> list, decimal[] values)
    {
        RadarColumnSeries item = new RadarColumnSeries();
        item.Stacked = false;
        item.LabelsAppearance.Visible = false;
        foreach (var value in values)
        {
            item.SeriesItems.Add(new CategorySeriesItem(value));
        }
        chart.PlotArea.Series.Add(item);
    }

    //protected void BuildChart(RadHtmlChart chart, string name, decimal value, int index, int total)
    //{
    //    RadarColumnSeries item = new RadarColumnSeries();
    //    item.Name = name;
    //    item.Stacked = false;
    //    item.LabelsAppearance.Visible = false;
    //    for (var i = 0; i < total; i++)
    //    {
    //        if (i == index)
    //            item.SeriesItems.Add(new CategorySeriesItem(value));
    //        else
    //            item.SeriesItems.Add(new CategorySeriesItem(null));
    //    }
    //    chart.PlotArea.Series.Add(item);
    //}


    protected DataTable ConvertResourceAssess(List<View_ResourceAssess> ResourceAssess)
    {
        DataTable table = new DataTable();
        if (ResourceAssess.Count == 0)
            return table;

        var Id = ResourceAssess.First().Id;


        var _ast = HomoryContext.Value.AssessTable.Single(o => o.Id == Id);
        


        List<AssessItem> obj = JsonConvert.DeserializeObject<List<AssessItem>>(_ast.Content);


        
        table.Columns.Add("评分者", typeof(string));
        
        for(var i=0;i<obj.Count;i++)
        {
            table.Columns.Add(obj[i].Name, typeof(decimal));
        }
        
        foreach (var t in ResourceAssess)
        {
            var x = JsonConvert.DeserializeObject<List<int>>(t.ResultContent);

            var row = table.NewRow();

            row["评分者"] = t.DisplayName;

            for(var j=1;j<table.Columns.Count;j++)
            {

                row[table.Columns[j].ColumnName] = x[j - 1].ToString();

            }

            table.Rows.Add(row);
        }
        DoChart(obj, ResourceAssess);

        return table;
        //DataTable dt = new DataTable();

        //dt.Columns.Add("评分者", typeof(string));

        //if (ResourceAssess.Count()>0)
        //{
        //    var JsonContent = ResourceAssess[0].Content;

        //    List<AssessItem> JsonList = (List<AssessItem>)JsonConvert.DeserializeObject(JsonContent);

        //    for (var i = 0; i < JsonList.Count;i++ )
        //    {
        //        dt.Columns.Add(JsonList.Columns[i].Caption, typeof(string));
        //    }


        //        for (int i = 0; i < JsonList.Columns.Count; i++)
        //        {
        //            dt.Columns.Add(JsonList.Columns[i].Caption, typeof(string));
        //        }  
        //}

        //for (int i = 0; i < ResourceAssess.Count; i++)
        //{
        //    var JsonContent = ResourceAssess[i].Content;

        //    DataTable JsonList = (DataTable)JsonConvert.DeserializeObject(JsonContent);

        //    List<int> ResultList = (List<int>)JsonConvert.DeserializeObject(ResourceAssess[i].ResultContent);

        //    DataRow dr = dt.NewRow();

        //    dr["评分者"] = ResourceAssess[i].DisplayName;

        //    for (int j = 0; j < JsonList.Columns.Count; j++)
        //    {
        //        dr[JsonList.Columns[j].Caption] = ResultList[j];
        //    }

        //    dt.Rows.Add(dr);
        //}

        //return dt;
    
    }



}