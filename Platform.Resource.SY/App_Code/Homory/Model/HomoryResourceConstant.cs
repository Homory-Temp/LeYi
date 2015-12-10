using System;

namespace Homory.Model
{
	public class HomoryResourceConstant
	{
		public const string SessionUserId = "0F959959-F835-6E6C-382C-6BB05FCBCD83";
		public const string SessionPeekId = "54097E1E-6DCA-AED0-8312-21D8C9B653EF";
		public static readonly Guid ApplicationKey = Guid.Parse("CE43E587-8CC0-4A1C-BF9E-08D1AF495D0C");
		public const int GradeMonth = 8;
		public static readonly string[] CourseNames = { "语文", "数学", "英语", "音乐", "体育", "美术", "综合" };
		public static readonly Guid CourseOtherId = Guid.Parse("F0B82122-4E2F-3522-22D7-9E5A7FFA8B13");
		public static readonly string[] GradeNames = { "一年级", "二年级", "三年级", "四年级", "五年级", "六年级", "七年级", "八年级", "九年级" };
	}
}
