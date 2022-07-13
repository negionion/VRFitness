﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
public class BTManager : MonoBehaviour {
	public GameObject connectedPanel;
	public GameObject linkButton;
	private int linkButtonPos;

	[SerializeField]
	private GameObject preConnectBtn;
	public Text BTlog;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetString("preConnectMAC").Length == 0)
			preConnectBtn.SetActive(false);
		linkButtonPos = 200;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("home");
		}
		BTlog.text = BTsocket.BTLog;
		connectedPanel.SetActive(BTsocket.BTStatus);
	}

	public void addPeripheralButton(string addr, string name)
	{
		GameObject newPeripheral = Instantiate(linkButton);
		newPeripheral.transform.SetParent(transform);
		newPeripheral.transform.localScale = new Vector3(1, 1, 1);
		newPeripheral.transform.localPosition = new Vector2(-250, linkButtonPos);
		newPeripheral.GetComponent<LinkButton>().address = addr;
		newPeripheral.GetComponentInChildren<Text>().text = name + "\n" + addr.ToUpper();

		linkButtonPos -= 200;
	}

	public void preConnect()
	{
		GetComponent<BTsocket>().connect(PlayerPrefs.GetString("preConnectMAC"));
	}

	public void disConnected()
	{
		BTsocket.disConnect();
		Invoke("delayScan", 2f);
	}
	private void delayScan()
	{
		SceneManager.LoadSceneAsync("connect");
	}

	public void gameStart()
	{
		BTsocket.subscribe();
		Time.timeScale = 1;	
		XRSettings.enabled = PanelSel.isVR;
		SceneManager.LoadScene("gamemenu");
	}

}
