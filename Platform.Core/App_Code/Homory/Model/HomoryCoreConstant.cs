using System;
using System.Drawing;

namespace Homory.Model
{
    public class HomoryCoreConstant
    {
        public const string SessionUserId = "5D9E8742-3AD8-B30F-728D-B0ABAFB4BB1A";
        public const string SessionStudentsId = "91b6e7e8-752b-4936-8ca2-806cb1009a8c";
        public const string SessionTeachersId = "bd329528-650b-4b48-bc33-8be4f9124260";
        public static readonly Guid ApplicationKey = Guid.Parse("3047E587-8CC1-4645-8536-08D1AF49409F");
        public const string RightEveryone = "Everyone";
        public const string RightGlobal = "Global";
        public const string RightMoveDepartment = "MoveDepartment";
        public const string RightMoveUser = "MoveUser";
        public static readonly Color Color启用 = Color.FromArgb(0x46, 0x88, 0x47);
        public static readonly Color Color停用 = Color.FromArgb(0xB9, 0x4A, 0x48);
        public static readonly Color Color审核 = Color.FromArgb(0xC0, 0x98, 0x5C);
        public static readonly Color Color其他 = Color.FromArgb(0x56, 0x4F, 0x8A);
        public const int GradeMonth = 8;
        public static readonly string[] CourseNames = { "语文", "数学", "英语", "音乐", "体育", "美术", "综合" };
        public static readonly Guid CourseOtherId = Guid.Parse("F0B82122-4E2F-3522-22D7-9E5A7FFA8B13");
        public static readonly string[] GradeNames = { "一年级", "二年级", "三年级", "四年级", "五年级", "六年级", "七年级", "八年级", "九年级" };

        public dynamic C = new System.Dynamic.ExpandoObject();
    }
}
