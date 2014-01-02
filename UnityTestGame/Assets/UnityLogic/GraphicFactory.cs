using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Unit;
using Assets.UnityLogic.Unit;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Unit.Animations;
using Assets.UnityLogic.Animations.CardAnimations;
using Assets.UnityLogic.Animations;
using Assets.UnityLogic.Animations.UnitAnimations;
using Assets.UnityLogic.Animations.SpellAnimations;
using Assets.UnityLogic.Exceptions;
using CruelGameData.GameLogic.Game.Units;
using CruelGameData.GameLogic.Game.Cards;
using CruelGameData.GameLogic.Game;
using CruelGameData.GameLogic.Game.Heroes;
using CruelGameData.GameLogic.Game.Unit;

namespace Assets.UnityLogic
{
	public class GraphicFactory
	{
        private static Dictionary<Type, Type> modelToGraphicTypeLookUp = new Dictionary<Type, Type>();

        static GraphicFactory()
        {
            linkUnitToGraphic<UnitEntity, DefaultUnitGraphics>();
            linkUnitToGraphic<ArcherUnit, ArcherGraphics>();
            linkUnitToGraphic<BruteUnit, BruteGraphics>();
            linkUnitToGraphic<GoblinPikerUnit, GoblinPikerGraphics>();
            linkUnitToGraphic<MonkUnit, MonkGraphics>();
            linkUnitToGraphic<SerpentUnit, SerpentGraphics>();
            linkUnitToGraphic<WarhoundUnit, WarhoundGraphics>();
            linkUnitToGraphic<DragonUnit, DragonGraphics>();
            linkUnitToGraphic<WizardHero, WizardGraphics>();
            linkUnitToGraphic<WarlordHero, WarlordGraphics>();
            linkUnitToGraphic<ZombieUnit, ZombieGraphics>();

            linkCardToGraphic<FireballCard, FireballGraphics>();
            linkCardToGraphic<RaiseDeadCard, RaiseDeadGraphics>();
            linkCardToGraphic<LightningBoltCard, LightningBoltGraphics>();
            linkCardToGraphic<InspirationCard, InspirationGraphics>();
            linkCardToGraphic<BattlecryCard, BattlecryGraphics>();
            linkCardToGraphic<SummoningSingleCard, SummoningGraphics>();
        }

        public static GameGraphics ConstuctGraphic(Type modeltype)
        {
            var mtype = modeltype;
            while (!modelToGraphicTypeLookUp.ContainsKey(mtype))
            {
                mtype = mtype.BaseType;
                if (mtype == typeof(object))
                    throw new GraphicsNotFoundException(modeltype);
            }
            var g = (GameGraphics)Activator.CreateInstance(modelToGraphicTypeLookUp[mtype]);
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
