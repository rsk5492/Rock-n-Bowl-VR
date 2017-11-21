using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTurn: IState {

	private BowlingPins bpins;
    private int turnCount = 0;
    private StateMachine state;
    private bool turn3 = false;
    public LastTurn(StateMachine st, BowlingPins bpins)
    {
        this.bpins = bpins;
        this.state = st;
    }

	public void bowl(Score sc, int FrameNo){
        turnCount++;
        int currentPinDown = bpins.countCurrentPinDown();
        int totalPinDown = bpins.countCurrentPinDown();
        if (turnCount == 3 && turn3)
        {
            sc.updateScore(FrameNo, currentPinDown, this);
            state.setState(null);
            Debug.Log("Game over thanks for playing" );
            return;
        }
        else if (turnCount == 3)
        {
            state.setState(null);
            Debug.Log("Game over thanks for playing" );
            return;
        }
        else
        {
            if (totalPinDown != 10 && currentPinDown != 10)
                sc.updateScore(FrameNo, currentPinDown, this);
            else
            {
                if (currentPinDown == 10)
                    sc.updateStrikeScore(FrameNo);
                else
                    sc.updateSpareScore(FrameNo, currentPinDown);
                turn3 = true;
                bpins.Reset();
            }
        }
        
	}

}