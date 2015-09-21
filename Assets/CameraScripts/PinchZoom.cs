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

		void Start()
		{
			inputManager = GameObject.Find("Director").GetComponent<UnifiedInput>();
		}

		void Update()
		{
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
