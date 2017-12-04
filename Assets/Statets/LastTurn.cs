using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastTurn: IState {

	private BowlingPins bpins;
    private int turnCount = 0;
    private StateMachine state;
    private int turnOnePinDown = 0;
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
        if (turnCount == 3 && turn3)
        {
            sc.lastTurnScore(FrameNo, turnCount, currentPinDown, totalPinDown, turn3, turnOnePinDown);
            gameover();
        }
        else
        {
            if(turnCount == 1)
            {
                turnOnePinDown = totalPinDown;
            }
            if (totalPinDown == 10 && turnCount <= 2)
            {
                bpins.Reset();
                turn3 = true;
            }
            sc.lastTurnScore(FrameNo, turnCount, currentPinDown, totalPinDown, turn3, turnOnePinDown);

            if (!turn3 && turnCount == 2)
            {
                //gameover
                gameover();

            }
        }
        
	}

    public void gameover()
    {
        System.Threading.Thread.Sleep(10000);
        bpins.Reset();
        turnOnePinDown = 0;
        turnCount = 0;
        state.gameReset();

    }

}