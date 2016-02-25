using System.Collections.Generic;

namespace Platform.JHMobile.Models
{
    public class TaskDoneObject
    {
        public f____Mobile_List_TaskDoneSingle_Result Object { get; set; }
        public List<dynamic> Form { get; set; }
        public List<TaskDoneConfigObject> Config { get; set; }
        public List<f____Mobile_List_TaskDoneFlow_Result> Flow { get; set; }
    }
}
