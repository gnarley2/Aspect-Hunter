using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologramman : MonoBehaviour
{
    public float destroyDelay = 2f;

   // private introSceneEnd sceneEndScript;

    // Start is called before the first frame update
    void Start()
    {
       // sceneEndScript = FindObjectOfType<introSceneEnd>();
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
