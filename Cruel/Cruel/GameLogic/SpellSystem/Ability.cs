using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using XmasEngineModel;
using JSLibrary.Data;
using Cruel.GameLogic.Events;
using XmasEngineModel.EntityLib;

namespace Cruel.GameLogic.SpellSystem
{
	public abstract class Ability : EnvironmentAction
	{
        public bool PreventResolving { get; set; }
        private Dictionary<int, object[]> targetList = new Dictionary<int, object[]>();
        private Dictionary<int, Predicate<object>> conditionList = new Dictionary<int, Predicate<object>>();
        private bool targetsRemaining = true;
        public Player Controller { get; private set; }

        public Ability(Player controller)
        {
            this.Controller = controller;
        }

        public object[][] Targets
        {
            get
            {
                int count = targetList.Keys.Max() + 1;
                object[][] targets = new object[count][];
                for (int i = 0; i < count; i++)
                {
                    object[] tars;
                    if (targetList.TryGetValue(i, out tars))
                    {
                        targets[i] = tars;
                    }
                    else
                        targets[i] = new object[0];
                    
                }
                return targets;
            }
        }

        public void SetTarget(int index, object[] xObj)
        {
            targetList[index] = xObj;
            
            XmasEntity[] ent = xObj.OfType<XmasEntity>().ToArray();
            
            foreach (XmasEntity o in ent)
            {
                o.Register(new Trigger<RemovedFromGameEvent>(_ => removeTarget(o,index)));
            }
        }

        protected abstract void FireAbility();

        public void SetTargetCondition(int index, Predicate<object> pred)
        {
            conditionList[index] = pred;
        }

        protected override void Execute()
        {
            if (targetsRemaining && conditionsTrue())
            {
                FireAbility();
                this.EventManager.Raise(new AbilityResolvesEvent());
            }
            else
                this.EventManager.Raise(new AbilityTargetInvalidEvent());
        }

        private bool conditionsTrue()
        {
            for (int i = 0; i < conditionList.Count; i++)
            {
                foreach (object target in targetList[i])
                {
                    if (!conditionList[i](target))
                        return false;
                }

            }
            return true;
        }

        private void removeTarget(object o, int index)
        {
            targetList[index] = targetList[index].Where(xO => xO != o).ToArray();
            if (targetList[index].Length == 0)
                targetsRemaining = false;
        }

        /// <summary>
        /// Get the targets of a given index
        /// </summary>
        /// <typeparam name="TUniversal">Target type as</typeparam>
        /// <param name="i">The index of the targets</param>
        /// <returns></returns>
        public TObject[] GetTargetAs<TObject>(int i)
        {
            return targetList[i].OfType<TObject>().ToArray();
        }
    }
}
