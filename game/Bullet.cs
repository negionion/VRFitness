using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private float iniPosZ;
	public float speed;
	public float shootRange;
	void OnEnable()
	{
		iniPosZ = transform.position.z;
		//transform.rotation = GameObject.Find("character").GetComponent<Transform>().rotation;
	}


	void Update () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
		if(Mathf.Abs(transform.position.z - iniPosZ) > shootRange)
			GetComponentInParent<ObjPool>().recovery(gameObject);
	}

	void OnTriggerEnter(Collider trig)
	{
		if (trig.tag == "enemy" && tag == "attack")
		{
			Debug.Log("hit!");

			//test
			GameObject.Find("boomPool").GetComponent<ObjPool>().reuse(transform.position);

			GetComponentInParent<ObjPool>().recovery(gameObject);
		}
	}
}
