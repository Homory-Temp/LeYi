<%@ WebHandler Language="C#" Class="WeChatHandler" %>

using System;
using System.Web;

public class WeChatHandler : IHttpHandler
{
    WeChat wc = new WeChat();

    public void ProcessRequest(HttpContext context)
    {
        string postString = string.Empty;
        if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
        {
            var wxmessage = wc.GetWeChatMessage();
            if (!string.IsNullOrEmpty(wxmessage.EventName) && wxmessage.EventName.Trim() == "subscribe")
            {
                var postUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&response_type=code&scope=snsapi_base&state=WeiXin#wechat_redirect", "wx5769287cfe0fd655");
                var templateId = "";
                var postData = "{\"touser\":\"" + wxmessage.FromUserName + "\",\"template_id\":\"" + templateId + "\",\"url\":\"" + postUrl + "\",\"topcolor\":\"#FF0000\","
                + "\"data\":{"
                    + "\"first\": {\"value\":\"发布一条班级活动\",\"color\":\"#173177\"},"
                    + "\"keyword1\":{\"value\":\"" + wxmessage.EventName + "\",\"color\":\"#173177\"},"
                    + "\"keyword2\":{\"value\":\"" + wxmessage.EventKey + "\",\"color\":\"#173177\"},"
                    + "\"keyword4\":{\"value\":\"xxxx\",\"color\":\"#173177\"}"
                + "}}";
                wc.RequestPlate(postData);
            }
        }
        else
        {
            wc.Valid();
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
