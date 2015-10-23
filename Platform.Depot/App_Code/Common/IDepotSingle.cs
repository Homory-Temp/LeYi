using Models;

public interface IDepotSingle : IDepot
{
    Depot Depot { get; }

    string DepotRights { get; }
}
