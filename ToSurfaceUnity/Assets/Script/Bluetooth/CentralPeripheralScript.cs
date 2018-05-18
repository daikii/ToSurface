/*
 * CentralPeripheralScript.cs
 *
 * for listing bluetooth devices on setup
 * (placed under "CentralPeripheralPrefab" Prefab)
 */

using System;
using UnityEngine;
using UnityEngine.UI;

public class CentralPeripheralScript : MonoBehaviour 
{
	// global manager
	//private GlobalManager global;

	public Text textName;
	public Text textAddress;

	void Start() 
	{
	}

	void Update() 
	{
	}

	public void OnConnect()
	{
		BluetoothLEHardwareInterface.ConnectToPeripheral(textAddress.text, null, null, (address, serviceUUID, characteristicUUID) => {
			BluetoothLEHardwareInterface.Log ("Connect Succeeded");
		});

		GameObject.Find("CanvasSetup/PanelBluetooth").GetComponent<BluetoothScript>().addressConnected = textAddress.text;

		// store ble device number
		string str = textName.text;
		//global.bluetoothNum = int.Parse(str.Split('-')[1]);
	}
}
