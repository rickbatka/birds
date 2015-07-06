using UnityEngine;
using System.Collections;
using Assets;
using UnityEngine.UI;

public class aimcannon : MonoBehaviour {
	bool isAiming = false;
	CircleCollider2D collider;
	Text debugText;
	// Use this for initialization
	void Start () 
	{
		collider = this.GetComponent<CircleCollider2D>();
		debugText = GameObject.Find("debug-text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		debugText.text = string.Empty;

		var mousePos2d = Camera.main.ScreenToWorldPoint(Input.mousePosition).FlattenZ();
		if (Input.GetMouseButtonDown(0))
		{
			if (collider.bounds.Contains(mousePos2d))
			{
				isAiming = true;
				debugText.text = "aim?y";
			}
		}

		if (isAiming && !Input.GetMouseButton(0))
		{
			isAiming = false;
			debugText.text = "aim?n";
		}

		if (isAiming && Input.GetMouseButton(0))
		{
			debugText.text += "pos x:" + mousePos2d.x + " y:" + mousePos2d.y;
		}
	}
}
