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
    public partial class SignLog
    {
        public System.Guid Id { get; set; }
        public System.DateTime Time { get; set; }
        public string TriedAccount { get; set; }
        public string Browser { get; set; }
        public string IP { get; set; }
        public bool Login { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid CampusId { get; set; }
    }
}
