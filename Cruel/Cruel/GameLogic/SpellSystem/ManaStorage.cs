using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary.Data;
using Cruel.GameLogic.SpellSystem;
using XmasEngineModel.EntityLib;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic;
using Cruel.GameLogic.PlayerCommands;
using XmasEngineModel.Management.Events;
using Cruel.GameLogic.Exceptions;

namespace CruelTest.SpellSystem
{
    public class ManaStorage : XmasUniversal
    {
        private Dictionary<Mana, List<ManaCrystal>> manaCrystals = new Dictionary<Mana, List<ManaCrystal>>();
        private Player owner;

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected override void OnAddedToEngine()
        {
            this.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(OnTurnChanged));
            this.EventManager.Register(new Trigger<ActionCompletedEvent<CastCardCommand>>(OnCardCast));
        }

        public void AddCrystal(Cruel.GameLogic.SpellSystem.Mana mana)
        {
            if (!manaCrystals.ContainsKey(mana))
                manaCrystals.Add(mana, new List<ManaCrystal>());
            manaCrystals[mana].Add(new ManaCrystal(mana));
            this.EventManager.Raise(new ManaCrystalAddedEvent(owner, mana, this));  
        }

        public bool IsCharged(Mana mana, int p)
        {
            return manaCrystals[mana][p].IsCharged;
        }

        public int Size(Mana mana)
        {
            if (manaCrystals.ContainsKey(mana))
                return manaCrystals[mana].Count();
            return 0;
        }

        private void OnTurnChanged(PlayersTurnChangedEvent evt)
        {
            if (evt.PlayersTurn == owner)
                chargeAll();
        }

        private void OnCardCast(ActionCompletedEvent<CastCardCommand> evt)
        {
            if (evt.Action.CastingPlayer == owner)
            {
                foreach (Mana m in evt.Action.SelectedMana)
                {
                    SpendOne(m);
                }
            }                
        }

        public void chargeAll()
        {
            foreach (List<ManaCrystal> l in manaCrystals.Values)
                foreach (ManaCrystal m in l)
                    m.Charge();
            this.EventManager.Raise(new ManaRechargedEvent(owner));
        }

        private void SpendOne(Mana m)
        {
            int index = manaCrystals[m].Count() - 1;
            while (!(manaCrystals[m][index].IsCharged))
            {
                index--;
                if (index < 0)
                    throw new ManaUnavailableException(owner, m);
            }
            manaCrystals[m][index].Spend();
            this.EventManager.Raise(new ManaCrystalSpentEvent(owner, m, this));
        }
    }
}
