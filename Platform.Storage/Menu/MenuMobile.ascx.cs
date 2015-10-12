using System;

public partial class Menu : System.Web.UI.UserControl
{
    protected override void OnLoad(EventArgs e)
    {
        if ("StorageId".Query().Null())
        {
            Response.Redirect("~/Storage/Storage");
            return;
        }
        base.OnLoad(e);
    }

    protected Guid StorageId
    {
        get { return "StorageId".Query().GlobalId(); }
    }
}
