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
        }

        public void AddCrystal(Cruel.GameLogic.SpellSystem.Mana mana)
        {
            if (!manaCrystals.ContainsKey(mana))
                manaCrystals.Add(mana, new List<ManaCrystal>());
            manaCrystals[mana].Add(new ManaCrystal(mana));                
        }

        public bool IsCharged(Mana mana, int p)
        {
            return manaCrystals[mana][p].IsCharged;
        }

        private void OnTurnChanged(PlayersTurnChangedEvent evt)
        {
            if (evt.PlayersTurn == owner)
                foreach (List<ManaCrystal> l in manaCrystals.Values)
                    foreach (ManaCrystal m in l)
                        m.Charge();
        }
    }
}
