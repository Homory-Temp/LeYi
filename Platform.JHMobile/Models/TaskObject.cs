using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.JHMobile.Models
{
    public class TaskObject
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Step { get; set; }
        public string Template { get; set; }
        public List<待办事项按钮_Result> Buttons { get; set; }
        public dynamic Data { get; set; }
    }

    public class TaskObject_5e58b0a3692d4069ae0652dd2c3e3abc
    {
        public string 键 { get; set; }
        public string 值 { get; set; }
    }
}
