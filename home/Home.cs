using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
public class Home : MonoBehaviour {



	void Awake()
	{		
		BTsocket.disConnect();
	}

	void Start()
	{
		if(XRSettings.loadedDeviceName != XRSettings.supportedDevices[1])
			XRSettings.LoadDeviceByName(XRSettings.supportedDevices[1]);	
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if(XRSettings.loadedDeviceName == XRSettings.supportedDevices[1])
			XRSettings.enabled = false;
	}

	public void gameStart()
	{
		SceneManager.LoadScene("connect");
	}
	public void arduinoHEXLink(string URL)
	{
		//https://goo.gl/od9cmn
		Application.OpenURL(URL);
	}

}
