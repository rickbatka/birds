using Assets.Cards;
using UnityEngine;

namespace Assets.Global
{
	public class Director : MonoBehaviour
	{
		CardManager CardManager;

		void Start()
		{
			CardManager = new CardManager(GameObject.Find("CardCanvas"));
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
		}
	}
}
