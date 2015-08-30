using Assets.Cards;
using UnityEngine;
using System.Linq;
using Assets.Cards.CardBehaviors;

namespace Assets.Global
{
	public class Director : MonoBehaviour
	{
		public GameObject Player1GameObject;
		public GameObject Player2GameObject;

		Player Player1;
		Player Player2;
		CardManager CardManager;

		void Awake()
		{
			CardManager = new CardManager(GameObject.Find("CardCanvas"));
			var players = GameObject.FindGameObjectsWithTag("Player");
			Player1GameObject = players.First(p => p.name.ToLower() == "player1");
			Player2GameObject = players.First(p => p.name.ToLower() == "player2");
			Player1 = Player1GameObject.GetComponent<Player>();
			Player2 = Player2GameObject.GetComponent<Player>();
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.P))
			{
				if (GameState.CantransitionTo(AllGameStates.MyTurn_Battleground))
				{
					GameState.TransitionTo(AllGameStates.MyTurn_Battleground);
					CardManager.Deactivate();
				}
				else if (GameState.CantransitionTo(AllGameStates.MyTurn_Cards))
				{
					GameState.TransitionTo(AllGameStates.MyTurn_Cards);
					CardManager.Activate();
				}
			}

			if (Input.GetKeyDown(KeyCode.E))
			{
				Player1GameObject.AddComponent<CB_ExtraPower>();
			}
		}
	}
}
