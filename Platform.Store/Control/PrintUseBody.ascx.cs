using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_PrintUseBody : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public int ItemIndex
    {
        get; set;
    }

    public Guid ObjectId
    {
        get; set;
    }

    protected Lazy<StoreEntity> db = new Lazy<StoreEntity>(() => new StoreEntity());

    private StoreObject obj;

    protected StoreObject Obj
    {
        get
        {
            if (obj == null)
                obj = db.Value.StoreObject.Single(o => o.Id == ObjectId);
            return obj;
        }
    }
}
