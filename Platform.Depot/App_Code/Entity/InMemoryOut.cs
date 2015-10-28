using Models;
using System;
using System.Collections.Generic;

public class InMemoryOut
{
    public InMemoryOut()
    {
        Ordinals = new List<int>();
    }

    public Guid? CatalogId { get; set; }
    public Guid? ObjectId { get; set; }
    public decimal? Amount { get; set; }
    public List<int> Ordinals { get; set; }
    public string Reason { get; set; }
}
