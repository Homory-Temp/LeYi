using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

public static class BusinessExtensions
{
    public static string GetUserName(this StoreEntity db, object id, string @default = "无")
    {
        if (id == null || id.ToString().Null())
            return @default;
        var uid = id.ToString().GlobalId();
        var u = db.User.SingleOrDefault(o => o.Id == uid);
        return u == null ? @default : u.RealName;
    }

    public static User GetUser(this StoreEntity db, object id)
    {
        if (id == null || id.ToString().Null())
            return null;
        var uid = id.ToString().GlobalId();
        var u = db.User.SingleOrDefault(o => o.Id == uid);
        return u;
    }

    public static Guid GlobalId(this StoreEntity db)
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

    public static int PeekValue(this RadNumericTextBox control, int @default)
    {
        return control.Value.HasValue ? (int)control.Value.Value : @default;
    }

    public static decimal PeekValue(this RadNumericTextBox control, decimal @default)
    {
        return control.Value.HasValue ? (decimal)control.Value.Value : @default;
    }

    public static int ToTimeNode(this DateTime time)
    {
        return int.Parse(time.ToString("yyyyMMdd"));
    }

    public static string ToMoney(this object money)
    {
        return decimal.Parse(money.ToString()).ToString("F2");
    }

    public static DateTime ToTime(this object timeNode)
    {
        return DateTime.Parse("{0}-{1}-{2}".Formatted(timeNode.ToString().Substring(0, 4), timeNode.ToString().Substring(4, 2), timeNode.ToString().Substring(6, 2)));
    }

    public static string FromTimeNode(this object timeNode)
    {
        return "{0}-{1}-{2}".Formatted(timeNode.ToString().Substring(0, 4), timeNode.ToString().Substring(4, 2), timeNode.ToString().Substring(6, 2));
    }

    public static int PeekValue(this RadComboBox control, int @default)
    {
        return control.SelectedIndex < 0 ? @default : int.Parse(control.SelectedValue);
    }

    public static string PeekValue(this RadButton control, bool detectCheckState = false)
    {
        return detectCheckState ? (control.Checked ? control.Value : string.Empty) : control.Value;
    }

    public static int PeekValue(this RadButton[] controls, int @default)
    {
        return controls.Count(o => o.Checked) == 1 ? int.Parse(controls.Single(o => o.Checked).Value) : @default;
    }

    public static void ActionIn(this StoreEntity db, Guid targetId, Guid objectId, string age, string place, string image, Guid? responsibleId, string note, DateTime inTime, Guid operatorId, string code, decimal amount, decimal totalPrice, decimal sourcePerPrice, decimal fee, decimal money)
    {
        var @in = new StoreIn
        {
            Id = db.GlobalId(),
            TargetId = targetId,
            ObjectId = objectId,
            Age = age,
            Place = place,
            Image = image,
            ResponsibleUserId = responsibleId,
            Note = note,
            TimeNode = inTime.ToTimeNode(),
            Time = inTime,
            OperationUserId = operatorId,
            OperationTime = DateTime.Now,
            Code = code,
            Amount = amount,
            PerPrice = decimal.Divide(totalPrice, amount),
            SourcePerPrice = sourcePerPrice,
            Fee = fee,
            Money = totalPrice + fee
        };
        db.StoreIn.Add(@in);
        var obj = db.StoreObject.Single(o => o.Id == objectId);
        obj.Amount += amount;
        obj.Money += money;
        var catalog = obj.StoreCatalog;
        var store = catalog.Store;
        if (store.State != StoreState.食品 && db.StoreDictionary.Count(o => o.StoreId == store.Id && o.Type == DictionaryType.年龄段 && o.Name == age) == 0)
        {
            var dictionary = new StoreDictionary
            {
                StoreId = catalog.StoreId,
                Type = DictionaryType.年龄段,
                Name = age,
                PinYin = db.ToPinYin(age).Single()
            };
            db.StoreDictionary.Add(dictionary);
        }
        var flow = new StoreFlow
        {
            Id = db.GlobalId(),
            ObjectId = objectId,
            UserId = operatorId,
            Type = FlowType.入库,
            TypeName = FlowType.入库.ToString(),
            TimeNode = inTime.ToTimeNode(),
            Time = inTime,
            Amount = amount,
            Money = money,
            Note = note
        };
        db.StoreFlow.Add(flow);
        db.ActionRecord(objectId, inTime, amount, money, 0M, 0M, 0M, 0M, 0M, 0M);
        if (obj.Single)
        {
            // To Do
        }
    }

