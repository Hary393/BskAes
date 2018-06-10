using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string CurrentUser;
        public Login()
        {
            InitializeComponent();
            this.LoginNameTxtBox.Focus();
        }

        private void Exitbutton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Loginbutton_Click(object sender, RoutedEventArgs e)
        {
            List<string> nameList = new List<string>();
            string login = LoginNameTxtBox.Text.ToString();
            string paswd = LoginPasswdTxtBox.Password;
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
                if (nameList.Contains(login))       //if user exist
                {
                    AuthenticateUser(login, paswd);
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Registerbutton_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
        }
        
        private void AuthenticateUser(string login,string passwrd)
        {
            string dirpath = @"..\..\UsersFiles\";
            dirpath += login;

            string salttxt = "";
            try
            {
                string saltpath = dirpath + "\\\\salt.txt";
                using (StreamReader sr = File.OpenText(saltpath))
                {
                    salttxt = sr.ReadLine();
                }
                byte[] saltBytes = Convert.FromBase64String(salttxt);

                string passpath = dirpath + "\\\\paswd.txt";

                string userpass;
                using (StreamReader sr = File.OpenText(passpath))
                {
                    userpass = sr.ReadLine();
                }
                string givenpass=SHA2salted.GenerateSHA512String(passwrd, saltBytes);
                if (givenpass.Equals(userpass))
                {
                    CurrentUser = login;
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetCurrentUser()
        {
            return CurrentUser;
        }

        private void SendCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SendCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Loginbutton_Click(sender,e);
        }
    }
}
