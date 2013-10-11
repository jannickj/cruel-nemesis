using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using UnityEngine;
using XmasEngineModel;
using JSLibrary.Data;

namespace Assets.GameLogic.SpellSystem
{
	public abstract class Ability : EnvironmentAction
	{
        private Dictionary<int, XmasObject[]> targetList;

        public void SetTarget(int p, XmasObject[] xObj)
        {
            targetList[p] = xObj;
        }

        public abstract void FireAbility();

        public void SetTargetCondition(int index, Predicate<XmasObject> pred)
        {

        }

        protected override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
