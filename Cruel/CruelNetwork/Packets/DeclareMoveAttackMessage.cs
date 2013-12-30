using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using JSLibrary.Network.Data;
using Cruel.GameLogic.PlayerCommands;

namespace CruelNetwork.Packets
{
    public class DeclareMoveAttackMessage : CommandMessage<DeclareMoveAttackCommand>
    {


        protected override void OnSerialize(JSPacket packet)
        {
            
        }

        

        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override DeclareMoveAttackCommand ConstructCommand(Cruel.GameLogic.Player player, JSPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}
