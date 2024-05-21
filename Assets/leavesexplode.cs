using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leavesexplode : MonoBehaviour
{
    public float destroyDelay = 2f;


    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyAfterTime(destroyDelay));
    }
    IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        // sceneEndScript.LoadSceneFromTrigger(0);
        Destroy(gameObject);
    }
}
