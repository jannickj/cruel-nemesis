using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Unit;

namespace Assets.UnityLogic.Game
{
	public class GameFactory : XmasFactory
	{

        public GameFactory(ActionManager actionmanager) : base(actionmanager)
        {
        }

        public TCard CreateCard<TCard>() where TCard : GameCard
        {
            return null;
        }

        public TUnit CreateUnit<TUnit>() where TUnit : UnitEntity
        {
            return null;
        }
	}
}
