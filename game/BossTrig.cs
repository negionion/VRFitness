using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTrig : MonoBehaviour {
	[SerializeField]
	private Image hp;
	[SerializeField]
	private PauseUI settle;
	[SerializeField]
	private GameObject pauseButton;
	public int full = 100;
	private float now;

	void Start()
	{
		now = full;
	}

	void OnTriggerEnter(Collider trig)
	{
		if (trig.tag == "attack")
		{
			now -= 4;
			hp.fillAmount = now / full;
			if(now <= 0)
			{
				GameObject.Find("boomPool").GetComponent<ObjPool>().reuse(transform.position);
				Invoke("gameSettle", 1f);
			}
		}
	}
	private void gameSettle()
	{		
		settle.pause(true);
		pauseButton.SetActive(false);
		gameObject.SetActive(false);
	}
}
