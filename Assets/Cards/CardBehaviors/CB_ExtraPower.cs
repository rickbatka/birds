using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Global;
using UnityEngine;

namespace Assets.Cards.CardBehaviors
{
	abstract class CardBehaviour : MonoBehaviour
	{
		protected abstract int TurnsToLive { get; }
		protected int TurnsLeft;

		protected abstract void Start();
		protected abstract void Update();
		
		protected abstract void OnTurnCompletingBegin();

		private void Awake() 
		{
			TurnsLeft = TurnsToLive;
			Debug.Log("card awake");
		}

		public void TurnCompleting()
		{
			OnTurnCompletingBegin();
			TurnsLeft--;
			if (TurnsLeft <= 0)
			{
				Destroy(this);
			}
			Debug.Log("card turn completing");
		}
	}

	class CB_ExtraPower : CardBehaviour
	{
		private Player player;
		protected override int TurnsToLive { get { return 2; } }
		
		protected override void Start() 
		{
			player = gameObject.GetComponent<Player>();
			player.ShotPowerDamage += 25;
		}

		protected override void Update() { }

		protected override void OnTurnCompletingBegin()
		{
			Debug.Log("shot card turn ended");
		}
	}
}
