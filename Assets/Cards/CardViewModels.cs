using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Cards.CardBehaviors;
using UnityEngine;

namespace Assets.CardViewModels
{
	public interface ICardViewModel
	{
		string Name { get; }
		string Use();
	}

	public class CardExtraPower : ICardViewModel
	{
		public string Name { get { return "Extra Power"; } }
		public string Use()
		{
			return "Next shot power up!";
		}
	}
}
