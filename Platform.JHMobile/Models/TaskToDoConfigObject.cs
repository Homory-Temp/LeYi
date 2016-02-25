using System;

namespace Platform.JHMobile.Models
{
    public class TaskToDoConfigObject
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Type Type { get; set; }
        public bool Visible { get; set; }
    }
}
