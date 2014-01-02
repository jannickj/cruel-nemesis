using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using JSLibrary.Network;
using CruelNetwork;
using JSLibrary.Network.Data;
using XmasEngineModel;

namespace CruelServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Parse("localhost"),4000);
            var ServerFactor = new ServerFactory();


            CruelServer server = new CruelServer(ServerFactor, listener);

            server.Start();

        }
    }
}
