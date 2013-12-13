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
using Cruel.GameLogic.Unit;

namespace Cruel.GameLogic
{
	public class Player : XmasUniversal
	{
        private string name;
        public GameLibrary Library { get; private set; }
        public Hand Hand { get; private set; }
        public ManaStorage ManaStorage { get; private set; }
        public int CurrentXP { get; private set;}
        public LevelRewarder Rewarder{ get; private set;}
        public HeroUnit Hero { get; internal set; }

        public Player() : this(null, null, null,null) { }

        public Player(GameLibrary lib, Hand hand, ManaStorage manaStorage,LevelRewarder rewarder)
        {
            this.CurrentXP = 0;
            this.Rewarder = rewarder;
            if (this.Rewarder != null)
                Rewarder.Owner = this;
            Library = lib;
            if(lib!=null)
                Library.Owner = this;
            Hand = hand;
            ManaStorage = manaStorage;
            if(manaStorage != null)
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
            if (this.Rewarder != null)
                this.ActionManager.Queue(new AddXmasObjectAction(this.Rewarder));
        }

        public void AddXP(int xp)
        {
            int gainedxp = xp > 0 ? xp : 0;
            if (gainedxp == 0)
                return;
            this.CurrentXP += gainedxp;
            this.Raise(new PlayerGainedXPEvent(this, gainedxp));
        }
    }
}
