using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Platform.JHMobile.Models
{
    public class AES
    {
        private byte[] Iv = null;

        private byte[] key = null;

        private Rijndael rij = null;

        public byte[] IV
        {
            get
            {
                return this.Iv;
            }
        }

        public byte[] Key
        {
            get
            {
                return this.key;
            }
        }

        public AES()
        {
        }

        public void CreateKey()
        {
            this.rij = Rijndael.Create();
            this.rij.GenerateIV();
            this.rij.GenerateKey();
            this.Iv = this.rij.IV;
            this.key = this.rij.Key;
        }

        public void CreateKey(byte[] keyInfo, byte[] IVInfo)
        {
            this.Iv = IVInfo;
            this.key = keyInfo;
            this.rij = Rijndael.Create();
            this.rij.IV = IVInfo;
            this.rij.Key = keyInfo;
        }

        public byte[] Decrypt(byte[] cipherText)
        {
            MemoryStream memoryStream = new MemoryStream(cipherText, 0, (int)cipherText.Length);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, this.rij.CreateDecryptor(), CryptoStreamMode.Read);
            int length = (int)cipherText.Length;
            byte[] numArray = new byte[length];
            int num = cryptoStream.Read(numArray, 0, length);
            byte[] numArray1 = new byte[num];
            for (int i = 0; i < num; i++)
            {
                numArray1[i] = numArray[i];
            }
            return numArray1;
        }

        public byte[] Encrypt(byte[] plainText)
        {
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, this.rij.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(plainText, 0, (int)plainText.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Close();
            return memoryStream.ToArray();
        }
    }
}
