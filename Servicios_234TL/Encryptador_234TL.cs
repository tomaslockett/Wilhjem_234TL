using System.Security.Cryptography;
using System.Text;

namespace Servicios_234TL
{
    public static class Encryptador_234TL
    {
        public static string MD5Encryptar_234TL(string valor)
        {
            var md5 = new MD5CryptoServiceProvider();
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(valor));
            return (new ASCIIEncoding()).GetString(md5Data);
        }

        public static string SHA256Encrpytar_234TL(string valor)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytesTexto = Encoding.UTF8.GetBytes(valor);
                byte[] hash = sha256.ComputeHash(bytesTexto);

                StringBuilder texto = new StringBuilder();
                foreach (var b in hash)
                {
                    texto.Append(b.ToString("x2"));
                }
                return texto.ToString();
            }
        }

        //public static string Encrypt_234TL(string plainText)
        //{
        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.GenerateIV();
        //        aes.GenerateKey();
        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        //        using (var ms = new System.IO.MemoryStream())
        //        {
        //            ms.Write(aes.IV, 0, aes.IV.Length);
        //            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (var sw = new System.IO.StreamWriter(cs))
        //                {
        //                    sw.Write(plainText);
        //                }
        //            }
        //            return Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //}
        //public static string Decrypt_234TL(string cipherText)
        //{
        //    byte[] fullCipher = Convert.FromBase64String(cipherText);
        //    byte[] iv = new byte[16];
        //    Array.Copy(fullCipher, iv, iv.Length);
        //    using (Aes aes = Aes.Create())
        //    {
        //        aes.Key = new byte[32]; // Use the same key used for encryption
        //        aes.IV = iv;
        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        //        using (var ms = new System.IO.MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
        //        {
        //            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (var sr = new System.IO.StreamReader(cs))
        //                {
        //                    return sr.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}