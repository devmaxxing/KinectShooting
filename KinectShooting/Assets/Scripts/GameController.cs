using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Windows.Kinect;

public class GameController : MonoBehaviour {

    public int numPlayers;
    public int gameMode;
    public int respawnSpeed;
    public int respawnNum;

    public GameObject[] players;
    public Text[] scoreTexts;
    public Text timeText;

    private int[] scores = { 0 };
    private float timeElapsed = 0;
    private float nextRespawn;
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        nextRespawn = Random.Range(respawnSpeed - 1, respawnSpeed + 1);
    }
	
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
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
        scoreTexts[playerID].text = "Player " + (playerID + 1) + ": " + scores[playerID];
    }
}
