using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Aspose.Words.Lists;
using Homory.Model;
using Telerik.Web.UI;
using Resource = Homory.Model.Resource;
using ResourceType = Homory.Model.ResourceType;

namespace Go
{
    public partial class GoCatalog : HomoryResourcePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                course.DataSource =
                    HomoryContext.Value.Catalog.Where(o => o.Type == CatalogType.课程 && o.State < State.审核 && o.ParentId == null)
                        .OrderBy(o => o.State)
                        .ThenBy(o => o.Ordinal).ToList();
                course.DataBind();
                var week = DateTime.Today.AddDays(1 - (int)DateTime.Today.DayOfWeek);
                count1.InnerText = HomoryContext.Value.Resource.Count(o => o.State < State.审核).ToString();
                count2.InnerText = HomoryContext.Value.Resource.Count(o => o.State < State.审核 && o.Time > week).ToString();
                latest.DataSource =
                    HomoryContext.Value.Resource.Where(o => o.State < State.审核).OrderByDescending(o => o.Time).Take(5).ToList();
                latest.DataBind();
                popular.DataSource =
                    HomoryContext.Value.Resource.Where(o => o.State < State.审核).OrderByDescending(o => o.View).Take(5).ToList();
                popular.DataBind();
            }
        }


        protected override bool ShouldOnline
        {
            get { return false; }
        }

        protected void search_go_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect(
                string.Format("Search?Content={0}", Server.UrlEncode(search_content.Value.Trim())), false);
        }
    }
}
