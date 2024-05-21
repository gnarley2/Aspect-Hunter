using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isInRange = false;

    private void Update()
    {
        if (!isInRange) return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            InformationPanel.Instance.ShowInformation("Save Check point");
            CheckpointManager.Instance.SetCheckpoint(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
