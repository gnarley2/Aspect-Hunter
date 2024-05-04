using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase : MonoBehaviour
{
    public enum Phase
    {
        Start,
        End
    }

    [SerializeField]
    public static Phase phase = Phase.Start;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        //if(phase == Phase.Start)
        //{
        //    Debug.Log($"{Phase.Start.ToString()} phase");
        //}
        //else
        //{
        //    Debug.Log($"{Phase.End.ToString()} phase");
        //}
    }
}
