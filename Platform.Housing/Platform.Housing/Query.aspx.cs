using PanGu;
using PanGu.Setting;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Query : SsoPage
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

    protected void back_Click(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("Grid.aspx?OnlineId={0}", Request.QueryString["OnlineId"]));
    }

    protected void query_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(basic.Text) || !string.IsNullOrEmpty(extended.Text))
            {
                var id = Guid.Parse(combo.SelectedValue).ToString().ToUpper();
                using (IStorageEngine engine = STSdb.FromFile(Server.MapPath(string.Format("App_Data/Housing_{0}.record.queryx", id))))
                {
                    var table = engine.OpenXTablePortable<Guid, HousingLog>("Record");
                    table[Guid.NewGuid()] = new HousingLog { 时间 = DateTime.Now, 用户ID = (Guid)Session["MemberId"], 用户姓名 = Session["MemberName"].ToString(), 学生信息查询内容 = basic.Text, 地址信息查询内容 = extended.Text };
                    engine.Commit();
                }
            }
        }
        catch
        {
        }
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
            var list = new List<HousingRecordX>();
            using (IStorageEngine engine = STSdb.FromFile(Server.MapPath(string.Format("App_Data/Housing_{0}.record", id))))
            {
                var table = engine.OpenXTablePortable<HousingKey, HousingValue>("Record");
                list = table.Where(o => o.Key.学校 == gid).Select(o => new HousingRecordX(o.Key, o.Value)).ToList();
            }
            if (!string.IsNullOrEmpty(basic.Text))
            {
                list = list.Where(o => o.姓名.ToLower().Contains(basic.Text.ToLower()) || o.身份证号.ToLower().Contains(basic.Text.ToLower()) || o.户籍.ToLower().Contains(basic.Text.ToLower()) || o.入学年份.ToLower().Contains(basic.Text.ToLower()) || o.班号.ToLower().Contains(basic.Text.ToLower()) || o.备注.ToLower().Contains(basic.Text.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(extended.Text))
            {
                var segment = new Segment();
                PanGuSettings.Load(Server.MapPath("Config.xml"));
                var words = segment.DoSegment(extended.Text, PanGuSettings.Config.MatchOptions, PanGuSettings.Config.Parameters);
                list.ForEach(o => { o.匹配度 = match_words(o.住址, words, segment); });
                list = list.Where(o => o.匹配度 > 0).OrderByDescending(o => o.匹配度).ToList();
            }
            grid.DataSource = list;
        }
    }

    protected int match_words(string toMatch, ICollection<WordInfo> list, Segment segment)
    {
        int result = 0;
        var words = segment.DoSegment(toMatch);
        var matches = words.Select(o => o.Word).Distinct();
        foreach (var match in matches)
        {
            foreach (var compair in list)
            {
                if (compair.Word == match)
                {
                    result += compair.Rank;
                    continue;
                }
            }
        }
        matches = words.Select(o => o.Word);
        foreach (var match in matches)
        {
            foreach (var compair in list)
            {
                if (compair.Word == match)
                {
                    result += compair.Rank;
                    continue;
                }
            }
        }
        return result;
    }
}
