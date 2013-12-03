using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GUIManaViewHandler : MonoBehaviour
	{
        public UnityFactory Factory { get; set; }
        public GuiInformation GUIInfo;
        public XmasModel Engine;
        public bool reversed;
        private Dictionary<Mana, GUITexture> manaBars = new Dictionary<Mana, GUITexture>();

        public void Initialize(UnityFactory factory, GuiInformation info, XmasModel engine, bool reversed)
        {
            this.Factory = factory;
            this.GUIInfo = info;
            this.Engine = engine;
            this.reversed = reversed;
            Engine.EventManager.Register(new Trigger<ManaCrystalAddedEvent>(OnManaAdded));
            Engine.EventManager.Register(new Trigger<ManaCrystalSpentEvent>(OnManaSpent));
            Engine.EventManager.Register(new Trigger<ManaRechargedEvent>(OnManaRecharged));
        }

        private void OnManaAdded(ManaCrystalAddedEvent evt)
        {
            if (evt.storage.Size(evt.crystalType) > 0)
            {
                manaBars[evt.crystalType] = Factory.CreateManaBar(evt.crystalType);
            }
            else
            {
                manaBars[evt.crystalType].renderer.material.SetTextureScale("_MainTex", new Vector2(1, (evt.storage.Size(evt.crystalType))));
            }
        }

        private void OnManaSpent(ManaCrystalSpentEvent evt)
        {

        }

        private void OnManaRecharged(ManaRechargedEvent evt)
        {

        }
	}
}
