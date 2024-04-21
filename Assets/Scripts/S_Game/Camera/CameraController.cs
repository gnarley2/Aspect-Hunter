using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    private GameObject player;
    
    private void Awake()
    {
        virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        virtualCam.Follow = player.transform;
        virtualCam.LookAt = player.transform;
    }
}
