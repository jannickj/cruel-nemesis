using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.EntityLib;
using JSLibrary;
using XmasEngineModel.Management.Actions;

namespace Cruel.GameLogic.SpellSystem
{
	public class CardCollection : XmasUniversal
	{
        protected SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();

        public bool IsEmpty()
        {
            return cards.Count == 0;
        }

        public virtual void Add(IEnumerable<GameCard> cards)
        {
            foreach (GameCard gc in cards)
            {
                this.cards.AddLast(gc);
            }
        }

        public List<GameCard> TakeCards(int count)
        {
            List<GameCard> cards = new List<GameCard>();
            int c = count;
            while (this.cards.Count > 0 && c > 0)
            {
                c--;
                cards.Add(this.cards.First());
                this.cards.RemoveFirst();
            }
            return cards;
        }

        public GameCard TakeCardAt(int index)
        {
            GameCard card = this.cards.ElementAt(index);
            cards.Remove(card);
            return card;
        }

        public GameCard TakeRandom()
        {
            Random rng = new Random();
            return TakeCardAt(rng.Next(cards.Count));
        }

        protected override void OnAddedToEngine()
        {
            foreach (var card in this.cards)
            {
                this.ActionManager.Queue(new AddXmasObjectAction(card));
            }
        }
	}
}
