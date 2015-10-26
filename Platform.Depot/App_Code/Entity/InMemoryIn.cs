using System;
using System.Collections.Generic;

public class InMemoryIn
{
    public Guid OrderId { get; set; }
    public DateTime Time { get; set; }
    public Guid? CatalogId { get; set; }
    public Guid? ObjectId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? PriceSet { get; set; }
    public decimal? Money { get; set; }
    public string Age { get; set; }
    public string Place { get; set; }
    public string Note { get; set; }
}
