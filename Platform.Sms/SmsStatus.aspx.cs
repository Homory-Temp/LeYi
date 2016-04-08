using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SmsStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var last = base.Request.QueryString.ToString();
        this.Log(last);
        try
        {
            string str = base.Request.QueryString["subcode"].Substring(WebConfigurationManager.AppSettings["SubCode"].Length);
            base.Server.UrlDecode(base.Request.QueryString["message"]);
            string str1 = "";
            string str2 = "";
            this.Log(string.Format("SELECT TOP 1 寻呼标识, 接收用户 FROM CallSms WHERE 短信标识 LIKE '%{0}' ORDER BY 时间 DESC", str));
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["C6"].ConnectionString);
            sqlConnection.Open();
            this.Log(string.Format("Connection Open"));
            SqlCommand sqlCommand = new SqlCommand(string.Format("SELECT TOP 1 寻呼标识, 接收用户 FROM CallSms WHERE 短信标识 LIKE '%{0}' ORDER BY 时间 DESC", str), sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                str1 = sqlDataReader.GetString(0);
                str2 = sqlDataReader.GetString(1);
            }
            this.Log(string.Format("Data Read"));
            if (!sqlDataReader.IsClosed)
            {
                try
                {
                    sqlDataReader.Close();
                }
                catch
                {
                }
            }
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                this.Log("Status-NotFound");
            }
            else
            {
                try
                {
                    DateTime now = DateTime.Now;
                    this.Log(string.Format("UPDATE CallDetail SET CallRead = 1, CallReadTime = '{0}' WHERE CallID = '{1}' AND CallDUser = '{2}'", now.ToString("yyyy-MM-dd HH:mm:ss"), str1, str2));
                    var sqlCommandx = new SqlCommand(string.Format("UPDATE CallDetail SET CallRead = 1, CallReadTime = '{0}' WHERE CallID = '{1}' AND CallDUser = '{2}'", now.ToString("yyyy-MM-dd HH:mm:ss"), str1, str2), sqlConnection);
                    sqlCommandx.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    this.Log(string.Concat("Exception：", ex.StackTrace));
                }
                this.Log("Status-Yes");
            }
            try
            {
                sqlConnection.Close();
            }
            catch (Exception exception)
            {
                this.Log(string.Concat("Exception：", exception.StackTrace));
            }
        }
        catch (Exception ex)
        {
            this.Log("Status-No" );
            this.Log(string.Concat("Exception：", ex.StackTrace));
        }
        base.Response.Write("OK");
    }

    protected void Log(string content)
    {
        string item = WebConfigurationManager.AppSettings["SmsStatus"];
        DateTime today = DateTime.Today;
        string str = string.Format(item, today.ToString("yyyyMMdd"));
        DateTime now = DateTime.Now;
        string str1 = string.Format("Time: {0}; Request: {1}{2}", now.ToString("yyyy-MM-dd HH:mm:ss"), content, Environment.NewLine);
        File.AppendAllText(base.Server.MapPath(str), str1);
    }
}
