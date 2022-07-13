using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
	private static int score;
	public static float timeScore;
	[SerializeField]
	private Text scoreTxt;
	// Use this for initialization
	void Start () {
		score = 0;
		timeScore = Time.timeSinceLevelLoad;
	}
	
	// Update is called once per frame
	void Update () {
		scoreTxt.text = score.ToString();
	}

	public static void add(int n = 1)
	{
		score += n;
	}

	public static int get()
	{
		return score;
	}

}
