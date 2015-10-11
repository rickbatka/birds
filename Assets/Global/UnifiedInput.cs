using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Global
{
	public delegate void OnBackgroundTouchDown(Vector2 screenPos);
	public delegate void OnBackgroundTouchMove(Vector2 delta);
	public delegate void OnBackgroundTouchUp(Vector2 screenPos);
	public delegate void OnBackgroundSecondTouchDown(Vector2 screenPos);
	public delegate void OnBackgroundSecondTouchMove(Vector2 delta);
	public delegate void OnBackgroundSecondTouchUp(Vector2 screenPos);

	interface IRawInputPointingDevice
	{
		void Update();
		bool GetDown();
		bool GetMove();
		bool GetUp();
		bool GetSecondDown();
		bool GetSecondMove();
		bool GetSecondUp();
		bool HandledMainInput();
		Vector2 GetScreenPosition();
		Vector2 GetSecondFingerScreenPosition();
		Vector2 GetDelta();
		Vector2 GetSecondFingerDelta();
	}

	public class MouseInput : IRawInputPointingDevice
	{
		bool tracking = false;
		Vector2 lastPosition;
		Vector2 frameDelta;
		public void Update()
		{
			if (GetMove())
			{
				frameDelta = (Vector2)Input.mousePosition - lastPosition;
                lastPosition = Input.mousePosition;
			}
			if (GetUp())
			{
				tracking = false;
			}
			if (GetDown())
			{
				tracking = true;
				lastPosition = Input.mousePosition;
			}
		}

		public bool GetDown()
		{
			return Input.GetMouseButtonDown(0);
		}

		public bool GetMove()
		{
			return Input.GetMouseButton(0);
		}

		public bool GetUp()
		{
			return Input.GetMouseButtonUp(0);
		}

		public Vector2 GetScreenPosition()
		{
			return Input.mousePosition;
		}

		public Vector2 GetDelta()
		{
			if (tracking && frameDelta != null)
			{
				return frameDelta;
			}

			return Vector2.zero;
		}

		public bool HandledMainInput()
		{
			return GetDown() || GetMove() || GetUp();
		}

		public bool GetSecondDown() { return false; }
		public bool GetSecondMove() { return false; }
		public bool GetSecondUp() { return false; }
		public Vector2 GetSecondFingerScreenPosition() { return Vector2.zero; }
		public Vector2 GetSecondFingerDelta() { return Vector2.zero; }
	}

	public class TouchInput : IRawInputPointingDevice
	{
		Touch? FingerOne;
		Touch? FingerTwo;
		bool FirstDownThisFrame = false;
		bool FirstMoveThisFrame = false;
		bool FirstUpThisFrame = false;

		bool SecondDownThisFrame = false;
		bool SecondMoveThisFrame = false;
		bool SecondUpThisFrame = false;

		public void Update()
		{
			FirstDownThisFrame = FirstMoveThisFrame = FirstUpThisFrame = false;
			SecondDownThisFrame = SecondMoveThisFrame = SecondUpThisFrame = false;

			foreach(var touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					if (FingerOne == null)
					{
						FirstDownThisFrame = true;
						FingerOne = touch;
					}
					else
					{
						SecondDownThisFrame = true;
						FingerTwo = touch;
					}
				}
				else
				{
					bool isFirst = (FingerOne != null && touch.fingerId == FingerOne.Value.fingerId);
					if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
					{
						if (isFirst)
						{
							FingerOne = touch;
							FirstMoveThisFrame = true;
						}
						else
						{
							FingerTwo = touch;
							SecondMoveThisFrame = true;
						}
					}

					if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
					{
						if (isFirst)
						{
							FirstUpThisFrame = true;
							FingerOne = null;
						}
						else
						{
							SecondUpThisFrame = true;
							FingerTwo = null;
						}
					}
				}
			}
		}

		public bool GetDown()
		{
			return FirstDownThisFrame;
		}

		public bool GetMove()
		{
			return FirstMoveThisFrame;
        }

		public bool GetUp()
		{
			return FirstUpThisFrame;
        }

		public bool HandledMainInput()
		{
			return GetDown() || GetMove() || GetUp();
		}

		public Vector2 GetScreenPosition()
		{
			return FingerOne != null ? FingerOne.Value.position : Vector2.zero;
		}

		public Vector2 GetDelta()
		{
			return FingerOne != null ? FingerOne.Value.deltaPosition : Vector2.zero;
		}

		public bool GetSecondDown()
		{
			return SecondDownThisFrame;
		}

		public bool GetSecondMove()
		{
			return SecondMoveThisFrame;
		}

		public bool GetSecondUp()
		{
			return SecondUpThisFrame;
		}

		public Vector2 GetSecondFingerScreenPosition()
		{
			return FingerTwo != null ? FingerTwo.Value.position : Vector2.zero;
		}

		public Vector2 GetSecondFingerDelta()
		{
			return FingerTwo != null ? FingerTwo.Value.deltaPosition : Vector2.zero;
		}
	}

	public class UnifiedInput : MonoBehaviour
	{
		public bool IsAiming = false;
		public event OnBackgroundTouchDown OnBackgroundDown;
		public event OnBackgroundTouchMove OnBackgroundMove;
		public event OnBackgroundTouchUp OnBackgroundUp;
		public event OnBackgroundSecondTouchDown OnBackgroundSecondDown;
		public event OnBackgroundSecondTouchMove OnBackgroundSecondMove;
		public event OnBackgroundSecondTouchUp OnBackgroundSecondUp;

		private CircleCollider2D AimingZone;
		private IRawInputPointingDevice[] inputs = new IRawInputPointingDevice[] {
			new TouchInput(),
			new MouseInput()
		};

		void Start()
		{
			AimingZone = GameObject.Find("cannon").GetComponent<CircleCollider2D>();
		}

		void Update()
		{
			foreach(var input in inputs)
			{
				input.Update();
				
				if (input.GetDown())
				{
					if (!IsCardOverlayBlockingGameInput())
					{
						if (!IsPointerInAimingZone(input.GetScreenPosition()) && OnBackgroundDown != null)
						{
							OnBackgroundDown(input.GetScreenPosition());
						}
					}
				}else if (input.GetMove())
				{
					if (!IsCardOverlayBlockingGameInput())
					{
						if (OnBackgroundMove != null)
						{
							OnBackgroundMove(input.GetDelta());
						}
					}
				}else if (input.GetUp())
				{
					if (!IsCardOverlayBlockingGameInput())
					{
						if (OnBackgroundUp != null)
						{
							OnBackgroundUp(input.GetScreenPosition());
						}
					}
				}

				if (input.GetSecondDown())
				{
					if (!IsCardOverlayBlockingGameInput() && OnBackgroundSecondDown != null)
					{
						OnBackgroundSecondDown(input.GetSecondFingerScreenPosition());
					}
				}
				else if (input.GetSecondMove())
				{
					if (!IsCardOverlayBlockingGameInput() && OnBackgroundSecondMove != null)
					{
						OnBackgroundSecondMove(input.GetSecondFingerDelta());
					}
				}
				else if (input.GetSecondUp())
				{
					if (!IsCardOverlayBlockingGameInput() && OnBackgroundSecondUp != null)
					{
						OnBackgroundSecondUp(input.GetSecondFingerScreenPosition());
					}
				}

				if (input.HandledMainInput())
				{
					break;
				}
			}
		}

		public bool IsCardOverlayBlockingGameInput()
		{
			return GameState.State == AllGameStates.MyTurn_Cards || GameState.State == AllGameStates.TheirTurn_Cards;
		}

		bool IsPointerInAimingZone(Vector2 pointerScreenPosition)
		{
			var pointerPos2d = Camera.main.ScreenToWorldPoint(pointerScreenPosition).FlattenZ();
			var contains = AimingZone.bounds.Contains(pointerPos2d);
			Debug.Log("pointer check raw " +  pointerScreenPosition+" world " + pointerPos2d +" contains " + contains);

			return contains;
        }
	}
}
