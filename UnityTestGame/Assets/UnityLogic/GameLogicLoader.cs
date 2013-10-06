using UnityEngine;
using System.Collections;
using XmasEngineModel;
using Assets.GameLogic.TurnLogic;

public class GameLogicLoader : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        XmasModel engine = EngineHandler.GetEngine();

        //Start turn manager
        TurnManager turnManager = new TurnManager();
        engine.AddActor(turnManager);
        turnManager.Initialize();
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
