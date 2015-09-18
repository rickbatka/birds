using Assets.Cards;
using UnityEngine;
using System.Linq;
using Assets.CardModels;
using System.Collections.Generic;
using System;

namespace Assets.Global
{
	public class Director : MonoBehaviour
	{
		private static Director _drct;
		public static Director Instance
		{
			get
			{
				return _drct ?? (_drct = GameObject.Find("Director").GetComponent<Director>() );
			}
		}

		public GameObject Player1GameObject;
		public GameObject Player2GameObject;
		public NewGameInitInfo NewGameInfo { get; set; }
		bool ThisLevelInitialized = false;

		Player player1;
		Player player2;
		
		void Awake()
		{
			// todo this is set from main menu before the leve loads
			NewGameInfo = new NewGameInitInfo(localPlayerNumber: 1, nextUpPlayer: 1);
			
			
		}

		void Start()
		{
			if (!ThisLevelInitialized) //todo onlevelwasloaded
			{
				InitLevel();
			}
		}

		void InitLevel() 
		{
			var players = GameObject.FindGameObjectsWithTag("Player");
			Player1GameObject = players.First(p => p.name.ToLower() == "player1");
			Player2GameObject = players.First(p => p.name.ToLower() == "player2");
			player1 = Player1GameObject.GetComponent<Player>();
			player2 = Player2GameObject.GetComponent<Player>();
			GameState.LocalPlayer = NewGameInfo.LocalPlayerNumber == 1 ? player1 : player2;
			
			if(NewGameInfo.LocalPlayerNumber == NewGameInfo.NextUpPlayer)
			{
				StartLocalPlayerTurn();
			}
			
			ThisLevelInitialized = true;
		}

		void StartLocalPlayerTurn()
		{
			var localPlayer = GameState.LocalPlayer;
			localPlayer.Cards.AddRange(dealCards(localPlayer));
			GameState.State = AllGameStates.MyTurn_Cards;
		}

		private List<ICardModel> dealCards(Player forPlayer)
		{
			var numCardsNeeded = Math.Max(Constants.NUM_CARDS_IN_HAND - forPlayer.Cards.Count, 0);
			var results = new List<ICardModel>();
            for (int i = 0; i < numCardsNeeded; i++)
			{
				results.Add(new CardExtraPower(forPlayer));
			}
			return results;
		}
		
		void UnloadLevel()
		{
			ThisLevelInitialized = false;
		}
		
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				if (GameState.CantransitionTo(AllGameStates.MyTurn_Battleground))
				{
					GameState.State = AllGameStates.MyTurn_Battleground;
				}
				else if (GameState.CantransitionTo(AllGameStates.MyTurn_Cards))
				{
					GameState.State = AllGameStates.MyTurn_Cards;
				}
			}
		}

		public void CardWasUsed(Player player, CardController card)
		{
			player.Cards.Remove(card.Card);
			Destroy(card);
			//todo state changes out of card mode immediately after using a card - desired?
			GameState.State = AllGameStates.MyTurn_Battleground;
		}
	}
}
