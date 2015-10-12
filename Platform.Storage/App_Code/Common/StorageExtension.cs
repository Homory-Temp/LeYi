using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public static class StorageExtension
{
    //private static JsonSerializerSettings jsonSetting = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.Objects };

    //public static string ToJson(this object entity)
    //{
    //    return JsonConvert.SerializeObject(entity, jsonSetting);
    //}

    //public static T FromJson<T>(this string json)
    //{
    //    return JsonConvert.DeserializeObject<T>(json, jsonSetting);
    //}

    //public static string Text(this RadTextBox input)
    //{
    //    return input.Text;
    //}

    //public static string Text(this RadComboBox input)
    //{
    //    return input.Text;
    //}

    //public static int Value(this RadNumericTextBox input, int @default)
    //{
    //    return input.Value.HasValue ? (int)input.Value.Value : @default;
    //}

    //public static int? Value(this RadNumericTextBox input, int? @default)
    //{
    //    return input.Value.HasValue ? (int)input.Value.Value : @default;
    //}

    //public static decimal Value(this RadNumericTextBox input, decimal @default)
    //{
    //    return input.Value.HasValue ? (decimal)input.Value.Value : @default;
    //}

    //public static decimal? Value(this RadNumericTextBox input, decimal? @default)
    //{
    //    return input.Value.HasValue ? (decimal)input.Value.Value : @default;
    //}

    //public static int Value(this RadDatePicker input)
    //{
    //    return input.SelectedDate.HasValue ? int.Parse(input.SelectedDate.Value.ToString("yyyyMMdd")) : int.Parse(DateTime.Today.ToString("yyyyMMdd"));
    //}

    //public static T Find<T>(this RadListViewCommandEventArgs e, string name)
    //    where T : Control
    //{
    //    return (T)e.ListViewItem.FindControl(name);
    //}

    //public static string GetKey(this RadListViewCommandEventArgs e, string name)
    //{
    //    return (e.ListViewItem as RadListViewEditableItem).GetDataKeyValue(name).ToString();
    //}

    //public static string GetArgs(this object sender)
    //{
    //    return (sender as IButtonControl).CommandArgument.ToString();
    //}

    //public static string GetQuery(this string key, bool decode)
    //{
    //    return decode ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[key]) : HttpContext.Current.Request.QueryString[key];
    //}

    //public static bool None(this string value)
    //{
    //    return string.IsNullOrEmpty(value);
    //}

    //public static string ToDate(this int timeNode, string joint)
    //{
    //    return string.Format("{0}{1}{2}{1}{3}", timeNode.ToString().Substring(0, 4), joint, timeNode.ToString().Substring(4, 2), timeNode.ToString().Substring(6, 2));
    //}

    //public static string ToF2(this object value)
    //{
    //    return ((decimal)value).ToString("F2");
    //}

    //public static string Lined(this string value)
    //{
    //    return value.Replace("\r\n", "<br />");
    //}
}
