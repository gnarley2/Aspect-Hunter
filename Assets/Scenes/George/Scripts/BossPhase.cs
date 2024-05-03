using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    enum Phase
    {
        Start,
        End
    }

    [SerializeField]
    Phase phase = new();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(phase == Phase.Start)
        {
            Debug.Log($"{Phase.Start.ToString()} phase");
        }
        else
        {
            Debug.Log($"{Phase.End.ToString()} phase");
        }
    }
}
