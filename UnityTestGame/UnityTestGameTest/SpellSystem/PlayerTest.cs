using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GameLogic;
using Assets.GameLogic.SpellSystem;
using JSLibrary;
using UnityTestGameTest.TestComponents;

namespace UnityTestGameTest.SpellSystem
{
    [TestClass]
    public class PlayerTest : EngineTest
    {
        [TestMethod]
        public void DrawACard_EmptyHandNonemptyLibrary_CardsInHand()
        {
            GameLibrary lib = new GameLibrary();
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            cards.AddFirst(new MockCardWithData(42));
            lib.Add(cards);
            Hand h = new Hand();
            Player p = new Player(lib, h);
            Engine.AddActor(lib);
            p.Draw(1);
            GameCard c = p.Hand.TakeCards(1)[0];
            Assert.IsTrue(lib.IsEmpty());
            Assert.IsTrue(((MockCardWithData)c).data == 42);
        }

        [TestMethod]
        public void Discard_CardInHand_CardOnBottomOfLibrary()
        {
            GameLibrary lib = new GameLibrary();
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            for (int i = 0; i < 29; i++)
                cards.AddFirst(new MockCard());
            lib.Add(cards);
            Hand h = new Hand();
            cards = new SelectableLinkedList<GameCard>();
            cards.AddFirst(new MockCardWithData(42));
            h.Add(cards);
            Player p = new Player(lib, h);
            Engine.AddActor(lib);
            p.Discard(0);
            GameCard c = p.Library.TakeCardAt(29);
            Assert.IsTrue(((MockCardWithData)c).data == 42);
        }
    }
}
