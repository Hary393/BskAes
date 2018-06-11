using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SzyfratorAES
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
            this.LoginNameTxtBox.Focus();
        }

        private void Exitbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Registerbutton_Click(object sender, RoutedEventArgs e)
        {
            List<string> nameList = new List<string>();
            string login = LoginNameTxtBox.Text.ToString();
            string paswd = LoginPasswdTxtBox.Password;
            string paswd2 = LoginPasswdTxtBox2.Password;
            string path = @"..\..\UsersFiles\UserList.txt";
            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path)) { }//create the file if it dosent exist
                }

                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        nameList.Add(s);
                    }
                }
                if (login.Length==0 || paswd.Length==0 || paswd2.Length==0)
                {
                    MessageBox.Show("Fill in requied fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (nameList.Contains(login))       //if user exist
                    {
                        MessageBox.Show("UserName is already taken.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (!paswd.Equals(paswd2))
                    {
                        MessageBox.Show("Passwords dont match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (!ValidatePassword(paswd)) { }//messages and handling in function}
                    else
                    {
                        CreateUser(login, paswd);
                    }
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void CreateUser(string login,string password) ///////Creates a user by adding his name to userlist.txt and creating his user folder with hashed pass and salt 
        {//////////also create RSA keys
            string path = @"..\..\UsersFiles\UserList.txt";
            string dirpath = @"..\..\UsersFiles\";
            dirpath += login;
            try
            {
                using (StreamWriter sw = File.AppendText(path)) ///////adding to userlist
                {
                    sw.WriteLine(login);
                }

                byte[] salt1 = new byte[8];
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(salt1);
                }
                System.IO.Directory.CreateDirectory(dirpath);
                string passpath = dirpath + "\\\\paswd.txt";
                using (StreamWriter sw = File.CreateText(passpath)) {   //create the file for password
                    sw.WriteLine(SHA2salted.GenerateSHA512String(password, salt1));
                }
                string saltpath = dirpath + "\\\\salt.txt";
                using (StreamWriter sw = File.CreateText(saltpath))   //create the file for salt
                {   
                    sw.WriteLine(SHA2salted.GetStringFromHash(salt1));
                }
                string who = "Hi " + login;
                MessageBox.Show("User Created", who, MessageBoxButton.OK, MessageBoxImage.None);
                //lets take a new CSP with a new 2048 bit rsa key pair
                var csp = new RSACryptoServiceProvider(2048);

                //how to get the private key
                var privKey = csp.ExportParameters(true);
                string privKeyString;
                //we need some buffer
                var sw1 = new System.IO.StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw1, privKey);
                //get the string from the stream
                privKeyString = sw1.ToString();

                //and the public key ...
                var pubKey = csp.ExportParameters(false);
                string pubKeyString;
                //serialize the key into the stream
                xs.Serialize(sw1, pubKey);
                //get the string from the stream
                pubKeyString = sw1.ToString();


                string dirpathPub = dirpath + @"\PUGB";
                System.IO.Directory.CreateDirectory(dirpathPub);
                dirpathPub+= @"\PUGB.txt";
                using (StreamWriter sw = File.CreateText(dirpathPub))
                {   //create the file for publicKey
                    sw.WriteLine(pubKeyString);
                }


                string dirpathPriv = dirpath + @"\PRIV";
                System.IO.Directory.CreateDirectory(dirpathPriv);
                dirpathPriv += @"\PRIV.txt";
                RSAHandle.EncryptPrivate(privKeyString, SHA2salted.GenerateSHA512String(password, salt1), dirpathPriv);
                string result=RSAHandle.DecryptPrivate(SHA2salted.GenerateSHA512String(password, salt1), dirpathPriv);
                this.Close();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private bool ValidatePassword(string password)
        {
            var input = password;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,52}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                MessageBox.Show("Password should contain At least one lower case letter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                MessageBox.Show("Password should contain At least one upper case letter", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                MessageBox.Show("Password should not be less than 12 characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                MessageBox.Show("Password should contain At least one numeric value", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                MessageBox.Show("Password should contain At least one special case characters", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SendCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SendCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Registerbutton_Click(sender, e);
        }


    }
}
