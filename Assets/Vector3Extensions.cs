using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
	public static class Vector3Extensions
	{
		public static Vector3 FlattenZ(this Vector3 self)
		{
			return new Vector3(self.x, self.y, 0);
		}
	}
}
