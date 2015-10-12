using Telerik.Web.UI;

namespace Homory.Model
{
    public abstract class HomoryCorePageWithNotify : HomoryCorePage
    {

        protected void Notify(RadAjaxPanel panel, string message, string type)
        {
            panel.ResponseScripts.Add(string.Format("notify(null, '{0}', '{1}');", message, type));
        }
    }
}
