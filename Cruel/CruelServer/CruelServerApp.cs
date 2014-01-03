using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Network.Server;
using System.Net.Sockets;
using JSLibrary.Network;
using System.Runtime.Serialization.Formatters.Binary;
using CruelNetwork;
using JSLibrary.Network.Data;

namespace CruelServer
{
    public class CruelServerApp : MultiClientServer
    {
        private CruelClientManager ClientManager;

        public CruelServerApp(ServerFactory factory, TcpListener listener, CruelClientManager ClientManager)
            : base(factory, listener)
        {
            this.ClientManager = ClientManager;
        }


        public override ClientConnector CreateConnector()
        {
            return new CruelClientConnector(new BinaryFormatter(), new MessageTool());
        }
    }
}
