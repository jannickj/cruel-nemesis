using UnityEngine;
using System.Collections;
using XmasEngineModel;
using Assets.GameLogic.TurnLogic;
using Assets.GameLogic.Actions;
using Assets.GameLogic;
using Assets.UnityLogic;

public class GameLogicLoader : MonoBehaviour {

    private bool gamestarted = false;
    private XmasModel engine;
	// Use this for initialization
	void Start () 
    {
        engine = EngineHandler.GetEngine();

        //Start turn manager
        TurnManager turnManager = new TurnManager();
        engine.AddActor(turnManager);
        turnManager.Initialize();

      
        Player[] players = GlobalGameSettings.GetSettings().LocalPlayers;

        foreach (Player p in players)
        {
            engine.ActionManager.Queue(new PlayerJoinAction(p));
        }
	    
	}

    
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (Event.current.type == EventType.keyDown && Input.GetButton("start_game"))
        {
            
            if (!gamestarted)
            {
                Debug.Log("Starting game");
                gamestarted = true;
                engine.ActionManager.Queue(new StartGameAction()); 
            }
        }
    }
}
