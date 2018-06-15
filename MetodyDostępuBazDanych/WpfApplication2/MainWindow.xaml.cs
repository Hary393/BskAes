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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Net.Sockets.TcpClient clientSocket;
        NetworkStream serverStream; 
        string readData = null;
        public MainWindow(TcpClient a)
        {
            clientSocket = a;
            serverStream = clientSocket.GetStream();
            InitializeComponent();
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[100250];
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("or|sh$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            
            buffSize = clientSocket.ReceiveBufferSize;
            serverStream.Read(inStream, 0, buffSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            returndata = returndata.Substring(0, returndata.IndexOf("$"));

            if (returndata.Equals("OK"))
            {
                Window ordWindow = new OrderWindow(clientSocket);
                ordWindow.Show();
            }
            else MessageBox.Show("Server returned " + returndata, returndata, MessageBoxButton.OK);
        }
        
        private void ButtonClicked2(object sender, RoutedEventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("pu|sh$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[100250];
            buffSize = clientSocket.ReceiveBufferSize;
            serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            returndata = returndata.Substring(0, returndata.IndexOf("$"));

            if (returndata.Equals("OK"))
            {
                Window prchWindow = new PurchaseWindow(clientSocket);
                prchWindow.Show();
            }
            else MessageBox.Show("Server returned " + returndata, returndata, MessageBoxButton.OK);
        }

        private void ButtonClicked3(object sender, RoutedEventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("re|sh$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[100250];
            buffSize = clientSocket.ReceiveBufferSize;
            serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            returndata = returndata.Substring(0, returndata.IndexOf("$"));

            if (returndata.Equals("OK"))
            {
                Window retWindow = new ReturnWindow(clientSocket);
                retWindow.Show();
            }
            else MessageBox.Show("Server returned " + returndata, returndata, MessageBoxButton.OK);
        }
        private void ButtonClicked4(object sender, RoutedEventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("or|sh$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[100250];
            buffSize = clientSocket.ReceiveBufferSize;
            serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            returndata = returndata.Substring(0, returndata.IndexOf("$"));

            if (returndata.Equals("OK"))
            {
                Window manWindow = new ManageWindow(clientSocket);
                manWindow.Show();
            }
            else MessageBox.Show("Server returned " + returndata, returndata, MessageBoxButton.OK);
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            //serverStream.Close();
            this.Close();
        }
    }
}
