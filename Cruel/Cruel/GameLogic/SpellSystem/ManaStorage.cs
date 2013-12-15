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
            //this.EventManager.Register(new Trigger<ActionCompletedEvent<CastCardCommand>>(OnCardCast));
        }

        public int GetChargedCount(Mana mana)
        {
            if (!manaCrystals.ContainsKey(mana))
                return 0;
            int count = 0;
            foreach (ManaCrystal m in manaCrystals[mana])
                if (m.IsCharged)
                    count++;
            return count;
        }

        public bool CheckMana(IEnumerable<Mana> mana)
        {
            int i;
            foreach (Mana type in Enum.GetValues(typeof(Mana)))
            {
                i = 0;
                foreach (Mana m in mana)
                    if (m == type)
                        i++;
                if (!manaCrystals.ContainsKey(type) || GetChargedCount(type) < i)
                    return false;
            }
            return true;
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

        public IEnumerable<Mana> getAllTypes()
        {
            return manaCrystals.Keys;
        }

        private void OnTurnChanged(PlayersTurnChangedEvent evt)
        {
            if (evt.PlayersTurn == owner)
                chargeAll();
        }

        //private void OnCardCast(ActionCompletedEvent<CastCardCommand> evt)
        //{
        //    if (evt.Action.CastingPlayer == owner)
        //    {
        //        foreach (Mana m in evt.Action.SelectedMana)
        //        {
        //            Spend(m);
        //        }
        //    }                
        //}

        public void chargeAll()
        {
            foreach (List<ManaCrystal> l in manaCrystals.Values)
                foreach (ManaCrystal m in l)
                    m.Charge();
            this.EventManager.Raise(new ManaRechargedEvent(owner));
        }

        public void Spend(IEnumerable<Mana> manas)
        {
            foreach (var m in manas)
                Spend(m);

        }

        public void Spend(Mana m)
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
