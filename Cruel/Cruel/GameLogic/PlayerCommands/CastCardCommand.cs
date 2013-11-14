using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.PlayerCommands
{
    public class CastCardCommand : EnvironmentAction
    {
        private Player castingPlayer;
        private GameCard card;

        public CastCardCommand(Player castingPlayer, GameCard card)
        {
            // TODO: Complete member initialization
            this.castingPlayer = castingPlayer;
            this.card = card;
        }

        protected override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
