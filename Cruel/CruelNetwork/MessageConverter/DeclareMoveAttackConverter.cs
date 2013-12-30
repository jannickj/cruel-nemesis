using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Conversion;
using XmasEngineModel;
using CruelNetwork.Packets;
using Cruel.GameLogic.PlayerCommands;

namespace CruelNetwork.PacketConverter
{


    public class DeclareMoveAttackConverter : JSConverterToForeign<DeclareMoveAttackCommand, DeclareMoveAttackMessage>
    {
        public override DeclareMoveAttackMessage BeginConversionToForeign(DeclareMoveAttackCommand gobj)
        {
            return new DeclareMoveAttackMessage() { Command = gobj };
        }
    }
}
