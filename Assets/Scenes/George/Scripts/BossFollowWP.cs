using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWP = 0;

    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (Vector2.Distance(this.transform.position, waypoints[currentWP].transform.position) < 1)
        {
            currentWP++;
        }

        switch (BossPhase.phase)
        {
            case BossPhase.Phase.Start:
                //if (currentWP == 2)
                //{
                //    currentWP = 0;
                //}
                //if (currentWP == 3)
                //{
                //    currentWP = 1;
                //}
                break;

            case BossPhase.Phase.End:
                //if (currentWP >= waypoints.Length) currentWP = 0;
                break;
        }
        if (currentWP >= waypoints.Length) currentWP = 0;

        this.transform.position = Vector2.MoveTowards(this.transform.position, waypoints[currentWP].transform.position, step);

        StartCoroutine(PrintPhase());
    }

    IEnumerator PrintPhase()
    {
        Debug.Log($"{BossPhase.phase} phase");
        yield return new WaitForSeconds(5);
    }
}
