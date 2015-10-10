<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Http" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Homory.Startup" %>
<%@ Import Namespace="Homory.Model" %>

<script RunAt="server">

    protected Lazy<Entities> HomoryContext = new Lazy<Entities>(() => new Entities());

    void Application_Start(object sender, EventArgs e)
    {
        AreaRegistration.RegisterAllAreas();
        GlobalConfiguration.Configure(ApiConfig.Register);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        var id = Guid.Parse("caace587-8c34-4075-8758-08d1af4862da");
        Application["Sso"] = HomoryContext.Value.Application.Single(o => o.Id == id).Home.Replace("Go/SignOn", "");
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
