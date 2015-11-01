using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

public static class DepotCommonExtensions
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

    public static bool None(this object value)
    {
        return value == null || string.IsNullOrEmpty(value.ToString().Trim());
    }

    public static int PeekValue(this RadComboBox control, int @default, bool ignoreFirst)
    {
        return control.SelectedIndex < (ignoreFirst ? 1 : 0) ? @default : int.Parse(control.SelectedValue);
    }

    public static Guid GlobalId(this object id)
    {
        return Guid.Parse(id.ToString());
    }
}
