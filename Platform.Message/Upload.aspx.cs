using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Upload;
using Telerik.Web.UI;
using System.Security.Cryptography;
using System.Text;
using System.IO;

public partial class Upload : System.Web.UI.Page
{
    protected Lazy<MEntities> db = new Lazy<MEntities>(() => new MEntities());

    private Rijndael aes;

    public void CreateKey(byte[] key, byte[] iv)
    {
        aes = Rijndael.Create();
        aes.Key = key;
        aes.IV = iv;
    }

    public byte[] Encrypt(byte[] cipherText)
    {
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(cipherText, 0, (int)cipherText.Length);
        cryptoStream.FlushFinalBlock();
        memoryStream.Close();
        return memoryStream.ToArray();
    }

    protected bool EncryptFile(string path, string output)
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
            bytes = Encrypt(bytes);
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

    protected Dictionary<int, string> Attachments
    {
        get
        {
            if (Session["MA"] == null)
            {
                Session["MA"] = new Dictionary<int, string>();
            }
            return (Dictionary<int, string>)Session["MA"];
        }
        set
        {
            Session["MA"] = value;
        }
    }

    protected void uploaded(object sender, FileUploadedEventArgs e)
    {
        var gid = Guid.NewGuid().ToString().ToLower();
        var ext = e.File.GetExtension();
        if (ext.IndexOf('.') == -1)
        {
            ext = "." + ext;
        }
        e.File.SaveAs(Server.MapPath("Resource/Slaves/Homory/" + gid + ext));
        EncryptFile(Server.MapPath("Resource/Slaves/Homory/" + gid + ext), Server.MapPath("Resource/Slaves/Homory/__" + gid + ext));
        var id = db.Value.M_寻呼_文件("../Resource/Slaves/Homory/__" + gid + ext, e.UploadResult.FileName, e.UploadResult.ContentType, (int)e.UploadResult.ContentLength);
        var a = Attachments;
        a.Add(id.Single().Value, e.UploadResult.FileName);
        Attachments = a;
        grid.DataSource = Attachments.Select(o => new Obj { K = o.Key, V = o.Value }).ToList();
        grid.DataBind();
        ap.ResponseScripts.Add("this.parent.rea(" + a.Count + ");");
    }

    protected void need(object sender, GridNeedDataSourceEventArgs e)
    {
        grid.DataSource = Attachments.Select(o => new Obj { K = o.Key, V = o.Value }).ToList();
        ap.ResponseScripts.Add("this.parent.rea(" + Attachments.Count + ");");
    }

    protected void del(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            var k = int.Parse((e.Item as GridEditableItem).GetDataKeyValue("K").ToString());
            var a = Attachments;
            a.Remove(k);
            Attachments = a;
        }
        grid.Rebind();
    }

    public class Obj
    {
        public int K { get; set; }
        public string V { get; set; }
    }
}
