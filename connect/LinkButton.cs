﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkButton : MonoBehaviour {

	public string address;
	// Use this for initialization
	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(OnClick);
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void OnClick()
	{
		GetComponentInParent<BTsocket>().connect(address);

	}
}
