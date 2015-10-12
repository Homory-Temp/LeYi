using System;
using System.Linq;

public partial class HomeMobile : SingleStoragePageMobile
{
    protected bool Can(Guid id)
    {
        return db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}'".Formatted(id, CurrentUser)).Single() > 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //switcher.SourceBind(db.Value.StorageGet().OrderBy(o => o.Ordinal).ToList());
            //switcher.SelectedIndex = switcher.FindItemIndexByValue("StorageId".Query());

            var ss = db.Value.StorageGet().OrderBy(o => o.Ordinal).ToList();
            switcher.SourceBind(ss.Where(o => Can(o.Id)).ToList());
            switcher.SelectedIndex = switcher.FindItemIndexByValue("StorageId".Query());

            cubeLink_a1_cubeNav313.HRef = "../StorageCheck/ToCheck?StorageId={0}".Formatted(StorageId);
            cubeLink_a2_cubeNav313.HRef = "../StorageObject/ObjectMobile?StorageId={0}".Formatted(StorageId);
            cubeLink_a3_cubeNav313.HRef = "../StorageQuery/QueryPersonalwap?StorageId={0}".Formatted(StorageId);
            cubeLink_a4_cubeNav313.HRef = "../StorageScan/ScanQueryMobile?StorageId={0}".Formatted(StorageId);
        

        }
    }

    protected void switcher_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (e.Value.Null() || e.Value.ToLower() == "StorageId".Query().ToLower())
            return;
        Response.Redirect("~/StorageHome/HomeMobile?StorageId={0}".Formatted(switcher.SelectedValue));
    }
}
