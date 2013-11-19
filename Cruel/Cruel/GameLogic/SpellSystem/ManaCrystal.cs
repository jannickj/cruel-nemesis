using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;

namespace Cruel.GameLogic.SpellSystem
{
    public class ManaCrystal : XmasUniversal
    {
        private Mana mana;
        private bool isCharged = false;

        public bool IsCharged
        {
            get { return isCharged; }
        }

        public ManaCrystal(Mana mana)
        {
            this.mana = mana;
        }

        public Mana Spend()
        {
            isCharged = false;
            return this.mana;
        }

        public void Charge()
        {
            isCharged = true;
        }
    }
}
