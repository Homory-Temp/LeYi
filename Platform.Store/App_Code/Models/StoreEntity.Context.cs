﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StoreEntity : DbContext
    {
        public StoreEntity()
            : base("name=StoreEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<StoreCatalog> StoreCatalog { get; set; }
        public virtual DbSet<StoreConsume> StoreConsume { get; set; }
        public virtual DbSet<StoreConsumeSingle> StoreConsumeSingle { get; set; }
        public virtual DbSet<StoreDictionary> StoreDictionary { get; set; }
        public virtual DbSet<StoreFlow> StoreFlow { get; set; }
        public virtual DbSet<StoreFlowSingle> StoreFlowSingle { get; set; }
        public virtual DbSet<StoreIn> StoreIn { get; set; }
        public virtual DbSet<StoreInSingle> StoreInSingle { get; set; }
        public virtual DbSet<StoreLend> StoreLend { get; set; }
        public virtual DbSet<StoreLendSingle> StoreLendSingle { get; set; }
        public virtual DbSet<StoreObject> StoreObject { get; set; }
        public virtual DbSet<StoreOut> StoreOut { get; set; }
        public virtual DbSet<StoreOutSingle> StoreOutSingle { get; set; }
        public virtual DbSet<StoreReturn> StoreReturn { get; set; }
        public virtual DbSet<StoreReturnSingle> StoreReturnSingle { get; set; }
        public virtual DbSet<StoreRole> StoreRole { get; set; }
        public virtual DbSet<StoreTarget> StoreTarget { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<StoreStatistics> StoreStatistics { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<DepartmentUser> DepartmentUser { get; set; }
        public virtual DbSet<UserOnline> UserOnline { get; set; }
        public virtual DbSet<Store_Creator> Store_Creator { get; set; }
    }
}
