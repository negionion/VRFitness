using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	private float posX, posY;

	public AudioClip bossBGM;
	// Use this for initialization
	void Start () {
		gameObject.SetActive(false);
		posX = Random.Range(5f, 10f);
		posY = Random.Range(4f, 7.5f);
	}

	void OnEnable()
	{
		GameObject.Find("BGM").GetComponent<BGM>().changeBGM(bossBGM);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > 10)
			posX = Random.Range(-10f, -5f);
		else if (transform.position.x < -10)
			posX = Random.Range(5f, 10f);
		if (transform.position.y > 10)
			posY = Random.Range(-7.5f, -3f);
		else if (transform.position.y < -3)
			posY = Random.Range(3f, 7.5f);


		transform.Translate(
			posX * Time.deltaTime,
			posY * Time.deltaTime,
			1f * Time.deltaTime * Control.moveSpeed,
			Space.World);
	}


	
}
