using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using Cruel.GameLogic.TurnLogic;
using JSLibrary.Data;
using UnityEngine;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Actions;

namespace Assets.UnityLogic.Gui
{
	public class PhaseSkipController
	{
        private GuiInformation guiinfo;
        private Player owner;
        private DictionaryList<Player, Phases> autoSkip = new DictionaryList<Player, Phases>();
        private Player[] players;
        private Player lastGainPrio;
        private int samePriority;
        private Player currentPlayersTurn;
        private Phases curPhase;

        public PhaseSkipController(GuiInformation guiinfo, Player owner, IEnumerable<Player> players)
        {
            // TODO: Complete member initialization
            this.guiinfo = guiinfo;
            this.owner = owner;
            this.players = players.ToArray();
            owner.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => currentPlayersTurn = evt.PlayersTurn ));
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

        public void TogglePhaseSkip(Player onPsTurn, Phases skipPhase)
        {
            ICollection<Phases> phases;
            if (autoSkip.TryGetValues(onPsTurn, out phases))
            {
                if (phases.Contains(skipPhase))
                {
                    SetPhaseSkip(onPsTurn, skipPhase, false);
                }
                else
                    SetPhaseSkip(onPsTurn, skipPhase, true);
            }
            else
                SetPhaseSkip(onPsTurn, skipPhase, true);
        }

        private void setSkipOnView(Player player, Phases phase, bool toggle)
        {

            GUITexture button = this.guiinfo.GetSkipPhaseButton(player, phase);
            Color color = guiinfo.FocusColor;
            if (toggle)
                button.color = color;
            else
                button.color = Color.white;
            
        }

        private void OnPlayerGainPriority(PlayerGainedPriorityEvent evt)
        {
            if (lastGainPrio == evt.Player)
                samePriority++;
            lastGainPrio = evt.Player;
            if (evt.Player == owner)
            {
                if (samePriority == 0)
                {
                    ICollection<Phases> phases;

                    if(this.autoSkip.TryGetValues(this.currentPlayersTurn,out phases) && phases.Contains(this.curPhase))
                    {
                        owner.ActionManager.Queue(new PlayerPassPriorityAction(owner));
                    }
                    else
                        redrawSkips();
                }
                else
                    redrawSkips();
            }
        }

        private void redrawSkips()
        {
            foreach (var player in this.players)
            {
                var skipPhases = new HashSet<Phases>(this.autoSkip.Get(player));
                var allphases = ((Phases[])Enum.GetValues(typeof(Phases))).Where(p => p != Phases.Attack && p != Phases.Move);

                var phaseSettings = allphases.Select(ph => new KeyValuePair<Phases, bool>(ph, skipPhases.Contains(ph)));
                foreach (var pSetting in phaseSettings)
                {
                    this.setSkipOnView(player, pSetting.Key, pSetting.Value);
                }
                
            }
        }
	}
}
