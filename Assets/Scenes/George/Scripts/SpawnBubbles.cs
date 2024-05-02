using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnBubbles : MonoBehaviour
{
    [SerializeField] private int bubblesAmount = 10;
    [SerializeField] private float startAngle;
    [SerializeField] private float endAngle;

    private Vector2 bubbleMoveDirection;

    float angle;
    public float time = 0f;
    public float repeatRate = 0f;

    void Start()
    {
        InvokeRepeating("SpawnInCirclePattern", time, repeatRate);
    }

    private void SpawnInArcPattern()
    {
        startAngle = 90f;
        endAngle = 270f;

        float angleStep = (endAngle - startAngle) / bubblesAmount;
        angle =  startAngle;

        for (int i = 0; i < bubblesAmount + 1; i++)
        {
            Spawn();

            angle += angleStep;
        }
    }
    private void SpawnInCirclePattern()
    {
        startAngle = 0f;
        endAngle = 360f;

        float angleStep = (endAngle - startAngle) / bubblesAmount;
        angle = startAngle;

        for (int i = 0; i < bubblesAmount + 1; i++)
        {
            Spawn();

            angle += angleStep;
        }
    }

    private void Spawn()
    {
        float bubbleDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bubbleDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bubbleMoveVector = new Vector3(bubbleDirX, bubbleDirY, 0f);
        Vector2 bubbleDir = (bubbleMoveVector - transform.position).normalized;

        GameObject bubble = BubblePool.bubblePoolInstance.GetBubble();
        bubble.transform.position = transform.position;
        bubble.transform.rotation = transform.rotation;
        bubble.SetActive(true);
        bubble.GetComponent<Bubble>().SetMoveDirection(bubbleDir);
    }
}
