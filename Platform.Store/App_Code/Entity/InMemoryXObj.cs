using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// InMemoryX 的摘要说明
/// </summary>
public class InMemoryXObj
{
    public Guid CatalogId { get; set; }
    public Guid? ParentId { get; set; }
    public string CatalogName { get; set; }
    public decimal I { get; set; }
    public decimal C { get; set; }
}
