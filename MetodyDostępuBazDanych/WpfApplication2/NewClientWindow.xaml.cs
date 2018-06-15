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
    /// Interaction logic for NewClientWindow.xaml
    /// </summary>
    public partial class NewClientWindow : Window
    {
        TcpClient clientSocket;
        public NetworkStream serverStream;
        PurchaseWindow p;
        public NewClientWindow(TcpClient sock, PurchaseWindow parent)
        {
            p = parent;
            clientSocket = sock;
            serverStream = clientSocket.GetStream();
            InitializeComponent();

        }
        private void ButtonClicked(object sender, RoutedEventArgs e)
        {

            int buffSize = 0;
            byte[] inStream = new byte[10025];
            buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("pu|ad|Klienci|'" + name.Text +"','" + surname.Text + "'$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            p.clientlist = new List<string>();
            outStream = System.Text.Encoding.ASCII.GetBytes("pu|re|Klienci$");
            string returndata = "";

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            while (!(returndata.Equals("DONE")))
            {
                serverStream.Read(inStream, 0, inStream.Length);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                if (!returndata.Equals("DONE"))
                    p.clientlist.Add(returndata);
            }
            p.klient.ItemsSource = p.clientlist;
            //serverStream.Close();
            this.Close();


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //serverStream.Close();
            this.Close();
        }
    }
}
