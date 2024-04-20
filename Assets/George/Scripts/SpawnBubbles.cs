using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBubbles : MonoBehaviour
{
    [SerializeField] private int bubblesAmount = 10;
    [SerializeField] private float startAngle = 90f;
    [SerializeField] private float endAngle = 270f;

    private Vector2 bubbleMoveDirection;

    void Start()
    {
        InvokeRepeating("Spawn", 0f, 2f);
    }

    private void Spawn()
    {
        float angleStep = (endAngle - startAngle) / bubblesAmount;
        float angle = startAngle;

        for (int i = 0; i < bubblesAmount + 1; i++)
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

            angle += angleStep;
        }
    }
}
