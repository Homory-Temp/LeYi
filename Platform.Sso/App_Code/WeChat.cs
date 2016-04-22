using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Xml;

public class WeChat
{
    string Token = "LXEDU";

    public WeChatMessage GetWeChatMessage()
    {

        var wx = new WeChatMessage();
        StreamReader str = new StreamReader(HttpContext.Current.Request.InputStream, Encoding.UTF8);
        XmlDocument xml = new XmlDocument();
        xml.Load(str);
        wx.ToUserName = xml.SelectSingleNode("xml").SelectSingleNode("ToUserName").InnerText;
        wx.FromUserName = xml.SelectSingleNode("xml").SelectSingleNode("FromUserName").InnerText;
        wx.MsgType = xml.SelectSingleNode("xml").SelectSingleNode("MsgType").InnerText;
        if (wx.MsgType.Trim() == "text")
        {
            wx.Content = xml.SelectSingleNode("xml").SelectSingleNode("Content").InnerText;
        }
        if (wx.MsgType.Trim() == "event")
        {
            wx.EventName = xml.SelectSingleNode("xml").SelectSingleNode("Event").InnerText;
        }
        return wx;
    }

    public void RequestPlate(string postData)
    {
        var access_token = WeChat.GetAccessToken().access_token;
        var url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token;
        HttpWebRequest request = default(System.Net.HttpWebRequest);
        Stream requestStream = default(System.IO.Stream);
        byte[] postBytes = Encoding.UTF8.GetBytes(postData.ToString());
        request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        request.ContentType = "application/x-www-form-urlencoded;charset=utf8";
        request.ContentLength = postBytes.Length;
        request.Timeout = 10000;
        request.Method = "POST";
        request.AllowAutoRedirect = false;
        requestStream = request.GetRequestStream();
        requestStream.Write(postBytes, 0, postBytes.Length);
        requestStream.Close();
    }

    public class Access_token
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }

    public static Access_token GetAccessToken()
    {
        string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + "wx5769287cfe0fd655" + "&secret=" + "7250f97d95e92aa1290f0a3915b75906";
        Access_token mode = new Access_token();
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);
        req.Method = "GET";
        using (WebResponse wr = req.GetResponse())
        {
            HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            Access_token token = new Access_token();
            token = JsonHelper.ParseFromJson<Access_token>(content);
            mode.access_token = token.access_token;
            mode.expires_in = token.expires_in;
        }
        return mode;
    }

    private bool CheckSignature()
    {
        string signature = HttpContext.Current.Request.QueryString["signature"].ToString();
        string timestamp = HttpContext.Current.Request.QueryString["timestamp"].ToString();
        string nonce = HttpContext.Current.Request.QueryString["nonce"].ToString();
        string[] ArrTmp = { Token, timestamp, nonce };
        Array.Sort(ArrTmp);     //字典排序  
        string tmpStr = string.Join("", ArrTmp);
        tmpStr =FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
        tmpStr = tmpStr.ToLower();
        if (tmpStr == signature)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Valid()
    {
        string echoStr = HttpContext.Current.Request.QueryString["echoStr"].ToString();
        if (CheckSignature())
        {
            if (!string.IsNullOrEmpty(echoStr))
            {
                HttpContext.Current.Response.Write(echoStr);
                HttpContext.Current.Response.End();
            }
        }
    }

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

    public UserInfo GetUserInfoByOpenId(string access_token, string openId)
    {
        UserInfo ui = new UserInfo();
        var url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", access_token, openId);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Accept = "*/*";
        request.KeepAlive = true;
        request.ContentType = "application/x-www-form-urlencoded";
        request.Method = "GET";
        Encoding encoding = Encoding.UTF8;
        request.ContentLength = 0;
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream responseStream = response.GetResponseStream();
        StreamReader streamReader = new StreamReader(responseStream, encoding);
        string retString = streamReader.ReadToEnd();
        streamReader.Close();
        responseStream.Close();
        JavaScriptSerializer js = new JavaScriptSerializer();
        ui = js.Deserialize<UserInfo>(retString);
        return ui;
    }

    public class UserInfo
    {
        public string subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_time { get; set; }
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

public class JsonHelper
{
    public static string GetJson<T>(T obj)
    {
        DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
        using (MemoryStream stream = new MemoryStream())
        {
            json.WriteObject(stream, obj);
            string szJson = Encoding.UTF8.GetString(stream.ToArray());
            return szJson;
        }
    }

    public static T ParseFromJson<T>(string szJson)
    {
        T obj = Activator.CreateInstance<T>();
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(szJson)))
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            return (T)serializer.ReadObject(ms);
        }
    }
}
