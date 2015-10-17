using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Control_ObjectInBody : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Note
    {
        get
        {
            return note.Text;
        }
        set
        {
            note.Text = value;
        }
    }
}