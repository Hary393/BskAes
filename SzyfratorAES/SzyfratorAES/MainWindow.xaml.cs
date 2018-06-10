using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SzyfratorAES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string LoggedUser;
        List<string> selectedUserList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            Login loginWindow = new Login();
            var result=loginWindow.ShowDialog();
            if (result==true)
            {
                LoggedUser = loginWindow.GetCurrentUser();
                string path = @"..\..\UsersFiles\UserList.txt";
                List<string> nameList = new List<string>();
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        nameList.Add(s);
                    }
                }
                usersListView.ItemsSource = nameList;
                reciversListView.ItemsSource = selectedUserList;

            }
            else
            {
                try
                {
                    Application.Current.Shutdown();
                }
                catch (Exception){}
                
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void fileSelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
            {
                fileLabel1.Content = openFileDialog.FileName;
            }

        }
        private void pathSelectButton_Click(object sender, RoutedEventArgs e)
        {
            var folderDialog= new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
               pathLabel.Content = folderDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("Nie wybrano scieżki.", "Nie wybrano scieżki.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void addPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (usersListView.SelectedItem != null)
            {
                string who = usersListView.SelectedItem.ToString();
                if (!selectedUserList.Contains(who))
                {
                    selectedUserList.Add(who);
                    ICollectionView view = CollectionViewSource.GetDefaultView(selectedUserList);
                    view.Refresh();
                }
            }
        }
        private void removePersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (reciversListView.SelectedItem != null)
            {
                string who = reciversListView.SelectedItem.ToString();
                selectedUserList.Remove(who);
                ICollectionView view = CollectionViewSource.GetDefaultView(selectedUserList);
                view.Refresh();
            }
        }

        private void magicButton_Click(object sender, RoutedEventArgs e)
        {
            string fileWhereToSave = pathLabel.Content.ToString() + "\\\\" + NazwaWynikowaTxtBox.Text + System.IO.Path.GetExtension(fileLabel1.Content.ToString());
            if (!File.Exists(fileLabel1.Content.ToString()))
            {
                MessageBox.Show("Plik nie istnieje.", "Plik nie istnieje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (!Directory.Exists(pathLabel.Content.ToString()))
            {
                MessageBox.Show("Folder nie istnieje.", "Folder nie istnieje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (NazwaWynikowaTxtBox.Text.Equals(""))
            {
                MessageBox.Show("Nazwa Wynikowa jest pusta", "Nazwa Wynikowa jest pusta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (File.Exists(fileWhereToSave))
            {
                MessageBox.Show("Istnieje Plik o Podanej Nazwie", "Istnieje Plik o Podanej Nazwie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (szyfrujButton.IsChecked==true)
            {
                if (selectedUserList.Count > 0)
                {
                    try
                    {

                        CipherMashine CMachine = new CipherMashine();
                        string file = fileLabel1.Content.ToString();
                        string password = "abcd1234";

                        CMachine.AES_Encrypt(file,fileWhereToSave, password, ((ComboBoxItem)comboBox.SelectedItem).Content.ToString(), ((ComboBoxItem)comboBoxKey.SelectedItem).Content.ToString(), selectedUserList.ToList<string>());

                        MessageBox.Show("Zakończono", "Szyfruj", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Szyfracja nie wyszła", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);

                        //AesSzyfrator.EncryptAes(fileLabel1.Content.ToString(), pathLabel.Content.ToString(), "kek", "huehuehuehue", comboBox.SelectedItem.ToString(),NazwaWynikowaTxtBox.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Wybierz chociaż 1 odbiorcę", "Wybierz chociaż 1 odbiorcę", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if(deszyfrujButton.IsChecked == true)
            {
                if (selectedUserList.Count == 1)
                {
                    try
                    {
                        CipherMashine CMachine = new CipherMashine();
                        string fileEncrypted= fileLabel1.Content.ToString();
                        string password = "abcd1234";

                        CMachine.AES_Decrypt(fileEncrypted, fileWhereToSave, password, ((ComboBoxItem)comboBox.SelectedItem).Content.ToString());

                        MessageBox.Show("Zakończono", "Deszyfruj", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Deszyfracja nie wyszła", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    //AesSzyfrator.DecryptAes(fileLabel1.Content.ToString(), pathLabel.Content.ToString(), "kek", "huehuehuehue", comboBox.SelectedItem.ToString(),NazwaWynikowaTxtBox.Text);
                }
                else
                {
                    MessageBox.Show("Wybierz tylko 1 odbiorcę", "Wybierz tylko 1 odbiorcę", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            //Magic magicWindow = new Magic();             Its magic don't touch it 
            //magicWindow.Show();
        }

        
    }
}
