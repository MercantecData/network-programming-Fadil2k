using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Simple_Call_Response
{
    class Program
    {
        static void Main(string[] args)
        {
            // "tryAgain" bool
            bool TA1 = true;
            //Loop to be able to go back to the beginning with!back command
            while (TA1 == true) {


            //Specify choice of application
            Console.WriteLine("Do you want to start a client or a server?");
            Console.WriteLine("Choose a number:");
            Console.WriteLine("1 : Server");
            Console.WriteLine("2 : Client");
            int option = Int16.Parse(Console.ReadLine());


            Console.Clear();

            //Specify port
            Console.WriteLine("Which port would you like to use?");
            int port = Int16.Parse(Console.ReadLine());


            
            bool TA2 = true;
            //Loop to make sure the rights arguments are passed to the server or client
            while (TA2)
            {
                switch (option)
                {
                    case 1:
                        TA2 = false;
                            option1(port);
                        break;

                    case 2:
                        TA2 = false;
                            option2(port);
                        break;

                    default:
                        Console.WriteLine("Error try again");
                        break;
                }
            }
        }
       }

        
        public static void option1(int port)
        {
            Console.WriteLine("You chose to run as server");
            Server(port);
        }

        //Divided the two options into two functions for better structure and easier overview

        public static void option2(int port)
        {
            Console.WriteLine("You chose to run as client");
            Console.WriteLine("Type IP-A of the server.");
            string ip = Console.ReadLine();
            if (IsIP(ip) == true)
            {
                Console.WriteLine("Enter a username");
                string username = Console.ReadLine();
                Client(ip, port, username);
            }
            else
            {
                Console.WriteLine("Error try again");
            }
        }


        public static NetworkStream ConnectToServer(IPEndPoint endpoint, TcpClient localClient)
        {
            localClient.Connect(endpoint);
            NetworkStream stream = localClient.GetStream();
            return stream;
        }


        //Server taking port as argument.
        public static void Server(int port) {
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localEndpoint);
            listener.Start();

            Console.WriteLine("Awaiting Clients");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[256];

            int numberOfBytesRead = stream.Read(buffer, 0, 256);

            string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

            Console.WriteLine(message);
        }

        //Client taking ip, port and username as arguments.
        public static void Client(string ip, int port, string username)
        {
            TcpClient client = new TcpClient();



            IPAddress ipA = IPAddress.Parse(ip);
            IPEndPoint endPoint = new IPEndPoint(ipA, port);

            try
            {
                bool TA3 = true;
                while (TA3 == true)
                {
                    string cmd = Console.ReadLine();
                    if (cmd != "!back") 
                    {

                    string text = username + ": " + Console.ReadLine();
                    client.Connect(endPoint);

                    NetworkStream stream = client.GetStream();

          
                    byte[] buffer = Encoding.UTF8.GetBytes(text);

                    stream.Write(buffer, 0, buffer.Length);

                        break;
                        } else
                    {
                        client.Close();
                        TA3 = false;
                        
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Couldn't connect to the server you specified.");
            }
        }

        //Check if the user input is a ip or not.
        private static bool IsIP(string ip)
        {
            //Bool that will be used for returning if input is a ip or not.
            bool value = false;

            try
            {
                IPAddress address;
                //TryParse determines whether a string is a valid IP address.

                value = IPAddress.TryParse(ip, out address);
            }
            catch (Exception ex)
            {
            }
            return value;
        }


    }
}
