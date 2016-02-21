using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace Platform.JHMobile.Models
{
    public class DingTalk
    {
        public static string corp_id = WebConfigurationManager.AppSettings["corp_id"];
        public static string corp_secret = WebConfigurationManager.AppSettings["corp_secret"];
        public static string corp_url = WebConfigurationManager.AppSettings["corp_url"];
        public static string corp_jinher = WebConfigurationManager.AppSettings["corp_jinher"];
        public static Dictionary<string, string> corp_modules
        {
            get
            {
                var modules = WebConfigurationManager.AppSettings["corp_modules"];
                var dict = new Dictionary<string, string>();
                foreach (var pair in modules.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var array = pair.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    dict.Add(array[0], array[1]);
                }
                return dict;
            }
        }
    }
}
