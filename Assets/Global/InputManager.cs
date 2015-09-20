using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Global
{
	public static class InputManager
	{
		public static bool IsAiming = false;

		private static CircleCollider2D _aimzn;
		private static CircleCollider2D AimingZone { get
			{
				return _aimzn ?? (_aimzn  = GameObject.Find("cannon").GetComponent<CircleCollider2D>());
			}
		}

		public static bool IsCardOverlayBlockingGameInput()
		{
			return GameState.State == AllGameStates.MyTurn_Cards || GameState.State == AllGameStates.TheirTurn_Cards;
		}

		public static bool IsMouseDownInAimingZone()
		{
			var mousePos2d = Camera.main.ScreenToWorldPoint(Input.mousePosition).FlattenZ();
			if((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && AimingZone.bounds.Contains(mousePos2d))
			{
				return true;
			}

			if(Input.touchCount > 0)
			{
				var touchPos2d = Camera.main.ScreenToWorldPoint(Input.touches[0].position).FlattenZ();
				if (AimingZone.bounds.Contains(touchPos2d))
				{
					return true;
				}
			}

			return false;
        }
	}
}
