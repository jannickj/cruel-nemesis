using UnityEngine;
using System.Collections;
using Assets.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using Assets.GameLogic.Events.UnitEvents;
using Assets.GameLogic.Modules;

public class UnitViewHandler : MonoBehaviour {

    public Transform HealthBar;

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
        Vector3 v;
        UnitHandler.ConvertUnitPos(evt.To, out v);
        this.gameObject.transform.localPosition = v;
        this.HealthBar.GetComponent<HealthbarView>().SetPosition(evt.To);
    }

    private Frame currentFrame()
    {
        return graphics.GetAnimation(curAni).CurrentFrame();
    }

    private void setFrame(Frame f)
    {
        
        this.renderer.sharedMaterial.SetTexture("_MainTex", f.Texture);
        this.renderer.sharedMaterial.SetTextureOffset("_MainTex", f.OffSet);
        this.renderer.sharedMaterial.SetTextureScale("_MainTex", f.Size);
    }

    public void UpdateFrame()
    {
        UnitAnimation ani = graphics.GetAnimation(this.curAni);
        ani.NextFrame();
        setFrame(ani.CurrentFrame());

    }

    
}
