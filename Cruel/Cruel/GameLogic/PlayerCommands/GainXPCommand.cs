using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using CruelTest.SpellSystem;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.PlayerCommands
{
    public class GainXPCommand : PlayerCommand
    {
        public int XP { get; private set; }
        public Mana SelectedMana { get; private set; }

        public GainXPCommand(Player player, int Xp, Mana selectedMana) : base(player)
        {
            this.XP = Xp;
            this.SelectedMana = selectedMana;
        }

        protected override void Execute()
        {
            this.Player.ManaStorage.Spend(SelectedMana);
            this.Player.AddXP(XP);
        }
    }
}
