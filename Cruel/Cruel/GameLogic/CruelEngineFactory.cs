using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Map;
using XmasEngineModel.Management;
using Cruel.GameLogic.TurnLogic;
using Cruel.GameLogic.SpellSystem;

namespace Cruel.GameLogic
{
    public class CruelEngineFactory
    {
        public CruelEngine CreateEngine(GameMapBuilder mapbuilder)
        {
            TurnManager turnManager = new TurnManager();

            AbilityManager abilityManager = new AbilityManager();
            EventManager evtman = new EventManager();
            ActionManager actman = new ActionManager(evtman);
            GameFactory factory = new GameFactory(actman);
            CruelEngine engine = new CruelEngine(turnManager,abilityManager,mapbuilder, actman, evtman, factory);
            return engine;
        }
    }
}
