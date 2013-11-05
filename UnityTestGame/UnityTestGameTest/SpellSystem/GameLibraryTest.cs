using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assets.GameLogic.SpellSystem;
using JSLibrary;
using UnityTestGameTest.TestComponents;

namespace UnityTestGameTest.SpellSystem
{
    [TestClass]
    public class GameLibraryTest
    {
        [TestMethod]
        public void DrawCard_NonemptyLibrary_CardReturned()
        {
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            for (int i = 0; i < 29; i++)
                cards.AddFirst(new MockCard());
            cards.AddFirst(new MockCardWithData(42));
            GameLibrary lib = new GameLibrary(cards);
            GameCard card = lib.draw();
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
            GameLibrary lib = new GameLibrary(cards);
            List<GameCard> returnedCards = lib.draw(2);
            foreach (GameCard card in returnedCards)
                Assert.IsTrue(((MockCardWithData)card).data == 42);
        }
    }
}
