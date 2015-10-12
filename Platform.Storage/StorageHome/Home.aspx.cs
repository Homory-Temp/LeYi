using Models;
using STSdb4.Database;
using System;
using System.Linq;

public partial class Home : SingleStoragePage
{
    protected bool Can(Guid id)
    {
        return db.Value.Database.SqlQuery<int>("SELECT COUNT(*) FROM 仓库权限表 WHERE StorageId = '{0}' AND UserId = '{1}'".Formatted(id, CurrentUser)).Single() > 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            targetCount.Text = db.Value.StorageTargetGet(StorageId).Count().ToString();
            targetCount.NavigateUrl = "~/StorageTarget/Target?StorageId={0}".Formatted(StorageId);
            outCount.NavigateUrl = "~/StorageOut/ToOut?StorageId={0}".Formatted(StorageId);
            //switcher.SourceBind(db.Value.StorageGet().OrderBy(o => o.Ordinal).ToList());
            //switcher.SelectedIndex = switcher.FindItemIndexByValue("StorageId".Query());

            var ss = db.Value.StorageGet().OrderBy(o => o.Ordinal).ToList();
            switcher.SourceBind(ss.Where(o => Can(o.Id)).ToList());
            switcher.SelectedIndex = switcher.FindItemIndexByValue("StorageId".Query());

            var s = db.Value.StorageGet(StorageId);
            a.Text = s.StorageCatalog.Count(o => o.State == Models.State.启用).ToString();
            b.Text = s.StorageObject.Count(o => o.State == Models.State.启用).ToString();
            c.Text = s.StorageFlow.Count().ToString();
            try
            {
                d1.Text = s.StorageObject.Where(o => o.State < Models.State.删除).Select(o => o.StorageIn.Sum(p => p.InAmount)).Sum().ToString();
            }
            catch { d1.Text = "0"; }
            try
            {
                d2.Text = s.StorageObject.Where(o => o.State < Models.State.删除).Select(o => o.StorageIn.Sum(p => p.InMoney)).Sum().Money().ToString();
            }
            catch { d2.Text = "0"; }
            var consume = db.Value.StorageFlow.Where(o => o.StorageId == StorageId).Where(o => o.Type == (Models.FlowType.领用 | Models.FlowType.出库));
            try
            {
                d3.Text = consume.Sum(o => o.Amount).ToString();
            }
            catch { d3.Text = "0"; }
            try
            {
                d4.Text = consume.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { d4.Text = "0"; }
            var lend = db.Value.StorageFlow.Where(o => o.StorageId == StorageId).Where(o => o.Type == (Models.FlowType.借用 | Models.FlowType.出库));
            var @return = db.Value.StorageFlow.Where(o => o.StorageId == StorageId).Where(o => o.Type == (Models.FlowType.归还 | Models.FlowType.入库));
            try
            {
                d5.Text = (lend.Sum(o => o.Amount) - @return.Sum(o => o.Amount)).ToString();
            }
            catch { d5.Text = "0"; }
            try
            {
                d6.Text = (lend.Sum(o => o.AdditionalFee + o.TotalPrice) - @return.Sum(o => o.AdditionalFee + o.TotalPrice)).Money().ToString();
            }
            catch { d6.Text = "0"; }
            var @out = db.Value.StorageFlow.Where(o => o.StorageId == StorageId).Where(o => o.Type == (Models.FlowType.报废 | Models.FlowType.出库));
            try
            {
                d7.Text = @out.Sum(o => o.Amount).ToString();
            }
            catch { d7.Text = "0"; }
            try
            {
                d8.Text = @out.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { d8.Text = "0"; }
            var today = DateTime.Today;
            var ms = int.Parse(new DateTime(today.Year, today.Month, 1).AddDays(-1).ToString("yyyyMMdd"));
            var me = int.Parse(new DateTime(today.Year, today.Month + 1, 1).ToString("yyyyMMdd"));
            var m_in = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ms && o.TimeNode < me).Where(o => o.Type == (Models.FlowType.购置 | Models.FlowType.入库));
            try
            {
                f1.Text = m_in.Sum(o => o.Amount).ToString();
            }
            catch { f1.Text = "0"; }
            try
            {
                f2.Text = m_in.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { f2.Text = "0"; }
            var m_consume = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ms && o.TimeNode < me).Where(o => o.Type == (Models.FlowType.领用 | Models.FlowType.出库));
            try
            {
                f3.Text = m_consume.Sum(o => o.Amount).ToString();
            }
            catch { f3.Text = "0"; }
            try
            {
                f4.Text = m_consume.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { f4.Text = "0"; }
            var m_lend = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ms && o.TimeNode < me).Where(o => o.Type == (Models.FlowType.借用 | Models.FlowType.出库));
            var @m_return = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ms && o.TimeNode < me).Where(o => o.Type == (Models.FlowType.归还 | Models.FlowType.入库));
            try
            {
                f5.Text = (m_lend.Sum(o => o.Amount) - @m_return.Sum(o => o.Amount)).ToString();
            }
            catch { f5.Text = "0"; }
            try
            {
                f6.Text = (m_lend.Sum(o => o.AdditionalFee + o.TotalPrice) - @m_return.Sum(o => o.AdditionalFee + o.TotalPrice)).Money().ToString();
            }
            catch { f6.Text = "0"; }
            var @m_out = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ms && o.TimeNode < me).Where(o => o.Type == (Models.FlowType.报废 | Models.FlowType.出库));
            try
            {
                f7.Text = @m_out.Sum(o => o.Amount).ToString();
            }
            catch { f7.Text = "0"; }
            try
            {
                f8.Text = @m_out.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { f8.Text = "0"; }
            var ys = int.Parse(new DateTime(today.Year, 1, 1).AddDays(-1).ToString("yyyyMMdd"));
            var ye = int.Parse(new DateTime(today.Year, 1, 1).AddYears(1).ToString("yyyyMMdd"));
            var y_in = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ys && o.TimeNode < ye).Where(o => o.Type == (Models.FlowType.购置 | Models.FlowType.入库));
            try
            {
                g1.Text = y_in.Sum(o => o.Amount).ToString();
            }
            catch { g1.Text = "0"; }
            try
            {
                g2.Text = y_in.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { g2.Text = "0"; }
            var y_consume = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ys && o.TimeNode < ye).Where(o => o.Type == (Models.FlowType.领用 | Models.FlowType.出库));
            try
            {
                g3.Text = y_consume.Sum(o => o.Amount).ToString();
            }
            catch { g3.Text = "0"; }
            try
            {
                g4.Text = y_consume.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { g4.Text = "0"; }
            var y_lend = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ys && o.TimeNode < ye).Where(o => o.Type == (Models.FlowType.借用 | Models.FlowType.出库));
            var @y_return = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ys && o.TimeNode < ye).Where(o => o.Type == (Models.FlowType.归还 | Models.FlowType.入库));
            try
            {
                g5.Text = (m_lend.Sum(o => o.Amount) - @m_return.Sum(o => o.Amount)).ToString();
            }
            catch { g5.Text = "0"; }
            try
            {
                g6.Text = (m_lend.Sum(o => o.AdditionalFee + o.TotalPrice) - @m_return.Sum(o => o.AdditionalFee + o.TotalPrice)).Money().ToString();
            }
            catch { g6.Text = "0"; }
            var @y_out = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ys && o.TimeNode < ye).Where(o => o.Type == (Models.FlowType.报废 | Models.FlowType.出库));
            try
            {
                g7.Text = @m_out.Sum(o => o.Amount).ToString();
            }
            catch { g7.Text = "0"; }
            try
            {
                g8.Text = @m_out.Sum(o => o.TotalPrice + o.AdditionalFee).Money().ToString();
            }
            catch { g8.Text = "0"; }
            try
            {
                d9.Text = db.Value.StorageFlow.Where(o => o.StorageId == StorageId).Where(o => o.Type == FlowType.调整).Sum(o => o.AdditionalFee).Money();
            }
            catch
            {
                d9.Text = "0";
            }
            try
            {
                f9.Text = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ms && o.TimeNode < me).Where(o => o.Type == FlowType.调整).Sum(o => o.AdditionalFee).Money();
            }
            catch
            {
                f9.Text = "0";
            }
            try
            {
                g9.Text = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ys && o.TimeNode < ye).Where(o => o.Type == FlowType.调整).Sum(o => o.AdditionalFee).Money();
            }
            catch
            {
                g9.Text = "0";
            }
            try
            {
                dx.Text = db.Value.StorageFlow.Where(o => o.StorageId == StorageId).Where(o => o.Type == FlowType.调整).Sum(o => o.Amount).ToString();
            }
            catch
            {
                dx.Text = "0";
            }
            try
            {
                fx.Text = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ms && o.TimeNode < me).Where(o => o.Type == FlowType.调整).Sum(o => o.Amount).ToString();
            }
            catch
            {
                fx.Text = "0";
            }
            try
            {
                gx.Text = db.Value.StorageFlow.Where(o => o.StorageId == StorageId && o.TimeNode > ys && o.TimeNode < ye).Where(o => o.Type == FlowType.调整).Sum(o => o.Amount).ToString();
            }
            catch
            {
                gx.Text = "0";
            }
        }
    }

    protected void target_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        target.Source(db.Value.StorageTargetGet(StorageId).OrderBy(o => o.TimeNode).ToList());
    }

    protected void in_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageObject/Object?StorageId={0}&TargetId={1}".Formatted(StorageId, sender.ButtonArgs().GlobalId()));
    }

    protected void save_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        Response.Redirect("~/StorageTarget/Target?StorageId={0}&TargetId={1}".Formatted(StorageId, sender.ButtonArgs().GlobalId()));
    }

    protected void warn_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        warn.Source(db.Value.StorageObjectGet(StorageId).Where(o => (o.Low > 0 && o.InAmount < o.Low) || (o.High > 0 && o.InAmount > o.High)).OrderBy(o => o.TimeNode).ToList());
    }

    protected void switcher_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (e.Value.Null() || e.Value.ToLower() == "StorageId".Query().ToLower())
            return;
        Response.Redirect("~/StorageHome/Home?StorageId={0}".Formatted(switcher.SelectedValue));
    }

    protected void viewOut_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        IStorageEngine engine = STSdb.FromFile(Server.MapPath("~/StorageOut/Out.table"));
        try
        {
            var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
            var s = table.OrderBy(o => o.Value.Time).Select(o => o.Value).ToList().Where(o => db.Value.StorageObjectGet(StorageId).Select(x => x.Id).ToList().Contains(o.ObjectId)).ToList();
            viewOut.Source(s);
            outCount.Text = s.Count.ToString();
        }
        finally
        {
            engine.Close();
        }
    }
}
