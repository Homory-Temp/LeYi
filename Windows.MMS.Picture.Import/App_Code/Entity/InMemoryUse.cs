using System;
using System.Collections.Generic;
using Windows.MMS.Picture.Import.App_Code.Models;

public class InMemoryUse
{
    public Guid? CatalogId { get; set; }
    public Guid? ObjectId { get; set; }
    public decimal? Amount { get; set; }
    public List<int> Ordinals { get; set; }
    public string Age { get; set; }
    public string Place { get; set; }
    public string Note { get; set; }
    public UseType? Type { get; set; }
}
