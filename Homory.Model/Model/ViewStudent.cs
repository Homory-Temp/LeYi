//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Homory.Model
{
    using System;
    using System.Collections.Generic;
    
    [EntityFramework.Audit.Audit]
    public partial class ViewStudent
    {
        public System.Guid Id { get; set; }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string DisplayName { get; set; }
        public string IDCard { get; set; }
        public string UniqueId { get; set; }
        public Nullable<bool> Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Birthplace { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string Charger { get; set; }
        public string ChargerContact { get; set; }
        public int Ordinal { get; set; }
        public Nullable<System.Guid> DepartmentId { get; set; }
        public Nullable<System.Guid> TopDepartmentId { get; set; }
        public Nullable<State> State { get; set; }
        public string DepartmentName { get; set; }
        public string DeaprtmentDisplayName { get; set; }
        public Nullable<int> Level { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public string PinYin { get; set; }
    }
}
