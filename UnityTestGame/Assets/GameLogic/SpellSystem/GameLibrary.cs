using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary;
using XmasEngineModel.EntityLib;
using Assets.GameLogic.Events.UnitEvents;

namespace Assets.GameLogic.SpellSystem
{
	public class GameLibrary : XmasUniversal
	{
        private SelectableLinkedList<GameCard> library = new SelectableLinkedList<GameCard>();
        private Player owner;

        public GameLibrary(Player p)
        {
            this.owner = p;
        }

        public void Add(IEnumerable<GameCard> cards)
        {
            foreach (GameCard gc in cards)
            {
                library.AddLast(gc);
            }
        }

        public GameCard Draw()
        {
            this.EventManager.Raise(new CardDrawnEvent(owner));
            return TakeCards(1)[0];
        }

        public List<GameCard> Draw(int count)
        {
            List<GameCard> cards = new List<GameCard>();
            int c = count;
            while (library.Count > 0 && c > 0)
            {
                c--;
                cards.Add(Draw());
            }
            return cards;
        }

        public List<GameCard> TakeCards(int count)
        {
            List<GameCard> cards = new List<GameCard>();
            int c = count;
            while (library.Count > 0 && c > 0)
            {
                c--;
                cards.Add(library.First());
                library.RemoveFirst();
            }
            return cards;
        }

        public void Shuffle()
        {
            Random rng = new Random();
            Shuffle(rng.Next);
        }

        public void Shuffle(Func<int, int> rngNext)
        {
            int n = library.Count;
            List<GameCard> list = new List<GameCard>(library);
            library = new SelectableLinkedList<GameCard>();
            while (n > 0)
            {
                n--;
                int k = rngNext(n + 1);
                library.AddFirst(list[k]);
                list.RemoveAt(k);
            } 
        }

        public void AddBottom(GameCard gc)
        {
            library.AddLast(gc);
        }

        public void AddAt(GameCard gc, int p)
        {
            int pos = p % library.Count;
            library.AddBefore(library.ElementAt(p - 1), gc);
        }
    }
}