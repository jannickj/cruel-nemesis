using UnityEngine;
using System.Collections;
using Cruel.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.Events.UnitEvents;
using Cruel.GameLogic.Modules;
using Assets.UnityLogic;

public class UnitViewHandler : MonoBehaviour {

    public Transform HealthBar;
    public UnityFactory Factory;

    private UnitEntity entity;
    private UnitGraphics graphics;
    private StandardUnitAnimations curAni = StandardUnitAnimations.Idle;
    private GameObject healthbar;

	// Use this for initialization
	void Start () 
    {
        UnitInformation info = this.gameObject.GetComponent<UnitInformation>();
        entity = info.Entity;
        graphics = info.Graphics;
        
        this.gameObject.renderer.material.color = info.ControllerInfo.FocusColor;
        entity.Register(new Trigger<BeginMoveEvent>(OnUnitBeginMove));
        entity.Register(new Trigger<UnitTakesDamageEvent>(OnTakeDamage));

        this.HealthBar.GetComponent<HealthbarView>().SetHealthPct(this.entity.Module<HealthModule>().HealthPct);
        setFrame(currentFrame());
	}
	
	// Update is called once per frame
	void Update () 
    {
        
        UpdateFrame();
	}

    private void OnTakeDamage(UnitTakesDamageEvent evt)
    {
        this.HealthBar.GetComponent<HealthbarView>().SetHealthPct(this.entity.Module<HealthModule>().HealthPct);
    }

    private void OnUnitBeginMove(BeginMoveEvent evt)
    {
        Vector3 v = Factory.ConvertUnitPos(evt.To);
        this.gameObject.transform.localPosition = v;
        this.HealthBar.GetComponent<HealthbarView>().SetPosition(evt.To);
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

    public void UpdateFrame()
    {
        UnitAnimation ani = graphics.GetAnimation(this.curAni);
        ani.NextFrame();
        setFrame(ani.CurrentFrame());

    }

    
}
