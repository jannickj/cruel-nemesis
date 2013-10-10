using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.GameLogic.Unit;
using Assets.GameLogic.Actions;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;

namespace Assets.UnityLogic.Commands
{
	public class MoveUnitCommand : Command
	{
        private UnitEntity unitEntity;
        private GameObject unit;

        
        public MoveUnitCommand(GameObject unit, UnitEntity unitEntity)
        {
            
            this.unit = unit;
            this.unitEntity = unitEntity;
            this.unit.renderer.material.color = Color.red;
        }

        public override void Update()
        {
            if (Event.current.type == EventType.MouseDown && Input.GetButtonDown("select_object"))
            {
                GameObject[] gobjs = this.PlayerController.GetGameObjectsOnMouse();
                GameObject firstter = gobjs.FirstOrDefault(go => go.gameObject.GetComponent<TerrainInformation>() != null);
                if (firstter != null)
                {
                    Debug.Log(firstter);
                    TerrainInformation terinfo = firstter.GetComponent<TerrainInformation>();
                    TilePosition tilepos = (TilePosition)terinfo.Terrain.Position;
                    TilePosition unitpos = (TilePosition)unitEntity.Position;
                    Vector v = new Vector(unitpos.Point,tilepos.Point);
                    unitEntity.QueueAction(new MoveAction(v, 0));
                    this.unit.renderer.material.color = Color.white;
                    Finished = true;
                }
            }
        }
    }
}
