using UnityEngine;
using System.Collections;
using XmasEngineModel;
using System;
using Assets.Map;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;

public class EngineHandler : MonoBehaviour {

    public MapTypes Map;
    private XmasModel engine;

	// Use this for initialization
	void Start () 
    {
       
        XmasWorldBuilder builder = retreiveBuilderFromMap();
        EventManager evtman = new EventManager();
        ActionManager actman = new ActionManager(evtman);
        XmasWorld world = builder.Build(actman);
        XmasFactory factory = new XmasFactory(actman);
        engine = new XmasModel(world, actman, evtman, factory);

        evtman.Register(new Trigger<ActionFailedEvent>(evt => Debug.Log("Engine action failed: "+evt.ActionException.Message)));

	}
	
	// Update is called once per frame
	void Update () 
    {
        engine.Update();
	}

    public static XmasModel GetEngine()
    {
        GameObject engineObj = GameObject.Find("XmasEngine");
        return engineObj.GetComponent<EngineHandler>().engine;
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
}
