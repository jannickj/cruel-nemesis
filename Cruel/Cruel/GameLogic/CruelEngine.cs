using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using Cruel.GameLogic.TurnLogic;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic
{
    public class CruelEngine : XmasModel
    {
        private TurnManager turnManager;
        private AbilityManager abilityManager;

        public AbilityManager AbilityManager
        {
            get { return abilityManager; }
        }

        public TurnManager TurnManager
        {
            get { return turnManager; }
        }


        public CruelEngine(TurnLogic.TurnManager turnManager, SpellSystem.AbilityManager abilityManager, Map.GameMapBuilder mapbuilder, ActionManager actman, EventManager evtman, GameFactory factory)
            : base(mapbuilder, actman, evtman, factory)
        {
            // TODO: Complete member initialization
            this.turnManager = turnManager;
            this.AddActor(turnManager);
            this.abilityManager = abilityManager;
            this.AddActor(abilityManager);
        }
    }
}
