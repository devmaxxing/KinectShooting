using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

    public int speed;
	// Update is called once per frame
	void Update () {
        if (GameController.gameStarted)
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x < -100)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
