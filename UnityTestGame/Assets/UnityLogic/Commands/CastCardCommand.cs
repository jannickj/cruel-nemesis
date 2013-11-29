using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using UnityEngine;

namespace Assets.UnityLogic.Commands
{
	public class CastCardCommand : Command
	{
        private GameCard gameCard;
        private int targetIndex = 0;

        public CastCardCommand(GameCard gameCard)
        {
            // TODO: Complete member initialization
            this.gameCard = gameCard;
        }



        public override void Update()
        {
            var objs = this.GuiController.GetGameObjectsOnMouse();
            foreach (GameObject gobj in objs)
            {
                var modelobj = this.GuiController.GetModelObjectsFromGameObjects(gobj);
                if (gameCard.TestTarget(targetIndex, modelobj))
                {

                }
            }

        }
    }
}
