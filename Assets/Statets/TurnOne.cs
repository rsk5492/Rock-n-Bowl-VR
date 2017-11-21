using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOne : IState {
	private BowlingPins bpins;
	private StateMachine state;
 	

	public TurnOne(StateMachine state, BowlingPins bpins){
        this.bpins = bpins;
		this.state = state;
	}

	public void bowl(Score sc, int FrameNo){
		int CurrentPinDown = bpins.countCurrentPinDown ();
        Debug.Log("Turn 1 =>>  Current Pin Down : " + CurrentPinDown);
        if (CurrentPinDown == 10) {
            //Debug.Log("Yay!!! Strike ");
            Strike(sc, FrameNo);
            state.ChangeState ();
		} else {
           // Debug.Log("U were able to manage to hit " + CurrentPinDown);
			sc.updateScore (FrameNo, CurrentPinDown, this);
		}
     }

	private void Strike(Score sc, int FrameNo){
		//this will never be called 
		bpins.Reset();
		sc.updateStrikeScore (FrameNo);
	}

}
