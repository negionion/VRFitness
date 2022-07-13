using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour {

	public GameObject pauseButton;
	public GameObject pauseUI;
	public Settle settleUI;
	// Use this for initialization
	void Start () {
		pauseUI.SetActive(false);
		if(PanelSel.isVR)
		{
			pauseButton.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pause();
		}
		if(PanelSel.isVR && pauseUI.activeSelf)
		{
			if(Control.attack())
			{
				quit();
			}
			else if(Control.block())
			{
				again();
			}
		}
	}

	public void pause(bool end = false)
	{
		pauseUI.SetActive(!pauseUI.activeSelf);
		if (pauseUI.activeSelf)
		{
			Time.timeScale = 0;
			pauseButton.GetComponentInChildren<Text>().text = "►";
			settleUI.settle(end);
		}
		else
		{
			Time.timeScale = 1;
			pauseButton.GetComponentInChildren<Text>().text = "||";
		}
		
	}

	public void quit()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("gamemenu");
	}

	public void again()
	{
		Time.timeScale = 1;
		SceneManager.LoadSceneAsync("loading");
	}
}
