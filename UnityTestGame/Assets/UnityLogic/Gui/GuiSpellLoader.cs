using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using Cruel.GameLogic.PlayerCommands;
using Cruel.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GuiSpellLoader : MonoBehaviour
	{
        public EngineHandler EngineHandler;
        public UnityFactory Factory;

        private XmasModel engine;

        public GuiSpellLoader()
        {
            
        }

        public void Start()
        {
            this.engine = EngineHandler.EngineModel;
            this.engine.EventManager.Register(new Trigger<ActionCompletedEvent<CastCardCommand>>(OnCastCard));
        }

        private void OnCastCard(ActionCompletedEvent<CastCardCommand> evt)
        {
            GameCard card = evt.Action.CastedCard;
            GameObject gobj = (GameObject)this.Factory.GameObjectFromModel(card);
            //gobj.c
        }
	}
}
