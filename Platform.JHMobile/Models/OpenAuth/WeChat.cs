using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Platform.JHMobile.Models
{
    public class WeChat
    {
        public static object OpenId
        {
            get
            {
                return HttpContext.Current.Session["WeChatOpenId"];
            }
            set
            {
                HttpContext.Current.Session["WeChatOpenId"] = value;
            }
        }

        public string GetOpenAuthId(string code)
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
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream, encoding);
            string result = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            return obj.openid.ToString();
        }
    }
}
