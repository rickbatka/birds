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

	interface IRawInputPointingDevice
	{
		void Update();
		bool GetDown();
		bool GetMove();
		bool GetUp();
		bool HandledThisFrame();
		Vector2 GetScreenPosition();
		Vector2 GetDelta();
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

		public bool HandledThisFrame()
		{
			return GetDown() || GetUp() || GetMove();
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
	}

	public class TouchInput : IRawInputPointingDevice
	{
		Touch trackingTouch;
		bool isTracking = false;
		bool downThisFrame = false;
		bool moveThisFrame = false;
		bool upThisFrame = false;

		public void Update()
		{
			downThisFrame = moveThisFrame = upThisFrame = false;

			if (Input.touchCount > 0)
			{
				if (isTracking)
				{
					var currentTouch = Input.touches.First(t => t.fingerId == trackingTouch.fingerId);
					trackingTouch = currentTouch;

					if (currentTouch.phase == TouchPhase.Moved || currentTouch.phase == TouchPhase.Stationary)
					{
						moveThisFrame = true;
                    }

					if(currentTouch.phase == TouchPhase.Canceled || currentTouch.phase == TouchPhase.Ended)
					{
						upThisFrame = true;
						isTracking = false;
					}
				}
				else
				{
					if(Input.touches.Any(t => t.phase == TouchPhase.Began))
					{
						downThisFrame = true;
						trackingTouch = Input.touches.First(t => t.phase == TouchPhase.Began);
						isTracking = true;
                    }
				}
			}
			else
			{
				isTracking = false;
			}

		}

		public bool GetDown()
		{
			return downThisFrame;
		}

		public bool GetMove()
		{
			return moveThisFrame;
        }

		public bool GetUp()
		{
			return upThisFrame;
        }
		
		public bool HandledThisFrame()
		{
			return downThisFrame || moveThisFrame || upThisFrame;
		}

		public Vector2 GetScreenPosition()
		{
			return trackingTouch.position;
		}

		public Vector2 GetDelta()
		{
			if (isTracking)
			{
				return trackingTouch.deltaPosition;
			}
			return Vector2.zero;
		}
	}

	public class UnifiedInput : MonoBehaviour
	{
		public bool IsAiming = false;
		public event OnBackgroundTouchDown OnBackgroundDown;
		public event OnBackgroundTouchMove OnBackgroundMove;
		public event OnBackgroundTouchUp OnBackgroundUp;

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

				if (input.HandledThisFrame())
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
