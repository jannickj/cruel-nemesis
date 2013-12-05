using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.SpellSystem;
using CruelTest.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GUIManaViewHandler : MonoBehaviour
	{
        public UnityFactory Factory { get; set; }
        public GuiInformation GUIInfo;
        public XmasModel Engine;
        public bool reversed;
        private float translateXBackground = 200;
        private float translateYBackground = 35;
        private float translateX = 200;
        private float translateY = 35;
        private float spacing = 100;
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
            if (evt.Storage.Size(mana) == 1)
            {
                CreateManaBar(mana);
                translateX += spacing;
                translateXBackground += spacing;
                Debug.Log("Translates: " + translateX + ", " + translateXBackground);
            }
            else
                UpdateManaLevel(evt.CrystalType);
        }

        private void CreateManaBar(Mana mana)
        {
            var emptyBar = Factory.CreateEmptyManaBar();
            var position = emptyBar.transform.position;
            position.z = -1;
            emptyBar.transform.position = position;
            emptyBar.transform.parent = this.GUIInfo.Portrait.transform;
            var scaler = emptyBar.GetComponent<GUITextureAutoScaler>();
            var size = scaler.CurSize;
            var offsetX = reversed ? (-1 * (translateX + (size.width / 2))) : translateX;
            size.x += offsetX;
            size.y -= translateY;
            scaler.CurSize = size;

            var manaBar = Factory.CreateManaBar(mana);
            manaBars[mana] = manaBar;
            manaBar.transform.parent = this.GUIInfo.Portrait.transform;
            var manaScaler = manaBar.GetComponent<GUITextureAutoScaler>();
            var manaSize = manaScaler.CurSize;
            var manaOffsetX = reversed ? (-1 * (translateXBackground + (manaSize.width / 2))) : translateX;
            manaSize.x += manaOffsetX;
            manaSize.y -= translateYBackground;
            manaScaler.CurSize = manaSize;
        }

        private void OnManaSpent(ManaCrystalSpentEvent evt)
        {
            UpdateManaLevel(evt.CrystalType);
        }

        private void OnManaRecharged(ManaRechargedEvent evt)
        {

        }

        private void UpdateManaLevel(Mana mana)
        {
            ManaStorage manaStorage = this.GUIInfo.Player.ManaStorage;
            int maxVal = manaStorage.Size(mana);
            int curVal;
            foreach (List<ManaCrystal> l in manaStorage.Values)
                foreach (ManaCrystal m in l)
        }
	}
}
