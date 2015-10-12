using System.Web.UI.WebControls;

public partial class ObjectImageOne : System.Web.UI.UserControl
{
    private string imageJson;

    public string ImageJson
    {
        get { return imageJson; }
        set
        {
            imageJson = value;
            var images = ImageJson.FromJson<Models.Image>().Images;
            var controls = new Image[] { img };
            for (var i = 0; i < images.Count;)
            {
                controls[i].Visible = true;
                controls[i].ImageUrl = images[i];
                break;
            }
        }
    }

    public Unit ImageWidth
    {
        get
        {
            return img.Width;
        }
        set
        {
            img.Width = value;
        }
    }

    public Unit ImageHeight
    {
        get
        {
            return img.Height;
        }
        set
        {
            img.Height = value;
        }
    }
}
