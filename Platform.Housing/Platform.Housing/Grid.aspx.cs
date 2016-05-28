using STSdb4.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Grid : SsoPage
{
    public Guid HousingId
    {
        get
        {
            return (Guid)Session["HousingId"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            combo.DataSource = db.Value.Housing_Department.OrderBy(o => o.Ordinal).ToList();
            combo.DataBind();
            if (HousingId == Guid.Empty)
            {
                combo.Enabled = true;
                combo.AllowCustomText = true;
                combo.SelectedIndex = 0;
            }
            else
            {
                combo.Enabled = false;
                combo.AllowCustomText = false;
                combo.SelectedIndex = combo.Items.FindItemIndexByValue(HousingId.ToString());
            }
            grid.Rebind();
        }
    }

    protected void combo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        grid.Rebind();
    }

    protected void grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (combo.SelectedIndex == -1)
        {
            grid.DataSource = new List<HousingRecord>();
        }
        else
        {
            var gid = Guid.Parse(combo.SelectedValue);
            var id = Guid.Parse(combo.SelectedValue).ToString().ToUpper();
            var list = new List<HousingRecord>();
            using (IStorageEngine engine = STSdb.FromFile(Server.MapPath(string.Format("App_Data/Housing_{0}.record", id))))
            {
                var table = engine.OpenXTablePortable<HousingKey, HousingValue>("Record");
                list = table.Where(o => o.Key.学校 == gid).Select(o => new HousingRecord(o.Key, o.Value)).ToList();
            }
            try
            {
                using (IStorageEngine engine = STSdb.FromFile(Server.MapPath("App_Data/Housing__Count.record")))
                {
                    var table = engine.OpenXTablePortable<Guid, int>("Record");
                    table[gid] = list.Count;
                    engine.Commit();
                }
            }
            catch
            {
            }
            grid.DataSource = list;
        }
    }

    protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
    {
        var gid = Guid.Parse(combo.SelectedValue);
        var id = combo.SelectedValue.ToUpper();
        using (IStorageEngine engine = STSdb.FromFile(Server.MapPath(string.Format("App_Data/Housing_{0}.record", id))))
        {
            var table = engine.OpenXTablePortable<HousingKey, HousingValue>("Record");
            foreach (var command in e.Commands)
            {
                var values = command.NewValues;
                var key = new HousingKey { 学校 = gid, 姓名 = values["姓名"].V(), 住址 = values["住址"].V(), 入学年份 = values["入学年份"].V(), 户籍 = values["户籍"].V(), 身份证号 = values["身份证号"].V() };
                var value = new HousingValue { 班号 = values["班号"].V(), 备注 = values["备注"].V(), 时间 = DateTime.Now };
                switch (command.Type)
                {
                    case GridBatchEditingCommandType.Insert:
                        {
                            table[key] = value;
                            engine.Commit();
                            break;
                        }
                    case GridBatchEditingCommandType.Update:
                        {
                            var old = command.OldValues;
                            var okey = new HousingKey { 学校 = gid, 姓名 = old["姓名"].V(), 住址 = old["住址"].V(), 入学年份 = old["入学年份"].V(), 户籍 = old["户籍"].V(), 身份证号 = old["身份证号"].V() };
                            table.Delete(okey);
                            table[key] = value;
                            engine.Commit();
                            break;
                        }
                    case GridBatchEditingCommandType.Delete:
                        {
                            table.Delete(key);
                            engine.Commit();
                            break;
                        }
                }
            }
        }
        grid.Rebind();
    }

    protected void import_Click(object sender, EventArgs e)
    {
        var id = combo.SelectedValue.ToUpper();
        Response.Redirect(string.Format("Import.aspx?OnlineId={0}&CampusId={1}", Request.QueryString["OnlineId"], id));
    }

    protected void query_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Query.aspx?OnlineId={0}", Request.QueryString["OnlineId"]));
    }

    protected void log_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Log.aspx?OnlineId={0}", Request.QueryString["OnlineId"]));
    }
}
