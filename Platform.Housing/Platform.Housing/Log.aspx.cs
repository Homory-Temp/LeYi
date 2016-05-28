using PanGu;
using PanGu.Setting;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Log : SsoPage
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
            from.SelectedDate = DateTime.Today.AddMonths(-1);
            to.SelectedDate = DateTime.Today;
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

    protected void query_Click(object sender, EventArgs e)
    {
        grid.Rebind();
    }

    protected void back_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Grid.aspx?OnlineId={0}", Request.QueryString["OnlineId"]));
    }

    protected void grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (combo.SelectedIndex == -1)
        {
            grid.DataSource = new List<HousingLog>();
        }
        else
        {
            var gid = Guid.Parse(combo.SelectedValue);
            var id = Guid.Parse(combo.SelectedValue).ToString().ToUpper();
            var list = new List<HousingLog>();
            var st = from.SelectedDate.HasValue ? from.SelectedDate.Value : DateTime.MinValue;
            var et = to.SelectedDate.HasValue ? to.SelectedDate.Value : DateTime.Today;
            if (st > et)
            {
                var tt = st;
                st = et;
                et = tt;
            }
            et = et.AddDays(1).AddMilliseconds(-1);
            var uid = Guid.Parse(Session["MemberId"].ToString());
            var root = db.Value.Housing_Member.Count(o => o.UserId == uid && o.DepartmentId == Guid.Empty) > 0;
            using (IStorageEngine engine = STSdb.FromFile(Server.MapPath(string.Format("App_Data/Housing_{0}.record.queryx", id))))
            {
                var table = engine.OpenXTablePortable<Guid, HousingLog>("Record");
                list = table.Where(o => o.Value.时间 >= st && o.Value.时间 <= et).OrderByDescending(o => o.Value.时间).Select(o => o.Value).ToList();
            }
            //if (!root)
            //{
                list = list.Join(db.Value.Housing_Member.Where(x => x.DepartmentId != Guid.Empty), o => o.用户ID, o => o.UserId, (a, b) => a).ToList();
            //}
            grid.DataSource = list;
        }
    }
}
