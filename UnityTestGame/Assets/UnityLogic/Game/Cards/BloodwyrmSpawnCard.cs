using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Game.Modules;

namespace Assets.UnityLogic.Game.Cards
{
	public class BloodwyrmSpawnCard : GameCard
	{
        public BloodwyrmSpawnCard()
        {
            this.RegisterModule(new GraphicsModule("bloodwyrm_spawn"));
        }
	}
}
