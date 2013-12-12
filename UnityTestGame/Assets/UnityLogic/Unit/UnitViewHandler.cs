using UnityEngine;
using System.Collections;
using Cruel.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Events.UnitEvents;
using Cruel.GameLogic.Modules;
using Assets.UnityLogic;
using XmasEngineModel.Management.Events;
using Cruel.GameLogic.Actions;
using System.Collections.Generic;
using Assets.UnityLogic.Animations;
using XmasEngineExtensions.TileExtension;
using JSLibrary.Data;

public class UnitViewHandler : MonoBehaviour {

    public Transform HealthBar;
    public UnityFactory Factory;

    private UnitEntity entity;
    private UnitGraphic graphics;
    private StandardUnitAnimations curAni = StandardUnitAnimations.Idle;

    private Queue<GameObjectAnimation> awaitingAnimations = new Queue<GameObjectAnimation>();
    private Queue<HandShake> moveHandShakes = new Queue<HandShake>();
    private GameObjectAnimation currentGObjAni = null;
    private Vector direction;

	// Use this for initialization
	void Start () 
    {
        UnitInformation info = this.gameObject.GetComponent<UnitInformation>();
        entity = info.Entity;
        graphics = info.Graphics;
        direction = new Vector(entity.PositionAs<TilePosition>().Point, new Point(0, 0)).Direction;
        //this.gameObject.renderer.material.color = info.ControllerInfo.FocusColor;
        entity.Register(new Trigger<ActionHandShakeInqueryEvent<MoveAction>>(evt => evt.Action.HandShakeRequired = true));
        entity.Register(new Trigger<ActionStartingEvent<MoveAction>>(OnStartMoveAction));
        entity.Register(new Trigger<BeginMoveEvent>(OnUnitBeginMove));
        entity.Register(new Trigger<UnitTakesDamageEvent>(OnTakeDamage));
        entity.Register(new Trigger<ActionStartingEvent<MovePathAction>>(OnUnitBeginPathMove));
        entity.Register(new Trigger<ActionCompletedEvent<MovePathAction>>(OnUnitFinishPathMove));
        
        this.HealthBar.GetComponent<HealthbarView>().SetHealthPct(this.entity.Module<HealthModule>().HealthPct);
        this.HealthBar.parent = this.transform;
        setFrame(currentFrame());
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        UpdateFrame();
        UpdateAnimation();
	}

    private void OnStartMoveAction(ActionStartingEvent<MoveAction> evt)
    {
        if(evt.HandShakeNeeded)
            this.moveHandShakes.Enqueue(evt.HandShake);
    }

    private void OnUnitBeginPathMove(ActionStartingEvent<MovePathAction> evt)
    {
        this.curAni = StandardUnitAnimations.Move;
    }

    private void OnUnitFinishPathMove(ActionCompletedEvent<MovePathAction> evt)
    {
        if (this.currentGObjAni == null)
            this.curAni = StandardUnitAnimations.Idle;
        else
            this.currentGObjAni.Completed += (_, _1) => this.curAni = StandardUnitAnimations.Idle;
    }

    private void OnTakeDamage(UnitTakesDamageEvent evt)
    {
        this.HealthBar.GetComponent<HealthbarView>().SetHealthPct(this.entity.Module<HealthModule>().HealthPct);
    }

    private void OnUnitBeginMove(BeginMoveEvent evt)
    {
        var currentPos = evt.From;
        var endPos = evt.To;

        var worldCurPos = Factory.ConvertUnitPos(currentPos);
        var worldEndPos = Factory.ConvertUnitPos(endPos);

        var speed = Factory.ConvertDurationToSpeed(entity.Module<MoveModule>().MoveDuration);

        var moveani = new MoveTransformAnimation(worldCurPos, worldEndPos, speed);
        moveani.Reset();
        moveani.Begining += (_, _1) =>
            {
                this.direction = (new Vector(currentPos, endPos)).Direction;
            };
        this.awaitingAnimations.Enqueue(moveani);
        

    }

    private Frame currentFrame()
    {
        return graphics.GetAnimation(curAni).CurrentFrame();
    }

    private void setFrame(Frame f)
    {
        
        this.renderer.material.SetTexture("_MainTex", f.Texture);
        this.renderer.material.SetTextureOffset("_MainTex", f.OffSet);
        this.renderer.material.SetTextureScale("_MainTex", f.Size);

    }

    public void UpdateAnimation()
    {
        if (this.currentGObjAni == null)
        {
            if (this.awaitingAnimations.Count > 0)
            {
                this.currentGObjAni = this.awaitingAnimations.Dequeue();
            }
            else if (this.moveHandShakes.Count > 0)
                this.moveHandShakes.Dequeue().PerformHandShake();
        }

        if (this.currentGObjAni != null)
            if (this.currentGObjAni.Update(this.gameObject))
                this.currentGObjAni = null;
    }

    public void UpdateFrame()
    {
        TextureAnimation ani;
        if (graphics.HasAnimation(this.curAni))
            ani = graphics.GetAnimation(this.curAni);
        else
            ani = graphics.GetAnimation(StandardUnitAnimations.Idle);
        ani.NextFrame();
        var scale = this.transform.localScale;
        var newX = ani.HeightToWidthAspect(scale.z);
        if (direction.X != 0)
            newX = newX * direction.X;
        this.transform.localScale = new Vector3(newX, scale.y, scale.z);
        setFrame(ani.CurrentFrame());

    }

    
}
