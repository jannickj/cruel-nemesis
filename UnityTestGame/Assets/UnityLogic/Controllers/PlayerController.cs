using UnityEngine;
using System.Collections;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using Assets.GameLogic.Unit;
using Assets.GameLogic.Actions;
using JSLibrary.Data;
using Assets.GameLogic.Events;

public class PlayerController : MonoBehaviour {
    private UnitEntity ue;
	// Use this for initialization
	void Start () 
    {
        EngineHandler.GetEngine().EventManager.Register(
            new Trigger<EntityAddedEvent>(e => e.AddedXmasEntity is UnitEntity, evt => ue = (UnitEntity)evt.AddedXmasEntity));
	    EngineHandler.GetEngine().EventManager.Register(new Trigger<EndMoveEvent>(evt => Debug.Log("Unit has moved to "+evt.To)));
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ue.QueueAction(new MoveAction(new Vector(1,0),0));
        }
	}
}
