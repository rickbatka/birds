using Assets.Cards;
using UnityEngine;
using System.Linq;
using Assets.Cards.CardBehaviors;
using Assets.CardViewModels;

namespace Assets.Global
{
	public class Director : MonoBehaviour
	{
		public GameObject Player1GameObject;
		public GameObject Player2GameObject;
		public GameObject CardsCanvas;
		CardDrawer CardsDrawer;

		Player _player1;
		Player _player2;
		Player _localPlayer;

		void Awake()
		{
			var players = GameObject.FindGameObjectsWithTag("Player");
			Player1GameObject = players.First(p => p.name.ToLower() == "player1");
			Player2GameObject = players.First(p => p.name.ToLower() == "player2");
			_player1 = Player1GameObject.GetComponent<Player>();
			_player2 = Player2GameObject.GetComponent<Player>();
			CardsDrawer = CardsCanvas.GetComponent<CardDrawer>();
			// todo net
			_localPlayer = _player1;

			//todo gamestart
			GameState.LocalPlayer = _localPlayer;
			_player1.Cards.Add(new CardExtraPower());
		}

		void Start() {
		
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				if (GameState.CantransitionTo(AllGameStates.MyTurn_Battleground))
				{
					GameState.TransitionTo(AllGameStates.MyTurn_Battleground);
					CardsDrawer.Deactivate();
				}
				else if (GameState.CantransitionTo(AllGameStates.MyTurn_Cards))
				{
					GameState.TransitionTo(AllGameStates.MyTurn_Cards);
					CardsDrawer.Activate();
				}
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				Player1GameObject.AddComponent<CB_ExtraPower>();
			}
		}
	}
}
