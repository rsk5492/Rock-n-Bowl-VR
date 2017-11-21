using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

	private IState CurrentState = null;
    private IState Turnone;
    private IState Turntwo;
    private IState lastTurn;
	private Score sc;
    private int FrameNo = 1;
	private int TotalFrames = 10;

    public StateMachine(BowlingPins bpins)
    {
        Debug.Log("Creating a state machine and Turn one and two as states");
        Turntwo = new TurnTwo(this, bpins);
        Turnone = new TurnOne(this, bpins);
        lastTurn = new TurnOne(this, bpins);
        CurrentState = Turnone;
        sc = new Score();
    }

	public void ChangeState(){
		
		if (FrameNo < TotalFrames) {
            if (FrameNo == TotalFrames)
            {
                CurrentState = lastTurn;
            }

            else if (CurrentState.GetType ().ToString ().Equals ("TurnOne")) {
				//Debug.Log ("Changing state from turn one to turn two");
				CurrentState = Turntwo;
			} else {
				//Debug.Log ("Changing state from turn two to turn one");
				CurrentState = Turnone;
                FrameNo++;
            } 	
			
		} 
	}

    public void displayFrame()
    {
        //yield return new WaitForSeconds(2);
        Debug.Log("Current frame no is : " + FrameNo);
        Debug.Log("Bowling in or current state is " + CurrentState.GetType().ToString());

    }

    public void ExecuteBowl(){
        
        CurrentState.bowl (sc, FrameNo);
        //sc.displayScore();
	}

	public IState getState(){
		return CurrentState;
	}
    
    public void setState(IState nextState)
    {
        CurrentState = nextState;
    }


}
