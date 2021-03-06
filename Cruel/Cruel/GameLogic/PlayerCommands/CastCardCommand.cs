﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic.Exceptions;

namespace Cruel.GameLogic.PlayerCommands
{
    public class CastCardCommand : EnvironmentAction
    {
        private Player castingPlayer;
        private GameCard card;
        private IEnumerable<IEnumerable<object>> targets;
        private Mana[] selectedMana;
        public Spell CastedSpell { get; private set; }

        public Mana[] SelectedMana
        {
            get { return selectedMana; }
        }

        public Player CastingPlayer
        {
            get { return castingPlayer; }
        }

        public CastCardCommand(Player castingPlayer, GameCard card, IEnumerable<Mana> selectedMana)
            : this(castingPlayer, card, new IEnumerable<object>[0], selectedMana)
        {

        }

        public CastCardCommand(Player castingPlayer, GameCard card, IEnumerable<IEnumerable<object>> targets, IEnumerable<Mana> selectedMana)
        {
            this.castingPlayer = castingPlayer;
            this.card = card;
            this.targets = targets;
            this.selectedMana = selectedMana.ToArray();
        }

        protected override void Execute()
        {
            
            var spell = card.ConstructSpell();
            this.CastedSpell = spell;
            int index = 0;
            if (IllegalManaUsed())
                throw new ManaMismatchException(card, castingPlayer, selectedMana);
            else
            {
                if(selectedMana.Length > 0)
                    this.castingPlayer.ManaStorage.Spend(selectedMana);
                
                foreach (IEnumerable<object> targetList in targets)
                {
                    spell.SetTarget(index, targetList.ToArray());
                    index++;
                }
                this.EventManager.Raise(new EnqueueAbilityEvent(spell));
                this.RunAction(new ResetPrioritiesAction());
                
                Trigger<DequeueAbilityEvent> trig = null;
                Action<DequeueAbilityEvent> action = evt => 
                    {
                        this.card.Owner.Library.AddBottom(this.CastedCard);
                        this.EventManager.Deregister(trig);
                    };
                trig = new Trigger<DequeueAbilityEvent>(evt => evt.Ability == spell,action);
                this.EventManager.Register(trig);
            }
        }


        public GameCard CastedCard
        {
            get { return card; }
        }


        public IEnumerable<object>[] Targets
        {
            get { return targets.ToArray(); }
        }

        public bool IllegalManaUsed()
        {
            return selectedMana.Except(card.ManaCost).Count() != 0;
        }
    }
}
