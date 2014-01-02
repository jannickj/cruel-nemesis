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
using XmasEngineModel.Management.Actions;
using Cruel.GameLogic.Map;
using Cruel.GameLogic.Events.UnitEvents;
using CruelGameData.GameLogic.Game.Maps;

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
        //if (Event.current.type == EventType.keyDown && Input.GetButton("start_game"))
        if(true)
        {
            
            if (!gamestarted)
            {
                if (players.Count != 2)
                {
                    Debug.Log("Cannot start game atleast 2 players must join");
                    return;
                }

                StandardGameMapBuilder builder = (StandardGameMapBuilder)engmodel.WorldBuilder;
                builder.SetPlayers(players[0], players[1]);

                Debug.Log("Starting game");
                gamestarted = true;
                engmodel.Initialize();
                engmodel.ActionManager.Queue(new StartGameCommand());
                engmodel.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => engmodel.ActionManager.Queue(new DrawCardAction(evt.PlayersTurn, 1))));
                engmodel.ActionManager.Queue(new DrawCardAction(players[0], CardsToStartWith));
                engmodel.ActionManager.Queue(new DrawCardAction(players[1], CardsToStartWith));
                engmodel.ActionManager.Queue(new SimpleAction(_=>
                {
                    players[0].AddXP(1);
                    players[1].AddXP(1);
                    engmodel.ActionManager.Queue(new SimpleAction(_1 =>
                    {
                        players[0].ManaStorage.chargeAll();
                        players[1].ManaStorage.chargeAll();
                    }));
                }));
                engmodel.ActionManager.Queue(new PlayerGainManaCrystalAction(players[0], Mana.Arcane));
                engmodel.ActionManager.Queue(new PlayerGainManaCrystalAction(players[1], Mana.Fury));
                players[0].Hero.Register(new Trigger<UnitDieEvent>(evt => Application.LoadLevel(1)));
                players[1].Hero.Register(new Trigger<UnitDieEvent>(evt => Application.LoadLevel(2)));
            }
        }
    }
}
