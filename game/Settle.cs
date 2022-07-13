using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settle : MonoBehaviour {
	public Text timeScore;
	private float iniTime;
	// Use this for initialization
	void Start () {
		iniTime = Time.timeSinceLevelLoad;
		if (PlayerPrefs.GetInt("timeScore") == 0)
			PlayerPrefs.SetInt("timeScore", 99999);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void settle(bool end = false)
	{
		Score.timeScore = Time.timeSinceLevelLoad - iniTime;
		timeScore.text = "FastestTime：" + PlayerPrefs.GetInt("timeScore") + " s\n";
		timeScore.text += "Time：" + (int)Score.timeScore + " s";
		if (end)
		{
			if (PlayerPrefs.GetInt("timeScore") > (int)Score.timeScore)
			{
				PlayerPrefs.SetInt("timeScore", (int)Score.timeScore);
				timeScore.text += "   new!!";
			}
		}
	}
}
