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
    }
}
