<%@ WebHandler Language="C#" Class="WeChatHandler" %>

using System;
using System.Web;

public class WeChatHandler : IHttpHandler
{
    WeChat wc = new WeChat();

    public void ProcessRequest(HttpContext context)
    {
        string text = string.Empty;
        if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
        {
            var wcm = wc.GetWeChatMessage();
            if (!string.IsNullOrEmpty(wcm.EventName) && wcm.EventName.Trim() == "subscribe")
            {
                string content = "/:rose 感谢关注梁溪教育微信号 /:rose\n在职教工请进行“在职登记”以绑定身份！";
                text = wc.sendTextMessage(wcm, content);
            }
        }
        else
        {
            wc.Valid();
        }
        HttpContext.Current.Response.Write(text);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
