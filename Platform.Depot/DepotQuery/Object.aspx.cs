using Models;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotQuery_Object : DepotPageSingle
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grid.Columns[1].Visible = Depot.Featured(DepotType.固定资产库);
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
                unit.InnerText = unitx.InnerText = obj.Unit;
                sp.InnerText = obj.Specification;
                no.InnerText = obj.Amount.ToAmount(Depot.Featured(DepotType.小数数量库));
                note.InnerText = obj.Note;
                brand.InnerText = obj.Brand;
                var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
                var noOut = query.Count() > 0 ? query.Where(o => o.Type == UseType.借用).Sum(o => o.Amount - o.ReturnedAmount) : 0;
                total.InnerText = (obj.Amount + noOut).ToAmount(Depot.Featured(DepotType.小数数量库));
                if (Depot.Featured(DepotType.幼儿园))
                {
                    age.InnerText = obj.Age;
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
        public string Number { get; set; }
        public DateTime Time { get; set; }
    }

    protected void grid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
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
            var source = DataContext.DepotInX.Where(o => /*(o.AvailableAmount > 0) &&*/ o.ObjectId == obj.Id).ToList().Select(o => new Placed { Id = o.Id, Number = o.DepotIn.Note, Time = o.DepotIn.Time, Ordinal = o.Ordinal, Fixed = true, Place = o.Place, Code = o.Code }).OrderBy(o => o.Ordinal).ToList();
            grid.DataSource = source;
        }
        else
        {
            var source = DataContext.DepotInX.Where(o => /*(o.AvailableAmount > 0) &&*/ o.ObjectId == obj.Id).OrderByDescending(o => o.AvailableAmount).ToList().Select(o => new Placed { Id = o.Id, Number = o.DepotIn.Note, Time = o.DepotIn.Time, Ordinal = 0, Fixed = false, Place = o.Place, Code = o.Code }).ToList();
            for (var i = 0; i < source.Count; i++)
            {
                source[i].Ordinal = i + 1;
            }
            grid.DataSource = source;
        }
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
            var number = values["Number"];
            if (number.None())
                continue;
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
                            @in.DepotIn.Note = number.ToString();
                        }
                        DataContext.SaveChanges();
                    }
                    break;
            }
        }
        DataContext.SaveChanges();
        grid.Rebind();
    }

    protected void gridX_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        var id = "ObjectId".Query().GlobalId();
        gridX.DataSource = DataContext.DepotUseXRecord.Where(o => o.ObjectId == id).OrderByDescending(o => o.Time).ToList();
    }
}
