//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Windows.MMS.Tool.Import.App_Code.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotRedo
    {
        public System.Guid Id { get; set; }
        public System.Guid DepotId { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid InId { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
        public System.DateTime Time { get; set; }
        public string Note { get; set; }
    
        public virtual DepotIn DepotIn { get; set; }
        public virtual DepotObject DepotObject { get; set; }
    }
}
