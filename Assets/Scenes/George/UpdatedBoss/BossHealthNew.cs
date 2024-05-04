using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthNew : MonoBehaviour
{
    //public int health;
    Health bossHealth;
    [SerializeField]
    float phaseShiftThreshold;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth = GetComponent<Health>();
        phaseShiftThreshold = bossHealth.GetPercent();
    }

    // Update is called once per frame
    void Update()
    {
        if (phaseShiftThreshold < 50f)
        {
            BossPhase.phase = BossPhase.Phase.End;
        }
        else
        {
            BossPhase.phase = BossPhase.Phase.Start;
        }
    }
}
