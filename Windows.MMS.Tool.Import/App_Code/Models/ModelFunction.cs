using Models;
using Newtonsoft.Json;
using STSdb4.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public static class ModelFunction
{
    private static JsonSerializerSettings jsonSetting = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects };

    public static string ToJson(this object entity)
    {
        return JsonConvert.SerializeObject(entity, jsonSetting);
    }

    public static T FromJson<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json, jsonSetting);
    }

    public static string ButtonArgs(this object sender)
    {
        return (sender as IButtonControl).CommandArgument.ToString();
    }

    public static string Formatted(this string format, object @object)
    {
        return string.Format(format, @object);
    }

    public static string Formatted(this string format, params object[] objects)
    {
        return string.Format(format, objects);
    }

    public static Guid GlobalId(this StorageEntity db)
    {
        var timeArray = BitConverter.GetBytes(DateTime.UtcNow.Ticks).Reverse().ToArray();
        var guidArray = Guid.NewGuid().ToByteArray();
        guidArray[0] = 0x87;
        guidArray[1] = 0xe5;
        guidArray[5] = 0x8c;
        for (var i = 2; i < 4; i++)
            guidArray[i] = timeArray[9 - i];
        for (var i = 10; i < 16; i++)
            guidArray[i] = timeArray[i - 10];
        return new Guid(guidArray);
    }

    public static Guid GlobalId(this string id)
    {
        return id.Null() ? Guid.Empty : Guid.Parse(id);
    }

    public static void InitialTree(this RadTreeView tree, int selectedLevel, int expandedLevelCount)
    {
        RadTreeNode node;
        for (var i = selectedLevel; i > -1; i--)
        {
            node = tree.GetAllNodes().SingleOrDefault(o => o.Level == i);
            if (node != null)
            {
                node.Selected = true;
                break;
            }
        }
        tree.GetAllNodes().Where(o => o.Level < expandedLevelCount).ToList().ForEach(o => o.ExpandParentNodes());
    }

    public static void RebindTreeCallback(this RadTreeView tree, object source)
    {
        var selected = tree.SelectedValue;
        var selectedParent = tree.SelectedNode.ParentNode == null ? null : tree.SelectedNode.Value;
        var expanded = tree.GetAllNodes().Where(o => o.Expanded == true).Select(o => o.Value).ToList();
        tree.SourceBind(source);
        tree.GetAllNodes().Where(o => expanded.Contains(o.Value)).ToList().ForEach(o => o.Expanded = true);
        var node = tree.GetAllNodes().SingleOrDefault(o => o.Value == selected);
        if (node == null)
            node = tree.GetAllNodes().SingleOrDefault(o => o.Value == selectedParent);
        if (node == null)
            node = tree.Nodes[0];
        node.Selected = true;
        node.Expanded = true;
        node.ExpandParentNodes();
    }

    public static void InitialValue(this Control[] controls, params dynamic[] values)
    {
        for (var i = 0; i < controls.Length; i++)
        {
            switch (controls[i].GetType().Name)
            {
                case "Label":
                    {
                        (controls[i] as Label).Text = values[i];
                        break;
                    }
                case "RadTextBox":
                    {
                        (controls[i] as RadTextBox).Text = values[i];
                        break;
                    }
                case "RadNumericTextBox":
                    {
                        (controls[i] as RadNumericTextBox).Value = values[i];
                        break;
                    }
                case "RadComboBox":
                    {
                        (controls[i] as RadComboBox).Text = values[i];
                        (controls[i] as RadComboBox).SelectedIndex = (controls[i] as RadComboBox).FindItemIndexByValue(values[i]);
                        break;
                    }
                case "RadSearchBox":
                    {
                        (controls[i] as RadSearchBox).Text = values[i];
                        break;
                    }
            }
        }
    }

    public static bool Null(this object value)
    {
        return value == null || string.IsNullOrWhiteSpace(value.ToString());
    }

    private static readonly char[] chars_a = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    private static readonly char[] chars_n = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    private static readonly char[] chars_h = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
    private static readonly char[] chars_e = chars_a.Except(chars_h).ToArray();
    private static Random r = new Random();

    public static string ToQR(this StorageEntity db, string type, int autoId)
    {
        StringBuilder sb = new StringBuilder(type);
        sb.Append(autoId.ToString("X").PadLeft(11, '0'));
        //sb.Append(autoId.ToString("X"));
        //var c = 12 - sb.Length;
        //for (var i = 0; i < c; i++)
        //{
        //    if (i == 0)
        //        sb.Insert(1, chars_e[r.Next(0, chars_e.Length - 1)]);
        //    else
        //        sb.Insert(1, chars_h[r.Next(0, chars_h.Length - 1)]);
        //}
        return sb.ToString();
    }

    public static string ToQR(this StorageEntity db, string type, int autoId, string name)
    {
        StringBuilder sb = new StringBuilder(type);
        sb.Append(autoId.ToString("X").PadLeft(11, '0'));
        //sb.Append(autoId.ToString("X"));
        //var c = 12 - sb.Length;
        //for (var i = 0; i < c; i++)
        //{
        //    if (i == 0)
        //        sb.Insert(1, chars_e[r.Next(0, chars_e.Length - 1)]);
        //    else
        //        sb.Insert(1, chars_h[r.Next(0, chars_h.Length - 1)]);
        //}
        sb.Append(name);
        return sb.ToString();
    }

    public static void FromQR(this StorageEntity db, string code, out string type, out Guid id)
    {
        type = code.Substring(0, 1);
        var autoId = Convert.ToInt32(code.Substring(1, 11), 16);
        //var autoString = code.Substring(code.LastIndexOfAny(chars_e) + 1);
        //var autoId = Convert.ToInt32(autoString, 16);
        switch (type)
        {
            case "F":
                id = db.StorageCatalog.SingleOrDefault(o => o.AutoId == autoId).Id;
                break;
            case "W":
                id = db.StorageObject.SingleOrDefault(o => o.AutoId == autoId).Id;
                break;
            case "D":
                id = db.StorageInSingle.SingleOrDefault(o => o.AutoId == autoId).Id;
                break;
            default:
                id = Guid.Empty;
                break;
        }
    }

    public static string Query(this string key, bool decode = false)
    {
        return decode ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[key]) : HttpContext.Current.Request.QueryString[key];
    }

    public static void Script(this RadAjaxPanel panel, string script)
    {
        panel.ResponseScripts.Add(script);
    }

    public static void Source(this DataBoundControl control, object source)
    {
        control.DataSource = source;
    }

    public static void SourceBind(this DataBoundControl control, object source)
    {
        control.DataSource = source;
        control.DataBind();
    }

    public static Guid StorageAdd(this StorageEntity db, Guid campusId, string name, int ordinal)
    {
        var storage = new Models.Storage();
        storage.Id = db.GlobalId();
        storage.Name = name;
        storage.Ordinal = ordinal;
        storage.State = State.启用;
        storage.CampusId = campusId;
        db.Storage.Add(storage);
        var catalog = new Models.StorageCatalog();
        catalog.Id = db.GlobalId();
        catalog.ParentId = null;
        catalog.StorageId = storage.Id;
        catalog.Name = name;
        catalog.PinYin = string.Empty;
        catalog.Ordinal = 0;
        catalog.State = State.启用;
        catalog.Code = string.Empty;
        db.StorageCatalog.Add(catalog);
        return storage.Id;
    }

    public static Guid StorageAdd(this StorageEntity db, Guid campusId, string name, int ordinal, Guid id, Guid idx)
    {
        var storage = new Models.Storage();
        storage.Id = id;
        storage.Name = name;
        storage.Ordinal = ordinal;
        storage.State = State.启用;
        storage.CampusId = campusId;
        db.Storage.Add(storage);
        var catalog = new Models.StorageCatalog();
        catalog.Id = idx;
        catalog.ParentId = null;
        catalog.StorageId = storage.Id;
        catalog.Name = name;
        catalog.PinYin = string.Empty;
        catalog.Ordinal = 0;
        catalog.State = State.启用;
        catalog.Code = string.Empty;
        db.StorageCatalog.Add(catalog);
        return storage.Id;
    }

    public static Guid StorageCatalogAdd(this StorageEntity db, Guid storageId, Guid parentId, string name, int ordinal, string code)
    {
        var catalog = new Models.StorageCatalog();
        catalog.Id = db.GlobalId();
        catalog.ParentId = parentId;
        catalog.StorageId = storageId;
        catalog.Name = name;
        catalog.PinYin = string.Empty;
        catalog.Ordinal = ordinal;
        catalog.State = State.启用;
        catalog.Code = code;
        db.StorageCatalog.Add(catalog);
        return catalog.Id;
    }

    public static Guid StorageDictionaryAdd(this StorageEntity db, Guid storageId, string name, DictionaryType type)
    {
        var dictionary = new Models.StorageDictionary();
        dictionary.Name = name;
        dictionary.PinYin = string.Empty;
        dictionary.StorageId = storageId;
        dictionary.Type = type;
        db.StorageDictionary.Add(dictionary);
        return Guid.Empty;
    }

    public static Guid StorageTargetAdd(this StorageEntity db, Guid storageId, string number, string receipt, string source, string target, string content, decimal toPay, decimal paid, Guid? keeper, Guid? brokerage, Guid @operator, DateTime time, DateTime timeNode)
    {
        var obj = new Models.StorageTarget();
        obj.Id = db.GlobalId();
        obj.StorageId = storageId;
        obj.Number = number;
        obj.ReceiptNumber = receipt;
        obj.OrderSource = source;
        if (db.StorageDictionaryGet(storageId, DictionaryType.采购来源, source) == null)
            db.StorageDictionaryAdd(storageId, source, DictionaryType.采购来源);
        obj.UsageTarget = target;
        if (db.StorageDictionaryGet(storageId, DictionaryType.使用对象, target) == null)
            db.StorageDictionaryAdd(storageId, target, DictionaryType.使用对象);
        obj.Content = content;
        obj.ToPay = toPay;
        obj.Paid = paid;
        obj.BrokerageUserId = brokerage;
        obj.KeepUserId = keeper;
        obj.OperationUserId = @operator;
        obj.In = false;
        obj.Time = time;
        obj.TimeNode = int.Parse(timeNode.ToString("yyyyMMdd"));
        db.StorageTarget.Add(obj);
        return obj.Id;
    }

    public static Guid StorageObjectAdd(this StorageEntity db, Guid storageId, Guid catalogId, string name, string unit, string specification, bool consumable, bool @single, bool @fixed, string fixedSerial, int low, int high, Models.Image image, Guid @operator, int ordinal, DateTime time, string note, string code)
    {
        var obj = new Models.StorageObject();
        obj.Id = db.GlobalId();
        obj.StorageId = storageId;
        obj.CatalogId = catalogId;
        obj.Name = name;
        obj.PinYin = string.Empty;
        obj.Unit = unit;
        if (db.StorageDictionaryGet(storageId, DictionaryType.单位, unit) == null)
            db.StorageDictionaryAdd(storageId, unit, DictionaryType.单位);
        obj.Specification = specification;
        if (db.StorageDictionaryGet(storageId, DictionaryType.规格, specification) == null)
            db.StorageDictionaryAdd(storageId, specification, DictionaryType.规格);
        ////////////////////////////////////
        obj.Single = @single; // To Do
        ////////////////////////////////////
        obj.Consumable = consumable;
        obj.Fixed = @fixed;
        obj.FixedSerial = fixedSerial;
        obj.Low = low;
        obj.High = high;
        obj.Image = image.ToJson();
        obj.OperationUserId = @operator;
        obj.Ordinal = ordinal;
        obj.State = State.启用;
        obj.Time = time;
        obj.TimeNode = int.Parse(time.ToString("yyyyMMdd"));
        obj.Note = note;
        obj.Code = code;
        db.StorageObject.Add(obj);
        return obj.Id;
    }

    public static void StorageObjectEdit(this StorageEntity db, Guid storageId, Guid id, string name, string unit, string specification, string fixedSerial, int low, int high, Models.Image image, int ordinal, string note, string code)
    {
        var obj = db.StorageObjectGetOne(id);
        obj.Name = name;
        obj.PinYin = string.Empty;
        obj.Unit = unit;
        if (db.StorageDictionaryGet(storageId, DictionaryType.单位, unit) == null)
            db.StorageDictionaryAdd(storageId, unit, DictionaryType.单位);
        obj.Specification = specification;
        if (db.StorageDictionaryGet(storageId, DictionaryType.规格, specification) == null)
            db.StorageDictionaryAdd(storageId, specification, DictionaryType.规格);
        obj.FixedSerial = fixedSerial;
        obj.Low = low;
        obj.High = high;
        obj.Image = image.ToJson();
        obj.Note = note;
        obj.Ordinal = ordinal;
        obj.Code = code;
    }

    public static void StorageEdit(this StorageEntity db, Guid id, string name, int ordinal)
    {
        var storage = db.StorageGet(id);
        storage.Name = name;
        storage.Ordinal = ordinal;
        storage.StorageCatalog.Single(o => o.ParentId == null).Name = name;
    }

    public static void StorageCatalogEdit(this StorageEntity db, Guid id, string name, int ordinal, string code)
    {
        var catalog = db.StorageCatalogGetOne(id);
        catalog.Name = name;
        catalog.Ordinal = ordinal;
        catalog.Code = code;
    }

    public static void StorageRemove(this StorageEntity db, Guid id)
    {
        var storage = db.StorageGet(id);
        storage.State = State.删除;
    }

    public static void StorageCatalogRemove(this StorageEntity db, Guid id)
    {
        var catalog = db.StorageCatalogGetOne(id);
        catalog.State = State.删除;
    }

    public static int StorageObjectRemove(this StorageEntity db, Guid id)
    {
        var obj = db.StorageObjectGetOne(id);
        if (obj.StorageIn.Count(o => o.LendAmount > 0) > 0)
            return 1;
        if (obj.StorageIn.Count(o => o.InAmount > 0) > 0)
            return 2;
        obj.State = State.删除;
        return 0;
    }

    public static void StorageDictionaryRemove(this StorageEntity db, Guid storageId, string name, DictionaryType type)
    {
        var dictionary = db.StorageDictionaryGet(storageId, type).Single(o => o.Name == name);
        db.StorageDictionary.Remove(dictionary);
    }

    public static IQueryable<Storage> StorageGet(this StorageEntity db)
    {
        return db.Storage.Where(o => o.State == State.启用);
    }

    public static StorageObject StorageObjectGetOne(this StorageEntity db, Guid id)
    {
        return db.StorageObject.Single(o => o.State == State.启用 && o.Id == id);
    }

    public static IEnumerable<StorageObject> StorageCatalogObjectGet(this StorageEntity db, Guid catalogId)
    {
        var catalog = db.StorageCatalogGetOne(catalogId);
        return catalog.StorageObject.Where(o => o.State == State.启用);
    }

    public static IEnumerable<StorageObject> StorageObjectGet(this StorageEntity db, Guid storageId)
    {
        return db.StorageObject.Where(o => o.State == State.启用 && o.StorageId == storageId);
    }

    public static IEnumerable<StorageObject> StorageCatalogObjectGetEx(this StorageEntity db, Guid storageId, Guid catalogId)
    {
        List<Guid> list = new List<Guid>();
        var sc = db.StorageCatalog.Single(o => o.Id == catalogId);
        StorageCatalogLoopAdd(list, sc);
        return list.Join(db.StorageObject.Where(o => o.StorageId == storageId && o.State == State.启用), o => o, o => o.CatalogId, (x, y) => y).ToList();
    }

    public static IEnumerable<StorageTarget> StorageTargetGet(this StorageEntity db, Guid storageId)
    {
        return db.StorageTarget.Where(o => o.StorageId == storageId && (o.In == false || o.ReceiptNumber == ""));
    }

    public static StorageTarget StorageTargetGetOne(this StorageEntity db, Guid id)
    {
        return db.StorageTarget.Single(o => o.Id == id);
    }

    public static StorageCatalog StorageCatalogGetOne(this StorageEntity db, Guid id)
    {
        return db.StorageCatalog.Single(o => o.State == State.启用 && o.Id == id);
    }

    public static IQueryable<StorageCatalog> StorageCatalogGet(this StorageEntity db, Guid storageId, Guid parentId)
    {
        return db.StorageCatalog.Where(o => o.State == State.启用 && o.ParentId == parentId && o.StorageId == storageId);
    }

    public static IQueryable<StorageDictionary> StorageDictionaryGet(this StorageEntity db, Guid storageId, DictionaryType type)
    {
        return db.StorageDictionary.Where(o => o.Type == type && o.StorageId == storageId);
    }

    public static StorageDictionary StorageDictionaryGet(this StorageEntity db, Guid storageId, DictionaryType type, string name)
    {
        return db.StorageDictionary.SingleOrDefault(o => o.Type == type && o.StorageId == storageId && o.Name == name);
    }

    public static IQueryable<StorageCatalog> StorageCatalogGet(this StorageEntity db, Guid storageId)
    {
        return db.StorageCatalog.Where(o => o.StorageId == storageId && o.State == State.启用);
    }

    private static void StorageCatalogLoopAdd(List<Guid> list, StorageCatalog catalog)
    {
        list.Add(catalog.Id);
        foreach (var c in catalog.Children.Where(o => o.State == State.启用))
        {
            StorageCatalogLoopAdd(list, c);
        }
    }

    public static Storage StorageGet(this StorageEntity db, Guid id)
    {
        return db.Storage.SingleOrDefault(o => o.Id == id && o.State == State.启用);
    }

    public static void StorageSave(this StorageEntity db)
    {
        db.SaveChanges();
    }

    public static string GeneratePath(this StorageObject obj, string mark = "-")
    {
        var sb = new StringBuilder();
        var catalog = obj.StorageCatalog;
        sb.Append(catalog.Name);
        while (catalog.ParentId.HasValue)
        {
            catalog = catalog.Parent;
            sb.Insert(0, mark);
            sb.Insert(0, catalog.Name);
        }
        return sb.ToString().Substring(sb.ToString().IndexOf("-") + 1);
    }

    public static void TitleAppend(this Page page, string text)
    {
        page.Title += text;
    }

    public static string Text(this RadTextBox input)
    {
        return input.Text;
    }

    public static int Value(this RadNumericTextBox input, int @default)
    {
        return input.Value.HasValue ? (int)input.Value.Value : @default;
    }

    public static decimal Value(this RadNumericTextBox input, decimal @default)
    {
        return input.Value.HasValue ? (decimal)input.Value.Value : @default;
    }

    public static Guid? Value(this HtmlInputHidden input, Guid? @default = null)
    {
        return input.Value.Null() ? (@default.HasValue ? @default.Value : (Guid?)null) : input.Value.GlobalId();
    }

    public static DateTime Value(this RadDatePicker picker, DateTime? @default = null)
    {
        return picker.SelectedDate.HasValue ? picker.SelectedDate.Value : (@default.HasValue ? @default.Value : DateTime.Now);
    }

    public static bool MissingText(this RadTextBox box, string message)
    {
        if (box.Text.Null())
        {
            box.EmptyMessage = message;
            return true;
        }
        box.EmptyMessage = string.Empty;
        return false;
    }

    public static bool MissingValue(this RadNumericTextBox box, string message)
    {
        if (!box.Value.HasValue || box.Value.Value <= 0)
        {
            box.EmptyMessage = message;
            return true;
        }
        box.EmptyMessage = string.Empty;
        return false;
    }

    public static string Money(this object value)
    {
        return ((decimal)value).ToString("F2");
    }

    public static string TimeNode(this int stamp, string mark = "-")
    {
        return string.Format("{0}{1}{2}{1}{3}", stamp.ToString().Substring(0, 4), mark, stamp.ToString().Substring(4, 2), stamp.ToString().Substring(6, 2));
    }

    public static string UserName(this StorageEntity db, object id, string @default = "无")
    {
        if (id == null || id.ToString().Null())
            return @default;
        var uid = id.ToString().GlobalId();
        var u = db.User.SingleOrDefault(o => o.Id == uid);
        return u == null ? @default : u.RealName;
    }

    public static string Lined(this string value)
    {
        return value.Replace("\r\n", "<br />");
    }

    public static Guid SetFlow(this StorageEntity db, Guid storageId, StorageObject obj, FlowType type, DateTime time, int timeNode, int amount, decimal perPrice, decimal additionalFee, Guid? relativeId, Guid? relativeSingleId, string note)
    {
        StorageFlow last = obj.LastFlowId == Guid.Empty ? null : db.StorageFlow.SingleOrDefault(o => o.Id == obj.LastFlowId);
        var flow = new StorageFlow();
        flow.Id = db.GlobalId();
        flow.StorageId = storageId;
        flow.CatalogId = obj.CatalogId;
        flow.ObjectId = obj.Id;
        flow.Type = type;
        flow.TimeNode = timeNode;
        flow.Time = time;
        flow.Amount = amount;
        flow.PerPrice = perPrice;
        flow.TotalPrice = amount * perPrice;
        flow.AdditionalFee = additionalFee;
        flow.RelativeId = relativeId.HasValue ? relativeId.Value : Guid.Empty;
        flow.RelativeSingleId = relativeSingleId.HasValue ? relativeSingleId.Value : Guid.Empty;
        flow.InitialInAmount = last == null ? 0 : last.FinalInAmount;
        flow.InitialInMoney = last == null ? 0.00M : last.FinalInMoney;
        flow.InitialLendAmount = last == null ? 0 : last.FinalLendAmount;
        flow.InitialLendMoney = last == null ? 0.00M : last.FinalLendMoney;
        flow.InitialOutAmount = last == null ? 0 : last.FinalOutAmount;
        flow.InitialOutMoney = last == null ? 0.00M : last.FinalOutMoney;
        flow.InitialTotalAmount = last == null ? 0 : last.FinalTotalAmount;
        flow.InitialTotalMoney = last == null ? 0.00M : last.FinalTotalMoney;
        flow.FinalInAmount = (int)(type & FlowType.入库) == 0 ? flow.InitialInAmount - amount : flow.InitialInAmount + amount;
        flow.FinalInMoney = (int)(type & FlowType.入库) == 0 ? flow.InitialInMoney - (flow.TotalPrice + additionalFee) : flow.InitialInMoney + (flow.TotalPrice + additionalFee);
        flow.FinalLendAmount = (int)(type & (FlowType.借用 | FlowType.归还)) == 0 ? flow.InitialLendAmount : ((int)(type & FlowType.借用) == 0 ? flow.InitialLendAmount - amount : flow.InitialLendAmount + amount);
        flow.FinalLendMoney = (int)(type & (FlowType.借用 | FlowType.归还)) == 0 ? flow.InitialLendMoney : ((int)(type & FlowType.借用) == 0 ? flow.InitialLendMoney - (flow.TotalPrice + additionalFee) : flow.InitialLendMoney + (flow.TotalPrice + additionalFee));
        flow.FinalOutAmount = (int)(type & (FlowType.报废 | FlowType.领用 | FlowType.转出)) == 0 ? flow.InitialOutAmount : flow.InitialOutAmount + amount;
        flow.FinalOutMoney = (int)(type & (FlowType.报废 | FlowType.领用 | FlowType.转出)) == 0 ? flow.InitialOutMoney : flow.InitialOutMoney + (flow.TotalPrice + additionalFee);
        flow.FinalTotalAmount = flow.FinalInAmount + flow.FinalLendAmount + flow.FinalOutAmount;
        flow.FinalTotalMoney = flow.FinalInMoney + flow.FinalLendMoney + flow.FinalOutMoney;
        flow.Note = note;
        db.StorageFlow.Add(flow);
        return flow.Id;
    }

    public static Guid SetIn(this StorageEntity db, Guid objectId, Guid targetId, string age, string place, Guid? responsible, Guid @operator, int amount, decimal price, decimal fee, string note)
    {
        var @in = new StorageIn();
        @in.Id = db.GlobalId();
        @in.ObjectId = objectId;
        @in.TargetId = targetId;
        @in.Age = age;
        @in.Place = place;
        @in.Image = new Models.Image().ToJson();
        @in.ResponsibleUserId = responsible;
        @in.OperationUserId = @operator;
        @in.Time = DateTime.Now;
        @in.TimeNode = int.Parse(@in.Time.ToString("yyyyMMdd"));
        @in.Amount = amount;
        @in.TotalPrice = price;
        @in.PerPrice = decimal.Divide(@in.TotalPrice, @in.Amount);
        @in.AdditionalFee = fee;
        @in.InAmount = @in.Amount;
        @in.InMoney = @in.TotalPrice + @in.AdditionalFee;
        @in.LendAmount = 0;
        @in.LendMoney = 0.00M;
        @in.OutAmount = 0;
        @in.OutMoney = 0.00M;
        @in.TotalAmount = @in.InAmount;
        @in.TotalMoney = @in.InMoney;
        @in.Note = note;
        db.StorageIn.Add(@in);
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (db.StorageDictionaryGet(obj.StorageId, DictionaryType.存放地, place) == null)
            db.StorageDictionaryAdd(obj.StorageId, place, DictionaryType.存放地);
        if (db.StorageDictionaryGet(obj.StorageId, DictionaryType.年龄段, age) == null)
            db.StorageDictionaryAdd(obj.StorageId, age, DictionaryType.年龄段);
        //////////////////////////////////// To Do
        if (obj.Single)
        {
            var m = db.StorageInSingle.Where(o => o.ObjectId == objectId).Count() == 0 ? 0 : db.StorageInSingle.Where(o => o.ObjectId == objectId).Max(o => o.InOrdinal);
            for (var i = 0; i < @in.Amount; i++)
            {
                m++;
                var @single = new StorageInSingle();
                @single.Id = db.GlobalId();
                @single.InId = @in.Id;
                @single.InOrdinal = m;
                @single.In = true;
                @single.Lend = false;
                @single.Out = false;
                @single.Place = place;
                @single.ObjectId = objectId;
                db.StorageInSingle.Add(@single);
            }
        }
        ////////////////////////////////////
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.入库 | FlowType.购置, @in.Time, @in.TimeNode, @in.Amount, @in.PerPrice, @in.AdditionalFee, @in.Id, null, @in.Note);
        obj.InAmount += @in.Amount;
        obj.InMoney += @in.TotalMoney;
        obj.TotalAmount += @in.Amount;
        obj.TotalMoney += @in.TotalMoney;
        obj.LastFlowId = flowId;
        return @in.Id;
    }

    public static Guid SetConsume(this StorageEntity db, Guid objectId, Guid? consumer, Guid @operator, int amount, string note)
    {
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (obj.InAmount < amount)
            return Guid.Empty;
        var consume = new StorageConsume();
        consume.Id = db.GlobalId();
        consume.ObjectId = objectId;
        consume.ConsumeUserId = !consumer.HasValue ? @operator : consumer.Value;
        consume.OperationUserId = @operator;
        consume.Time = DateTime.Now;
        consume.TimeNode = int.Parse(consume.Time.ToString("yyyyMMdd"));
        consume.Amount = 0;
        consume.PerPrice = 0.00M;
        consume.TotalPrice = 0.00M;
        consume.AdditionalFee = 0.00M;
        consume.TotalMoney = 0.00M;
        consume.Note = note;
        db.StorageConsume.Add(consume);
        var left = amount;
        var counter = 0;
        foreach (var @in in obj.StorageIn.Where(o => o.InAmount > 0).OrderBy(o => o.Time))
        {
            counter++;
            if (@in.InAmount >= left)
            {
                var @single = new StorageConsumeSingle();
                consume.Amount += left;
                consume.TotalPrice += @in.PerPrice * left;
                consume.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * left;
                @single.OriginalAmount = @in.InAmount;
                @in.InAmount -= left;
                @in.InMoney -= (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                @in.OutAmount += left;
                @in.OutMoney += (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                @single.Id = db.GlobalId();
                @single.ConsumeId = consume.Id;
                @single.InId = @in.Id;
                @single.ConsumedAmount = left;
                @single.ConsumeOrdinal = counter;
                db.StorageConsumeSingle.Add(@single);
                break;
            }
            else
            {
                var @single = new StorageConsumeSingle();
                consume.Amount += @in.InAmount;
                consume.TotalPrice += @in.PerPrice * @in.InAmount;
                consume.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * @in.InAmount;
                left -= @in.InAmount;
                @single.OriginalAmount = @in.InAmount;
                @single.ConsumedAmount = @in.InAmount;
                @in.OutAmount += @in.InAmount;
                @in.OutMoney += @in.InMoney;
                @in.InAmount = 0;
                @in.InMoney -= @in.InMoney;
                @single.Id = db.GlobalId();
                @single.ConsumeId = consume.Id;
                @single.InId = @in.Id;
                @single.ConsumeOrdinal = counter;
                db.StorageConsumeSingle.Add(@single);
            }
        }
        consume.TotalMoney = consume.TotalPrice + consume.AdditionalFee;
        consume.PerPrice = decimal.Divide(consume.TotalPrice, consume.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.出库 | FlowType.领用, consume.Time, consume.TimeNode, consume.Amount, consume.PerPrice, consume.AdditionalFee, consume.Id, null, consume.Note);
        obj.InAmount -= consume.Amount;
        obj.InMoney -= consume.TotalMoney;
        obj.OutAmount += consume.Amount;
        obj.OutMoney += consume.TotalMoney;
        obj.LastFlowId = flowId;
        return consume.Id;
    }

    public static bool SetOutM(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, OutType type, string reason, int amount, string note)
    {
        try
        {
            var x = new ToOutTable
            {
                ObjectId = objectId,
                OutUserId = outUser.HasValue ? outUser.Value : @operator,
                OperatorId = @operator,
                OutType = type,
                OutReason = reason,
                OutAmount = amount,
                OutOrdinals = null,
                OutNote = note,
                Type = ToOutType.Multiple,
                Id = Guid.NewGuid().ToString(),
                Time = DateTime.Now
            };
            IStorageEngine engine = STSdb.FromFile(HttpContext.Current.Server.MapPath("~/StorageOut/Out.table"));
            try
            {
                var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
                table[x.Id] = x;
                engine.Commit();
            }
            finally
            {
                engine.Close();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SetOutDone(this StorageEntity db, OutDoneTable x)
    {
        try
        {
            IStorageEngine engine = STSdb.FromFile(HttpContext.Current.Server.MapPath("~/StorageOut/Out.table"));
            try
            {
                var table = engine.OpenXTablePortable<string, OutDoneTable>("OutDone");
                table[x.Id] = x;
                engine.Commit();
            }
            finally
            {
                engine.Close();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SetOutRandom(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, OutType type, string reason, int amount, string note)
    {
        try
        {
            var x = new ToOutTable
            {
                ObjectId = objectId,
                OutUserId = outUser.HasValue ? outUser.Value : @operator,
                OperatorId = @operator,
                OutType = type,
                OutReason = reason,
                OutAmount = amount,
                OutOrdinals = null,
                OutNote = note,
                Type = ToOutType.Random,
                Id = Guid.NewGuid().ToString(),
                Time = DateTime.Now
            };
            IStorageEngine engine = STSdb.FromFile(HttpContext.Current.Server.MapPath("~/StorageOut/Out.table"));
            try
            {
                var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
                table[x.Id] = x;
                engine.Commit();
            }
            finally
            {
                engine.Close();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SetOutSpecific(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, OutType type, string reason, List<int> list, string note)
    {
        try
        {
            var x = new ToOutTable
            {
                ObjectId = objectId,
                OutUserId = outUser.HasValue ? outUser.Value : @operator,
                OperatorId = @operator,
                OutType = type,
                OutReason = reason,
                OutAmount = null,
                OutOrdinals = list,
                OutNote = note,
                Type = ToOutType.Specific,
                Id = Guid.NewGuid().ToString(),
                Time = DateTime.Now
            };
            IStorageEngine engine = STSdb.FromFile(HttpContext.Current.Server.MapPath("~/StorageOut/Out.table"));
            try
            {
                var table = engine.OpenXTablePortable<string, ToOutTable>("ToOut");
                table[x.Id] = x;
                engine.Commit();
            }
            catch(Exception ex)
            {
                var t = 0;
            }
            finally
            {
                engine.Close();
            }
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static Guid SetOutMDo(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, OutType type, string reason, int amount, string note)
    {
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (obj.InAmount < amount || obj.Single)
            return Guid.Empty;
        var @out = new StorageOut();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.OutUserId = !outUser.HasValue ? @operator : outUser.Value;
        @out.OperationUserId = @operator;
        @out.Type = type;
        @out.Reason = reason;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageOut.Add(@out);
        var left = amount;
        var counter = 0;
        foreach (var @in in obj.StorageIn.Where(o => o.InAmount > 0).OrderBy(o => o.Time))
        {
            counter++;
            if (@in.InAmount >= left)
            {
                var @single = new StorageOutSingle();
                @single.Id = db.GlobalId();
                @single.OriginalAmount = @in.InAmount;
                @out.Amount += left;
                @out.TotalPrice += @in.PerPrice * left;
                @out.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * left;
                @in.InAmount -= left;
                @in.InMoney -= (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                @in.OutAmount += left;
                @in.OutMoney += (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                @single.InId = @in.Id;
                @single.OutId = @out.Id;
                @single.OutAmount = left;
                @single.Ordinal = counter;
                db.StorageOutSingle.Add(@single);
                break;
            }
            else
            {
                var @single = new StorageOutSingle();
                @single.Id = db.GlobalId();
                @single.OriginalAmount = @in.InAmount;
                @single.OutAmount = @in.InAmount;
                @out.Amount += @in.InAmount;
                @out.TotalPrice += @in.PerPrice * @in.InAmount;
                @out.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * @in.InAmount;
                left -= @in.InAmount;
                @in.OutAmount += @in.InAmount;
                @in.OutMoney += @in.InMoney;
                @in.InAmount = 0;
                @in.InMoney = 0.00M;
                @single.InId = @in.Id;
                @single.OutId = @out.Id;
                @single.Ordinal = counter;
                db.StorageOutSingle.Add(@single);
            }
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.出库 | FlowType.报废, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.InAmount -= @out.Amount;
        obj.InMoney -= @out.TotalMoney;
        obj.OutAmount += @out.Amount;
        obj.OutMoney += @out.TotalMoney;
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetOutRandomDo(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, OutType type, string reason, int amount, string note)
    {
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (obj.InAmount < amount || !obj.Single)
            return Guid.Empty;
        var @out = new StorageOut();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.OutUserId = !outUser.HasValue ? @operator : outUser.Value;
        @out.OperationUserId = @operator;
        @out.Type = type;
        @out.Reason = reason;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageOut.Add(@out);
        var t = obj.StorageInSingle.Where(o => o.In == true).OrderBy(o => o.InOrdinal).Take(amount).ToList();
        foreach (var s in t)
        {
            s.Out = true;
            s.In = false;
            var si = s.StorageIn;
            si.InAmount -= 1;
            si.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            si.OutAmount += 1;
            si.OutMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.InAmount -= 1;
            obj.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.OutAmount += 1;
            obj.OutMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            @out.Amount += 1;
            @out.TotalPrice += si.PerPrice;
            @out.AdditionalFee += decimal.Divide(si.AdditionalFee, si.Amount);
            var @single = new StorageOutSingle();
            @single.Id = db.GlobalId();
            @single.OutId = @out.Id;
            @single.InId = si.Id;
            @single.OriginalAmount = 1;
            @single.OutAmount = 1;
            @single.Ordinal = s.InOrdinal;
            db.StorageOutSingle.Add(@single);
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.出库 | FlowType.报废, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetOutSpecificDo(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, OutType type, string reason, List<int> list, string note)
    {
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (!obj.Single)
            return Guid.Empty;
        var @out = new StorageOut();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.OutUserId = !outUser.HasValue ? @operator : outUser.Value;
        @out.OperationUserId = @operator;
        @out.Type = type;
        @out.Reason = reason;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageOut.Add(@out);
        var t = list.Join(obj.StorageInSingle.Where(o => o.In == true), o => o, o => o.InOrdinal, (x, y) => y).OrderBy(o => o.InOrdinal).ToList();
        foreach (var s in t)
        {
            s.Out = true;
            s.In = false;
            var si = s.StorageIn;
            si.InAmount -= 1;
            si.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            si.OutAmount += 1;
            si.OutMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.InAmount -= 1;
            obj.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.OutAmount += 1;
            obj.OutMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            @out.Amount += 1;
            @out.TotalPrice += si.PerPrice;
            @out.AdditionalFee += decimal.Divide(si.AdditionalFee, si.Amount);
            var @single = new StorageOutSingle();
            @single.Id = db.GlobalId();
            @single.OutId = @out.Id;
            @single.InId = si.Id;
            @single.OriginalAmount = 1;
            @single.OutAmount = 1;
            @single.Ordinal = s.InOrdinal;
            db.StorageOutSingle.Add(@single);
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.出库 | FlowType.报废, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetLendM(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, int amount, string note)
    {
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (obj.InAmount < amount || obj.Single)
            return Guid.Empty;
        var @out = new StorageLend();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.BorrowUserId = !outUser.HasValue ? @operator : outUser.Value;
        @out.OperationUserId = @operator;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.ReturnedAmount = 0;
        @out.Returned = false;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageLend.Add(@out);
        var left = amount;
        var counter = 0;
        foreach (var @in in obj.StorageIn.Where(o => o.InAmount > 0).OrderBy(o => o.Time))
        {
            counter++;
            if (@in.InAmount >= left)
            {
                var @single = new StorageLendSingle();
                @single.Id = db.GlobalId();
                @single.Returned = false;
                @single.ReturnedAmount = 0;
                @out.Amount += left;
                @out.TotalPrice += @in.PerPrice * left;
                @out.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * left;
                @in.InAmount -= left;
                @in.InMoney -= (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                @in.LendAmount += left;
                @in.LendMoney += (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                @single.InId = @in.Id;
                @single.LendId = @out.Id;
                @single.LendAmount = left;
                @single.Ordinal = counter;
                db.StorageLendSingle.Add(@single);
                break;
            }
            else
            {
                var @single = new StorageLendSingle();
                @single.Id = db.GlobalId();
                @single.Returned = false;
                @single.ReturnedAmount = 0;
                @single.LendAmount = @in.InAmount;
                @out.Amount += @in.InAmount;
                @out.TotalPrice += @in.PerPrice * @in.InAmount;
                @out.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * @in.InAmount;
                left -= @in.InAmount;
                @in.LendAmount += @in.InAmount;
                @in.LendMoney += @in.InMoney;
                @in.InAmount = 0;
                @in.InMoney = 0.00M;
                @single.InId = @in.Id;
                @single.LendId = @out.Id;
                @single.Ordinal = counter;
                db.StorageLendSingle.Add(@single);
            }
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.出库 | FlowType.借用, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.InAmount -= @out.Amount;
        obj.InMoney -= @out.TotalMoney;
        obj.LendAmount += @out.Amount;
        obj.LendMoney += @out.TotalMoney;
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetLendRandom(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, int amount, string note, string place)
    {
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (obj.InAmount < amount || !obj.Single)
            return Guid.Empty;
        if (db.StorageDictionaryGet(obj.StorageId, DictionaryType.存放地, place) == null)
            db.StorageDictionaryAdd(obj.StorageId, place, DictionaryType.存放地);
        var @out = new StorageLend();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.BorrowUserId = !outUser.HasValue ? @operator : outUser.Value;
        @out.OperationUserId = @operator;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        @out.ReturnedAmount = 0;
        @out.Returned = false;
        db.StorageLend.Add(@out);
        var t = obj.StorageInSingle.Where(o => o.In == true).OrderBy(o => o.InOrdinal).Take(amount).ToList();
        foreach (var s in t)
        {
            s.Lend = true;
            s.In = false;
            s.Place = place;
            var si = s.StorageIn;
            si.InAmount -= 1;
            si.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            si.LendAmount += 1;
            si.LendMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.InAmount -= 1;
            obj.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.LendAmount += 1;
            obj.LendMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            @out.Amount += 1;
            @out.TotalPrice += si.PerPrice;
            @out.AdditionalFee += decimal.Divide(si.AdditionalFee, si.Amount);
            var @single = new StorageLendSingle();
            @single.Id = db.GlobalId();
            @single.LendId = @out.Id;
            @single.InId = si.Id;
            @single.Returned = false;
            @single.ReturnedAmount = 0;
            @single.LendAmount = 1;
            @single.Ordinal = s.InOrdinal;
            db.StorageLendSingle.Add(@single);
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.出库 | FlowType.借用, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetLendSpecific(this StorageEntity db, Guid objectId, Guid? outUser, Guid @operator, List<int> list, string note, string place)
    {
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (!obj.Single)
            return Guid.Empty;
        if (db.StorageDictionaryGet(obj.StorageId, DictionaryType.存放地, place) == null)
            db.StorageDictionaryAdd(obj.StorageId, place, DictionaryType.存放地);
        var @out = new StorageLend();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.BorrowUserId = !outUser.HasValue ? @operator : outUser.Value;
        @out.OperationUserId = @operator;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.ReturnedAmount = 0;
        @out.Returned = false;
        @out.Note = note;
        db.StorageLend.Add(@out);
        var t = list.Join(obj.StorageInSingle.Where(o => o.In == true), o => o, o => o.InOrdinal, (x, y) => y).OrderBy(o => o.InOrdinal).ToList();
        foreach (var s in t)
        {
            s.Lend = true;
            s.In = false;
            s.Place = place;
            var si = s.StorageIn;
            si.InAmount -= 1;
            si.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            si.LendAmount += 1;
            si.LendMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.InAmount -= 1;
            obj.InMoney -= decimal.Divide(si.TotalMoney, si.TotalAmount);
            obj.LendAmount += 1;
            obj.LendMoney += decimal.Divide(si.TotalMoney, si.TotalAmount);
            @out.Amount += 1;
            @out.TotalPrice += si.PerPrice;
            @out.AdditionalFee += decimal.Divide(si.AdditionalFee, si.Amount);
            var @single = new StorageLendSingle();
            @single.Id = db.GlobalId();
            @single.LendId = @out.Id;
            @single.InId = si.Id;
            @single.Returned = false;
            @single.ReturnedAmount = 0;
            @single.LendAmount = 1;
            @single.Ordinal = s.InOrdinal;
            db.StorageLendSingle.Add(@single);
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.出库 | FlowType.借用, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetReturnM(this StorageEntity db, Guid objectId, Guid borrowUser, Guid @operator, int amount, string note)
    {
        var m = db.StorageLend.Where(o => o.BorrowUserId == borrowUser && o.Returned == false && o.ObjectId == objectId);
        var total = m.Sum(o => o.Amount) - m.Sum(o => o.ReturnedAmount);
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (total < amount || obj.Single)
            return Guid.Empty;
        var @out = new StorageReturn();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.ReturnUserId = borrowUser;
        @out.OperationUserId = @operator;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageReturn.Add(@out);
        var left = amount;
        var counter = 0;
        var skip = false;
        foreach (var lend in db.StorageLend.Where(o => o.BorrowUserId == borrowUser && o.Returned == false && o.ObjectId == objectId))
        {
            if (skip)
                break;
            foreach (var l in lend.StorageLendSingle.Where(o => o.Returned == false))
            {
                counter++;
                var @in = l.StorageIn;
                if (l.LendAmount - l.ReturnedAmount >= left)
                {
                    var @single = new StorageReturnSingle();
                    @single.Id = db.GlobalId();
                    @single.ReturnId = @out.Id;
                    @single.LendId = lend.Id;
                    @single.Ordinal = counter;
                    @single.ReturnedAmount = left;
                    @out.Amount += left;
                    @out.TotalPrice += @in.PerPrice * left;
                    @out.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * left;
                    @in.InAmount += left;
                    @in.InMoney += (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                    @in.LendAmount -= left;
                    @in.LendMoney -= (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * left;
                    db.StorageReturnSingle.Add(@single);
                    l.ReturnedAmount += left;
                    l.Returned = l.LendAmount == l.ReturnedAmount;
                    lend.ReturnedAmount += left;
                    lend.Returned = lend.Amount == lend.ReturnedAmount;
                    skip = true;
                    break;
                }
                else
                {
                    var @single = new StorageReturnSingle();
                    @single.Id = db.GlobalId();
                    @single.ReturnId = @out.Id;
                    @single.LendId = lend.Id;
                    @single.Ordinal = counter;
                    @single.ReturnedAmount = l.LendAmount - l.ReturnedAmount;
                    @out.Amount += l.LendAmount - l.ReturnedAmount;
                    @out.TotalPrice += @in.PerPrice * (l.LendAmount - l.ReturnedAmount);
                    @out.AdditionalFee += (decimal.Divide(@in.AdditionalFee, @in.Amount)) * (l.LendAmount - l.ReturnedAmount);
                    left -= l.LendAmount - l.ReturnedAmount;
                    @in.LendAmount -= l.LendAmount - l.ReturnedAmount;
                    @in.LendMoney -= (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * (l.LendAmount - l.ReturnedAmount);
                    @in.InAmount += l.LendAmount - l.ReturnedAmount;
                    @in.InMoney += (decimal.Divide(@in.TotalMoney, @in.TotalAmount)) * (l.LendAmount - l.ReturnedAmount);
                    db.StorageReturnSingle.Add(@single);
                    l.ReturnedAmount += l.LendAmount - l.ReturnedAmount;
                    l.Returned = true;
                    lend.ReturnedAmount += l.LendAmount - l.ReturnedAmount;
                    lend.Returned = lend.Amount == lend.ReturnedAmount;
                }
            }
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.入库 | FlowType.归还, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.InAmount += @out.Amount;
        obj.InMoney += @out.TotalMoney;
        obj.LendAmount -= @out.Amount;
        obj.LendMoney -= @out.TotalMoney;
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetReturnRandom(this StorageEntity db, Guid objectId, Guid borrowUser, Guid @operator, int amount, string note)
    {
        var m = db.StorageLend.Where(o => o.BorrowUserId == borrowUser && o.Returned == false && o.ObjectId == objectId);
        var total = m.Sum(o => o.Amount) - m.Sum(o => o.ReturnedAmount);
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (total < amount || !obj.Single)
            return Guid.Empty;
        var @out = new StorageReturn();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.ReturnUserId = borrowUser;
        @out.OperationUserId = @operator;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageReturn.Add(@out);
        var source = db.StorageLend.Where(o => o.BorrowUserId == borrowUser && o.Returned == false && o.ObjectId == objectId).Select(o => o.StorageLendSingle.Where(p => p.Returned == false)).ToList();
        var ids = new List<StorageLendSingle>();
        foreach (var x in source)
        {
            ids.AddRange(x.ToList());
        }
        foreach (var s in ids.Take(amount))
        {
            s.StorageIn.StorageInSingle.Single(o => o.InOrdinal == s.Ordinal).Place = s.StorageIn.Place;
            var @in = s.StorageIn;
            var lend = s.StorageLend;
            var @single = new StorageReturnSingle();
            @single.Id = db.GlobalId();
            @single.ReturnId = @out.Id;
            @single.LendId = lend.Id;
            @single.Ordinal = s.Ordinal;
            @single.ReturnedAmount = 1;
            @out.Amount += 1;
            @out.TotalPrice += @in.PerPrice;
            @out.AdditionalFee += decimal.Divide(@in.AdditionalFee, @in.Amount);
            @in.InAmount += 1;
            @in.InMoney += decimal.Divide(@in.TotalMoney, @in.TotalAmount);
            @in.LendAmount -= 1;
            @in.LendMoney -= decimal.Divide(@in.TotalMoney, @in.TotalAmount);
            db.StorageReturnSingle.Add(@single);
            s.ReturnedAmount += 1;
            s.Returned = true;
            lend.ReturnedAmount += 1;
            lend.Returned = lend.ReturnedAmount == lend.Amount;
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.入库 | FlowType.归还, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.InAmount += @out.Amount;
        obj.InMoney += @out.TotalMoney;
        obj.LendAmount -= @out.Amount;
        obj.LendMoney -= @out.TotalMoney;
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetReturnSpecific(this StorageEntity db, Guid objectId, Guid borrowUser, Guid @operator, List<int> list, string note)
    {
        var m = db.StorageLend.Where(o => o.BorrowUserId == borrowUser && o.Returned == false && o.ObjectId == objectId);
        var total = m.Sum(o => o.Amount) - m.Sum(o => o.ReturnedAmount);
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == objectId);
        if (total < list.Count || !obj.Single)
            return Guid.Empty;
        var @out = new StorageReturn();
        @out.Id = db.GlobalId();
        @out.ObjectId = objectId;
        @out.ReturnUserId = borrowUser;
        @out.OperationUserId = @operator;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageReturn.Add(@out);
        var source = db.StorageLend.Where(o => o.BorrowUserId == borrowUser && o.Returned == false && o.ObjectId == objectId).Select(o => o.StorageLendSingle.Where(p => p.Returned == false)).ToList();
        var ids = new List<StorageLendSingle>();
        foreach (var x in source)
        {
            ids.AddRange(x.ToList());
        }
        var xids = list.Join(ids, o => o, o => o.Ordinal, (x, y) => y).ToList();
        foreach (var s in xids)
        {
            s.StorageIn.StorageInSingle.Single(o => o.InOrdinal == s.Ordinal).Place = s.StorageIn.Place;
            var @in = s.StorageIn;
            var lend = s.StorageLend;
            var fixS = @in.StorageInSingle.Single(o => o.InOrdinal == s.Ordinal);
            fixS.Lend = false;
            fixS.In = true;
            var @single = new StorageReturnSingle();
            @single.Id = db.GlobalId();
            @single.ReturnId = @out.Id;
            @single.LendId = lend.Id;
            @single.Ordinal = s.Ordinal;
            @single.ReturnedAmount = 1;
            @out.Amount += 1;
            @out.TotalPrice += @in.PerPrice;
            @out.AdditionalFee += decimal.Divide(@in.AdditionalFee, @in.Amount);
            @in.InAmount += 1;
            @in.InMoney += decimal.Divide(@in.TotalMoney, @in.TotalAmount);
            @in.LendAmount -= 1;
            @in.LendMoney -= decimal.Divide(@in.TotalMoney, @in.TotalAmount);
            db.StorageReturnSingle.Add(@single);
            s.ReturnedAmount += 1;
            s.Returned = true;
            lend.ReturnedAmount += 1;
            lend.Returned = lend.ReturnedAmount == lend.Amount;
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.入库 | FlowType.归还, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.InAmount += @out.Amount;
        obj.InMoney += @out.TotalMoney;
        obj.LendAmount -= @out.Amount;
        obj.LendMoney -= @out.TotalMoney;
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static Guid SetReturnSpecific(this StorageEntity db, 查询_借还流 kcuf, Guid @operator, string note)
    {
        var m = db.StorageLend.Where(o => o.BorrowUserId == kcuf.人员标识 && o.Returned == false && o.ObjectId == kcuf.物品标识);
        var total = m.Sum(o => o.Amount) - m.Sum(o => o.ReturnedAmount);
        var time = DateTime.Now;
        var timeNode = int.Parse(time.ToString("yyyyMMdd"));
        var obj = db.StorageObject.Single(o => o.Id == kcuf.物品标识);
        var @out = new StorageReturn();
        @out.Id = db.GlobalId();
        @out.ObjectId = kcuf.物品标识;
        @out.ReturnUserId = kcuf.人员标识;
        @out.OperationUserId = @operator;
        @out.Time = time;
        @out.TimeNode = timeNode;
        @out.Amount = 0;
        @out.PerPrice = 0.00M;
        @out.TotalPrice = 0.00M;
        @out.AdditionalFee = 0.00M;
        @out.TotalMoney = 0.00M;
        @out.Note = note;
        db.StorageReturn.Add(@out);
        var source = db.StorageLend.Where(o => o.BorrowUserId == kcuf.人员标识 && o.Returned == false && o.ObjectId == kcuf.物品标识).Select(o => o.StorageLendSingle.Where(p => p.Returned == false)).ToList();
        var ids = new List<StorageLendSingle>();
        foreach (var x in source)
        {
            ids.AddRange(x.ToList());
        }
        var xids = ids.Where(o=>o.Ordinal == kcuf.编号).ToList();
        foreach (var s in xids)
        {
            s.StorageIn.StorageInSingle.Single(o => o.InOrdinal == s.Ordinal).Place = s.StorageIn.Place;
            var @in = s.StorageIn;
            var lend = s.StorageLend;
            var fixS = @in.StorageInSingle.Single(o => o.InOrdinal == s.Ordinal);
            fixS.Lend = false;
            fixS.In = true;
            var @single = new StorageReturnSingle();
            @single.Id = db.GlobalId();
            @single.ReturnId = @out.Id;
            @single.LendId = lend.Id;
            @single.Ordinal = s.Ordinal;
            @single.ReturnedAmount = 1;
            @out.Amount += 1;
            @out.TotalPrice += @in.PerPrice;
            @out.AdditionalFee += decimal.Divide(@in.AdditionalFee, @in.Amount);
            @in.InAmount += 1;
            @in.InMoney += decimal.Divide(@in.TotalMoney, @in.TotalAmount);
            @in.LendAmount -= 1;
            @in.LendMoney -= decimal.Divide(@in.TotalMoney, @in.TotalAmount);
            db.StorageReturnSingle.Add(@single);
            s.ReturnedAmount += 1;
            s.Returned = true;
            lend.ReturnedAmount += 1;
            lend.Returned = lend.ReturnedAmount == lend.Amount;
        }
        @out.TotalMoney = @out.TotalPrice + @out.AdditionalFee;
        @out.PerPrice = decimal.Divide(@out.TotalPrice, @out.Amount);
        var flowId = db.SetFlow(obj.StorageId, obj, FlowType.入库 | FlowType.归还, @out.Time, @out.TimeNode, @out.Amount, @out.PerPrice, @out.AdditionalFee, @out.Id, null, @out.Note);
        obj.InAmount += @out.Amount;
        obj.InMoney += @out.TotalMoney;
        obj.LendAmount -= @out.Amount;
        obj.LendMoney -= @out.TotalMoney;
        obj.LastFlowId = flowId;
        return @out.Id;
    }

    public static void InitializePermission(this StorageEntity db, Guid userId, Guid storageId)
    {
        var role = new StorageRole
        {
            Id = db.GlobalId(),
            StorageId = storageId,
            Name = "内置管理员",
            Ordinal = 0,
            State = State.内置
        };
        role.StorageRoleRight.Add(new StorageRoleRight { RoleId = role.Id, Right = "+" });
        role.StorageRoleRight.Add(new StorageRoleRight { RoleId = role.Id, Right = "*" });
        role.StorageRoleRight.Add(new StorageRoleRight { RoleId = role.Id, Right = "-" });
        role.StorageRoleRight.Add(new StorageRoleRight { RoleId = role.Id, Right = "?" });
        role.StorageRoleRight.Add(new StorageRoleRight { RoleId = role.Id, Right = "!" });
        role.StorageRoleRight.Add(new StorageRoleRight { RoleId = role.Id, Right = "=" });
        var user = db.User.Single(o => o.Id == userId);
        user.StorageRole.Add(role);
    }
}
