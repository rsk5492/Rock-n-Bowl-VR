using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bowling : MonoBehaviour {

    public GameObject bowlingPins;
	private Vector3 startposition;
    private StateMachine sm;
    private BowlingPins bpins;

	// Use this for initialization
	void Start () {
		
        bpins = GameObject.Find("Pins").gameObject.GetComponent<BowlingPins>();
        bpins.countCurrentPinDown();
        sm = new StateMachine(bpins);
        Debug.Log("Creating StateMachine was Successful");
		startposition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if ((GetComponent<Rigidbody>().velocity == Vector3.zero && GetComponent<Rigidbody>().angularVelocity == Vector3.zero && transform.position.z < 3.5f) || transform.position.y < -30f)
        {
            sm.displayFrame();
            Invoke("WaitForBall", 0.5f);
            transform.position = startposition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
         }
	}
    public void WaitForBall()
    {
        //yield return new WaitForSeconds(2);

        sm.ExecuteBowl();
        sm.ChangeState();

    }
}
