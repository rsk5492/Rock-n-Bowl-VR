using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPins : MonoBehaviour {

    private List<Vector3> startPos = new List<Vector3>();
    private List<Transform> pins = new List<Transform>();

    private int numPinsDown;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
        {
            startPos.Add(child.position);
            pins.Add(child);
        }
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in pins)
        {
            if(child.gameObject.activeInHierarchy && child.position.y < -5f)
            {
                child.gameObject.SetActive(false);
                numPinsDown++;
                print(numPinsDown);
            }
            
        }

        if(numPinsDown == pins.Count)
        {
            Reset();
        }
    }

    private void Reset()
    {
        for(int i=0; i< pins.Count; i++)
        {
            pins[i].gameObject.SetActive(true);
            pins[i].position = startPos[i];
            pins[i].rotation = Quaternion.identity;
            Rigidbody r = pins[i].GetComponent<Rigidbody>();
            r.velocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }
        numPinsDown = 0;
    }
}
