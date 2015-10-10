using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace LY.Service.ResourceCount
{
    public partial class HomoryResourceCountService : ServiceBase
    {
		private Timer _timer;

        public HomoryResourceCountService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
			try
			{
				Log("Service", "Start");
				CountResource();
				_timer = new Timer(double.Parse(ConfigurationManager.AppSettings["ResourceCountInterval"]) * 60 * 1000)
				{
					AutoReset = true
				};
				_timer.Elapsed += timer_Elapsed;
				_timer.Start();
			}
			catch (Exception exception)
			{
				Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
			}
        }

		private long GetDirectorySize(string path)
		{
			var di = new DirectoryInfo(path);
			long length = 0;
			foreach (var fsi in di.GetFileSystemInfos())
			{
				if (fsi.Attributes.ToString().ToLower() == "directory")
				{
					length += GetDirectorySize(fsi.FullName);
				}
				else
				{
					var fi = new FileInfo(fsi.FullName);
					length += fi.Length;
				}
			}
			return length;
		}

		private void CountResource()
		{
			try
			{
				string root = ConfigurationManager.AppSettings["ResourcePath"];
				var length = GetDirectorySize(root);
				try
				{
					var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
					var com = new System.Data.SqlClient.SqlCommand(string.Format("UPDATE [Dictionary] SET [Value] = {0} WHERE [Key] = 'ResourceAmount'", length), con);
					con.Open();
					com.ExecuteNonQuery();
					try
					{
						con.Close();
					}
					catch
					{
					}
				}
				catch (Exception ex)
				{
					Log("Error", ex.StackTrace.ToString(CultureInfo.InvariantCulture));
				}
				Log("Data", length.ToString(CultureInfo.InvariantCulture));
			}
			catch (Exception exception)
			{
				Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
			}
		}

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			CountResource();
		}

        protected override void OnStop()
        {
			_timer.Stop();
			_timer.Close();
			Log("Service", "Stop");
        }

		protected void Log(string key, string content)
		{
			var path = string.Format(ConfigurationManager.AppSettings["ResourceCountLog"], DateTime.Today.ToString("yyyyMMdd"));
			var line = string.Format("Time: {0}; Key: {1}; Content: {2}.{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), key,
				content, Environment.NewLine);
			File.AppendAllText(path, line);
		}
    }
}
