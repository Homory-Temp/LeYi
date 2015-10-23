<%@ Application Language="C#" %>
<%@ Import Namespace="Models" %>

<script RunAt="server">

    protected Lazy<DepotEntities> db = new Lazy<DepotEntities>(() => new DepotEntities());

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        var id = Guid.Parse("caace587-8c34-4075-8758-08d1af4862da");
        Application["Sso"] = db.Value.Application.Single(o => o.Id == id).Home.Replace("Go/SignOn", "");
        var cid = Guid.Parse("59f2e587-8c78-4de6-b138-08d2db6c4bf8");
        Application["Depot"] = db.Value.Application.Single(o => o.Id == cid).Home.Replace("Depot/Home", "");
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
