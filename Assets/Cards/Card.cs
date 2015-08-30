using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Cards
{
	interface ICard
	{
		string Name { get; }
		string Use();
	}

	class CardExtraPower : ICard
	{
		public string Name { get { return "Extra Power"; } }
		public string Use()
		{

			return "Next shot power up!";
		}
	}
}
