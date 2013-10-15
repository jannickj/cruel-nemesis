using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using Assets.GameLogic.Events;
using XmasEngineModel.Management;
using Assets.UnityLogic.Gui;

namespace Assets.UnityLogic
{
	public class GuiLoader : MonoBehaviour
	{
        public GUITexture PlayerLogo_Friendly;
        public GUITexture HealthBar_Friendly;

        public GUITexture PlayerLogo_Opponent;
        public GUITexture HealthBar_Opponent;

        public GUITexture[] Phases;        
        public GUITexture[] ManaCrystalTypes;
        private GlobalGameSettings settings;


        void Start()
        {
            XmasModel engine = EngineHandler.GetEngine();
            settings = GlobalGameSettings.GetSettings();
            engine.EventManager.Register(new Trigger<PlayerJoinedEvent>(OnPlayerJoin));


        }

        private void OnPlayerJoin(PlayerJoinedEvent evt)
        {

            GameObject gobj = new GameObject();
            gobj.AddComponent<GuiInformation>();
            GuiInformation ginfo = gobj.GetComponent<GuiInformation>();

            ginfo.Player = evt.Player;
            if (settings.MainPlayer == evt.Player)
            {
                ginfo.Portrait = PlayerLogo_Friendly;
                ginfo.HealthBar = HealthBar_Friendly;
                ginfo.FocusColor = Color.green;
            }
            else
            {
                ginfo.Portrait = PlayerLogo_Opponent;
                ginfo.HealthBar = HealthBar_Opponent;
                ginfo.FocusColor = Color.blue;
            }

            ginfo.SetPhasesGui(Phases);
            ginfo.SetManaCrystalTypes(ManaCrystalTypes);

            if(settings.LocalPlayers.Any(p => p == evt.Player))
                gobj.AddComponent<GuiController>();

            gobj.AddComponent<GuiViewHandler>();
        }

        
	}
}
