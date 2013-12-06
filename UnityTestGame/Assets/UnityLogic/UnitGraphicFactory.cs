using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using Assets.UnityLogic.Unit.UnitTypes;

namespace Assets.UnityLogic
{
	public class UnitGraphicFactory
	{
        private static Dictionary<Type, Type> unittypes = new Dictionary<Type, Type>();

        static UnitGraphicFactory()
        {
            linkUnitToGraphic<GruntUnit, GruntGraphics>();
            linkUnitToGraphic<DragonUnit, DragonGraphics>();
        }

        public static UnitGraphics ConstuctUnitGraphic(Type unittype)
        {
            var g = (UnitGraphics)Activator.CreateInstance(unittypes[unittype]);
            g.LoadAnimations();
            foreach (UnitAnimation ua in g.Animations)
            {
                ua.SetTexture(TextureDictionary.GetTexture("unit_"+ua.TextureId));
            }
            return g;
        }

        private static void linkUnitToGraphic<EntType,GraphicType>() 
            where EntType : UnitEntity 
            where GraphicType : UnitGraphics
        {
            unittypes.Add(typeof(EntType),typeof(GraphicType));
        }
	}

}
