using UnityEngine;
using System.Collections;

public class GunshotControler : MonoBehaviour {

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.17f);
        Destroy(gameObject);
    }

    void Update()
    {
        StartCoroutine(KillOnAnimationEnd());
    }
}
