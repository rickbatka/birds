using UnityEngine;
using System.Collections;

public class thrower : MonoBehaviour {
	public float ForwardForce = 950;
	bool freeRange = true;
	Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
		rigidBody = this.gameObject.GetComponent<Rigidbody2D>();
		rigidBody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (freeRange)
		{
			var mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			this.transform.position = new Vector3(mouseInWorld.x, mouseInWorld.y, 1);
		}

		if (freeRange && Input.GetKeyDown(KeyCode.Space))
		{
			freeRange = false;
			rigidBody.isKinematic = false;
			rigidBody.AddForce(new Vector2(ForwardForce, 0));
		}

		if (!freeRange && Input.GetMouseButtonUp(0))
		{
			freeRange = true;
			rigidBody.isKinematic = true;
		}

	}
}
