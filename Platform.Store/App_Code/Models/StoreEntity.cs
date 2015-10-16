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
        public int State { get; set; }
        public int Ordinal { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepartmentUser
    {
        public System.Guid DepartmentId { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid TopDepartmentId { get; set; }
        public int Type { get; set; }
        public System.DateTime Time { get; set; }
        public int State { get; set; }
        public int Ordinal { get; set; }
    
        public virtual User User { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Store()
        {
            this.StoreCatalog = new HashSet<StoreCatalog>();
            this.StoreDictionary = new HashSet<StoreDictionary>();
            this.StoreRole = new HashSet<StoreRole>();
            this.StoreTarget = new HashSet<StoreTarget>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid CampusId { get; set; }
        public int Ordinal { get; set; }
        public int DefaultView { get; set; }
        public int DefaultType { get; set; }
        public string Types { get; set; }
        public StoreState State { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreCatalog> StoreCatalog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreDictionary> StoreDictionary { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreRole> StoreRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreTarget> StoreTarget { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store_Creator
    {
        public System.Guid Id { get; set; }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public System.Guid Stamp { get; set; }
        public string Password { get; set; }
        public string PasswordEx { get; set; }
        public string CryptoKey { get; set; }
        public string CryptoSalt { get; set; }
        public int Type { get; set; }
        public int State { get; set; }
        public int Ordinal { get; set; }
        public string Description { get; set; }
        public string PinYin { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Store_Visitor
    {
        public System.Guid Id { get; set; }
        public string RealName { get; set; }
        public string PinYin { get; set; }
        public int State { get; set; }
        public string Right { get; set; }
        public System.Guid StoreId { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreCatalog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreCatalog()
        {
            this.ChildrenStoreCatalog = new HashSet<StoreCatalog>();
            this.SourceStoreObject = new HashSet<StoreObject>();
            this.StoreObject = new HashSet<StoreObject>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public System.Guid StoreId { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
        public int Ordinal { get; set; }
        public int State { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
    
        public virtual Store Store { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreCatalog> ChildrenStoreCatalog { get; set; }
        public virtual StoreCatalog ParentStoreCatalog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreObject> SourceStoreObject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreObject> StoreObject { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreConsume
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreConsume()
        {
            this.StoreConsumeSingle = new HashSet<StoreConsumeSingle>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid ConsumeUserId { get; set; }
        public string Note { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperationUserId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
    
        public virtual User ConsumeUser { get; set; }
        public virtual User OperationUser { get; set; }
        public virtual StoreObject StoreObject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreConsumeSingle> StoreConsumeSingle { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreConsumeSingle
    {
        public System.Guid Id { get; set; }
        public System.Guid InId { get; set; }
        public System.Guid ConsumeId { get; set; }
        public int Ordinal { get; set; }
        public int Amount { get; set; }
        public decimal PerPrice { get; set; }
        public decimal SourcePerPrice { get; set; }
        public decimal Fee { get; set; }
        public decimal Money { get; set; }
    
        public virtual StoreConsume StoreConsume { get; set; }
        public virtual StoreIn StoreIn { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreDictionary
    {
        public System.Guid StoreId { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreFlow
    {
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid UserId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
        public string Note { get; set; }
    
        public virtual StoreObject StoreObject { get; set; }
        public virtual User User { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreFlowSingle
    {
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public int ObjectOrdinal { get; set; }
        public System.Guid UserId { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
        public string Note { get; set; }
    
        public virtual StoreObject StoreObject { get; set; }
        public virtual User User { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreIn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreIn()
        {
            this.StoreConsumeSingle = new HashSet<StoreConsumeSingle>();
            this.StoreInSingle = new HashSet<StoreInSingle>();
            this.StoreOutSingle = new HashSet<StoreOutSingle>();
            this.StoreLendSingle = new HashSet<StoreLendSingle>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid TargetId { get; set; }
        public System.Guid ObjectId { get; set; }
        public string Age { get; set; }
        public string Place { get; set; }
        public string Image { get; set; }
        public Nullable<System.Guid> ResponsibleUserId { get; set; }
        public string Note { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperationUserId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal PerPrice { get; set; }
        public decimal SourcePerPrice { get; set; }
        public decimal Fee { get; set; }
        public decimal Money { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreConsumeSingle> StoreConsumeSingle { get; set; }
        public virtual User OperationUser { get; set; }
        public virtual User ResponsibleUser { get; set; }
        public virtual StoreObject StoreObject { get; set; }
        public virtual StoreTarget StoreTarget { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreInSingle> StoreInSingle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreOutSingle> StoreOutSingle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreLendSingle> StoreLendSingle { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreInSingle
    {
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid InId { get; set; }
        public int Ordinal { get; set; }
        public bool In { get; set; }
        public string Place { get; set; }
        public int AutoId { get; set; }
    
        public virtual StoreIn StoreIn { get; set; }
        public virtual StoreObject StoreObject { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreLend
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreLend()
        {
            this.StoreReturnSingle = new HashSet<StoreReturnSingle>();
            this.StoreLendSingle = new HashSet<StoreLendSingle>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid LendUserId { get; set; }
        public string Note { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperationUserId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
        public bool Returned { get; set; }
    
        public virtual User LendUser { get; set; }
        public virtual User OperationUser { get; set; }
        public virtual StoreObject StoreObject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreReturnSingle> StoreReturnSingle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreLendSingle> StoreLendSingle { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreLendSingle
    {
        public System.Guid Id { get; set; }
        public System.Guid InId { get; set; }
        public System.Guid LendId { get; set; }
        public int Ordinal { get; set; }
        public int Amount { get; set; }
        public decimal PerPrice { get; set; }
        public decimal SourcePerPrice { get; set; }
        public decimal Fee { get; set; }
        public decimal Money { get; set; }
        public decimal ReturnedAmount { get; set; }
        public decimal ReturnedMoney { get; set; }
    
        public virtual StoreIn StoreIn { get; set; }
        public virtual StoreLend StoreLend { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreObject()
        {
            this.StoreConsume = new HashSet<StoreConsume>();
            this.StoreFlow = new HashSet<StoreFlow>();
            this.StoreFlowSingle = new HashSet<StoreFlowSingle>();
            this.StoreIn = new HashSet<StoreIn>();
            this.StoreInSingle = new HashSet<StoreInSingle>();
            this.StoreLend = new HashSet<StoreLend>();
            this.StoreOut = new HashSet<StoreOut>();
            this.StoreReturn = new HashSet<StoreReturn>();
            this.StoreStatistics = new HashSet<StoreStatistics>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid CatalogId { get; set; }
        public Nullable<System.Guid> SourceCatalogId { get; set; }
        public string Name { get; set; }
        public string PinYin { get; set; }
        public bool Single { get; set; }
        public bool Consumable { get; set; }
        public bool Fixed { get; set; }
        public string Serial { get; set; }
        public string Unit { get; set; }
        public string Specification { get; set; }
        public decimal Low { get; set; }
        public decimal High { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperationUserId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public int Ordinal { get; set; }
        public int State { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
    
        public virtual StoreCatalog SourceStoreCatalog { get; set; }
        public virtual StoreCatalog StoreCatalog { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreConsume> StoreConsume { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreFlow> StoreFlow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreFlowSingle> StoreFlowSingle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreIn> StoreIn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreInSingle> StoreInSingle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreLend> StoreLend { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreOut> StoreOut { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreReturn> StoreReturn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreStatistics> StoreStatistics { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreOut
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreOut()
        {
            this.StoreOutSingle = new HashSet<StoreOutSingle>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid OutUserId { get; set; }
        public int Type { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperationUserId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
    
        public virtual StoreObject StoreObject { get; set; }
        public virtual User OperationUser { get; set; }
        public virtual User OutUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreOutSingle> StoreOutSingle { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreOutSingle
    {
        public System.Guid Id { get; set; }
        public System.Guid InId { get; set; }
        public System.Guid OutId { get; set; }
        public int Ordinal { get; set; }
        public int Amount { get; set; }
        public decimal PerPrice { get; set; }
        public decimal SourcePerPrice { get; set; }
        public decimal Fee { get; set; }
        public decimal Money { get; set; }
    
        public virtual StoreIn StoreIn { get; set; }
        public virtual StoreOut StoreOut { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreReturn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreReturn()
        {
            this.StoreReturnSingle = new HashSet<StoreReturnSingle>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid ObjectId { get; set; }
        public System.Guid ReturnUserId { get; set; }
        public string Note { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperationUserId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public int AutoId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
    
        public virtual StoreObject StoreObject { get; set; }
        public virtual User OperationUser { get; set; }
        public virtual User ReturnUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreReturnSingle> StoreReturnSingle { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreReturnSingle
    {
        public System.Guid Id { get; set; }
        public System.Guid LendId { get; set; }
        public System.Guid ReturnId { get; set; }
        public int Ordinal { get; set; }
        public decimal Amount { get; set; }
        public decimal Money { get; set; }
    
        public virtual StoreLend StoreLend { get; set; }
        public virtual StoreReturn StoreReturn { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreRole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreRole()
        {
            this.User = new HashSet<User>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Right { get; set; }
        public int Ordinal { get; set; }
        public int State { get; set; }
    
        public virtual Store Store { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreStatistics
    {
        public System.Guid ObjectId { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal StartInAmount { get; set; }
        public decimal StartInMoney { get; set; }
        public decimal StartLendAmount { get; set; }
        public decimal StartLendMoney { get; set; }
        public decimal StartConsumeAmount { get; set; }
        public decimal StartConsumeMoney { get; set; }
        public decimal StartOutAmount { get; set; }
        public decimal StartOutMoney { get; set; }
        public decimal EndInAmount { get; set; }
        public decimal EndInMoney { get; set; }
        public decimal EndLendAmount { get; set; }
        public decimal EndLendMoney { get; set; }
        public decimal EndConsumeAmount { get; set; }
        public decimal EndConsumeMoney { get; set; }
        public decimal EndOutAmount { get; set; }
        public decimal EndOutMoney { get; set; }
    
        public virtual StoreObject StoreObject { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreTarget
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StoreTarget()
        {
            this.StoreIn = new HashSet<StoreIn>();
        }
    
        public System.Guid Id { get; set; }
        public System.Guid StoreId { get; set; }
        public string Number { get; set; }
        public string ReceiptNumber { get; set; }
        public string OrderSource { get; set; }
        public string UsageTarget { get; set; }
        public string Content { get; set; }
        public decimal ToPay { get; set; }
        public decimal Paid { get; set; }
        public Nullable<System.Guid> BrokerageUserId { get; set; }
        public Nullable<System.Guid> KeepUserId { get; set; }
        public bool In { get; set; }
        public int TimeNode { get; set; }
        public System.DateTime Time { get; set; }
        public System.Guid OperationUserId { get; set; }
        public System.DateTime OperationTime { get; set; }
        public int State { get; set; }
    
        public virtual Store Store { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreIn> StoreIn { get; set; }
        public virtual User BrokerageUser { get; set; }
        public virtual User KeepUser { get; set; }
        public virtual User OperationUser { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.StoreConsumeUser = new HashSet<StoreConsume>();
            this.StoreConsumeOperator = new HashSet<StoreConsume>();
            this.StoreFlowUser = new HashSet<StoreFlow>();
            this.StoreFlowSingleUser = new HashSet<StoreFlowSingle>();
            this.StoreInOperator = new HashSet<StoreIn>();
            this.StoreInResponsible = new HashSet<StoreIn>();
            this.StoreLendUser = new HashSet<StoreLend>();
            this.StoreLendOperator = new HashSet<StoreLend>();
            this.StoreObjectOperator = new HashSet<StoreObject>();
            this.StoreOutOperator = new HashSet<StoreOut>();
            this.StoreOutUser = new HashSet<StoreOut>();
            this.StoreReturnOperator = new HashSet<StoreReturn>();
            this.StoreReturnUser = new HashSet<StoreReturn>();
            this.StoreTargetBrokerage = new HashSet<StoreTarget>();
            this.StoreTargetKeep = new HashSet<StoreTarget>();
            this.StoreTargetUser = new HashSet<StoreTarget>();
            this.StoreRole = new HashSet<StoreRole>();
            this.DepartmentUser = new HashSet<DepartmentUser>();
            this.UserOnline = new HashSet<UserOnline>();
        }
    
        public System.Guid Id { get; set; }
        public string Account { get; set; }
        public string RealName { get; set; }
        public string DisplayName { get; set; }
        public string Icon { get; set; }
        public System.Guid Stamp { get; set; }
        public string Password { get; set; }
        public string PasswordEx { get; set; }
        public string CryptoKey { get; set; }
        public string CryptoSalt { get; set; }
        public int Type { get; set; }
        public int State { get; set; }
        public int Ordinal { get; set; }
        public string Description { get; set; }
        public string PinYin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreConsume> StoreConsumeUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreConsume> StoreConsumeOperator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreFlow> StoreFlowUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreFlowSingle> StoreFlowSingleUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreIn> StoreInOperator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreIn> StoreInResponsible { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreLend> StoreLendUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreLend> StoreLendOperator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreObject> StoreObjectOperator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreOut> StoreOutOperator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreOut> StoreOutUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreReturn> StoreReturnOperator { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreReturn> StoreReturnUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreTarget> StoreTargetBrokerage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreTarget> StoreTargetKeep { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreTarget> StoreTargetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StoreRole> StoreRole { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentUser> DepartmentUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserOnline> UserOnline { get; set; }
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
    
        public virtual User User { get; set; }
    }
}
namespace Models
{
    using System;
    
    public enum StoreState : int
    {
        内置 = 0,
        启用 = 1,
        删除 = 2,
        食品 = -1,
        固产 = -2
    }
}