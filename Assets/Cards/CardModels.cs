using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Cards.CardBehaviors;
using Assets.Global;
using UnityEngine;

namespace Assets.CardModels
{
	public interface ICardModel
	{
		string Name { get; }
		string Use();
	}

	public class CardExtraPower : ICardModel
	{
		private Player Player;
		public CardExtraPower(Player forPlayer)
		{
			Player = forPlayer;
		}
		public string Name { get { return "Extra Power"; } }
		public string Use()
		{
			Player.gameObject.AddComponent<CB_ExtraPower>();
            return "Next shot power up!";
		}
	}
}
