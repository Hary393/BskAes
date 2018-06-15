using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for PurchaseWindow.xaml
    /// </summary>
    public partial class PurchaseWindow : Window
    {
        System.Net.Sockets.TcpClient clientSocket;
        NetworkStream serverStream;// = default(NetworkStream);
        string readData = null;

        public List<string> booklist, clientlist;
        public PurchaseWindow(TcpClient sock)
        {
            clientSocket = sock;
            InitializeComponent();

            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[10025];
            buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("pu|re|Ksiazki$");
            string returndata = "";
            booklist = new List<string>();
            clientlist = new List<string>();

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            while (!(returndata.Equals("DONE")))
            {
                serverStream.Read(inStream, 0, inStream.Length);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                if (!returndata.Equals("DONE"))
                    booklist.Add(returndata);
                else break;
            }
            outStream = System.Text.Encoding.ASCII.GetBytes("pu|re|Klienci$");
            returndata = "";

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            while (!(returndata.Equals("DONE")))
            {
                serverStream.Read(inStream, 0, inStream.Length);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                if (!returndata.Equals("DONE"))
                    clientlist.Add(returndata);
                else break;
            }
            klient.ItemsSource = clientlist;
            ksiazka.ItemsSource = booklist;

        }
        private void ButtonClicked(object sender, RoutedEventArgs e)
        {

            int buffSize = 0;
            //byte[] inStream = new byte[10025];
            //buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("pu|ad|Zakupy|'" + 
                ksiazka.Text.Split(',')[0]+ "','"+klient.Text.Split(',')[0]+ "','" + cena.Text+"'$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            //serverStream.Close();
            this.Close();


        }

        private void ValidateNumberText(TextBox txt)
        {
            txt.Text = Regex.Replace(txt.Text, @"[^\d-]", string.Empty);
            txt.SelectionStart = txt.Text.Length; // add some logic if length is 0
            txt.SelectionLength = 0;
        }

        private void cena_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidateNumberText(cena);
        }


        private void ButtonNewClient(object sender, RoutedEventArgs e)
        {
            Window newWindow = new NewClientWindow(clientSocket, this);
            newWindow.Show();


        }
    }
}
