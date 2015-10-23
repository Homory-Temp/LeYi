using Models;
using Telerik.Web.UI;

public interface IDepot
{
    DepotEntities DataContext { get; }

    DepotUser DepotUser { get; }

    bool DepotOnline { get; }

    bool RightCreate { get; }

    void NotifyOK(RadAjaxPanel panel, string message);

    void NotifyError(RadAjaxPanel panel, string message);
}
