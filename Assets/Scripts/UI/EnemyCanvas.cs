using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private BossMovement bossMovement;
    [SerializeField] private BossFollowWP bossFollowWP;

    private Vector2 offset;

    private void Start()
    {
        if (movement != null)
        {
            offset = (Vector2)transform.position - movement.GetPosition();
        }

        if (bossMovement != null)
        {
            offset = (Vector2)transform.position - (Vector2)bossMovement.transform.position;
        }

        if (bossFollowWP != null)
        {
            offset = (Vector2)transform.position - (Vector2)bossFollowWP.transform.position;
        }
    }

    private void Update()
    {
        UpdateCanvasPostion();
    }

    private void UpdateCanvasPostion()
    {
        if (movement != null)
        {
            transform.position = offset + movement.GetPosition();
        }

        if (bossMovement != null)
        {
            transform.position = offset + (Vector2)Camera.main.transform.position + Vector2.down * -6.5f;
        }

        if (bossFollowWP != null)
        {
            transform.position = offset + (Vector2)Camera.main.transform.position + Vector2.down * -6.5f;
        }

        if (movement == null && bossMovement == null && bossFollowWP == null)
        {
            gameObject.SetActive(false);
        }
    }
}
