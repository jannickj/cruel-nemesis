using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Cruel.GameLogic.PlayerCommands;
using JSLibrary.Network.Data;
using Cruel.GameLogic;
using XmasEngineModel;

namespace CruelNetwork.Packets
{
    public abstract class CommandMessage<TPlayerCommand> : JSMessage where TPlayerCommand : PlayerCommand
    {
        public TPlayerCommand Command { get; set; }
        public XmasModel Engine { get; internal set; }

        public abstract TPlayerCommand ConstructCommand(Player player, JSPacket packet);

        protected sealed override void  OnDeserialize(JSPacket packet)
        {
            int playerid = (int)packet.Get("player");
            
            //Command = ConstructCommand(, packet);
        }

        protected override void OnSerialize(JSPacket packet)
        {
            packet.Add("player", Command.Player);
        }
    }
}
