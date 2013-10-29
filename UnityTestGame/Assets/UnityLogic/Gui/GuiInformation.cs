using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic;
using UnityEngine;
using Assets.GameLogic.TurnLogic;
using Assets.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GuiInformation : MonoBehaviour
	{
        private GUITexture[] phases;
        private GUITexture[] manacrystals;

        public Color FocusColor { get; set; }

        public Player Player { get; set; }

        public GUITexture Portrait { get; set; }

        public GUITexture HealthBar { get; set; }

        public GUITexture this[Phases phase]
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



        public void SetPhasesGui(GUITexture[] Phases)
        {
            this.phases = Phases;
        }

        public void SetManaCrystalTypes(GUITexture[] ManaCrystalTypes)
        {
            this.manacrystals = ManaCrystalTypes;
        }

        internal void TogglePhase(Phases phase)
        {
            throw new NotImplementedException();
        }
    }
}
