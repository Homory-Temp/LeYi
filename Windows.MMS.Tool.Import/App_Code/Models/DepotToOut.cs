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
    
    public partial class DepotToOut
    {
        public int Id { get; set; }
        public System.Guid DepotId { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid UserId { get; set; }
        public string Code { get; set; }
        public string Reason { get; set; }
        public decimal ToAmount { get; set; }
        public decimal Amount { get; set; }
        public string PreservedA { get; set; }
        public string PreservedB { get; set; }
        public string PreservedC { get; set; }
        public string PreservedD { get; set; }
        public System.DateTime Time { get; set; }
        public int State { get; set; }
    }
}
