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
        private DictionaryList<Mana, ManaCrystal> manaCrystals = new DictionaryList<Mana, ManaCrystal>();
        private Player owner;

        public Player Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected override void OnAddedToEngine()
        {
            //this.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => );
        }

        public void AddCrystal(Cruel.GameLogic.SpellSystem.Mana mana)
        {
            throw new NotImplementedException();
        }

        public bool IsCharged(Mana mana, int p)
        {
            throw new NotImplementedException();
        }
    }
}
