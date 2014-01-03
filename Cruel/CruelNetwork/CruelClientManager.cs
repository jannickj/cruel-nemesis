using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using CruelGameData.GameLogic.Game.Maps;
using System.Threading;
using JSLibrary.Network;

namespace CruelNetwork
{
    public class CruelClientManager
    {
        private Dictionary<CruelClientConnector, GameDetail> connectedTo  = new Dictionary<CruelClientConnector, GameDetail>();
        private CruelEngineFactory GameFactory;
        private ServerFactory ServerFactory;

        public void CreateGame(CruelClientConnector connection)
        {
            var engine = GameFactory.CreateEngine(new StandardGameMapBuilder());
            var thread = ServerFactory.CreateThread(() => engine.Start());
            var details = new GameDetail() { Engine = engine, Thread = thread };
            thread.Start();
            lock (this)
            {
                connectedTo.Add(connection, details);
            }

        }


        
        class GameDetail
        {
            public CruelEngine Engine { get;  set; }
            public Thread Thread { get; set; }
        }
    }
}
