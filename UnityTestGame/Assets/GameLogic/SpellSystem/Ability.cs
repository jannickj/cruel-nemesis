using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using UnityEngine;
using XmasEngineModel;
using JSLibrary.Data;
using Assets.GameLogic.Events;
using XmasEngineModel.EntityLib;

namespace Assets.GameLogic.SpellSystem
{
	public abstract class Ability : EnvironmentAction
	{
        private Dictionary<int, XmasObject[]> targetList = new Dictionary<int,XmasObject[]>();
        private Dictionary<int, Predicate<XmasObject>> conditionList = new Dictionary<int,Predicate<XmasObject>>();
        private bool targetsRemaining = true;

        public void SetTarget(int index, XmasObject[] xObj)
        {
            targetList[index] = xObj;

            XmasEntity[] ent = xObj.OfType<XmasEntity>().ToArray();
            
            foreach (XmasEntity o in ent)
            {
                o.Register(new Trigger<RemovedFromGameEvent>(_ => removeTarget(o,index)));
            }
        }

        protected abstract void FireAbility();

        public void SetTargetCondition(int index, Predicate<XmasObject> pred)
        {
            conditionList[index] = pred;
        }

        protected override void Execute()
        {  
            if (targetsRemaining && conditionsTrue())
                FireAbility();
            else
                this.EventManager.Raise(new AbilityTargetInvalidEvent());
        }

        private bool conditionsTrue()
        {
            for (int i = 0; i < conditionList.Count; i++)
            {
                foreach (XmasObject target in targetList[i])
                {
                    if (!conditionList[i](target))
                        return false;
                }

            }
            return true;
        }

        private void removeTarget(XmasObject o, int index)
        {
            targetList[index] = targetList[index].Where(xO => xO != o).ToArray();
            if (targetList[index].Length == 0)
                targetsRemaining = false;
        }
    }
}
