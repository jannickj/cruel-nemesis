using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.EntityLib;
using Assets.GameLogic.SpellSystem;

namespace Assets.GameLogic
{
	public class Player : XmasUniversal
	{
        private string name;
        public GameLibrary Library { get; private set; }
        public Hand Hand { get; private set; }


        public Player(GameLibrary lib, Hand hand)
        {
            Library = lib;
            Library.Owner = this;
            Hand = hand;
        }

        public void Draw(int number)
        {
            Hand.Add(Library.Draw(number));
        }

        public void Discard(int index)
        {
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
	}
}
