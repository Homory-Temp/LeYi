using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Platform.JHMobile.Models
{
    public class Corp
    {
        public const string corp_id = "ding58d580673b412e33";
        public const string corp_secret = "vcEKPEJuV7WdWdpL8RALe6pHBwPEkluO6fAQynZDVpYka3opAUgSqTvF-kFMT1ou";
        public const string corp_url = "http://i.btedu.gov.cn/DingDing/";
        public const string corp_c6 = @"E:\JINHER\C6\";
        public static readonly Dictionary<string, string> corp_messages = new Dictionary<string, string> { ["1002"] = "教育局通告", ["1005"] = "局周工作安排", ["1023"] = "教研室通告" };
    }
}
