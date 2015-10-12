using System.Web.UI.WebControls;

public partial class StorageObject_ObjectImage : System.Web.UI.UserControl
{
    private string imageJson;

    public string ImageJson
    {
        get { return imageJson; }
        set
        {
            imageJson = value;
            var images = ImageJson.FromJson<Models.Image>().Images;
            var controls = new Image[] { img1, img2, img3, img4 };
            for (var i = 0; i < images.Count; i++)
            {
                controls[i].Visible = true;
                controls[i].ImageUrl = images[i];
            }
        }
    }
}
