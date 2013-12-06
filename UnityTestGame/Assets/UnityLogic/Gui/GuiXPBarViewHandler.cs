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

        public void Initialize(UnityFactory Factory, GuiInformation ginfo, bool isNotMainPlayer)
        {
            this.Factory = Factory;
            this.ginfo = ginfo;
            this.isNotMainPlayer = isNotMainPlayer;
            ginfo.Player.Register(new Trigger<PlayerGainedXPEvent>(OnXPGain));
        }

        private void OnXPGain(PlayerGainedXPEvent evt)
        {

        }
    }
}
