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

    public static Guid DepotCatalogAdd(this DepotEntities db, Guid depotId, Guid? parent, Guid top, string name, int ordinal, string code)
    {
        var pinYin = db.ToPinYin(name).Single();
        var newId = db.GlobalId();
        var catalog = new DepotCatalog
        {
            Id = newId,
            ParentId = parent,
            TopId = parent.HasValue ? top : newId,
            DepotId = depotId,
            Name = name,
            PinYin = pinYin,
            Ordinal = ordinal,
            State = State.启用,
            Code = code
        };
        db.DepotCatalog.Add(catalog);
        db.SaveChanges();
        return newId;
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

    public static IEnumerable<DepotObjectX> DepotObjectXLoad(this DepotEntities db, Guid depotId, Guid? catalogId)
    {
        if (catalogId.HasValue)
        {
            var id = catalogId.Value;
            return db.DepotCatalog.Where(o => o.DepotId == depotId && o.Id == id && o.State < State.停用).Select(o => o.Id).ToList().Join(db.DepotObjectCatalog, o => o, o => o.CatalogId, (c, oc) => oc.ObjectId).Distinct().ToList().Join(db.DepotObjectX, o => o, o => o.Id, (oc, o) => o);
        }
        else
        {
            return db.DepotCatalog.Where(o => o.DepotId == depotId && o.State < State.停用).Select(o => o.Id).ToList().Join(db.DepotObjectCatalog, o => o, o => o.CatalogId, (c, oc) => oc.ObjectId).Distinct().ToList().Join(db.DepotObjectX, o => o, o => o.Id, (oc, o) => o);
        }
    }

    public static void DepotObjectAdd(this DepotEntities db, Guid id, List<Guid> catalogIds, Guid depotId, string name, bool single, bool consumable, bool @fixed, string fixedCard, string fixedNumber, string brand, string extension, string unit, string specification, decimal low, decimal high, string pa, string pb, string pc, string pd, string note, int ordinal, string age)
    {
        var obj = new DepotObject
        {
            Id = id,
            Name = name,
            PinYin = db.ToPinYin(name).Single(),
            Single = single,
            Consumable = consumable,
            Fixed = @fixed,
            FixedCard = fixedCard,
            FixedNumber = fixedNumber,
            Brand = brand,
            Extension = extension,
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
            Money = 0.00M,
            Age = age
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
        db.DepotDictionaryAdd(depotId, DictionaryType.品牌, brand);
    }

    public static void DepotObjectAddX(this DepotEntities db, Guid id, List<Guid> catalogIds, Guid depotId, string name, bool single, bool consumable, bool @fixed, string fixedCard, string fixedNumber, string brand, string extension, string unit, string specification, decimal low, decimal high, string pa, string pb, string pc, string pd, string note, int ordinal)
    {
        var obj = new DepotObject
        {
            Id = id,
            Name = name,
            PinYin = db.ToPinYin(name).Single(),
            Single = single,
            Consumable = consumable,
            Fixed = @fixed,
            FixedCard = fixedCard,
            FixedNumber = fixedNumber,
            Brand = brand,
            Extension = extension,
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
            db.DepotObjectCatalog.Add(new DepotObjectCatalog { ObjectId = id, CatalogId = catalogIds[i], IsVirtual = true, Level = i, IsLeaf = i == catalogIds.Count - 1 });
        }
        db.SaveChanges();
        obj.Code = db.ToQR(CodeType.Object, obj.AutoId);
        db.SaveChanges();
        db.DepotDictionaryAdd(depotId, DictionaryType.单位, unit);
        db.DepotDictionaryAdd(depotId, DictionaryType.规格, specification);
        db.DepotDictionaryAdd(depotId, DictionaryType.品牌, brand);
    }

    public static void DepotObjectEdit(this DepotEntities db, Guid id, List<Guid> catalogIds, Guid depotId, string name, string fixedCard, string fixedNumber, string brand, string extension, string unit, string specification, decimal low, decimal high, string pa, string pb, string pc, string pd, string note, int ordinal, string age)
    {
        var obj = db.DepotObject.Single(o => o.Id == id);
        obj.Name = name;
        obj.PinYin = db.ToPinYin(name).Single();
        obj.FixedCard = fixedCard;
        obj.FixedNumber = fixedNumber;
        obj.Brand = brand;
        obj.Extension = extension;
        obj.Unit = unit;
        obj.Specification = specification;
        obj.Low = low;
        obj.High = high;
        obj.Age = age;
        obj.ImageA = pa;
        obj.ImageB = pb;
        obj.ImageC = pc;
        obj.ImageD = pd;
        obj.Note = note;
        obj.Ordinal = ordinal;
        var catalogs = db.DepotObjectCatalog.Where(o => o.ObjectId == id && o.IsVirtual == false).ToList();
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
        db.DepotDictionaryAdd(depotId, DictionaryType.品牌, brand);
    }

    public static void DepotObjectEditX(this DepotEntities db, Guid id, List<Guid> catalogIds, Guid depotId, string name, string fixedCard, string fixedNumber, string brand, string extension, string unit, string specification, decimal low, decimal high, string pa, string pb, string pc, string pd, string note, int ordinal, string age, bool isVirtual)
    {
        var obj = db.DepotObject.Single(o => o.Id == id);
        obj.Name = name;
        obj.PinYin = db.ToPinYin(name).Single();
        obj.FixedCard = fixedCard;
        obj.FixedNumber = fixedNumber;
        obj.Brand = brand;
        obj.Extension = extension;
        obj.Unit = unit;
        obj.Specification = specification;
        obj.Low = low;
        obj.High = high;
        obj.Age = age;
        obj.ImageA = pa;
        obj.ImageB = pb;
        obj.ImageC = pc;
        obj.ImageD = pd;
        obj.Note = note;
        obj.Ordinal = ordinal;
        var catalogs = db.DepotObjectCatalog.Where(o => o.ObjectId == id && o.IsVirtual == isVirtual).ToList();
        for (var i = 0; i < catalogs.Count(); i++)
        {
            db.DepotObjectCatalog.Remove(catalogs.ElementAt(i));
        }
        for (var i = 0; i < catalogIds.Count; i++)
        {
            db.DepotObjectCatalog.Add(new DepotObjectCatalog { ObjectId = id, CatalogId = catalogIds[i], IsVirtual = isVirtual, Level = i, IsLeaf = i == catalogIds.Count - 1 });
        }
        db.SaveChanges();
        db.DepotDictionaryAdd(depotId, DictionaryType.单位, unit);
        db.DepotDictionaryAdd(depotId, DictionaryType.规格, specification);
        db.DepotDictionaryAdd(depotId, DictionaryType.品牌, brand);
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
        if (name.None())
            return;
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

    public static void DepotOrderAdd(this DepotEntities db, Guid id, Guid depotId, string name, string receipt, string orderSource, string usageTarget, string note, decimal toPay, decimal paid, Guid? brokerageId, Guid? keeperId, DateTime orderTime, Guid operatorId, string flowNo)
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
            MainID = flowNo,
            State = State.启用
        };
        db.DepotOrder.Add(order);
        db.SaveChanges();
        db.DepotDictionaryAdd(depotId, DictionaryType.购置来源, orderSource);
        db.DepotDictionaryAdd(depotId, DictionaryType.使用对象, usageTarget);
    }

    public static void DepotActIn(this DepotEntities db, Guid depotId, Guid orderId, DateTime inTime, Guid operatorId, List<InMemoryIn> list)
    {
        foreach (var item in list)
        {
            if (!item.ObjectId.HasValue || !item.Amount.HasValue || item.Amount.Value == 0)
                continue;
            var objId = item.ObjectId.Value;
            var obj = db.DepotObject.Single(o => o.Id == objId);
            var @in = new DepotIn
            {
                Id = db.GlobalId(),
                OrderId = orderId,
                ObjectId = item.ObjectId.Value,
                Age = item.Age,
                Place = item.Place,
                ResponsibleId = item.ResponsibleId,
                Note = item.Note,
                Time = inTime,
                OperatorId = operatorId,
                OperationTime = DateTime.Now,
                Amount = item.Amount.Value,
                AvailableAmount = item.Amount.Value,
                Price = decimal.Divide(item.Money.Value, item.Amount.Value),
                PriceSet = item.PriceSet.HasValue ? item.PriceSet.Value : 0M,
                Total = item.Money.Value,
            };
            db.DepotIn.Add(@in);
            obj.Amount += @in.Amount;
            obj.Money += @in.Total;
            if (obj.Single)
            {
                var current = db.DepotInX.Count(o => o.ObjectId == obj.Id) == 0 ? 0 : db.DepotInX.Where(o => o.ObjectId == obj.Id).Max(o => o.Ordinal);
                for (var j = 0; j < @in.Amount; j++)
                {
                    current++;
                    var inx = new DepotInX
                    {
                        Id = db.GlobalId(),
                        InId = @in.Id,
                        OrderId = @in.OrderId,
                        ObjectId = @in.ObjectId,
                        Age = @in.Age,
                        Place = @in.Place,
                        Ordinal = current,
                        Amount = 1,
                        PriceSet = @in.PriceSet,
                        Price = @in.Price,
                        Total = @in.Price,
                        AvailableAmount = 1,
                        Code = obj.Code,
                        ResponsibleId = item.ResponsibleId
                    };
                    db.DepotInX.Add(inx);
                    db.SaveChanges();
                    inx.Code = db.ToQR(CodeType.Single, inx.AutoId);
                    var flowx = new DepotFlowX
                    {
                        Id = db.GlobalId(),
                        ObjectId = @in.ObjectId,
                        ObjectOrdinal = inx.Ordinal,
                        UserId = operatorId,
                        Type = FlowType.入库,
                        TypeName = FlowType.入库.ToString(),
                        Time = inTime,
                        Amount = 1,
                        Money = @in.Price,
                        Note = @in.Note
                    };
                    db.DepotFlowX.Add(flowx);
                }
            }
            else
            {
                var inx = new DepotInX
                {
                    Id = db.GlobalId(),
                    InId = @in.Id,
                    OrderId = @in.OrderId,
                    ObjectId = @in.ObjectId,
                    Age = @in.Age,
                    Place = @in.Place,
                    Ordinal = -1,
                    Amount = @in.Amount,
                    PriceSet = @in.PriceSet,
                    Price = @in.Price,
                    Total = @in.Total,
                    AvailableAmount = @in.AvailableAmount,
                    Code = obj.Code,
                    ResponsibleId = item.ResponsibleId
                };
                db.DepotInX.Add(inx);
                var flow = new DepotFlow
                {
                    Id = db.GlobalId(),
                    ObjectId = @in.ObjectId,
                    UserId = operatorId,
                    Type = FlowType.入库,
                    TypeName = FlowType.入库.ToString(),
                    Time = inTime,
                    Amount = @in.Amount,
                    Money = @in.Total,
                    Note = @in.Note
                };
                db.DepotFlow.Add(flow);
            }
            db.DepotActStatistics(@in.ObjectId, inTime, @in.Amount, @in.Total, 0, 0, 0, 0, 0, 0, 0, 0);
            db.SaveChanges();
            db.DepotDictionaryAdd(depotId, DictionaryType.年龄段, item.Age);
            db.DepotDictionaryAdd(depotId, DictionaryType.存放地, item.Place);
        }
    }

    public static Guid DepotActUse(this DepotEntities db, Guid depotId, DateTime useTime, Guid operatorId, Guid userId, List<InMemoryUse> list)
    {
        if (list == null || list.Count == 0)
        {
            return Guid.Empty;
        }
        var use = new DepotUse
        {
            Id = db.GlobalId(),
            DepotId = depotId,
            UserId = userId,
            Time = useTime,
            OperatorId = operatorId,
            OperationTime = DateTime.Now,
            Age = string.Empty,
            Place = string.Empty,
            Money = 0
        };
        db.DepotUse.Add(use);
        foreach (var item in list)
        {
            if (!item.ObjectId.HasValue)
                continue;
            var objId = item.ObjectId.Value;
            var obj = db.DepotObject.Single(o => o.Id == objId);
            if (obj.Single)
            {
                if (item.Ordinals.Count == 0 || !item.Type.HasValue)
                {
                    continue;
                }
                foreach (var index in item.Ordinals)
                {
                    var @in = db.DepotInX.Single(o => o.ObjectId == objId && o.Ordinal == index);
                    var x = new DepotUseX
                    {
                        Id = db.GlobalId(),
                        ObjectId = objId,
                        UseId = use.Id,
                        InXId = @in.Id,
                        Type = item.Type.Value,
                        Age = item.Age,
                        Place = item.Place,
                        Amount = 1,
                        Money = @in.Price,
                        ReturnedAmount = 0,
                        Note = item.Note
                    };
                    db.DepotUseX.Add(x);
                    @in.AvailableAmount = 0;
                    @in.DepotIn.AvailableAmount -= 1;
                    obj.Amount -= 1;
                    var flowx = new DepotFlowX
                    {
                        Id = db.GlobalId(),
                        ObjectId = @in.ObjectId,
                        ObjectOrdinal = index,
                        UserId = operatorId,
                        Type = FlowType.借用出库,
                        TypeName = FlowType.借用出库.ToString(),
                        Time = useTime,
                        Amount = -1,
                        Money = @in.Price,
                        Note = item.Note
                    };
                    use.Money += @in.Price;
                    db.DepotFlowX.Add(flowx);
                    db.DepotActStatistics(@in.ObjectId, useTime, 0, 0, 1, @in.Price, 0, 0, 0, 0, 0, 0);
                }
            }
            else
            {
                if (!item.Amount.HasValue || item.Amount.Value == 0)
                {
                    continue;
                }
                if (!item.Type.HasValue)
                {
                    continue;
                }
                var todo = obj.Amount < item.Amount.Value ? obj.Amount : item.Amount.Value;
                var totalAmount = 0M;
                var totalMoney = 0M;
                foreach (var @in in obj.DepotIn.Where(o => o.ObjectId == objId && o.AvailableAmount > 0).OrderBy(o => o.Time).ToList())
                {
                    var xObj = db.DepotInX.Single(o => o.ObjectId == objId && o.InId == @in.Id);
                    if (@in.AvailableAmount < todo)
                    {
                        var x = new DepotUseX
                        {
                            Id = db.GlobalId(),
                            ObjectId = objId,
                            UseId = use.Id,
                            InXId = xObj.Id,
                            Type = item.Type.Value,
                            Age = item.Age,
                            Place = item.Place,
                            Amount = @in.AvailableAmount,
                            Money = @in.Price * @in.AvailableAmount,
                            ReturnedAmount = 0,
                            Note = item.Note
                        };
                        db.DepotUseX.Add(x);
                        todo -= @in.AvailableAmount;
                        totalAmount += @in.AvailableAmount;
                        totalMoney += @in.AvailableAmount * @in.Price;
                        obj.Amount -= @in.AvailableAmount;
                        obj.Money -= @in.AvailableAmount * @in.Price;
                        @in.Total -= @in.AvailableAmount * @in.Price;
                        @in.AvailableAmount = 0;
                        var inx = @in.DepotInX.Single();
                        inx.AvailableAmount = @in.AvailableAmount;
                        inx.Total = @in.Total;
                    }
                    else
                    {
                        var x = new DepotUseX
                        {
                            Id = db.GlobalId(),
                            ObjectId = objId,
                            UseId = use.Id,
                            InXId = xObj.Id,
                            Type = item.Type.Value,
                            Age = item.Age,
                            Place = item.Place,
                            Amount = todo,
                            Money = @in.Price * todo,
                            ReturnedAmount = 0,
                            Note = item.Note
                        };
                        db.DepotUseX.Add(x);
                        totalAmount += todo;
                        totalMoney += todo * @in.Price;
                        obj.Amount -= todo;
                        obj.Money -= todo * @in.Price;
                        @in.Total -= todo * @in.Price;
                        @in.AvailableAmount -= todo;
                        var inx = @in.DepotInX.Single();
                        inx.AvailableAmount = @in.AvailableAmount;
                        inx.Total = @in.Total;
                        todo = 0;
                        break;
                    }
                }
                use.Money += totalMoney;
                var flow = new DepotFlow
                {
                    Id = db.GlobalId(),
                    ObjectId = objId,
                    UserId = userId,
                    Type = item.Type.Value == UseType.借用 ? FlowType.借用出库 : FlowType.领用出库,
                    TypeName = (item.Type.Value == UseType.借用 ? FlowType.借用出库 : FlowType.领用出库).ToString(),
                    Time = useTime,
                    Amount = -totalAmount,
                    Money = -totalMoney,
                    Note = "出库"
                };
                db.DepotFlow.Add(flow);
                if (item.Type.Value == UseType.借用)
                    db.DepotActStatistics(objId, useTime, 0, 0, totalAmount, totalMoney, 0, 0, 0, 0, 0, 0);
                else
                    db.DepotActStatistics(objId, useTime, 0, 0, 0, 0, totalAmount, totalMoney, 0, 0, 0, 0);
            }
        }
        db.SaveChanges();
        if (use.DepotUseX.Count == 0)
        {
            db.DepotUse.Remove(use);
            db.SaveChanges();
            return Guid.Empty;
        }
        return use.Id;
    }

    public static void DepotActOut(this DepotEntities db, Guid depotId, DateTime outTime, Guid operatorId, Guid userId, List<InMemoryOut> list)
    {
        foreach (var @out in list)
        {
            if (!@out.ObjectId.HasValue)
            {
                continue;
            }
            var objId = @out.ObjectId.Value;
            var obj = db.DepotObject.Single(o => o.Id == objId);
            if (obj.Single)
            {
                foreach (var s in @out.Ordinals)
                {
                    var xi = db.DepotInX.SingleOrDefault(o => o.ObjectId == objId && o.Ordinal == s);
                    if (xi == null)
                        continue;
                    var to = new DepotToOut
                    {
                        DepotId = depotId,
                        ObjectId = objId,
                        UserId = userId,
                        Code = xi.Code,
                        Reason = @out.Reason,
                        ToAmount = 1,
                        Amount = 0,
                        PreservedA = "",
                        PreservedB = "",
                        PreservedC = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        PreservedD = operatorId.ToString(),
                        Time = outTime,
                        State = 2
                    };
                    db.DepotToOut.Add(to);
                }
            }
            else
            {
                if (!@out.Amount.HasValue || @out.Amount.Value == 0)
                    continue;
                var to = new DepotToOut
                {
                    DepotId = depotId,
                    ObjectId = objId,
                    UserId = userId,
                    Code = obj.Code,
                    Reason = @out.Reason,
                    ToAmount = @out.Amount.Value,
                    Amount = 0,
                    PreservedA = "",
                    PreservedB = "",
                    PreservedC = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    PreservedD = operatorId.ToString(),
                    Time = outTime,
                    State = 2
                };
                db.DepotToOut.Add(to);
            }
            db.SaveChanges();
        }
    }

    public static void DepotActReturn(this DepotEntities db, Guid depotId, DateTime returnTime, Guid operatorId, List<InMemoryReturn> list)
    {
        if (list == null || list.Count == 0)
        {
            return;
        }
        var @out = new List<InMemoryOut>();
        foreach (var item in list)
        {
            if (!item.Amount.HasValue || item.Amount.Value == 0)
                continue;
            var usexId = item.UseX;
            var usex = db.DepotUseX.SingleOrDefault(o => o.Id == usexId && o.ReturnedAmount < o.Amount);
            if (usex == null)
                continue;
            var inx = usex.DepotInX;
            var obj = inx.DepotObject;
            var @in = inx.DepotIn;
            var @return = new DepotReturn
            {
                Id = db.GlobalId(),
                UserId = usex.DepotUse.UserId,
                UseXId = item.UseX,
                Amount = item.Amount.Value,
                Price = inx.Price,
                Total = item.Amount.Value * inx.Price,
                Time = returnTime,
                Note = item.Note
            };
            var catalog = db.DepotObjectCatalog.Where(o => o.ObjectId == obj.Id && o.IsLeaf == true).ToList().Join(db.DepotCatalog.Where(o => o.DepotId == depotId), o => o.CatalogId, o => o.Id, (a, b) => a.CatalogId).FirstOrDefault();
            usex.ReturnedAmount += @return.Amount;
            db.DepotReturn.Add(@return);
            obj.Amount += @return.Amount;
            obj.Money += @return.Total;
            @in.AvailableAmount += @return.Amount;
            @in.Total += @in.Price * @return.Amount;
            inx.AvailableAmount += @return.Amount;
            @in.Total += @in.Price * @return.Amount;
            if (obj.Single)
            {
                var flowx = new DepotFlowX
                {
                    Id = db.GlobalId(),
                    ObjectId = @in.ObjectId,
                    ObjectOrdinal = inx.Ordinal,
                    UserId = usex.DepotUse.UserId,
                    Type = FlowType.归还,
                    TypeName = FlowType.归还.ToString(),
                    Time = returnTime,
                    Amount = @return.Amount,
                    Money = @in.Price,
                    Note = item.Note
                };
                db.DepotFlowX.Add(flowx);
                if (item.OutAmount.HasValue && item.OutAmount.Value > 0)
                {
                    var nl = new List<int>();
                    nl.Add(inx.Ordinal);
                    @out.Add(new InMemoryOut { Amount = item.OutAmount.Value, ObjectId = @in.ObjectId, Ordinals = nl, Reason = "报废", CatalogId = catalog });
                }
            }
            else
            {
                var flow = new DepotFlow
                {
                    Id = db.GlobalId(),
                    ObjectId = @in.ObjectId,
                    UserId = usex.DepotUse.UserId,
                    Type = FlowType.归还,
                    TypeName = FlowType.归还.ToString(),
                    Time = returnTime,
                    Amount = @return.Amount,
                    Money = @in.Price * @return.Amount,
                    Note = item.Note
                };
                db.DepotFlow.Add(flow);
                if (item.OutAmount > 0)
                {
                    var nl = new List<int>();
                    @out.Add(new InMemoryOut { Amount = item.OutAmount, ObjectId = @in.ObjectId, Ordinals = nl, Reason = "报废", CatalogId = catalog });
                }
            }
            db.DepotActStatistics(@in.ObjectId, returnTime, 0, 0, -@return.Amount, -@return.Total, 0, 0, 0, 0, 0, 0);
        }
        db.SaveChanges();
        try
        {
            db.DepotActOut(depotId, returnTime, operatorId, operatorId, @out);
        }
        catch
        {
        }
    }

    public static void DepotActInEdit(this DepotEntities db, Guid depotId, DepotIn @in, DateTime day, decimal amount, decimal priceSet, decimal money, string age, string place, string note, Guid operatorId)
    {
        var obj = db.DepotObject.Single(o => o.Id == @in.ObjectId);
        var order = db.DepotOrder.Single(o => o.Id == @in.OrderId);
        decimal plusAmount = amount - @in.Amount;
        decimal plusMoney = money - @in.Total;
        if (day.Year == @in.Time.Year && day.Month == @in.Time.Month)
        {
            db.DepotActStatistics(obj.Id, day, plusAmount, plusMoney, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        else
        {
            db.DepotActStatistics(obj.Id, @in.Time, -@in.Amount, -@in.Total, 0, 0, 0, 0, 0, 0, 0, 0);
            db.DepotActStatistics(obj.Id, day, amount, money, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        if (obj.Single)
        {
            return;
        }
        else
        {
            @in.Age = age;
            @in.Place = place;
            @in.Note = note;
            @in.Time = day;
            var x = @in.DepotInX.First();
            x.Age = @in.Age;
            x.Place = @in.Place;
            if (amount != @in.Amount || money != @in.Total)
            {
                if (amount > 0 && money > 0)
                {
                    @in.Amount = amount;
                    @in.AvailableAmount = amount;
                    @in.Total = money;
                    @in.PriceSet = priceSet;
                    @in.Price = decimal.Divide(money, amount);
                    obj.Amount += plusAmount;
                    obj.Money += plusMoney;
                    order.Paid += plusMoney;
                    var flow = new DepotFlow
                    {
                        Id = db.GlobalId(),
                        ObjectId = @in.ObjectId,
                        UserId = operatorId,
                        Type = FlowType.入库修改,
                        TypeName = FlowType.入库修改.ToString(),
                        Time = day,
                        Amount = plusAmount,
                        Money = plusMoney,
                        Note = @in.Note
                    };
                    db.DepotFlow.Add(flow);
                    x.Amount = @in.Amount;
                    x.AvailableAmount = @in.Amount;
                    x.Total = @in.Total;
                    x.PriceSet = @in.PriceSet;
                    x.Price = @in.Price;
                }
                else
                {
                    db.DepotInX.Remove(x);
                    db.DepotIn.Remove(@in);
                    obj.Amount += plusAmount;
                    obj.Money += plusMoney;
                    order.Paid += plusMoney;
                    var flow = new DepotFlow
                    {
                        Id = db.GlobalId(),
                        ObjectId = @in.ObjectId,
                        UserId = operatorId,
                        Type = FlowType.入库修改,
                        TypeName = FlowType.入库修改.ToString(),
                        Time = day,
                        Amount = plusAmount,
                        Money = plusMoney,
                        Note = @in.Note
                    };
                    db.DepotFlow.Add(flow);
                }
            }
        }
        db.SaveChanges();
        db.DepotDictionaryAdd(depotId, DictionaryType.年龄段, age);
        db.DepotDictionaryAdd(depotId, DictionaryType.存放地, place);
    }

    public static void DepotActInRedo(this DepotEntities db, Guid depotId, DepotIn @in, decimal backed, Guid operatorId)
    {
        if (backed == 0)
            return;
        var obj = db.DepotObject.Single(o => o.Id == @in.ObjectId);
        var order = db.DepotOrder.Single(o => o.Id == @in.OrderId);
        decimal amount = @in.Amount - backed;
        decimal money = (@in.Amount - backed) * @in.Price;
        decimal plusAmount = -backed;
        decimal plusMoney = -backed * @in.Price;
        db.DepotActStatistics(obj.Id, @in.Time, plusAmount, plusMoney, 0, 0, 0, 0, 0, 0, 0, 0);
        if (obj.Single)
        {
            if (amount >= 0 && money >= 0)
            {
                foreach (var inx in @in.DepotInX)
                {
                    if (inx.DepotUseX.Count > 0)
                        return;
                }
                @in.Amount = amount;
                @in.AvailableAmount = amount;
                @in.Total = money;
                @in.Price = decimal.Divide(money, amount);
                obj.Amount += plusAmount;
                obj.Money += plusMoney;
                order.Paid += plusMoney;
                var dr = new DepotRedo
                {
                    Id = db.GlobalId(),
                    DepotId = depotId,
                    UserId = operatorId,
                    ObjectId = obj.Id,
                    InId = @in.Id,
                    Amount = backed,
                    Money = backed * @in.Price,
                    Time = DateTime.Now,
                    Note = ""
                };
                db.DepotRedo.Add(dr);
                var left = backed;
                foreach (var inx in @in.DepotInX.OrderByDescending(o => o.Ordinal).ToList())
                {
                    inx.Amount = 0;
                    inx.AvailableAmount = 0;
                    inx.Total = 0;
                    var flowx = new DepotFlowX
                    {
                        Id = db.GlobalId(),
                        ObjectId = @in.ObjectId,
                        ObjectOrdinal = inx.Ordinal,
                        UserId = operatorId,
                        Type = FlowType.退货,
                        TypeName = FlowType.退货.ToString(),
                        Time = @in.Time,
                        Amount = plusAmount,
                        Money = plusMoney,
                        Note = ""
                    };
                    db.DepotFlowX.Add(flowx);
                    left--;
                    if (left == 0)
                        break;
                }
            }
        }
        else
        {
            var x = @in.DepotInX.First();
            if (x.DepotUseX.Count(o => o.InXId == x.Id) > 0)
                return;
            if (amount >= 0 && money >= 0)
            {
                @in.Amount = amount;
                @in.AvailableAmount = amount;
                @in.Total = money;
                @in.Price = amount == 0 ? 0 : decimal.Divide(money, amount);
                obj.Amount += plusAmount;
                obj.Money += plusMoney;
                order.Paid += plusMoney;
                var flow = new DepotFlow
                {
                    Id = db.GlobalId(),
                    ObjectId = @in.ObjectId,
                    UserId = operatorId,
                    Type = FlowType.退货,
                    TypeName = FlowType.退货.ToString(),
                    Time = @in.Time,
                    Amount = plusAmount,
                    Money = plusMoney,
                    Note = ""
                };
                db.DepotFlow.Add(flow);
                x.Amount = @in.Amount;
                x.AvailableAmount = @in.Amount;
                x.Total = @in.Total;
                var dr = new DepotRedo
                {
                    Id = db.GlobalId(),
                    DepotId = depotId,
                    UserId = operatorId,
                    ObjectId = obj.Id,
                    InId = @in.Id,
                    Amount = backed,
                    Money = backed * @in.Price,
                    Time = DateTime.Now,
                    Note = ""
                };
                db.DepotRedo.Add(dr);
            }
        }
        db.SaveChanges();
    }

    public static void DepotActStatistics(this DepotEntities db, Guid objectId, DateTime time, decimal @in, decimal inMoney, decimal lend, decimal lendMoney, decimal consume, decimal consumeMoney, decimal @out, decimal outMoney, decimal redo, decimal redoMoney)
    {
        var year = time.Year;
        var month = time.Month;
        var stamp = new DateTime(year, month, 1);
        var exists = db.DepotStatistics.Count(o => o.ObjectId == objectId && o.Year == year && o.Month == month);
        var last = db.DepotStatistics.Where(o => o.ObjectId == objectId && o.Time < stamp).OrderByDescending(o => o.Time).FirstOrDefault();
        if (exists == 0)
        {
            var ss = new DepotStatistics
            {
                ObjectId = objectId,
                Year = year,
                Month = month,
                Time = stamp,
                StartAmount = last == null ? 0 : last.EndAmount,
                StartMoney = last == null ? 0 : last.EndMoney,
                InAmount = 0,
                InMoney = 0,
                ConsumeAmount = 0,
                ConsumeMoney = 0,
                LendAmount = 0,
                LendMoney = 0,
                OutAmount = 0,
                OutMoney = 0,
                RedoAmount = 0,
                RedoMoney = 0,
                EndAmount = last == null ? 0 : last.EndAmount,
                EndMoney = last == null ? 0 : last.EndMoney
            };
            ss.InAmount += @in;
            ss.InMoney += inMoney;
            ss.ConsumeAmount += consume;
            ss.ConsumeMoney += consumeMoney;
            ss.LendAmount += lend;
            ss.LendMoney += lendMoney;
            ss.OutAmount += @out;
            ss.OutMoney += outMoney;
            ss.RedoAmount += redo;
            ss.RedoMoney += redoMoney;
            ss.EndAmount += @in - consume - @out - lend - redo;
            ss.EndMoney += inMoney - consumeMoney - outMoney - lendMoney - redoMoney;
            db.DepotStatistics.Add(ss);
        }
        else
        {
            var current = db.DepotStatistics.Single(o => o.ObjectId == objectId && o.Year == year && o.Month == month);
            current.InAmount += @in;
            current.InMoney += inMoney;
            current.ConsumeAmount += consume;
            current.ConsumeMoney += consumeMoney;
            current.LendAmount += lend;
            current.LendMoney += lendMoney;
            current.OutAmount += @out;
            current.OutMoney += outMoney;
            current.RedoAmount += redo;
            current.RedoMoney += redoMoney;
            current.EndAmount += @in - consume - @out - lend - redo;
            current.EndMoney += inMoney - consumeMoney - outMoney - lendMoney - redoMoney;
        }
        foreach (var current in db.DepotStatistics.Where(o => o.ObjectId == objectId && o.Time > stamp))
        {
            current.StartAmount += @in - @out - consume - lend - redo;
            current.StartMoney += inMoney - outMoney - consumeMoney - lendMoney - redoMoney;
            current.EndAmount += @in - @out - consume - lend - redo;
            current.EndMoney += inMoney - outMoney - consumeMoney - lendMoney - redoMoney;
        }
    }
}
