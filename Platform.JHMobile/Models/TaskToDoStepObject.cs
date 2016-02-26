using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.JHMobile.Models
{
    public class TaskToDoStepObject
    {
        public int AppID { get; set; }
        public string Type { get; set; }
        public string StepText { get; set; }
        public List<f____Mobile_List_TaskToDoSingleButtonNext_Result> Next { get; set; }
    }
}
