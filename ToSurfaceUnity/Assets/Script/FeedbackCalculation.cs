using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackCalculation : MonoBehaviour 
{
	public BluetoothScript ble;
	public NetworkManager network;
	CamTrackManager track;
	public Transform arCamera;

	// location of AR objects that affect haptic feedback
	public Transform arObjShower;
	public Transform arObjRain;
	Vector2 showerPos;
	Vector2 rainPos;

	// position of tracked object
	public Vector2 objPos = new Vector2 (0, 0);

	// max coordinate of plane that is manually defined
	Vector2 maxPos = new Vector2(1, 1);

	// vector difference between two points
	int diffX = 0;
	int diffY = 0;
	int negX = 0;
	int negY = 0;

	// threshold of distance bw ar and real objects
	float thresh = 0.12f;
	float threshNear = 0.06f;

	bool isReady = false;

	void Awake ()
	{
	}
	
	void Update () 
	{
		// AR objects. multiply x by -1 to match with camera coordinate.
		showerPos = new Vector2(arObjShower.position.x * -1, arObjShower.position.z);
		rainPos = new Vector2(arObjRain.position.x * -1, arObjRain.position.z);
		Debug.Log ("Rain Pos: " + rainPos.x + ", " + rainPos.y);

		if (network.isJoin && !isReady) 
		{
			track = GameObject.Find ("CamTrack(Clone)").GetComponent<CamTrackManager> ();
			isReady = true;

			// slow down BLE update
			InvokeRepeating ("TrackUpdate", 0, 0.5f);
		} 
	}

	void TrackUpdate()
	{
		// latest tracked object position
		objPos = track.objPos;

		// scale camera track positions to AR positions 
		objPos.x = Mathf.Clamp(map(objPos.x, 0f, 1f, 0f, maxPos.x), 0f, maxPos.x);
		objPos.y = Mathf.Clamp(map(objPos.y, 0f, 1f, 0f, maxPos.y), 0f, maxPos.y);
		Debug.Log ("Object(scaled): " + objPos.x + ", " + objPos.y);

		// compute distance between AR and tracked objects
		ComputeDistance();
	}

	void ComputeDistance()
	{
		float showerDist = Vector2.Distance (objPos, showerPos);
		float rainDist = Vector2.Distance (objPos, rainPos);

		Debug.Log ("shower dist: " + showerDist);
		Debug.Log ("rain dist: " + rainDist);

		if (showerDist < thresh)
		{
			if (showerDist < threshNear) 
			{
				ble.SendByteHaptic (1, 0);
			} 
			else 
			{
				ble.SendByteHaptic(1, Mathf.RoundToInt(showerDist * 100));
			}
		} 
		else if (rainDist < thresh) 
		{
			if (rainDist < threshNear) 
			{
				ble.SendByteHaptic (2, 0);
			} 
			else 
			{
				ble.SendByteHaptic(2, Mathf.RoundToInt(rainDist * 100));
			}
		} 
		else 
		{
			ble.SendByteHaptic (0, 0);
		}
	}

	void ComputeVectorDiff(Vector2 obj, Vector2 ar)
	{
		if (Vector2.Distance (obj, ar) > threshNear) 
		{
			// send via BLE in int
			diffX = Mathf.RoundToInt((ar.x - obj.x) * 100);
			diffY = Mathf.RoundToInt((ar.y - obj.y) * 100);

			if (diffX < 0) {
				diffX *= -1;
				negX = 1;
			} else {
				negX = 0;
			}
			if (diffY < 0) {
				diffY *= -1;
				negY = 1;
			} else {
				negY = 0;
			}
		} 
		else 
		{
			// if close enough to ar object, activate all motors
			diffX = 0;
			diffY = 0;
			negX = 0;
			negY = 0;
		}
	}

	float map(float s, float a1, float a2, float b1, float b2)
	{
		return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
	}

	// triggered by button press "Set Max"
	public void SetMaxPosition()
	{
		maxPos.x = arCamera.position.x * -1;
		maxPos.y = arCamera.position.z;
		Debug.Log ("Max Set: " + maxPos.x + " , " + maxPos.y);
	}
}
