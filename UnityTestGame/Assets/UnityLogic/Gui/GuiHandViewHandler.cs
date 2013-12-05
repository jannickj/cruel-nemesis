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
using XmasEngineModel.Management.Events;
using Cruel.GameLogic.PlayerCommands;

namespace Assets.UnityLogic.Gui
{
	public class GuiHandViewHandler : MonoBehaviour
	{
        public UnityFactory Factory { get; set; }
        private Camera playerCam;
        private float x, y, width, height;
        private EventManager evtman;
        private Player player;
        private Dictionary<GameCard, Transform> currentHand = new Dictionary<GameCard, Transform>();
        private LinkedList<Transform> cardOrder = new LinkedList<Transform>();
        

        public void Initialize(Camera playerCam, float x, float y, float width, float height, EventManager evtman, Player player)
        {
            this.player = player;
            this.playerCam = playerCam;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.evtman = evtman;
            this.evtman.Register(new Trigger<CardDrawnEvent>(evt => evt.Player == player, OnCardDrawn));
            this.evtman.Register(new Trigger<PlayerGainedPriorityEvent>(OnPlayerGainPriority));
            this.evtman.Register(new Trigger<ActionCompletedEvent<CastCardCommand>>(OnCastCard));
        }

        private void OnCastCard(ActionCompletedEvent<CastCardCommand> evt)
        {
            GameCard card = evt.Action.CastedCard;
            removeCard(card);
            
        }

        private void removeCard(GameCard card)
        {
            if (!currentHand.ContainsKey(card))
                return;
            var cardobj = this.currentHand[card];
            this.currentHand.Remove(card);
            cardOrder.Remove(cardobj);

            this.PositionHand();
        }

        private void OnPlayerGainPriority(PlayerGainedPriorityEvent evt)
        {
            foreach(Transform card in cardOrder)
                card.gameObject.SetActive(this.player.HasPriority);
        }

        private void OnCardDrawn(CardDrawnEvent evt)
        {
            Transform cardobj = Factory.CreateCard(evt.DrawnCard);
           
            cardOrder.AddLast(cardobj);            
            cardobj.parent = playerCam.transform;
            currentHand.Add(evt.DrawnCard, cardobj);
            
            PositionHand();
            cardobj.gameObject.SetActive(this.player.HasPriority);
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
