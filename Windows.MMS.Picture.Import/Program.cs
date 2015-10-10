using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Windows.MMS.Picture.Import.App_Code.ToModels;
using System.Configuration;

namespace Windows.MMS.Picture.Import
{
    class Program
    {
        private static StorageEntity db = new StorageEntity();
        private static LibEntities dbx = new LibEntities();

        static void Main(string[] args)
        {
            var error = true;
            while (error)
            {
                try
                {
                    error = false;
                    Guid 学校Id = Guid.Parse(ConfigurationManager.AppSettings["学校Guid"]);
                    Guid 图片库用户Id = Guid.Parse(ConfigurationManager.AppSettings["图片库管理员Guid"]);
                    Guid 图片库Id;
                    Guid 图片库总分类Id;
                    var sid = ConfigurationManager.AppSettings["图片库Guid"];
                    if (string.IsNullOrWhiteSpace(sid))
                    {
                        Console.WriteLine("图片库未创建，正在创建中");
                        图片库Id = db.GlobalId();
                        图片库总分类Id = db.GlobalId();
                        db.StorageAdd(学校Id, "图片库", 1, 图片库Id, 图片库总分类Id);
                        db.SaveChanges();
                        Console.WriteLine("图片库和默认总分类创建成功");
                        db.InitializePermission(图片库用户Id, 图片库Id);
                        db.SaveChanges();
                        Console.WriteLine("图片库默认权限分配成功");
                    }
                    else
                    {
                        图片库Id = Guid.Parse(sid);
                        图片库总分类Id = Guid.Parse(ConfigurationManager.AppSettings["图片库总分类Guid"]);
                    }
                    Console.WriteLine("图片库购置单检测中");
                    var 图片库购置单编号 = ConfigurationManager.AppSettings["图片库购置单编号"];
                    Guid 图片库购置单Id;
                    if (db.StorageTarget.Count(o => o.Number == 图片库购置单编号) == 0)
                    {
                        Console.WriteLine("图片库购置单未获取，正在创建");
                        图片库购置单Id = db.StorageTargetAdd(图片库Id, 图片库购置单编号, 图片库购置单编号, "导入", "无锡市实验幼儿园", "图片", 0.00M, 0.00M, 图片库用户Id, 图片库用户Id, 图片库用户Id, DateTime.Now, DateTime.Today);
                        db.SaveChanges();
                        Console.WriteLine("图片库购置单创建成功");
                    }
                    else
                    {
                        图片库购置单Id = db.StorageTarget.Single(o => o.Number == 图片库购置单编号).Id;
                        Console.WriteLine("图片库购置单已获取");
                    }
                    Console.WriteLine("图片库开始导入分类");
                    int i0 = 0;
                    foreach (var c in dbx.pcodelist.Where(o => o.type == "P" && o.parent == 0).OrderBy(o => o.id))
                    {
                        i0++;
                        Guid id;
                        if (db.StorageCatalog.Count(o => o.StorageId == 图片库Id && o.Code == c.code && o.State < State.删除) == 0)
                        {
                            id = db.StorageCatalogAdd(图片库Id, 图片库总分类Id, c.name, i0, c.code);
                        }
                        else
                        {
                            id = db.StorageCatalog.Single(o => o.StorageId == 图片库Id && o.Code == c.code && o.State < State.删除).Id;
                        }
                        int j0 = 0;
                        foreach (var cc in dbx.pcodelist.Where(o => o.type == "P" && o.parent == c.id).OrderBy(o => o.id))
                        {
                            j0++;
                            Guid idx;
                            if (db.StorageCatalog.Count(o => o.StorageId == 图片库Id && o.Code == cc.code && o.State < State.删除) == 0)
                            {
                                idx = db.StorageCatalogAdd(图片库Id, id, cc.name, j0, cc.code);
                            }
                            else
                            {
                                idx = db.StorageCatalog.Single(o => o.StorageId == 图片库Id && o.Code == cc.code && o.State < State.删除).Id;
                            }
                            int k0 = 0;
                            foreach (var ccc in dbx.pcodelist.Where(o => o.type == "P" && o.parent == cc.id).OrderBy(o => o.id))
                            {
                                k0++;
                                if (db.StorageCatalog.Count(o => o.StorageId == 图片库Id && o.Code == ccc.code && o.State < State.删除) == 0)
                                {
                                    db.StorageCatalogAdd(图片库Id, idx, ccc.name, k0, ccc.code);
                                }
                            }
                        }
                    }
                    db.SaveChanges();
                    Console.WriteLine("图片库分类导入成功");
                    Console.WriteLine("图片库开始导入物资");
                    #region 导入物资
                    foreach (var wz in dbx.P_picinfo)
                    {
                        if (wz.classcode == null)
                            continue;
                        var classcode = wz.classcode.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        int ordinal;
                        try
                        {
                            ordinal = classcode.Length == 1 ? 99 : int.Parse(classcode[1]);
                        }
                        catch
                        {
                            ordinal = 99;
                        }
                        int pid = wz.pid;
                        var pimg = dbx.P_picimg.SingleOrDefault(o => o.pid == pid);
                        Image image;
                        image = new Image();
                        if (!(pimg == null || (pimg.pic1 == null && pimg.pic2 == null && pimg.pic3 == null && pimg.pic4 == null)))
                        {
                            if (pimg.pic1 != null)
                            {
                                var p1 = ToPic(pimg.pic1);
                                var pgid = Guid.NewGuid();
                                p1.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
                            }
                            if (pimg.pic2 != null)
                            {
                                var p2 = ToPic(pimg.pic2);
                                var pgid = Guid.NewGuid();
                                p2.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
                            }
                            if (pimg.pic3 != null)
                            {
                                var p3 = ToPic(pimg.pic3);
                                var pgid = Guid.NewGuid();
                                p3.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
                            }
                            if (pimg.pic4 != null)
                            {
                                var p4 = ToPic(pimg.pic4);
                                var pgid = Guid.NewGuid();
                                p4.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
                            }
                        }
                        if (db.StorageObject.Count(o => o.StorageId == 图片库Id && o.Code == wz.code && o.State < State.删除) == 0)
                        {
                            var cc = wz.classcode.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                            var cco = db.StorageCatalog.SingleOrDefault(o => o.Code == cc);
                            if (cco == null)
                                continue;
                            var picid = db.StorageObjectAdd(图片库Id, cco.Id, wz.name ?? "", "张", wz.gg, false, false, false, "", 0, 0, image, 图片库用户Id, ordinal, DateTime.Now, wz.xh, wz.code);
                            db.SaveChanges();
                            db.SetIn(picid, 图片库购置单Id, Age(wz.syfw.HasValue ? wz.syfw.Value : 0), "图片库", null, 图片库用户Id, 1, 0.00M, 0.00M, "");
                            db.SaveChanges();
                        }
                    }
                    Console.WriteLine("图片库物资导入成功");
                    #endregion
                }
                catch (Exception ex)
                {
                    error = true;
                    Console.WriteLine(ex.StackTrace);
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("图片库导入过程中发生错误，输入任意内容重新导入");
                    Console.ReadLine();
                }
            }
            Console.ReadLine();
        }

