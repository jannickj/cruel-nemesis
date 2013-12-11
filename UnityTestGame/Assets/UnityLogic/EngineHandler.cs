using UnityEngine;
using System.Collections;
using XmasEngineModel;
using System;
using Cruel.Map;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using Assets.UnityLogic.Game;
using Assets.UnityLogic.Game.Maps;

public class EngineHandler : MonoBehaviour {

    public MapTypes Map;
    private XmasModel engine;

	// Use this for initialization
	void Start () 
    {
       
        XmasWorldBuilder builder = retreiveBuilderFromMap();
        EventManager evtman = new EventManager();
        ActionManager actman = new ActionManager(evtman);
        XmasFactory factory = new GameFactory(actman);
        engine = new XmasModel(builder, actman, evtman, factory);

        

	}
	
	// Update is called once per frame
	void Update () 
    {
        engine.Update();
	}


    private XmasWorldBuilder retreiveBuilderFromMap()
    {
        var type = typeof(MapTypes);
        var memInfo = type.GetMember(Map.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(MapTypeAttribute),
            false);
        var MapType = ((MapTypeAttribute)attributes[0]).Type;

        XmasWorldBuilder worldbuilder = (XmasWorldBuilder)Activator.CreateInstance(MapType);

        return worldbuilder;
    }

 

    public XmasModel EngineModel 
    {
        get
        {
            return this.engine;
        }
    }
}
