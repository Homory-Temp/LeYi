using Windows.MMS.Picture.Import.App_Code.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Windows.MMS.Picture.Import.App_Code.ToModels;

namespace Windows.MMS.Picture.Import
{
    class Program
    {
        private static DepotEntities db = new DepotEntities();
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
                    Guid 图片库Id = Guid.Parse(ConfigurationManager.AppSettings["图片库Guid"]);
                    Guid 购置单Id = db.GlobalId();
                    db.DepotOrderAdd(购置单Id, 图片库Id, "图片库导入_{0}".Formatted(DateTime.Now.ToString("yyyyMMddHHmmss")), "导入", "导入", "导入", "导入", 0M, 0M, null, null, DateTime.Now, 图片库用户Id);
                    Console.WriteLine("图片库开始导入分类");
                    int i0 = 0;
                    foreach (var c in dbx.pcodelist.Where(o => o.type == "P" && o.parent == 0).OrderBy(o => o.id))
                    {
                        i0++;
                        Guid id;
                        if (db.DepotCatalog.Count(o => o.DepotId == 图片库Id && o.Code == c.code && o.State <  State.停用) == 0)
                        {
                            id = db.DepotCatalogAdd(图片库Id, null, Guid.Empty, c.name, i0, c.code);
                        }
                        else
                        {
                            id = db.DepotCatalog.Single(o => o.DepotId == 图片库Id && o.Code == c.code && o.State < State.停用).Id;
                        }
                        int j0 = 0;
                        foreach (var cc in dbx.pcodelist.Where(o => o.type == "P" && o.parent == c.id).OrderBy(o => o.id))
                        {
                            j0++;
                            Guid idx;
                            if (db.DepotCatalog.Count(o => o.DepotId == 图片库Id && o.Code == cc.code && o.State < State.停用) == 0)
                            {
                                idx = db.DepotCatalogAdd(图片库Id, id, id, cc.name, j0, cc.code);
                            }
                            else
                            {
                                idx = db.DepotCatalog.Single(o => o.DepotId == 图片库Id && o.Code == cc.code && o.State < State.停用).Id;
                            }
                            int k0 = 0;
                            foreach (var ccc in dbx.pcodelist.Where(o => o.type == "P" && o.parent == cc.id).OrderBy(o => o.id))
                            {
                                k0++;
                                if (db.DepotCatalog.Count(o => o.DepotId == 图片库Id && o.Code == ccc.code && o.State < State.停用) == 0)
                                {
                                    db.DepotCatalogAdd(图片库Id, idx, id, ccc.name, k0, ccc.code);
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
                        try
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
                            string a = "", b = "", c = "", d = "";
                            if (!(pimg == null || (pimg.pic1 == null && pimg.pic2 == null && pimg.pic3 == null && pimg.pic4 == null)))
                            {
                                if (pimg.pic1 != null)
                                {
                                    var p1 = ToPic(pimg.pic1);
                                    var pgid = Guid.NewGuid();
                                    p1.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                    a = "../Common/物资/图片/{0}.png".Formatted(pgid);
                                }
                                if (pimg.pic2 != null)
                                {
                                    var p2 = ToPic(pimg.pic2);
                                    var pgid = Guid.NewGuid();
                                    p2.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                    b = "../Common/物资/图片/{0}.png".Formatted(pgid);
                                }
                                if (pimg.pic3 != null)
                                {
                                    var p3 = ToPic(pimg.pic3);
                                    var pgid = Guid.NewGuid();
                                    p3.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                    c = "../Common/物资/图片/{0}.png".Formatted(pgid);
                                }
                                if (pimg.pic4 != null)
                                {
                                    var p4 = ToPic(pimg.pic4);
                                    var pgid = Guid.NewGuid();
                                    p4.Save(ConfigurationManager.AppSettings["图片库图片导出路径"].Formatted(pgid), System.Drawing.Imaging.ImageFormat.Png);
                                    d = "../Common/物资/图片/{0}.png".Formatted(pgid);
                                }
                            }
                            var catalogs = db.DepotCatalogLoad(图片库Id).Select(o => o.Id).Join(db.DepotObjectCatalog, o => o, o => o.CatalogId, (x, y) => y.ObjectId).Join(db.DepotObject, o => o, o => o.Id, (x, y) => y).ToList();
                            if (catalogs.Count(o => o.Extension == wz.code && o.State < State.停用) == 0)
                            {
                                var cc = wz.classcode.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[0];
                                var cco = db.DepotCatalogLoad(图片库Id).SingleOrDefault(o => o.Code == cc);
                                if (cco == null)
                                    continue;
                                var picid = db.GlobalId();

                                var gids = new List<Guid>();
                                gids.Add(cco.Id);
                                var xcco = cco.DepotCatalogParent;
                                while (xcco != null)
                                {
                                    gids.Add(xcco.Id);
                                    xcco = xcco.DepotCatalogParent;
                                }

                                db.DepotObjectAdd(picid, gids, 图片库Id, wz.name, false, false, false, "", "", "", wz.code, "套", wz.gg ?? "", 0, 0, a, b, c, d, wz.xh ?? "", ordinal, Age(wz.syfw.HasValue ? wz.syfw.Value : 0));

                                var @in = new InMemoryIn { Age = Age(wz.syfw.HasValue ? wz.syfw.Value : 0), Place = "图片库", Amount = dbx.P_picbarcode.Count(o => o.pid == pid && (o.state == "0" || o.state == "1")), CatalogId = cco.Id, Money = 0, Note = "", ObjectId = picid, PriceSet = 0, Time = DateTime.Today };
                                var list = new List<InMemoryIn>();
                                list.Add(@in);
                                db.DepotActIn(图片库Id, 购置单Id, DateTime.Today, 图片库用户Id, list);
                            }
                        }
                        catch
                        {
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
