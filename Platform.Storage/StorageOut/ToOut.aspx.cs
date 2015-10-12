using Models;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ToOutConfirm : SingleStoragePage
{
    protected void viewM_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
            var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
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
            var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
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
            var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
            viewS.Source(table.Where(o => o.Value.Type == ToOutType.Specific).OrderBy(o => o.Value.Time).Select(o => o.Value).ToList());
        }
        finally
        {
            engine.Close();
        }
    }

    protected void bM_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var n = ((sender as ImageButton).NamingContainer.FindControl("n") as RadNumericTextBox).Value;
        if (!n.HasValue || n.Value == 0)
            return;
        var id = (sender as ImageButton).CommandArgument;
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
            var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
            var x = table[id];
            db.Value.SetOutMDo(x.ObjectId, x.OutUserId, x.OperatorId, x.OutType, x.OutReason, (decimal)n.Value, x.OutNote);
            db.Value.SaveChanges();
            OutDoneTable _done = new OutDoneTable();
            _done.Id = x.Id;
            _done.ObjectId = x.ObjectId;
            _done.OperatorId = x.OperatorId;
            _done.OutAmount = x.OutAmount;
            _done.OutDoneAmount = (decimal)n.Value;
            _done.OutNote = x.OutNote;
            _done.OutOrdinals = x.OutOrdinals;
            _done.OutDoneOrdinals = x.OutOrdinals;
            _done.OutReason = x.OutReason;
            _done.OutType = x.OutType;
            _done.OutUserId = x.OutUserId;
            _done.Time = x.Time;
            _done.Type = x.Type;
            _done.AuditUserId = CurrentUser;
                table.Delete(id);
                engine.Commit();
                db.Value.SetOutDone(_done);
        }
        finally
        {
            engine.Close();
        }
        viewM.Rebind();
    }

    protected void bR_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var n = ((sender as ImageButton).NamingContainer.FindControl("n") as RadNumericTextBox).Value;
        if (!n.HasValue || n.Value == 0)
            return;
        var id = (sender as ImageButton).CommandArgument;
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
            var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
            var x = table[id];
            db.Value.SetOutRandomDo(x.ObjectId, x.OutUserId, x.OperatorId, x.OutType, x.OutReason, (int)n.Value, x.OutNote);
            db.Value.SaveChanges();
            OutDoneTable _done = new OutDoneTable();
            _done.Id = x.Id;
            _done.ObjectId = x.ObjectId;
            _done.OperatorId = x.OperatorId;
            _done.OutAmount = x.OutAmount;
            _done.OutDoneAmount = (decimal)n.Value;
            _done.OutNote = x.OutNote;
            _done.OutOrdinals = x.OutOrdinals;
            _done.OutDoneOrdinals = x.OutOrdinals;
            _done.OutReason = x.OutReason;
            _done.OutType = x.OutType;
            _done.OutUserId = x.OutUserId;
            _done.Time = x.Time;
            _done.Type = x.Type;
            _done.AuditUserId = CurrentUser;
                table.Delete(id);
                engine.Commit();
            db.Value.SetOutDone(_done);
        }
        finally
        {
            engine.Close();
        }
        viewR.Rebind();
    }

    protected void bS_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var id = (sender as ImageButton).CommandArgument;
        var r = ((sender as ImageButton).NamingContainer.FindControl("r") as Repeater).Items;
        var l = new List<int>();
        foreach (var ri in r)
        {
            var c = (ri as RepeaterItem).FindControl("c") as CheckBox;
            if (c.Checked)
            {
                l.Add(int.Parse(c.Text));
            }
        }
        if (l.Count == 0)
            return;
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
            var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
            var x = table[id];
            db.Value.SetOutSpecificDo(x.ObjectId, x.OutUserId, x.OperatorId, x.OutType, x.OutReason, l, x.OutNote);
            db.Value.SaveChanges();
            OutDoneTable _done = new OutDoneTable();
            _done.Id = x.Id;
            _done.ObjectId = x.ObjectId;
            _done.OperatorId = x.OperatorId;
            _done.OutAmount = x.OutAmount;
            _done.OutDoneAmount = x.OutAmount;
            _done.OutNote = x.OutNote;
            _done.OutOrdinals = x.OutOrdinals;
            _done.OutDoneOrdinals = l;
            _done.OutReason = x.OutReason;
            _done.OutType = x.OutType;
            _done.OutUserId = x.OutUserId;
            _done.Time = x.Time;
            _done.Type = x.Type;
            _done.AuditUserId = CurrentUser;
                table.Delete(id);
                engine.Commit();
            db.Value.SetOutDone(_done);
        }
        finally
        {
            engine.Close();
        }
        viewS.Rebind();
    }
}
