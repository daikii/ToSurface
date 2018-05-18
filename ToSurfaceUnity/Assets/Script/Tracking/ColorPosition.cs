// placed under TrackPointer prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPosition : Photon.MonoBehaviour 
{
	public PhotonView pv;
	public RectTransform rect;
	bool isReady = false;

	void Awake () 
	{
	}

	void Update () 
	{
		if (!isReady && GameObject.Find ("CamTrack(Clone)")) 
		{
			pv = GameObject.Find ("CamTrack(Clone)").GetComponent<PhotonView> ();
			isReady = true;
		} 

		if (isReady) 
		{
			// continuously update latest tracked position via PUN
			pv.RPC ("TrackedData", PhotonTargets.Others, new object[] { rect.anchorMax });
			Debug.Log ("Tracking: " + rect.anchorMax);
		}
	}
}
