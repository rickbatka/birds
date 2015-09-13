using Assets.Cards;
using UnityEngine;
using System.Linq;
using Assets.CardViewModels;
using System.Collections.Generic;

namespace Assets.Global
{
	public class Director : MonoBehaviour
	{
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
			
			if(!ThisLevelInitialized) //todo onlevelwasloaded
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
			player1.Cards = dealCards();
			player2 = Player2GameObject.GetComponent<Player>();
			player2.Cards = dealCards();
			GameState.LocalPlayer = NewGameInfo.LocalPlayerNumber == 1 ? player1 : player2;
			
			var initialState = NewGameInfo.LocalPlayerNumber == NewGameInfo.NextUpPlayer ? AllGameStates.MyTurn_Cards : AllGameStates.TheirTurn_Battleground; 
			GameState.State = initialState;
			ThisLevelInitialized = true;
			
		}
		
		private List<ICardViewModel> dealCards(){
			return new List<ICardViewModel>{ new CardExtraPower() };
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

			if (Input.GetKeyDown(KeyCode.E))
			{
				//Player1GameObject.AddComponent<CB_ExtraPower>();
				player1.Cards.Add(new CardExtraPower());
			}
		}
	}
}
