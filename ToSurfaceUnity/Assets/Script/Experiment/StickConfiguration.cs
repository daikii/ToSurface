using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickConfiguration : MonoBehaviour 
{
	public Camera cam;
	public Slider mainSlider;
	public float length;

	void Awake () 
	{ 
	}
	
	void Update () 
	{
		// Update length
		this.transform.localScale = new Vector3(this.transform.localScale.x, mainSlider.value, this.transform.localScale.z);

		// Update position and rotation
		this.transform.rotation = cam.transform.rotation;
		this.transform.Rotate (90, 0, 0);
		this.transform.position = cam.transform.position + cam.transform.forward * 1.0f;
		this.transform.Translate (0, -0.17f, this.transform.localScale.y - 0.9f, Camera.main.transform);
		//this.transform.position = (cam.transform.position + cam.transform.forward * 1.2f) + new Vector3(0, -0.17f, this.transform.localScale.y + 0.3f);
		//this.transform.position = cam.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, cam.nearClipPlane)) + new Vector3(0, -0.17f, this.transform.localScale.y + 0.3f);
		//this.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + 90, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
	}
}
