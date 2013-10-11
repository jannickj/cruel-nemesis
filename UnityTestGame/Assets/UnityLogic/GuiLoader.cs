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
        public GUITexture PlayerLogo;
        public GUITexture[] Phases;
        public GUITexture HealthBar;
        public GUITexture ManaCrystalFury;



        void Start()
        {
            XmasModel engine = EngineHandler.GetEngine();

            engine.EventManager.Register(new Trigger<PlayerJoinedEvent>(OnPlayerJoin));
        }

        private void OnPlayerJoin(PlayerJoinedEvent evt)
        {
            
            if (GlobalGameSettings.GetSettings().MainPlayer == evt.Player)
            {
                GameObject gobj = new GameObject();
                gobj.AddComponent<PlayerInformation>();
                PlayerInformation pinfo = gobj.GetComponent<PlayerInformation>();

                pinfo.Player = evt.Player;

                gobj.AddComponent<GuiController>();
                gobj.AddComponent<GuiViewHandler>();


               
            }
        }

        
	}
}
