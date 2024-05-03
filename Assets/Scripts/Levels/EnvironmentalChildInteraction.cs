using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalChildInteraction : MonoBehaviour
{
    [SerializeField] private EnvironmentInteraction parentInteraction;
    [SerializeField] private string animName;
    [SerializeField] private bool canDestroy = true;

    private Animator anim;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        parentInteraction.OnTrigger += OnTrigger;
    }

    private void OnTrigger()
    {
        anim.Play(animName);
        if (canDestroy) Destroy(gameObject, 1f);
    }
}
