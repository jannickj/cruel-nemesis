using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Exceptions
{
    public class ManaMismatchException : Exception
    {
        public GameCard card { get; set; }
        public Player player { get; set; }
        public List<Mana> mana { get; set; }
        public ManaMismatchException(GameCard card, Player player, List<Mana> mana) : base("Error: " + player + " attempted to cast " + card + " with " + mana + ".")
        {
            this.card = card;
            this.player = player;
            this.mana = mana;
        }
    }
}
