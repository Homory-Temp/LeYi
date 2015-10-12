using Models;
using System;
using System.Xml.Linq;
using System.Linq;
using Homory.Model;

public partial class Login : System.Web.UI.Page
{
    protected Lazy<StorageEntity> db = new Lazy<StorageEntity>(() => new StorageEntity());

    protected bool IsOnline
    {
        get { return Session["Storage__UserId"] != null; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected bool UserSignOn(string account, string password)
    {
        try
        {
            var user = db.Value.User.SingleOrDefault(o => o.Account == account && o.State < State.删除);
            if (user == null || !password.Equals(HomoryCryptor.Decrypt(user.Password, user.CryptoKey, user.CryptoSalt),
                    StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (user.State != State.内置 && user.State != State.启用)
            {
                return false;
            }
            Session["Storage__UserId"] = user.Id;
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void go_ServerClick(object sender, EventArgs e)
    {
        if (UserSignOn(name.Value, password.Value))
        {
            Response.Redirect("~/Storage/StorageMobile");
        }
    }
}
