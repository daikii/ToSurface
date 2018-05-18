using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class PlaneSelector : MonoBehaviour 
{
	Transform selectPlane;
	public Vector3 position;
	public float maxRayDistance = 30.0f;
	public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer
	public bool isSelect = false;

	void Start () 
	{
	}

	void Update () 
	{
		#if UNITY_EDITOR // for debugging on editor
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			// check for a plane at position tapped
			if (Physics.Raycast (ray, out hit, maxRayDistance, collisionLayer)) 
			{
				selectPlane = hit.transform;
				//position = selectPlane.position;
				position = hit.point;
				isSelect = true;

				Debug.Log (string.Format ("x':{0:0.######} y':{1:0.######} z':{2:0.######}", position.x, position.y, position.z));
			}
		} 
		#else // safer to use touchCount for touchscreen
		if (Input.touchCount > 0) 
		{
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) 
			{
				var screenPosition = Camera.main.ScreenToViewportPoint (touch.position);

				Ray ray = Camera.main.ScreenPointToRay (screenPosition);
				RaycastHit hit;

				// check for a plane at position tapped
				if (Physics.Raycast (ray, out hit, maxRayDistance, collisionLayer)) 
				{
					selectPlane = hit.transform;
					//position = selectPlane.position;
					//position = hit.point;
					ArTapPosition(screenPosition.x, screenPosition.y);

					isSelect = true;

					Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", position.x, position.y, position.z));
				}
			}
		}
		#endif
	}

	void ArTapPosition(float x, float y)
	{
		ARPoint point = new ARPoint {
			x = x,
			y = y
		};

		List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, 
										   ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);

		if (hitResults.Count > 0) 
		{
			position = UnityARMatrixOps.GetPosition (hitResults[0].worldTransform);
		}
	}
}