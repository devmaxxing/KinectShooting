using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;

public class GameController : MonoBehaviour {

    public static bool gameStarted = false;
    public static bool gameOver = false;
    public int numPlayers = 1;
    public int gameMode;
    public float respawnSpeed;
    public int respawnNum;

    public GameObject[] players;
    public GameObject target;
    public GameObject startTarget;
    public Text[] scoreTexts;
    //public Text timeText;
    public Text startText;

    private int[] scores = { 0 };
    private float timeElapsed = 0;

	// Use this for initialization
	void Start () {
        //for(int i = 4; i > numPlayers; i--)
        //{
        //    players[i - 1].SetActive(false);
        //    scoreTexts[i - 1].text = "";
        //}
        Cursor.visible = false;
        startText.text = "Shoot the target to start.";
    }
	
	// Update is called once per frame
	void Update () {
        if (gameStarted && !gameOver) {
            startText.text = "";
            timeElapsed += Time.deltaTime;
            if(timeElapsed > respawnSpeed)
            {
                for(int i = 0; i< respawnNum; i++)
                {
                    float randY = Random.Range(-20,20);
                    GameObject newTarget = Instantiate(target);
                    newTarget.transform.position = new Vector2(50, randY);
                }
                timeElapsed = 0;
            }
            //_Data = kinectManager.getData();
            //int numBodies = kinectManager.getNumBodies();

            //int player1 = -1;
            //int player2 = -1;
            //for (int i = 0; i < numBodies; i++)
            //{
            //    if (_Data[i].IsTracked)
            //    {
            //        if (player1 == -1)
            //            player1 = i;
            //        else
            //            player2 = i;
            //    }
            //}
            //moveGameObject(p1, player1);
            //moveGameObject(p2, player2);
        }else if (gameOver)
        {
            startText.text = "Game Over! Shoot the target to try again.";
            scores[0] = 0;
            startTarget.SetActive(true);
            gameOver = false;
        }
    }

    void moveGameObject(GameObject g, int playerIndex)
    {
        //if (playerIndex > -1)
        //{
        //    if (_Data[playerIndex].HandRightState != HandState.Closed)
        //    {
        //        float horizontal =
        //            (float)(_Data[playerIndex].Joints[JointType.HandRight].Position.X
        //                * 50 - _Data[playerIndex].Joints[JointType.ShoulderRight].Position.X * 60);
        //        float vertical =
        //            (float)(_Data[playerIndex].Joints[JointType.HandRight].Position.Y
        //                * 30);

        //        g.transform.position = new Vector2(horizontal, vertical);
        //    }
        //}
    }

    public void increaseScore(int incBy, int playerID)
    {
        scores[playerID] += incBy;
        if (scores[playerID] % 10 == 0)
        {
            target.GetComponent<TargetScript>().speed += 5;
            if(respawnSpeed!=0.0f)
                respawnSpeed -= 0.05f;
        }
        scoreTexts[playerID].text = "Player " + (playerID + 1) + ": " + scores[playerID];
    }

    public void reset()
    {
        scoreTexts[0].text = "Player 1: " + scores[0];
        target.GetComponent<TargetScript>().speed = 20;
        respawnSpeed = 1;
    }
}
