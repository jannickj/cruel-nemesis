using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using Assets.GameLogic;

namespace Assets.UnityLogic.Gui
{
	public class GuiViewHandler : MonoBehaviour
	{
        private GuiInformation guiinfo;
        public EngineHandler Engine;
        private XmasModel engmodel;
        private Player currentTurnOwner;

        void Start()
        {
            guiinfo = this.gameObject.GetComponent<GuiInformation>();

            engmodel = Engine.EngineModel;

            engmodel.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(OnPlayerPriority));
            engmodel.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(OnTurnChanged));
            engmodel.EventManager.Register(new Trigger<PhaseChangedEvent>(OnPhaseChanged));
        }


        void Update()
        {

        }


        public void OnPhaseChanged(PhaseChangedEvent evt)
        {
            if (currentTurnOwner != guiinfo.Player)
                return;
            guiinfo[evt.OldPhase].color = Color.white;
            guiinfo[evt.NewPhase].color = guiinfo.FocusColor;
        }

        public void OnTurnChanged(PlayersTurnChangedEvent evt)
        {            
            currentTurnOwner = evt.PlayersTurn;
        }

        public void OnPlayerPriority(PlayerGainedPriorityEvent evt)
        {
            if (evt.Player == guiinfo.Player)
                guiinfo.Portrait.color = guiinfo.FocusColor;
            else
                guiinfo.Portrait.color = Color.white;

        }
    }
}
