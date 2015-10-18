using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class StoreQuery_TargetPrint : SingleStorePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void view_record_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var targetId = "TargetId".Query().GlobalId();
        view_record.DataSource = db.Value.Store_RecordIn.Where(o => o.TargetId == targetId).OrderByDescending(o => o.入库日期).ToList();
    }
}
