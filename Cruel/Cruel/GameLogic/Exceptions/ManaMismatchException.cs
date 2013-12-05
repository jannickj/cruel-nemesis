using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic.Exceptions
{
    public class ManaMismatchException : Exception
    {
        public GameCard card { get; private set; }
        public Player player { get; private set; }
        public Mana[] mana { get; private set; }
        public ManaMismatchException(GameCard card, Player player, IEnumerable<Mana> mana) : base("Error: " + player + " attempted to cast " + card + " with " + mana.ToArray() + ".")
        {
            this.card = card;
            this.player = player;
            this.mana = mana.ToArray();
        }
    }
}
