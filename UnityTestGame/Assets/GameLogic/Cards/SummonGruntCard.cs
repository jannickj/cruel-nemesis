using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic.SpellSystem;
using Assets.Map.Terrain;
using Assets.GameLogic.Unit;
using Assets.GameLogic.Modules;

namespace Assets.GameLogic.Cards
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
