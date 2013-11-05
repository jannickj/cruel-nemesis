using UnityEngine;
using System.Collections;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using Assets.GameLogic.Unit;
using Assets.GameLogic.Actions;
using JSLibrary.Data;
using Assets.GameLogic.Events;
using System.Collections.Generic;
using System.Linq;
using Assets.UnityLogic.Commands;
using Assets.UnityLogic.Unit;
using Assets.GameLogic;
using XmasEngineModel;
using Assets.GameLogic.TurnLogic;
using Assets.GameLogic.Modules;

namespace Assets.UnityLogic.Gui
{
    public class GuiController : MonoBehaviour
    {
        public EngineHandler Engine;
        public Camera PlayerCamera;
        private HashSet<Command> runningCommands = new HashSet<Command>();
        private HashSet<Command> awaitingCommands = new HashSet<Command>();
        public GuiViewHandler GuiView { get; private set; }

        private GuiInformation guiinfo;
        private PhaseSkipController skipController;
        private XmasModel engmodel;
        private bool hasPriority;
        private bool allowedToDeclare = false;
        // Use this for initialization
        void Start()
        {
            skipController = new PhaseSkipController(this.GuiInfo,guiinfo.Player);
            this.guiinfo = this.gameObject.GetComponent<GuiInformation>();
            this.GuiView = this.gameObject.GetComponent<GuiViewHandler>();
            hasPriority = false;
            if (PlayerCamera == null)
                PlayerCamera = Camera.main;

            engmodel = Engine.EngineModel;

            
            engmodel.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => hasPriority = evt.Player == guiinfo.Player ));
            engmodel.EventManager.Register(new Trigger<PlayerAllowedToDeclareMoveAttackEvent>(evt => allowedToDeclare = (evt.Player == guiinfo.Player && evt.Allowed)));
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
                if (Input.GetButtonDown("select_object") && allowedToDeclare)
                {
                    GameObject[] objs = GetGameObjectsOnMouse();
                    GameObject firstunit = objs.FirstOrDefault(go => go.GetComponent<UnitControllerHandler>() != null);

                    if (firstunit == null)
                        return;
                    var ent = firstunit.GetComponent<UnitInformation>().Entity;

                    if (ent.Module<UnitInfoModule>().Controller != this.guiinfo.Player)
                        return;

                    this.PerformCommand(new DeclareMoveAttackUnitCommand(firstunit, ent));
                }
                else if (Input.GetButtonDown("pass_priority"))
                {
                    this.PerformCommand(new PassPriorityCommand(guiinfo.Player));
                }



            UpdateCommands();
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

    }
}
