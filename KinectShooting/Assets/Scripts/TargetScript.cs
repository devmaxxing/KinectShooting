using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    private float timeElapsed = 0;
	// Update is called once per frame
	void Update () {
        timeElapsed += Time.deltaTime;
        if(timeElapsed > 5)
        {
            Destroy(this.gameObject);
        }
	}
}
