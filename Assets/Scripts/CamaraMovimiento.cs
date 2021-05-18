using UnityEngine;
using System.Collections;

public class CamaraMovimiento : MonoBehaviour {

	public GameObject target;
	private Vector3 distance;

	void Start(){

		distance = transform.position;

	}

	// Update is called once per frame
	void Update () {

		transform.position = target.transform.position + distance;

	
	
	}
}
