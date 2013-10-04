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


public class MapHandler : MonoBehaviour {

    public Transform Terrain;

	// Use this for initialization
	void Start () {
        XmasModel eng = EngineHandler.GetEngine();
        eng.EventManager.Register(new Trigger<EntityAddedEvent>(ent => ent.AddedXmasEntity is TerrainEntity,OnTerrainEntity));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTerrainEntity(EntityAddedEvent evt)
    {
        TerrainEntity terEnt = (TerrainEntity)evt.AddedXmasEntity;
        TilePosition posinfo = (TilePosition)evt.AddedPosition;
        Point pos = posinfo.Point;
        var  transform = (Transform)Instantiate(Terrain,new Vector3((float)pos.X,(float)pos.Y),Terrain.rotation);
        transform.renderer.sharedMaterial.SetTexture("_MainTex", TextureDictionary.GetTexture(terEnt.TextureType));
    }
}
