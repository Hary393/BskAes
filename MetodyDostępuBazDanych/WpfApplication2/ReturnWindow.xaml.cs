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
    /// Interaction logic for ReturnWindow.xaml
    /// </summary>
    public partial class ReturnWindow : Window
    {
        System.Net.Sockets.TcpClient clientSocket;
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        public List<string> clients, purchases, books;

        public ReturnWindow(TcpClient sock)
        {
            clientSocket = sock;
            InitializeComponent();
            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[10025];
            buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("re|re|Ksiazki$");
            string returndata = "";
            books = new List<string>();
            clients = new List<string>();
            purchases = new List<string>();

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            while (!(returndata.Equals("DONE")))
            {
                serverStream.Read(inStream, 0, inStream.Length);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                if (!returndata.Equals("DONE"))
                    books.Add(returndata);
            }
            outStream = System.Text.Encoding.ASCII.GetBytes("re|re|Klienci$");
            returndata = "";

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            while (!(returndata.Equals("DONE")))
            {
                serverStream.Read(inStream, 0, inStream.Length);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                if (!returndata.Equals("DONE"))
                    clients.Add(returndata);
            }

            outStream = System.Text.Encoding.ASCII.GetBytes("re|re|Zakupy$");
            returndata = "";

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            while (!(returndata.Equals("DONE")))
            {
                serverStream.Read(inStream, 0, inStream.Length);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                if (!returndata.Equals("DONE"))
                    purchases.Add(returndata);
            }

            id.ItemsSource = purchases;
            //klient.ItemsSource = clientlist;
            //ksiazka.ItemsSource = booklist;

        }

        private void id_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string klient = id.SelectedItem.ToString().Split(',')[2];
            string ksiazka = id.SelectedItem.ToString().Split(',')[1];

            //string temp = "";
            foreach (string s in books)
                if (s.Split(',')[0].Equals(ksiazka)){
                    Ksiazka.Text = s;
                    break;
                }
            foreach (string s in clients)
                if (s.Split(',')[0].Equals(klient)){
                    Klient.Text = s;
                    break;
                }

            
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            string idz = id.SelectedItem.ToString().Split(',')[0];
            string data = DateTime.Now.ToString("yyyy-MM-dd");
            int buffSize = 0;
            byte[] inStream = new byte[10025];
            buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("re|ad|Zwroty|'"+idz+"','" + 
                data + "','" + reason.Text + "'$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            serverStream.Close();
            this.Close();
        }
    }
}
