﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

public partial class C6Entities : DbContext
{
    public C6Entities()
        : base("name=C6Entities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<C____OnlineCountStatistics> C____OnlineCountStatistics { get; set; }
    public virtual DbSet<Department> Department { get; set; }
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<RelationshipUsers> RelationshipUsers { get; set; }
}
