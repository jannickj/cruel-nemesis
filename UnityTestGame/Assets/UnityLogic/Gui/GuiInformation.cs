using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using UnityEngine;
using Cruel.GameLogic.TurnLogic;
using Cruel.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GuiInformation : MonoBehaviour
	{
        private GUITexture[] phases;
        private Color[] defaultPhaseColors;
        private GUITexture[] manacrystals;
        private Dictionary<Player, GUITexture[]> skipPhaseButtons = new Dictionary<Player, GUITexture[]>();
        
        public Color FocusColor { get; set; }

        public Player Player { get; set; }

        public GUITexture XPBar { get; set; }
        public GUITexture XPButton { get; set; }

        public GUITexture Portrait { get; set; }

        public GUITexture HealthBar { get; set; }

        private GUITexture this[Phases phase]
        {
            get
            {
                return this.phases[(int)phase];
            }
        }

        public GUITexture this[ManaType manatype]
        {
            get
            {
                return this.manacrystals[(int)manatype];
            }
        }

        public void SetPhaseColor(Phases phase, Color color)
        {
            //Debug.Log("GUI VIEW PHASE CHANGED: " + phase+" to color "+color);
            this[phase].color = color;
        }

        public void ResetPhaseColor(Phases phase)
        {
            this[phase].color = defaultPhaseColors[(int)phase];
        }

        public void SetPhasesGui(GUITexture[] Phases)
        {
            this.phases = Phases;
            this.defaultPhaseColors = phases.Select(ph => ph.color).ToArray();
        }

        public void SetManaCrystalTypes(GUITexture[] ManaCrystalTypes)
        {
            this.manacrystals = ManaCrystalTypes;
        }

        public GUITexture GetSkipPhaseButton(Player p, Phases phase)
        {
            return this.skipPhaseButtons[p][(int)phase];
        }


        public void SetSkipPhaseButton(Player p, GUITexture[] buttons)
        {
            this.skipPhaseButtons[p] = buttons.ToArray();
        }

    }
}
