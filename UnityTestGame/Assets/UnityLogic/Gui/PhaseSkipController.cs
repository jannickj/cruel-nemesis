using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic;
using Assets.GameLogic.TurnLogic;
using JSLibrary.Data;
using UnityEngine;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using Assets.GameLogic.Actions;

namespace Assets.UnityLogic.Gui
{
	public class PhaseSkipController
	{
        private GuiInformation guiinfo;
        private Player owner;
        private DictionaryList<Player, Phases> autoSkip = new DictionaryList<Player, Phases>();

        private Player lastGainPrio;
        private int samePriority;
        private Player currentPlayersTurn;
        private Phases curPhase;

        public PhaseSkipController(GuiInformation guiinfo, Player owner)
        {
            // TODO: Complete member initialization
            this.guiinfo = guiinfo;
            this.owner = owner;
            owner.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => currentPlayersTurn = evt.PlayersTurn));
            owner.EventManager.Register(new Trigger<PhaseChangedEvent>(evt => { lastGainPrio = null; samePriority = 0; curPhase = evt.NewPhase; }));
            owner.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(this.OnPlayerGainPriority));
            
        }

        public void SetPhaseSkip(Player onPsTurn, Phases skipPhase, bool toggle)
        {
            if (toggle)
                autoSkip.Add(onPsTurn, skipPhase);
            else
                autoSkip.Remove(onPsTurn, skipPhase);

            setSkipOnView(onPsTurn, skipPhase, toggle);
        }

        private void setSkipOnView(Player player, Phases phase, bool toggle)
        {

            GUITexture button = this.guiinfo.GetSkipPhaseButton(player, phase);
            Color color = guiinfo.FocusColor;
            if (toggle)
                button.renderer.material.color = color;
            else
                button.renderer.material.color = Color.white;
        }

        private void OnPlayerGainPriority(PlayerGainedPriorityEvent evt)
        {
            if (lastGainPrio == evt.Player)
                samePriority++;
            if (evt.Player == owner)
            {
                if (samePriority == 0)
                {
                    ICollection<Phases> phases;
                    //this.autoSkip.TryGetValues(
                    if (this.autoSkip[this.currentPlayersTurn].Contains(this.curPhase))
                    {
                        owner.ActionManager.Queue(new PlayerPassPriorityAction(owner));
                    }
                    else
                        redrawSkips();

                }
            }
        }

        private void redrawSkips()
        {

        }
	}
}
