using Models;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class OutDone : SingleStoragePage
{
    protected void viewM_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
            var table = engine.OpenXTablePortable<string, OutDoneTable>("OutDone");
            viewM.Source(table.Where(o => o.Value.Type == ToOutType.Multiple).OrderBy(o => o.Value.Time).Select(o => o.Value).ToList());
        }
        finally
        {
            engine.Close();
        }
    }

    protected void viewR_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
        var table = engine.OpenXTablePortable<string, OutDoneTable>("OutDone");
        viewR.Source(table.Where(o => o.Value.Type == ToOutType.Random).OrderBy(o => o.Value.Time).Select(o => o.Value).ToList());
        }
        finally
        {
        engine.Close();
        }
    }

    protected void viewS_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
            var table = engine.OpenXTablePortable<string, OutDoneTable>("OutDone");
            viewS.Source(table.Where(o => o.Value.Type == ToOutType.Specific).OrderBy(o => o.Value.Time).Select(o => o.Value).ToList());
        }
        finally
        {
            engine.Close();
        }
    }
}
