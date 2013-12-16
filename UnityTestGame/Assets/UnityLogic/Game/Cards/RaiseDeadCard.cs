using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Modules;
using Assets.UnityLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Cruel.GameLogic.Actions;
using XmasEngineModel.Management.Actions;
using Assets.UnityLogic.Game.Units;

namespace Assets.UnityLogic.Game.Cards
{
	public class RaiseDeadCard : GameCardWithSpellEffect
	{
        public RaiseDeadCard()
        {
            this.RegisterModule(new GraphicsModule("raise_dead"));
            this.SetTargetCondition(0, obj => obj is TerrainEntity);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Arcane, Mana.Arcane });
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
                new Point(0,0),
                new Point(0,1),
                new Point(0,-1),
                new Point(1,0),
                new Point(-1,0)
            };
            TilePosition[] tileArea = area.Select(pos => new TilePosition(targetPos.Point + pos)).ToArray();
            foreach (TilePosition tile in tileArea)
            {
                var targets = this.World.GetEntities(tile).OfType<UnitEntity>();
                foreach (UnitEntity target in targets)
                    if (target.Module<HealthModule>().IsDead())
                    {
                        if (targets.Count() == 1)
                        {
                            var unit = new ZombieUnit(this.Owner);
                            spell.RunAction(new AddEntityAction(unit, tile.GenerateSpawn()));
                        }
                        spell.RunAction(new RemoveEntityAction(target));
                    }
            }
            
        }
	}
}
