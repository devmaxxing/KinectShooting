using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public int playerID;
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
        if (Input.GetMouseButtonDown(0)) {
            shoot();
        }
    }

    private void shoot()
    {
        var targets = GameObject.FindGameObjectsWithTag("Target");
        foreach(GameObject target in targets)
        {
            Collider2D targetCollider = target.GetComponent<Collider2D>();
            if (targetCollider.bounds.Intersects(this.gameObject.GetComponent<Collider2D>().bounds))
            {
                Destroy(target);
                gameController.increaseScore(1, playerID);
                break;
            }
        }
    }
}
