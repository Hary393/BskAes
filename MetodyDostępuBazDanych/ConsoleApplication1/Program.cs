using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        public static String[] Permissions = { "clients_r", "clients_w",
                                                "orders_r", "orders_w",
                                                "orderbooks_r","orderbooks_w",
                                                "purchases_r", "purchases_w",
                                                "books_r", "books_w",
                                                "returns_r", "returns_w",
                                                "users_r", "users_w",
                                                "perms_r", "perms_w",
                                                "roles_r", "roles_w" };

        public static string path = ".\\tables\\";

        public static Dictionary<String, HashSet<String>> roles;
        public static Dictionary<String, HashSet<String>> users;

        public Boolean hsContains<T>(HashSet<T> set, T val)
        {
            foreach (T t in set)
                if (t.Equals(val)) return true;
            return false;
        }

        //public static HashSet<string> clientsList;
        public static List<String> activeUsers;

        static void Main(string[] args)
        {
            activeUsers = new List<String>();
            Console.WriteLine("Getting roles...");
            roles = new Dictionary<string, HashSet<string>>();
            string[] roleLines = File.ReadAllLines(path+"roles.txt");
            int en;
            foreach (string l in roleLines)
            {
                string[] line = l.Split(';');
                HashSet<string> r =  new HashSet<string>() ;
                en = 0;
                foreach (string l2 in line)
                {
                    if (en == 0) ;
                    //r.name = l2;
                    else if (Permissions.Contains(l2))
                        r.Add(l2);
                    en++;
                }
                roles.Add(line[0], r);
            }
            Console.WriteLine("Getting users...");
            string[] userLines = File.ReadAllLines(path+"users.txt");
            users = new Dictionary<string, HashSet<string>>();
            foreach (string l in userLines)
            {
                string[] line = l.Split(';');
                HashSet<string> u = new  HashSet<string>();
                en = 0;
                foreach (string l2 in line)
                {
                    if (en == 0) ;
                       // u.name = l2;
                    else
                    {
                        //foreach (KeyValuePair<string,HashSet<string>> r in roles)
                        //{
                            if (roles.ContainsKey(l2))
                                u.Add(l2);
                        //}
                    }
                    en++;
                }
                users.Add(line[0], u);
            }

            Console.WriteLine("Preparing server application...");

            List<string> temp_u = new List<string>();
            foreach (KeyValuePair<string, HashSet<string>> u in users)
                temp_u.Add(u.Key);

            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(localAddr, port);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;
            Byte[] bytesTo = null;
            //String[] split = null;

            serverSocket.Start();
            Console.WriteLine("Server started at "+ DateTime.Now.ToString("MMM d yyyy HH:mm:ss"));
            counter = 0;
            while ((true))
            {
                try
                {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFrom = new byte[1002500];
                    string dataFromClient = null;

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    //clientsList.Add(dataFromClient, clientSocket);
                    Console.WriteLine(dataFromClient);
                    String[] splitMsg = dataFromClient.Split('|');
                    //broadcast(dataFromClient + " Joined ", dataFromClient, false);
                    if (activeUsers.Contains(dataFromClient))
                    {

                        bytesTo = System.Text.Encoding.ASCII.GetBytes("ALREADY_LOGGED$");
                        networkStream.Write(bytesTo, 0, bytesTo.Length);
                        networkStream.Flush();
                        clientSocket = null;
                    }
                    else if (!temp_u.Contains(splitMsg[0]))
                    {
                        bytesTo = System.Text.Encoding.ASCII.GetBytes("INCORRECT_LOGIN$");
                        networkStream.Write(bytesTo, 0, bytesTo.Length);
                        networkStream.Flush();
                        clientSocket = null;
                    }
                    else
                    {
                        if (!users[splitMsg[0]].Contains(splitMsg[1]))
                        {
                            bytesTo = System.Text.Encoding.ASCII.GetBytes("INCORRECT_ROLE$");
                            networkStream.Write(bytesTo, 0, bytesTo.Length);
                            networkStream.Flush();
                            clientSocket = null;
                        }
                        else
                        {
                            bytesTo = System.Text.Encoding.ASCII.GetBytes("OK$");
                            networkStream.Write(bytesTo, 0, bytesTo.Length);
                            networkStream.Flush();
                            activeUsers.Add(dataFromClient);
                            Console.WriteLine(dataFromClient + " logged in ");
                            handleClinet client = new handleClinet();
                            client.startClient(clientSocket, splitMsg[0], splitMsg[1], activeUsers);//, clientsList);
                        }
                    }
                }
                catch { }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadLine();
        }

        public class handleClinet
        {
            TcpClient clientSocket;
            string clNo;
            string clRole;
            //Hashtable clientsList;
            List<String> activeClients;
            SqlConnection con;

            public void startClient(TcpClient inClientSocket, string clineNo, string cliRole, List<String> aC)//, Hashtable cList)
            {
                this.clientSocket = inClientSocket;
                this.clNo = clineNo;
                //this.clientsList = cList;
                clRole = cliRole;
                activeClients = aC;
                con = new SqlConnection();
                con.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;"+
                    "Initial Catalog=Ksiegarnia;Integrated Security=True;"+
                    "Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;"+
                    "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                con.Open();
                Console.WriteLine("Connected to "+con.Database);
                //con.ChangeDatabase("Księgarnia");
               // Console.WriteLine(con.Database);
                Thread ctThread = new Thread(doChat);
                ctThread.Start();
            }

            public List<string> retrieveInformation(string table) {
                return new List<string>(File.ReadAllLines(path + table+".txt").ToList<string>());
            }

            private void secondSwitch(string[] split, NetworkStream stream) {
                //byte[] bytesFrom = new byte[1002500];
                string dataFromClient = null;
                Byte[] sendBytes = null;
                Byte[] bytesTo = null;
                string query;
                SqlCommand c;
                switch (split[1]) {
                    case "sh":
                        bytesTo = System.Text.Encoding.ASCII.GetBytes("OK$");
                        stream.Write(bytesTo, 0, bytesTo.Length);
                        stream.Flush();
                        break;
                    case "ad":
                        query = "INSERT INTO dbo."+split[2]+" VALUES("+split[3]+");";
                        c = new SqlCommand(query, con);
                        c.ExecuteNonQuery();
                        break;
                    case "up":
                        query = "UPDATE dbo." + split[2] + " SET " + split[3] + " WHERE "+ split[4]+";";
                        c = new SqlCommand(query, con);
                        c.ExecuteNonQuery();
                        break;
                    case "re":
                        query = "SELECT * FROM dbo."+split[2]+";";
                        c = new SqlCommand(query, con);
                        SqlDataAdapter da = new SqlDataAdapter(c);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        foreach (DataRow dr in dt.Rows)
                        {
                            string temp = "";
                            foreach (var s in dr.ItemArray)
                                temp += s.ToString()+",";
                            temp.Replace(" ,", ",");
                            Console.WriteLine(temp);
                            bytesTo = System.Text.Encoding.ASCII.GetBytes(temp+"$");
                            stream.Write(bytesTo, 0, bytesTo.Length);
                            stream.Flush();
                        }

                        bytesTo = System.Text.Encoding.ASCII.GetBytes("DONE$");
                        stream.Write(bytesTo, 0, bytesTo.Length);
                        stream.Flush();

                        break;
                    case "ct":
                        query = "SELECT MAX(ID_Zamowienia) FROM dbo." + split[2] + ";";
                        c = new SqlCommand(query, con);
                        da = new SqlDataAdapter(c);
                        dt = new DataTable();
                        da.Fill(dt);
                        string count = dt.Rows[0].ItemArray[0].ToString();
                        //foreach (DataRow dr in dt.Rows)
                            //count++;
                        bytesTo = System.Text.Encoding.ASCII.GetBytes(count + "$");
                        stream.Write(bytesTo, 0, bytesTo.Length);
                        stream.Flush();
                        break;
                }
            }

            private void doChat()
            {
                int requestCount = 0;
                byte[] bytesFrom = new byte[100250];
                string dataFromClient = null;
                Byte[] sendBytes = null;
                Byte[] bytesTo = null;
                string serverResponse = null;
                string rCount = null;
                requestCount = 0;

                while ((true))
                {
                    try
                    {
                        requestCount = requestCount + 1;
                        NetworkStream networkStream = clientSocket.GetStream();
                        networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                        dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                        Console.WriteLine(clNo + "  " + dataFromClient);
                        rCount = Convert.ToString(requestCount);
                        string[] split = dataFromClient.Split('|');
                        switch (split[0]) {
                            case "cl":
                                if (!roles[clRole].Contains("clients_w"))
                                {
                                bytesTo = System.Text.Encoding.ASCII.GetBytes("BAD_ROLE$");
                                    networkStream.Write(bytesTo, 0, bytesTo.Length);
                                    networkStream.Flush();
                                }
                                else {
                                    secondSwitch(split, networkStream);
                                //bytesTo = System.Text.Encoding.ASCII.GetBytes("OK$");
                                }
                                break;
                            case "or":
                                if (!roles[clRole].Contains("orders_w"))
                                {
                                    bytesTo = System.Text.Encoding.ASCII.GetBytes("BAD_ROLE$");
                                    networkStream.Write(bytesTo, 0, bytesTo.Length);
                                    networkStream.Flush();
                                }
                                else
                                {
                                    secondSwitch(split, networkStream);
                                    //bytesTo = System.Text.Encoding.ASCII.GetBytes("OK$");
                                }
                                break;
                            case "pu":
                                if (!roles[clRole].Contains("purchases_w"))
                                {
                                    bytesTo = System.Text.Encoding.ASCII.GetBytes("BAD_ROLE$");
                                    networkStream.Write(bytesTo, 0, bytesTo.Length);
                                    networkStream.Flush();
                                }
                                else
                                {
                                    secondSwitch(split, networkStream);
                                    //bytesTo = System.Text.Encoding.ASCII.GetBytes("OK$");
                                }
                                break;
                            case "re":
                                if (!roles[clRole].Contains("returns_w"))
                                {
                                    bytesTo = System.Text.Encoding.ASCII.GetBytes("BAD_ROLE$");
                                    networkStream.Write(bytesTo, 0, bytesTo.Length);
                                    networkStream.Flush();
                                }
                                else
                                {
                                    secondSwitch(split, networkStream);
                                    //bytesTo = System.Text.Encoding.ASCII.GetBytes("OK$");
                                }
                                
                                break;

                        }
                        //Program.broadcast(dataFromClient, clNo, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(clNo+"|"+clRole+" left");
                        activeClients.Remove(clNo+"|"+clRole);
                        con.Close();
                        break;
                    }
                }//end while
            }//end doChat
        } //end class handleClinet
    }
}
