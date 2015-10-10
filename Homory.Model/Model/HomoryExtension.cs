using System;
using System.Linq;
using System.Web.Configuration;
using Newtonsoft.Json;
using System.Threading;

namespace Homory.Model
{
	public static class HomoryExtension
	{
		public static Guid GetId(this Entities db)
		{
			var timeArray = BitConverter.GetBytes(DateTime.UtcNow.Ticks).Reverse().ToArray();
			var guidArray = Guid.NewGuid().ToByteArray();
			guidArray[0] = 0x87;
			guidArray[1] = 0xe5;
			guidArray[5] = 0x8c;
			for (var i = 2; i < 4; i++)
				guidArray[i] = timeArray[9 - i];
			for (var i = 10; i < 16; i++)
				guidArray[i] = timeArray[i - 10];
			return new Guid(guidArray);
		}

		public static DateTime GetTime(this Guid id)
		{
			var guidArray = id.ToByteArray();
			var timeArray = new byte[8];
			for (var i = 10; i < 16; i++)
				timeArray[i - 10] = guidArray[i];
			for (var i = 2; i < 4; i++)
				timeArray[9 - i] = guidArray[i];
			timeArray = timeArray.Reverse().ToArray();
			return new DateTime(BitConverter.ToInt64(timeArray, 0));
		}

		public static string ToJson(this object entity)
		{
			return JsonConvert.SerializeObject(entity);
		}

		public static T FromJson<T>(this string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		public static string FromWebConfig(this string configName)
		{
			return WebConfigurationManager.AppSettings[configName];
		}

        public static string ToHomoryUrl(this string relativeUrl, string configName)
		{
			return string.Concat(configName.FromWebConfig(), relativeUrl);
		}
	}
}