    public static void ActionRecord(this StoreEntity db, Guid objectId, DateTime time, decimal @in, decimal inMoney, decimal lend, decimal lendMoney, decimal consume, decimal consumeMoney, decimal @out, decimal outMoney)
    {
        var year = time.Year;
        var month = time.Month;
        var stamp = new DateTime(year, month, 1).ToTimeNode();
        var exists = db.StoreStatistics.Count(o => o.ObjectId == objectId && o.Year == year && o.Month == month);
        var last = db.StoreStatistics.Where(o => o.ObjectId == objectId && o.TimeNode < stamp).OrderByDescending(o => o.TimeNode).FirstOrDefault();
        if (exists == 0)
        {
            var ss = new StoreStatistics
            {
                ObjectId = objectId,
                Year = year,
                Month = month,
                TimeNode = stamp,
                Time = new DateTime(year, month, 1),
                StartInAmount = last == null ? 0M : last.EndInAmount,
                StartInMoney = last == null ? 0M : last.EndInMoney,
                StartConsumeAmount = last == null ? 0M : last.EndConsumeAmount,
                StartConsumeMoney = last == null ? 0M : last.EndConsumeMoney,
                StartLendAmount = last == null ? 0M : last.EndLendAmount,
                StartLendMoney = last == null ? 0M : last.EndLendMoney,
                StartOutAmount = last == null ? 0M : last.EndOutAmount,
                StartOutMoney = last == null ? 0M : last.EndOutMoney,
            };
            ss.EndInAmount = ss.StartInAmount + @in;
            ss.EndInMoney = ss.StartInMoney + inMoney;
            ss.EndConsumeAmount = ss.StartConsumeAmount + consume;
            ss.EndConsumeMoney = ss.StartConsumeMoney + consumeMoney;
            ss.EndLendAmount = ss.StartLendAmount + lend;
            ss.EndLendMoney = ss.StartLendMoney + lendMoney;
            ss.EndOutAmount = ss.StartOutAmount + @out;
            ss.EndOutMoney = ss.StartOutMoney + outMoney;
            db.StoreStatistics.Add(ss);
        }
        else
        {
            var current = db.StoreStatistics.Single(o => o.ObjectId == objectId && o.Year == year && o.Month == month);
            current.EndInAmount += @in;
            current.EndInMoney += inMoney;
            current.EndLendAmount += lend;
            current.EndLendMoney += lendMoney;
            current.EndConsumeAmount += consume;
            current.EndConsumeMoney += consumeMoney;
            current.EndOutAmount += @out;
            current.EndOutMoney += outMoney;
        }
        foreach (var current in db.StoreStatistics.Where(o => o.ObjectId == objectId && o.TimeNode > stamp))
        {
            current.StartInAmount += @in;
            current.StartInMoney += inMoney;
            current.StartLendAmount += lend;
            current.StartLendMoney += lendMoney;
            current.StartConsumeAmount += consume;
            current.StartConsumeMoney += consumeMoney;
            current.StartOutAmount += @out;
            current.StartOutMoney += outMoney;
            current.EndInAmount += @in;
            current.EndInMoney += inMoney;
            current.EndLendAmount += lend;
            current.EndLendMoney += lendMoney;
            current.EndConsumeAmount += consume;
            current.EndConsumeMoney += consumeMoney;
            current.EndOutAmount += @out;
            current.EndOutMoney += outMoney;
        }
    }
}
