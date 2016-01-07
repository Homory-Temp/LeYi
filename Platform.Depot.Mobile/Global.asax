<%@ Application Language="C#" %>

<script RunAt="server">

    void Application_PostAuthorizeRequest()
    {
        HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
    }

</script>
