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
using Cruel.GameLogic.Events;
using JSLibrary.Data;
using Cruel.GameLogic;
using Assets.UnityLogic.Unit;

namespace Assets.UnityLogic.Gui
{
	public class GuiSpellLoader : MonoBehaviour
	{
        public EngineHandler EngineHandler;
        public UnityFactory Factory;
        public GlobalGameSettings Settings;
        public int SpellDistanceFromCenter = 9;
        public int YOffSet = 2;
        private XmasModel engine;
        private DictionaryList<Player, Spell> playersCardStack = new DictionaryList<Player, Spell>();

        public GuiSpellLoader()
        {
            
        }

        public void Start()
        {
            this.engine = EngineHandler.EngineModel;
            this.engine.EventManager.Register(new Trigger<ActionCompletedEvent<CastCardCommand>>(OnCastCard));
            this.engine.EventManager.Register(new Trigger<DequeueAbilityEvent>(evt => evt.Ability is Spell,OnSpellRemovedFromStack));
        }

        private void OnSpellRemovedFromStack(DequeueAbilityEvent evt)
        {
            var spell = (Spell)evt.Ability;
            var card = spell.Creator;
            var spellobj = Factory.GameObjectFromModel(spell);
            Factory.RemoveModel(spell);
            this.playersCardStack.Remove(card.Owner, spell);
            TextureGraphics graphic = GraphicFactory.ConstuctCardGraphic(card.GetType());
        }

        private void OnCastCard(ActionCompletedEvent<CastCardCommand> evt)
        {
            GameCard card = evt.Action.CastedCard;
            Spell spell = evt.Action.CastedSpell;
            int xpos = XPosFromPlayer(card.Owner);
            int ypos = YPosFromPlayer(card.Owner);
            GameObject gobj = this.Factory.GameObjectFromModel(card);
            Factory.TransformCardToSpell(gobj,spell);
            var spellview = gobj.GetComponent<GuiSpellViewHandler>();
            var vec3 = Factory.ConvertPos(new Point(xpos, ypos));
            vec3.z += +1.5f - ZPosFromPlayer(card.Owner);
            var scale = gobj.transform.localScale;
            scale.x = scale.x * 10;
            scale.y = scale.y * 10;
            scale.z = scale.z * 10;
            gobj.transform.localScale = scale;
            spellview.FlyToPos = vec3;

            playersCardStack.Add(card.Owner, spell);

            
            //gobj.c
        }

        private int YPosFromPlayer(Player p)
        {

            ICollection<Spell> cards;
            if (!playersCardStack.TryGetValues(p, out cards))
                return YOffSet;

            return -cards.Count + YOffSet;
        }

        private float ZPosFromPlayer(Player p)
        {
            ICollection<Spell> cards;
            if (!playersCardStack.TryGetValues(p, out cards))
                return 0;

            return cards.Count*0.1f;
        }

        private int XPosFromPlayer(Player p)
        {
            if (p == Settings.MainPlayer)
                return -SpellDistanceFromCenter;
            else
                return SpellDistanceFromCenter;
        }
	}
}
