﻿<%@ Application Language="C#" %>
<%@ Import Namespace="Models" %>

<script RunAt="server">

    protected Lazy<StoreEntity> db = new Lazy<StoreEntity>(() => new StoreEntity());

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        var id = Guid.Parse("caace587-8c34-4075-8758-08d1af4862da");
        Application["Sso"] = db.Value.Application.Single(o => o.Id == id).Home.Replace("Go/SignOn", "");
        var cid = Guid.Parse("233BE587-8C6E-4A3E-99E0-08D29ECB798E");
        Application["Store"] = db.Value.Application.Single(o => o.Id == cid).Home.Replace("Store/Store", "");
    }

    void Application_End(object sender, EventArgs e)
    {

    }
    
    void Application_PostAuthorizeRequest()
    {
        HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
    }

    void Application_Error(object sender, EventArgs e)
    {

    }

    void Session_Start(object sender, EventArgs e)
    {

    }

    void Session_End(object sender, EventArgs e)
    {

    }

</script>
