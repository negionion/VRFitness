using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFire : MonoBehaviour {
	public float speed = 50;
	public float fireRate = 0.5f;
	public GameObject[] launcher = new GameObject[3];
	private Vector3[] fireRotation = new Vector3[3];

	private float timing;
	private int fireSign;
	// Use this for initialization
	void Start () {
		timing = 0;
		InvokeRepeating("fireAngle", 0, 3);
	}
	
	// Update is called once per frame
	void Update () {
		timing += Time.deltaTime;
		if (timing > fireRate)
		{
			timing = 0;
			fireSign = Random.Range(0, 3);
			GameObject bullet;
			bullet = launcher[fireSign].GetComponent<ObjPool>().reuse(launcher[fireSign].transform.position, Quaternion.Euler(fireRotation[fireSign]));
		}
		
	}
	private void fireAngle()
	{
		Debug.Log("rotate!");
		for (int i = 0; i < 3; i++)
		{
			fireRotation[i] = new Vector3(
				Random.Range(-20f, 20f),
				Random.Range(160f, 200f));
		}
	}
}
