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
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;

    public partial class StorageEntity : DbContext
    {
        public StorageEntity()
            : base("name=StorageEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentUser> DepartmentUser { get; set; }
        public virtual DbSet<Storage> Storage { get; set; }
        public virtual DbSet<StorageCatalog> StorageCatalog { get; set; }
        public virtual DbSet<StorageConsume> StorageConsume { get; set; }
        public virtual DbSet<StorageConsumeSingle> StorageConsumeSingle { get; set; }
        public virtual DbSet<StorageFlow> StorageFlow { get; set; }
        public virtual DbSet<StorageIn> StorageIn { get; set; }
        public virtual DbSet<StorageInSingle> StorageInSingle { get; set; }
        public virtual DbSet<StorageLend> StorageLend { get; set; }
        public virtual DbSet<StorageLendSingle> StorageLendSingle { get; set; }
        public virtual DbSet<StorageOut> StorageOut { get; set; }
        public virtual DbSet<StoragePlace> StoragePlace { get; set; }
        public virtual DbSet<StorageOutSingle> StorageOutSingle { get; set; }
        public virtual DbSet<StorageReturn> StorageReturn { get; set; }
        public virtual DbSet<StorageReturnSingle> StorageReturnSingle { get; set; }
        public virtual DbSet<StorageTarget> StorageTarget { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserOnline> UserOnline { get; set; }
        public virtual DbSet<ViewTeacher> ViewTeacher { get; set; }
        public virtual DbSet<StorageQueryObject> StorageQueryObject { get; set; }
        public virtual DbSet<StorageDictionary> StorageDictionary { get; set; }
        public virtual DbSet<StorageObject> StorageObject { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<查询_购置单> 查询_购置单 { get; set; }
        public virtual DbSet<查询_入库单> 查询_入库单 { get; set; }
        public virtual DbSet<查询_报废单> 查询_报废单 { get; set; }
        public virtual DbSet<查询_归还单> 查询_归还单 { get; set; }
        public virtual DbSet<查询_借用单> 查询_借用单 { get; set; }
        public virtual DbSet<查询_领用单> 查询_领用单 { get; set; }
        public virtual DbSet<查询_数据流> 查询_数据流 { get; set; }
        public virtual DbSet<查询_借还流> 查询_借还流 { get; set; }
        public virtual DbSet<StorageRole> StorageRole { get; set; }
        public virtual DbSet<StorageRoleRight> StorageRoleRight { get; set; }
        public virtual DbSet<查询_盘库流> 查询_盘库流 { get; set; }
    
        public virtual ObjectResult<StorageObject> QueryStorageObject(string content, Nullable<System.Guid> storageId)
        {
            var contentParameter = content != null ?
                new ObjectParameter("Content", content) :
                new ObjectParameter("Content", typeof(string));
    
            var storageIdParameter = storageId.HasValue ?
                new ObjectParameter("storageId", storageId) :
                new ObjectParameter("storageId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StorageObject>("QueryStorageObject", contentParameter, storageIdParameter);
        }
    
        public virtual ObjectResult<StorageObject> QueryStorageObject(string content, Nullable<System.Guid> storageId, MergeOption mergeOption)
        {
            var contentParameter = content != null ?
                new ObjectParameter("Content", content) :
                new ObjectParameter("Content", typeof(string));
    
            var storageIdParameter = storageId.HasValue ?
                new ObjectParameter("storageId", storageId) :
                new ObjectParameter("storageId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StorageObject>("QueryStorageObject", mergeOption, contentParameter, storageIdParameter);
        }
    
        public virtual ObjectResult<StorageTarget> QueryStorageTarget(string content, Nullable<System.Guid> storageId)
        {
            var contentParameter = content != null ?
                new ObjectParameter("Content", content) :
                new ObjectParameter("Content", typeof(string));
    
            var storageIdParameter = storageId.HasValue ?
                new ObjectParameter("storageId", storageId) :
                new ObjectParameter("storageId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StorageTarget>("QueryStorageTarget", contentParameter, storageIdParameter);
        }
    
        public virtual ObjectResult<StorageTarget> QueryStorageTarget(string content, Nullable<System.Guid> storageId, MergeOption mergeOption)
        {
            var contentParameter = content != null ?
                new ObjectParameter("Content", content) :
                new ObjectParameter("Content", typeof(string));
    
            var storageIdParameter = storageId.HasValue ?
                new ObjectParameter("storageId", storageId) :
                new ObjectParameter("storageId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<StorageTarget>("QueryStorageTarget", mergeOption, contentParameter, storageIdParameter);
        }
    
        public virtual ObjectResult<User> QueryStorageBorrow(string content, Nullable<System.Guid> storageId)
        {
            var contentParameter = content != null ?
                new ObjectParameter("Content", content) :
                new ObjectParameter("Content", typeof(string));
    
            var storageIdParameter = storageId.HasValue ?
                new ObjectParameter("StorageId", storageId) :
                new ObjectParameter("StorageId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<User>("QueryStorageBorrow", contentParameter, storageIdParameter);
        }
    
        public virtual ObjectResult<User> QueryStorageBorrow(string content, Nullable<System.Guid> storageId, MergeOption mergeOption)
        {
            var contentParameter = content != null ?
                new ObjectParameter("Content", content) :
                new ObjectParameter("Content", typeof(string));
    
            var storageIdParameter = storageId.HasValue ?
                new ObjectParameter("StorageId", storageId) :
                new ObjectParameter("StorageId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<User>("QueryStorageBorrow", mergeOption, contentParameter, storageIdParameter);
        }
    }
}
