using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows;

namespace SzyfratorAES
{
    class CipherMashine
    {
        public byte[] AES_Encrypt(string originFile,string whereToSave,string password,string mode, string keySize,List<string> selectedUsers)
        {
            //wektor poczatkowy
            string IVString=GetUniqueKey(16);
            byte[] IV = System.Text.Encoding.ASCII.GetBytes(IVString);
            //generowanie soli
            byte[] salt = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
                //rng.GetBytes(IV);
            }

            string header=HeaderToString(password,  mode, keySize, selectedUsers, IVString);

            FileStream fsCrypt = new FileStream(whereToSave, FileMode.Create);
            


            byte[] passwordBytes = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] stringAsBytes = new byte[9000];
            //Przekonwertuj naglowek na byte
            stringAsBytes = Encoding.ASCII.GetBytes(header);
            //oblicz rozmiar naglowka i dodaj go na poczatku pliku z wiadocymi zerami
            string result = stringAsBytes.Length.ToString().PadLeft(4, '0');
            //wpisz nagłówek 
            byte[] stringlenghtAsBytes = new byte[4];
            stringlenghtAsBytes = Encoding.ASCII.GetBytes(result);
            fsCrypt.Write(stringlenghtAsBytes, 0, stringlenghtAsBytes.Length);
            fsCrypt.Write(stringAsBytes, 0, stringAsBytes.Length);



            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            //wpisz sol na poczatek pliku
            fsCrypt.Write(salt, 0, salt.Length);
            byte[] encryptedBytes = null;
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                if (keySize.Contains("128"))
                {
                    AES.KeySize = 128;
                }
                if (keySize.Contains("192"))
                {
                    AES.KeySize = 192;
                }
                if (keySize.Contains("256"))
                {
                    AES.KeySize = 256;
                }
                AES.BlockSize = 128;
                AES.Padding = PaddingMode.PKCS7;

                var key = new Rfc2898DeriveBytes(passwordBytes, salt, 10000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = IV;


                if (mode.Contains("CBC"))
                {
                    AES.Mode = CipherMode.CBC;
                }
                if (mode.Contains("ECB"))
                {
                    AES.Mode = CipherMode.ECB;
                }
                if (mode.Contains("CFB"))
                {
                    AES.Mode = CipherMode.CFB;
                }
                if (mode.Contains("OFB"))
                {
                    AES.Mode = CipherMode.OFB;
                }

                CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);
                FileStream fsIn = new FileStream(originFile, FileMode.Open);

                //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
                byte[] buffer = new byte[1048576];
                int read;

                try
                {
                    while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        //Application.DoEvents(); // -> for responsive GUI, using Task will be better!
                        cs.Write(buffer, 0, read);
                    }

                    //close up
                    fsIn.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Szyfracja nie wyszła "+ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                finally
                {
                    cs.Close();
                    fsCrypt.Close();
                }
            }
            

            return encryptedBytes;
        }
        public byte[] AES_Decrypt(string originFile, string whereToSave, string password)
        {
            
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            
            FileStream fsCrypt = new FileStream(originFile, FileMode.Open);
            
            //odczytaj rozmiar naglowka
            byte[] stringLenghtAsBytes = new byte[4];
            fsCrypt.Read(stringLenghtAsBytes, 0, stringLenghtAsBytes.Length);
            string result = System.Text.Encoding.ASCII.GetString(stringLenghtAsBytes);
            int paresdResult = Int32.Parse(result);
            //odczytaj naglowek
            byte[] stringAsBytes = new byte[paresdResult];
            fsCrypt.Read(stringAsBytes, 0, stringAsBytes.Length);
            string header = System.Text.Encoding.ASCII.GetString(stringAsBytes);

            string[] headerArray;
            headerArray = header.Split('|');
            //na podstawie pozycji w nagłówku uzupełnij pola 
            string keySize = headerArray[4];
            string mode = headerArray[8];

            string IVString = headerArray[10];
            byte[] IV = System.Text.Encoding.ASCII.GetBytes(IVString);
            //11 ApprovedUsers 12User 13username 14SessionKey 15paswd

            byte[] decryptedBytes = null;
            //odczytaj sol
            byte[] salt = new byte[32];
            fsCrypt.Read(salt, 0, salt.Length);
            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.

            using (RijndaelManaged AES = new RijndaelManaged())
            {
                if (keySize.Contains("128"))
                {
                    AES.KeySize = 128;
                }
                if (keySize.Contains("192"))
                {
                    AES.KeySize = 192;
                }
                if (keySize.Contains("256"))
                {
                    AES.KeySize = 256;
                }
                AES.BlockSize = 128;
                AES.Padding = PaddingMode.PKCS7;

                var key = new Rfc2898DeriveBytes(passwordBytes, salt, 10000);
                AES.Key = key.GetBytes(AES.KeySize / 8);
                AES.IV = IV;
                if (mode.Contains("CBC"))
                {
                    AES.Mode = CipherMode.CBC;
                }
                if (mode.Contains("ECB"))
                {
                    AES.Mode = CipherMode.ECB;
                }
                if (mode.Contains("CFB"))
                {
                    AES.Mode = CipherMode.CFB;
                }
                if (mode.Contains("OFB"))
                {
                    AES.Mode = CipherMode.OFB;
                }
                

                CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(whereToSave, FileMode.Create);

                int read;
                byte[] buffer = new byte[1048576];
                try
                {
                    while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        //Application.DoEvents();
                        fsOut.Write(buffer, 0, read);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Szyfracja nie wyszła "+ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                try
                {
                    cs.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Szyfracja nie wyszła nie zamknięto streamu " + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                finally
                {
                    fsOut.Close();
                    fsCrypt.Close();
                }
            }
            

            return decryptedBytes;
        }

        private string HeaderToString(string password, string mode, string keySize, List<string> selectedUsers,string IV)
        {
            string header = "";
            header += "EncryptedFileHeader|Algorithm|AES|KeySize|" + keySize.ToString() + "|BlockSize|128|CipherMode|" + mode.ToString() + "|IV|";
            header += IV + "|ApprovedUsers|";
            foreach (var user in selectedUsers)
            {
                header+= "User|"+user+"|SessionKey|";
                header += password + "|";///////////TO DO ENCRYPT
            }
            header += "Done";
            return header;
        }

        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
