using UnityEngine;
using System.Collections;
using XmasEngineModel;
using Cruel.GameLogic.TurnLogic;
using Cruel.GameLogic.Actions;
using Cruel.GameLogic;
using Assets.UnityLogic;
using Cruel.GameLogic.Events;
using XmasEngineModel.Management;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using System.Linq;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Modules;
using Cruel.Map;
using System.Collections.Generic;
using Cruel.GameLogic.PlayerCommands;
using Cruel.GameLogic.SpellSystem;

public class GameLogicLoader : MonoBehaviour {

    public int CardsToStartWith = 5;
    public EngineHandler Engine;
    private bool gamestarted = false;
    private XmasModel engmodel;
    public GlobalGameSettings Settings;
    private List<Player> players = new List<Player>();
	// Use this for initialization
	void Start () 
    {
        engmodel = Engine.EngineModel;

        //Start turn manager
        TurnManager turnManager = new TurnManager();
        engmodel.AddActor(turnManager);

        AbilityManager abilityManager = new AbilityManager();
        engmodel.AddActor(abilityManager);

        engmodel.EventManager.Register(new Trigger<PlayerJoinedEvent>(OnPlayerJoin));
        Player[] players = Settings.LocalPlayers;

        foreach (Player p in players)
        {
            engmodel.ActionManager.Queue(new PlayerJoinAction(p));
        }

        
	    
	}


    private void OnPlayerJoin(PlayerJoinedEvent evt)
    {
        players.Add(evt.Player);
    }

    // Update is called once per frame
    void Update()
    {
	
	}

    void OnGUI()
    {
        if (Event.current.type == EventType.keyDown && Input.GetButton("start_game"))
        {
            
            if (!gamestarted)
            {
                if (players.Count != 2)
                {
                    Debug.Log("Cannot start game atleast 2 players must join");
                }

                StandardGameMapBuilder builder = (StandardGameMapBuilder)engmodel.WorldBuilder;
                builder.SetPlayers(players[0], players[1]);

                Debug.Log("Starting game");
                gamestarted = true;
                engmodel.Initialize();
                engmodel.ActionManager.Queue(new StartGameCommand());
                engmodel.ActionManager.Queue(new DrawCardAction(players[0], CardsToStartWith));
                engmodel.ActionManager.Queue(new DrawCardAction(players[1], CardsToStartWith));
            }
        }
    }
}
