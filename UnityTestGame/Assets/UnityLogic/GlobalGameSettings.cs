using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic;
using UnityEngine;
using Cruel.GameLogic.SpellSystem;
using Assets.UnityLogic.Game.Cards;
using CruelTest.SpellSystem;
using Assets.UnityLogic.Game;

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


        void Start()
        {
            UnityEngine.Object.DontDestroyOnLoad(this);

            var cardspawner = Enumerable.Repeat<Func<GameCard>>(() => new BloodwyrmSpawnCard(), 30).Select(genCard => genCard());
            var dragonspawner = Enumerable.Repeat<Func<GameCard>>(() => new DragonCard(), 30).Select(genCard => genCard());

            var p1lib = new GameLibrary();
            Player p = new Player(p1lib,new Hand(), new ManaStorage(), new GameLevelRewards(Mana.Arcane));
            var cards1 = cardspawner.ToArray();
            p1lib.Add(cards1);
            p.Name = "player 1";
            this.mainPlayer = p;
            this.AddPlayer(p);

            var p2lib = new GameLibrary();
            p = new Player(p2lib, new Hand(), new ManaStorage(), new GameLevelRewards(Mana.Fury));
            var cards2 = dragonspawner.ToArray();
            p2lib.Add(cards2);
            p.Name = "player 2";
            this.AddPlayer(p);
            
        }


	}
}
