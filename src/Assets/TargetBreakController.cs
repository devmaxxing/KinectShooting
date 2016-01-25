using UnityEngine;
using System.Collections;

public class TargetBreakController : MonoBehaviour {

    private IEnumerator KillOnAnimationEnd()
    {
        yield return new WaitForSeconds(0.18f);
        Destroy(gameObject);
    }

    void Update()
    {
        StartCoroutine(KillOnAnimationEnd());
    }
}
