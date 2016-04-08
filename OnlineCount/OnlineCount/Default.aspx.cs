using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            years.DataSource = new[] { DateTime.Today.Year, DateTime.Today.Year - 1, DateTime.Today.Year - 2 };
            years.DataBind();
            years.SelectedIndex = 0;
            months.DataSource = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            months.DataBind();
            months.SelectedIndex = DateTime.Today.Month - 1;
        }
    }
}
