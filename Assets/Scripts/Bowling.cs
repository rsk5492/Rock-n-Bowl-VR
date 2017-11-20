using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour {

	private Vector3 startposition;

	// Use this for initialization
	void Start () {
		startposition = transform.position;
	}

    // Update is called once per frame
    void Update() {
        if ((GetComponent<Rigidbody>().velocity == Vector3.zero && GetComponent<Rigidbody>().angularVelocity == Vector3.zero && transform.position.z < 5f) || transform.position.y < -5f)
        {
            transform.position = startposition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
	}
}
