﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSLibrary;
using XmasEngineModel.EntityLib;
using Assets.GameLogic.Events.UnitEvents;

namespace Assets.GameLogic.SpellSystem
{
	public class GameLibrary : CardCollection
	{
        public Player Owner {get;set;}

        public GameLibrary() {}

        public GameCard Draw()
        {
            return TakeCards(1)[0];
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
    }
}