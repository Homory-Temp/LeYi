//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    [EntityFramework.Audit.Audit]
    public partial class UserCatalog
    {
        public System.Guid UserId { get; set; }
        public System.Guid CatalogId { get; set; }
        public State State { get; set; }
    
        public virtual Catalog Catalog { get; set; }
        public virtual User User { get; set; }
    }
}
