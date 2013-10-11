using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic;

namespace Assets.UnityLogic
{
	public class GlobalGameSettings
	{
        private Player mainPlayer;
        private List<Player> localPlayers = new List<Player>();


        public Player MainPlayer
        {
            get { return mainPlayer; }
         }

        public Player[] LocalPlayers
        {
            get { return localPlayers.ToArray(); }
        }

        public void AddPlayer(Player player)
        {
            this.localPlayers.Add(player);
        }

        void Start()
        {
            Player p = new Player();
            p.Name = "player 1";
            this.mainPlayer = p;
            this.AddPlayer(p);
        }


	}
}
