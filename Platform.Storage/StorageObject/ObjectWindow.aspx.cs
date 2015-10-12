using Models;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;

public partial class ObjectWindow : StoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var obj = db.Value.StorageObjectGetOne("Id".Query().GlobalId());
            new Control[] { ordinal, name, fixedSerial, low, high, unit, specification, note, code }.InitialValue(obj.Ordinal.ToString(), obj.Name, obj.FixedSerial, obj.Low.ToString(), obj.High.ToString(), obj.Unit, obj.Specification, obj.Note, obj.Code);
            fixedSerial.Visible = obj.Fixed;
            ObjectImage.ImageJson = obj.Image;
        }
    }

    private bool _v = true;

    protected bool PlaceVisible
    {
        get
        {
            return _v;
        }
        set
        {
            grid.Visible = value;
            _v = value;
        }
    }

    protected void cancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        ap.Script("cancel();");
    }

    public class Placed
    {
        public Guid Id { get; set; }
        public bool Fixed { get; set; }
        public int Ordinal { get; set; }
        public string Place { get; set; }
    }

    protected void grid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        var obj = db.Value.StorageObjectGetOne("Id".Query().GlobalId());
        if (obj.Fixed)
        {
            var source = db.Value.StorageInSingle.Where(o => (o.In == true || o.Lend == true) && o.ObjectId == obj.Id).ToList().Select(o => new Placed { Id = o.Id, Ordinal = o.InOrdinal, Fixed = true, Place = o.Place }).OrderBy(o => o.Ordinal).ToList();
            grid.DataSource = source;
        }
        else
        {
            var source = db.Value.StorageIn.Where(o => (o.InAmount > 0 || o.LendAmount > 0) && o.ObjectId == obj.Id).OrderBy(o => o.Time).ToList().Select(o => new Placed { Id = o.Id, Ordinal = 0, Fixed = false, Place = o.Place }).ToList();
            for (var i = 0; i < source.Count; i++)
            {
                source[i].Ordinal = i + 1;
            }
            grid.DataSource = source;
        }
    }

    protected void grid_BatchEditCommand(object sender, Telerik.Web.UI.GridBatchEditingEventArgs e)
    {
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
                        if (@fixed)
                        {
                            var @single = db.Value.StorageInSingle.Single(o => o.Id == id);
                            @single.Place = place;
                            var sp = new StoragePlace
                            {
                                Id = db.Value.GlobalId(),
                                IsSingle = true,
                                InId = @single.Id,
                                Place = place,
                                TimeNode = int.Parse(DateTime.Today.ToString("yyyyMMdd")),
                                Time = DateTime.Now,
                                UserId = CurrentUser
                            };
                            db.Value.StoragePlace.Add(sp);
                        }
                        else
                        {
                            var @in = db.Value.StorageIn.Single(o => o.Id == id);
                            @in.Place = place;
                            var sp = new StoragePlace
                            {
                                Id = db.Value.GlobalId(),
                                IsSingle = false,
                                InId = @in.Id,
                                Place = place,
                                TimeNode = int.Parse(DateTime.Today.ToString("yyyyMMdd")),
                                Time = DateTime.Now,
                                UserId = CurrentUser
                            };
                            db.Value.StoragePlace.Add(sp);
                        }
                    }
                    break;
            }
        }
        db.Value.StorageSave();
        grid.Rebind();
    }
}
