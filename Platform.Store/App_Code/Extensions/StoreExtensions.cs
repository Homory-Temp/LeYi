using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

public static class StoreExtensions
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

    public static bool Null(this object value)
    {
        return value == null || string.IsNullOrWhiteSpace(value.ToString());
    }

    public static string Query(this string key, bool decode = false)
    {
        return decode ? HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString[key]) : HttpContext.Current.Request.QueryString[key];
    }

    public static Guid GlobalId(this string id)
    {
        return id.Null() ? Guid.Empty : Guid.Parse(id);
    }

    public static string EmptyWhenZero(this int value, string prefix = "（", string suffix = "）")
    {
        return value == 0 ? string.Empty : "{0}{1}{2}".Formatted(prefix, value, suffix);
    }

    public static string Formatted(this string format, object @object)
    {
        return string.Format(format, @object);
    }

    public static string Formatted(this string format, params object[] objects)
    {
        return string.Format(format, objects);
    }

    public static string ToAmount(this object amount)
    {
        var temp = ((decimal)amount).ToString("F2").Reverse();
        var sb = new StringBuilder();
        bool ext = false;
        foreach (var c in temp)
        {
            if (c == '0')
            {
                if (ext)
                {
                    sb.Append(c);
                }
            }
            else
            {
                sb.Append(c);
                ext = true;
            }
        }
        temp = sb.ToString().Reverse();
        sb = new StringBuilder();
        foreach (var c in temp)
        {
            sb.Append(c);
        }
        if (sb.ToString().EndsWith("."))
            sb.Replace(".", "");
        return sb.ToString();
    }
}
