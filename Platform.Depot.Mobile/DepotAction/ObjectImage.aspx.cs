﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class DepotAction_ObjectImage : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var oid = "ObjectId".Query().GlobalId();
            var obj = DataContext.DepotObject.Single(o => o.Id == oid);
            var cid = "CatalogId".Query();
            var imgs = new string[] { obj.ImageA, obj.ImageB, obj.ImageC, obj.ImageD };
            for (var i = 0; i < imgs.Length; i++)
            {
                if (!imgs[i].None())
                    new[] { p0, p1, p2, p3 }[i].Src = imgs[i];
            }
            var img = new[] { p0, p1, p2, p3 }.ToList();
            var count = img.Where(o => o.Src.Contains("/Content/Images/Transparent.png")).Count();
            upload.InitialFileInputsCount = count == 0 ? 0 : 1;
            clear.Visible = count < 4;
            imgRow.Visible = count < 4;
            upload.MaxFileInputsCount = count == 0 ? 0 : count;
        }
    }

    protected void cancel_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotQuery/Object?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, "ObjectId".Query()));
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        Save();
        Response.Redirect("~/DepotQuery/Object?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, "ObjectId".Query()));
    }

    protected Guid Save()
    {
        var oid = "ObjectId".Query().GlobalId();
        var img = new[] { p0, p1, p2, p3 }.ToList();
        var photo = img.Count(o => !o.Src.Contains("/Content/Images/Transparent.png")) == 0 ? new string[] { } : img.Where(o => !o.Src.Contains("/Content/Images/Transparent.png")).Select(o => o.Src).ToArray();
        DataContext.DepotObjectEditX(oid, photo.Length > 0 ? photo[0] : "", photo.Length > 1 ? photo[1] : "", photo.Length > 2 ? photo[2] : "", photo.Length > 3 ? photo[3] : "");
        return oid;
    }

    protected void upload_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
    {
        var files = upload.UploadedFiles;
        var img = new[] { p0, p1, p2, p3 }.ToList();
        for (var i = 0; i < files.Count; i++)
        {
            for (var j = 0; j < img.Count; j++)
            {
                if (img[j].Src.Contains("/Content/Images/Transparent.png"))
                {
                    var path = "../Common/物资/图片/{0}{1}".Formatted(DataContext.GlobalId(), files[i].GetExtension());
                    files[i].SaveAs(Server.MapPath(path));
                    img[j].Src = path;
                    break;
                }
            }
            var count = img.Where(o => o.Src.Contains("/Content/Images/Transparent.png")).Count();
            upload.InitialFileInputsCount = count == 0 ? 0 : 1;
            clear.Visible = count < 4;
            imgRow.Visible = count < 4;
            upload.MaxFileInputsCount = count == 0 ? 0 : count;
        }
    }

    protected void clear_Click(object sender, EventArgs e)
    {
        var img = new[] { p0, p1, p2, p3 }.ToList();
        img.ForEach(o => o.Src = "../Content/Images/Transparent.png");
        upload.InitialFileInputsCount = 1;
        upload.MaxFileInputsCount = 4;
        clear.Visible = false;
        imgRow.Visible = false;
    }
}
