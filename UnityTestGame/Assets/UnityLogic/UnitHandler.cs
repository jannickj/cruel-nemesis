using UnityEngine;
using System.Collections;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using XmasEngineModel.EntityLib;
using Assets.Map.Terrain;
using Assets.UnityLogic;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Assets.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using Assets.UnityLogic.Gui;
using Assets.GameLogic.Modules;


public class UnitHandler : MonoBehaviour {

    public Transform UnitTile;
    public GuiLoader GuiLoader;
    public EngineHandler Engine;

	// Use this for initialization
	void Start () {
        XmasModel eng = Engine.EngineModel;
        eng.EventManager.Register(new Trigger<EntityAddedEvent>(ent => ent.AddedXmasEntity is UnitEntity,OnUnitEntity));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void ConvertUnitPos(Point pos, out Vector3 v)
    {
        v = new Vector3(-(float)pos.X, (float)pos.Y + 0.5f, 0.3f);
    }

    private void OnUnitEntity(EntityAddedEvent evt)
    {
        UnitInfoModule uinfo = evt.AddedXmasEntity.Module<UnitInfoModule>();
        GuiInformation guiinfo = this.GuiLoader.GetGuiInfo(uinfo.Controller);

        UnitEntity unitEnt = (UnitEntity)evt.AddedXmasEntity;
        TilePosition posinfo = (TilePosition)evt.AddedPosition;
        Point pos = posinfo.Point;
        Quaternion ur = UnitTile.rotation;
        Quaternion rot = new Quaternion(ur.x+0.14f,ur.y,ur.z,ur.w);
        Vector3 unitvec;
        ConvertUnitPos(pos, out unitvec);
        var  transform = (Transform)Instantiate(UnitTile,unitvec,rot);
        transform.gameObject.AddComponent<UnitInformation>();
        UnitInformation info = transform.gameObject.GetComponent<UnitInformation>();
        info.ControllerInfo = guiinfo;
        info.SetEntity(unitEnt);
        UnitGraphics graphic = UnitGraphicFactory.ConstuctUnitGraphic(unitEnt.getUnitType());
        info.SetGraphics(graphic);
        transform.gameObject.AddComponent<UnitViewHandler>();
        transform.gameObject.AddComponent<UnitControllerHandler>();
    }
}
