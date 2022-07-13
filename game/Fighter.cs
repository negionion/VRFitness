using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
	[SerializeField]
	private HP hp;

	void OnTriggerEnter(Collider trig)
	{
		if (trig.tag == "enemy")
		{
			if (tag == "normal")
				hp.lose();
			GameObject.Find("boomPool").GetComponent<ObjPool>().reuse(transform.position);
		}
	}
}
