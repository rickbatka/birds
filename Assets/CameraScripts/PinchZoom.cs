using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Global;
using UnityEngine;

namespace Assets.CameraScripts
{
	class PinchZoom : MonoBehaviour
	{
		public float OrthoZoomSpeed = 0.02f;        // The rate of change of the orthographic size in orthographic mode.
        private UnifiedInput inputManager;
		private bool IsZooming = false;

		private Vector2 FirstFingerPrevPos;
		private Vector2 FirstFingerCurPos;
		private Vector2 SecondFingerPrevPos;
		private Vector2 SecondFingerCurPos;

		void Start()
		{
			inputManager = GameObject.Find("Director").GetComponent<UnifiedInput>();
			inputManager.OnBackgroundDown += OnMainFingerAdded;
			inputManager.OnBackgroundMove += OnMainFingerMoved;
			inputManager.OnBackgroundUp += OnMainFingerRemoved;
			inputManager.OnBackgroundSecondDown += OnSecondFingerAdded;
			inputManager.OnBackgroundSecondMove += OnSecondFingerMoved;
			inputManager.OnBackgroundSecondUp += OnSecondFingerRemoved;
		}

		private void OnMainFingerAdded(Vector2 fingerPos)
		{
			FirstFingerPrevPos = FirstFingerCurPos = fingerPos;
		}

		private void OnMainFingerMoved(Vector2 delta)
		{
			FirstFingerPrevPos = FirstFingerCurPos;
			FirstFingerCurPos += delta;
		}

		private void OnMainFingerRemoved(Vector2 fingerPos)
		{
			IsZooming = false;
		}

		private void OnSecondFingerAdded(Vector2 fingerPos)
		{
			SecondFingerPrevPos = SecondFingerCurPos = fingerPos;
			IsZooming = true;
		}

		private void OnSecondFingerMoved(Vector2 delta)
		{
			SecondFingerPrevPos = SecondFingerCurPos;
			SecondFingerCurPos += delta;
		}

		private void OnSecondFingerRemoved(Vector2 fingerPos)
		{
			IsZooming = false;
		}

		void Update()
		{
			if (IsZooming)
			{
				float prevTouchDeltaMag = (FirstFingerPrevPos - SecondFingerPrevPos).magnitude;
				float touchDeltaMag = (FirstFingerCurPos - SecondFingerCurPos).magnitude;

				float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

				// ... change the orthographic size based on the change in distance between the touches.
				Camera.main.orthographicSize += deltaMagnitudeDiff * OrthoZoomSpeed;
				Debug.Log("change size delta " + (deltaMagnitudeDiff * OrthoZoomSpeed) + " new size " + Camera.main.orthographicSize);

				// Make sure the orthographic size never drops below zero.
				Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2.5f, 5.5f);
			}
			// If there are two touches on the device...
			//if (!inputManager.IsAiming
			//	&& !inputManager.IsPointerInAimingZone()
			//	&&!inputManager.IsCardOverlayBlockingGameInput()
			//	&& Input.touchCount == 2)
			//{
			//	var camera = Camera.main;
			//	// Store both touches.
			//	Touch touchZero = Input.GetTouch(0);
			//	Touch touchOne = Input.GetTouch(1);

			//	// Find the position in the previous frame of each touch.
			//	Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			//	Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			//	// Find the magnitude of the vector (the distance) between the touches in each frame.
			//	float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			//	float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			//	// Find the difference in the distances between each frame.
			//	float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			//	// ... change the orthographic size based on the change in distance between the touches.
			//	camera.orthographicSize += deltaMagnitudeDiff * OrthoZoomSpeed;

			//	// Make sure the orthographic size never drops below zero.
			//	camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
			//}
		}

	}
}
