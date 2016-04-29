using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace Platform.JHMobile.Models
{
    public class OfficeController : Controller
    {
        public C6Entities DB = new C6Entities();

        private Rijndael aes;

        public void CreateKey(byte[] key, byte[] iv)
        {
            aes = Rijndael.Create();
            aes.Key = key;
            aes.IV = iv;
        }

        public byte[] Decrypt(byte[] cipherText)
        {
            MemoryStream memoryStream = new MemoryStream(cipherText, 0, cipherText.Length);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
            int length = (int)cipherText.Length;
            byte[] numArray = new byte[length];
            int num = cryptoStream.Read(numArray, 0, length);
            byte[] result = new byte[num];
            for (int i = 0; i < num; i++)
            {
                result[i] = numArray[i];
            }
            return result;
        }

        public static string Account
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["OfficeUserAccount"] == null)
                {
                    if (System.Web.HttpContext.Current.Request.Cookies["OfficeUserAccount"] != null)
                    {
                        System.Web.HttpContext.Current.Session["OfficeUserAccount"] = System.Web.HttpContext.Current.Request.Cookies["OfficeUserAccount"].Value;
                    }
                    else
                    {
                        return null;
                    }
                }
                return System.Web.HttpContext.Current.Session["OfficeUserAccount"].ToString();
            }
            set
            {
                System.Web.HttpContext.Current.Request.Cookies.Add(new System.Web.HttpCookie("OfficeUserAccount") { Value = value, Expires = DateTime.MaxValue });
                System.Web.HttpContext.Current.Session["OfficeUserAccount"] = value;
            }
        }

        protected ActionResult 认证()
        {
            return RedirectToAction("认证", "微信");
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

        protected bool DecryptFile(string path, string output)
        {
            try
            {
                CreateKey(Encoding.Default.GetBytes("12345678123456781234567812345678"), Encoding.Default.GetBytes("1234567812345678"));
                if (!System.IO.File.Exists(path))
                    return false;
                var stream = System.IO.File.OpenRead(path);
                var length = stream.Length;
                byte[] bytes = new byte[Convert.ToInt32(length.ToString())];
                stream.Read(bytes, 0, (int)bytes.Length);
                stream.Close();
                bytes = Decrypt(bytes);
                var stream_x = new FileStream(output, FileMode.Create, FileAccess.Write);
                stream_x.Write(bytes, 0, (int)bytes.Length);
                stream_x.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string Cut(string text, int total, string suffix = "...")
        {
            var sb = new StringBuilder();
            var x = text.Length;
            var i = 0;
            bool e = false;
            bool n = false;
            while (i < x)
            {
                if (n)
                {
                    e = false;
                    n = false;
                }
                if (text[i] == '<')
                {
                    e = true;
                }
                else if (text[i] == '>')
                {
                    n = true;
                }
                if (!e)
                    sb.Append(text[i]);
                i++;
            }
            var value = sb.ToString();
            if (value.Length <= total)
                return value;
            else
                return value.Substring(0, total) + suffix;
        }
    }
}
