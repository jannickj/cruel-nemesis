using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Cruel.GameLogic.Events;
using XmasEngineModel.Management;

namespace Assets.UnityLogic.Gui
{
    public class GuiXPBarViewHandler : MonoBehaviour
    {
        private UnityFactory Factory;
        private GuiInformation ginfo;
        private bool isNotMainPlayer;
        private float initwidth;
        private GUITextureAutoScaler xpscaler;

        public void Initialize(UnityFactory Factory, GuiInformation ginfo, bool isNotMainPlayer)
        {
            this.Factory = Factory;
            this.ginfo = ginfo;
            this.isNotMainPlayer = isNotMainPlayer;
            ginfo.Player.Register(new Trigger<PlayerGainedXPEvent>(OnXPGain));
            xpscaler = this.ginfo.XPBar.GetComponent<GUITextureAutoScaler>();
            initwidth = xpscaler.CurSize.width;
            CheckXp();
        }

        private void OnXPGain(PlayerGainedXPEvent evt)
        {
            CheckXp();
        }

        private void CheckXp()
        {
            float xpCur = this.ginfo.Player.CurrentXP;
            float xpForNextLevel = (float)this.ginfo.Player.Rewarder.XPOfNextLevel();
            float xpForThisLevel = (float)this.ginfo.Player.Rewarder.XPOfThisLevel();
            float xppct = xpForNextLevel == -1 ? 1 : (xpCur - xpForThisLevel)/(xpForNextLevel-xpForThisLevel);
            
            var rect = this.xpscaler.CurSize;

            rect.width = this.initwidth - (this.initwidth * xppct);
            this.xpscaler.CurSize = rect;
        }
    }
}
