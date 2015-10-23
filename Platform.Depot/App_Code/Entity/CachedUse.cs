using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CachedUse
{
    public string UserTarget { get; set; }
    public Guid? CatalogId { get; set; }
    public Guid? ObjectId { get; set; }
    public decimal? Amount { get; set; }
    public List<int> SingleList { get; set; }
    public string Place { get; set; }
    public string Note { get; set; }
    public string Type { get; set; }
}
