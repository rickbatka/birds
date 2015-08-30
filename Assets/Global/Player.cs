using System.Collections.Generic;
using Assets.Cards.CardBehaviors;
using Assets.CardViewModels;
using UnityEngine;

namespace Assets.Global
{
	public class Player : MonoBehaviour
	{
		public int PlayerNum;
		public int StartingHealth;
		public int ShotPowerDamage;

		public int CurrentHealth { get; private set; }
		public List<ICardViewModel> Cards;

		void Awake() 
		{
			CurrentHealth = StartingHealth;
			Cards = new List<ICardViewModel>();
		}

		void Update() 
		{
			if (Input.GetKeyDown(KeyCode.X))
			{
				var cardsAttached = GetComponents<CardBehaviour>();
				foreach (var card in cardsAttached)
				{
					card.TurnCompleting();
				}
			}
		}
	}

}
