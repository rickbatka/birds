using UnityEngine;
using System.Collections;
using Assets.Global;

namespace Assets.CameraScripts
{
	public class TouchPan : MonoBehaviour
	{
		public float PanSpeed = 0.02f;
		private UnifiedInput inputManager;
		private bool isPanning = false;
		private Vector2 lastPosition;
		private Rigidbody2D cameraRigidBody;
		private Vector2 lastDelta;

		void Start()
		{
			cameraRigidBody = GetComponent<Rigidbody2D>();
			inputManager = GameObject.Find("Director").GetComponent<UnifiedInput>();
			inputManager.OnBackgroundDown += OnBackgroundDown;
			inputManager.OnBackgroundMove += OnBackgroundMove;
			inputManager.OnBackgroundUp += OnBackgroundUp;
		}

		void OnBackgroundDown(Vector2 pointerPosition)
		{
			var pointerPos2d = Camera.main.ScreenToWorldPoint(pointerPosition).FlattenZ();
			isPanning = true;
			lastPosition = pointerPos2d;
		}

		void OnBackgroundMove(Vector2 delta)
		{
			if (isPanning)
			{
				//var pointerPos2d = Camera.main.ScreenToWorldPoint(pointerPosition).FlattenZ();
				//var delta = (Vector2)pointerPos2d - lastPosition;
				var newPosition = new Vector3(-delta.x * PanSpeed, -delta.y * PanSpeed, 0);
				transform.Translate(newPosition);
				lastDelta = delta;
				//lastPosition = pointerPos2d;
			}
		}

		void OnBackgroundUp(Vector2 pointerPosition)
		{
			if (isPanning)
			{
				isPanning = false;
				cameraRigidBody.AddForce(-lastDelta, ForceMode2D.Impulse);
			}
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}