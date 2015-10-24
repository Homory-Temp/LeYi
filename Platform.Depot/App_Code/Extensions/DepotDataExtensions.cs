using Models;
using System;
using System.Linq;

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

    public static void DepotAdd(this DepotEntities db, string name, Guid campusId, Guid userId, int ordinal, string defaultObjectView, string defaultObjectType, string objectTypes, DepotType type, State state)
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
            Type = type,
            State = state
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
}
