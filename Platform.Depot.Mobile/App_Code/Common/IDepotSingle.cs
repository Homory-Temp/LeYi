using Models;

public interface IDepotSingle : IDepot
{
    Depot Depot { get; }

    string DepotRights { get; }

    bool RightRoot { get; }

    bool RightAction { get; }

    bool RightQuery { get; }
}
