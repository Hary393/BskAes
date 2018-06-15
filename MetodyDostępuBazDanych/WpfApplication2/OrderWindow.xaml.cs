using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public List<KeyValuePair<string, int>> TestList{ get; set; }
    //static public List<int> test2list;
    public List<String> booklist{ get; set; }
public System.Net.Sockets.TcpClient clientSocket;
        public NetworkStream serverStream;
        string readData = null;

        public struct element {
            public List<string> books;
            public int amt;
            public element(List<string> l, int i) { books = l; amt = i; }
        }
        public OrderWindow(TcpClient sock)
        {

            InitializeComponent();
            clientSocket = sock;
            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[10025];
            buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("or|re|Ksiazki$");
            string returndata="";
            booklist = new List<string>();

            TestList = new List<KeyValuePair<string, int>>();
            //test2list = new List<int>();
            //TestList.Add(new KeyValuePair<string, int>("aieou", 3));
            OrderGrid.ItemsSource = null;
            OrderGrid.ItemsSource = TestList;

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


        }
        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            string data = DateTime.Now.ToString("yyyy-MM-dd");
            
            int buffSize = 0;
            byte[] inStream = new byte[100250];
            buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("or|ad|Zamowienia|'"+data+"',NULL$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            outStream = System.Text.Encoding.ASCII.GetBytes("or|ct|Zamowienia$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            string returndata = "";
            serverStream.Read(inStream, 0, inStream.Length);
            returndata = System.Text.Encoding.ASCII.GetString(inStream);
            returndata = returndata.Substring(0, returndata.IndexOf("$"));
            string id = returndata;

            foreach (KeyValuePair<string, int> dt in OrderGrid.Items)
            {
                string query = "'" + id + "','" + dt.Key.Split(',')[0] + "','"+dt.Value+"'";
                outStream = System.Text.Encoding.ASCII.GetBytes("or|ad|ZamowieniaKsiazek|"+query+"$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            //serverStream.Close();
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            FIXWindow f = new FIXWindow(this);
            f.Show();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public class Test 
        {
            public String Books { get; set; }
            public int Amt { get; set; }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //serverStream.Close();
            this.Close();
        }
    }
}
