using UnityEngine;
using System.Collections;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Actions;
using JSLibrary.Data;
using Cruel.GameLogic.Events;
using System.Collections.Generic;
using System.Linq;
using Assets.UnityLogic.Commands;
using Assets.UnityLogic.Unit;
using Cruel.GameLogic;
using XmasEngineModel;
using Cruel.GameLogic.TurnLogic;
using Cruel.GameLogic.Modules;
using System;
using Assets.UnityLogic.Gui.Controls;

namespace Assets.UnityLogic.Gui
{
    public enum ControllerType
    {
        Full,
        Shared
    }

    public class GuiController : MonoBehaviour
    {
        public EngineHandler Engine;
        public Camera PlayerCamera;
        private HashSet<Command> runningCommands = new HashSet<Command>();
        private HashSet<Command> awaitingCommands = new HashSet<Command>();
        public GuiViewHandler GuiView { get; private set; }
        public ControllerType ControllerType { get; set; }

        private GuiInformation guiinfo;
        public PhaseSkipController SkipController { get; set; }
        public List<Player> JoinedPlayers { get; set; }
        private XmasModel engmodel;
        private bool hasPriority;
        private bool allowedToDeclare = false;
        // Use this for initialization
        public void Initialize()
        {
            
            this.guiinfo = this.gameObject.GetComponent<GuiInformation>();
            this.GuiView = this.gameObject.GetComponent<GuiViewHandler>();
            
            hasPriority = false;
            if (PlayerCamera == null)
                PlayerCamera = Camera.main;

            engmodel = Engine.EngineModel;

            
            engmodel.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => hasPriority = evt.Player == guiinfo.Player ));
            engmodel.EventManager.Register(new Trigger<PlayerAllowedToDeclareMoveAttackEvent>(evt => allowedToDeclare = (evt.Player == guiinfo.Player && evt.Allowed)));

            

            foreach (var player in this.JoinedPlayers)
            {
                foreach(Phases phase in (Phases[])Enum.GetValues(typeof(Phases)))
                {
                    var texture = guiinfo.GetSkipPhaseButton(player, phase);
                    if (texture == null)
                        continue;

                    var buttonHandler = texture.GetComponent<GUIButtonHandler>();
                    var selectedPlayer = player;
                    var selectedPhase = phase;
                    buttonHandler.MouseDownEvent += (sender, evt) =>
                    {
                        if (this.hasPriority && this.ControllerType == ControllerType.Shared || this.ControllerType == ControllerType.Full)
                        {
                            this.PerformCommand(new ToggleStopPriorityCommand(SkipController, selectedPlayer, selectedPhase));
                        }
                    };
                }
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Command cmd in awaitingCommands.ToArray())
            {
                awaitingCommands.Remove(cmd);
                runningCommands.Add(cmd);
            }

            if (this.runningCommands.Count == 0 && hasPriority)
                UpdateControls();

            UpdateCommands();
        }

        private void UpdateControls()
        {
            if (Input.GetButtonDown("select_object"))
            {
                GameObject[] objs = GetGameObjectsOnMouse();

                foreach (GameObject selectedObject in objs)
                {
                    var unitinfo = selectedObject.GetComponent<UnitInformation>();
                    var cardinfo = selectedObject.GetComponent<CardInformation>();

                    if (cardinfo != null)
                    {
                        this.PerformCommand(new DeclareCastCardCommand(cardinfo.Card));
                        return;
                    }

                    if (unitinfo != null)
                    {

                        if (!allowedToDeclare)
                            continue;

                        var ent = unitinfo.Entity;

                        if (ent.Module<UnitInfoModule>().Controller != this.guiinfo.Player)
                            return;

                        this.PerformCommand(new DeclareMoveAttackUnitCommand(selectedObject, ent));
                        return;
                    }
                }
            }
            else if (Input.GetButtonDown("pass_priority"))
            {
                this.PerformCommand(new PassPriorityCommand(guiinfo.Player));
            }
        }


        public GuiInformation GuiInfo
        {
            get { return guiinfo; }
        }

        public void PerformCommand(Command cmd)
        {
            engmodel.AddActor(cmd);
            cmd.GuiController = this;
            awaitingCommands.Add(cmd);
        }

        public void UpdateCommands()
        {
            foreach (Command cmd in runningCommands.ToArray())
            {
                cmd.Update();
                if (cmd.Finished)
                    this.runningCommands.Remove(cmd);
            }
        }

        

        public GameObject[] GetGameObjectsOnMouse()
        {
            HashSet<GameObject> objs = new HashSet<GameObject>();
            List<GameObject> sortobjs = new List<GameObject>();
            var ray = this.PlayerCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (!objs.Contains(hit.collider.gameObject))
                {
                    objs.Add(hit.collider.gameObject);
                    sortobjs.Add(hit.collider.gameObject);
                }
            }

            return sortobjs.ToArray();
        }


        public object GetModelObjectsFromGameObjects(GameObject gobj)
        {
            var terinfo = gobj.GetComponent<TerrainInformation>();
            var unitinfo = gobj.GetComponent<UnitInformation>();
            var cardinfo = gobj.GetComponent<CardInformation>();

            if (terinfo != null)
                return terinfo.Terrain;
            if (unitinfo != null)
                return unitinfo.Entity;
            if (cardinfo != null)
                return cardinfo.Card;
            return null;
        }
        
    }
}
