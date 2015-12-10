using Models;
using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotQuery_Object : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var value = "ObjectId".Query();
            if (value.None())
            {
                Response.Redirect("~/Depot/Home");
                return;
            }
            else
            {
                var objId = value.GlobalId();
                var obj = DataContext.DepotObject.Single(o => o.Id == objId);
                name.InnerText = obj.Name;
                sp.InnerText = obj.Specification;
                note.InnerText = obj.Note;
                brand.InnerText = obj.Brand;
                var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
                var noOut = query.Count() > 0 ? query.Sum(o => o.Amount - o.ReturnedAmount) : 0;
                if (Depot.Featured(DepotType.幼儿园))
                {
                    var di = obj.DepotInX.OrderByDescending(o => o.AutoId).FirstOrDefault();
                    if (di != null)
                    {
                        age.InnerText = di.Age;
                    }
                    xRow.Visible = true;
                }
                else
                {
                    xRow.Visible = false;
                }
                pa.Src = obj.ImageA.None() ? "../Content/Images/Transparent.png" : obj.ImageA;
                da.Visible = !obj.ImageA.None();
                pb.Src = obj.ImageB.None() ? "../Content/Images/Transparent.png" : obj.ImageB;
                db.Visible = !obj.ImageB.None();
                pc.Src = obj.ImageC.None() ? "../Content/Images/Transparent.png" : obj.ImageC;
                dc.Visible = !obj.ImageC.None();
                pd.Src = obj.ImageD.None() ? "../Content/Images/Transparent.png" : obj.ImageD;
                dd.Visible = !obj.ImageD.None();
            }
        }
    }

    public class Placed
    {
        public Guid Id { get; set; }
        public bool Fixed { get; set; }
        public int Ordinal { get; set; }
        public string Place { get; set; }
        public string Code { get; set; }
    }

    protected void grid_BatchEditCommand(object sender, GridBatchEditingEventArgs e)
    {
        var value = "ObjectId".Query();
        if (value.None())
        {
            Response.Redirect("~/Depot/Home");
            return;
        }
        var objId = value.GlobalId();
        var obj = DataContext.DepotObject.Single(o => o.Id == objId);
        foreach (var command in e.Commands)
        {
            var values = command.NewValues;
            var place = values["Place"].ToString();
            switch (command.Type)
            {
                case GridBatchEditingCommandType.Update:
                    {
                        var id = values["Id"].ToString().GlobalId();
                        var @fixed = bool.Parse(values["Fixed"].ToString());
                        var @in = DataContext.DepotInX.SingleOrDefault(o => o.Id == id);
                        @in.Place = place;
                        if (!obj.Single)
                        {
                            @in.DepotIn.Place = @in.Place;
                        }
                        DataContext.SaveChanges();
                    }
                    break;
            }
        }
        DataContext.SaveChanges();
        view.Rebind();
    }

    protected void do_up_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotAction/ObjectImage?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, "ObjectId".Query()));
    }

    protected void back_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("~/DepotScan/Object?DepotId={0}&ObjectId={1}".Formatted(Depot.Id, "ObjectId".Query()));
    }

    protected void view_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
    {
        var value = "ObjectId".Query();
        if (value.None())
        {
            Response.Redirect("~/Depot/Home");
            return;
        }
        var objId = value.GlobalId();
        var obj = DataContext.DepotObject.Single(o => o.Id == objId);
        if (obj.Fixed)
        {
            var source = DataContext.DepotInX.Where(o => (o.AvailableAmount > 0) && o.ObjectId == obj.Id).ToList().Select(o => new Placed { Id = o.Id, Ordinal = o.Ordinal, Fixed = true, Place = o.Place, Code = o.Code }).OrderBy(o => o.Ordinal).ToList();
            if (obj.Single)
            {
                source = source.Where(o => o.Code == "Code".Query()).ToList();
            }
            view.DataSource = source;
        }
        else
        {
            var source = DataContext.DepotInX.Where(o => (o.AvailableAmount > 0) && o.ObjectId == obj.Id).OrderByDescending(o => o.AvailableAmount).ToList().Select(o => new Placed { Id = o.Id, Ordinal = 0, Fixed = false, Place = o.Place, Code = o.Code }).ToList();
            for (var i = 0; i < source.Count; i++)
            {
                source[i].Ordinal = i + 1;
            }
            if (obj.Single)
            {
                source = source.Where(o => o.Code == "Code".Query()).ToList();
            }
            view.DataSource = source;
        }
    }

    protected void save_ServerClick(object sender, EventArgs e)
    {
        var id = (sender as HtmlInputControl).Attributes["match"].GlobalId();
        var value = "ObjectId".Query();
        var @in = DataContext.DepotInX.Single(o => o.Id == id);
        var objId = value.GlobalId();
        var obj = DataContext.DepotObject.Single(o => o.Id == objId);
        var place = ((sender as HtmlInputControl).NamingContainer.FindControl("place") as RadTextBox).Text;
        @in.Place = place;
        var ___px = new DepotPlace
        {
            Code = @in.Code,
            Place = place,
            Time = DateTime.Now
        };
        DataContext.DepotPlace.Add(___px);
        if (!obj.Single)
        {
            @in.DepotIn.Place = @in.Place;
        }
        DataContext.SaveChanges();
    }
}
