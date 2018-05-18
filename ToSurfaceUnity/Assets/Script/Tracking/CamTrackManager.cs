// Placed under CamTrack prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrackManager : Photon.PunBehaviour 
{
	public PhotonView pv;
	public Vector2 objPos = new Vector2 (0, 0);

	void Awake ()
	{
		pv = GetComponent<PhotonView>();
	}

	void Update () 
	{
	}

	[PunRPC]
	void TrackedData(Vector2 obj)
	{
		objPos = obj;

		if (!pv.isMine) 
		{
			//Debug.Log ("Object: " + objPos);
		}
	}
}