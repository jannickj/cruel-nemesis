using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using Cruel.Map.Terrain;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Modules;

namespace Cruel.GameLogic.Cards
{
	public class SummonGruntCard : GameCard
	{
        public SummonGruntCard()
        {
            this.AddSpellAction(OnCast);
        }

        private void OnCast(Spell spell)
        {
            TerrainEntity ter = spell.GetTargetAs<TerrainEntity>(0).First();
            var tpos = ter.Position;

            GruntUnit gunit = new GruntUnit(this.Module<UnitInfoModule>().Owner);

            this.World.AddEntity(gunit, tpos.GenerateSpawn());

        }

	}
}
