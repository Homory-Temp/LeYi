﻿using Models;
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

    public static void ActionInExt(this StoreEntity db, Guid targetId, Guid objectId, string age, string place, string image, Guid? responsibleId, string note, DateTime inTime, Guid operatorId, string code, decimal amount, decimal totalPrice, decimal sourcePerPrice, decimal fee, decimal money)
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
            SourceAmount = amount,
            OriginalAmount = amount,
            PerPrice = decimal.Divide(totalPrice, amount),
            SourcePerPrice = sourcePerPrice,
            Fee = fee,
            Money = totalPrice + fee,
            SourceMoney = totalPrice + fee,
            OriginalMoney = totalPrice + fee
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
        db.ActionRecord(objectId, inTime, amount, money, 0M, 0M, 0M, 0M, 0M, 0M, 0M, 0M);
        if (obj.Single)
        {
            // To Do
        }
        db.SaveChanges();
    }

    public static void ActionInEditExt(this StoreEntity db, StoreIn @in, DateTime day, decimal amount, decimal perPrice, decimal money, string place, string note, Guid operatorId)
    {
        var obj = db.StoreObject.Single(o => o.Id == @in.ObjectId);
        var target = db.StoreTarget.Single(o => o.Id == @in.TargetId);
        if (obj.Single)
            return;
        decimal plusAmount = amount - @in.Amount;
        decimal plusMoney = money - @in.Money;
        if (day.Year == @in.Time.Year || day.Month == @in.Time.Month)
        {
            db.ActionRecord(obj.Id, day, plusAmount, plusMoney, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        else
        {
            db.ActionRecord(obj.Id, @in.Time, plusAmount, plusMoney, 0, 0, 0, 0, 0, 0, 0, 0);
            db.ActionRecord(obj.Id, day, amount, money, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        if (amount != @in.OriginalAmount || money != @in.OriginalMoney)
        {
            if (amount > 0 && money > 0)
            {
                @in.Amount = amount;
                @in.OriginalAmount = amount;
                @in.SourceAmount = amount;
                @in.Money = money;
                @in.OriginalMoney = money;
                @in.SourceMoney = money;
                @in.SourcePerPrice = perPrice;
                @in.PerPrice = decimal.Divide(money, amount);
                @in.Place = place;
                @in.Note = note;
                @in.Time = day;
                @in.TimeNode = day.ToTimeNode();
                obj.Amount += plusAmount;
                obj.Money += plusMoney;
                target.Paid += plusMoney;
                var flow = new StoreFlow
                {
                    Id = db.GlobalId(),
                    ObjectId = obj.Id,
                    UserId = operatorId,
                    Type = FlowType.入库修改,
                    TypeName = FlowType.入库修改.ToString(),
                    TimeNode = day.ToTimeNode(),
                    Time = day,
                    Amount = plusAmount,
                    Money = plusMoney,
                    Note = note
                };
                db.StoreFlow.Add(flow);
            }
            else
            {
                db.StoreIn.Remove(@in);
                obj.Amount += plusAmount;
                obj.Money += plusMoney;
                target.Paid += plusMoney;
                var flow = new StoreFlow
                {
                    Id = db.GlobalId(),
                    ObjectId = obj.Id,
                    UserId = operatorId,
                    Type = FlowType.入库修改,
                    TypeName = FlowType.入库修改.ToString(),
                    TimeNode = day.ToTimeNode(),
                    Time = day,
                    Amount = plusAmount,
                    Money = plusMoney,
                    Note = note
                };
                db.StoreFlow.Add(flow);
            }
        }
        db.SaveChanges();
    }

    public static Guid ActionUseExt(this StoreEntity db, List<CachedUse> list, Guid userId, DateTime time, Guid operatorId, Guid storeId, string code = "")
    {
        if (list.Count == 0)
        {
            return Guid.Empty;
        }
        var gid = db.GlobalId();
        var su = new StoreUse
        {
            Id = gid,
            UserId = userId,
            Time = time,
            TimeNode = time.ToTimeNode(),
            OperationUserId = operatorId,
            OperationTime = DateTime.Now,
            UsageTarget = list.First().UserTarget,
            StoreId = storeId,
            Money = 0
        };
        db.StoreUse.Add(su);
        db.SaveChanges();
        foreach (var use in list)
        {
            decimal _money = 0;
            var objId = use.ObjectId.Value;
            var obj = db.StoreObject.Single(o => o.Id == objId);
            if (!obj.Single)
            {
                if (obj.Amount < use.Amount.Value)
                    use.Amount = obj.Amount;
                if (use.Type == "领用")
                {
                    var consume = new StoreConsume
                    {
                        Id = db.GlobalId(),
                        ObjectId = use.ObjectId.Value,
                        ConsumeUserId = userId,
                        Note = use.Note,
                        TimeNode = time.ToTimeNode(),
                        Time = time,
                        OperationUserId = operatorId,
                        OperationTime = DateTime.Now,
                        Code = code,
                        Amount = use.Amount.Value,
                        Money = 0
                    };
                    db.StoreConsume.Add(consume);
                    var counter = 0;
                    var left = consume.Amount;
                    foreach (var @in in obj.StoreIn.Where(o => o.Amount > 0).OrderBy(o => o.TimeNode))
                    {
                        counter++;
                        if (@in.Amount >= left)
                        {
                            var @single = new StoreConsumeSingle();
                            @single.Id = db.GlobalId();
                            var us = new StoreUseSingle
                            {
                                Id = db.GlobalId(),
                                UseId = gid,
                                Type = 0,
                                InId = @in.Id,
                                SingleInId = null,
                                ObjectId = obj.Id,
                                Note = use.Note,
                                Amount = left,
                                Money = (decimal.Divide(@in.SourceMoney, @in.SourceAmount)) * left,
                                SingleConsumeId = @single.Id
                            };
                            db.StoreUseSingle.Add(us);
                            consume.Money += (decimal.Divide(@in.SourceMoney, @in.SourceAmount)) * left;
                            @single.InId = @in.Id;
                            @single.ConsumeId = consume.Id;
                            @single.Ordinal = counter;
                            @single.Amount = left;
                            @single.PerPrice = @in.PerPrice;
                            @single.SourcePerPrice = @in.SourcePerPrice;
                            @single.Fee = (decimal.Divide(@in.Fee, @in.Amount)) * left;
                            @single.Money = (decimal.Divide(@in.SourceMoney, @in.SourceAmount)) * left;
                            @in.Amount -= left;
                            if (@in.Amount == 0)
                                @in.Money = 0;
                            else
                                @in.Money -= (decimal.Divide(@in.SourceMoney, @in.SourceAmount)) * left;
                            db.StoreConsumeSingle.Add(@single);
                            break;
                        }
                        else
                        {
                            left -= @in.Amount;
                            var @single = new StoreConsumeSingle();
                            @single.Id = db.GlobalId();
                            var us = new StoreUseSingle
                            {
                                Id = db.GlobalId(),
                                UseId = gid,
                                Type = 0,
                                InId = @in.Id,
                                SingleInId = null,
                                ObjectId = obj.Id,
                                Note = use.Note,
                                Amount = @in.Amount,
                                Money = @in.Amount * (decimal.Divide(@in.SourceMoney, @in.SourceAmount)),
                                SingleConsumeId = @single.Id
                            };
                            db.StoreUseSingle.Add(us);
                            consume.Money += @in.Amount * (decimal.Divide(@in.SourceMoney, @in.SourceAmount));
                            @single.InId = @in.Id;
                            @single.ConsumeId = consume.Id;
                            @single.Ordinal = counter;
                            @single.Amount = @in.Amount;
                            @single.PerPrice = @in.PerPrice;
                            @single.SourcePerPrice = @in.SourcePerPrice;
                            @single.Fee = @in.Fee;
                            @single.Money = @in.Amount * (decimal.Divide(@in.SourceMoney, @in.SourceAmount));
                            @in.Amount = 0;
                            @in.Money = 0;
                            db.StoreConsumeSingle.Add(@single);
                        }
                    }
                    _money = consume.Money;
                    su.Money += consume.Money;
                    obj.Amount -= use.Amount.Value;
                    obj.Money -= _money;
                    var flow = new StoreFlow
                    {
                        Id = db.GlobalId(),
                        ObjectId = objId,
                        UserId = operatorId,
                        Type = FlowType.领用出库,
                        TypeName = FlowType.领用出库.ToString(),
                        TimeNode = time.ToTimeNode(),
                        Time = time,
                        Amount = -use.Amount.Value,
                        Money = -consume.Money,
                        Note = use.Note
                    };
                    db.StoreFlow.Add(flow);
                    db.ActionRecord(objId, time, 0M, 0M, 0M, 0M, use.Amount.Value, _money, 0M, 0M, 0M, 0M);
                    db.SaveChanges();
                }
            }
        }
        return gid;
    }

    public static void ActionRecord(this StoreEntity db, Guid objectId, DateTime time, decimal @in, decimal inMoney, decimal lend, decimal lendMoney, decimal consume, decimal consumeMoney, decimal @out, decimal outMoney, decimal redo, decimal redoMoney)
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
            db.StoreStatistics.Add(ss);
        }
        else
        {
            var current = db.StoreStatistics.Single(o => o.ObjectId == objectId && o.Year == year && o.Month == month);
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
        foreach (var current in db.StoreStatistics.Where(o => o.ObjectId == objectId && o.TimeNode > stamp))
        {
            current.StartAmount += @in - @out - consume - lend - redo;
            current.StartMoney += inMoney - outMoney - consumeMoney - lendMoney - redoMoney;
            current.EndAmount += @in - @out - consume - lend - redo;
            current.EndMoney += inMoney - outMoney - consumeMoney - lendMoney - redoMoney;
        }
    }
}
