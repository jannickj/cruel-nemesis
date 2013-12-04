using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cruel.GameLogic.SpellSystem;
using JSLibrary;
using CruelTest.TestComponents;
using Cruel.GameLogic;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class GameLibraryTest : EngineTest
    {
        private GameLibrary ConstructLibrary()
        {
            GameLibrary lib = new GameLibrary();
            Hand h = new Hand();
            Player p = new Player(lib, h, null,null);
            Engine.AddActor(lib);
            return lib;
        }

        [TestMethod]
        public void DrawCard_NonemptyLibrary_CardReturned()
        {
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            for (int i = 0; i < 29; i++)
                cards.AddFirst(new MockCard());
            cards.AddFirst(new MockCardWithData(42));
            GameLibrary lib = ConstructLibrary();
            lib.Add(cards);
            GameCard card = lib.Draw();
            Assert.IsTrue(((MockCardWithData)card).data == 42);
        }

        [TestMethod]
        public void DrawMultipleCards_NonemptyLibrary_CardsReturned()
        {
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            for (int i = 0; i < 28; i++)
                cards.AddFirst(new MockCard());
            cards.AddFirst(new MockCardWithData(42));
            cards.AddFirst(new MockCardWithData(42));
            GameLibrary lib = ConstructLibrary();
            lib.Add(cards);
            List<GameCard> returnedCards = lib.Draw(2);
            foreach (GameCard card in returnedCards)
                Assert.IsTrue(((MockCardWithData)card).data == 42);
        }

        [TestMethod]
        public void ShuffleLibrary_OrderedLibrary_LibraryOrderReversed()
        {
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            for (int i = 0; i < 10; i++)
                cards.AddLast(new MockCardWithData(i + 1));
            GameLibrary lib = new GameLibrary(); 
            lib.Add(cards);
            lib.Shuffle(_ => 0);
            List<GameCard> newLib = lib.TakeCards(10);
            for (int i = 0; i < newLib.Count; i++)
            {
                Assert.IsTrue(((MockCardWithData)newLib[i]).data == 10 - i);
            }
        }

        [TestMethod]
        public void PutCardOnBottom_OrderedLibrary_CardOnBottom()
        {
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            for (int i = 0; i < 29; i++)
                cards.AddFirst(new MockCard());
            GameLibrary lib = new GameLibrary();
            lib.Add(cards);
            lib.AddBottom(new MockCardWithData(42));
            lib.TakeCards(29);
            Assert.IsTrue(((MockCardWithData)lib.TakeCards(1)[0]).data == 42);
        }

        [TestMethod]
        public void PutCardAtPosition_NonemptyLibrary_CardAtPosition()
        {
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            for (int i = 0; i < 29; i++)
                cards.AddFirst(new MockCard());
            GameLibrary lib = new GameLibrary();
            lib.Add(cards);
            lib.AddAt(new MockCardWithData(42), 10);
            lib.TakeCards(9);
            Assert.IsTrue(((MockCardWithData)lib.TakeCards(1)[0]).data == 42);
        }
    }
}
