using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Linq;
using System.Timers;
using Telerik.Web.UI;
using SysImage = System.Drawing.Image;
using System.Drawing.Imaging;

namespace LY.Service.QRCode
{
    public partial class HomoryQRCodeService : ServiceBase
    {
        private Timer _timer;

        public HomoryQRCodeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Log("Service", "Start");
                QR();
                _timer = new Timer(double.Parse(ConfigurationManager.AppSettings["CodeInterval"]) * 1000)
                {
                    AutoReset = true
                };
                _timer.Elapsed += timer_Elapsed;
                _timer.Start();
            }
            catch (Exception exception)
            {
                Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            QR();
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Close();
            Log("Service", "Stop");
        }

        protected void Log(string key, string content)
        {
            var path = string.Format(ConfigurationManager.AppSettings["CodeLog"], DateTime.Today.ToString("yyyyMMdd"));
            var line = string.Format("Time: {0}; Key: {1}; Content: {2}.{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), key,
                content, Environment.NewLine);
            File.AppendAllText(path, line);
        }

        public class DepotCode
        {
            public Guid DepotId { get; set; }
            public Guid BatchId { get; set; }
            public int BatchOrdinal { get; set; }
            public string CodeJson { get; set; }
            public DateTime Time { get; set; }
            public int State { get; set; }
#if xsfx
            public string XsfxName { get; set; }
#endif
        }

