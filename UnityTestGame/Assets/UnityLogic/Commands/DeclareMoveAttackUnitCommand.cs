using System;
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
using Assets.UnityLogic.Gui;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Commands
{
	public class DeclareMoveAttackUnitCommand : Command
	{
        private UnitEntity unitEntity;
        private GameObject unit;
        private TilePosition lastpos;
        private UnitEntity lastFoundEnemy;
        private LinkedList<Path<TileWorld, TilePosition>> fullroute = new LinkedList<Path<TileWorld, TilePosition>>();
        private Point mousePoint;
        private Path<TileWorld, TilePosition> mousePath;
        private bool refindMouse = false;
        bool enemySelected = false;
        
        public DeclareMoveAttackUnitCommand(GameObject unit, UnitEntity unitEntity)
        {
            
            this.unit = unit;
            this.unitEntity = unitEntity;
            this.unit.renderer.material.color = Color.red;
            this.lastpos = (TilePosition)unitEntity.Position;
        }

        public override void Update()
        {
            GameObject[] gobjs = this.GuiController.GetGameObjectsOnMouse();
            GameObject firstter = gobjs.FirstOrDefault(go => go.gameObject.GetComponent<TerrainInformation>() != null);
            UnitEntity attackUnit = null;
            if (firstter != null && !enemySelected)
            {
                TerrainInformation terinfo = firstter.GetComponent<TerrainInformation>();
                TilePosition tilepos = (TilePosition)terinfo.Terrain.Position;

                if (tilepos != null && (mousePoint != tilepos.Point && lastpos.Point != tilepos.Point) || refindMouse)
                {
                    refindMouse = false;
                    var oldMousePath = mousePath;
                    TilePathFinder path = new TilePathFinder((TileWorld)this.World);
                    mousePoint = tilepos.Point;
                    GuiViewHandler view = this.GuiController.GuiView;
                    Path<TileWorld, TilePosition> foundPath;
                    
                    bool foundpath;
                    var entities = this.World.GetEntities(tilepos).OfType<UnitEntity>();
                    attackUnit = entities.FirstOrDefault(ent => ent.Module<UnitInfoModule>().Controller != this.GuiController.GuiInfo.Player);

                    if (attackUnit == null)
                    {
                        foundpath = path.FindFirst(lastpos, tilepos, out foundPath);
                        this.lastFoundEnemy = null;
                    }
                    else
                    {
                        Predicate<TilePosition> goalcond = pos => (float)attackUnit.Module<AttackModule>().AttackRange >= new Vector(pos.Point, mousePoint).Distance;
                        foundpath = path.FindFirst(lastpos, goalcond, pos => new Vector(pos.Point, mousePoint).Distance, out foundPath);
                        this.lastFoundEnemy = attackUnit;
                    }

                    if (foundpath)
                    {
                        mousePath = new Path<TileWorld,TilePosition>(foundPath.Map,foundPath.Road.Skip(1));
                    }
                    
                    view.drawRoute(mousePath,this.GuiController.GuiInfo.FocusColor);
                    if (oldMousePath.Map != null)
                        view.unDrawRoute(oldMousePath);
                }
            }

            if (Input.GetButtonDown("select_object") && !enemySelected)
            {
                
                if(mousePath.Map != null)
                {
                    
                                        
                    fullroute.AddLast(mousePath);
                    this.lastpos = mousePath.Road.Last.Value;

                    QueueDelcareAction(this.lastFoundEnemy);
                    Debug.Log("Enemy " + this.lastFoundEnemy);
                    this.enemySelected = this.lastFoundEnemy != null;
                    
                    //Finished = true;
                }
            }
            else if (Input.GetButtonDown("deselect_object"))
            {
                enemySelected = false;
                fullroute.RemoveLast();
                if (fullroute.Count == 0)
                    lastpos = this.unitEntity.PositionAs<TilePosition>();
                else
                    lastpos = this.fullroute.Last.Value.Road.Last.Value;
                QueueDelcareAction();
                this.refindMouse = true;
            }
            else if (Input.GetButtonDown("accept") || enemySelected)
            {
                this.unit.renderer.material.color = this.GuiController.GuiInfo.FocusColor;
                if(mousePath.Map !=null)
                    this.GuiController.GuiView.unDrawRoute(this.mousePath);
                Finished = true;
            }
            
        }

        private void QueueDelcareAction(UnitEntity attackUnit = null)
        {
            Path<TileWorld, TilePosition> fullpath;
            if(fullroute.Count == 0)
                fullpath = new Path<TileWorld,TilePosition>(this.WorldAs<TileWorld>());
            else
                fullpath = new Path<TileWorld, TilePosition>(fullroute);

            DeclareMoveAttackCommand declareAction;
            if (attackUnit == null)
                declareAction = new DeclareMoveAttackCommand(this.GuiController.GuiInfo.Player, fullpath);
            else
            {
                declareAction = new DeclareMoveAttackCommand(this.GuiController.GuiInfo.Player, fullpath, attackUnit);
                
            }
            this.unitEntity.QueueAction(declareAction);
        }


    }
}
