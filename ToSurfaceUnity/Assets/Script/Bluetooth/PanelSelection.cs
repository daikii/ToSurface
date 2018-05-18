/*
 * PanelSelection.cs
 *
 * for switching setup display
 * (placed under "CanvasSetup" GameObject)
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelSelection : MonoBehaviour 
{
	public Transform panelActive;
	public Transform panelInActive;
	public Transform panelPlayer;
	
	static private PanelSelection _panelScript;
	static public void Show (Transform panel)
	{
		if (_panelScript == null)
		{
			GameObject gameObject = GameObject.Find("CanvasSetup");
			if (gameObject != null)
			{
				_panelScript = gameObject.GetComponent<PanelSelection>();
			}
		}
		
		if (_panelScript != null)
		{
			while (_panelScript.panelActive.childCount > 0)
			{
				_panelScript.panelActive.GetChild (0).transform.SetParent(_panelScript.panelInActive);
			}
			
			panel.SetParent(_panelScript.panelActive);
		}
	}
	
	void Start()
	{
		Show(panelPlayer);
	}
	
	void Update()
	{
	}
}
