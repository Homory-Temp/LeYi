﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Application
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Home { get; set; }
        public string Quit { get; set; }
        public int Type { get; set; }
        public State State { get; set; }
        public int Ordinal { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Department
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department()
        {
            this.DepartmentChildren = new HashSet<Department>();
            this.DepartmentAll = new HashSet<Department>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public System.Guid TopId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Level { get; set; }
        public bool Hidden { get; set; }
        public int Type { get; set; }
        public State State { get; set; }
        public int Ordinal { get; set; }
        public int AutoId { get; set; }
        public int BuildType { get; set; }
        public int ClassType { get; set; }
        public string Code { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> DepartmentChildren { get; set; }
        public virtual Department DepartmentParent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> DepartmentAll { get; set; }
        public virtual Department DepartmentCampus { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Depot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Depot()
        {
            this.DepotUse = new HashSet<DepotUse>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid CampusId { get; set; }
        public int Ordinal { get; set; }
        public string DefaultObjectView { get; set; }
        public string DefaultObjectType { get; set; }
        public string ObjectTypes { get; set; }
        public DepotType Type { get; set; }
        public State State { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotUse> DepotUse { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotCatalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepotCatalog()
        {
            this.DepotCatalogChildren = new HashSet<DepotCatalog>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public System.Guid TopId { get; set; }
        public System.Guid DepotId { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
        public int Ordinal { get; set; }
        public State State { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotCatalog> DepotCatalogChildren { get; set; }
        public virtual DepotCatalog DepotCatalogParent { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotCatalogTree
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public System.Guid TopId { get; set; }
        public System.Guid DepotId { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
        public int Ordinal { get; set; }
        public int State { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotCreator
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
        public State State { get; set; }
        public System.Guid CampusId { get; set; }
        public string CampusName { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotDictionary
    {
        public System.Guid DepotId { get; set; }
        public DictionaryType Type { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
    }
}
namespace Models
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
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotFlowX
    {
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public int ObjectOrdinal { get; set; }
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
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotIn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepotIn()
        {
            this.DepotInX = new HashSet<DepotInX>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid OrderId { get; set; }
        public System.Guid ObjectId { get; set; }
        public string Age { get; set; }
        public string Place { get; set; }
        public Nullable<System.Guid> ResponsibleId { get; set; }
        public string Note { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperatorId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public decimal Amount { get; set; }
        public decimal PriceSet { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal AvailableAmount { get; set; }
    
        public virtual DepotObject DepotObject { get; set; }
        public virtual DepotOrder DepotOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotInX> DepotInX { get; set; }
    }
}
namespace Models
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
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotInX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepotInX()
        {
            this.DepotUseX = new HashSet<DepotUseX>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid InId { get; set; }
        public System.Guid OrderId { get; set; }
        public System.Guid ObjectId { get; set; }
        public string Age { get; set; }
        public string Place { get; set; }
        public int Ordinal { get; set; }
        public decimal Amount { get; set; }
        public decimal PriceSet { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal AvailableAmount { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
    
        public virtual DepotIn DepotIn { get; set; }
        public virtual DepotObject DepotObject { get; set; }
        public virtual DepotOrder DepotOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotUseX> DepotUseX { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotInXRecord
    {
        public string OrderName { get; set; }
        public bool Done { get; set; }
        public string Operator { get; set; }
        public string Responsible { get; set; }
        public string Name { get; set; }
        public System.Guid CatalogId { get; set; }
        public int Level { get; set; }
        public string CatalogName { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public System.Guid InId { get; set; }
        public System.Guid OrderId { get; set; }
        public System.Guid ObjectId { get; set; }
        public string Age { get; set; }
        public string Place { get; set; }
        public Nullable<System.Guid> ResponsibleId { get; set; }
        public string Note { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperatorId { get; set; }
        public decimal PriceSet { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
        public bool IsVirtual { get; set; }
        public string OrderSource { get; set; }
        public string UsageTarget { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotMember
    {
        public System.Guid Id { get; set; }
        public string RealName { get; set; }
        public string PinYin { get; set; }
        public string Rights { get; set; }
        public System.Guid DepotId { get; set; }
        public System.Guid RoleId { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepotObject()
        {
            this.DepotFlow = new HashSet<DepotFlow>();
            this.DepotFlowX = new HashSet<DepotFlowX>();
            this.DepotIn = new HashSet<DepotIn>();
            this.DepotStatistics = new HashSet<DepotStatistics>();
            this.DepotInX = new HashSet<DepotInX>();
            this.DepotUseX = new HashSet<DepotUseX>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
        public bool Single { get; set; }
        public bool Consumable { get; set; }
        public bool Fixed { get; set; }
        public string SerialA { get; set; }
        public string SerialB { get; set; }
        public string SerialC { get; set; }
        public string SerialD { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public string ImageA { get; set; }
        public string ImageB { get; set; }
        public string ImageC { get; set; }
        public string ImageD { get; set; }
        public string Note { get; set; }
        public int Ordinal { get; set; }
        public State State { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotFlow> DepotFlow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotFlowX> DepotFlowX { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotIn> DepotIn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotStatistics> DepotStatistics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotInX> DepotInX { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotUseX> DepotUseX { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotObjectCatalog
    {
        public System.Guid ObjectId { get; set; }
        public System.Guid CatalogId { get; set; }
        public int Level { get; set; }
        public bool IsLeaf { get; set; }
        public bool IsVirtual { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepotOrder()
        {
            this.DepotIn = new HashSet<DepotIn>();
            this.DepotInX = new HashSet<DepotInX>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid DepotId { get; set; }
        public string Name { get; set; }
        public string Receipt { get; set; }
        public string OrderSource { get; set; }
        public string UsageTarget { get; set; }
        public string Note { get; set; }
        public decimal ToPay { get; set; }
        public decimal Paid { get; set; }
        public Nullable<System.Guid> BrokerageId { get; set; }
        public Nullable<System.Guid> KeeperId { get; set; }
        public bool Done { get; set; }
        public System.DateTime OrderTime { get; set; }
        public System.DateTime RecordTime { get; set; }
        public System.Guid OperatorId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public State State { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotIn> DepotIn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotInX> DepotInX { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotRole
    {
        public System.Guid Id { get; set; }
        public System.Guid DepotId { get; set; }
        public string Name { get; set; }
        public string Rights { get; set; }
        public int Ordinal { get; set; }
        public State State { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotStatistics
    {
        public System.Guid ObjectId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public System.DateTime Time { get; set; }
        public decimal StartAmount { get; set; }
        public decimal StartMoney { get; set; }
        public decimal InAmount { get; set; }
        public decimal InMoney { get; set; }
        public decimal LendAmount { get; set; }
        public decimal LendMoney { get; set; }
        public decimal ConsumeAmount { get; set; }
        public decimal ConsumeMoney { get; set; }
        public decimal OutAmount { get; set; }
        public decimal OutMoney { get; set; }
        public decimal RedoAmount { get; set; }
        public decimal RedoMoney { get; set; }
        public decimal EndAmount { get; set; }
        public decimal EndMoney { get; set; }
    
        public virtual DepotObject DepotObject { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotUse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepotUse()
        {
            this.DepotUseX = new HashSet<DepotUseX>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid DepotId { get; set; }
        public System.Guid UserId { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperatorId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public string Age { get; set; }
        public string Place { get; set; }
        public decimal Money { get; set; }
    
        public virtual Depot Depot { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepotUseX> DepotUseX { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotUser
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
        public State State { get; set; }
        public System.Guid CampusId { get; set; }
        public string CampusName { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotUserRole
    {
        public System.Guid UserId { get; set; }
        public System.Guid DepotRoleId { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotUseX
    {
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid UseId { get; set; }
        public System.Guid InXId { get; set; }
        public UseType Type { get; set; }
        public string Age { get; set; }
        public string Place { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
        public decimal ReturnedAmount { get; set; }
        public string Note { get; set; }
    
        public virtual DepotInX DepotInX { get; set; }
        public virtual DepotObject DepotObject { get; set; }
        public virtual DepotUse DepotUse { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserOnline
    {
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public System.DateTime TimeStamp { get; set; }
    }
}
namespace Models
{
    using System;
    
    public enum CodeType : int
    {
        Catalog = 1,
        Object = 2,
        Single = 3
    }
}
namespace Models
{
    using System;
    
    [Flags]
    public enum DepotType : int
    {
        通用库 = 1,
        易耗品库 = 2,
        固定资产库 = 4,
        可借可领库 = 8,
        对象一级分类库 = 16,
        小数数量库 = 32,
        无 = 0
    }
}
namespace Models
{
    using System;
    
    public enum DictionaryType : int
    {
        单位 = 1,
        规格 = 2,
        购置来源 = 3,
        使用对象 = 4,
        年龄段 = 5,
        存放地 = 6
    }
}
namespace Models
{
    using System;
    
    public enum FlowType : int
    {
        入库 = 1,
        入库修改 = 2,
        借用出库 = 3,
        领用出库 = 4
    }
}
namespace Models
{
    using System;
    
    public enum State : int
    {
        启用 = 1,
        停用 = 2,
        内置 = 0
    }
}
namespace Models
{
    using System;
    
    public enum UseType : int
    {
        领用 = 1,
        借用 = 2
    }
}
