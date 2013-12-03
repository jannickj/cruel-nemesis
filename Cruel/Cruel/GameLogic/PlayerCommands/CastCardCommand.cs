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
        private IEnumerable<IEnumerable<object>> targets;
        public Spell CastedSpell { get; private set; }

        public CastCardCommand(Player castingPlayer, GameCard card)
            : this(castingPlayer, card, new IEnumerable<object>[0])
        {

        }

        public CastCardCommand(Player castingPlayer, GameCard card, IEnumerable<IEnumerable<object>> targets)
        {
            this.castingPlayer = castingPlayer;
            this.card = card;
            this.targets = targets;
        }

        protected override void Execute()
        {
            var spell = card.ConstructSpell();
            this.CastedSpell = spell;
            int index = 0;
            foreach (IEnumerable<object> tars in targets)
            {
                spell.SetTarget(index, tars.ToArray());
                index++;
            }
            this.EventManager.Raise(new EnqueueAbilityEvent(spell));
            this.RunAction(new ResetPrioritiesAction());
        }


        public GameCard CastedCard
        {
            get { return card; }
        }


        public IEnumerable<object>[] Targets
        {
            get { return targets.ToArray(); }
        }
    }
}
