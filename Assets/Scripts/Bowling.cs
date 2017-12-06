using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bowling : MonoBehaviour {

    public GameObject bowlingPins;
	private Vector3 startposition;
    private StateMachine sm;
    private BowlingPins bpins;
    AudioSource roll;

	// Use this for initialization
	void Start () {
		
        bpins = GameObject.Find("Pins").gameObject.GetComponent<BowlingPins>();
        sm = StateMachine.getInstance(bpins);
        Debug.Log("Creating StateMachine was Successful");
		startposition = transform.position;
       roll = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if ((GetComponent<Rigidbody>().velocity == Vector3.zero && GetComponent<Rigidbody>().angularVelocity == Vector3.zero && transform.position.z < 33.0f))
        {
            sm.displayFrame();
            roll.Stop();
            Invoke("WaitForBall", 5f);
            transform.position = startposition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
            //roll.Play();
         }
	}
    public void WaitForBall()
    {
        //yield return new WaitForSeconds(2);

        sm.ExecuteBowl();
        sm.ChangeState();

    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag=="Floor")
        {
            if (!roll.isPlaying)
            {
                Debug.Log("touch");
                roll.Play();
            }
            if (roll.isPlaying && GetComponent<Rigidbody>().velocity.magnitude < 0.1)
                roll.Stop();
           
        }
    }
}
