using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StateMachine {

	private IState CurrentState = null;
    private IState Turnone;
    private IState Turntwo;
    private IState lastTurn;
	private Score sc;
    private int startFrame = 1;
    private int FrameNo = 1;
	private int TotalFrames = 10;


    private static volatile StateMachine Instance;
    private static object syncRoot = new Object();


   
    private StateMachine(BowlingPins bpins)
    {
        Debug.Log("Creating a state machine and Turn one and two as states");
        Turntwo = new TurnTwo(this, bpins);
        Turnone = new TurnOne(this, bpins);
        lastTurn = new LastTurn(this, bpins);
        CurrentState = Turnone;
        FrameNo = startFrame;
        sc = new Score();
    }
    public static StateMachine getInstance(BowlingPins bpins)
    {
        if(null == Instance)
        {
            lock (syncRoot)
            {
                if (null == Instance)
                {
                    Instance = new StateMachine(bpins);
                }
            }
        }
        return Instance;
    }

	public void ChangeState(){
		
		if (FrameNo < TotalFrames) {
            if (CurrentState.GetType ().ToString ().Equals ("TurnOne")) {
				//Debug.Log ("Changing state from turn one to turn two");
				CurrentState = Turntwo;
			} else {
				//Debug.Log ("Changing state from turn two to turn one");
				CurrentState = Turnone;
                FrameNo++;
            }
            if (FrameNo == TotalFrames)
            {
                CurrentState = lastTurn;
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

    public void gameReset()
    {
        sc = new Score();
        FrameNo = startFrame;
        for (int i =1; i<=21; i++)
        {
            Text bowl = GameObject.Find("Bowl" + i).GetComponent<Text>();
            bowl.text = "";
            if (i <= 10)
            {
                Text frameScore = GameObject.Find("Score" + i).GetComponent<Text>();
                frameScore.text = "";
            }
        }


    }


}
