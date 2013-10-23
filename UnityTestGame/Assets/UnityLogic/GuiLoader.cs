using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using Assets.GameLogic.Events;
using XmasEngineModel.Management;
using Assets.UnityLogic.Gui;
using Assets.GameLogic;

namespace Assets.UnityLogic
{
	public class GuiLoader : MonoBehaviour
	{
        public MapHandler MapHandler;
        public EngineHandler Engine;
        public GlobalGameSettings Settings;
        public GUITexture PlayerLogo_Friendly;
        public GUITexture HealthBar_Friendly;

        public GUITexture PlayerLogo_Opponent;
        public GUITexture HealthBar_Opponent;

        public GUITexture[] Phases;
        public GUITexture[] StopPhases_Main;
        public GUITexture[] StopPhases_Other;

        public GUITexture[] ManaCrystalTypes;
        private Dictionary<Player, GuiInformation> guiLookup = new Dictionary<Player, GuiInformation>();

        void Start()
        {
            XmasModel engine = Engine.EngineModel;
            engine.EventManager.Register(new Trigger<PlayerJoinedEvent>(OnPlayerJoin));

        }

        private void OnPlayerJoin(PlayerJoinedEvent evt)
        {

            GameObject gobj = new GameObject();
            gobj.name = "GUI for player " + evt.Player.Name;
            gobj.AddComponent<GuiInformation>();
            GuiInformation ginfo = gobj.GetComponent<GuiInformation>();
            this.guiLookup.Add(evt.Player, ginfo);
            ginfo.Player = evt.Player;
            if (Settings.MainPlayer == evt.Player)
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

            if (Settings.LocalPlayers.Any(p => p == evt.Player))
            {
                gobj.AddComponent<GuiController>();
                gobj.GetComponent<GuiController>().Engine = this.Engine;
            }

            gobj.AddComponent<GuiViewHandler>();
            var guiview = gobj.GetComponent<GuiViewHandler>();
            guiview.Engine = this.Engine;
            guiview.MapHandler = this.MapHandler;
        }



        public GuiInformation GetGuiInfo(Player player)
        {
            return this.guiLookup[player];
        }
    }
}
