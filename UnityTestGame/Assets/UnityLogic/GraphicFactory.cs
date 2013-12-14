using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Game.Cards;
using Assets.UnityLogic.Unit.Animations;
using Assets.UnityLogic.Game.Unit;
using Assets.UnityLogic.Game.Heroes;
using Assets.UnityLogic.Animations.CardAnimations;
using Assets.UnityLogic.Animations;
using Assets.UnityLogic.Animations.UnitAnimations;
using Assets.UnityLogic.Game.Units;

namespace Assets.UnityLogic
{
	public class GraphicFactory
	{
        private static Dictionary<Type, Type> modelToGraphicTypeLookUp = new Dictionary<Type, Type>();

        static GraphicFactory()
        {
            linkUnitToGraphic<GruntUnit, GruntGraphics>();
            linkUnitToGraphic<ArcherUnit, ArcherGraphics>();
            linkUnitToGraphic<BruteUnit, BruteGraphics>();
            linkUnitToGraphic<GoblinPikerUnit, GoblinPikerGraphics>();
            linkUnitToGraphic<MonkUnit, MonkGraphics>();
            linkUnitToGraphic<SerpentUnit, SerpentGraphics>();
            linkUnitToGraphic<WarhoundUnit, WarhoundGraphics>();
            linkUnitToGraphic<DragonUnit, DragonGraphics>();
            linkCardToGraphic<FireballCard, FireballGraphics>();
            //linkCardToGraphic<BloodwyrmSpawnCard, FireballGraphics>();
            linkUnitToGraphic<WizardHero, WizardGraphics>();
        }

        public static GameGraphics ConstuctGraphic(Type modeltype)
        {
            var g = (GameGraphics)Activator.CreateInstance(modelToGraphicTypeLookUp[modeltype]);
            return g;
        }


        private static void linkUnitToGraphic<EntType,GraphicType>() 
            where EntType : UnitEntity 
            where GraphicType : UnitGraphics
        {
            modelToGraphicTypeLookUp.Add(typeof(EntType),typeof(GraphicType));
        }

        private static void linkCardToGraphic<CardType, GraphicType>()
            where CardType : GameCard
            where GraphicType : SpellGraphics
        {
            modelToGraphicTypeLookUp.Add(typeof(CardType), typeof(GraphicType));
        }
	}

}
