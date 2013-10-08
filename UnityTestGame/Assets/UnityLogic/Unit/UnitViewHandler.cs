using UnityEngine;
using System.Collections;
using Assets.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;

public class UnitViewHandler : MonoBehaviour {

    private UnitEntity entity;
    private UnitGraphics graphics;
    private StandardUnitAnimations curAni = StandardUnitAnimations.Idle;

	// Use this for initialization
	void Start () 
    {
        UnitInformation info = this.gameObject.GetComponent<UnitInformation>();
        entity = info.Entity;
        graphics = info.Graphics;

        entity.Register(new Trigger<BeginMoveEvent>(OnUnitBeginMove));

        setFrame(currentFrame());
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateFrame();
	}

    private void OnUnitBeginMove(BeginMoveEvent evt)
    {
        Vector3 v;
        UnitHandler.ConvertUnitPos(evt.To, out v);
        this.gameObject.transform.localPosition = v;
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
