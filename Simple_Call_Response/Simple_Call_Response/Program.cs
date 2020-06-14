using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Simple_Call_Response
{
    class Program
    {

        public static Settings settings;
        public static List<TcpClient> lstClients = new List<TcpClient>();

        static void Main(string[] args)
        {


            settings = InitializeSettings();
            Console.WriteLine("Welcome to Chatio");
            Console.WriteLine("Type !help for commands");
            while (true)
            {

                string a = Console.ReadLine();

                if (a[0] == '!')
                {
                    Console.Clear();
                    switch (a)
                    {
                        case "!help":
                            Commands.Help();
                            break;
                        case "!name":
                            string name;
                            Console.WriteLine("Input name");

                                name =  Console.ReadLine();
                                settings.Name = name;
                                Console.WriteLine("name set to: "+ settings.Name);

                            
                            break;

                        case "!ip":
                            string ipA;
                            Console.WriteLine("Input IP");
                            ipA = Console.ReadLine();
                            if (!IsIP(ipA) == true)
                                {

                                Console.WriteLine("Wrong input. try again.");
                            }
                            else
                            {
                                settings.IP = ipA;
                                Console.WriteLine("IP set to: " + settings.IP);

                            }
                            break;

                        case "!port":
                            int PORT;
                            Console.WriteLine("Input port");
                            if (!int.TryParse(Console.ReadLine(), out PORT))
                            {

                                Console.WriteLine("Wrong input. try again.");
                            }
                            else
                            {
                                settings.Port = PORT;
                                Console.WriteLine("Port set to: " + settings.Port);

                            }
                            break;

                        case "!server":
                            
                            settings.ServerE = true;
                            settings.ClientE = false;
                            Server();
                           
                            break;
                        case "!client":

                            settings.ClientE = true;
                            settings.ServerE = false;
                            Client();

                            break;
                        case "!clear":
                            Console.Clear();
                            break;
                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }



                }




            }
        }

        public static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[255];
            while (true)
            {
                int numberOfBytesRead = await stream.ReadAsync(buffer, 0, 255);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

                Console.WriteLine("\n" + receivedMessage);
            }
        }




        public static async Task<NetworkStream> ClientConnect(TcpListener listener)
        {
            listener.Start();
            Console.WriteLine("Awaiting Clients");

            TcpClient client = await listener.AcceptTcpClientAsync();

            NetworkStream stream = client.GetStream();
            return stream;
        }


        //Server
        public static async void Server()
        {
            while (settings.ServerE == true) { 
            IPAddress ip = IPAddress.Any;
            IPEndPoint endpoint = new IPEndPoint(ip, settings.Port);
                //Use port from settings

            TcpListener listener = new TcpListener(endpoint);

            Task<NetworkStream> stream = ClientConnect(listener);

            NetworkStream streamR = stream.Result;

            byte[] buffer = new byte[255];

            int numberOfBytesRead = await streamR.ReadAsync(buffer, 0, 255);

            string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);

            Console.WriteLine(message);
            }
        }

        //Client taking ip, port and username as arguments.
        public static void Client()
        {
            while (settings.ClientE == true)
            {
                TcpClient client = new TcpClient();

                

                IPAddress ipA = IPAddress.Parse(settings.IP);
                IPEndPoint endPoint = new IPEndPoint(ipA, settings.Port);

                try
                {
                    
                    while (true)
                    {

                            string text = settings.Name + ": " + Console.ReadLine();
                            client.Connect(endPoint);

                            NetworkStream stream = client.GetStream();


                            byte[] buffer = Encoding.UTF8.GetBytes(text);

                            stream.Write(buffer, 0, buffer.Length);

                            break;
                        }
                         
                    }
            catch (Exception)
                {
                    Console.WriteLine("Error: Couldn't connect to the server you specified.");
                }
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


        static Settings InitializeSettings()
        {
            Settings settings = new Settings();
            
            return settings;
        }







    }
}

