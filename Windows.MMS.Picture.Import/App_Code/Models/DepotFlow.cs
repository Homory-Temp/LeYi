//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Windows.MMS.Picture.Import.App_Code.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotFlow
    {
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid UserId { get; set; }
        public FlowType Type { get; set; }
        public string TypeName { get; set; }
        public System.DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
        public string Note { get; set; }
    
        public virtual DepotObject DepotObject { get; set; }
    }
}
