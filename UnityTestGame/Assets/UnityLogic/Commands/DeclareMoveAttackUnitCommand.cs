using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Actions;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;
using Cruel.GameLogic.Map;
using Cruel.Library.PathFinding;
using XmasEngineModel.Management.Actions;
using JSLibrary;
using Cruel.GameLogic.Modules;
using Cruel.GameLogic.PlayerCommands;
using Assets.UnityLogic.Gui;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Commands
{
	public class DeclareMoveAttackUnitCommand : Command
	{
        private Color originalColor;
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
            this.originalColor = this.unit.renderer.material.color;
            this.unit.renderer.material.color = Color.red;
            this.lastpos = (TilePosition)unitEntity.Position;
        }

        public override void Update()
        {
       

            GameObject[] gobjs = this.GuiController.GetGameObjectsOnMouse();
            GameObject firstter = gobjs.FirstOrDefault(go => go.gameObject.GetComponent<TerrainInformation>() != null);
            UnitEntity attackUnit = null;
            if (firstter != null)
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
                    
                    bool hasPath;
                    var entities = this.World.GetEntities(tilepos).OfType<UnitEntity>();
                    attackUnit = entities.FirstOrDefault(ent => ent.Module<UnitInfoModule>().Controller != this.GuiController.GuiInfo.Player);
                    
                    RemoveColorOfLastFound();
                    if (attackUnit == null)
                    {
                        hasPath = path.FindFirst(lastpos, tilepos, out foundPath);
                        this.lastFoundEnemy = null;
                    }
                    else
                    {
                        
                        Predicate<TilePosition> goalcond = pos => 
                        {
                            //var pointP = pos.Point;
                            //int difx = Math.Abs(pointP.X - mousePoint.X);
                            //int dify = Math.Abs(pointP.Y - mousePoint.Y);
                            //int attackrange = unitEntity.Module<AttackModule>().AttackRange;
                            //return attackrange >= difx && attackrange >= dify;
                            return unitEntity.Module<AttackModule>().CanReachPoint(pos.Point, mousePoint);
                            
                        };
                        hasPath = path.FindFirst(lastpos, goalcond, pos => new Vector(pos.Point, mousePoint).Distance, out foundPath);
                        this.lastFoundEnemy = attackUnit;
                        var gobj= this.GuiController.Factory.GameObjectFromModel(attackUnit);
                        gobj.renderer.material.color = Color.red;
                        
                        
                    }
                    
                    if (hasPath)
                    {
                        int movelength = unitEntity.Module<MoveModule>().MoveLength-this.pathLength();
                        
                        mousePath = new Path<TileWorld,TilePosition>(foundPath.Map,foundPath.Road.Skip(1).Take(movelength));
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
                    if(mousePath.Road.Last != null)
                        this.lastpos = mousePath.Road.Last.Value;
                    this.GuiController.GuiView.unDrawRoute(mousePath);
                    mousePath = default(Path<TileWorld, TilePosition>);

                    QueueDeclareAction(this.lastFoundEnemy);
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
                QueueDeclareAction();
                this.refindMouse = true;
            }
            else if (Input.GetButtonDown("accept") || enemySelected || this.unitEntity.Module<MoveModule>().MoveLength == pathLength())
            {
                this.unit.renderer.material.color = originalColor;
                if(mousePath.Map !=null)
                    this.GuiController.GuiView.unDrawRoute(this.mousePath);
                RemoveColorOfLastFound();
                Finished = true;
            }
            
        }

        private int pathLength()
        {
            return this.fullroute.Sum(p => p.Road.Count);
        }

        private void QueueDeclareAction(UnitEntity attackUnit = null)
        {
            Path<TileWorld, TilePosition> fullpath;
            if (fullroute.Count == 0)
                fullpath = new Path<TileWorld, TilePosition>(this.WorldAs<TileWorld>());
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

        private void RemoveColorOfLastFound()
        {
            if (lastFoundEnemy != null)
            {
                var lastFoundGobj = GuiController.Factory.GameObjectFromModel(lastFoundEnemy);
                lastFoundGobj.renderer.material.color = Color.white;
            }
        }
    }
}
