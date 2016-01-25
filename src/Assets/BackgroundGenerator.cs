using UnityEngine;
using System.Collections;

public class BackgroundGenerator : MonoBehaviour {
    public GameObject tree;
    public GameObject hill;
    public GameObject mountain;
    public GameObject cloud;

    private float timeElapsed1 = 0;
    private float timeElapsed2 = 0;
    private float timeElapsed3 = 0;
    private float timeElapsed4 = 0;

    public float spawnTime1 = 1;
    public float spawnTime2 = 3;
    public float spawnTime3 = 5;
    public float spawnTime4 = 2;

    // Update is called once per frame
    void Update () {
        if (GameController.gameStarted) {
            timeElapsed1 += Time.deltaTime;
            timeElapsed2 += Time.deltaTime;
            timeElapsed3 += Time.deltaTime;
            timeElapsed4 += Time.deltaTime;
        
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

            if(timeElapsed4 > spawnTime4)
            {
                GameObject cloudSpawn = GameObject.Instantiate(cloud);
                cloudSpawn.transform.position = new Vector3(45, Random.Range(5, 13));
                cloudSpawn.GetComponent<BackgroundController>().speed = Mathf.RoundToInt(Random.Range(3, 5));
                spawnTime4 = Random.Range(1, 2);
                timeElapsed4 = 0;
            }
        }
    }
}
