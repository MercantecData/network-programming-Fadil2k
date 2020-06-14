using System;
namespace Simple_Call_Response
{
    public class Settings
    {

            private string name;
            private string ip;
            private int port;
            private bool serverEnabled;
            private bool clientEnabled;


            //Username
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

        //IP
        public string IP
            {
                get { return ip; }
                set { ip = value; }
            }

            //Port
            public int Port
            {
                get { return port; }
                set { port = value; }
            }

            //Client on-off
            public bool ClientE
            {
                get { return clientEnabled; }
                set { clientEnabled = value; }
            }
            //Server on-off
            public bool ServerE
            {
                get { return serverEnabled; }
                set { serverEnabled = value; }
            }

       
    
            }
        }
