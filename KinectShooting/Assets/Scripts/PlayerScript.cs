using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class PlayerScript : MonoBehaviour {
    public int playerID;
    public KinectManager kinectManager;
    private GameObject target;
    private GameController gameController;
    // Use this for initialization
    void Start () {
        gameController = GameObject.FindObjectOfType<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!KinectManager.kinectEnabled)
        {
            float distance_to_screen = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
            transform.position = new Vector3(pos_move.x, pos_move.y, transform.position.z);
        }
        else
        {
            Body[] _Data = kinectManager.getData();
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
            if (player1 > -1)
            {
                if (_Data[player1].HandRightState != HandState.Closed)
                {
                    float horizontal =
                        (float)(_Data[player1].Joints[JointType.HandRight].Position.X
                            * 50 - _Data[player1].Joints[JointType.ShoulderRight].Position.X * 60);
                    float vertical =
                        (float)(_Data[player1].Joints[JointType.HandRight].Position.Y
                            * 30);

                    transform.position = new Vector2(horizontal, vertical);
                }
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            shoot();
        }
    }

    public void shoot()
    {
        var targets = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targets)
        {
            Collider2D targetCollider = target.GetComponent<Collider2D>();
            if (targetCollider.bounds.Intersects(this.gameObject.GetComponent<Collider2D>().bounds))
            {
                target.GetComponent<Animator>();
                if (GameController.gameStarted) {
                    Destroy(target);
                    gameController.increaseScore(1, playerID);
                }
                else
                {
                    target.SetActive(false);
                    gameController.reset();
                    GameController.gameStarted = true;
                }
                break;
            }
        }
    }
}
