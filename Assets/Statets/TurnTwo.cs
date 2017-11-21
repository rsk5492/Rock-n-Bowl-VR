using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTwo : IState {
	private BowlingPins bpins;
	private StateMachine state;
	
	public TurnTwo(StateMachine state, BowlingPins bpins){
        //Debug.Log("Turn two state is created");
        this.bpins = bpins;
		this.state = state;
	}

	public void bowl(Score sc, int FrameNo)
    {
		int CurrentPinDown = bpins.countCurrentPinDown ();
		int totalPinDown = bpins.TotalPinDown ();
        Debug.Log("Turn 2 =>> Total Pin down : " + totalPinDown + ", Current Pin Down : " + CurrentPinDown);
         
        if (totalPinDown == 10) {
            sc.updateSpareScore(FrameNo, CurrentPinDown);
		}
		else {
            sc.updateScore(FrameNo, totalPinDown, this);
        }
        if (FrameNo < 10)
        {
            bpins.Reset();
        }
        else
        {
            Debug.Log("Game Over");
        }
    }
	
}
