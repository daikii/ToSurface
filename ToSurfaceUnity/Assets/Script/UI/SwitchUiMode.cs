using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUiMode : MonoBehaviour 
{
	public GameObject ui;

	void Start () 
	{
	}
	
	void Update () 
	{
		// UI buttons disable
		if (Input.touchCount > 2) 
		{
			if (ui.gameObject.activeSelf) 
			{
				ui.gameObject.SetActive (false);
			} 
			else 
			{
				ui.gameObject.SetActive (true);
			}
		}

		/*if (Input.GetMouseButtonDown (0)) 
		{
			if (ui.gameObject.activeSelf) 
			{
				Debug.Log (1111);
				ui.gameObject.SetActive (false);
			} 
			else 
			{
				Debug.Log (2222);
				ui.gameObject.SetActive (true);
			}
		}*/
	}
}
