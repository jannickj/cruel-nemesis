using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic;
using Cruel.GameLogic.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GuiHandViewHandler : MonoBehaviour
	{
        private Camera playerCam;
        private Transform CardTemplate;
        private float x, y, width, height;
        private EventManager evtman;
        private Player player;
        private Dictionary<GameCard, Transform> currentHand = new Dictionary<GameCard, Transform>();
        private LinkedList<Transform> cardOrder = new LinkedList<Transform>();

        public void Initialize(Camera playerCam, float x, float y, float width, float height, EventManager evtman, Player player, Transform CardTemplate)
        {
            this.playerCam = playerCam;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.evtman = evtman;
            this.CardTemplate = CardTemplate;
            this.evtman.Register(new Trigger<CardDrawnEvent>(evt => evt.Player == player, OnCardDrawn));
        }

        private void OnCardDrawn(CardDrawnEvent evt)
        {
            Transform cardobj = (Transform)GameObject.Instantiate(CardTemplate);
            var cardinfo = cardobj.gameObject.AddComponent<CardInformation>();
            cardinfo.Card = evt.DrawnCard;
            cardOrder.AddLast(cardobj);            
            cardobj.parent = playerCam.transform;
            PositionHand();

        }

        private void PositionHand()
        {
            int count = cardOrder.Count;
            int index = 0;
            foreach (Transform cardobj in cardOrder)
            {
                float offset = cardobj.localScale.x * index - 0.5f * cardobj.localScale.x * count;

                var vp = playerCam.ViewportToWorldPoint(new Vector3(x, 0, 1f));

                cardobj.position = new Vector3(vp.x + offset, vp.y + cardobj.localScale.z * 0.5f, vp.z);
                
                index++;
            }

        }


        void Update()
        {

        }
	}
}
