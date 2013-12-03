using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Events;
using CruelTest.SpellSystem;
using XmasEngineModel.Management.Actions;

namespace Cruel.GameLogic
{
	public class Player : XmasUniversal
	{
        private string name;
        public GameLibrary Library { get; private set; }
        public Hand Hand { get; private set; }
        public ManaStorage ManaStorage { get; private set; }

        public Player() : this(null, null, null) { }

        public Player(GameLibrary lib, Hand hand, ManaStorage manaStorage)
        {
            Library = lib;
            if(lib!=null)
                Library.Owner = this;
            Hand = hand;
            ManaStorage = manaStorage;
            ManaStorage.Owner = this;
        }

        public void Draw(int number)
        {
            var cards = Library.Draw(number);
            foreach (GameCard card in cards)
            {
                this.EventManager.Raise(new CardDrawnEvent(this, card));
                Hand.Add(new GameCard[]{card});
            }
            
        }

        public void Discard(int index)
        {
            this.EventManager.Raise(new CardDiscardedEvent(this));
            Library.AddBottom(Hand.TakeCardAt(index));
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            return "player("+name+")";
        }

        public bool HasPriority { get; internal set; }

        protected override void OnAddedToEngine()
        {
            if(ManaStorage != null)
                this.ActionManager.Queue(new AddXmasObjectAction(this.ManaStorage));
        }
    }
}
