using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using Cruel.GameLogic.SpellSystem;
using Cruel.Map.Terrain;
using XmasEngineExtensions.TileExtension;
using Assets.UnityLogic.Gui;
using JSLibrary.Data;
using Assets.UnityLogic.Unit;
using Cruel.GameLogic.Unit;

namespace Assets.UnityLogic
{
	public class UnityFactory : MonoBehaviour
	{

        public EngineHandler Engine;
        public Transform CardTemplate;
        public Transform TerrainTemplate;
        public Transform UnitTemplate;
        public Transform HealthBarTemplate;
        public GUITexture ManaBarTemplate;
        public GUITexture EmptyManaBarTemplate;

        private Dictionary<object, GameObject> gameobjLookUp = new Dictionary<object, GameObject>();

        public GUITexture CreateEmptyManaBar()
        {
            return (GUITexture)GameObject.Instantiate(EmptyManaBarTemplate);
        }

        public GUITexture CreateManaBar(Mana mana)
        {
            var texture = (GUITexture)GameObject.Instantiate(ManaBarTemplate);

            switch (mana)
            {
                case Mana.Arcane:
                    texture.color = Color.blue;
                    break;
                case Mana.Fury:
                    texture.color = Color.red;
                    break;
            }

            return texture;
        }

        public GameObject GameObjectFromModel(object modelobj)
        {
            return gameobjLookUp[modelobj];
        }

        public Transform CreateTile(TerrainEntity terEnt, TilePosition posinfo)
        {
            var pos = posinfo.Point;
            var tileobj = (Transform)Instantiate(TerrainTemplate, new Vector3(-(float)pos.X, (float)pos.Y), TerrainTemplate.rotation);
            gameobjLookUp.Add(terEnt, tileobj.gameObject);
            return tileobj;
        }

        public Transform CreateCard(GameCard gameCard)
        {
            var cardobj = (Transform)GameObject.Instantiate(CardTemplate);
            var cardinfo = cardobj.gameObject.AddComponent<CardInformation>();
            cardinfo.Card = gameCard;
            gameobjLookUp.Add(gameCard, cardobj.gameObject);
            return cardobj;
        }

        public  Vector3 ConvertUnitPos(Point pos)
        {
            return new Vector3(-(float)pos.X, (float)pos.Y + 0.5f, 0.3f);
        }

        public Transform CreateUnit(UnitEntity unitEnt, TilePosition posinfo)
        {
            var pos = posinfo.Point;
            Quaternion ur = UnitTemplate.rotation;
            Quaternion rot = new Quaternion(ur.x + 0.14f, ur.y, ur.z, ur.w);
            Vector3 unitvec = ConvertUnitPos(pos);
            var unitobj = (Transform)Instantiate(UnitTemplate, unitvec, rot);
            var info = unitobj.gameObject.AddComponent<UnitInformation>();

            unitobj.gameObject.AddComponent<UnitViewHandler>();
            unitobj.gameObject.AddComponent<UnitControllerHandler>();

            info.SetEntity(unitEnt);
            UnitGraphic graphic = GraphicFactory.ConstuctUnitGraphic(unitEnt.getUnitType());
            info.SetGraphics(graphic);

            var viewhandler = unitobj.gameObject.GetComponent<UnitViewHandler>();
            viewhandler.Factory = this;
            viewhandler.HealthBar = (Transform)Instantiate(this.HealthBarTemplate);
            viewhandler.HealthBar.GetComponent<HealthbarView>().SetPosition(pos);
            this.gameobjLookUp.Add(unitEnt,unitobj.gameObject);
            return unitobj;
        }

        //public Transform CreateGraphic(TextureAnimation animation)
        //{
        //    var graphicsObj = (Transform)Instantiate(UnitTemplate);
        //
        //}

        public void TransformCardToSpell(GameObject cardObj, Spell spell)
        {
            CardInformation cinfo = cardObj.GetComponent<CardInformation>();
            this.gameobjLookUp.Remove(cinfo.Card);

            var components = cardObj.GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                component.enabled = false;
            }

            this.gameobjLookUp.Add(spell, cardObj);
            var spellinfo = cardObj.AddComponent<SpellInformation>();
            spellinfo.Spell = spell;
            cardObj.AddComponent<GuiSpellViewHandler>();
        }

        public Vector3 ConvertPos(Point point)
        {
            return new Vector3(-(float)point.X, (float)point.Y, 0.0f);
        }

        public void RemoveModel(object model)
        {
            var gobj = this.gameobjLookUp[model];
            GameObject.Destroy(gobj);
            
        }

        public float ConvertDurationToSpeed(int msDur)
        {
            return (TerrainTemplate.localScale.x / (float)msDur)*1000 ;
        }
    }
}
