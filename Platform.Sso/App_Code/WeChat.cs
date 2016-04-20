using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

public class WeChat
{
    public string GetOpenId(string code)
    {
        var url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + "wx5769287cfe0fd655" + "&secret=" + "7250f97d95e92aa1290f0a3915b75906" + "&code=" + code + "&grant_type=authorization_code";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Accept = "*/*";
        request.KeepAlive = true;
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "GET";
        var encoding = Encoding.UTF8;
        request.ContentLength = 0;
        var response = (HttpWebResponse)request.GetResponse();
        var responseStream = response.GetResponseStream();
        var streamReader = new StreamReader(responseStream, encoding);
        string retString = streamReader.ReadToEnd();
        streamReader.Close();
        responseStream.Close();
        JavaScriptSerializer js = new JavaScriptSerializer();
        OAuthClass oac = js.Deserialize<OAuthClass>(retString);
        return oac.openid;
    }

    public class OAuthClass
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }

    }
}
