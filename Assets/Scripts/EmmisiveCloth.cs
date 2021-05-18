using UnityEngine;
using System.Collections;

public class EmmisiveCloth : MonoBehaviour {

	public Renderer rend;
	private Color myColorBlue;
	private float myEmission;

	void Start() {
		rend = GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Standard");

	}
	void Update() {

		myEmission = Mathf.PingPong (Time.time, 1.0f);
		myColorBlue = new Color (0f, myEmission * 2, myEmission * 2);
		rend.material.SetColor("_EmissionColor", myColorBlue);




	}
}
