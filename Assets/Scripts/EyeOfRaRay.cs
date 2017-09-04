using UnityEngine;
using System.Collections;

public class EyeOfRaRay : MonoBehaviour
{
	private Color color; 
	private GameLogic gameLogic;
	private string name;

	void Start() {
		color = gameObject.GetComponent<Renderer>().material.color; 
		gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 0f);
		gameLogic = GameObject.FindGameObjectWithTag ("GameLogic").GetComponent<GameLogic> ();
		name = "";
	}

	public void RayOn(string objName) {
		name = objName;
		StartCoroutine("Appear");
	}

	public void RayOff() {
		StopCoroutine ("Appear");
		gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 0f);
	}

	IEnumerator Appear() {
		for (float f = 0f; f <= 1.05f; f += 0.05f) {
			gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, f);
			yield return new WaitForSeconds(.05f);
		}
		//Debug.Log (color.a);
		gameLogic.InputAnswer(name);
		gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 0f);
	}
}