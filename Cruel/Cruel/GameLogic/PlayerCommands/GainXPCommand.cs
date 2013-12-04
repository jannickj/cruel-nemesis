using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using CruelTest.SpellSystem;

namespace Cruel.GameLogic.PlayerCommands
{
    public class GainXPCommand : EnvironmentAction
    {
        public Player Player { get; private set; }
        public int XP { get; private set; }
        public ManaStorage Storage { get; private set; }

        public GainXPCommand(Player player, int Xp, ManaStorage storage)
        {
            this.Player = player;
            this.XP = Xp;
            this.Storage = storage;
        }
    }
}
