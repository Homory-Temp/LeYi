<%@ Application Language="C#" %>
<%@ Import Namespace="Models" %>

<script RunAt="server">

    protected Lazy<DepotEntities> db = new Lazy<DepotEntities>(() => new DepotEntities());

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
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
