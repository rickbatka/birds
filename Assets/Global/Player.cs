using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Global
{
	class Player
	{
		public int PlayerNum { get; private set; }
		public int Health { get; }

		public int GetShotPower()
		{
			return 10;
		}
	}
}
