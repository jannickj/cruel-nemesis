using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Network.Server;
using System.Net.Sockets;
using JSLibrary.Network;

namespace CruelServer
{
    public class CruelServer : MultiClientServer
    {

        public CruelServer(ServerFactory factory, TcpListener listener) : base(factory, listener)
        {

        }

        public override ClientConnector CreateConnector()
        {
            return new CruelClientConnector();
        }
    }
}
