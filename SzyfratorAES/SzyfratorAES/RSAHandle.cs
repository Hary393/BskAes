using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SzyfratorAES
{
    class RSAHandle
    {
        public static void EncryptPrivate(string thingToEncrypt,string keyPassword,string whereToSave)
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Padding = PaddingMode.PKCS7;
                var key = new Rfc2898DeriveBytes(keyPassword, saltBytes, 10000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.ECB;

                FileStream fsCrypt = new FileStream(whereToSave, FileMode.Create);
                CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(thingToEncrypt);
                cs.Write(passwordBytes, 0, passwordBytes.Length);
                cs.Close();
                fsCrypt.Close();
            }
        }

        public static string DecryptPrivate( string keyPassword, string whereToRead)
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                AES.KeySize = 256;
                AES.BlockSize = 128;
                AES.Padding = PaddingMode.PKCS7;
                var key = new Rfc2898DeriveBytes(keyPassword, saltBytes, 10000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = key.GetBytes(AES.BlockSize / 8);
                AES.Mode = CipherMode.ECB;
                byte[] passwordBytes = File.ReadAllBytes(whereToRead);
    
                FileStream fsCrypt = new FileStream(whereToRead, FileMode.Open);
                CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

                int read;
                byte[] buffer = new byte[5000];

                read = cs.Read(buffer, 0, buffer.Length);
                byte[] privateKeyByte = new byte[read];
                for (int i = 0; i < read; i++)
                {
                    privateKeyByte[i] = buffer[i];
                }
                string result = System.Text.Encoding.ASCII.GetString(privateKeyByte);
                cs.Close();
                fsCrypt.Close();
                return result;
            }
        }
    }
}
