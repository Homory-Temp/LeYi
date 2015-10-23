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
    
    public partial class Depot
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.Guid CampusId { get; set; }
        public int Ordinal { get; set; }
        public string DefaultObjectView { get; set; }
        public string DefaultObjectType { get; set; }
        public string ObjectTypes { get; set; }
        public DepotType Type { get; set; }
        public State State { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotCatalog
    {
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
        public System.Guid TopCatalogId { get; set; }
        public bool IsVirtual { get; set; }
    }
}
namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepotOrder
    {
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
        public int State { get; set; }
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
    
    [Flags]
    public enum DepotType : int
    {
        通用库 = 1,
        易耗品库 = 2,
        固定资产库 = 4,
        可借可领库 = 8,
        对象一级分类库 = 16
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
    
    public enum State : int
    {
        启用 = 1,
        停用 = 2,
        内置 = 0
    }
}
