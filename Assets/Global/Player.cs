using System.Collections.Generic;
using Assets.Cards.CardBehaviors;
using Assets.CardModels;
using UnityEngine;

namespace Assets.Global
{
	public class Player : MonoBehaviour
	{
		public int PlayerNum;
		public int StartingHealth;
		public int ShotPowerDamage;

		public int CurrentHealth { get; private set; }
		public List<ICardModel> Cards;

		void Awake() 
		{
			CurrentHealth = StartingHealth;
			Cards = new List<ICardModel>();
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
