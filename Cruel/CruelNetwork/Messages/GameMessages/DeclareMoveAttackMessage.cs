using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using JSLibrary.Network.Data;
using Cruel.GameLogic.PlayerCommands;
using Cruel.GameLogic;
using Cruel.GameLogic.Unit;
using XmasEngineExtensions.TileExtension;
using Cruel.Library.PathFinding;
using JSLibrary.Data;

namespace CruelNetwork.Messages.GameMessages
{
    public class DeclareMoveAttackMessage : CommandMessage<DeclareMoveAttackCommand>
    {


        protected override void OnSerialize(JSPacket packet)
        {
            base.OnSerialize(packet);
            var com = this.Command;
            ulong attackid = com.AttackUnit == null ? 0 : com.AttackUnit.Id;
            packet.Add("attackunit",attackid);
            packet.Add("moveunit",com.MoveUnit.Id);
            var positions = com.Path.Road.Select(tile => tile.Point).ToArray();
            packet.Add("path",positions);
        }

        

        

        public override PlayerCommand ConstructCommand(Player player, JSPacket packet)
        {
            var attackid = (ulong)packet.Get("attackunit");
            UnitEntity attackunit = null;
            if(attackid != 0)
                attackunit = Engine.FindObjectAs<UnitEntity>(attackid);
            
            UnitEntity moveunit = Engine.FindObjectAs<UnitEntity>((ulong)packet.Get("moveunit"));
            var positions = (Point[])packet.Get("path");
            var tilepos = positions.Select(p => new TilePosition(p));
            Path<TileWorld, TilePosition> movePath = new Path<TileWorld, TilePosition>((TileWorld)this.Engine.World, tilepos);
            DeclareMoveAttackCommand pcom;
            if (attackid != 0)
                pcom = new DeclareMoveAttackCommand(player, moveunit, movePath, attackunit);
            else
                pcom = new DeclareMoveAttackCommand(player, moveunit, movePath);
            return pcom;
        }
    }
}
