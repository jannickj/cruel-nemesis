using UnityEngine;
using System.Collections;
using XmasEngineModel;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using XmasEngineModel.Management.Events;

public class DebugMessenger : MonoBehaviour {

    private XmasModel engine;

	// Use this for initialization
	void Start () 
    {
        engine = EngineHandler.GetEngine();
        engine.EventManager.Register(new Trigger<ActionFailedEvent>(evt => Debug.Log("Engine action("+evt.FailedAction+") failed: " + evt.Exception.Message+" at "+evt.Exception.StackTrace)));
        engine.EventManager.Register(new Trigger<EndMoveEvent>(evt => Debug.Log("Unit has moved to " + evt.To)));
        engine.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => Debug.Log("Player turn changed to: "+evt.PlayersTurn.Name)));
        engine.EventManager.Register(new Trigger<PhaseChangedEvent>(evt => Debug.Log("Phase changed to: " + evt.NewPhase.ToString())));
        engine.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => Debug.Log("Player gained priority: " + evt.Player.Name)));
        engine.EventManager.Register(new Trigger<PlayerJoinedEvent>(evt => Debug.Log("Player joined: " + evt.Player.Name)));
        engine.EventManager.Register(new Trigger<TriggerFailedEvent>(evt => Debug.Log(evt.FailedTrigger + " failed: " + evt.Exception.Message + " at " + evt.Exception.StackTrace)));
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
