using Platform.JHMobile.Models;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Platform.JHMobile.Controllers
{
    public class JinHerController : Controller
    {
        private C6Entities db = new C6Entities();

        private string account;

        protected string Account
        {
            get
            {
                if (string.IsNullOrEmpty(account))
                {
                    if (Session["user_id"] != null)
                    {
                        account = Session["user_id"].ToString();
                    }
                    else if (Request.Cookies["user_id"] != null)
                    {
                        account = Request.Cookies["user_id"].Value;
                        Session["user_id"] = account;
                    }
                }
                return account;
            }
        }

        protected ActionResult Authenticate()
        {
            return RedirectToAction("Authentication", "Ding");
        }

        protected ActionResult Authorize()
        {
            return RedirectToAction("Authorization", "Ding");
        }

        protected string ConvertDoc(string doc)
        {
            if (string.IsNullOrEmpty(doc))
                return null;
            var file = new FileInfo(doc);
            if (!file.Exists)
                return null;
            var suffix = file.Extension.Replace(".", "").ToLower();
            var path = string.Format("{0}.pdf", file.FullName.Substring(0, file.FullName.LastIndexOf('.')));
            var newFile = new FileInfo(path);
            if (newFile.Exists)
                return path;
            switch (suffix)
            {
                case "doc":
                case "docx":
                case "txt":
                case "rtf":
                    var docW = new Aspose.Words.Document(file.FullName);
                    docW.Save(path, Aspose.Words.SaveFormat.Pdf);
                    return path;
                case "ppt":
                case "pptx":
                    var docP = new Aspose.Slides.Presentation(file.FullName);
                    docP.Save(path, Aspose.Slides.Export.SaveFormat.Pdf);
                    return path;
                case "xls":
                case "xlsx":
                    var docE = new Aspose.Cells.Workbook(file.FullName);
                    docE.Save(path, Aspose.Cells.SaveFormat.Pdf);
                    return path;
                default: return null;
            }
        }

        protected void DecryptFile(string path, string output)
        {
            try
            {
                var aes = new JinHerAES();
                aes.CreateKey(Encoding.Default.GetBytes("12345678123456781234567812345678"), Encoding.Default.GetBytes("1234567812345678"));
                if (!System.IO.File.Exists(path))
                    return;
                var stream = System.IO.File.OpenRead(path);
                var length = stream.Length;
                byte[] bytes = new byte[Convert.ToInt32(length.ToString())];
                stream.Read(bytes, 0, (int)bytes.Length);
                stream.Close();
                bytes = aes.Decrypt(bytes);
                var stream_x = new FileStream(output, FileMode.Create, FileAccess.Write);
                stream_x.Write(bytes, 0, (int)bytes.Length);
                stream_x.Close();
            }
            catch
            { }
        }
    }
}
