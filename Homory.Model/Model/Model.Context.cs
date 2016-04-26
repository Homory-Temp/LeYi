﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homory.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationPolicy> ApplicationPolicy { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserOnline> UserOnline { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Right> Right { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleRight> RoleRight { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<DepartmentUser> DepartmentUser { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<ViewStudent> ViewStudent { get; set; }
        public virtual DbSet<ViewRole> ViewRole { get; set; }
        public virtual DbSet<Learned> Learned { get; set; }
        public virtual DbSet<ViewLearned> ViewLearned { get; set; }
        public virtual DbSet<Taught> Taught { get; set; }
        public virtual DbSet<ViewTaught> ViewTaught { get; set; }
        public virtual DbSet<ViewTeacher> ViewTeacher { get; set; }
        public virtual DbSet<ViewQueryStudent> ViewQueryStudent { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<ResourceCatalog> ResourceCatalog { get; set; }
        public virtual DbSet<ResourceStatisticsMonthly> ResourceStatisticsMonthly { get; set; }
        public virtual DbSet<ResourceAttachment> ResourceAttachment { get; set; }
        public virtual DbSet<UserCatalog> UserCatalog { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<PrizeCredit> PrizeCredit { get; set; }
        public virtual DbSet<GroupUser> GroupUser { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<AssessTable> AssessTable { get; set; }
        public virtual DbSet<ResourceTag> ResourceTag { get; set; }
        public virtual DbSet<UserFavourite> UserFavourite { get; set; }
        public virtual DbSet<Dictionary> Dictionary { get; set; }
        public virtual DbSet<ResourceComment> ResourceComment { get; set; }
        public virtual DbSet<Action> Action { get; set; }
        public virtual DbSet<ViewResource> ViewResource { get; set; }
        public virtual DbSet<MediaNote> MediaNote { get; set; }
        public virtual DbSet<ResourceAssess> ResourceAssess { get; set; }
        public virtual DbSet<ResourceRoom> ResourceRoom { get; set; }
        public virtual DbSet<ViewResourceX> ViewResourceX { get; set; }
        public virtual DbSet<ResourceCommentTemp> ResourceCommentTemp { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRole { get; set; }
        public virtual DbSet<Api> Api { get; set; }
        public virtual DbSet<ApiLog> ApiLog { get; set; }
        public virtual DbSet<ViewQueryTeacher> ViewQueryTeacher { get; set; }
        public virtual DbSet<ViewQueryTaught> ViewQueryTaught { get; set; }
        public virtual DbSet<ViewQuerySign> ViewQuerySign { get; set; }
        public virtual DbSet<SignLog> SignLog { get; set; }
        public virtual DbSet<OperationLog> OperationLog { get; set; }
        public virtual DbSet<ViewQueryOperation> ViewQueryOperation { get; set; }
        public virtual DbSet<ResourceLog> ResourceLog { get; set; }
        public virtual DbSet<ViewQueryResource> ViewQueryResource { get; set; }
        public virtual DbSet<GroupBoard> GroupBoard { get; set; }
        public virtual DbSet<ViewStatistics> ViewStatistics { get; set; }
        public virtual DbSet<ViewTS> ViewTS { get; set; }
        public virtual DbSet<CardNo> CardNo { get; set; }
        public virtual DbSet<ViewTeacherExport> ViewTeacherExport { get; set; }
        public virtual DbSet<View_ResourceAssess> View_ResourceAssess { get; set; }
        public virtual DbSet<ResourceMap> ResourceMap { get; set; }
        public virtual DbSet<ResourceAudit> ResourceAudit { get; set; }
        public virtual DbSet<ViewDingDing> ViewDingDing { get; set; }
        public virtual DbSet<Contact_Users> Contact_Users { get; set; }
        public virtual DbSet<C__机构> C__机构 { get; set; }
        public virtual DbSet<C__用户> C__用户 { get; set; }
    
        public virtual int ResetPolicyCommon()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ResetPolicyCommon");
        }
    
        public virtual int SsoInitialize(Nullable<System.Guid> id, string phone, Nullable<bool> reset)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(System.Guid));
    
            var phoneParameter = phone != null ?
                new ObjectParameter("Phone", phone) :
                new ObjectParameter("Phone", typeof(string));
    
            var resetParameter = reset.HasValue ?
                new ObjectParameter("Reset", reset) :
                new ObjectParameter("Reset", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SsoInitialize", idParameter, phoneParameter, resetParameter);
        }
    
        public virtual ObjectResult<Department> Contact_GetDepartments(Nullable<System.Guid> departmentId)
        {
            var departmentIdParameter = departmentId.HasValue ?
                new ObjectParameter("DepartmentId", departmentId) :
                new ObjectParameter("DepartmentId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Department>("Contact_GetDepartments", departmentIdParameter);
        }
    
        public virtual ObjectResult<Department> Contact_GetDepartments(Nullable<System.Guid> departmentId, MergeOption mergeOption)
        {
            var departmentIdParameter = departmentId.HasValue ?
                new ObjectParameter("DepartmentId", departmentId) :
                new ObjectParameter("DepartmentId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Department>("Contact_GetDepartments", mergeOption, departmentIdParameter);
        }
    
        public virtual ObjectResult<Contact_Users> Contact_GetDepartmentUsers(Nullable<System.Guid> departmentId)
        {
            var departmentIdParameter = departmentId.HasValue ?
                new ObjectParameter("DepartmentId", departmentId) :
                new ObjectParameter("DepartmentId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact_Users>("Contact_GetDepartmentUsers", departmentIdParameter);
        }
    
        public virtual ObjectResult<Contact_Users> Contact_GetDepartmentUsers(Nullable<System.Guid> departmentId, MergeOption mergeOption)
        {
            var departmentIdParameter = departmentId.HasValue ?
                new ObjectParameter("DepartmentId", departmentId) :
                new ObjectParameter("DepartmentId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact_Users>("Contact_GetDepartmentUsers", mergeOption, departmentIdParameter);
        }
    
        public virtual ObjectResult<Contact_Users> Contact_GetDepartmentVIPUsers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact_Users>("Contact_GetDepartmentVIPUsers");
        }
    
        public virtual ObjectResult<Contact_Users> Contact_GetDepartmentVIPUsers(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact_Users>("Contact_GetDepartmentVIPUsers", mergeOption);
        }
    
        public virtual ObjectResult<Contact_Users> Contact_GetUsers(string query)
        {
            var queryParameter = query != null ?
                new ObjectParameter("query", query) :
                new ObjectParameter("query", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact_Users>("Contact_GetUsers", queryParameter);
        }
    
        public virtual ObjectResult<Contact_Users> Contact_GetUsers(string query, MergeOption mergeOption)
        {
            var queryParameter = query != null ?
                new ObjectParameter("query", query) :
                new ObjectParameter("query", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Contact_Users>("Contact_GetUsers", mergeOption, queryParameter);
        }
    
        public virtual ObjectResult<C__用户> 机构用户(Nullable<System.Guid> orgId)
        {
            var orgIdParameter = orgId.HasValue ?
                new ObjectParameter("OrgId", orgId) :
                new ObjectParameter("OrgId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<C__用户>("机构用户", orgIdParameter);
        }
    
        public virtual ObjectResult<C__用户> 机构用户(Nullable<System.Guid> orgId, MergeOption mergeOption)
        {
            var orgIdParameter = orgId.HasValue ?
                new ObjectParameter("OrgId", orgId) :
                new ObjectParameter("OrgId", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<C__用户>("机构用户", mergeOption, orgIdParameter);
        }
    }
}
