using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Cards.CardBehaviors;
using UnityEngine;

namespace Assets.Global
{
	class Player : MonoBehaviour
	{
		public int PlayerNum;
		public int StartingHealth;
		public int ShotPowerDamage;

		public int CurrentHealth { get; private set; }

		void Start() 
		{
			CurrentHealth = StartingHealth;
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
