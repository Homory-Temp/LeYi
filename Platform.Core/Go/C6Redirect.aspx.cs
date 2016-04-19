using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Go_C6Redirect : Homory.Model.HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var id = Guid.Parse(Request.QueryString[0]);
        var user = HomoryContext.Value.User.Single(o => o.Id == id);
        var ssoUrl = string.Format("{0}Go/SignOn?Name={1}&Password={2}", Application["Sso"], user.Account, Homory.Model.HomoryCryptor.Decrypt(user.Password, user.CryptoKey, user.CryptoSalt));
        Response.Write(ssoUrl);
    }
}
