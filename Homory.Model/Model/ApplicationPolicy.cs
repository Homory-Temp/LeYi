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
    public partial class ApplicationPolicy
    {
        public System.Guid Id { get; set; }
        public System.Guid ApplicationId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
    
        public virtual Application Application { get; set; }
    }
}
