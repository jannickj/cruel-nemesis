using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Actions;

namespace Cruel.GameLogic.PlayerCommands
{
    public class CastCardCommand : EnvironmentAction
    {
        private Player castingPlayer;
        private GameCard card;

        public CastCardCommand(Player castingPlayer, GameCard card)
        {
            this.castingPlayer = castingPlayer;
            this.card = card;
        }

        protected override void Execute()
        {
            this.EventManager.Raise(new EnqueueAbilityEvent(card.ConstructSpell()));
            this.RunAction(new ResetPrioritiesAction());
        }
    }
}
