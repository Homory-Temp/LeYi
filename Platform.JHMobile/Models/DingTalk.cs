using System;
using System.Collections.Generic;
using System.Web.Configuration;

namespace Platform.JHMobile.Models
{
    public class DingTalk
    {
        public static string CorpId = WebConfigurationManager.AppSettings["corp_id"];

        public static string CorpSecret = WebConfigurationManager.AppSettings["corp_secret"];

        public static string CorpUrl = WebConfigurationManager.AppSettings["corp_url"];

        public static string CorpJinHer = WebConfigurationManager.AppSettings["corp_jinher"];

        public static Dictionary<string, string> CorpUserMappings
        {
            get
            {
                var modules = WebConfigurationManager.AppSettings["corp_user_mappings"];
                var dict = new Dictionary<string, string>();
                foreach (var pair in modules.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var array = pair.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    dict.Add(array[0], array[1]);
                }
                return dict;
            }
        }

        public static Dictionary<string, string> CorpMessageModules
        {
            get
            {
                var modules = WebConfigurationManager.AppSettings["corp_message_modules"];
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
