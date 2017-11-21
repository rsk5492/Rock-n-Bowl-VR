using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPins : MonoBehaviour {

    private List<Vector3> startPos = new List<Vector3>();
    private List<Transform> pins = new List<Transform>();

	private int numPinsDown;
    private int lastPinDown;
    private int currentPinDown;
    private bool previousPinDown;

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
        {
            startPos.Add(child.position);
            pins.Add(child);
        }
        previousPinDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (Transform child in pins)
        {
            if(child.gameObject.activeInHierarchy && child.transform.up.y < 0.5f )
            {
                child.gameObject.SetActive(false);
                numPinsDown++;
			} 
        }
    }

    public void Reset()
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
        lastPinDown = 0;
        previousPinDown = false;
    }

	public int countCurrentPinDown(){
        currentPinDown = numPinsDown;
        if (previousPinDown != true)
        {   
            lastPinDown = numPinsDown;
            previousPinDown = true;
        }
       // Debug.Log("Current Pin down : " + currentPinDown + ", last turn Pin Down : " + lastPinDown);
        numPinsDown = 0;
        return currentPinDown;
	}

	public int TotalPinDown(){
        return lastPinDown + currentPinDown;
	}
}
