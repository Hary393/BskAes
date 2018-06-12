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
        public static RSAParameters StringToKey(string key)
        {
            //get a stream from the string
            var sr = new System.IO.StringReader(key);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            var pubKey = (RSAParameters)xs.Deserialize(sr);
            return pubKey;
        }
        public static string EncryptMessage(string publicKey, string toEncrypt)
        {
            var key = StringToKey(publicKey);
            //we have a public key ... let's get a new csp and load that key
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(key);

            //for encryption, always handle bytes...
            var bytesPlainTextData = System.Text.Encoding.ASCII.GetBytes(toEncrypt);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCypherText = csp.Encrypt(bytesPlainTextData, false);

            //we might want a string representation of our cypher text
            var cypherText = System.Text.Encoding.ASCII.GetString(bytesCypherText);
            return cypherText;
        }
        public static string DecryptMessage(string privateKey, string toDecrypt)
        {

            var key = StringToKey(privateKey);

            var bytesCypherText = System.Text.Encoding.ASCII.GetBytes(toDecrypt);

            //we want to decrypt, therefore we need a csp and load our private key
            var csp = new RSACryptoServiceProvider();
            csp.ImportParameters(key);

            //decrypt and strip pkcs#1.5 padding
            var bytesPlainTextData = csp.Decrypt(bytesCypherText, false);

            //get our original plainText back...
            var plainTextData = System.Text.Encoding.ASCII.GetString(bytesPlainTextData);
            return plainTextData;
        }

    }
}
