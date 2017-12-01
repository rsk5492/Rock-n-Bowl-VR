using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {

	private Queue strikeQueue = new Queue();
	private bool lastSpare = false;
	private int score = 0;
    //public bool updateScoreStrike = false;
    //public bool updateScoreSpare = false;
    //public string displayComments = "Welcome to coconut alley!!!";
    public Text Comments;
    public Text[] Turns;
    public Text[] ScoreText;

   public void Start()
    {
        Comments.text = "Welcome to coconut alley!!!";       
    }
    public void Update() {
        
    }
	
	public void updateStrikeScore (int frame){
        int frameId = frame;
        int bowlid = (frameId - 1) * 2 + 1;
        updateBowlPins(bowlid, "X");
        displayComment("* * * Strike * * * ");
        
        strikeQueue.Enqueue(frame);
        if (lastSpare)
        {
            score += 20;
            frameId = frame - 1;
            lastSpare = false;
        }       
        if (strikeQueue.Count == 3) {
            displayComment("O)>  O)>  O)> Turkey <(O  <(O  <(O ");
            score += 30;
			frameId = (int)strikeQueue.Dequeue();
			int frameScore = score;
		}
        //updateScoreBoard(frameId, score.ToString());
    }


	public void updateSpareScore(int frame, int PinsDown){
		int bowlid = (frame - 1) * 2 + 2;

        //Comments.text = "Spare!!!!";
        displayComment("~~~~Spare~~~");
        updateBowlPins(bowlid, "/");
        //updateScoreSpare = true;
        lastSpare = true;
        if (strikeQueue.Count == 1){
			score += 20;
			frame = (int)strikeQueue.Dequeue();
		}
       // updateScoreBoard(frame, score.ToString());
        
	}

	public void updateScore (int frame, int currentPinDown, int totalPinDown, IState state){
		int frameId = frame ;
        int bowlid;
        if (state.GetType().ToString().Equals("TurnTwo")){
            bowlid = (frameId - 1) * 2 + 2;
        }
        else
        {
            bowlid = (frameId - 1 ) * 2 + 1;
        }
        if (currentPinDown == 0)
            updateBowlPins(bowlid, "-");
        else
            updateBowlPins(bowlid, currentPinDown.ToString());

        Debug.Log("Score turn : " + state.GetType().ToString());
        if (strikeQueue.Count == 2) {
			score += 20 + currentPinDown;
            frameId = (int)strikeQueue.Dequeue();
            updateScoreBoard(frameId, score.ToString());
        }
        
        if (lastSpare) {
			lastSpare = false;
			score += 10 + currentPinDown;
            frameId = frame - 1;
            updateScoreBoard(frameId, score.ToString());
		}
        if (state.GetType().ToString().Equals("TurnTwo")) { //this should be done in only turn 2
            if (strikeQueue.Count == 1)
            {
                score += 10 + totalPinDown;
                frameId = (int)strikeQueue.Dequeue();
                
            }else
                score += totalPinDown;
            updateScoreBoard(frameId, score.ToString());
            strikeQueue.Clear();
		}
    }

    public void lastTurnScore(int frame, int PinsDown)
    {
        while(strikeQueue.Count > 0)
        {
            score += 10;
        }
        score += PinsDown;
    }

    public void displayComment(string cmnt)
    {
        Comments = GameObject.Find("Comment").GetComponent<Text>();
        Comments.text = cmnt;
    }
    public void updateBowlPins(int bowlid, string display)
    {
        Debug.Log("Bowl id is  : " + bowlid + ", displays " + display);
        Text bowl = GameObject.Find("Bowl" + bowlid).GetComponent<Text>();
        bowl.text = display;
    }

    public void updateScoreBoard(int frame, string score)
    {
        Debug.Log("Frame no is  : " + frame + ", score is : " + score);
        Text frameScore = GameObject.Find("Score" + frame).GetComponent<Text>();
        frameScore.text = score;
    }

}