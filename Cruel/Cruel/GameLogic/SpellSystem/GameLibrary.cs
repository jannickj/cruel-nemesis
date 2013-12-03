using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary;
using XmasEngineModel.EntityLib;
using Cruel.GameLogic.Events.UnitEvents;
using Cruel.GameLogic.Events;

namespace Cruel.GameLogic.SpellSystem
{
	public class GameLibrary : CardCollection
	{
        public Player Owner {get; internal set;}

        public GameLibrary() {}

        public GameCard Draw()
        {
            var card = TakeCards(1)[0];
            
            return card;
        }

        public List<GameCard> Draw(int count)
        {
            List<GameCard> cards = new List<GameCard>();
            int c = count;
            while (this.cards.Count > 0 && c > 0)
            {
                c--;
                cards.Add(Draw());
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
            int n = this.cards.Count;
            List<GameCard> list = new List<GameCard>(this.cards);
            this.cards = new SelectableLinkedList<GameCard>();
            while (n > 0)
            {
                n--;
                int k = rngNext(n + 1);
                this.cards.AddFirst(list[k]);
                list.RemoveAt(k);
            } 
        }

        public void AddBottom(GameCard gc)
        {
            this.cards.AddLast(gc);
        }

        public void AddAt(GameCard gc, int p)
        {
            int pos = p % this.cards.Count;
            this.cards.AddBefore(this.cards.ElementAt(p - 1), gc);
        }

        public override void Add(IEnumerable<GameCard> cards)
        {
            base.Add(cards);
            foreach (GameCard card in cards)
                card.Owner = this.Owner;
        }
    }
}