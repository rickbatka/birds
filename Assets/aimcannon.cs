using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.UI;
using System;
using Assets.Aiming;
using Assets.Global;

public class aimcannon : MonoBehaviour {
	public Rigidbody2D CannonballPrefab;
	public float ShotVelocityModifier = 5000;
	CircleCollider2D cannonCollider;
	Rigidbody2D cannonBall;
	Text debugText;
	AimingLine _aimingLine;
	
	// Use this for initialization
	void Start () 
	{
		cannonCollider = this.GetComponent<CircleCollider2D>();
		debugText = GameObject.Find("debug-text").GetComponent<Text>();
		_aimingLine = GetComponent<AimingLine>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		var mousePos2d = Camera.main.ScreenToWorldPoint(Input.mousePosition).FlattenZ();
		if (!InputManager.IsAiming
			&& GameState.CurrentStateName == StateNames.myturn_battleground
			&& InputManager.IsMouseDownInAimingZone())
		{
			InputManager.IsAiming = true;
			//debugText.text = "aim?y";
			cannonBall = (Rigidbody2D)Instantiate(CannonballPrefab, mousePos2d.FlattenZ(), cannonCollider.transform.rotation);
			cannonBall.isKinematic = true;
		}

		if (InputManager.IsAiming)
		{
			var positionRelativeToCannon = mousePos2d - cannonCollider.transform.position;
			var allowedDistance = Vector3.ClampMagnitude(positionRelativeToCannon, 3 * cannonCollider.radius);
			var clampedPosition = cannonCollider.transform.position + allowedDistance;
			cannonBall.transform.position = clampedPosition;

			var difference = (cannonCollider.transform.position - clampedPosition); 
			var direction = difference.normalized;
			var speed = difference.magnitude;
			var shotVelocity = direction * speed * ShotVelocityModifier;

			_aimingLine.UpdateTrajectory(clampedPosition, shotVelocity);

			if (!Input.GetMouseButton(0))
			{
				InputManager.IsAiming = false;
				cannonBall.isKinematic = false;
				debugText.text = "shot " + shotVelocity.ToString();
				cannonBall.velocity = shotVelocity;
				cannonBall = null;
			}
		}
		else
		{
			_aimingLine.kill();
		}
	}

	private float Clamp(float value, float min, float max)  
	{  
		return (value < min) ? min : (value > max) ? max : value;  
	}

	private double RadianToDegree(double angle)
	{
		return angle * (180.0 / Math.PI);
	}
}
