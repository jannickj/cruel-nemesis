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

namespace Assets.UnityLogic.Gui
{
    public class GuiController : MonoBehaviour
    {
        private UnitEntity ue;
        public Camera PlayerCamera;
        private HashSet<Command> runningCommands = new HashSet<Command>();
        private HashSet<Command> awaitingCommands = new HashSet<Command>();

        private Player player;
        private XmasModel engine;
        private bool hasPriority;

        // Use this for initialization
        void Start()
        {
            hasPriority = true;
            if (PlayerCamera == null)
                PlayerCamera = Camera.main;
            EngineHandler.GetEngine().EventManager.Register(
                new Trigger<EntityAddedEvent>(e => e.AddedXmasEntity is UnitEntity, evt => ue = (UnitEntity)evt.AddedXmasEntity));
            EngineHandler.GetEngine().EventManager.Register(new Trigger<EndMoveEvent>(evt => Debug.Log("Unit has moved to " + evt.To)));
            engine = EngineHandler.GetEngine();
            
            engine.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => hasPriority = evt.Player == player ));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ue.QueueAction(new MoveAction(new Vector(1, 0), 0));
            }
        }

        void OnGUI()
        {

            foreach (Command cmd in awaitingCommands.ToArray())
            {
                awaitingCommands.Remove(cmd);
                runningCommands.Add(cmd);
            }

            if (this.runningCommands.Count == 0 && hasPriority)
                if (Event.current.type == EventType.MouseDown && Input.GetButton("select_object"))
                {
                    Debug.Log("Starting select");
                    GameObject[] objs = GetGameObjectsOnMouse();
                    GameObject firstunit = objs.FirstOrDefault(go => go.GetComponent<UnitControllerHandler>() != null);
                    if (firstunit == null)
                        return;

                    this.PerformCommand(new MoveUnitCommand(firstunit, firstunit.GetComponent<UnitInformation>().Entity));
                }
                else if (Event.current.type == EventType.keyDown && Input.GetButton("pass_priority"))
                {
                    this.PerformCommand(new PassPriorityCommand(player));
                }



            UpdateCommands();
        }

        void OnMouseDown()
        {
            Debug.Log("TEST");
        }

        public void PerformCommand(Command cmd)
        {
            engine.AddActor(cmd);
            cmd.PlayerController = this;
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
