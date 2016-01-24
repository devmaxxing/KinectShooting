using UnityEngine;
using System.Collections;

public class BackgroundGenerator : MonoBehaviour {
    public GameObject tree;
    public GameObject hill;
    public GameObject mountain;

    private float timeElapsed1 = 0;
    private float timeElapsed2 = 0;
    private float timeElapsed3 = 0;

    public float spawnTime1 = 1;
    public float spawnTime2 = 3;
    public float spawnTime3 = 5;

    // Update is called once per frame
    void Update () {
        timeElapsed1 += Time.deltaTime;
        timeElapsed2 += Time.deltaTime;
        timeElapsed3 += Time.deltaTime;
        if (timeElapsed1 > spawnTime1)
        {
            GameObject.Instantiate(tree);
            spawnTime1 = Random.Range(0.6f, 2);
            timeElapsed1 = 0;
        }

        if (timeElapsed2 > spawnTime2)
        {
            GameObject.Instantiate(hill);
            spawnTime2 = Random.Range(2, 3);
            timeElapsed2 = 0;
        }

        if(timeElapsed3 > spawnTime3)
        {
            GameObject.Instantiate(mountain);
            spawnTime3 = Random.Range(8, 10);
            timeElapsed3 = 0;
        }
    }
}
