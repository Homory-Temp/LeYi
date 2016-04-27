using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// InMemoryCatalog 的摘要说明
/// </summary>
public class InMemoryCatalog
{
    public Guid Id { get; set; }
    public Guid? Parent { get; set; }
    public string Name { get; set; }
}