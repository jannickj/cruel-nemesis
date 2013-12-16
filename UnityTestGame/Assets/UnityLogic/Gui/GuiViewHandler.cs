using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic;
using XmasEngineModel.EntityLib;
using XmasEngineExtensions.TileExtension;
using Cruel.Library.PathFinding;
using Cruel.Map.Terrain;
using Cruel.GameLogic.TurnLogic;
using JSLibrary.Data;
using Assets.UnityLogic.Game.Modules;
using Cruel.GameLogic.Modules;

namespace Assets.UnityLogic.Gui
{
	public class GuiViewHandler : MonoBehaviour
	{
        public MapHandler MapHandler;
        private GuiInformation guiinfo;
        public UnityFactory Factory;
        public EngineHandler Engine;
        private XmasModel engmodel;
        private Player currentTurnOwner;
        private Dictionary<XmasEntity, Path<TileWorld, TilePosition>> routes = new Dictionary<XmasEntity, Path<TileWorld, TilePosition>>();
        private Dictionary<Point, Stack<Color>> drawnSquares = new Dictionary<Point, Stack<Color>>();
        private Color defaultColor = Color.white;
        private GUIText HpText;
        private float initHPWidth;
        public bool isNotMainPlayer;

        private static Vector2 HPTEXT_OFFSET = new Vector2(250, 30);

        public void Initialize()
        {
            
            guiinfo = this.gameObject.GetComponent<GuiInformation>();
            engmodel = Engine.EngineModel;

            engmodel.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(OnPlayerPriority));
            engmodel.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(OnTurnChanged));
            engmodel.EventManager.Register(new Trigger<PhaseChangedEvent>(OnPhaseChanged));
            engmodel.EventManager.Register(new Trigger<PlayerDeclareMoveAttackEvent>(evt => evt.Player == this.guiinfo.Player,OnPlayerDeclare));
            engmodel.EventManager.Register(new Trigger<PhaseChangedEvent>(OnPhaseChangedEvt));
            engmodel.EventManager.Register(new Trigger<CardDrawnEvent>(evt => evt.Player == this.guiinfo.Player, OnPlayerDrawCard));
            setupHpText();
            this.initHPWidth = this.guiinfo.HealthBar.GetComponent<GUITextureAutoScaler>().CurPlacement.width;
        }

        private void setupHpText()
        {
            HpText = Factory.CreateText();
            HpText.transform.parent = guiinfo.HealthBar.transform;
            var hpBarPos = this.guiinfo.HealthBar.GetComponent<GUITextureAutoScaler>().CurPlacement;
            if(this.isNotMainPlayer)
                hpBarPos.x += HPTEXT_OFFSET.x;
            else
                hpBarPos.x += HPTEXT_OFFSET.x;
            hpBarPos.y += HPTEXT_OFFSET.y;

            var pos = HpText.transform.position;
            pos.z += 10f;
            HpText.transform.position = pos;
            HpText.color = Color.red;
            HpText.GetComponent<GUITextureAutoScaler>().CurPlacement = hpBarPos;
            
            updateHpText();
        }

        private void updateHpText()
        {
            
            var hpmod = this.guiinfo.Player.Hero.Module<HealthModule>();
            HpText.text = hpmod.Health + " / " + hpmod.MaxHealth;

        }

        void Update()
        {
            SetHealthPct(this.guiinfo.Player.Hero.Module<HealthModule>().HealthPct);
            updateHpText();
        }

        public void SetHealthPct(float pct)
        {
            var val = ( pct / 100f);
            var tex = this.guiinfo.HealthBar.GetComponent<GUITextureAutoScaler>();
            var rect = tex.CurPlacement;
            rect.width = val * initHPWidth;
            tex.CurPlacement = rect;
        }

        private void OnPlayerDrawCard(CardDrawnEvent evt)
        {
            //var cardobj = (Transform)GameObject.Instantiate(this.guiinfo.CardTemplate);

            //var cardhandler = cardobj.gameObject.AddComponent<CardViewHandler>();
            //var card = evt.DrawnCard;
            //Texture tex = TextureDictionary.GetTexture("cards_"+card.Module<GraphicsModule>().TextureId);
            //cardhandler.Texture = tex;
            //cardhandler.Card = card;
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
            this.drawRoute(evt.MoveAction.Path,Color.red);
        }


        public void unDrawRoute(Path<TileWorld, TilePosition> path)
        {
            
            foreach (TilePosition pos in path.Road)
            {
                Stack<Color> drawn;
                Color color;
                

                if (!drawnSquares.TryGetValue(pos.Point, out drawn) || drawn.Count == 0)
                {
                    color = defaultColor;
                }
                else
                {
                    drawn.Pop();

                    if (drawn.Count == 0)
                        color = defaultColor;
                    else
                        color = drawnSquares[pos.Point].Peek();
                }

                var terrain = this.engmodel.World.GetEntities(pos).OfType<TerrainEntity>().First();
                Transform terobj = this.MapHandler[terrain];
                terobj.renderer.material.color = color;
                
            }
        }

        public void drawRoute(Path<TileWorld, TilePosition> path, Color roadColor)
        {
            foreach (TilePosition pos in path.Road)
            {

                Stack<Color> drawn;
                if (!drawnSquares.TryGetValue(pos.Point, out drawn))
                {
                    drawn = new Stack<Color>();
                    drawnSquares[pos.Point] = drawn;
                }

                drawn.Push(roadColor);
                

                var terrain = this.engmodel.World.GetEntities(pos).OfType<TerrainEntity>().First();
                Transform terobj = this.MapHandler[terrain];
                terobj.renderer.material.color = roadColor;
            }
        }

        public void OnPhaseChanged(PhaseChangedEvent evt)
        {
            
            if (currentTurnOwner != guiinfo.Player)
                return;
            guiinfo.ResetPhaseColor(evt.OldPhase);
            guiinfo.SetPhaseColor(evt.NewPhase, guiinfo.FocusColor);
            
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
