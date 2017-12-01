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
        if (CurrentPinDown == 10) {
            Strike(sc, FrameNo);
            state.ChangeState ();
		} else {
			sc.updateScore (FrameNo, CurrentPinDown, 0, this);
		}
     }

	private void Strike(Score sc, int FrameNo){
		bpins.Reset();
		sc.updateStrikeScore (FrameNo);
	}

}
