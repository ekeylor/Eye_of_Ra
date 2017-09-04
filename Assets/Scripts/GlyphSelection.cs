using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphSelection : MonoBehaviour {
	private EyeOfRaRay eye;

	// Use this for initialization
	void Start () {
		eye = GameObject.FindGameObjectWithTag ("EyeOfRaRay").GetComponent<EyeOfRaRay> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log(gameObject.name);
		eye.RayOn (gameObject.name);
	}

	void OnTriggerExit(Collider other) {
		//Debug.Log(gameObject.name);
		eye.RayOff ();
	}
}