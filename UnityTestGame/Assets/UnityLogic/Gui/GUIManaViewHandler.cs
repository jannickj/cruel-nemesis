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
        private float translateX = 100;
        private float translateY = 0;
        private Dictionary<Mana, GUITexture> manaBars = new Dictionary<Mana, GUITexture>();

        public void Initialize(UnityFactory factory, GuiInformation info, XmasModel engine, bool reversed)
        {
            this.Factory = factory;
            this.GUIInfo = info;
            this.Engine = engine;
            this.reversed = reversed;
            Engine.EventManager.Register(new Trigger<ManaCrystalAddedEvent>(evt=>evt.Owner==this.GUIInfo.Player,OnManaAdded));
            Engine.EventManager.Register(new Trigger<ManaCrystalSpentEvent>(evt => evt.Owner == this.GUIInfo.Player, OnManaSpent));
            Engine.EventManager.Register(new Trigger<ManaRechargedEvent>(evt => evt.Owner == this.GUIInfo.Player, OnManaRecharged));
        }

        private void OnManaAdded(ManaCrystalAddedEvent evt)
        {
            Mana mana = evt.CrystalType;
            var manaBar = manaBars[mana];
            Debug.Log(mana);
            if (evt.Storage.Size(mana) > 0)
            {
                manaBar = Factory.CreateManaBar(mana);
                manaBar.transform.parent = this.GUIInfo.Portrait.transform;
                if(!reversed)
                {
                    var scaler = manaBar.GetComponent<GUITextureAutoScaler>();
                    
                }
            }
            else
            {
                manaBar.renderer.material.SetTextureScale("_MainTex", new Vector2(1, (evt.Storage.Size(evt.CrystalType))));
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
