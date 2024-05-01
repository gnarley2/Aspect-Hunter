using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXObject : MonoBehaviour
{
    private void Awake()
    {   
        Destroy(gameObject, 1f);
    }
}
