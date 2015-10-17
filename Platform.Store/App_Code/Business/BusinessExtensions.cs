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
}
