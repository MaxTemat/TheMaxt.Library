using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TheMaxt.Cryptography
{
    /// <summary>
    /// This class allows data to be hashed using the AES algorithm
    /// </summary>
    public static class AES
    {
        /// <summary>
        /// This method allows to encrypt a string according to the AES algorithm
        /// </summary>
        /// <param name="Text">The string to hash</param>
        /// <param name="Pkey">public key for AES encryption.
        /// It must contain only 8 characters</param>
        /// <param name="Skey">secret key for AES encryption.
        /// It must contain only 8 characters</param>
        /// <returns>Returns the encrypted string</returns>
        public static string Encrypt(string Text, string Pkey = "12345678", string Skey = "12345678")
        {
            byte[] skeyBytes = Encoding.UTF8.GetBytes(Skey);
            byte[] pkeyBytes = Encoding.UTF8.GetBytes(Pkey);
            byte[] textBytes = Encoding.UTF8.GetBytes(Text);
            string s;
            using (DESCryptoServiceProvider Dtsp = new DESCryptoServiceProvider())
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, Dtsp.CreateEncryptor(pkeyBytes, skeyBytes), CryptoStreamMode.Write);
                cs.Write(textBytes, 0, textBytes.Length);
                cs.FlushFinalBlock();
                s = Convert.ToBase64String(ms.ToArray());
            }
            return s;
        }

        /// <summary>
        /// This method allows to decrypt a string according to the AES algorithm
        /// </summary>
        /// <param name="Text">The string to hash</param>
        /// <param name="Pkey">public key for AES encryption.
        /// It must contain only 8 characters</param>
        /// <param name="Skey">secret key for AES encryption.
        /// It must contain only 8 characters</param>
        /// <returns>Returns the decrypted string</returns>
        public static string Decrypt(string Text, string Pkey = "12345678", string Skey = "12345678")
        {
            byte[] skeyBytes = Encoding.UTF8.GetBytes(Skey);
            byte[] pkeyBytes = Encoding.UTF8.GetBytes(Pkey);
            byte[] textBytes = new byte[Text.Replace(" ", "+").Length];
            textBytes = Convert.FromBase64String(Text.Replace(" ", "+"));
            string s;
            using (DESCryptoServiceProvider Dtsp = new DESCryptoServiceProvider())
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, Dtsp.CreateDecryptor(pkeyBytes, skeyBytes), CryptoStreamMode.Write);
                cs.Write(textBytes, 0, textBytes.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                s = encoding.GetString(ms.ToArray());
            }
            return s;
        }
    }
}
