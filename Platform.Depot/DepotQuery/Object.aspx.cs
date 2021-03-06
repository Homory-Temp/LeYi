﻿using Models;
using System;
using System.Collections.Generic;
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
            grid.Columns[5].Visible = Depot.Featured(DepotType.固定资产库);
            grid.Columns[grid.Columns.Count - 1].Visible = !Depot.Featured(DepotType.固定资产库);
            grid.Columns[grid.Columns.Count - 2].Visible = !Depot.Featured(DepotType.固定资产库);
            grid.Columns[grid.Columns.Count - 3].Visible = !Depot.Featured(DepotType.固定资产库);
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
                var isVirtual = Depot.Featured(DepotType.固定资产库);
                var cid_x = DataContext.DepotObjectCatalog.FirstOrDefault(o => o.ObjectId == objId && o.IsLeaf == true && o.IsVirtual == isVirtual);
                if (Depot.Id == Guid.Parse("42bce587-8cc2-4bc3-9ac7-08d30ffd8584"))
                    cn.InnerText = cid_x == null ? "" : DataContext.ToCatalog(cid_x.CatalogId, cid_x.Level).Single().Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[0];
                else
                    cn.InnerText = cid_x == null ? "" : DataContext.ToCatalog(cid_x.CatalogId, cid_x.Level).Single();
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

    public class Placed
    {
        public Guid Id { get; set; }
        public bool Fixed { get; set; }
        public int Ordinal { get; set; }
        public string Place { get; set; }
        public string Code { get; set; }
        public string Number { get; set; }
        public DateTime Time { get; set; }
        public string State { get; set; }
        public DateTime StateTime { get; set; }
        public decimal Amount { get; set; }
        public Guid OId { get; set; }
    }

    public class PlacedAll : Placed
    {
        public string NumberX { get; set; }
        public decimal PriceX { get; set; }
        public decimal TotalX { get; set; }
        public Guid OId { get; set; }

        public PlacedAll(Placed p)
        {
            Id = p.Id;
            Fixed = p.Fixed;
            Ordinal = p.Ordinal;
            Place = p.Place;
            Code = p.Code;
            Number = p.Number;
            Time = p.Time;
            State = p.State;
            StateTime = p.StateTime;
            OId = p.OId;
            Amount = p.Amount;
        }
    }

    public class PlacedX
    {
        public Guid IdX { get; set; }
        public string NumberX { get; set; }
        public DateTime TimeX { get; set; }
        public decimal PriceX { get; set; }
        public decimal TotalX { get; set; }
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
        var source = new List<Placed>();
        if (obj.Fixed)
        {
            source = DataContext.DepotInX.Where(o => /*(o.AvailableAmount > 0) &&*/ o.ObjectId == obj.Id).ToList().Select(o => new Placed { OId = o.DepotIn.Id, Amount = o.Amount, Time = o.DepotIn.Time, Id = o.Id, Number = o.DepotIn.Note, StateTime = (DataContext.DepotToOut.SingleOrDefault(ox => ox.State == 1 && ox.Code == o.Code) == null ? o.DepotIn.Time : DataContext.DepotToOut.Single(ox => ox.State == 1 && ox.Code == o.Code).Time), State = (DataContext.DepotToOut.SingleOrDefault(ox => ox.State == 1 && ox.Code == o.Code) == null ? "使用中" : "已报废"), Ordinal = o.Ordinal, Fixed = true, Place = o.Place, Code = o.Code }).OrderBy(o => o.Ordinal).ToList();
        }
        else
        {
            source = DataContext.DepotInX.Where(o => /*(o.AvailableAmount > 0) &&*/ o.ObjectId == obj.Id).OrderByDescending(o => o.AvailableAmount).ToList().Select(o => new Placed { OId = o.DepotIn.Id, Amount = o.Amount, Id = o.Id, Number = o.DepotIn.Note, State = "使用中", Time = o.DepotIn.Time, Ordinal = 0, Fixed = false, Place = o.Place, Code = o.Code }).ToList();
            for (var i = 0; i < source.Count; i++)
            {
                source[i].Ordinal = i + 1;
            }
        }
        var sourcex = DataContext.DepotIn.Where(o => /*(o.AvailableAmount > 0) &&*/ o.ObjectId == obj.Id).ToList().Select(o => new PlacedX { TimeX = o.Time, IdX = o.Id, NumberX = o.Note, PriceX = o.Price, TotalX = o.Total }).OrderBy(o => o.TimeX).ToList();
        grid.DataSource = Gen(source, sourcex);
    }

    protected List<PlacedAll> Gen(List<Placed> a, List<PlacedX> b)
    {
        var l = a.Select(o => new PlacedAll(o)).ToList();
        l.ForEach(o =>
        {
            var t = b.FirstOrDefault(x => x.IdX == o.OId);
            if (t != null)
            {
                o.NumberX = t.NumberX;
                o.PriceX = t.PriceX;
                o.TotalX = t.PriceX * o.Amount;
            }
        });
        return l;
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
            var numberx = values["NumberX"];
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
                        if (!number.None())
                        {
                            var @inx = DataContext.DepotIn.SingleOrDefault(o => o.Id == id);
                            @inx.Note = numberx.ToString();
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
        gridX.DataSource = DataContext.DepotUseXRecord.Where(o => o.ObjectId == id && o.Amount > o.ReturnedAmount && o.Type == 2).OrderByDescending(o => o.Time).ToList();
    }

    protected void search_ServerClick(object sender, EventArgs e)
    {
        var url = "../DepotAction/Object?DepotId={0}&Search={1}".Formatted(Depot.Id, Server.UrlEncode(toSearch.Text.Trim()));
        Response.Redirect(url);
    }
}
