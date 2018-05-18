// Placed under ??

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideManager : MonoBehaviour 
{
	public bool isColliding = false;
	
	void Update () 
	{
		if (isColliding) Handheld.Vibrate();
	}

	void OnTriggerEnter(Collider other)
	{
		isColliding = true;
		Debug.Log ("HIT!!");
	}

	void OnTriggerStay(Collider other)
	{
	}

	void OnTriggerExit(Collider other)
	{
		isColliding = false;
	}
}
