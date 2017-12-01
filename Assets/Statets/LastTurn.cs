using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastTurn: IState {

	private BowlingPins bpins;
    private int turnCount = 0;
    private StateMachine state;
    private bool turn3;
    public LastTurn(StateMachine st, BowlingPins bpins)
    {
        this.bpins = bpins;
        this.state = st;
    }

	public void bowl(Score sc, int FrameNo){
        turnCount++;
        int currentPinDown = bpins.countCurrentPinDown();
        int totalPinDown = bpins.TotalPinDown();
        if (turn3)
        {
            sc.updateScore(FrameNo, currentPinDown, totalPinDown, this);
            state.setState(null);
        }
        else
        {
            if (totalPinDown != 10 && currentPinDown != 10) {
                sc.updateScore(FrameNo, currentPinDown, totalPinDown, this);
                if (turnCount == 2)
                {
                    Text Comments = GameObject.Find("Comment").GetComponent<Text>();
                    Comments.text = "Thank you for playing coconut ball";
                    state.setState(null);
                    Debug.Log("Game over thanks for playing");
                }
            }
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