        private void QR()
        {
            try
            {
                try
                {
                    var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
#if xsfx
                    var com = new System.Data.SqlClient.SqlCommand("SELECT DepotCode.DepotId, DepotCode.BatchId, DepotCode.BatchOrdinial, DepotCode.CodeJson, DepotCode.Time, DepotCode.[State], Depot.Name Xsfx FROM DepotCode INNER JOIN Depot ON DepotCode.DepotId = Depot.Id AND DepotCode.[State] = 2", con);
#else
                    var com = new System.Data.SqlClient.SqlCommand("SELECT DepotId, BatchId, BatchOrdinial, CodeJson, Time, [State] FROM DepotCode WHERE [State] = 2", con);
#endif
                    var list = new List<DepotCode>();
                    con.Open();
                    var reader = com.ExecuteReader();
                    while (reader.Read())
                    {
#if xsfx
                        list.Add(
                            new DepotCode
                            {
                                DepotId = Guid.Parse(reader[0].ToString()),
                                BatchId = Guid.Parse(reader[1].ToString()),
                                BatchOrdinal = int.Parse(reader[2].ToString()),
                                CodeJson = reader[3].ToString(),
                                Time = DateTime.Parse(reader[4].ToString()),
                                State = int.Parse(reader[5].ToString()),
                                XsfxName = reader[6].ToString()
                            });
#else
                        list.Add(
                            new DepotCode
                            {
                                DepotId = Guid.Parse(reader[0].ToString()),
                                BatchId = Guid.Parse(reader[1].ToString()),
                                BatchOrdinal = int.Parse(reader[2].ToString()),
                                CodeJson = reader[3].ToString(),
                                Time = DateTime.Parse(reader[4].ToString()),
                                State = int.Parse(reader[5].ToString())
                            });
#endif
                    }
                    try
                    {
                        reader.Close();
                    }
                    catch
                    {
                    }
                    try
                    {
                        con.Close();
                    }
                    catch
                    {
                    }
                    Log("ToQRCount", list.Count.ToString());
                    QRS(list);
                    con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
                    con.Open();
                    foreach (var @do in list)
                    {
                        com = new System.Data.SqlClient.SqlCommand("UPDATE DepotCode SET [State] = 1 WHERE BatchId = '{0}'".Formatted(@do.BatchId), con);
                        com.ExecuteNonQuery();
                    }
                    try
                    {
                        con.Close();
                    }
                    catch
                    {
                    }
                }
                catch (Exception ex)
                {
                    Log("Error", ex.StackTrace.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception exception)
            {
                Log("Error", exception.StackTrace.ToString(CultureInfo.InvariantCulture));
            }
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

        private void QRS(List<DepotCode> list)
        {
#region 图片参数
            var 图片宽度 = 600;
            var 图片高度 = 图片宽度 / 2;
            var 边框旁白 = 15;
            var 边框宽度 = 2;
            var 图标上边距 = 边框旁白 + 边框宽度 + 8;
            var 图标左边距 = 边框旁白 + 边框宽度 + 33;
#if yz
            var 图标宽度 = 400;
#else
            var 图标宽度 = 60;
#endif
            var 图标高度 = 40;
            var 标题字体 = "SimHei";
            var 标题字号 = 20;
            var 标题上边距 = 边框旁白 + 边框宽度 + 17;
            var 标题左边距 = 图标左边距 + 图标宽度 + 16;
            var 右侧宽度 = 200;
            var 左侧宽度 = 图片宽度 - 2 * (边框旁白 + 边框宽度) - 右侧宽度;
            var 二维码边长 = 右侧宽度;
#if xsfx
            var 二维码上边距 = 边框旁白 + 边框宽度 + 图标高度 + 7 + 20;
#else
            var 二维码上边距 = 边框旁白 + 边框宽度 + 图标高度 + 7;
#endif
            var 二维码左边距 = 图片宽度 - (边框旁白 + 边框宽度) - 右侧宽度;
            var 二维码文字字体 = "Arial";
            var 二维码文字字号 = 15;
            var 二维码文字上边距 = 二维码上边距 + 二维码边长 - 15;
            var 二维码文字左边距 = 图片宽度 - (边框旁白 + 边框宽度) - 右侧宽度 + 21;
            var 左侧左边距 = 边框旁白 + 边框宽度 + 12;
#if xsfx
            var 左侧上边距 = 边框旁白 + 边框宽度 + 图标高度 + 27-10;
#else
            var 左侧上边距 = 边框旁白 + 边框宽度 + 图标高度 + 27;
#endif
            var 内容字体 = "SimHei";
            var 内容字号 = 18;
            var 内容每行字数 = 15;
            var 内容空字符数 = 5;
            var W = new SolidBrush(Color.White);
            var B = new SolidBrush(Color.Black);
            var BasePath = ConfigurationManager.AppSettings["CodePath"];
            var Logo = ConfigurationManager.AppSettings["CodeLogo"];
            var Title = ConfigurationManager.AppSettings["CodeTitle"];
#endregion
            foreach (var group in list.GroupBy(o => o.BatchId))
            {
                var fold_id = group.Key;
                var path = string.Format("{0}\\临时\\{1}", BasePath, fold_id);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                var codes = group.SelectMany(o => o.CodeJson.FromJson<List<string>>()).ToList();
                foreach (var qrcode in codes)
                {
                    SysImage icon = Bitmap.FromFile(Logo);
                    Bitmap image = new Bitmap(图片宽度, 图片高度);
                    Graphics g = Graphics.FromImage(image);
                    g.FillRectangle(W, 0, 0, 图片宽度, 图片高度);
                    g.DrawRectangle(new Pen(B, 边框宽度), 边框旁白, 边框旁白, 图片宽度 - 2 * 边框旁白, 图片高度 - 2 * 边框旁白);
                    g.DrawImage(icon, 图标左边距, 图标上边距, 图标宽度, 图标高度);
#if xsfx
                    string title = "{0}".Formatted(Title);
                    g.DrawString(title, new Font(标题字体, 标题字号), B, 标题左边距, 标题上边距);
                    g.Save();
                    RadBarcode code = new RadBarcode { Type = BarcodeType.QRCode, Text = qrcode, OutputType = BarcodeOutputType.EmbeddedPNG };
                    code.QRCodeSettings.Mode = Telerik.Web.UI.Barcode.Modes.CodeMode.Alphanumeric;
                    code.QRCodeSettings.ErrorCorrectionLevel = Telerik.Web.UI.Barcode.Modes.ErrorCorrectionLevel.M;
                    code.QRCodeSettings.ECI = Telerik.Web.UI.Barcode.Modes.ECIMode.None;
                    code.QRCodeSettings.AutoIncreaseVersion = true;
                    code.QRCodeSettings.Version = 0;
                    code.QRCodeSettings.DotSize = 5;
                    SysImage qr = code.GetImage();
                    g.DrawImage(qr, 二维码左边距, 二维码上边距, 二维码边长, 二维码边长);
                    //g.DrawString(qrcode, new Font(二维码文字字体, 二维码文字字号), B, 二维码文字左边距, 二维码文字上边距);
                    var content = "";
                    var sb = new StringBuilder();
                    var info = string.Empty;

                    try
                    {
                        var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
                        var com = new System.Data.SqlClient.SqlCommand("EXEC [dbo].[GetQRInfo] @QR = N'{0}'".Formatted(qrcode), con);
                        con.Open();
                        var reader = com.ExecuteReader();
                        if (reader.Read())
                        {
                            info = reader.GetString(0);
                        }
                        try
                        {
                            reader.Close();
                        }
                        catch
                        {
                        }
                        try
                        {
                            con.Close();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    var infos = info.Split(new string[] { "@@@" }, StringSplitOptions.None);
                    content = "　　　　　　　固定资产{0}".Formatted(list[0].XsfxName.Substring(list[0].XsfxName.IndexOf('(')));
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    content = " ";
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    content = "类别：{0}".Formatted(infos[5].Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    content = "名称：{0}".Formatted(infos[0]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    if (infos[3] == "1")
                    {
                        //content = "资产编号：{0}".Formatted(infos[4].Length > 7 ? infos[4].Substring(infos[4].Length - 7) : infos[4]);
                        //Cut(sb, content, 内容每行字数, 内容空字符数);
                        var time = infos[7].None() ? "" : DateTime.Parse(infos[7]).ToString("yyyy-MM-dd");
                        content = "购置日期：{0}".Formatted(time);
                        Cut(sb, content, 内容每行字数, 内容空字符数);
                    }
                    content = "条码：{0}".Formatted(qrcode);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    g.DrawString(sb.ToString(), new Font(内容字体, 内容字号), B, 左侧左边距, 左侧上边距);
#elif hb
                    string title = "{0} 资产标签".Formatted(Title);
                    g.DrawString(title, new Font(标题字体, 标题字号), B, 标题左边距, 标题上边距);
                    g.Save();
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
                    var info = string.Empty;

                    try
                    {
                        var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
                        var com = new System.Data.SqlClient.SqlCommand("EXEC [dbo].[GetQRInfo] @QR = N'{0}'".Formatted(qrcode), con);
                        con.Open();
                        var reader = com.ExecuteReader();
                        if (reader.Read())
                        {
                            info = reader.GetString(0);
                        }
                        try
                        {
                            reader.Close();
                        }
                        catch
                        {
                        }
                        try
                        {
                            con.Close();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    var infos = info.Split(new string[] { "@@@" }, StringSplitOptions.None);
                    content = "资产名称：{0}".Formatted(infos[0]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    content = "规格型号：{0}{1}{2}".Formatted(infos[1], infos[1].None() ? "" : " ", infos[2]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    //if (infos[3] == "1")
                    //{
                        content = "资产编号：{0}".Formatted(infos[4].Length > 7 ? infos[4].Substring(infos[4].Length - 7) : infos[4]);
                        Cut(sb, content, 内容每行字数, 内容空字符数);
                        content = "保管部门：{0}".Formatted(infos[8].None() ? "" : infos[8]);
                        Cut(sb, content, 内容每行字数, 内容空字符数);
                        //var time = infos[7].None() ? "" : DateTime.Parse(infos[7]).ToString("yyyy-MM-dd");
                        //content = "购置日期：{0}".Formatted(time);
                        //Cut(sb, content, 内容每行字数, 内容空字符数);
                    //}
                    //content = "物资分类：{0}".Formatted(infos[5]);
                    //Cut(sb, content, 内容每行字数, 内容空字符数);
                    g.DrawString(sb.ToString(), new Font(内容字体, 内容字号), B, 左侧左边距, 左侧上边距);
#elif yz
                    //string title = "{0} 资产标签".Formatted(Title);
                    //g.DrawString(title, new Font(标题字体, 标题字号), B, 标题左边距, 标题上边距);
                    //g.Save();
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
                    var info = string.Empty;

                    try
                    {
                        var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
                        var com = new System.Data.SqlClient.SqlCommand("EXEC [dbo].[GetQRInfo] @QR = N'{0}'".Formatted(qrcode), con);
                        con.Open();
                        var reader = com.ExecuteReader();
                        if (reader.Read())
                        {
                            info = reader.GetString(0);
                        }
                        try
                        {
                            reader.Close();
                        }
                        catch
                        {
                        }
                        try
                        {
                            con.Close();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    var infos = info.Split(new string[] { "@@@" }, StringSplitOptions.None);
                    content = "资产名称：{0}".Formatted(infos[0]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    content = "规格型号：{0}{1}{2}".Formatted(infos[1], infos[1].None() ? "" : " ", infos[2]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    //if (infos[3] == "1")
                    //{
                        content = "资产编号：{0}".Formatted(infos[4].Length > 7 ? infos[4].Substring(infos[4].Length - 7) : infos[4]);
                        Cut(sb, content, 内容每行字数, 内容空字符数);
                        content = "保管部门：{0}".Formatted(infos[8].None() ? "" : infos[8]);
                        Cut(sb, content, 内容每行字数, 内容空字符数);
                        //var time = infos[7].None() ? "" : DateTime.Parse(infos[7]).ToString("yyyy-MM-dd");
                        //content = "购置日期：{0}".Formatted(time);
                        //Cut(sb, content, 内容每行字数, 内容空字符数);
                    //}
                    //content = "物资分类：{0}".Formatted(infos[5]);
                    //Cut(sb, content, 内容每行字数, 内容空字符数);
                    g.DrawString(sb.ToString(), new Font(内容字体, 内容字号), B, 左侧左边距, 左侧上边距);
#else
                    string title = "{0} 资产标签".Formatted(Title);
                    g.DrawString(title, new Font(标题字体, 标题字号), B, 标题左边距, 标题上边距);
                    g.Save();
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
                    var info = string.Empty;

                    try
                    {
                        var con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
                        var com = new System.Data.SqlClient.SqlCommand("EXEC [dbo].[GetQRInfo] @QR = N'{0}'".Formatted(qrcode), con);
                        con.Open();
                        var reader = com.ExecuteReader();
                        if (reader.Read())
                        {
                            info = reader.GetString(0);
                        }
                        try
                        {
                            reader.Close();
                        }
                        catch
                        {
                        }
                        try
                        {
                            con.Close();
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        continue;
                    }

                    var infos = info.Split(new string[] { "@@@" }, StringSplitOptions.None);
                    content = "资产名称：{0}".Formatted(infos[0]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    content = "规格型号：{0}{1}{2}".Formatted(infos[1], infos[1].None() ? "" : " ", infos[2]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    if (infos[3] == "1")
                    {
                        content = "资产编号：{0}".Formatted(infos[4].Length > 7 ? infos[4].Substring(infos[4].Length - 7) : infos[4]);
                        Cut(sb, content, 内容每行字数, 内容空字符数);
                        var time = infos[7].None() ? "" : DateTime.Parse(infos[7]).ToString("yyyy-MM-dd");
                        content = "购置日期：{0}".Formatted(time);
                        Cut(sb, content, 内容每行字数, 内容空字符数);
                    }
                    content = "物资分类：{0}".Formatted(infos[5]);
                    Cut(sb, content, 内容每行字数, 内容空字符数);
                    g.DrawString(sb.ToString(), new Font(内容字体, 内容字号), B, 左侧左边距, 左侧上边距);
#endif
                    //content = "存放地　：{0}".Formatted("教室A");
                    //Cut(sb, content, 内容每行字数, 内容空字符数);
                    //content = "责任人　：{0}".Formatted("凌俊伟");
                    //Cut(sb, content, 内容每行字数, 内容空字符数);
                    image.Save("{0}/{1}.png".Formatted(path, qrcode), ImageFormat.Png);
                    g.Dispose();
                    image.Dispose();
                }
        (new FastZip()).CreateZip(string.Format("{0}\\打包\\{1}.zip".Formatted(BasePath, fold_id)), string.Format("{0}\\临时\\{1}", BasePath, fold_id), false, ".png");
            }
        }
    }
}
