using System.Collections.Generic;

namespace Platform.JHMobile.Models
{
    public class 待办工作办理内容
    {
        public f______列表待办单条表_Result 对象 { get; set; }
        public string 类型 { get; set; }
        public string 文本 { get; set; }
        public List<f______列表待办流程表_Result> 流程 { get; set; }
        public List<f______列表待办下步表_Result> 按钮 { get; set; }
    }
}
