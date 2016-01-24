using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    public float speed;
	// Update is called once per frame
	void Update () {
        transform.position = transform.position - new Vector3(speed*Time.deltaTime, 0,0);
    }
}
