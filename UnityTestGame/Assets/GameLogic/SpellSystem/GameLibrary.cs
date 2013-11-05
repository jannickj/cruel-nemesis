using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameLogic.SpellSystem
{
	public class GameLibrary : object
	{
        private JSLibrary.SelectableLinkedList<GameCard> library;

        public GameLibrary(JSLibrary.SelectableLinkedList<GameCard> cards)
        {
            this.library = cards;
        }

        public GameCard draw()
        {
            GameCard card = library.First();
            library.RemoveFirst();
            return card;
        }

        public List<GameCard> draw(int count)
        {
            List<GameCard> cards = new List<GameCard>();
            for (int i = 0; i < count; i++)
            {
                cards.Add(library.First());
                library.RemoveFirst();
            }
            return cards;
        }
    }
}
