using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.GameLogic;
using UnityEngine;

namespace Assets.UnityLogic
{
	public class GlobalGameSettings : MonoBehaviour
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

        public static GlobalGameSettings GetSettings()
        {
            GameObject settingsObj = GameObject.Find("GlobalGameSettings");
            return settingsObj.GetComponent<GlobalGameSettings>();
        }

        void Start()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);
            Player p = new Player();
            p.Name = "player 1";
            this.mainPlayer = p;
            this.AddPlayer(p);
            
        }


	}
}
