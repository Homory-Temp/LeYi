using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homory.Model;

public partial class VIP_查询 : Homory.Model.HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadInfo();
    }

    protected void timer_Tick(object sender, EventArgs e)
    {
        LoadInfo();
    }

    protected void LoadInfo()
    {
        info.InnerHtml = string.Format("剩余<span style='color: red;'>{0}</span>条机构用户关系未处理！（{1}）", HomoryContext.Value.机构用户关系.Count(o => o.State < 3), DateTime.Now.ToString("HH:mm:ss"));
    }
}
