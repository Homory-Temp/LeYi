using System;
using System.Collections.Generic;

namespace Models
{
    public class Image
    {
        public Image()
        {
            Images = new List<string>();
        }

        public List<string> Images { get; set; }

        public int Count { get { return Images.Count; } }
    }

    public class KV
    {
        public int K { get; set; }
        public bool V { get; set; }
    }

    public class QRC
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public int AutoId { get; set; }
        public Guid? ParentId { get; set; }
        public string Type { get; set; }
        public string Fixed { get; set; }
        public StorageObject Obj { get; set; }
    }

    public class Rights
    {
        public static readonly string[] List = new string[]{ "+", "*", "-", "?" };
    }

    public class S_CheckObj
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string People { get; set; }
        public StorageObject Obj { get; set; }
        public StorageIn In { get; set; }
        public List<SI_CheckObj> Items { get; set; }
    }

    public class SI_CheckObj
    {
        public Guid Id { get; set; }
        public int Ordinal { get; set; }
        public string Place { get; set; }
    }

    public class StoragePlaced
    {
        public string Place { get; set; }
        public List<int> Ordinals { get; set; }
    }

    public class CheckTable
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OperationUserId { get; set; }
        public int TimeNode { get; set; }
        public DateTime Time { get; set; }
        public string ContentItem { get; set; }
        public string ContentResult { get; set; }
    }

    public class ToCheckTable
    {
        public Guid Id { get; set; }
        public List<Guid> List { get; set; }
        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime Time { get; set; }
        public int TimeNode { get; set; }
        public Guid UserId { get; set; }
    }

    public enum ToOutType
    {
        Multiple = 1,
        Random = 2,
        Specific = 3
    }

    public class ToOutTable
    {
        public Guid ObjectId { get; set; }
        public Guid OutUserId { get; set; }
        public Guid OperatorId { get; set; }
        public OutType OutType { get; set; }
        public string OutReason { get; set; }
        public int? OutAmount { get; set; }
        public List<int> OutOrdinals { get; set; }
        public string OutNote { get; set; }
        public ToOutType Type { get; set; }
        public string Id
        {
            get; set;
        }
        public DateTime Time
        {
            get; set;
        }
    }

    public class OutDoneTable : ToOutTable
    {
        public Guid AuditUserId { get; set; }
        public int? OutDoneAmount { get; set; }
        public List<int> OutDoneOrdinals { get; set; }
    }
}
