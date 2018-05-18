// Placed under ArPosition prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArPositionManager : Photon.PunBehaviour 
{
	private SerialHandler serial;
	private PhotonView photonView;
	private Camera cam;
	
	public Vector3 cameraPos = new Vector3 (0, 0, 0);
	public Vector3 contrPos = new Vector3 (0, 0, 0);
	public Vector3 objPos = new Vector3 (0, 0, 0);
	public bool isCollide = false;

	public int test = 0;

	void Awake ()
	{
		photonView = GetComponent<PhotonView>();

		if (photonView.isMine) {
			cam = GameObject.Find ("CameraParent/MainCamera/").GetComponent<Camera> ();
		} else {
			serial = GameObject.Find ("GlobalManager").GetComponent<SerialHandler> ();
		}
	}

	void Update () 
	{
		if (photonView.isMine) 
		{
			cameraPos = cam.transform.position;
		}
	}

	[PunRPC]
	void ReceiveData(Vector3 contr, Vector3 obj)
	{
		contrPos = contr;
		objPos = obj;

		serial.Write ("1");

		Debug.Log ("Controller: " + contrPos);
		Debug.Log ("Object: " + objPos);
	}
}
