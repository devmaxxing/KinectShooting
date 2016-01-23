using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    public float lifeTime;
    private float timeElapsed = 0;
	// Update is called once per frame
	void Update () {
        if(lifeTime != 0) {
            timeElapsed += Time.deltaTime;
            if(timeElapsed > lifeTime)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
