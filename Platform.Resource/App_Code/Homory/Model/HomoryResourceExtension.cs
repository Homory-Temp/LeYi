using System;
using System.Linq;

namespace Homory.Model
{
	public static class ModelExtension
	{
		public static void ST_Resource(this Entities db, Guid id, ResourceOperationType operationType, decimal score = 0M)
		{
			var today = DateTime.Today;
			var year = today.Year;
			var month = today.Month;
			var count = db.ResourceStatisticsMonthly.Count(o => o.Year == year && o.Month == month && o.Id == id);
			if (count == 0)
			{
				db.ResourceStatisticsMonthly.Add(new ResourceStatisticsMonthly { Id = id, Year = year, Month = month, TimeStamp = DateTime.Now });
				db.SaveChanges();
			}
			var ops = db.ResourceStatisticsMonthly.First(o => o.Year == year && o.Month == month && o.Id == id);
			switch (operationType)
			{
				case ResourceOperationType.Comment:
					ops.Comment++;
					break;
				case ResourceOperationType.Download:
					ops.Download++;
					break;
				case ResourceOperationType.Favourite:
					ops.Favourite++;
					break;
				case ResourceOperationType.Grade:
					ops.Grade += score;
					break;
				case ResourceOperationType.Rate:
					ops.Rate++;
					break;
				case ResourceOperationType.View:
					ops.View++;
					break;
			}
		}

        public static void ST_ResourceX(this Entities db, Guid id, ResourceOperationType operationType, decimal score = 0M)
        {
            var today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var count = db.ResourceStatisticsMonthly.Count(o => o.Year == year && o.Month == month && o.Id == id);
            if (count == 0)
            {
                db.ResourceStatisticsMonthly.Add(new ResourceStatisticsMonthly { Id = id, Year = year, Month = month, TimeStamp = DateTime.Now });
                db.SaveChanges();
            }
            var ops = db.ResourceStatisticsMonthly.First(o => o.Year == year && o.Month == month && o.Id == id);
            switch (operationType)
            {
                case ResourceOperationType.Comment:
                    ops.Comment--;
                    break;
                case ResourceOperationType.Download:
                    ops.Download--;
                    break;
                case ResourceOperationType.Favourite:
                    ops.Favourite--;
                    break;
                case ResourceOperationType.Grade:
                    ops.Grade -= score;
                    break;
                case ResourceOperationType.Rate:
                    ops.Rate--;
                    break;
                case ResourceOperationType.View:
                    ops.View--;
                    break;
            }
        }

        public static void LogOp(this Entities db, Guid userId, Guid campusId, ResourceLogType logType, int? value = null)
        {
            var today = DateTime.Today;
            var year = today.Year;
            var month = today.Month;
            var count = db.ResourceLog.Count(o => o.Year == year && o.Month == month && o.Id == userId);
            if (count == 0)
            {
                db.ResourceLog.Add(new ResourceLog
                {
                    Id = userId,
                    Year = year,
                    Month = month,
                    Article = 0,
                    Courseware = 0,
                    Paper = 0,
                    Media = 0,
                    View = 0,
                    Favourite = 0,
                    Download = 0,
                    Comment = 0,
                    Reply = 0,
                    Rate = 0,
                    Credit = 0,
                    TimeStamp = DateTime.Now,
                    CampusId = campusId
                });
                db.SaveChanges();
            }
            var ops = db.ResourceLog.First(o => o.Year == year && o.Month == month && o.Id == userId);
            var dict = db.Dictionary.ToList();
            switch (logType)
            {
                case ResourceLogType.下载资源:
                    ops.Download++;
                    break;
                case ResourceLogType.个人积分:
                    ops.Credit += value ?? 0;
                    break;
                case ResourceLogType.发布文章:
                    ops.Article++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditPublish").Value);
                    break;
                case ResourceLogType.发布视频:
                    ops.Media++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditPublish").Value);
                    break;
                case ResourceLogType.发布试卷:
                    ops.Paper++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditPublish").Value);
                    break;
                case ResourceLogType.发布课件:
                    ops.Courseware++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditPublish").Value);
                    break;
                case ResourceLogType.回复评论:
                    ops.Reply++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditReply").Value);
                    break;
                case ResourceLogType.收藏资源:
                    ops.Favourite++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditFavourite").Value);
                    break;
                case ResourceLogType.浏览资源:
                    ops.View++;
                    break;
                case ResourceLogType.评定资源:
                    ops.Rate++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditRate").Value);
                    break;
                case ResourceLogType.评论资源:
                    ops.Comment++;
                    ops.Credit += int.Parse(dict.First(o => o.Key == "CreditComment").Value);
                    break;
            }
            db.SaveChanges();
        }

        public static string CutString(this string text, int? maxLength, string suffix = "")
		{
			if (string.IsNullOrWhiteSpace(text))
				return string.Empty;
			if (!maxLength.HasValue)
				maxLength = text.Length - 1;
			return text.Length > maxLength
				? string.Format("{0}{1}", text.Substring(0, maxLength.Value), suffix)
				: text;
		}

		public static string CutString(this string text, int maxLength)
		{
			return text.CutString(maxLength, "...");
		}

		public static string FormatTime(this DateTime time)
		{
			if (time.Date == DateTime.Today)
			{
				return time.ToString("HH:mm");
			}
			if ((DateTime.Today - time).TotalDays < 1)
				return "昨天";
			return (DateTime.Today - time).TotalDays < 2 ? "前天" : time.ToString("yyyy-MM-dd");
		}

		public static string FormatTimeShort(this DateTime time)
		{
			if (time.Date == DateTime.Today)
			{
				return time.ToString("HH:mm");
			}
			if ((DateTime.Today - time).TotalDays < 1)
				return "昨天";
			return (DateTime.Today - time).TotalDays < 2 ? "前天" : time.ToString("MM/dd");
		}

        public static string FormatTimeShortSecond(this DateTime time)
        {
            if (time.Date == DateTime.Today)
            {
                return time.ToString("HH:mm:ss");
            }
            if ((DateTime.Today - time).TotalDays < 1)
                return "昨天";
            return (DateTime.Today - time).TotalDays < 2 ? "前天" : time.ToString("MM/dd");
        }
    }
}
