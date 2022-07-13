﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public struct BluetoothData
{
	public string name;
	public string address;
	public string service;
	public string rCharacteristic;
	public string wCharacteristic;
	//private static string full = "6e40****-b5a3-f393-e0a9-e50e24dcca9e";
	public BluetoothData(string _name = "", string _address = "")
	{
		name = _name;
		address = _address;
		service = BTsocket.profile.serviceID; //"0001";
		rCharacteristic = BTsocket.profile.readChID; //"0002";	//RX
		wCharacteristic = BTsocket.profile.writeChID; //"0003";	//TX
	}
	public static string fullUUID(string uuid)
	{
		return BTsocket.profile.full.Replace("****", uuid);
	}
	public static bool isEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
		{
			uuid1 = fullUUID(uuid1);
		}
		if (uuid2.Length == 4)
		{
			uuid2 = fullUUID(uuid2);
		}
		return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
	}
}

public struct BTprofile
{
	public string full;
	public string serviceID;
	public string readChID;
	public string writeChID;

	private bool isEmpty()
	{
		if (full == "" || serviceID == "" || readChID == "" || writeChID == "")
		{
			return true;
		}
		return false;
	}

	public void fileReLoad()
	{
		full = PlayerPrefs.GetString("BTfull");
		serviceID = PlayerPrefs.GetString("BTservice");
		readChID = PlayerPrefs.GetString("BTreadCh");
		writeChID = PlayerPrefs.GetString("BTwriteCh");
		if(isEmpty())
		{
			initialize();
		}
		
	}
	public void initialize()
	{
		PlayerPrefs.SetString("BTfull", "6e40****-b5a3-f393-e0a9-e50e24dcca9e");
		PlayerPrefs.SetString("BTservice", "0001");
		PlayerPrefs.SetString("BTreadCh", "0002");
		PlayerPrefs.SetString("BTwriteCh", "0003");
		fileReLoad();
	}

	public void Save(BTprofile _profile)
	{
		PlayerPrefs.SetString("BTfull", _profile.full);
		PlayerPrefs.SetString("BTservice", _profile.serviceID);
		PlayerPrefs.SetString("BTreadCh", _profile.readChID);
		PlayerPrefs.SetString("BTwriteCh", _profile.writeChID);
		fileReLoad();
	}
}


public class BTsocket : MonoBehaviour {
	public static BTprofile profile;
	private static bool iniStatus = false;
	public static bool BTStatus = false;

	private Dictionary<string, BluetoothData> peripheralList;
	public static BluetoothData linkData;

	private static bool isConnected = false;
	private bool readFound = false;
	private bool writeFound = false;

	public static string BTLog;

	// Use this for initialization
	void Start () {
		initialize();
		Invoke("scan", 2f);
	}

	private void initialize()
	{
		requestAndroidPermission();
		profile.fileReLoad();
		if (iniStatus)
			return;
		BTLog = "Initialising bluetooth\n";
		BluetoothLEHardwareInterface.Initialize(true, false, () => {; }, (error) => {; });
		iniStatus = true;
	}

	public void scan()
	{
		BTLog = "Starting scan \n";
		BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null,
			(address, name) => { addPeripheral(address, name); },
			(address, name, rssi, advertisingInfo) => {; });
	}

	private void addPeripheral(string address, string name)
	{		
		if (peripheralList == null)
		{
			peripheralList = new Dictionary<string, BluetoothData>();
		}
		if (!peripheralList.ContainsKey(address))
		{
			BTLog += ("Found " + address + " \n");
			peripheralList[address] = new BluetoothData(name, address);
			//新增一個可點擊連線的按鈕
			GetComponent<BTManager>().addPeripheralButton(address, name);
		}
		else
		{
			BTLog += "No address found \n";
		}
	}

	public void connect(string addr)
	{
		if (isConnected || !peripheralList.ContainsKey(addr))
			return;
		BTStatus = false;
		linkData = new BluetoothData();
		isConnected = false;
		readFound = false;
		writeFound = false;
		BTLog = "Connecting to \n" + addr + "\n";
		BluetoothLEHardwareInterface.ConnectToPeripheral(addr, (address) => { isConnected = true; },
		   (address, serviceUUID) => {; },
		   (address, serviceUUID, characteristicUUID) =>
		   {
			   // discovered characteristic	
			   if (BluetoothData.isEqual(serviceUUID, peripheralList[address].service))
			   {				   
				   linkData.name = peripheralList[address].name;
				   linkData.address = address;
				   linkData.service = peripheralList[address].service;
				   if (BluetoothData.isEqual(characteristicUUID, peripheralList[address].rCharacteristic))
				   {
					   readFound = true;
					   linkData.rCharacteristic = peripheralList[address].rCharacteristic;
					   BTLog += "readTrue \n";
				   }
				   if (BluetoothData.isEqual(characteristicUUID, peripheralList[address].wCharacteristic))
				   {
					   writeFound = true;
					   linkData.wCharacteristic = peripheralList[address].wCharacteristic;
					   BTLog += "writeTrue \n";
				   }

				   if (readFound && writeFound)
				   {
					   Invoke("delayConnect", 1f);
					   BTLog = address + "\n";
					   BTLog += "Connected! \n";
					   PlayerPrefs.SetString("preConnectMAC",address);
				   }
				   BluetoothLEHardwareInterface.StopScan();
			   }
		   },
		   (address) =>
		   {
			   // this will get called when the device disconnects 
			   // be aware that this will also get called when the disconnect 
			   // is called above. both methods get call for the same action 
			   // this is for backwards compatibility
		   });
	}

	private void delayConnect()
	{
		BTStatus = true;
	}

	public static void subscribe()
	{
		if (!BTStatus)
			return;
		BTLog = "readStart!\n";
		/*BTLog += linkData.address;
		BTLog += BluetoothData.fullUUID(linkData.service);
		BTLog += BluetoothData.fullUUID(linkData.rCharacteristic);*/
		BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress(
			linkData.address, BluetoothData.fullUUID(linkData.service), BluetoothData.fullUUID(linkData.rCharacteristic),
			(deviceAddress, notification) => {; },
			(deviceAddress2, characteristic, data) =>
			{				
				if (deviceAddress2.CompareTo(linkData.address) == 0) //讀資料
				{
					if (data.Length > 0)
					{
						string s = Encoding.UTF8.GetString(data); ///Byte to string
						receiveText(s);
					}
				}
			});
	}

	private static void receiveText(string s)   //讀取資料後的操作
	{
		BTLog = s;
		Control.analysis(s);
	}

	public static void disConnect()
	{
		if (!isConnected)
			return;
		BluetoothLEHardwareInterface.UnSubscribeCharacteristic(
			linkData.address, BluetoothData.fullUUID(linkData.service), BluetoothData.fullUUID(linkData.rCharacteristic),
			(deviceAddress) => {BTLog = "UnSubscribe " + deviceAddress + " \n";});

		BluetoothLEHardwareInterface.DisconnectPeripheral(linkData.address,
			(deviceAddress) =>
			{
				BTLog += "Disconnect " + deviceAddress + " \n";
				isConnected = false;
				BTStatus = false;
			});
	}

	public static void requestAndroidPermission()
	{
		#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        #endif
	}

}
