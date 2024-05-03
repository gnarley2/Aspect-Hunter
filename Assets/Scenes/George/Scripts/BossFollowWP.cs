using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollowWP : MonoBehaviour
{
    public GameObject[] waypoints;
    public int currentWP = 0;

    public float speed = 5.0f;

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if (Vector2.Distance(this.transform.position, waypoints[currentWP].transform.position) < 0.5f)
        {
            currentWP++;
        }

        if (BossPhase.phase == BossPhase.Phase.Start)
        {
            if (currentWP >= (waypoints.Length / 2))
            {
                currentWP = 0;
            }
        }
        if (BossPhase.phase == BossPhase.Phase.End)
        {
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }

        this.transform.position = Vector2.MoveTowards(this.transform.position, waypoints[currentWP].transform.position, step);
    }
}
