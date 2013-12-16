using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityLogic.Game.Modules;
using Cruel.Map.Terrain;
using Cruel.GameLogic.SpellSystem;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Modules;

namespace Assets.UnityLogic.Game.Cards
{
	public class BattlecryCard : GameCardWithSpellEffect
	{
        public BattlecryCard()
        {
            this.RegisterModule(new GraphicsModule("battlecry"));
            this.SetTargetCondition(0, _ => true);
            this.TargetCounts = new int[] { 1 };
            this.ManaCost = new List<Mana>(new Mana[] { Mana.Fury, Mana.Fury, Mana.Fury });
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
                new Point(-1,0)
            };
            TilePosition[] tileArea = area.Select(pos => new TilePosition(targetPos.Point + pos)).ToArray();
            foreach (TilePosition tile in tileArea)
            {
                var targets = this.World.GetEntities(tile).OfType<UnitEntity>();
                foreach (UnitEntity target in targets)
                    target.Module<AttackModule>().Damage += 2;
            }
            
        }
	}
}
