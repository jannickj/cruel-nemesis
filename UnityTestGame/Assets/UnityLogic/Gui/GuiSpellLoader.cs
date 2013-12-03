using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using XmasEngineModel.Management.Events;
using Cruel.GameLogic.PlayerCommands;
using Cruel.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GuiSpellLoader : MonoBehaviour
	{
        public EngineHandler EngineHandler;
        public UnityFactory Factory;

        private XmasModel engine;

        public GuiSpellLoader()
        {
            
        }

        public void Start()
        {
            this.engine = EngineHandler.EngineModel;
            this.engine.EventManager.Register(new Trigger<ActionCompletedEvent<CastCardCommand>>(OnCastCard));
        }

        private void OnCastCard(ActionCompletedEvent<CastCardCommand> evt)
        {
            GameCard card = evt.Action.CastedCard;
            Spell spell = evt.Action.CastedSpell;
            
            GameObject gobj = (GameObject)this.Factory.GameObjectFromModel(card);

            Factory.TransformCardToSpell(gobj,spell);
            var spellview = gobj.GetComponent<GuiSpellViewHandler>();
            var vec3 = Factory.ConvertPos(new JSLibrary.Data.Point(0, 0));
            vec3.z += 1f;
            var scale = gobj.transform.localScale;
            scale.x = scale.x * 10;
            scale.y = scale.y * 10;
            scale.z = scale.z * 10;
            gobj.transform.localScale = scale;
            spellview.FlyToPos = vec3;
            

            
            //gobj.c
        }
	}
}
