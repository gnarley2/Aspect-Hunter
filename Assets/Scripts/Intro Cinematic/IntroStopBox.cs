using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStopBox : MonoBehaviour
{

    private introSceneEnd sceneEndScript;
    public Animator animator;  // Reference to the Animator component
    public Animator Cameraanimator;
    public float delay = 1f;
    public float leavesdelay = 2f;
    public float hologramDelay = 3f;
    public float zoomDelay = 3f;
    public float scenedelay = 3f;
    public GameObject prefab;
    public GameObject Hologramprefab;
    public GameObject leavesprefab;
    public Transform instantiationLocation;
    public Transform HoloinstantiationLocation;
    public Transform leavesinstantiationLocation;
    // Start is called before the first frame update
    void Start()
    {
        sceneEndScript = FindObjectOfType<introSceneEnd>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method is called when the Collider2D enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (animator != null)
        {
            // Set the "Open" trigger in the Animator
         
            StartCoroutine(SetDelay(delay));
               StartCoroutine(leavesDelay(leavesdelay));
            StartCoroutine(HologramDelay(hologramDelay));
            StartCoroutine(ZoomDelay(zoomDelay));
            StartCoroutine(sceneDelay(scenedelay));
         
        }
    }
    IEnumerator SetDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
         animator.SetTrigger("Open");
         if (prefab != null && instantiationLocation != null)
         {
             Instantiate(prefab, instantiationLocation.position, instantiationLocation.rotation);
            
        }
    }

    IEnumerator HologramDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("Open");
        if (prefab != null && instantiationLocation != null)
        {
            Instantiate(Hologramprefab, HoloinstantiationLocation.position, HoloinstantiationLocation.rotation);
           
        }
    }

    IEnumerator ZoomDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Cameraanimator.SetTrigger("Zoom");

    }

    IEnumerator sceneDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        sceneEndScript.LoadSceneFromTrigger(1);

    }

    IEnumerator leavesDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (leavesprefab != null && leavesinstantiationLocation != null)
        {
            Instantiate(leavesprefab, leavesinstantiationLocation.position, leavesinstantiationLocation.rotation);

        }

    }
}