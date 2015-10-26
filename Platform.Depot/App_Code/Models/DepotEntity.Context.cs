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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DepotEntities : DbContext
    {
        public DepotEntities()
            : base("name=DepotEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Depot> Depot { get; set; }
        public virtual DbSet<DepotCatalog> DepotCatalog { get; set; }
        public virtual DbSet<DepotDictionary> DepotDictionary { get; set; }
        public virtual DbSet<DepotRole> DepotRole { get; set; }
        public virtual DbSet<DepotUserRole> DepotUserRole { get; set; }
        public virtual DbSet<DepotOrder> DepotOrder { get; set; }
        public virtual DbSet<DepotUser> DepotUser { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<UserOnline> UserOnline { get; set; }
        public virtual DbSet<DepotCreator> DepotCreator { get; set; }
        public virtual DbSet<DepotMember> DepotMember { get; set; }
        public virtual DbSet<DepotObjectCatalog> DepotObjectCatalog { get; set; }
        public virtual DbSet<DepotInRecord> DepotInRecord { get; set; }
        public virtual DbSet<DepotCatalogTree> DepotCatalogTree { get; set; }
        public virtual DbSet<DepotObject> DepotObject { get; set; }
        public virtual DbSet<DepotFlow> DepotFlow { get; set; }
        public virtual DbSet<DepotFlowX> DepotFlowX { get; set; }
        public virtual DbSet<DepotIn> DepotIn { get; set; }
        public virtual DbSet<DepotStatistics> DepotStatistics { get; set; }
        public virtual DbSet<DepotInX> DepotInX { get; set; }
        public virtual DbSet<DepotInXRecord> DepotInXRecord { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepotUse> DepotUse { get; set; }
        public virtual DbSet<DepotUseX> DepotUseX { get; set; }
    
        public virtual ObjectResult<string> ToPinYin(string name)
        {
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ToPinYin", nameParameter);
        }
    
        public virtual ObjectResult<string> ToCatalog(Nullable<System.Guid> catalogId, Nullable<int> level)
        {
            var catalogIdParameter = catalogId.HasValue ?
                new ObjectParameter("CatalogId", catalogId) :
                new ObjectParameter("CatalogId", typeof(System.Guid));
    
            var levelParameter = level.HasValue ?
                new ObjectParameter("Level", level) :
                new ObjectParameter("Level", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ToCatalog", catalogIdParameter, levelParameter);
        }
    }
}
