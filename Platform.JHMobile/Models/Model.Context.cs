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
    
    
        public virtual ObjectResult<f____Mobile_Count_Result> f____Mobile_Count(string loginCode)
        {
            var loginCodeParameter = loginCode != null ?
                new ObjectParameter("LoginCode", loginCode) :
                new ObjectParameter("LoginCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f____Mobile_Count_Result>("f____Mobile_Count", loginCodeParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> f____Mobile_Count_CallToRead(string loginCode)
        {
            var loginCodeParameter = loginCode != null ?
                new ObjectParameter("LoginCode", loginCode) :
                new ObjectParameter("LoginCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("f____Mobile_Count_CallToRead", loginCodeParameter);
        }
    
        public virtual ObjectResult<f____Mobile_List_CallToRead_Result> f____Mobile_List_CallToRead(string loginCode)
        {
            var loginCodeParameter = loginCode != null ?
                new ObjectParameter("LoginCode", loginCode) :
                new ObjectParameter("LoginCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f____Mobile_List_CallToRead_Result>("f____Mobile_List_CallToRead", loginCodeParameter);
        }
    
        public virtual ObjectResult<f____Mobile_List_CallAttachment_Result> f____Mobile_List_CallAttachment(string callID)
        {
            var callIDParameter = callID != null ?
                new ObjectParameter("CallID", callID) :
                new ObjectParameter("CallID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<f____Mobile_List_CallAttachment_Result>("f____Mobile_List_CallAttachment", callIDParameter);
        }
    
        public virtual int f____Mobile_Do_CallRead(Nullable<int> callNoSeeId)
        {
            var callNoSeeIdParameter = callNoSeeId.HasValue ?
                new ObjectParameter("CallNoSeeId", callNoSeeId) :
                new ObjectParameter("CallNoSeeId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("f____Mobile_Do_CallRead", callNoSeeIdParameter);
        }
    }
}
