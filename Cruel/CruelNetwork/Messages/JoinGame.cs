using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Network.Data;
using System.Net;

namespace CruelNetwork.Messages
{
    public class JoinGame : JSMessage
    {
        private IPAddress ipadd;

        public JoinGame(IPAddress ipadd)
        {
            this.ipadd = ipadd;
        }

        protected override void OnSerialize(JSPacket packet)
        {
        }

        protected override void OnDeserialize(JSPacket packet)
        {
        }

        public override void Execute()
        {
        }
    }
}
