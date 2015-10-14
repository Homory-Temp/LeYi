using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Peek : Homory.Model.HomoryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var id = Guid.Parse(Request.QueryString[0]);
        var user = HomoryContext.Value.User.Single(o => o.Id == id);
        Response.Write(Homory.Model.HomoryCryptor.Decrypt(user.Password, user.CryptoKey, user.CryptoSalt));
    }
}