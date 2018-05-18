// Placed under AxesPrefab prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectedPosition : MonoBehaviour 
{
	public ArPositionManager arPos;
	bool isReady = false;

	void Awake () 
	{
	}
	
	void Update () 
	{
		Debug.Log (this.transform.position);

		if (!isReady && GameObject.Find ("ArPosition(Clone)")) 
		{
			arPos = GameObject.Find ("ArPosition(Clone)").GetComponent<ArPositionManager> ();
			isReady = true;
		} 
		else if (isReady) 
		{
			arPos.contrPos = this.transform.position;
		}
	}
}
