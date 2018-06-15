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
    /// Interaction logic for ManageWindow.xaml
    /// </summary>
    public partial class ManageWindow : Window
    {
        System.Net.Sockets.TcpClient clientSocket;
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;
        public List<string> managelist;
        public List<Tuple<string, string, string>> source { get; set; }
        public int selector;
        public ManageWindow(TcpClient sock)
        {

            InitializeComponent();
            clientSocket = sock;
            selector = -1;
            serverStream = clientSocket.GetStream();
            int buffSize = 0;
            byte[] inStream = new byte[10025];
            buffSize = clientSocket.ReceiveBufferSize;
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("or|re|Zamowienia$");
            string returndata = "";
            managelist = new List<string>();
            source = new List<Tuple<string, string, string>>();
            ManageGrid.ItemsSource = null;

            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            while (!(returndata.Equals("DONE")))
            {
                serverStream.Read(inStream, 0, inStream.Length);
                returndata = System.Text.Encoding.ASCII.GetString(inStream);
                returndata = returndata.Substring(0, returndata.IndexOf("$"));
                if (!returndata.Equals("DONE"))
                    managelist.Add(returndata);
                else break;
            }
            foreach (string s in managelist)
                source.Add(new Tuple<string, string, string>(s.Split(',')[0], s.Split(',')[1].Split(' ')[0], s.Split(',')[2].Split(' ')[0]));
            ManageGrid.ItemsSource = source;

        }
        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!(selector < 0))
            {
                if (!source.ElementAt(selector).Item3.Equals("")) return;
                else
                {
                    string data = DateTime.Now.ToString("yyyy-MM-dd");
                    var temp = ManageGrid.Items.GetItemAt(selector);
                    string id = temp.ToString().Split(',')[0].Replace("(", "");
                    string query = "or|up|Zamowienia|Data_Odbioru='" + data + "'|ID_Zamowienia='" + id + "'$";
                    byte[] outStream = System.Text.Encoding.ASCII.GetBytes(query);
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();


                    int buffSize = 0;
                    byte[] inStream = new byte[100250];
                    buffSize = clientSocket.ReceiveBufferSize;
                    outStream = System.Text.Encoding.ASCII.GetBytes("or|re|Zamowienia$");
                    string returndata = "";
                    managelist = new List<string>();
                    source = new List<Tuple<string, string, string>>();
                    ManageGrid.ItemsSource = null;

                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();

                    while (!(returndata.Equals("DONE")))
                    {
                        serverStream.Read(inStream, 0, inStream.Length);
                        returndata = System.Text.Encoding.ASCII.GetString(inStream);
                        returndata = returndata.Substring(0, returndata.IndexOf("$"));
                        if (!returndata.Equals("DONE"))
                            managelist.Add(returndata);
                        else break;
                    }
                    foreach (string s in managelist)
                        source.Add(new Tuple<string, string, string>(s.Split(',')[0], s.Split(',')[1].Split(' ')[0], s.Split(',')[2].Split(' ')[0]));
                    ManageGrid.ItemsSource = source;
                }
            }
        }
        private void ButtonExit(object sender, RoutedEventArgs e)
        {
            //serverStream.Close();
            this.Close();


        }

        private void ManageGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selector = ManageGrid.SelectedIndex;
        }
    }
}
