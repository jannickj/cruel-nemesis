using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using JSLibrary.Network.Data;

namespace CruelNetwork
{
    public class CruelConnection
    {
        private BinaryFormatter formatter;
        private Stream stream;
        private MessageTool presolver;
        private Queue<JSPacket> packetQueue = new Queue<JSPacket>();
        private AutoResetEvent packetRecieved = new AutoResetEvent(false);

        public CruelConnection(Stream stream, MessageTool presolver, BinaryFormatter formatter)
        {
            this.stream = stream;
            this.presolver = presolver;
        }

        public void UpdateRecieving()
        {
            var packet = (JSPacket)formatter.Deserialize(stream);
            var msg = presolver.Open(packet);
            msg.Execute();
        }

        public void UpdateSending()
        {
            JSPacket[] packets;
            packetRecieved.WaitOne();
            lock (this)
                packets = packetQueue.ToArray();

            foreach (JSPacket packet in packets)
                formatter.Serialize(stream, packet);
        }

        public void QueuePacket(JSPacket packet)
        {
            lock(this)
                this.packetQueue.Enqueue(packet);
            packetRecieved.Set();
        }
    }
}
