using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using Assets.GameLogic;
using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension;
using Assets.Library.PathFinding;
using Assets.Map.Terrain;
using Assets.GameLogic.TurnLogic;
using JSLibrary.Data;

namespace Assets.UnityLogic.Gui
{
	public class GuiViewHandler : MonoBehaviour
	{
        public MapHandler MapHandler;
        private GuiInformation guiinfo;
        public EngineHandler Engine;
        private XmasModel engmodel;
        private Player currentTurnOwner;
        private Dictionary<XmasEntity, Path<TileWorld, TilePosition>> routes = new Dictionary<XmasEntity, Path<TileWorld, TilePosition>>();
        private Dictionary<Point, int> drawnSquares = new Dictionary<Point, int>();

        void Start()
        {
            guiinfo = this.gameObject.GetComponent<GuiInformation>();

            engmodel = Engine.EngineModel;

            engmodel.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(OnPlayerPriority));
            engmodel.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(OnTurnChanged));
            engmodel.EventManager.Register(new Trigger<PhaseChangedEvent>(OnPhaseChanged));
            engmodel.EventManager.Register(new Trigger<PlayerDeclareMoveAttackEvent>(OnPlayerDeclare));
            engmodel.EventManager.Register(new Trigger<PhaseChangedEvent>(OnPhaseChangedEvt));
        }


        void Update()
        {

        }

        public void OnPhaseChangedEvt(PhaseChangedEvent evt)
        {
            if (evt.NewPhase != Phases.Move)
                return;

            foreach (var kv in routes)
            {
                this.unDrawRoute(kv.Value);   
            }
            routes.Clear();
        }
                

        public void OnPlayerDeclare(PlayerDeclareMoveAttackEvent evt)
        {
            Path<TileWorld,TilePosition> path;
            if(this.routes.TryGetValue(evt.Entity,out path))
            {
                this.unDrawRoute(path);
                this.routes.Remove(evt.Entity);
            }

            this.routes.Add(evt.Entity, evt.MoveAction.Path);
            this.drawRoute(evt.MoveAction.Path);
        }


        public void unDrawRoute(Path<TileWorld, TilePosition> path)
        {
            foreach (TilePosition pos in path.Road)
            {
                drawnSquares[pos.Point]--;
                int left = drawnSquares[pos.Point];
                if (left != 0)
                    continue;
                var terrain = this.engmodel.World.GetEntities(pos).OfType<TerrainEntity>().First();
                Transform terobj = this.MapHandler[terrain];
                terobj.renderer.material.color = Color.white;
                
            }
        }

        public void drawRoute(Path<TileWorld, TilePosition> path)
        {
            foreach (TilePosition pos in path.Road)
            {

                var terrain = this.engmodel.World.GetEntities(pos).OfType<TerrainEntity>().First();
                Transform terobj = this.MapHandler[terrain];
                terobj.renderer.material.color = Color.red;

                int drawn;
                if (!drawnSquares.TryGetValue(pos.Point, out drawn))
                    drawn = 0;
                drawnSquares[pos.Point] = drawn + 1;
            }
        }

        public void OnPhaseChanged(PhaseChangedEvent evt)
        {
            if (currentTurnOwner != guiinfo.Player)
                return;
            guiinfo[evt.OldPhase].color = Color.white;
            guiinfo[evt.NewPhase].color = guiinfo.FocusColor;
        }

        public void OnTurnChanged(PlayersTurnChangedEvent evt)
        {            
            currentTurnOwner = evt.PlayersTurn;
        }

        public void OnPlayerPriority(PlayerGainedPriorityEvent evt)
        {
            if (evt.Player == guiinfo.Player)
                guiinfo.Portrait.color = guiinfo.FocusColor;
            else
                guiinfo.Portrait.color = Color.white;

        }
    }
}
