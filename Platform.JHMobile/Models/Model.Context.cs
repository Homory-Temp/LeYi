﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Platform.JHMobile.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
    
        public virtual DbSet<信息门户对象简略> 信息门户对象简略 { get; set; }
    
        public virtual ObjectResult<Nullable<int>> f______计数寻呼历史数(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数寻呼历史数", userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______计数寻呼未阅数(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数寻呼未阅数", userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______计数寻呼已阅数(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数寻呼已阅数", userIDParameter);
        }
    
        public virtual ObjectResult<f______列表寻呼未阅表_Result> f______列表寻呼未阅表(string userId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表寻呼未阅表_Result>("f______列表寻呼未阅表", userIdParameter);
        }
    
        public virtual ObjectResult<string> f______微信ID获取用户ID(string openId)
        {
            var openIdParameter = openId != null ?
                new ObjectParameter("OpenId", openId) :
                new ObjectParameter("OpenId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("f______微信ID获取用户ID", openIdParameter);
        }
    
        public virtual ObjectResult<f______列表寻呼附件表_Result> f______列表寻呼附件表(string callID)
        {
            var callIDParameter = callID != null ?
                new ObjectParameter("CallID", callID) :
                new ObjectParameter("CallID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表寻呼附件表_Result>("f______列表寻呼附件表", callIDParameter);
        }
    
        public virtual int f______列表寻呼转已阅(Nullable<int> callNoSeeId)
        {
            var callNoSeeIdParameter = callNoSeeId.HasValue ?
                new ObjectParameter("CallNoSeeId", callNoSeeId) :
                new ObjectParameter("CallNoSeeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f______列表寻呼转已阅", callNoSeeIdParameter);
        }
    
        public virtual ObjectResult<f______列表寻呼历史表_Result> f______列表寻呼历史表(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表寻呼历史表_Result>("f______列表寻呼历史表", userIDParameter);
        }
    
        public virtual ObjectResult<f______列表寻呼已阅表_Result> f______列表寻呼已阅表(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表寻呼已阅表_Result>("f______列表寻呼已阅表", userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______计数信息门户数(string userID, string moduleType)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var moduleTypeParameter = moduleType != null ?
                new ObjectParameter("ModuleType", moduleType) :
                new ObjectParameter("ModuleType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数信息门户数", userIDParameter, moduleTypeParameter);
        }
    
        public virtual ObjectResult<f______信息门户模块表_Result> f______信息门户模块表()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______信息门户模块表_Result>("f______信息门户模块表");
        }
    
        public virtual ObjectResult<信息门户对象简略> f______列表信息门户表(string userID, string moduleType)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var moduleTypeParameter = moduleType != null ?
                new ObjectParameter("ModuleType", moduleType) :
                new ObjectParameter("ModuleType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<信息门户对象简略>("f______列表信息门户表", userIDParameter, moduleTypeParameter);
        }
    
        public virtual ObjectResult<信息门户对象简略> f______列表信息门户表(string userID, string moduleType, MergeOption mergeOption)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var moduleTypeParameter = moduleType != null ?
                new ObjectParameter("ModuleType", moduleType) :
                new ObjectParameter("ModuleType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<信息门户对象简略>("f______列表信息门户表", mergeOption, userIDParameter, moduleTypeParameter);
        }
    
        public virtual ObjectResult<f______信息门户附件表_Result> f______信息门户附件表(string messageID)
        {
            var messageIDParameter = messageID != null ?
                new ObjectParameter("MessageID", messageID) :
                new ObjectParameter("MessageID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______信息门户附件表_Result>("f______信息门户附件表", messageIDParameter);
        }
    
        public virtual ObjectResult<f______信息门户内容表_Result> f______信息门户内容表(Nullable<int> messageId)
        {
            var messageIdParameter = messageId.HasValue ?
                new ObjectParameter("MessageId", messageId) :
                new ObjectParameter("MessageId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______信息门户内容表_Result>("f______信息门户内容表", messageIdParameter);
        }
    
        public virtual ObjectResult<string> f______信息门户已阅表(Nullable<int> messageId, string moduleType)
        {
            var messageIdParameter = messageId.HasValue ?
                new ObjectParameter("MessageId", messageId) :
                new ObjectParameter("MessageId", typeof(int));
    
            var moduleTypeParameter = moduleType != null ?
                new ObjectParameter("ModuleType", moduleType) :
                new ObjectParameter("ModuleType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("f______信息门户已阅表", messageIdParameter, moduleTypeParameter);
        }
    
        public virtual int f______信息门户转已阅(Nullable<int> messageId, string moduleType, string userID)
        {
            var messageIdParameter = messageId.HasValue ?
                new ObjectParameter("MessageId", messageId) :
                new ObjectParameter("MessageId", typeof(int));
    
            var moduleTypeParameter = moduleType != null ?
                new ObjectParameter("ModuleType", moduleType) :
                new ObjectParameter("ModuleType", typeof(string));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f______信息门户转已阅", messageIdParameter, moduleTypeParameter, userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______计数待阅信息数(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数待阅信息数", userIDParameter);
        }
    
        public virtual ObjectResult<f______待阅信息发文表_Result> f______待阅信息发文表(Nullable<int> appG_ID)
        {
            var appG_IDParameter = appG_ID.HasValue ?
                new ObjectParameter("AppG_ID", appG_ID) :
                new ObjectParameter("AppG_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______待阅信息发文表_Result>("f______待阅信息发文表", appG_IDParameter);
        }
    
        public virtual ObjectResult<f______待阅信息类型表_Result> f______待阅信息类型表(Nullable<int> appG_ID)
        {
            var appG_IDParameter = appG_ID.HasValue ?
                new ObjectParameter("AppG_ID", appG_ID) :
                new ObjectParameter("AppG_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______待阅信息类型表_Result>("f______待阅信息类型表", appG_IDParameter);
        }
    
        public virtual ObjectResult<f______待阅信息收文表_Result> f______待阅信息收文表(Nullable<int> appG_ID)
        {
            var appG_IDParameter = appG_ID.HasValue ?
                new ObjectParameter("AppG_ID", appG_ID) :
                new ObjectParameter("AppG_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______待阅信息收文表_Result>("f______待阅信息收文表", appG_IDParameter);
        }
    
        public virtual int f______待阅信息转已阅(Nullable<int> giveOutId, string userID)
        {
            var giveOutIdParameter = giveOutId.HasValue ?
                new ObjectParameter("GiveOutId", giveOutId) :
                new ObjectParameter("GiveOutId", typeof(int));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f______待阅信息转已阅", giveOutIdParameter, userIDParameter);
        }
    
        public virtual ObjectResult<f______列表待阅信息表_Result> f______列表待阅信息表(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表待阅信息表_Result>("f______列表待阅信息表", userIDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______计数待办工作数(string userId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数待办工作数", userIdParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______计数流程查询数(string userId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数流程查询数", userIdParameter);
        }
    
        public virtual ObjectResult<f______列表流程查询表_Result> f______列表流程查询表(string userId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表流程查询表_Result>("f______列表流程查询表", userIdParameter);
        }
    
        public virtual ObjectResult<f______列表流程单条表_Result> f______列表流程单条表(Nullable<int> app_ID)
        {
            var app_IDParameter = app_ID.HasValue ?
                new ObjectParameter("App_ID", app_ID) :
                new ObjectParameter("App_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表流程单条表_Result>("f______列表流程单条表", app_IDParameter);
        }
    
        public virtual ObjectResult<f______列表流程流程表_Result> f______列表流程流程表(Nullable<int> app_ID)
        {
            var app_IDParameter = app_ID.HasValue ?
                new ObjectParameter("App_ID", app_ID) :
                new ObjectParameter("App_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表流程流程表_Result>("f______列表流程流程表", app_IDParameter);
        }
    
        public virtual ObjectResult<string> f______列表流程文件表(string appT_ID, Nullable<int> appO_ID)
        {
            var appT_IDParameter = appT_ID != null ?
                new ObjectParameter("AppT_ID", appT_ID) :
                new ObjectParameter("AppT_ID", typeof(string));
    
            var appO_IDParameter = appO_ID.HasValue ?
                new ObjectParameter("AppO_ID", appO_ID) :
                new ObjectParameter("AppO_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("f______列表流程文件表", appT_IDParameter, appO_IDParameter);
        }
    
        public virtual int f______待办工作转办结(Nullable<int> app_ID, string userID, string idea)
        {
            var app_IDParameter = app_ID.HasValue ?
                new ObjectParameter("App_ID", app_ID) :
                new ObjectParameter("App_ID", typeof(int));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var ideaParameter = idea != null ?
                new ObjectParameter("Idea", idea) :
                new ObjectParameter("Idea", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f______待办工作转办结", app_IDParameter, userIDParameter, ideaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______待办工作转返回(Nullable<int> app_ID, string app_InnerIdea, string userID, string idea)
        {
            var app_IDParameter = app_ID.HasValue ?
                new ObjectParameter("App_ID", app_ID) :
                new ObjectParameter("App_ID", typeof(int));
    
            var app_InnerIdeaParameter = app_InnerIdea != null ?
                new ObjectParameter("App_InnerIdea", app_InnerIdea) :
                new ObjectParameter("App_InnerIdea", typeof(string));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var ideaParameter = idea != null ?
                new ObjectParameter("Idea", idea) :
                new ObjectParameter("Idea", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______待办工作转返回", app_IDParameter, app_InnerIdeaParameter, userIDParameter, ideaParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______待办工作转下步(Nullable<int> app_ID, string next_User_ID, string app_InnerIdea, string userID, string idea)
        {
            var app_IDParameter = app_ID.HasValue ?
                new ObjectParameter("App_ID", app_ID) :
                new ObjectParameter("App_ID", typeof(int));
    
            var next_User_IDParameter = next_User_ID != null ?
                new ObjectParameter("Next_User_ID", next_User_ID) :
                new ObjectParameter("Next_User_ID", typeof(string));
    
            var app_InnerIdeaParameter = app_InnerIdea != null ?
                new ObjectParameter("App_InnerIdea", app_InnerIdea) :
                new ObjectParameter("App_InnerIdea", typeof(string));
    
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            var ideaParameter = idea != null ?
                new ObjectParameter("Idea", idea) :
                new ObjectParameter("Idea", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______待办工作转下步", app_IDParameter, next_User_IDParameter, app_InnerIdeaParameter, userIDParameter, ideaParameter);
        }
    
        public virtual ObjectResult<f______列表待办按钮表_Result> f______列表待办按钮表(Nullable<int> appD_ID, string version)
        {
            var appD_IDParameter = appD_ID.HasValue ?
                new ObjectParameter("AppD_ID", appD_ID) :
                new ObjectParameter("AppD_ID", typeof(int));
    
            var versionParameter = version != null ?
                new ObjectParameter("Version", version) :
                new ObjectParameter("Version", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表待办按钮表_Result>("f______列表待办按钮表", appD_IDParameter, versionParameter);
        }
    
        public virtual ObjectResult<f______列表待办单条表_Result> f______列表待办单条表(string userId, Nullable<int> approveId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            var approveIdParameter = approveId.HasValue ?
                new ObjectParameter("ApproveId", approveId) :
                new ObjectParameter("ApproveId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表待办单条表_Result>("f______列表待办单条表", userIdParameter, approveIdParameter);
        }
    
        public virtual ObjectResult<f______列表待办工作表_Result> f______列表待办工作表(string userId)
        {
            var userIdParameter = userId != null ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表待办工作表_Result>("f______列表待办工作表", userIdParameter);
        }
    
        public virtual ObjectResult<f______列表待办流程表_Result> f______列表待办流程表(Nullable<int> app_ID)
        {
            var app_IDParameter = app_ID.HasValue ?
                new ObjectParameter("App_ID", app_ID) :
                new ObjectParameter("App_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表待办流程表_Result>("f______列表待办流程表", app_IDParameter);
        }
    
        public virtual ObjectResult<f______列表待办下步表_Result> f______列表待办下步表(Nullable<int> appD_ID, string version, Nullable<int> appDA_Type)
        {
            var appD_IDParameter = appD_ID.HasValue ?
                new ObjectParameter("AppD_ID", appD_ID) :
                new ObjectParameter("AppD_ID", typeof(int));
    
            var versionParameter = version != null ?
                new ObjectParameter("Version", version) :
                new ObjectParameter("Version", typeof(string));
    
            var appDA_TypeParameter = appDA_Type.HasValue ?
                new ObjectParameter("AppDA_Type", appDA_Type) :
                new ObjectParameter("AppDA_Type", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表待办下步表_Result>("f______列表待办下步表", appD_IDParameter, versionParameter, appDA_TypeParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f______计数已阅信息数(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f______计数已阅信息数", userIDParameter);
        }
    
        public virtual ObjectResult<f______列表已阅信息表_Result> f______列表已阅信息表(string userID)
        {
            var userIDParameter = userID != null ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______列表已阅信息表_Result>("f______列表已阅信息表", userIDParameter);
        }
    
        public virtual ObjectResult<f______已阅信息发文表_Result> f______已阅信息发文表(Nullable<int> appG_ID)
        {
            var appG_IDParameter = appG_ID.HasValue ?
                new ObjectParameter("AppG_ID", appG_ID) :
                new ObjectParameter("AppG_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______已阅信息发文表_Result>("f______已阅信息发文表", appG_IDParameter);
        }
    
        public virtual ObjectResult<f______已阅信息类型表_Result> f______已阅信息类型表(Nullable<int> appG_ID)
        {
            var appG_IDParameter = appG_ID.HasValue ?
                new ObjectParameter("AppG_ID", appG_ID) :
                new ObjectParameter("AppG_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______已阅信息类型表_Result>("f______已阅信息类型表", appG_IDParameter);
        }
    
        public virtual ObjectResult<f______已阅信息收文表_Result> f______已阅信息收文表(Nullable<int> appG_ID)
        {
            var appG_IDParameter = appG_ID.HasValue ?
                new ObjectParameter("AppG_ID", appG_ID) :
                new ObjectParameter("AppG_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f______已阅信息收文表_Result>("f______已阅信息收文表", appG_IDParameter);
        }
    }
}
