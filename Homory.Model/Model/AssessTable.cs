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
    public partial class AssessTable
    {
        public AssessTable()
        {
            this.ResourceAssess = new HashSet<ResourceAssess>();
        }
    
        public System.Guid Id { get; set; }
        public string Title { get; set; }
        public System.Guid GradeId { get; set; }
        public System.Guid CourseId { get; set; }
        public string Content { get; set; }
        public State State { get; set; }
        public int Ordinal { get; set; }
        public System.DateTime Time { get; set; }
    
        public virtual Catalog Course { get; set; }
        public virtual Catalog Grade { get; set; }
        public virtual ICollection<ResourceAssess> ResourceAssess { get; set; }
    }
}
