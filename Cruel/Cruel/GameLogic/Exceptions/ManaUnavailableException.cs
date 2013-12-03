using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Exceptions
{
    public class ManaUnavailableException : Exception
    {
        public Player player { get; set; }
        public Mana mana { get; set; }
        public ManaUnavailableException(Player player, Mana mana)
            : base("Error: " + player + " attempted to discharge " + mana + ", which was not charged.")
        {
            this.player = player;
            this.mana = mana;
        }
    }
}
