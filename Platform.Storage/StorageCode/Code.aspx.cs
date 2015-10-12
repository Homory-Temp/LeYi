using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using SysImage = System.Drawing.Image;
using ICSharpCode.SharpZipLib.Zip;

public partial class Code : SingleStoragePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tree.SourceBind(QRCL);
            tree.InitialTree(-1, 4);
            view.Rebind();
        }
    }

    private List<QRC> _qrcl;

    protected List<QRC> QRCL
    {
        get
        {
            if (_qrcl == null)
            {
                _qrcl = new List<QRC>();
                _qrcl.AddRange(db.Value.StorageCatalogGet(StorageId).OrderBy(o => o.Ordinal).ToList().Select(o => new QRC { Obj = null, Id = o.Id, ParentId = o.ParentId, AutoId = o.AutoId, Name = o.Name, Type = "F" }).ToList());
                _qrcl.AddRange(db.Value.StorageObjectGet(StorageId).OrderBy(o => o.Ordinal).ToList().Select(o => new QRC { Obj = o, Id = o.Id, ParentId = o.CatalogId, AutoId = o.AutoId, Name = o.Name, Type = "W", Fixed = o.Fixed ? o.FixedSerial : null }).ToList());
                _qrcl.AddRange(db.Value.StorageObjectGet(StorageId).Where(o => o.Single).ToList().SelectMany(o => o.StorageInSingle).OrderBy(o => o.InOrdinal).Select(o => new QRC { Obj = o.StorageObject, Id = o.Id, ParentId = o.StorageObject.Id, Fixed = o.StorageObject.Fixed ? o.StorageObject.FixedSerial : null, AutoId = o.AutoId, Name = "{0}-{1}".Formatted(o.StorageObject.Name, o.InOrdinal), Type = "D" }).ToList());
            }
            return _qrcl;
        }
    }

    protected string ConvertCode(string type, int autoId, string name)
    {
        return db.Value.ToQR(type, autoId, name);
        //var bytes = Encoding.UTF8.GetBytes(db.Value.ToQR(type, autoId, name));
        //StringBuilder sb = new StringBuilder();
        //foreach (var b in bytes)
        //    sb.Append(b.ToString("X2"));
        //return sb.ToString();
    }

    protected string ConvertCode(string type, int autoId)
    {
        return db.Value.ToQR(type, autoId);
    }

    protected string ConvertCode(string type, int autoId, StorageObject obj)
    {
        var content = db.Value.ToQR(type, autoId, " {0}（{1}）".Formatted(obj.Name, obj.GeneratePath()));
        return content;
        //return content;
        //var bytes = Encoding.GetEncoding("GB2312").GetBytes(content);
        //StringBuilder sb = new StringBuilder();
        //foreach (var b in bytes)
        //    sb.Append(b.ToString("X2"));
        //return sb.ToString();
    }

    protected void tree_NodeClick(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        e.Node.Selected = false;
        if (e.Node.Checkable)
            e.Node.Checked = !e.Node.Checked;
        foreach (var n in e.Node.GetAllNodes())
        {
            if (n.Checkable)
            {
                n.Checked = !n.Checked;
            }
        }
        view.Rebind();
    }

    protected void view_NeedDataSource(object sender, Telerik.Web.UI.RadListViewNeedDataSourceEventArgs e)
    {
        var source = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList().Join(QRCL, o => o, o => o.Id, (x, y) => y).ToList();
        switch (rb.SelectedIndex)
        {
            case 1:
                source = source.Where(o => o.Obj.Fixed == true).ToList();
                break;
            case 2:
                source = source.Where(o => o.Obj.Fixed == false && o.Obj.Single == true).ToList();
                break;
        }
        view.Source(source);
        printx.Visible = source != null && source.Count > 0;
    }

    protected void tree_NodeCheck(object sender, Telerik.Web.UI.RadTreeNodeEventArgs e)
    {
        view.Rebind();
    }

    protected void tree_NodeDataBound(object sender, RadTreeNodeEventArgs e)
    {
        var item = e.Node.DataItem as QRC;
        if (item.Type == "F")
        {
            e.Node.Checkable = false;
        }
        else
        {
            var obj = item.Obj;
            if (obj.Consumable)
            {
                e.Node.Checkable = true;
            }
            else
            {
                if ((obj.Single && item.Type == "D") || (!obj.Single && item.Type == "W"))
                {
                    e.Node.Checkable = true;
                }
                else
                {
                    e.Node.Checkable = false;
                }
            }
        }
        //if ((e.Node.DataItem as QRC).Obj != null && (e.Node.DataItem as QRC).Obj.Single)
        //{
        //    if ((e.Node.DataItem as QRC).Type == "D")
        //        e.Node.Checkable = true;
        //    else
        //        e.Node.Checkable = false;
        //}
        //else
        //{
        //    if ((e.Node.DataItem as QRC).Type == "F")
        //        e.Node.Checkable = false;
        //    else
        //        e.Node.Checkable = true;
        //}
    }

    protected string P(QRC qrc)
    {
        if (qrc.Obj == null)
            return "";
        if (qrc.Obj.Fixed)
        {
            return db.Value.StorageInSingle.Single(o => o.Id == qrc.Id).Place;
        }
        else if (qrc.Obj.Single)
        {
            return db.Value.StorageInSingle.Single(o => o.Id == qrc.Id).Place;
        }
        else
        {
            return "";
        }
    }

    protected string U(Guid id)
    {
        return db.Value.StorageInSingle.Single(o => o.Id == id).StorageIn.ResponsibleUser == null ? "" : db.Value.StorageInSingle.Single(o => o.Id == id).StorageIn.ResponsibleUser.RealName;
    }

    protected StringBuilder Cut(StringBuilder sb, string content, int count, int blank)
    {
        if (content.Length <= count)
        {
            sb.Append(content);
            sb.Append("\r\n");
        }
        else
        {
            sb.Append(content.Substring(0, count));
            sb.Append("\r\n");
            content = content.Substring(count);
            int total = count - blank;
            for (var j = 0; j < (int)Math.Ceiling(content.Length / (double)total); j++)
            {
                for (var k = 0; k < blank; k++)
                    sb.Append("　");
                sb.Append(content.Substring(j * total).Length >= total ? content.Substring(j * total, total) : content.Substring(j * total));
                sb.Append("\r\n");
            }
        }
        return sb;
    }

    protected void print_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        var source = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList().Join(QRCL, o => o, o => o.Id, (x, y) => y).ToList();
        if (source == null || source.Count == 0)
            return;
        var s = tree.GetAllNodes().Where(o => o.Checked).Select(o => o.Value.GlobalId()).ToList().Join(QRCL, o => o, o => o.Id, (x, y) => y).ToList();
        var 图片宽度 = 600;
        var 图片高度 = 图片宽度 / 2;
        var 边框旁白 = 5;
        var 边框宽度 = 2;
        var 图标上边距 = 边框旁白 + 边框宽度 + 8;
        var 图标左边距 = 边框旁白 + 边框宽度 + 33;
        var 图标宽度 = 60;
        var 图标高度 = 40;
        var 标题字体 = "SimHei";
        var 标题字号 = 20;
        var 标题上边距 = 边框旁白 + 边框宽度 + 17;
        var 标题左边距 = 图标左边距 + 图标宽度 + 16;
        var 右侧宽度 = 200;
        var 左侧宽度 = 图片宽度 - 2 * (边框旁白 + 边框宽度) - 右侧宽度;
        var 二维码边长 = 右侧宽度;
        var 二维码上边距 = 边框旁白 + 边框宽度 + 图标高度 + 7;
        var 二维码左边距 = 图片宽度 - (边框旁白 + 边框宽度) - 右侧宽度;
        var 二维码文字字体 = "Arial";
        var 二维码文字字号 = 16;
        var 二维码文字上边距 = 二维码上边距 + 二维码边长;
        var 二维码文字左边距 = 图片宽度 - (边框旁白 + 边框宽度) - 右侧宽度 + 21;
        var 左侧左边距 = 边框旁白 + 边框宽度 + 12;
        var 左侧上边距 = 边框旁白 + 边框宽度 + 图标高度 + 27;
        var 内容字体 = "SimSun";
        var 内容字号 = 18;
        var 内容每行字数 = 16;
        var 内容空字符数 = 5;
        var W = new SolidBrush(Color.White);
        var B = new SolidBrush(Color.Black);
        var fold_id = db.Value.GlobalId();
        var path = Server.MapPath("~/StorageCode/ToPrint/{0}".Formatted(fold_id));
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        foreach (var x in s)
        {
            SysImage icon = Bitmap.FromFile(Server.MapPath("~/StorageCode/Logo.png"));
            Bitmap image = new Bitmap(图片宽度, 图片高度);
            Graphics g = Graphics.FromImage(image);
            g.FillRectangle(W, 0, 0, 图片宽度, 图片高度);
            g.DrawRectangle(new Pen(B, 边框宽度), 边框旁白, 边框旁白, 图片宽度 - 2 * 边框旁白, 图片高度 - 2 * 边框旁白);
            g.DrawImage(icon, 图标左边距, 图标上边距, 图标宽度, 图标高度);
            string title = "乐翼教育云物资 {0}".Formatted(x.Obj.Single ? (x.Obj.Fixed ? "固定资产标签" : "准固定资产标签") : "物资标签");
            g.DrawString(title, new Font(标题字体, 标题字号), B, 标题左边距, 标题上边距);
            g.Save();
            string qrcode = ConvertCode(x.Type, x.AutoId);
            RadBarcode code = new RadBarcode { Type = BarcodeType.QRCode, Text = qrcode, OutputType = BarcodeOutputType.EmbeddedPNG };
            code.QRCodeSettings.Mode = Telerik.Web.UI.Barcode.Modes.CodeMode.Alphanumeric;
            code.QRCodeSettings.ErrorCorrectionLevel = Telerik.Web.UI.Barcode.Modes.ErrorCorrectionLevel.M;
            code.QRCodeSettings.ECI = Telerik.Web.UI.Barcode.Modes.ECIMode.None;
            code.QRCodeSettings.AutoIncreaseVersion = true;
            code.QRCodeSettings.Version = 0;
            code.QRCodeSettings.DotSize = 5;
            SysImage qr = code.GetImage();
            g.DrawImage(qr, 二维码左边距, 二维码上边距, 二维码边长, 二维码边长);
            g.DrawString(qrcode, new Font(二维码文字字体, 二维码文字字号), B, 二维码文字左边距, 二维码文字上边距);

            var content = "";
            var sb = new StringBuilder();
            content = "物资名称：{0}".Formatted(x.Name);
            Cut(sb, content, 内容每行字数, 内容空字符数);
            content = "单位规格：{0} {1}".Formatted(x.Obj.Unit, x.Obj.Specification);
            Cut(sb, content, 内容每行字数, 内容空字符数);
            if (x.Obj.Single)
            {
                if (x.Obj.Fixed)
                {
                    content = "物资编号：{0}".Formatted(x.Fixed);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                }
                content = "存放地　：{0}".Formatted(P(x));
                Cut(sb, content, 内容每行字数, 内容空字符数);
                content = "责任人　：{0}".Formatted(U(x.Id));
                Cut(sb, content, 内容每行字数, 内容空字符数);
            }
            else
            {
                content = "物资分类：{0}".Formatted(x.Obj.GeneratePath());
                Cut(sb, content, 内容每行字数, 内容空字符数);
            }

            g.DrawString(sb.ToString(), new Font(内容字体, 内容字号), B, 左侧左边距, 左侧上边距);

            image.Save("{0}/{1}.png".Formatted(path, qrcode), ImageFormat.Png);
            g.Dispose();
            image.Dispose();
        }
        (new FastZip()).CreateZip(Server.MapPath("~/StorageCode/ToPrint/{0}.zip".Formatted(fold_id)), Server.MapPath("~/StorageCode/ToPrint/{0}".Formatted(fold_id)), false, ".png");
        //Session["ToPrint"] = s;
        //Session["ToPrintCount"] = s.Count;
        ap.ResponseScripts.Add("window.open('{0}', '_blank');".Formatted("./ToPrint/{0}.zip".Formatted(fold_id)));
    }

    protected void rb_SelectedIndexChanged(object sender, EventArgs e)
    {
        view.Rebind();
    }
}
