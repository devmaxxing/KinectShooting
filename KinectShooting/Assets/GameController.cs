using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class GameController : MonoBehaviour {

    public KinectManager kinectManager;
    public int numPlayers;
    public int gameMode;
    public GameObject p1;
    public GameObject p2;

    private Body[] _Data;
    private GameObject[] crossHairs;
    private GameObject[] targets;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        _Data = kinectManager.getData();
        int numBodies = kinectManager.getNumBodies();

        int player1 = -1;
        int player2 = -1;
        for (int i = 0; i < numBodies; i++)
        {
            if (_Data[i].IsTracked)
            {
                if (player1 == -1)
                    player1 = i;
                else
                    player2 = i;
            }
        }
        moveGameObject(p1, player1);
        moveGameObject(p2, player2);
    }

    void moveGameObject(GameObject g, int playerIndex)
    {
        if (playerIndex > -1)
        {
            if (_Data[playerIndex].HandRightState != HandState.Closed)
            {
                float horizontal =
                    (float)(_Data[playerIndex].Joints[JointType.HandRight].Position.X
                        * 50 - _Data[playerIndex].Joints[JointType.ShoulderRight].Position.X * 60);
                float vertical =
                    (float)(_Data[playerIndex].Joints[JointType.HandRight].Position.Y
                        * 30);

                g.transform.position = new Vector2(horizontal, vertical);
            }
        }
    }
}
