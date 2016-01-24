using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    public float speed;
	// Update is called once per frame
	void Update () {
        if (GameController.gameStarted)
        {
            transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x < -50)
            {
                GameController.gameOver = true;
                GameController.gameStarted = false;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
