using System;

public class HousingKey
{
    public Guid 学校 { get; set; }
    public string 姓名 { get; set; }
    public string 身份证号 { get; set; }
    public string 入学年份 { get; set; }
    public string 户籍 { get; set; }
    public string 住址 { get; set; }
}

public class HousingValue
{
    public string 班号 { get; set; }
    public DateTime 时间 { get; set; }
    public string 备注 { get; set; }
}

public class HousingRecord
{
    public Guid 学校 { get; set; }
    public string 姓名 { get; set; }
    public string 身份证号 { get; set; }
    public string 入学年份 { get; set; }
    public string 户籍 { get; set; }
    public string 住址 { get; set; }
    public string 班号 { get; set; }
    public string 备注 { get; set; }
    public DateTime 时间 { get; set; }

    public HousingRecord(HousingKey key, HousingValue value)
    {
        学校 = key.学校;
        姓名 = key.姓名;
        身份证号 = key.身份证号;
        入学年份 = key.入学年份;
        户籍 = key.户籍;
        住址 = key.住址;
        班号 = value.班号;
        时间 = value.时间;
        备注 = value.备注;
    }
}

public class HousingRecordX : HousingRecord
{
    public int 匹配度 { get; set; }

    public HousingRecordX(HousingKey key, HousingValue value)
        : base(key, value)
    {

    }
}

public class HousingCount
{
    public int 数量 { get; set; }
    public Housing_Department 单位 { get; set; }
}

public class HousingLog
{
    public DateTime 时间 { get; set; }
    public string 学生信息查询内容 { get; set; }
    public string 地址信息查询内容 { get; set; }
    public Guid 用户ID { get; set; }
    public string 用户姓名 { get; set; }
}
