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
using System.Collections.Generic;


public class MapHandler : MonoBehaviour {

    public EngineHandler Engine;
    public UnityFactory Factory;
    private Dictionary<TerrainEntity, Transform> termap = new Dictionary<TerrainEntity, Transform>();

	// Use this for initialization
	void Start () {
        XmasModel eng = Engine.EngineModel;
        eng.EventManager.Register(new Trigger<EntityAddedEvent>(ent => ent.AddedXmasEntity is TerrainEntity,OnTerrainEntity));
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Transform this[TerrainEntity ent]
    {
        get
        {
            return termap[ent];
        }
    }

    private void OnTerrainEntity(EntityAddedEvent evt)
    {
        TerrainEntity terEnt = (TerrainEntity)evt.AddedXmasEntity;
        TilePosition posinfo = (TilePosition)evt.AddedPosition;
        Point pos = posinfo.Point;

        var transform = Factory.CreateTile(terEnt, posinfo); 

        transform.gameObject.AddComponent<TerrainInformation>();
        var terinfo = transform.gameObject.GetComponent<TerrainInformation>();
        terinfo.SetTerrain(terEnt);
        this.termap[terEnt] = transform;
        transform.renderer.sharedMaterial.SetTexture("_MainTex", TextureDictionary.GetTexture(terEnt.TextureType));
    }
}
