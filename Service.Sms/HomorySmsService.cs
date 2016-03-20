using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using System.Web;

namespace LY.Service.Sms
{
    public partial class HomorySmsService : ServiceBase
    {
        private System.Timers.Timer _timer;

        public HomorySmsService()
        {
            this.InitializeComponent();
        }

        protected void Log(string key, string content)
        {
            string item = ConfigurationManager.AppSettings["SmsLog"];
            DateTime today = DateTime.Today;
            string str = string.Format(item, today.ToString("yyyyMMdd"));
            object[] newLine = new object[4];
            today = DateTime.Now;
            newLine[0] = today.ToString("yyyy-MM-dd HH:mm:ss");
            newLine[1] = key;
            newLine[2] = content;
            newLine[3] = Environment.NewLine;
            File.AppendAllText(str, string.Format("Time: {0}; Key: {1}; Content: {2}.{3}", newLine));
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                this.Log("服务", "开启");
                this.PeekSms();
                System.Timers.Timer timer = new System.Timers.Timer(double.Parse(ConfigurationManager.AppSettings["SmsInterval"]) * 1000)
                {
                    AutoReset = true
                };
                this._timer = timer;
                this._timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);
                this._timer.Start();
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                this.Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
        }

        protected override void OnStop()
        {
            this._timer.Stop();
            this._timer.Close();
            this.Log("Service", "Stop");
        }

        private void PeekSms()
        {
            string str;
            Exception exception;
            try
            {
                int num = int.Parse(ConfigurationManager.AppSettings["ObsoleteDays"]);
                string sys_account = ConfigurationManager.AppSettings["Account"];
                string sys_password = ConfigurationManager.AppSettings["Password"];
                DateTime now = DateTime.Now;
                now = now.AddDays((double)(-num));
                string str1 = now.ToString("yyyy-MM-dd HH:mm:ss");
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["C6"].ConnectionString);
                SqlCommand sqlCommand = new SqlCommand(string.Format("SELECT  [Sms].[SmsID], [Sms].[SmsContent], [Sms].[SmsToTel], [Sms].[SmsNO], [Users].[UserName] FROM [Sms] INNER JOIN [Users] ON  SMS.SmsUser = Users.UserId AND [Sms].[SmsState] = 0 AND [Sms].[SubTime] > '{0}'", str1), sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                List<string> strs = new List<string>();
                List<string> strs1 = new List<string>();
                while (sqlDataReader.Read())
                {
                    string str2 = sqlDataReader.GetString(0);
                    string str3 = HttpUtility.UrlEncode(sqlDataReader.GetString(1));
                    string str4 = sqlDataReader.GetString(2);
                    sqlDataReader.GetString(3).PadLeft(4, '0');
                    string str5 = sqlDataReader.GetString(4);
                    str3 = string.Format("{0}（{1}）", str3, str5);
                    str = string.Format("http://www.4001185185.com/sdk/smssdk!mt.action?sdk={3}&code={4}&phones={0}&msg={1}&resulttype=txt&subcode=2802{2}&rpt=1", str4, str3, str2, sys_account, sys_password);
                    this.Log("待发送内容", str);
                    strs1.Add(str);
                    strs.Add(str2);
                }
                sqlConnection.Close();
                sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["C6"].ConnectionString);
                sqlConnection.Open();
                try
                {
                    SqlCommand sqlCommand1 = new SqlCommand(string.Format("UPDATE [Sms] SET [SmsState] = 1 WHERE [SubTime] > '{0}'", str1), sqlConnection);
                    sqlCommand1.ExecuteNonQuery();
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    this.Log("发送完成置1错误", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
                }
                foreach (string strx in strs1)
                {
                    try
                    {
                        WebResponse response = WebRequest.CreateHttp(strx).GetResponse();
                        this.Log("接口流返回", "成功");
                        Stream responseStream = response.GetResponseStream();
                        this.Log("接口返回", (new StreamReader(responseStream)).ReadToEnd());
                        Thread.Sleep(new TimeSpan((long)100));
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        this.Log("实际调接口错误", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
                    }
                }
                try
                {
                    sqlConnection.Close();
                }
                catch
                {
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
                this.Log("取代发信息列表错误", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.PeekSms();
        }
    }
}
