using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Homory.Model
{
	public class HomoryCryptor
	{
		public static string Encrypt(string input, out string key, out string salt)
		{
			var aes = new AesManaged();
			key = "7U7x0keu+d5EbThVQZzgFlfdVelKNmqml2RRmSi3Y/4=";
			salt = "l46OWQ3WRn4RBpAQpUZhDg==";
			aes.Key = Convert.FromBase64String(key);
			aes.IV = Convert.FromBase64String(salt);
			var source = Encoding.UTF8.GetBytes(input);
			var transform = aes.CreateEncryptor();
			var stream = new MemoryStream();
			var cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(source, 0, source.Length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			var output = Convert.ToBase64String(stream.ToArray());
			stream.Close();
			return output;
		}

		public static string Decrypt(string input, string key, string salt)
		{
			var aes = new AesManaged { Key = Convert.FromBase64String(key), IV = Convert.FromBase64String(salt) };
			var source = Convert.FromBase64String(input);
			var transform = aes.CreateDecryptor();
			var stream = new MemoryStream();
			var cryptoStream = new CryptoStream(stream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(source, 0, source.Length);
			cryptoStream.FlushFinalBlock();
			cryptoStream.Close();
			var output = Encoding.UTF8.GetString(stream.ToArray());
			return output;
		}
	}
}
