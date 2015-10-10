using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;

public partial class Go_ViewRate : HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
	    try
	    {
		    var id = Guid.Parse(Request.QueryString[0]);
		    var items =
			    HomoryContext.Value.Action.Where(o => o.State == State.启用 && o.Type == ActionType.用户评分资源 && o.Id2 == id).OrderByDescending(o => o.Time).ToList();
		    grid.DataSource = items;
			grid.DataBind();
	    }
	    catch (Exception)
	    {
		    Response.Write("暂无评分");
		    throw;
	    }
    }


	protected User U(object id)
	{
		var gid = Guid.Parse(id.ToString());
		return HomoryContext.Value.User.Single(o => o.Id == gid);
	}

}