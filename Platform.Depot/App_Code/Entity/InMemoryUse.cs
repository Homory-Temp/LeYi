﻿using Models;
using System;
using System.Collections.Generic;

public class InMemoryUse
{
    public InMemoryUse()
    {
        Ordinals = new List<int>();
    }

    public Guid? CatalogId { get; set; }
    public Guid? ObjectId { get; set; }
    public decimal? Amount { get; set; }
    public List<int> Ordinals { get; set; }
    public string Age { get; set; }
    public string Place { get; set; }
    public string Note { get; set; }
    public UseType? Type { get; set; }
}
