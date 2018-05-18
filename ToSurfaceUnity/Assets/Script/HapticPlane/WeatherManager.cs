using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour 
{
	public PlaneSelector plane;

	void Start () 
	{
	}
	
	void Update () 
	{
		if (plane.isSelect) 
		{
			this.transform.position = new Vector3(plane.position.x, plane.position.y + 0.2f, plane.position.z);
		}
	}
}
