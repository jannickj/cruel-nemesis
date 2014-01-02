using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Actions;
using JSLibrary.Data;
using XmasEngineExtensions.TileExtension;
using CruelGameData.GameLogic.Game.Modules;

namespace CruelGameData.GameLogic.Game.Cards
{
	public class FireballCard : GameCardWithSpellEffect
	{
        public FireballCard()
        {
            this.RegisterModule(new GraphicsModule("fireball"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Arcane, Mana.Arcane, Mana.Arcane });
        }


        protected override void OnSpellEffect(Spell spell)
        {
            var targetPos = (TilePosition)((TerrainEntity)spell.Targets[0][0]).Position;

            Point[] area = new Point[]
            {                
                new Point(1,1),
                new Point(-1,-1),
                new Point(-1,1),
                new Point(1,-1),
                new Point(0,1),
                new Point(0,-1),
                new Point(1,0),
                new Point(-1,0),
                new Point(0,0)
            };
            TilePosition[] tileArea = area.Select(pos => new TilePosition(targetPos.Point + pos)).ToArray();
            foreach (TilePosition tile in tileArea)
            {
                var targets = this.World.GetEntities(tile).OfType<UnitEntity>();
                foreach(UnitEntity target in targets)
                    spell.RunAction(target,new DealDamageToUnitAction(target, 3));
            }
            
        }
	}
}
