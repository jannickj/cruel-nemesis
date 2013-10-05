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


public class UnitHandler : MonoBehaviour {

    public Transform UnitTile;

	// Use this for initialization
	void Start () {
        XmasModel eng = EngineHandler.GetEngine();
        eng.EventManager.Register(new Trigger<EntityAddedEvent>(ent => ent.AddedXmasEntity is UnitEntity,OnUnitEntity));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnUnitEntity(EntityAddedEvent evt)
    {
        UnitEntity unitEnt = (UnitEntity)evt.AddedXmasEntity;
        TilePosition posinfo = (TilePosition)evt.AddedPosition;
        Point pos = posinfo.Point;
        Quaternion ur = UnitTile.rotation;
        Quaternion rot = new Quaternion(ur.x+0.14f,ur.y,ur.z,ur.w);
        var  transform = (Transform)Instantiate(UnitTile,new Vector3((float)pos.X,(float)pos.Y+0.5f,0.3f),rot);
        transform.gameObject.AddComponent<UnitInformation>();
        UnitInformation info = transform.gameObject.GetComponent<UnitInformation>();
        info.SetEntity(unitEnt);
        UnitGraphics graphic = UnitGraphicFactory.ConstuctUnitGraphic(unitEnt.getUnitType());
        info.SetGraphics(graphic);
        transform.gameObject.AddComponent<UnitViewHandler>();
        transform.gameObject.AddComponent<UnitControllerHandler>();
    }
}
