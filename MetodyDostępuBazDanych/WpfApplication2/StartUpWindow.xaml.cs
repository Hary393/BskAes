using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        public List<string> rolelist;

        public StartUpWindow()
        {
            InitializeComponent();
            rolelist = new List<string>();
            rolelist.Add("supplier");
            rolelist.Add("clerk");
            role.ItemsSource = rolelist;
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
            NetworkStream serverStream = default(NetworkStream);
            string readData = null;
            try
            {
                clientSocket.Connect("127.0.0.1", 13000);
                serverStream = clientSocket.GetStream();

                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(UsrName.Text + "|"+role.SelectedItem+"$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                serverStream = clientSocket.GetStream();
                int buffSize = 0;
                byte[] inStream = new byte[1002500];
                buffSize = clientSocket.ReceiveBufferSize;
                serverStream.Read(inStream, 0, inStream.Length);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));

                if (returndata.Equals("OK"))
                {
                    MainWindow w = new MainWindow(clientSocket);
                    w.Show();
                    this.Close();
                }
                else {
                    MessageBox.Show("Server returned " + returndata, returndata, MessageBoxButton.OK);
                    clientSocket.Close();
                    //serverSocket.Stop();
                }
                //else { }
            }
            catch {

            }
        }
    }
}
