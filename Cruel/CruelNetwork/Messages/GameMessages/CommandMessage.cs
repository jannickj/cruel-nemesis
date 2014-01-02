using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Cruel.GameLogic.PlayerCommands;
using JSLibrary.Network.Data;
using Cruel.GameLogic;
using XmasEngineModel;

namespace CruelNetwork.Messages.GameMessages
{

    public abstract class CommandMessage : JSMessage
    {
        public PlayerCommand Command { get; set; }
        public abstract PlayerCommand ConstructCommand(Player player, JSPacket packet);
        public XmasModel Engine { get; internal set; }

        protected sealed override void OnDeserialize(JSPacket packet)
        {
            ulong playerid = (ulong)packet.Get("cmm_player");
            Player p = (Player)Engine.FindObject(playerid);
            Command = ConstructCommand(p, packet);
        }

        protected override void OnSerialize(JSPacket packet)
        {
            packet.Add("cmm_player", Command.Player);
        }

        public override void Execute()
        {
            
            this.Engine.ActionManager.Queue(this.Command);
        
        }
    }

    public abstract class CommandMessage<TPlayerCommand> : CommandMessage where TPlayerCommand : PlayerCommand
    {
        public new TPlayerCommand Command { get { return (TPlayerCommand)base.Command; } set { base.Command = value; } }      

        
    }
}
