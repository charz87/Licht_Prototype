using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public float rotationVelocity;

    // Update is called once per frame
    void Update() {

        //Rotacion simple del sol
        transform.RotateAround(Vector3.zero, Vector3.forward, rotationVelocity * Time.deltaTime);
        transform.LookAt(Vector3.zero);
	
	}
}
