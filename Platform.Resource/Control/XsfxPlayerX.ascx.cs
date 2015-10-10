using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HomoryPlayerX : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Video { get; set; }

    public decimal? StartSeconds { get; set; }

    public decimal? EndSeconds { get; set; }

    public string Comment { get; set; }

    public string PlayerStarts()
    {
        if (StartSeconds.HasValue && EndSeconds.HasValue)
        {
            return StartSeconds.Value.ToString();
        }
        else if (StartSeconds.HasValue)
        {
            return StartSeconds.Value.ToString();
        }
        else if (EndSeconds.HasValue)
        {
            return EndSeconds.Value.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    public string PlayerTime()
    {
        if (StartSeconds.HasValue && EndSeconds.HasValue)
        {
            return string.Format("{0}|{1}", StartSeconds.Value, EndSeconds.Value);
        }
        else if (StartSeconds.HasValue)
        {
            return StartSeconds.Value.ToString();
        }
        else if (EndSeconds.HasValue)
        {
            return EndSeconds.Value.ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    public string PlayerComment()
    {
        if (StartSeconds.HasValue && EndSeconds.HasValue)
        {
            return string.Format("{0}|{0}", Comment);
        }
        else if (StartSeconds.HasValue)
        {
            return Comment;
        }
        else if (EndSeconds.HasValue)
        {
            return Comment;
        }
        else
        {
            return string.Empty;
        }
    }
}
