﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DepotQuery_ObjectFixedMove : DepotPageSingle
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
                unit.InnerText = unitx.InnerText = obj.Unit;
                sp.InnerText = obj.Specification;
                no.InnerText = CountDone(obj).ToAmount(Depot.Featured(DepotType.小数数量库));
                note.InnerText = obj.Note;
                brand.InnerText = obj.Brand;
                var query = obj.DepotUseX.Where(o => o.ReturnedAmount < o.Amount);
                var noOut = query.Count() > 0 ? query.Where(o => o.Type == UseType.借用).Sum(o => o.Amount - o.ReturnedAmount) : 0;
                var isVirtual = Depot.Featured(DepotType.固定资产库);
                var cid_x = DataContext.DepotObjectCatalog.FirstOrDefault(o => o.ObjectId == objId && o.IsLeaf == true && o.IsVirtual == isVirtual);
                cn.InnerText = "固定资产";
                total.InnerText = (obj.Amount + noOut).ToAmount(Depot.Featured(DepotType.小数数量库));
                if (Depot.Featured(DepotType.幼儿园))
                {
                    age.InnerText = obj.Age;
                    fk1.ColSpan = 1;
                    fk2.Visible = fk3.Visible = true;
                }
                else
                {
                    age.InnerText = "";
                    fk1.ColSpan = 3;
                    fk2.Visible = fk3.Visible = false;
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

    private IEnumerable<DepotIn> ININ;

    protected IEnumerable<DepotIn> XININ
    {
        get
        {
            if (ININ == null)
                ININ = DataContext.DepotObjectInLoad(Depot.Id, null, true);
            return ININ;
        }
    }

    protected decimal CountDone(DepotObject obj)
    {
        var query = obj.DepotIn.ToList();
        var amount = 0M;
        foreach (var g in query.GroupBy(o => o.Note))
        {
            amount += XININ.Where(o => o.Note == g.Key).Sum(o => o.Amount);
        }
        return (amount);
    }

    public class Placed
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
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
        var source = DataContext.DepotIn.Where(o => /*(o.AvailableAmount > 0) &&*/ o.ObjectId == obj.Id).ToList().Select(o => new Placed { Time = o.Time, Id = o.Id, Number = o.Note, Amount = o.Amount, Price = o.Price, Total = o.Total }).OrderByDescending(o => o.Time).ToList();
        grid.DataSource = source;
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

    protected void search_ServerClick(object sender, EventArgs e)
    {
        var url = "../DepotAction/ObjectFixed?DepotId={0}&Search={1}".Formatted(Depot.Id, Server.UrlEncode(toSearch.Text.Trim()));
        Response.Redirect(url);
    }
}
