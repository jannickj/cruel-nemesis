using UnityEngine;
using System.Collections;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineModel.EntityLib;
using Cruel.Map.Terrain;
using Assets.UnityLogic;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Cruel.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using Assets.UnityLogic.Gui;
using Cruel.GameLogic.Modules;


public class UnitHandler : MonoBehaviour {

    
    public GuiLoader GuiLoader;
    public EngineHandler Engine;
    public UnityFactory Factory;

	// Use this for initialization
	void Start () {
        XmasModel eng = Engine.EngineModel;
        eng.EventManager.Register(new Trigger<EntityAddedEvent>(ent => ent.AddedXmasEntity is UnitEntity,OnUnitEntity));
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    private void OnUnitEntity(EntityAddedEvent evt)
    {
        UnitInfoModule uinfo = evt.AddedXmasEntity.Module<UnitInfoModule>();
        GuiInformation guiinfo = this.GuiLoader.GetGuiInfo(uinfo.Controller);

        UnitEntity unitEnt = (UnitEntity)evt.AddedXmasEntity;
        TilePosition posinfo = (TilePosition)evt.AddedPosition;
        

        Point pos = posinfo.Point;
        Transform unitobj = Factory.CreateUnit(unitEnt, posinfo);

        UnitInformation info = unitobj.gameObject.GetComponent<UnitInformation>();
        info.ControllerInfo = guiinfo;
        
    }
}
