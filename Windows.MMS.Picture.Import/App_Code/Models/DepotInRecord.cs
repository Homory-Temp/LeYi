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
    
    public partial class DepotInRecord
    {
        public System.Guid Id { get; set; }
        public System.Guid DepotId { get; set; }
        public string 购置单号 { get; set; }
        public string 发票编号 { get; set; }
        public string 购置来源 { get; set; }
        public string 使用对象 { get; set; }
        public string 清单简述 { get; set; }
        public decimal 应付金额 { get; set; }
        public decimal 实付金额 { get; set; }
        public Nullable<System.Guid> BrokerageId { get; set; }
        public string 经手人 { get; set; }
        public Nullable<System.Guid> KeeperId { get; set; }
        public string 保管人 { get; set; }
        public bool Done { get; set; }
        public System.DateTime RecordTime { get; set; }
        public System.Guid OperatorId { get; set; }
        public string 操作人 { get; set; }
        public System.DateTime OperationTime { get; set; }
        public System.DateTime OrderTime { get; set; }
    }
}
