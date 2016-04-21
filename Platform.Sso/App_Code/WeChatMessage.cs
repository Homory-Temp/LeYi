using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class WeChatMessage
{
    public string FromUserName { get; set; }
    public string ToUserName { get; set; }
    public string MsgType { get; set; }
    public string EventName { get; set; }
    public string Content { get; set; }
    public string EventKey { get; set; }
}
