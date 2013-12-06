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
        }

        public static TextureGraphics ConstuctUnitGraphic(Type unittype)
        {
            var g = (TextureGraphics)Activator.CreateInstance(unittypes[unittype]);
            g.LoadAnimations();
            foreach (TextureAnimation ua in g.Animations)
            {
                ua.SetTexture(TextureDictionary.GetTexture("unit_"+ua.TextureId));
            }
            return g;
        }

        public static TextureGraphics ConstuctCardGraphic(Type cardtype)
        {
            var g = (TextureGraphics)Activator.CreateInstance(unittypes[cardtype]);
            g.LoadAnimations();
            foreach (TextureAnimation ua in g.Animations)
            {
                ua.SetTexture(TextureDictionary.GetTexture("spell_" + ua.TextureId));
            }
            return g;
        }

        private static void linkUnitToGraphic<EntType,GraphicType>() 
            where EntType : UnitEntity 
            where GraphicType : TextureGraphics
        {
            unittypes.Add(typeof(EntType),typeof(GraphicType));
        }

        private static void linkCardToGraphic<CardType, GraphicType>()
            where CardType : GameCard
            where GraphicType : TextureGraphics
        {
            unittypes.Add(typeof(CardType), typeof(GraphicType));
        }
	}

}
