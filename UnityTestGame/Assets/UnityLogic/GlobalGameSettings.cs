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

            var p1lib = new GameLibrary();
            Player p = new Player(p1lib,new Hand(), new ManaStorage(), new GameLevelRewards(Mana.Arcane));
            var cards1 = CreatePlayer1Deck();
            p1lib.Add(cards1);
            p1lib.Shuffle();
            p.Name = "player 1";
            this.mainPlayer = p;
            this.AddPlayer(p);

            var p2lib = new GameLibrary();
            p = new Player(p2lib, new Hand(), new ManaStorage(), new GameLevelRewards(Mana.Fury));
            var cards2 = CreatePlayer2Deck();
            p2lib.Add(cards2);
            p2lib.Shuffle();
            p.Name = "player 2";
            this.AddPlayer(p);
            
        }

        private IEnumerable<GameCard> CreatePlayer1Deck()
        {
            List<GameCard> deck = new List<GameCard>();
            for (int i = 0; i < 4; i++)
                deck.Add(new SerpentCard());
            for (int i = 0; i < 4; i++)
                deck.Add(new MonkCard());
            for (int i = 0; i < 4; i++)
                deck.Add(new LightningBoltCard());
            for (int i = 0; i < 3; i++)
                deck.Add(new InspirationCard());
            for (int i = 0; i < 2; i++)
                deck.Add(new FireballCard());
            for (int i = 0; i < 2; i++)
                deck.Add(new RaiseDeadCard());

            return deck;
        }

        private IEnumerable<GameCard> CreatePlayer2Deck()
        {
            List<GameCard> deck = new List<GameCard>();
            for (int i = 0; i < 4; i++)
                deck.Add(new GoblinPikerCard());
            for (int i = 0; i < 4; i++)
                deck.Add(new WarhoundCard());
            for (int i = 0; i < 3; i++)
                deck.Add(new ArcherCard());
            for (int i = 0; i < 3; i++)
                deck.Add(new BruteCard());
            for (int i = 0; i < 3; i++)
                deck.Add(new DragonCard());

            return deck;
        }
	}
}
