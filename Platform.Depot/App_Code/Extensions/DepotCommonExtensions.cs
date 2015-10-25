using Models;
using System;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

public static class DepotCommonExtensions
{
    public static string Formatted(this string format, params object[] objects)
    {
        return string.Format(format, objects);
    }

    public static string EmptyWhenZero(this object value, string prefix = "（", string suffix = "）")
    {
        return (value == null || value.ToString().None() || value.ToString() == "0") ? string.Empty : "{0}{1}{2}".Formatted(prefix, value.ToString(), suffix);
    }

    public static string WhenZero(this object value, string content = "")
    {
        return (value == null || value.ToString().None() || value.ToString() == "0") ? content : value.ToString();
    }

    public static bool Featured(this Depot depot, DepotType type)
    {
        return (depot.Type & type) > DepotType.无;
    }

    public static string Query(this string key, bool decode = false)
    {
        return decode ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[key]) : HttpContext.Current.Request.QueryString[key];
    }

    public static string ToDay(this object time)
    {
        return DateTime.Parse(time.ToString()).ToString("yyyy-MM-dd");
    }

    public static string ToAmount(this object amount, bool dotted = false)
    {
        return decimal.Parse(amount.ToString()).ToString(dotted ? "F2" : "F0");
    }

    public static string ToMoney(this object money)
    {
        return decimal.Parse(money.ToString()).ToString("F2");
    }

    public static string GetFirstChar(this string value)
    {
        return value.None() ? string.Empty : value[0].ToString();
    }

    public static bool None(this object value)
    {
        return value == null || string.IsNullOrEmpty(value.ToString().Trim());
    }

    public static int PeekValue(this RadComboBox control, int @default, bool ignoreFirst)
    {
        return control.SelectedIndex < (ignoreFirst ? 1 : 0) ? @default : int.Parse(control.SelectedValue);
    }

    public static string PeekValue(this RadComboBox control, string @default, bool ignoreFirst)
    {
        return control.SelectedIndex < (ignoreFirst ? 1 : 0) ? @default : control.SelectedValue;
    }

    public static int PeekValue(this RadTreeView control, int @default)
    {
        return control.SelectedValue.None() ? @default : int.Parse(control.SelectedValue);
    }

    public static string PeekValue(this RadButton control, bool detectCheckState = false)
    {
        return detectCheckState ? (control.Checked ? control.Value : string.Empty) : control.Value;
    }

    public static string PeekRadioValue(this RadButton[] controls, string @default)
    {
        return controls.Count(o => o.Checked) == 1 ? controls.Single(o => o.Checked).Value : @default;
    }

    public static int PeekRadioValue(this RadButton[] controls, int @default)
    {
        return controls.Count(o => o.Checked) == 1 ? int.Parse(controls.Single(o => o.Checked).Value) : @default;
    }

    public static Guid GlobalId(this object id)
    {
        return Guid.Parse(id.ToString());
    }

    public static int PeekValue(this RadNumericTextBox control, int @default)
    {
        return control.Value.HasValue ? (int)control.Value.Value : @default;
    }

    public static decimal PeekValue(this RadNumericTextBox control, decimal @default)
    {
        return control.Value.HasValue ? (decimal)control.Value.Value : @default;
    }
}
