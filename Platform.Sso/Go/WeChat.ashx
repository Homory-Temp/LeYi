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
                string content = "/:rose  感谢关注梁溪教育微信服务平台，平台将不断提升功能，努力服务好学校师生、全体家长及关心梁溪教育的广大人民群众。\n        因崇安、南长、北塘三区合并，本微信号名称暂时沿用原北塘教育微信号，将在下阶段待相关资料齐全后再办理更名验证，特此说明。\n        请首次关注本微信号的梁溪教育系统教职工，通过底部菜单进行“在职登记”。";
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
