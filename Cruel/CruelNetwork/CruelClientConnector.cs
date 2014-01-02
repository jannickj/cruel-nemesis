using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Network.Server;
using System.Runtime.Serialization.Formatters.Binary;
using CruelNetwork;
using JSLibrary.Network.Data;

namespace CruelNetwork
{
    public class CruelClientConnector : ClientConnector
    {
        private BinaryFormatter binaryFormatter;
        private MessageTool messageTool;



        public CruelClientConnector(BinaryFormatter binaryFormatter, MessageTool messageTool)
        {
            // TODO: Complete member initialization
            this.binaryFormatter = binaryFormatter;
            this.messageTool = messageTool;
        }
        public override void Start()
        {
            var stream = this.Client.GetStream();

            var packet = (JSPacket)binaryFormatter.Deserialize(stream);

            var message = messageTool.Open(packet);
        }
    }
}
