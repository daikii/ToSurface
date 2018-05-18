// Placed under Sphere prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArCollideManager : Photon.MonoBehaviour 
{
	public BluetoothScript ble;
	public PhotonView pv;
	public ArPositionManager arPos;
	bool isReady = false;

	void Update () 
	{
		if (!isReady && GameObject.Find ("ArPosition(Clone)")) 
		{
			arPos = GameObject.Find ("ArPosition(Clone)").GetComponent<ArPositionManager> ();
			pv = GameObject.Find ("ArPosition(Clone)").GetComponent<PhotonView> ();
			isReady = true;
		} 
	}

	void OnTriggerEnter(Collider other)
	{
		arPos.objPos = this.transform.position;
		arPos.isCollide = true;

		//Debug.Log ("hit");
		//ble.SendByte ((byte)3);

		pv.RPC ("ReceiveData", PhotonTargets.Others, new object[] {arPos.contrPos, arPos.objPos});
	}

	void OnTriggerStay(Collider other)
	{
	}

	void OnTriggerExit(Collider other)
	{
		arPos.isCollide = false;
	}
}

