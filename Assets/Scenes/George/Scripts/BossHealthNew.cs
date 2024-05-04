using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthNew : MonoBehaviour
{
    public int health;
    Health bossHealth;
    //[SerializeField]
    //float phaseShiftThreshold = 0f;

    // Start is called before the first frame update
    void Start()
    {
        bossHealth = this.transform.GetChild(0).GetChild(0).GetComponent<Health>();
        health = bossHealth.GetHealth();
        //Debug.Log($"boss health percent: {health}");
    }

    // Update is called once per frame
    void Update()
    {
        health = bossHealth.GetHealth();
        Debug.Log($"boss health percent: {health}");

        if (health < 50f)
        {
            BossPhase.phase = BossPhase.Phase.End;
        }
        else
        {
            BossPhase.phase = BossPhase.Phase.Start;
        }
    }
}
