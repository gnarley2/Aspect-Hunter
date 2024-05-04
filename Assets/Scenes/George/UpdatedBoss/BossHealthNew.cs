using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthNew : MonoBehaviour
{
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health < 50)
        {
            BossPhase.phase = BossPhase.Phase.End;
        }
        else
        {
            BossPhase.phase = BossPhase.Phase.Start;
        }
    }
}
