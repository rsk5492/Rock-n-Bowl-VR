using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPins : MonoBehaviour {

    private List<Vector3> startPos = new List<Vector3>();
    private List<Transform> pins = new List<Transform>();
    private List<Transform> Defaultpins = new List<Transform>();

    private int numPinsDown;
    private int lastPinDown;
    private int currentPinDown;
    private bool previousPinDown;
    private static object syncRoot = new Object();

    // Use this for initialization
    void Start () {
		foreach(Transform child in transform)
        {
            startPos.Add(child.position);
            Defaultpins.Add(child);
            pins.Add(child);
        }
        previousPinDown = false;
	}
	
	// Update is called once per frame
	void Update () {
        List<Transform> temPins = new List<Transform>();

        foreach (Transform child in pins)
        {
            if (child.gameObject.activeInHierarchy && child.transform.localPosition.y > -2f  && 
                child.transform.localPosition.y < 0.025f && child.transform.rotation.z!=0 && child.transform.rotation.x!=0
                && child.transform.rotation.y != 0)
            {
                StartCoroutine(removePins(child));
            }
            else
            {
                temPins.Add(child);
            }
        }
        pins = temPins;
    }

    private IEnumerator removePins(Transform child)
    {
            yield return new WaitForSeconds(2f);
            child.gameObject.SetActive(false);
            numPinsDown++;
    }

    /*private IEnumerator CountPins(Transform child)
    {

        while (true)
        {

            yield return new WaitForSeconds(2.5f);
            numPinsDown++;

            //print("WaitAndPrint " + Time.time);
        }
    }*/
    public void Reset()
    {
        pins.Clear();
        int i = 0;
        foreach (Transform child in Defaultpins)
        {
            //startPos.Add(child.position);
            pins.Add(child);
            child.gameObject.SetActive(true);
            child.transform.position = startPos[i++];
            child.rotation = Quaternion.identity;
            Rigidbody r = child.GetComponent<Rigidbody>();
            r.velocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }
        /*for (int i=0; i< pins.Count; i++)
        {
            //Text frameScore = GameObject.Find("Pin");
            pins[i].gameObject.SetActive(true);
            pins[i].position = startPos[i];
            pins[i].rotation = Quaternion.identity;
            Rigidbody r = pins[i].GetComponent<Rigidbody>();
            r.velocity = Vector3.zero;
            r.angularVelocity = Vector3.zero;
        }*/
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
