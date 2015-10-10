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
    public partial class Group
    {
        public Group()
        {
            this.GroupUser = new HashSet<GroupUser>();
            this.GroupBoard = new HashSet<GroupBoard>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public GroupType Type { get; set; }
        public string Icon { get; set; }
        public OpenType OpenType { get; set; }
        public State State { get; set; }
        public int Ordinal { get; set; }
        public string Introduction { get; set; }
        public Nullable<System.Guid> GradeId { get; set; }
        public Nullable<System.Guid> CourseId { get; set; }
    
        public virtual Catalog Catalog { get; set; }
        public virtual Catalog Catalog1 { get; set; }
        public virtual ICollection<GroupUser> GroupUser { get; set; }
        public virtual ICollection<GroupBoard> GroupBoard { get; set; }
    }
}
