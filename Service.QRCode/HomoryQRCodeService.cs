using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace LY.Service.QRCode
{
    public partial class HomoryQRCodeService : ServiceBase
    {
		private Timer _timer;

        public HomoryQRCodeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
			try
			{
				Log("Service", "Start");
				QR();
				_timer = new Timer(double.Parse(ConfigurationManager.AppSettings["CodeInterval"]) * 1000)
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

		private void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			QR();
		}

        protected override void OnStop()
        {
			_timer.Stop();
			_timer.Close();
			Log("Service", "Stop");
        }

		protected void Log(string key, string content)
		{
			var path = string.Format(ConfigurationManager.AppSettings["CodeLog"], DateTime.Today.ToString("yyyyMMdd"));
			var line = string.Format("Time: {0}; Key: {1}; Content: {2}.{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), key,
				content, Environment.NewLine);
			File.AppendAllText(path, line);
		}

        public class DepotCode
        {
            public Guid DepotId { get; set; }
            public Guid BatchId { get; set; }
            public int BatchOrdinal { get; set; }
            public string CodeJson { get; set; }
            public DateTime Time { get; set; }
            public int State { get; set; }
        }

        private void QR()
        {
            try
            {
                try
                {
                    var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
                    var com = new System.Data.SqlClient.SqlCommand("SELECT DepotId, BatchId, BatchOrdinial, CodeJson, Time, [State] FROM DepotCode WHERE [State] = 2", con);
                    var list = new List<DepotCode>();
                    con.Open();
                    var reader = com.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(
                            new DepotCode
                            {
                                DepotId = Guid.Parse(reader[0].ToString()),
                                BatchId = Guid.Parse(reader[1].ToString()),
                                BatchOrdinal = int.Parse(reader[2].ToString()),
                                CodeJson = reader[3].ToString(),
                                Time = DateTime.Parse(reader[4].ToString()),
                                State = int.Parse(reader[5].ToString())
                            });
                    }
                    try
                    {
                        reader.Close();
                    }
                    catch
                    {
                    }
                    try
                    {
                        con.Close();
                    }
                    catch
                    {
                    }
                    Log("ToQRCount", list.Count.ToString());
                }
                catch (Exception ex)
                {
                    Log("Error", ex.StackTrace.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception exception)
            {
                Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
