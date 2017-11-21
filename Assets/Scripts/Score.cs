using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {

	private Queue strikeQueue = new Queue();
	private bool lastSpare = false;
	private int score = 0;
    public Text comments;
	
	public void updateStrikeScore (int frame){
        int frameId = frame;
        
        Debug.Log("Strike!!!!");

        strikeQueue.Enqueue(frame);
        if (lastSpare)
        {
            score += 20;
            frameId = frame - 1;
            lastSpare = false;
        }       
        if (strikeQueue.Count == 3) {
			score += 30;
			frameId = (int)strikeQueue.Dequeue();
			int frameScore = score;
		}
        updateScoreBoard(frameId, score.ToString());
    }


	public void updateSpareScore(int frame, int PinsDown){
		int frameScore;
        Debug.Log("Spare!!!!");
        lastSpare = true;
        if (strikeQueue.Count == 1){
			score += 20;
			frame = (int)strikeQueue.Dequeue();
		}
		frameScore = score;
        updateScoreBoard(frame, score.ToString());
        
	}

	public void updateScore (int frame, int PinsDown, IState state){
		int frameId = frame ;
		int frameScore = 0;

        Debug.Log("Score turn : " + state.GetType().ToString());
        if (strikeQueue.Count == 2) {
			score += 20 + PinsDown;
            frameId = (int)strikeQueue.Dequeue();
            updateScoreBoard(frameId, score.ToString());
        }
        
        if (lastSpare) {
			lastSpare = false;
			score += 10 + PinsDown;
            frameId = frame - 1;
            updateScoreBoard(frameId, score.ToString());
		}
        if (state.GetType().ToString().Equals("TurnTwo")) { //this should be done in only turn 2
            if (strikeQueue.Count == 1)
            {
                score += 10 + PinsDown;
                frameId = (int)strikeQueue.Dequeue();
                updateScoreBoard(frameId, score.ToString());
            }
            score += PinsDown;
            strikeQueue.Clear();
		}
        frameScore = score; // update on scoreboard.
        //Debug.Log("Current Score: " + score);
        //updateScoreBoard(frame, score.ToString());

    }

    public void lastTurnScore(int frame, int PinsDown)
    {
        while(strikeQueue.Count > 0)
        {
            score += 10;
        }
        score += PinsDown;
        //Debug.Log("Final Score: " + score);
    }

    public void displayScore()
    {
        Debug.Log("Total Score is : " + score);
    }

    public void updateScoreBoard(int frame, string score)
    {
        Debug.Log("Frame no is  : " + frame + ", score is : " + score);
    }
}