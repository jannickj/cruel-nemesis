using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.SpellSystem;
using UnityEngine;
using Cruel.GameLogic.PlayerCommands;

namespace Assets.UnityLogic.Commands
{
	public class DeclareCastCardCommand : Command
	{
        private GameCard gameCard;
        private int targetIndex = 0;
        private Color formerSelectedsColor;
        private GameObject currentSelected;
        private object modelSelected;
        private bool selectedAccepted = false;
        private List<Dictionary<GameObject, object>> targets = new List<Dictionary<GameObject, object>>();

        public DeclareCastCardCommand(GameCard gameCard)
        {
            // TODO: Complete member initialization
            this.gameCard = gameCard;
            targetsAtIndex(0);
            
        }

        private bool targetsAtIndex(int index)
        {
            if (gameCard.TargetCounts.Length >= index + 1)
            {
                targets.Add(new Dictionary<GameObject, object>());
                return true;
            }
            else
                return false;
        }

        public override void Update()
        {
            var objs = this.GuiController.GetGameObjectsOnMouse().Where(gobj => !this.targets[targetIndex].ContainsKey(gobj));
            GameObject newSelected = null;
  

            foreach (GameObject gobj in objs)
            {
                if (gobj == currentSelected)
                {
                    newSelected = currentSelected;
                    break;
                }
                

                var modelobj = this.GuiController.GetModelObjectsFromGameObjects(gobj);
                if (gameCard.TestTarget(targetIndex, modelobj))
                {
                    newSelected = gobj;
                    modelSelected = modelobj;
                    selectedAccepted = true;
                    break;
                }
            }

            if (newSelected == null)
            {

                newSelected = objs.FirstOrDefault();
                modelSelected = null;
                selectedAccepted = false;
            }

            if (selectedAccepted && Input.GetButtonDown("select_object"))
            {
                RestoreColor();
                selectObject(newSelected, modelSelected);
                return;
            }

            if (newSelected == currentSelected)
                return;

            RestoreColor();
            
            if (newSelected == null)
                return;
            this.formerSelectedsColor = newSelected.renderer.material.color;
            if (this.selectedAccepted)
                newSelected.renderer.material.color = Color.yellow;
            else
                newSelected.renderer.material.color = Color.red;

            this.currentSelected = newSelected;
            
        }

        private void RestoreColor()
        {
            if (currentSelected != null)
                currentSelected.renderer.material.color = this.formerSelectedsColor;
        }
        
        private void selectObject(GameObject gameObject, object modelSelected)
        {
            var targetdic = this.targets[targetIndex];
            targetdic.Add(gameObject, modelSelected);
            if (targetdic.Count == this.gameCard.TargetCounts[targetIndex])
            {
                targetIndex++;
                if (!this.targetsAtIndex(targetIndex))
                {
                    var tars = targets.Select(dic => dic.Values).Cast<IEnumerable<object>>();
                    this.ActionManager.Queue(new CastCardCommand(this.GuiController.GuiInfo.Player, this.gameCard, tars, new List<Mana>(new Mana[]{Mana.Arcane})));
                    this.Finished = true;
                }
                
            }
            
        }
    }
}
