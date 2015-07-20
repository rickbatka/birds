using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.UI;
using System;

public class aimcannon : MonoBehaviour {
	public Rigidbody2D CannonballPrefab;
	public float ShotVelocityModifier = 5000;
	bool isAiming = false;
	CircleCollider2D cannonCollider;
	Rigidbody2D cannonBall;
	Text debugText;
	
	// Use this for initialization
	void Start () 
	{
		cannonCollider = this.GetComponent<CircleCollider2D>();
		debugText = GameObject.Find("debug-text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		var mousePos2d = Camera.main.ScreenToWorldPoint(Input.mousePosition).FlattenZ();
		if (!isAiming && Input.GetMouseButtonDown(0))
		{
			if (cannonCollider.bounds.Contains(mousePos2d))
			{
				isAiming = true;
				//debugText.text = "aim?y";
				cannonBall = (Rigidbody2D)Instantiate(CannonballPrefab, mousePos2d.FlattenZ(), cannonCollider.transform.rotation);
				cannonBall.isKinematic = true;
			}
		}

		if (isAiming)
		{
			var positionRelativeToCannon = mousePos2d - cannonCollider.transform.position;
			var allowedDistance = Vector3.ClampMagnitude(positionRelativeToCannon, 2 * cannonCollider.radius);
			var clampedPosition = cannonCollider.transform.position + allowedDistance;
			cannonBall.transform.position = clampedPosition;

			debugText.text = "ball " + cannonBall.transform.position.ToString();

			if (!Input.GetMouseButton(0))
			{
				isAiming = false;
				cannonBall.isKinematic = false;
				var shotForce = (cannonCollider.transform.position - clampedPosition) * ShotVelocityModifier;
				debugText.text = "shot " + shotForce.ToString();
				cannonBall.AddForce(shotForce);
				cannonBall = null;
			}
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
