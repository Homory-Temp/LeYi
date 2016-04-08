using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SmsReply : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var last = base.Request.QueryString.ToString();
        this.Log(last);
        if (Session["Duplicate"] != null && Session["Duplicate"].ToString() == last)
            return;
        Session["Duplicate"] = last;
        try
        {
            string item = base.Request.QueryString["subcode"].Substring(WebConfigurationManager.AppSettings["SubCode"].Length);
            string str = base.Server.UrlDecode(base.Request.QueryString["message"]);
            string str1 = "";
            string str2 = "";
            string str3 = "";
            SqlConnection sqlConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["C6"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(string.Format("SELECT TOP 1 SmsUser, SmsToUser FROM Sms WHERE SmsID LIKE '%{0}' ORDER BY SubTime DESC", item), sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                str1 = sqlDataReader.GetString(0);
                str2 = sqlDataReader.GetString(1);
            }
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
            try
            {
                var sqlCommandx = new SqlCommand(string.Format("SELECT UserName FROM Users WHERE UserID='{0}'", str2), sqlConnection);
                sqlDataReader = sqlCommandx.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    str3 = sqlDataReader.GetString(0);
                }
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
            }
            catch
            {
            }
            str = string.Concat(str, string.Format("（{0}）", str3));
            var sqlCommandy = new SqlCommand(string.Format("EXECUTE [dbo].[JHHR_SendCall] '{0}', '{1}', 'System'", str1, str), sqlConnection);
            sqlCommandy.ExecuteNonQuery();
            try
            {
                sqlConnection.Close();
            }
            catch
            {
            }
            this.Log("Reply-Yes");
        }
        catch
        {
            this.Log("Reply-No");
        }
        this.Log(base.Request.QueryString.ToString());
        base.Response.Write("OK");
    }

    protected void Log(string content)
    {
        string item = WebConfigurationManager.AppSettings["SmsReply"];
        DateTime today = DateTime.Today;
        string str = string.Format(item, today.ToString("yyyyMMdd"));
        DateTime now = DateTime.Now;
        string str1 = string.Format("Time: {0}; Request: {1}{2}", now.ToString("yyyy-MM-dd HH:mm:ss"), content, Environment.NewLine);
        File.AppendAllText(base.Server.MapPath(str), str1);
    }
}
