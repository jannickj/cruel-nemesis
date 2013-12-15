using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using Cruel.GameLogic.Events;
using XmasEngineModel.Management;
using Assets.UnityLogic.Gui;
using Cruel.GameLogic;

namespace Assets.UnityLogic
{
	public class GuiLoader : MonoBehaviour
	{
        public MapHandler MapHandler;
        public EngineHandler Engine;
        public GlobalGameSettings Settings;
        public UnityFactory Factory;

        public GUITexture PlayerLogo_Friendly;
        public GUITexture HealthBar_Friendly;
        public GUITexture XPBar_Friendly;
        public GUITexture XPButton_Friendly;

        public GUITexture PlayerLogo_Opponent;
        public GUITexture HealthBar_Opponent;
        public GUITexture XPBar_Opponent;
        public GUITexture XPButton_Opponent;
        

        public GUITexture[] Phases;
        public GUITexture[] StopPhases_Main;
        public GUITexture[] StopPhases_Other;

        public GUITexture[] ManaCrystalTypes;
        private Dictionary<Player, KeyValuePair<GameObject, GuiInformation>> guiLookup = new Dictionary<Player, KeyValuePair<GameObject, GuiInformation>>();
        private List<Player> joinedPlayers = new List<Player>();

        void Start()
        {
            XmasModel engine = Engine.EngineModel;
            engine.EventManager.Register(new Trigger<PlayerJoinedEvent>(OnPlayerJoin));
            engine.EventManager.Register(new Trigger<GamePreStartEvent>(OnGameStart));
        }

        private void OnGameStart(GamePreStartEvent evt)
        {
            foreach (Player player in joinedPlayers)
            {
                var pgui = this.guiLookup[player];
                var ginfo = pgui.Value;
                var gobj = pgui.Key;
                bool isNotMainPlayer;

                if (Settings.MainPlayer == player)
                {
                    ginfo.Portrait = PlayerLogo_Friendly;
                    ginfo.HealthBar = HealthBar_Friendly;
                    ginfo.XPBar = XPBar_Friendly;
                    ginfo.XPButton = XPButton_Friendly;
                    ginfo.FocusColor = Color.green;
                    isNotMainPlayer = false;
                }
                else
                {
                    ginfo.Portrait = PlayerLogo_Opponent;
                    ginfo.HealthBar = HealthBar_Opponent;
                    ginfo.XPBar = XPBar_Opponent;
                    ginfo.XPButton = XPButton_Opponent;
                    ginfo.FocusColor = Color.blue;
                    isNotMainPlayer = true;
                }

                ginfo.SetSkipPhaseButton(player, StopPhases_Main);
                ginfo.SetSkipPhaseButton(joinedPlayers.First(p => p != player), StopPhases_Other);

                ginfo.SetPhasesGui(Phases);
                ginfo.SetManaCrystalTypes(ManaCrystalTypes);

                gobj.AddComponent<GuiViewHandler>();
                var guiview = gobj.GetComponent<GuiViewHandler>();
                guiview.Engine = this.Engine;
                guiview.Factory = Factory;
                guiview.MapHandler = this.MapHandler;

                guiview.Initialize();

                gobj.AddComponent<GuiHandViewHandler>();
                var handview = gobj.GetComponent<GuiHandViewHandler>();
                handview.Factory = Factory;
                handview.Initialize(Camera.main, 0.5f, 0.5f, 0.25f, 0.25f, Engine.EngineModel.EventManager, player);

                var xpviewer = gobj.AddComponent<GuiXPBarViewHandler>();
                xpviewer.Initialize(Factory, ginfo, isNotMainPlayer);


                gobj.AddComponent<GUIManaViewHandler>();
                var manaview = gobj.GetComponent<GUIManaViewHandler>();
                manaview.Initialize(Factory, ginfo, Engine.EngineModel, isNotMainPlayer);

                if (Settings.LocalPlayers.Any(p => p == player))
                {
                    gobj.AddComponent<GuiController>();
                    var controller = gobj.GetComponent<GuiController>();
                    controller.Engine = this.Engine;
                    controller.JoinedPlayers = this.joinedPlayers;
                    controller.SkipController = new PhaseSkipToController(ginfo, player);
                    controller.Initialize();
                    if (Settings.LocalPlayers.Count() > 1)
                        controller.ControllerType = ControllerType.Shared;
                    else
                        controller.ControllerType = ControllerType.Full;
                }

                
            }
        }

        private void OnPlayerJoin(PlayerJoinedEvent evt)
        {
            Player player = evt.Player;
            GameObject gobj = new GameObject();
            gobj.name = "GUI for player " + player.Name;
            gobj.AddComponent<GuiInformation>();
            GuiInformation ginfo = gobj.GetComponent<GuiInformation>();
            this.guiLookup.Add(player, new KeyValuePair<GameObject,GuiInformation>(gobj,ginfo));
            ginfo.Player = player;

            this.joinedPlayers.Add(player);
        }



        public GuiInformation GetGuiInfo(Player player)
        {
            return this.guiLookup[player].Value;
        }
    }
}
