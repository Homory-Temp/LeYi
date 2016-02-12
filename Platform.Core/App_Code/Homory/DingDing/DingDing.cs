using Homory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

public class DingDing
{
    public static bool Ding
    {
        get
        {
            return bool.Parse(WebConfigurationManager.AppSettings["Ding"]);
        }
    }

    public static string Ding_CorpId
    {
        get
        {
            return WebConfigurationManager.AppSettings["Ding_CorpId"];
        }
    }

    public static string Ding_CorpSecret
    {
        get
        {
            return WebConfigurationManager.AppSettings["Ding_CorpSecret"];
        }
    }

    public static string Ding_UrlAccessToken
    {
        get
        {
            return WebConfigurationManager.AppSettings["Ding_UrlAccessToken"];
        }
    }

    public static string Ding_UrlDepartmentAdd
    {
        get
        {
            return WebConfigurationManager.AppSettings["Ding_UrlDepartmentAdd"];
        }
    }

    public static void Ding_Department(Department campus)
    {

    }
}
