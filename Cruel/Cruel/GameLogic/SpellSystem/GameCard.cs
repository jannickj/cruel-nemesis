using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.EntityLib;

namespace Cruel.GameLogic.SpellSystem
{
	public class GameCard : XmasUniversal
	{
        private Dictionary<int, Predicate<object>> conditionList = new Dictionary<int, Predicate<object>>();
        private List<Action<Spell>> spells = new List<Action<Spell>>();
        private List<Mana> manaCost = new List<Mana>();

        public List<Mana> ManaCost
        {
            get { return manaCost; }
            set { manaCost = value; }
        }

        public int[] TargetCounts { get; protected set; }

        public GameCard()
        {
            TargetCounts = new int[0];
        }

        public bool TestTarget(int targetIndex, object target)
        {
            if(conditionList.ContainsKey(targetIndex))
                return conditionList[targetIndex](target);
            return true;
        }

        public Spell ConstructSpell()
        {
            Spell s = new Spell(spells);
            foreach (var cond in conditionList)
            {
                s.SetTargetCondition(cond.Key, cond.Value);
            }
            return s;
        }
        public void AddSpellAction(Action<Spell> spell)
        {
            spells.Add(spell);
        }

        public void SetTargetCondition(int index, Predicate<object> pred)
        {
            conditionList[index] = pred;
        }
	}
}
