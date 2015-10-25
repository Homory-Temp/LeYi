using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class DepotDataExtensions
{
    public static Guid GlobalId(this DepotEntities db)
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

    public static string ToQR(this DepotEntities db, CodeType type, int autoId)
    {
        StringBuilder sb = new StringBuilder(type.ToString().GetFirstChar());
        sb.Append(autoId.ToString("X").PadLeft(11, '0'));
        return sb.ToString();
    }

    public static void DepotAdd(this DepotEntities db, string name, Guid campusId, Guid userId, int ordinal, string defaultObjectView, string defaultObjectType, string objectTypes)
    {
        var depot = new Depot
        {
            Id = db.GlobalId(),
            Name = name,
            CampusId = campusId,
            Ordinal = ordinal,
            DefaultObjectView = defaultObjectView[0].ToString(),
            DefaultObjectType = defaultObjectType[0].ToString(),
            ObjectTypes = objectTypes,
            Type = DepotType.通用库,
            State = State.启用
        };
        db.Depot.Add(depot);
        var depotRole = new DepotRole
        {
            Id = db.GlobalId(),
            DepotId = depot.Id,
            Name = "{0}管理组".Formatted(name),
            Rights = "*",
            Ordinal = 0,
            State = State.内置
        };
        db.DepotRole.Add(depotRole);
        var depotUserRole = new DepotUserRole
        {
            UserId = userId,
            DepotRoleId = depotRole.Id
        };
        db.DepotUserRole.Add(depotUserRole);
        db.SaveChanges();
    }

    public static void DepotEdit(this DepotEntities db, Guid id, string name, int ordinal, string defaultObjectView, string defaultObjectType, string objectTypes)
    {
        var depot = db.Depot.Single(o => o.Id == id);
        depot.Name = name;
        depot.Ordinal = ordinal;
        depot.DefaultObjectView = defaultObjectView[0].ToString();
        depot.DefaultObjectType = defaultObjectType[0].ToString();
        depot.ObjectTypes = objectTypes;
        db.SaveChanges();
    }

    public static void DepotRemove(this DepotEntities db, Guid id)
    {
        var depot = db.Depot.Single(o => o.Id == id);
        depot.State = State.停用;
        db.SaveChanges();
    }

    public static IQueryable<DepotCatalog> DepotCatalogLoad(this DepotEntities db, Guid depotId)
    {
        return db.DepotCatalog.Where(o => o.DepotId == depotId && o.State < State.停用).OrderBy(o => o.Ordinal).ThenBy(o => o.Name);
    }

    public static IQueryable<DepotCatalogTree> DepotCatalogTreeLoad(this DepotEntities db, Guid depotId)
    {
        return db.DepotCatalogTree.Where(o => o.DepotId == depotId).OrderBy(o => o.Ordinal).ThenBy(o => o.Name);
    }

    public static IEnumerable<DepotObject> DepotObjectLoad(this DepotEntities db, Guid depotId, Guid? catalogId)
    {
        if (catalogId.HasValue)
        {
            var id = catalogId.Value;
            return db.DepotCatalog.Where(o => o.DepotId == depotId && o.Id == id && o.State < State.停用).Select(o => o.Id).ToList().Join(db.DepotObjectCatalog, o => o, o => o.CatalogId, (c, oc) => oc.ObjectId).Distinct().ToList().Join(db.DepotObject.Where(o => o.State < State.停用), o => o, o => o.Id, (oc, o) => o);
        }
        else
        {
            return db.DepotCatalog.Where(o => o.DepotId == depotId && o.State < State.停用).Select(o => o.Id).ToList().Join(db.DepotObjectCatalog, o => o, o => o.CatalogId, (c, oc) => oc.ObjectId).Distinct().ToList().Join(db.DepotObject.Where(o => o.State < State.停用), o => o, o => o.Id, (oc, o) => o);
        }
    }

    public static void DepotObjectAdd(this DepotEntities db, Guid id, List<Guid> catalogIds, Guid depotId, string name, bool single, bool consumable, bool @fixed, string a, string b, string c, string d, string unit, string specification, decimal low, decimal high, string pa, string pb, string pc, string pd, string note, int ordinal)
    {
        var obj = new DepotObject
        {
            Id = id,
            Name = name,
            PinYin = db.ToPinYin(name).Single(),
            Single = single,
            Consumable = consumable,
            Fixed = @fixed,
            SerialA = a,
            SerialB = b,
            SerialC = c,
            SerialD = d,
            Unit = unit,
            Specification = specification,
            Low = low,
            High = high,
            ImageA = pa,
            ImageB = pb,
            ImageC = pc,
            ImageD = pd,
            Note = note,
            Ordinal = ordinal,
            State = State.启用,
            Code = string.Empty,
            Amount = 0.00M,
            Money = 0.00M
        };
        db.DepotObject.Add(obj);
        for (var i = 0; i < catalogIds.Count; i++)
        {
            db.DepotObjectCatalog.Add(new DepotObjectCatalog { ObjectId = id, CatalogId = catalogIds[i], IsVirtual = false, Level = i, IsLeaf = i == catalogIds.Count - 1 });
        }
        db.SaveChanges();
        obj.Code = db.ToQR(CodeType.Object, obj.AutoId);
        db.SaveChanges();
        db.DepotDictionaryAdd(depotId, DictionaryType.单位, unit);
        db.DepotDictionaryAdd(depotId, DictionaryType.规格, specification);
    }

    public static void DepotObjectEdit(this DepotEntities db, Guid id, List<Guid> catalogIds, Guid depotId, string name, string a, string b, string c, string d, string unit, string specification, decimal low, decimal high, string pa, string pb, string pc, string pd, string note, int ordinal)
    {
        var obj = db.DepotObject.Single(o => o.Id == id);
        obj.Name = name;
        obj.PinYin = db.ToPinYin(name).Single();
        obj.SerialA = a;
        obj.SerialB = b;
        obj.SerialC = c;
        obj.SerialD = d;
        obj.Unit = unit;
        obj.Specification = specification;
        obj.Low = low;
        obj.High = high;
        obj.ImageA = pa;
        obj.ImageB = pb;
        obj.ImageC = pc;
        obj.ImageD = pd;
        obj.Note = note;
        obj.Ordinal = ordinal;
        var catalogs = db.DepotObjectCatalog.Where(o => o.ObjectId == id).ToList();
        for (var i = 0; i < catalogs.Count(); i++)
        {
            db.DepotObjectCatalog.Remove(catalogs.ElementAt(i));
        }
        for (var i = 0; i < catalogIds.Count; i++)
        {
            db.DepotObjectCatalog.Add(new DepotObjectCatalog { ObjectId = id, CatalogId = catalogIds[i], IsVirtual = false, Level = i, IsLeaf = i == catalogIds.Count - 1 });
        }
        db.SaveChanges();
        db.DepotDictionaryAdd(depotId, DictionaryType.单位, unit);
        db.DepotDictionaryAdd(depotId, DictionaryType.规格, specification);
    }

    public static void DepotObjectRemove(this DepotEntities db, Guid id)
    {
        var obj = db.DepotObject.Single(o => o.Id == id);
        obj.State = State.停用;
        var catalogs = db.DepotObjectCatalog.Where(o => o.ObjectId == id).ToList();
        for (var i = 0; i < catalogs.Count(); i++)
        {
            db.DepotObjectCatalog.Remove(catalogs.ElementAt(i));
        }
        db.SaveChanges();
    }

    public static IQueryable<DepotDictionary> DepotDictionaryLoad(this DepotEntities db, Guid depotId, DictionaryType type)
    {
        return db.DepotDictionary.Where(o => o.DepotId == depotId && o.Type == type).OrderBy(o => o.Name);
    }

    public static void DepotDictionaryAdd(this DepotEntities db, Guid depotId, DictionaryType type, string name)
    {
        var count = db.DepotDictionaryLoad(depotId, type).Count(o => o.Name == name);
        if (count == 0)
        {
            var dictionary = new DepotDictionary
            {
                DepotId = depotId,
                Type = type,
                Name = name,
                PinYin = db.ToPinYin(name).Single()
            };
            db.DepotDictionary.Add(dictionary);
            db.SaveChanges();
        }
    }

    public static void DepotDictionaryRemove(this DepotEntities db, Guid depotId, DictionaryType type, string name)
    {
        var dictionary = db.DepotDictionaryLoad(depotId, type).SingleOrDefault(o => o.Name == name);
        if (dictionary != null)
        {
            db.DepotDictionary.Remove(dictionary);
            db.SaveChanges();
        }
    }

    public static IQueryable<DepotUser> DepotUserLoad(this DepotEntities db, Guid campusId)
    {
        return db.DepotUser.Where(o => o.CampusId == campusId).OrderBy(o => o.Name);
    }

    public static IQueryable<DepotRole> DepotRoleLoad(this DepotEntities db, Guid depotId)
    {
        return db.DepotRole.Where(o => o.DepotId == depotId && o.State < State.停用).OrderBy(o => o.State).ThenBy(o => o.Ordinal);
    }

    public static void DepotRoleAddOrUpdate(this DepotEntities db, Guid depotId, Guid id, string name, string rights, int ordinal)
    {
        var count = db.DepotRoleLoad(depotId).Count(o => o.Id == id);
        if (count == 0)
        {
            var role = new DepotRole
            {
                Id = id,
                DepotId = depotId,
                Name = name,
                Rights = rights,
                Ordinal = ordinal,
                State = State.启用
            };
            db.DepotRole.Add(role);
        }
        else
        {
            var role = db.DepotRoleLoad(depotId).Single(o => o.Id == id);
            role.Name = name;
            role.Rights = rights;
            role.Ordinal = ordinal;
        }
        db.SaveChanges();
    }

    public static void DepotOrderAdd(this DepotEntities db, Guid id, Guid depotId, string name, string receipt, string orderSource, string usageTarget, string note, decimal toPay, decimal paid, Guid? brokerageId, Guid? keeperId, DateTime orderTime, Guid operatorId)
    {
        var order = new DepotOrder
        {
            Id = id,
            DepotId = depotId,
            Name = name,
            Receipt = receipt,
            OrderSource = orderSource,
            UsageTarget = usageTarget,
            Note = note,
            ToPay = toPay,
            Paid = paid,
            BrokerageId = brokerageId,
            KeeperId = keeperId,
            Done = false,
            OrderTime = orderTime,
            RecordTime = orderTime,
            OperatorId = operatorId,
            OperationTime = DateTime.Now,
            State = State.启用
        };
        db.DepotOrder.Add(order);
        db.SaveChanges();
        db.DepotDictionaryAdd(depotId, DictionaryType.购置来源, orderSource);
        db.DepotDictionaryAdd(depotId, DictionaryType.使用对象, usageTarget);
    }
}
