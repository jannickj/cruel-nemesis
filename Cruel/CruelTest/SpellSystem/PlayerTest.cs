using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cruel.GameLogic;
using Cruel.GameLogic.SpellSystem;
using JSLibrary;
using CruelTest.TestComponents;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;

namespace CruelTest.SpellSystem
{
    [TestClass]
    public class PlayerTest : EngineTest
    {
        [TestMethod]
        public void DrawACard_EmptyHandNonemptyLibrary_CardsInHand()
        {
            bool cardDrawnEventTriggered = false;

            GameLibrary lib = new GameLibrary();
            SelectableLinkedList<GameCard> cards = new SelectableLinkedList<GameCard>();
            cards.AddFirst(new MockCardWithData(42));
            lib.Add(cards);
            Hand h = new Hand();
            Player p = new Player(lib, h);
            p.EventManager = this.EventManager;
            this.EventManager.Register(new Trigger<CardDrawnEvent>(_ => cardDrawnEventTriggered = true));
            Engine.AddActor(p);
            p.Draw(1);
            GameCard c = p.Hand.TakeCards(1)[0];
            Assert.IsTrue(cardDrawnEventTriggered);
            Assert.IsTrue(lib.IsEmpty());
            Assert.IsTrue(((MockCardWithData)c).data == 42);
        }

        [TestMethod]
        public void Discard_CardInHand_CardOnBottomOfLibrary()
        {
            bool cardDiscardedEventTriggered = false;
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
            p.EventManager = this.EventManager;
            this.EventManager.Register(new Trigger<CardDiscardedEvent>(_ => cardDiscardedEventTriggered = true));
            Engine.AddActor(lib);
            p.Discard(0);
            GameCard c = p.Library.TakeCardAt(29);
            Assert.IsTrue(cardDiscardedEventTriggered);
            Assert.IsTrue(((MockCardWithData)c).data == 42);
        }
    }
}
