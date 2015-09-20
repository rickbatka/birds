using UnityEngine;
using System.Collections;
using Assets.Global;

namespace Assets.CameraScripts
{
	public class TouchPan : MonoBehaviour
	{
		public float PanSpeed = 0.02f;
		private bool IsPanning = false;
		private Vector2 LastPosition;
		private Rigidbody2D cameraRigidBody;

		void Awake()
		{
			cameraRigidBody = GetComponent<Rigidbody2D>();
        }

		// Update is called once per frame
		void Update()
		{
			if (IsPanning && Input.GetMouseButton(0))
			{
				Vector2 delta = LastPosition - (Vector2)Input.mousePosition;
				// Move object across XY plane
				transform.Translate(delta.x * PanSpeed, delta.y * PanSpeed, 0);
				LastPosition = Input.mousePosition;
			}

			if(IsPanning && Input.GetMouseButtonUp(0))
			{
				IsPanning = false;
				Vector2 delta = LastPosition - (Vector2)Input.mousePosition;
				cameraRigidBody.AddForce(delta, ForceMode2D.Impulse);
			}

			if (!InputManager.IsMouseDownInAimingZone() 
				&& !InputManager.IsCardOverlayBlockingGameInput()
				&& Input.GetMouseButtonDown(0))
			{
				IsPanning = true;
				//LastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition).FlattenZ();
				LastPosition = Input.mousePosition;
			}
		}
	}
}