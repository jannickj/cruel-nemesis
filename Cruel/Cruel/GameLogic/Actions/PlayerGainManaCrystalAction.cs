using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Actions
{
    public class PlayerGainManaCrystalAction : EnvironmentAction
    {
        private Player player;
        private Mana mana;

        public PlayerGainManaCrystalAction(Player p, Mana m)
        {
            player = p;
            mana = m;
        }

        protected override void Execute()
        {
            player.ManaStorage.AddCrystal(mana);
        }
    }
}
