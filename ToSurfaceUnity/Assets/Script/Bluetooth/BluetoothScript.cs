/*
 * BluetoothScript.cs
 *
 * bluetooth-related manipulations for controller->tablet
 * (placed under "CanvasSetup/PanelInactive/PanelBluetooth" GameObject)
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BluetoothScript : MonoBehaviour
{
	// setup menu UI
	public Transform panelScrollContents;
	public Text textScanButton;

	// found BLE
	public GameObject centralPeripheralPrefab;

	private Dictionary<string, CentralPeripheralScript> _peripheralList;
	private bool _scanning;
	public string addressConnected;

	// RBL BLE Shield UUID
	private string serviceUUID;
	private string characteristicUUID;

	// test send data to Bluno
	bool isOn = false;

	void Start()
	{
		_scanning = false;
		addressConnected = "";

		// use fixed values defined by Bluno
		serviceUUID = "0000dfb0-0000-1000-8000-00805f9b34fb";
		characteristicUUID = "0000dfb1-0000-1000-8000-00805f9b34fb";

		Initialize ();
	}

	public void Initialize()
	{
		BluetoothLEHardwareInterface.Initialize (true, false, () => {
		}, (error) => {
		});
	}

	void Update()
	{
	}
	
	protected string BytesToString(byte[] bytes)
	{
		string result = "";
		
		foreach (var b in bytes) 
		{
			result += b.ToString ("X2");
		}
		
		return result;
	}
	
	public void OnScan()
	{
		if (_scanning)
		{
			BluetoothLEHardwareInterface.StopScan ();
			textScanButton.text = "Start Scan";
			_scanning = false;
		}
		else
		{
			RemovePeripherals ();

			// the first callback will only get called the first time this device is seen
			// this is because it gets added to a list in the BluetoothDeviceScript
			// after that only the second callback will get called and only if there is
			// advertising data available
			BluetoothLEHardwareInterface.ScanForPeripheralsWithServices (null, (address, name) => {
				AddPeripheral (name, address);
			}, (address, name, rssi, advertisingInfo) => {
				if (advertisingInfo != null)
				{
					BluetoothLEHardwareInterface.Log (string.Format ("Device: {0} RSSI: {1} Data Length: {2} Bytes: {3}", 
					                                                 name, rssi, advertisingInfo.Length, BytesToString (advertisingInfo)));
				}
			});
			
			textScanButton.text = "Stop Scan";
			_scanning = true;
		}
	}

	void RemovePeripherals ()
	{
		for (int i = 0; i < panelScrollContents.childCount; ++i)
		{
			GameObject gameObject = panelScrollContents.GetChild (i).gameObject;
			Destroy (gameObject);
		}
		
		if (_peripheralList != null) {
			_peripheralList.Clear ();
		}
	}
	
	void AddPeripheral (string name, string address)
	{
		if (_peripheralList == null) 
		{
			_peripheralList = new Dictionary<string, CentralPeripheralScript> ();
		}
		
		if (!_peripheralList.ContainsKey (address))
		{
			GameObject peripheralObject = (GameObject)Instantiate(centralPeripheralPrefab);
			CentralPeripheralScript script = peripheralObject.GetComponent<CentralPeripheralScript>();
			script.textName.text = name;
			script.textAddress.text = address;
			peripheralObject.transform.SetParent(panelScrollContents);
			peripheralObject.transform.localScale = new Vector3 (1f, 1f, 1f);

			_peripheralList[address] = script;
		}
	}

	/////////////////////////////////////////////////////////
	// event listerner for start streaming ble pressure data
	////////////////////////////////////////////////////////

	public void OnSubscribe ()
	{
		Debug.Log ("address: " + addressConnected + " :end");

		// subscribe for incoming data
		BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress (addressConnected, serviceUUID, characteristicUUID, null, (address, characteristic, bytes) => {
			foreach (byte byteValue in bytes) 
			{
				// get incoming data here
				//BluetoothLEHardwareInterface.Log ("Incoming data: " + System.Convert.ToInt32(byteValue));
			}
		});
			
		// stop scanning
		if (_scanning) 
		{
			OnScan();
		}

		// remove list of devices
		RemovePeripherals();

		// hide menu
		this.GetComponentInParent<CanvasGroup>().interactable = false;
		this.GetComponentInParent<CanvasGroup>().alpha = 0f;
		this.GetComponentInParent<CanvasGroup>().blocksRaycasts = false;
	}

	public void SendByte (int value)
	{
		byte[] data = new byte[] { (byte)value, 0 };
		BluetoothLEHardwareInterface.WriteCharacteristic (addressConnected, serviceUUID, characteristicUUID, data, data.Length, true, (characteristicUUID) => {
			BluetoothLEHardwareInterface.Log ("Write Succeeded");
		});
	}

	public void SendByteHaptic (int mode, int dist)
	{
		byte[] data = new byte[] { (byte)mode, (byte)dist };
		BluetoothLEHardwareInterface.WriteCharacteristic (addressConnected, serviceUUID, characteristicUUID, data, data.Length, true, (characteristicUUID) => {
			BluetoothLEHardwareInterface.Log ("Write Succeeded");
		});
	}
}