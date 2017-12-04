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

    public void updateStrikeScore(int frame, bool lastTurn) {
        int frameId = frame;
        int bowlid = (frameId - 1) * 2 + 1;
        strikeQueue.Enqueue(frame);
        if (lastSpare){
            score += 20;
            frameId = frame - 1;
            lastSpare = false;
            updateScoreBoard(frameId, score.ToString());
        }       
        if (strikeQueue.Count == 3) {
            displayComment("O)>  O)>  O)> Turkey <(O  <(O  <(O ");
            score += 30;
			frameId = (int)strikeQueue.Dequeue();
            updateScoreBoard(frameId, score.ToString());
        }
        if (!lastTurn)
        {
            updateBowlPins(bowlid, "X");
            displayComment("* * * Strike * * * ");
        }
    }


	public void updateSpareScore(int frame, bool lastTurn ){
		int bowlid = (frame - 1) * 2 + 2;
        lastSpare = true;
        if (strikeQueue.Count == 1)
        {
            score += 20;
            frame = (int)strikeQueue.Dequeue();
            updateScoreBoard(frame, score.ToString());
        }
        if (!lastTurn)
        {
            displayComment("~~~~Spare~~~");
            updateBowlPins(bowlid, "/");
            
        }               
	}

    public void sameFrameScoreLogic(int frame, int currentPinDown)
    {
        int frameId = frame;
        if (strikeQueue.Count == 2)
        {
            score += 20 + currentPinDown;
            frameId = (int)strikeQueue.Dequeue();
            updateScoreBoard(frameId, score.ToString());
        }

        if (lastSpare)
        {
            lastSpare = false;
            score += 10 + currentPinDown;
            frameId = frame - 1;
            updateScoreBoard(frameId, score.ToString());
        }
    }

	public void updateScore (int frame, int currentPinDown, int totalPinDown, IState state){
		int frameId = frame ;
        int bowlid;
        if (state.GetType().ToString().Equals("TurnTwo"))
            bowlid = (frameId - 1) * 2 + 2;
        else
            bowlid = (frameId - 1 ) * 2 + 1;

        sameFrameScoreLogic(frame, currentPinDown);

        if (state.GetType().ToString().Equals("TurnTwo")) { //this should be done in only turn 2
            if (strikeQueue.Count == 1)
            {
                score += 10 + totalPinDown;
                frameId = (int)strikeQueue.Dequeue();
                updateScoreBoard(frameId, score.ToString());
            }
            else
                score += totalPinDown;
            updateScoreBoard(frame, score.ToString());
            strikeQueue.Clear();
		}

        if (currentPinDown == 0)
            updateBowlPins(bowlid, "-");
        else
            updateBowlPins(bowlid, currentPinDown.ToString());

    }

    public void lastTurnScore(int frame, int turnCount, int PinsDown, int TotalPinDown, bool turn3, int turnOnePinDown)
    {
        int bowlid = 18 + turnCount;
        
        if (PinsDown == 0)
        {
            updateBowlPins(bowlid, "-");
        }
        if (turnCount <= 2)
        { //this should  be done in only turn 2
            if (PinsDown == 10 && turnOnePinDown != PinsDown)
            {
                updateStrikeScore(frame, true);
                updateBowlPins(bowlid, "X");
            }
            else if (TotalPinDown == 10 && turnCount == 2)
            {
                updateSpareScore(frame, true);
                updateBowlPins(bowlid, "/");
            }
            else
            {
                sameFrameScoreLogic(frame, PinsDown);
                updateBowlPins(bowlid, PinsDown.ToString());
                if (!turn3 && turnCount == 2)
                {
                    score += TotalPinDown;
                    updateScoreBoard(frame, score.ToString());
                }
            }
        }
        else
        {
            if(PinsDown == 10)
            {
                updateBowlPins(bowlid, "X");
            }
            else if(TotalPinDown == 10)
            {
                updateBowlPins(bowlid, "/");
            }
            else
            {
                updateBowlPins(bowlid, PinsDown.ToString());
            }
            while (strikeQueue.Count > 0)
                score += 10;
            if (lastSpare)
            {
                score += 10 + PinsDown;
                lastSpare = false;
            }
            else
                score += TotalPinDown;
            updateScoreBoard(frame, score.ToString());
        }
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