        //static void Main(string[] args)
        //{
        //    var error = true;
        //    while (error)
        //    {
        //        try
        //        {
        //            error = false;
        //            Guid 学校Id = Guid.Parse(ConfigurationManager.AppSettings["学校Guid"]);
        //            Guid 图片库用户Id = Guid.Parse(ConfigurationManager.AppSettings["图片库管理员Guid"]);
        //            Guid 图片库Id;
        //            Guid 图片库总分类Id;
        //            var sid = ConfigurationManager.AppSettings["图片库Guid"];
        //            if (string.IsNullOrWhiteSpace(sid))
        //            {
        //                Console.WriteLine("图片库未创建，正在创建中");
        //                图片库Id = db.GlobalId();
        //                图片库总分类Id = db.GlobalId();
        //                db.StorageAdd(学校Id, "图片库", 1, 图片库Id, 图片库总分类Id);
        //                db.SaveChanges();
        //                Console.WriteLine("图片库和默认总分类创建成功");
        //                db.InitializePermission(图片库用户Id, 图片库Id);
        //                db.SaveChanges();
        //                Console.WriteLine("图片库默认权限分配成功");
        //            }
        //            else
        //            {
        //                图片库Id = Guid.Parse(sid);
        //                图片库总分类Id = Guid.Parse(ConfigurationManager.AppSettings["图片库总分类Guid"]);
        //            }
        //            Console.WriteLine("图片库购置单检测中");
        //            var 图片库购置单编号 = ConfigurationManager.AppSettings["图片库购置单编号"];
        //            Guid 图片库购置单Id;
        //            if (db.StorageTarget.Count(o => o.Number == 图片库购置单编号) == 0)
        //            {
        //                Console.WriteLine("图片库购置单未获取，正在创建");
        //                图片库购置单Id = db.StorageTargetAdd(图片库Id, 图片库购置单编号, 图片库购置单编号, "导入", "无锡市实验幼儿园", "图片", 0.00M, 0.00M, 图片库用户Id, 图片库用户Id, 图片库用户Id, DateTime.Now, DateTime.Today);
        //                db.SaveChanges();
        //                Console.WriteLine("图片库购置单创建成功");
        //            }
        //            else
        //            {
        //                图片库购置单Id = db.StorageTarget.Single(o => o.Number == 图片库购置单编号).Id;
        //                Console.WriteLine("图片库购置单已获取");
        //            }
        //            Console.WriteLine("图片库开始导入分类");
        //            int i0 = 0;
        //            foreach (var c in dbx.pcodelist.Where(o => o.type == "P" && o.parent == 0).OrderBy(o => o.id))
        //            {
        //                i0++;
        //                Guid id;
        //                if (db.StorageCatalog.Count(o => o.StorageId == 图片库Id && o.Code == c.code && o.State < State.删除) == 0)
        //                {
        //                    id = db.StorageCatalogAdd(图片库Id, 图片库总分类Id, c.name, i0, c.code);
        //                }
        //                else
        //                {
        //                    id = db.StorageCatalog.Single(o => o.StorageId == 图片库Id && o.Code == c.code && o.State < State.删除).Id;
        //                }
        //                int j0 = 0;
        //                foreach (var cc in dbx.pcodelist.Where(o => o.type == "P" && o.parent == c.id).OrderBy(o => o.id))
        //                {
        //                    j0++;
        //                    Guid idx;
        //                    if (db.StorageCatalog.Count(o => o.StorageId == 图片库Id && o.Code == cc.code && o.State < State.删除) == 0)
        //                    {
        //                        idx = db.StorageCatalogAdd(图片库Id, id, cc.name, j0, cc.code);
        //                    }
        //                    else
        //                    {
        //                        idx = db.StorageCatalog.Single(o => o.StorageId == 图片库Id && o.Code == cc.code && o.State < State.删除).Id;
        //                    }
        //                    int k0 = 0;
        //                    foreach (var ccc in dbx.pcodelist.Where(o => o.type == "P" && o.parent == cc.id).OrderBy(o => o.id))
        //                    {
        //                        k0++;
        //                        if (db.StorageCatalog.Count(o => o.StorageId == 图片库Id && o.Code == ccc.code && o.State < State.删除) == 0)
        //                        {
        //                            db.StorageCatalogAdd(图片库Id, idx, ccc.name, k0, ccc.code);
        //                        }
        //                    }
        //                }
        //            }
        //            db.SaveChanges();
        //            Console.WriteLine("图片库分类导入成功");
        //            Console.WriteLine("图片库开始导入物资");
        //            #region 导入物资
        //            foreach (var wz in dbx.P_picinfo)
        //            {
        //                if (wz.classcode == null)
        //                    continue;
        //                var classcode = wz.classcode.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        //                int ordinal;
        //                try
        //                {
        //                    ordinal = classcode.Length == 1 ? 99 : int.Parse(classcode[1]);
        //                }
        //                catch
        //                {
        //                    ordinal = 99;
        //                }
        //                int pid = wz.pid;
        //                var pimg = dbx.P_picimg.SingleOrDefault(o => o.pid == pid);
        //                Image image;
        //                image = new Image();
        //                if (!(pimg == null || (pimg.pic1 == null && pimg.pic2 == null && pimg.pic3 == null && pimg.pic4 == null)))
        //                {
        //                    if (pimg.pic1 != null)
        //                    {
        //                        var p1 = ToPic(pimg.pic1);
        //                        var pgid = Guid.NewGuid();
        //                        p1.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
        //                        image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
        //                    }
        //                    if (pimg.pic2 != null)
        //                    {
        //                        var p2 = ToPic(pimg.pic2);
        //                        var pgid = Guid.NewGuid();
        //                        p2.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
        //                        image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
        //                    }
        //                    if (pimg.pic3 != null)
        //                    {
        //                        var p3 = ToPic(pimg.pic3);
        //                        var pgid = Guid.NewGuid();
        //                        p3.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
        //                        image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
        //                    }
        //                    if (pimg.pic4 != null)
        //                    {
        //                        var p4 = ToPic(pimg.pic4);
        //                        var pgid = Guid.NewGuid();
        //                        p4.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
        //                        image.Images.Add("~/Upload/{0}.png".Formatted(pgid));
        //                    }
        //                }
        //                if (db.StorageObject.Count(o => o.StorageId == 图片库Id && o.Code == wz.code && o.State < State.删除) == 0)
        //                {
        //                    var cc = wz.classcode.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
        //                    var cco = db.StorageCatalog.SingleOrDefault(o => o.Code == cc);
        //                    if (cco == null)
        //                        continue;
        //                    var picid = db.StorageObjectAdd(图片库Id, cco.Id, wz.name ?? "", "张", wz.gg, false, true, false, "", 0, 0, image, 图片库用户Id, ordinal, DateTime.Now, wz.xh, wz.code);
        //                    db.SaveChanges();
        //                    db.SetIn(picid, 图片库购置单Id, Age(wz.syfw.HasValue ? wz.syfw.Value : 0), "图片库", null, 图片库用户Id, 1, 0.00M, 0.00M, "");
        //                    db.SaveChanges();
        //                }
        //            }
        //            Console.WriteLine("图片库物资导入成功");
        //            #endregion
        //        }
        //        catch(Exception ex)
        //        {
        //            error = true;
        //            Console.WriteLine(ex.StackTrace);
        //            Console.WriteLine(ex.Message);
        //            Console.WriteLine("图片库导入过程中发生错误，输入任意内容重新导入");
        //            Console.ReadLine();
        //        }
        //    }
        //    Console.ReadLine();
        //}

        static System.Drawing.Image ToPic(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

        static string Age(int type)
        {
            switch(type)
            {
                case 4:
                    return "托班上学期";
                case 5:
                    return "托班下学期";
                case 7:
                    return "小班上学期";
                case 18:
                    return "小班下学期";
                case 19:
                    return "中班上学期";
                case 20:
                    return "中班下学期";
                case 21:
                    return "大班上学期";
                case 22:
                    return "大班下学期";
                case 25:
                    return "通用";
                case 29:
                    return "中小通用";
                case 30:
                    return "中大通用";
            }
            return "";
        }
    }
}
