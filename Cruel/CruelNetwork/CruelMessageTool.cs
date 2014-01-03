using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using JSLibrary.Conversion;
using JSLibrary.Network.Data;
using XmasEngineModel;
using Cruel.GameLogic;
using CruelNetwork.Messages.GameMessages;

namespace CruelNetwork
{
    public class CruelMessageTool : MessageTool
    {
        private CruelEngine engine;

        protected override void OnOpening(JSMessage message)
        {
            base.OnOpening(message);
            if (message is CommandMessage)
                ((CommandMessage)message).Engine = engine;
        }

        public void SetEngine(Cruel.GameLogic.CruelEngine Engine)
        {
            this.engine = Engine;
        }
    }
}
