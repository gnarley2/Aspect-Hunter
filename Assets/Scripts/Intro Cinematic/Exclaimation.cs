using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exclaimation : MonoBehaviour
{
    public float destroyDelay = 2f;
    // Start is called before the first frame update
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
        Destroy(gameObject);
    }
}
