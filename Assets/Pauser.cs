using UnityEngine;
using System.Collections;

public class Pauser : MonoBehaviour {
	GameObject CardCanvas;
	bool IsPaused = false;

	// Use this for initialization
	void Start () 
	{
		CardCanvas = GameObject.Find("CardCanvas");
		CardCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			IsPaused = !IsPaused;
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
			CardCanvas.SetActive(!CardCanvas.activeInHierarchy);
		}
	}
}
