using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using Assets.UnityLogic.Unit.UnitTypes;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Game.Cards;
using Assets.UnityLogic.Unit.Animations;
using Assets.UnityLogic.Game.Unit;
using Assets.UnityLogic.Game.Heroes;
using Assets.UnityLogic.Animations.CardAnimations;

namespace Assets.UnityLogic
{
	public class GraphicFactory
	{
        private static Dictionary<Type, Type> unittypes = new Dictionary<Type, Type>();

        static GraphicFactory()
        {
            linkUnitToGraphic<GruntUnit, GruntGraphics>();
            linkUnitToGraphic<DragonUnit, DragonGraphics>();
            linkCardToGraphic<FireballCard, FireballGraphics>();
            linkUnitToGraphic<WizardHero, WizardGraphics>();
        }

        public static UnitGraphic ConstuctUnitGraphic(Type unittype)
        {
            var g = (UnitGraphic)Activator.CreateInstance(unittypes[unittype]);
            g.LoadAnimations();
            foreach (TextureAnimation ua in g.Animations)
            {
                ua.SetTexture(TextureDictionary.GetTexture("unit_"+ua.TextureId));
            }
            return g;
        }

        public static UnitGraphic ConstuctCardGraphic(Type cardtype)
        {
            var g = (UnitGraphic)Activator.CreateInstance(unittypes[cardtype]);
            g.LoadAnimations();
            foreach (TextureAnimation ua in g.Animations)
            {
                ua.SetTexture(TextureDictionary.GetTexture("spell_" + ua.TextureId));
            }
            return g;
        }

        private static void linkUnitToGraphic<EntType,GraphicType>() 
            where EntType : UnitEntity 
            where GraphicType : UnitGraphic
        {
            unittypes.Add(typeof(EntType),typeof(GraphicType));
        }

        private static void linkCardToGraphic<CardType, GraphicType>()
            where CardType : GameCard
            where GraphicType : UnitGraphic
        {
            unittypes.Add(typeof(CardType), typeof(GraphicType));
        }
	}

}
