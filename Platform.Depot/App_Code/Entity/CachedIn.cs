using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CachedIn
{
    public Guid TargetId { get; set; }
    public Guid? CatalogId { get; set; }
    public Guid? ObjectId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? SourcePerPrice { get; set; }
    public decimal? Fee { get; set; }
    public decimal? Money { get; set; }
    public string Age { get; set; }
    public string Place { get; set; }
    public string Note { get; set; }
    public int? TimeNode { get; set; }
}
