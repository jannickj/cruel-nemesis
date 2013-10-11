using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using UnityEngine;
using XmasEngineModel;

namespace Assets.GameLogic.SpellSystem
{
	public abstract class Ability : EnvironmentAction
	{
        public void SetTarget(int p, XmasObject[] xObj)
        {
            throw new NotImplementedException();
        }

        public void FireAbility()
        {
            throw new NotImplementedException();
        }

        public void SetTargetCondition(int index, Predicate<XmasObject> pred)
        {

        }

        
    }
}
