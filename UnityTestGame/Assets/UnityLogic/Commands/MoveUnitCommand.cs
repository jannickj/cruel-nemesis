﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.GameLogic.Unit;
using Assets.GameLogic.Actions;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Assets.GameLogic.Map;
using Assets.Library.PathFinding;
using XmasEngineModel.Management.Actions;
using JSLibrary;
using Assets.GameLogic.Modules;
using Assets.GameLogic.PlayerCommands;

namespace Assets.UnityLogic.Commands
{
	public class MoveUnitCommand : Command
	{
        private UnitEntity unitEntity;
        private GameObject unit;
        //private List<
        
        public MoveUnitCommand(GameObject unit, UnitEntity unitEntity)
        {
            
            this.unit = unit;
            this.unitEntity = unitEntity;
            this.unit.renderer.material.color = Color.red;
        }

        public override void Update()
        {
            if (Input.GetButtonDown("select_object"))
            {
                GameObject[] gobjs = this.GuiController.GetGameObjectsOnMouse();
                GameObject firstter = gobjs.FirstOrDefault(go => go.gameObject.GetComponent<TerrainInformation>() != null);
                if (firstter != null)
                {
                    Debug.Log(firstter);
                    TerrainInformation terinfo = firstter.GetComponent<TerrainInformation>();
                    TilePosition tilepos = (TilePosition)terinfo.Terrain.Position;
                    TilePosition unitpos = (TilePosition)unitEntity.Position;
                    TilePathFinder path = new TilePathFinder((TileWorld)this.World);
                    bool foundPath;
                    Path<TileWorld,TilePosition> route = path.FindFirst(unitpos, tilepos, out foundPath);

                    var declareAction = new DeclareMoveAttackCommand(this.GuiController.GuiInfo.Player,route);

                    this.unitEntity.QueueAction(declareAction);

                    this.unit.renderer.material.color = this.GuiController.GuiInfo.FocusColor;
                    Finished = true;
                }
            }
            
        }
    }
}
